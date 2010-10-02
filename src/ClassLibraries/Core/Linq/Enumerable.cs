namespace Cavity.Linq
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public static class Enumerable
    {
        public static string Concat(this IEnumerable<string> source, char separator)
        {
            if (null == source)
            {
                return null;
            }

            if (0 == source.Count())
            {
                return string.Empty;
            }

            var buffer = new StringBuilder();
            foreach (var item in source)
            {
                if (0 != buffer.Length)
                {
                    buffer.Append(separator);
                }

                buffer.Append(item);
            }

            return buffer.ToString();
        }
    }
}