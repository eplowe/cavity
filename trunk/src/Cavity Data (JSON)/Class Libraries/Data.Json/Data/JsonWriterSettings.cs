namespace Cavity.Data
{
    using System;
    using System.Linq;

    using Cavity.Properties;

    public class JsonWriterSettings
    {
        private string _colonPadding;

        private string _commaPadding;

        private string _indent;

        private string _itemsName;

        public JsonWriterSettings()
        {
            ColonPadding = string.Empty;
            CommaPadding = string.Empty;
            Indent = string.Empty;
            ItemsName = string.Empty;
        }

        public static JsonWriterSettings Debug
        {
            get
            {
                return new JsonWriterSettings
                           {
                               ColonPadding = " ", 
                               CommaPadding = Environment.NewLine, 
                               Indent = "·"
                           };
            }
        }

        public static JsonWriterSettings Pretty
        {
            get
            {
                return new JsonWriterSettings
                           {
                               ColonPadding = " ", 
                               CommaPadding = Environment.NewLine, 
                               Indent = "\t"
                           };
            }
        }

        public static JsonWriterSettings Terse
        {
            get
            {
                return new JsonWriterSettings();
            }
        }

        public string ColonPadding
        {
            get
            {
                return _colonPadding;
            }

            set
            {
                if (null == value)
                {
                    throw new ArgumentNullException("value");
                }

#if NET20
                foreach (var c in value)
                {
                    if (CharExtensionMethods.IsWhiteSpace(c))
                    {
                        continue;
                    }
                
                    throw new FormatException(Resources.PaddingFormatExceptionMessage);
                }

#else
                if (value.Any(c => !c.IsWhiteSpace()))
                {
                    throw new FormatException(Resources.PaddingFormatExceptionMessage);
                }

#endif

                _colonPadding = value;
            }
        }

        public string CommaPadding
        {
            get
            {
                return _commaPadding;
            }

            set
            {
                if (null == value)
                {
                    throw new ArgumentNullException("value");
                }

#if NET20
                foreach (var c in value)
                {
                    if (CharExtensionMethods.IsWhiteSpace(c))
                    {
                        continue;
                    }
                
                    throw new FormatException(Resources.PaddingFormatExceptionMessage);
                }

#else
                if (value.Any(c => !c.IsWhiteSpace()))
                {
                    throw new FormatException(Resources.PaddingFormatExceptionMessage);
                }

#endif

                _commaPadding = value;
            }
        }

        public string Indent
        {
            get
            {
                return _indent;
            }

            set
            {
                if (null == value)
                {
                    throw new ArgumentNullException("value");
                }

#if NET20
                foreach (var c in value)
                {
                    if (CharExtensionMethods.IsWhiteSpace(c))
                    {
                        continue;
                    }
                
                    throw new FormatException(Resources.PaddingFormatExceptionMessage);
                }

#else
                if (value.Any(c => !c.IsWhiteSpace()))
                {
                    throw new FormatException(Resources.PaddingFormatExceptionMessage);
                }

#endif

                _indent = value;
            }
        }

        public string ItemsName
        {
            get
            {
                return _itemsName;
            }

            set
            {
                if (null == value)
                {
                    throw new ArgumentNullException("value");
                }

                _itemsName = value;
            }
        }
    }
}