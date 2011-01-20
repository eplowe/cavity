namespace Cavity.Collections.Generic
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

            return (threshold + 1) > x.LevenshteinDistance(y)
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

            return value
                .ReplaceAllWith(string.Empty, StringComparison.Ordinal, ".", "'")
                .ReplaceAllWith(" ", StringComparison.Ordinal, "-", ",")
                .Replace(" & ", " and ", StringComparison.Ordinal)
                .ToUpperInvariant()
                .Trim();
        }
    }
}