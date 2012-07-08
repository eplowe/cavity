namespace Cavity.Data
{
    using System;
    using System.Globalization;
    using System.IO;
    using System.Text;

    using Cavity.Dynamic;

    public class JsonStreamReader : StreamReader
    {
        public JsonStreamReader(Stream stream)
            : base(stream)
        {
        }

        private DynamicJson Current { get; set; }

        private bool Escape { get; set; }

        private MutableString Name { get; set; }

        private int Quote { get; set; }

        private MutableString Unicode { get; set; }

        private MutableString Value { get; set; }

        public virtual dynamic ReadEntry()
        {
            Value = new MutableString();
            while (!EndOfStream)
            {
                var c = (char)Read();
                switch (c)
                {
                    case '{':
                        Current = new DynamicJson();
                        break;
                    case '\\':
                        if (Escape)
                        {
                            Property(c);
                        }
                        else
                        {
                            Escape = true;
                        }

                        break;
                    case '"':
                        if (Escape)
                        {
                            Property(c);
                        }
                        else
                        {
                            if (0 == Quote)
                            {
                                Name = new MutableString();
                            }

                            Quote++;
                            Quote = Quote % 4;
                        }

                        break;
                    case '}':
                        if (null != Name)
                        {
                            Current.SetMember(Name, Value);
                        }

                        return Current;
                    default:
                        Property(c);
                        break;
                }
            }
            
            return null;
        }

        private void Property(char c)
        {
            if (Escape)
            {
                switch (c)
                {
                    case 'b':
                        c = '\b';
                        break;
                    case 'f':
                        c = '\f';
                        break;
                    case 'n':
                        c = '\n';
                        break;
                    case 'r':
                        c = '\r';
                        break;
                    case 't':
                        c = '\t';
                        break;
                    case 'u':
                        Unicode = new MutableString();
                        return;
                }

                if (null != Unicode)
                {
                    Unicode += c;
                    if (4 != Unicode.Length)
                    {
                        return;
                    }

                    c = Convert.ToChar(int.Parse(Unicode, NumberStyles.HexNumber, NumberFormatInfo.InvariantInfo));
                    Unicode = null;
                }
            }

            if (1 == Quote)
            {
                Name += c;
            }

            if (3 == Quote)
            {
                Value += c;
            }

            if (Escape)
            {
                Escape = false;
            }
        }
    }
}