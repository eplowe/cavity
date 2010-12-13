namespace Cavity.Models
{
    using System;
    using System.Text;

    public static class AddressStringExtensionMethods
    {
        public static string ExtractFlatNumber(this string value)
        {
            if (null == value)
            {
                throw new ArgumentNullException("value");
            }

            if (0 == value.Length)
            {
                return string.Empty;
            }

            if (!value.StartsWithAny(StringComparison.OrdinalIgnoreCase, "FLAT ", "PLOT ", "UNIT "))
            {
                return string.Empty;
            }

            var buffer = new StringBuilder();
            foreach (var part in value.Split(' ', StringSplitOptions.RemoveEmptyEntries))
            {
                if (0 == buffer.Length)
                {
                    buffer.Append("{0} ".FormatWith(part));
                    continue;
                }

                if (1 == part.Length)
                {
                    buffer.Append(part);
                    break;
                }

                if (char.IsDigit(part[0]))
                {
                    buffer.Append(part);
                    break;
                }
            }

            return buffer.ToString().Trim();
        }

        public static string ExtractHouseNumber(this string value)
        {
            if (null == value)
            {
                throw new ArgumentNullException("value");
            }

            if (0 == value.Length)
            {
                return string.Empty;
            }

            var buffer = new StringBuilder();
            foreach (var part in value.Split(' ', StringSplitOptions.RemoveEmptyEntries))
            {
                if (1 == part.Length)
                {
                    buffer.Append("{0} ".FormatWith(part));
                    continue;
                }

                for (var i = 0; i < part.Length; i++)
                {
                    if (char.IsDigit(part[i]) ||
                        '-'.Equals(part[i]) ||
                        '/'.Equals(part[i]))
                    {
                        buffer.Append(part[i]);
                        continue;
                    }

                    if (i == part.Length - 1)
                    {
                        buffer.Append(part[i]);
                    }

                    return buffer.ToString().Trim();
                }

                buffer.Append(' ');
            }

            return buffer.ToString().Trim();
        }
    }
}