namespace Cavity.IO
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Text;
    using Cavity.Properties;

    public sealed class CsvStreamReader : StreamReader
    {
        public CsvStreamReader(Stream stream)
            : base(stream)
        {
        }

        public int EntryNumber { get; private set; }

        public string Header { get; private set; }

        public string Line { get; private set; }

        public int LineNumber { get; private set; }

        private List<string> Headings { get; set; }

        public IDictionary<string, string> ReadEntry()
        {
            var result = new Dictionary<string, string>();

            if (!EndOfStream)
            {
                if (null == Headings)
                {
                    Headings = new List<string>();
                    foreach (var heading in Next())
                    {
                        Headings.Add(heading);
                    }

                    Header = Line;
                }
            }

            if (!EndOfStream)
            {
                var entry = Next();
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
            var result = new List<string>();

            if (!EndOfStream)
            {
                var buffer = new StringBuilder();
                Line = ReadLine();
                LineNumber++;
                var quote = false;
                foreach (var c in Line)
                {
                    switch (c)
                    {
                        case ',':
                            if (quote)
                            {
                                buffer.Append(c);
                            }
                            else
                            {
                                result.Add(buffer.ToString());
                                buffer.Remove(0, buffer.Length);
                            }

                            break;

                        case '"':
                            if (quote)
                            {
                                quote = false;
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

                result.Add(buffer.ToString());
            }

            return result;
        }
    }
}