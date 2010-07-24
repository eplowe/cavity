namespace Cavity.IO
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
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

        public CsvStreamReader(Stream stream, IEnumerable<string> headers)
            : base(stream)
        {
            if (null == headers)
            {
                throw new ArgumentNullException("headers");
            }

            if (0 == headers.Count())
            {
                throw new ArgumentOutOfRangeException("headers");
            }

            Headings = new List<string>();
            foreach (var header in headers)
            {
                Headings.Add(header);
            }
        }

        public int EntryNumber { get; private set; }

        public string Header { get; private set; }

        public string Line { get; private set; }

        public int LineNumber { get; private set; }

        private List<string> Headings { get; set; }

        public IDictionary<string, string> ReadEntry()
        {
            var result = new Dictionary<string, string>();

            if (null == Headings)
            {
                Headings = new List<string>();
                foreach (var heading in Next())
                {
                    Headings.Add(heading);
                }

                Header = Line;
            }

            var entry = Next();
            if (0 != entry.Count)
            {
                EntryNumber++;
                if (Headings.Count !=
                    entry.Count)
                {
                    var message = string.Format(
                        CultureInfo.InvariantCulture,
                        Resources.CsvStreamReader_ReadEntry_FormatException,
                        LineNumber);
                    throw new FormatException(message);
                }

                for (var i = 0; i < Headings.Count; i++)
                {
                    result.Add(Headings[i], entry[i]);
                }
            }

            return result;
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
            IList<string> result = new List<string>();

            if (!string.IsNullOrEmpty(line))
            {
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
                            }
                            else
                            {
                                result.Add(trim ? buffer.ToString().Trim() : buffer.ToString());
                                buffer.Remove(0, buffer.Length);
                                trim = true;
                            }

                            break;

                        case '"':
                            trim = false;
                            if (quote)
                            {
                                if (i == line.Length - 1)
                                {
                                    quote = false;
                                }
                                else if ('"' == line[i + 1])
                                {
                                    buffer.Append(c);
                                    i++;
                                }
                                else
                                {
                                    quote = false;
                                }
                            }
                            else if (0 == buffer.Length)
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
                    result = Parse(Line);
                }
                else
                {
                    result.Add(trim ? buffer.ToString().Trim() : buffer.ToString());
                }
            }

            return result;
        }
    }
}