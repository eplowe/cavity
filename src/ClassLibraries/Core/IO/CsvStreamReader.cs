namespace Cavity.IO
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;
    using System.Linq;
    using System.Text;
    using Cavity.Properties;

    public sealed class CsvStreamReader : StreamReader
    {
        public CsvStreamReader(Stream stream)
            : base(stream)
        {
        }

        public CsvStreamReader(Stream stream,
                               string header)
            : this(stream, ParseHeader(header))
        {
            Header = header;
        }

        public CsvStreamReader(Stream stream,
                               IEnumerable<string> columns)
            : base(stream)
        {
            if (null == columns)
            {
                throw new ArgumentNullException("columns");
            }

            if (0 == columns.Count())
            {
                throw new ArgumentOutOfRangeException("columns");
            }

            Columns = new List<string>();
            foreach (var header in columns)
            {
                Columns.Add(header);
                Header += (string.IsNullOrEmpty(Header) ? string.Empty : ",") + header;
            }
        }

        public int EntryNumber { get; private set; }

        public string Header { get; private set; }

        public string Line { get; private set; }

        public int LineNumber { get; private set; }

        private List<string> Columns { get; set; }

        public IDictionary<string, string> ReadEntry()
        {
            var result = new Dictionary<string, string>();

            if (null == Columns)
            {
                Columns = new List<string>();
                foreach (var heading in Next())
                {
                    Columns.Add(heading);
                }

                Header = Line;
            }

            var entry = Next();
            if (null == entry)
            {
                return null;
            }

            if (0 != entry.Count)
            {
                EntryNumber++;
                if (Columns.Count !=
                    entry.Count)
                {
                    throw new FormatException(Resources.CsvStreamReader_ReadEntry_FormatException.FormatWith(LineNumber));
                }

                for (var i = 0; i < Columns.Count; i++)
                {
                    result.Add(Columns[i], entry[i]);
                }
            }

            return result;
        }

        [SuppressMessage("Microsoft.Usage", "CA2202:Do not dispose objects multiple times", Justification = "This is an odd rule that seems to be impossible to actually pass.")]
        private static IEnumerable<string> ParseHeader(string header)
        {
            if (null == header)
            {
                throw new ArgumentNullException("header");
            }

            if (0 == header.Length)
            {
                throw new ArgumentOutOfRangeException("header");
            }

            using (var stream = new MemoryStream())
            using (var writer = new StreamWriter(stream))
            {
                writer.WriteLine(header);
                writer.Flush();
                stream.Position = 0;
                using (var reader = new CsvStreamReader(stream))
                {
                    reader.ReadEntry();
                    return reader.Columns;
                }
            }
        }

        private IList<string> Next()
        {
            Line = null;
            while (!EndOfStream)
            {
                Line = ReadLine();
                LineNumber++;
                if (!string.IsNullOrEmpty(Line))
                {
                    break;
                }
            }

            return Parse(Line);
        }

        private IList<string> Parse(string line)
        {
            if (string.IsNullOrEmpty(line))
            {
                return null;
            }

            IList<string> result = new List<string>();

            var trim = true;
            var buffer = new StringBuilder();
            var quote = false;
            for (var i = 0; i < line.Length; i++)
            {
                var c = line[i];
                switch (c)
                {
                    case ',':
                        if (quote)
                        {
                            buffer.Append(c);
                            break;
                        }

                        result.Add(trim ? buffer.ToString().Trim() : buffer.ToString());
                        buffer.Remove(0, buffer.Length);
                        trim = true;
                        break;

                    case '"':
                        trim = false;
                        if (quote)
                        {
                            if (i == line.Length - 1)
                            {
                                quote = false;
                                break;
                            }

                            if ('"' == line[i + 1])
                            {
                                buffer.Append(c);
                                i++;
                                break;
                            }

                            quote = false;
                            break;
                        }

                        if (0 == buffer.Length)
                        {
                            quote = true;
                        }

                        break;

                    default:
                        buffer.Append(c);
                        break;
                }
            }

            if (quote)
            {
                Line = string.Concat(line, Environment.NewLine, ReadLine());
                LineNumber++;
                return Parse(Line);
            }

            result.Add(trim ? buffer.ToString().Trim() : buffer.ToString());

            return result;
        }
    }
}