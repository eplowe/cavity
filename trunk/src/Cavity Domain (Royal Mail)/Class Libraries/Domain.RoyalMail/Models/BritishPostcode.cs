namespace Cavity.Models
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
#if !NET20
    using System.Linq;
#endif
    using Cavity;
    using Cavity.Diagnostics;

    public sealed class BritishPostcode : ComparableObject
    {
        private BritishPostcode()
        {
        }

        private BritishPostcode(string area,
                                string district)
        {
            Area = area;
            District = district;
        }

        private BritishPostcode(string area,
                                string district,
                                string sector,
                                string unit)
            : this(area, district)
        {
            Sector = sector;
            Unit = unit;
        }

        public string Area { get; set; }

        public string District { get; set; }

        public string Sector { get; set; }

        public string Unit { get; set; }

        public static implicit operator BritishPostcode(string value)
        {
            return ReferenceEquals(null, value) ? null : FromString(value);
        }

        public static BritishPostcode FromString(string value)
        {
            Trace.WriteIf(Tracing.Is.TraceVerbose, value);
            if (null == value)
            {
                throw new ArgumentNullException("value");
            }

            value = value
                .NormalizeWhiteSpace()
                .Trim()
                .ToUpperInvariant()
                .Where(c => ' '.Equals(c) || char.IsLetterOrDigit(c))
                .Aggregate(string.Empty, (current, c) => current + c);

            if (0 == value.Length)
            {
                return new BritishPostcode();
            }

            var parts = value.Split(' ');
            var area = ToArea(parts[0]);
            switch (parts.Length)
            {
                case 1:
                    return new BritishPostcode(area, parts[0]);

                case 2:
                    return new BritishPostcode(
                        area,
                        parts[0],
                        string.Concat(parts[0], ' ', ToSector(parts[1])),
                        value);

                default:
                    return new BritishPostcode();
            }
        }

        public override string ToString()
        {
            return Unit ?? string.Empty;
        }

        private static string ToArea(IEnumerable<char> value)
        {
#if NET20
            var result = string.Empty;
            foreach (var c in value)
            {
                if (c >= 'A' && c <= 'Z')
                {
                    result += c;
                }
                else
                {
                    return result;
                }
            }

            return result;
#else
            return value
                .TakeWhile(c => c >= 'A' && c <= 'Z')
                .Aggregate<char, string>(null, (x, c) => x + c);
#endif
        }

        private static string ToSector(IEnumerable<char> value)
        {
#if NET20
            var result = string.Empty;
            foreach (var c in value)
            {
                if (c >= '0' && c <= '9')
                {
                    result += c;
                }
            }

            return result;
#else
            return value
                .Where(c => c >= '0' && c <= '9')
                .Aggregate<char, string>(null, (x, c) => x + c);
#endif
        }
    }
}