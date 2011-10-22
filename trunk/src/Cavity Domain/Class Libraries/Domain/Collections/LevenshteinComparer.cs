namespace Cavity.Collections
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    public abstract class LevenshteinComparer : INormalizationComparer
    {
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "x", Justification = "Following same parameter names as IComparer<string>.")]
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "y", Justification = "Following same parameter names as IComparer<string>.")]
        public virtual int CalculateThreshold(string x,
                                              string y)
        {
            if (null == y)
            {
                return 0;
            }

            return 3 < y.Length
                       ? (y.Length / 3)
                       : 0;
        }

        public virtual int Compare(string x,
                                   string y)
        {
            var threshold = Math.Abs(CalculateThreshold(x, y));
            if (0 == threshold)
            {
                return string.Compare(x, y, StringComparison.OrdinalIgnoreCase);
            }

            if (string.Equals(x, y, StringComparison.OrdinalIgnoreCase))
            {
                return 0;
            }

#if NET20
            return (threshold + 1) > StringExtensionMethods.LevenshteinDistance(x, y)
#else
            return (threshold + 1) > x.LevenshteinDistance(y)
#endif
                       ? 0
                       : string.Compare(x, y, StringComparison.OrdinalIgnoreCase);
        }

        public virtual string Normalize(string value)
        {
            if (null == value)
            {
                return string.Empty;
            }

            if (0 == value.Trim().Length)
            {
                return string.Empty;
            }

#if NET20
            value = StringExtensionMethods.ReplaceAllWith(value, string.Empty, StringComparison.Ordinal, ".", "'");
            value = StringExtensionMethods.ReplaceAllWith(value, " ", StringComparison.Ordinal, "-", ",");
            value = StringExtensionMethods.Replace(value, " & ", " and ", StringComparison.Ordinal);
            return value
#else
            return value
                .ReplaceAllWith(string.Empty, StringComparison.Ordinal, ".", "'")
                .ReplaceAllWith(" ", StringComparison.Ordinal, "-", ",")
                .Replace(" & ", " and ", StringComparison.Ordinal)
#endif
                .ToUpperInvariant()
                .Trim();
        }
    }
}