namespace Cavity.Models
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
#if !NET20
    using System.Linq;
#endif
    using System.Xml;

    using Cavity.Collections;
    using Cavity.Diagnostics;

    public sealed class BritishPostcode : IComparable,
                                          IComparable<BritishPostcode>,
                                          IEquatable<BritishPostcode>
    {
        private static readonly IList<string> _areas = "AB,AL,B,BA,BB,BD,BH,BL,BN,BR,BS,BT,CA,CB,CF,CH,CM,CO,CR,CT,CV,CW,DA,DD,DE,DG,DH,DL,DN,DT,DY,E,EC,EH,EN,EX,FK,FY,G,GIR,GL,GU,GY,HA,HD,HG,HP,HR,HS,HU,HX,IG,IM,IP,IV,JE,KA,KT,KW,KY,L,LA,LD,LE,LL,LN,LS,LU,M,ME,MK,ML,N,NE,NG,NN,NP,NR,NW,OL,OX,PA,PE,PH,PL,PO,PR,RG,RH,RM,S,SA,SE,SG,SK,SL,SM,SN,SO,SP,SR,SS,ST,SW,SY,TA,TD,TF,TN,TQ,TR,TS,TW,UB,W,WA,WC,WD,WF,WN,WR,WS,WV,YO,ZE".Split(',').ToList();

        private BritishPostcode()
        {
        }

        private BritishPostcode(string area,
                                string district,
                                string sector = null,
                                string unit = null)
        {
            Area = area;
            SetDistrict(district);
            SetSector(sector);
            SetUnit(unit);
        }

        public string Area { get; private set; }

        public string District { get; private set; }

        public string InCode { get; private set; }

        public string OutCode { get; private set; }

        public string Sector { get; private set; }

        public string Unit { get; private set; }

        private char? DistrictLetter { get; set; }

        private int? DistrictNumber { get; set; }

        private int? SectorNumber { get; set; }

        private string UnitCode { get; set; }

        public static bool operator ==(BritishPostcode operand1,
                                       BritishPostcode operand2)
        {
            return ReferenceEquals(null, operand1)
                       ? ReferenceEquals(null, operand2)
                       : operand1.Equals(operand2);
        }

        public static bool operator >(BritishPostcode operand1,
                                      BritishPostcode operand2)
        {
            return Compare(operand1, operand2) > 0;
        }

        public static implicit operator BritishPostcode(string value)
        {
            return ReferenceEquals(null, value) ? null : FromString(value);
        }

        public static implicit operator string(BritishPostcode value)
        {
            return ReferenceEquals(null, value)
                       ? null
                       : value.ToString();
        }

        public static bool operator !=(BritishPostcode operand1,
                                       BritishPostcode operand2)
        {
            return ReferenceEquals(null, operand1)
                       ? !ReferenceEquals(null, operand2)
                       : !operand1.Equals(operand2);
        }

        public static bool operator <(BritishPostcode operand1,
                                      BritishPostcode operand2)
        {
            return Compare(operand1, operand2) < 0;
        }

        public static int Compare(BritishPostcode comparand1,
                                  BritishPostcode comparand2)
        {
            if (ReferenceEquals(comparand1, comparand2))
            {
                return 0;
            }

            if (ReferenceEquals(null, comparand1))
            {
                return -1;
            }

            if (ReferenceEquals(null, comparand2))
            {
                return 1;
            }

            var comparison = string.Compare(comparand1.Area, comparand2.Area, StringComparison.OrdinalIgnoreCase);
            if (0 != comparison)
            {
                return comparison > 0 ? 1 : -1;
            }

            if (comparand1.DistrictNumber.HasValue || comparand2.DistrictNumber.HasValue)
            {
                if (!comparand1.DistrictNumber.HasValue)
                {
                    return -1;
                }

                if (!comparand2.DistrictNumber.HasValue)
                {
                    return 1;
                }

                if (comparand1.DistrictNumber.Value != comparand2.DistrictNumber.Value)
                {
                    return comparand1.DistrictNumber.Value.CompareTo(comparand2.DistrictNumber.Value);
                }
            }

            if (comparand1.DistrictLetter.HasValue || comparand2.DistrictLetter.HasValue)
            {
                if (!comparand1.DistrictLetter.HasValue)
                {
                    return -1;
                }

                if (!comparand2.DistrictLetter.HasValue)
                {
                    return 1;
                }

                if (comparand1.DistrictLetter.Value != comparand2.DistrictLetter.Value)
                {
                    return comparand1.DistrictLetter.Value.CompareTo(comparand2.DistrictLetter.Value) > 0 ? 1 : -1;
                }
            }

            if (comparand1.SectorNumber.HasValue || comparand2.SectorNumber.HasValue)
            {
                if (!comparand1.SectorNumber.HasValue)
                {
                    return -1;
                }

                if (!comparand2.SectorNumber.HasValue)
                {
                    return 1;
                }

                if (comparand1.SectorNumber.Value != comparand2.SectorNumber.Value)
                {
                    return comparand1.SectorNumber.Value.CompareTo(comparand2.SectorNumber.Value);
                }
            }

            comparison = string.Compare(comparand1.UnitCode, comparand2.UnitCode, StringComparison.OrdinalIgnoreCase);
            if (0 == comparison)
            {
                return 0;
            }

            return comparison > 0 ? 1 : -1;
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
                .Aggregate(string.Empty, (current,
                                          c) => current + c);

            if (0 == value.Length)
            {
                return new BritishPostcode();
            }

            var parts = value.Split(' ');
            var area = ToArea(parts[0]);
            if (!_areas.Contains(area))
            {
                return new BritishPostcode();
            }

            BritishPostcode result;
            switch (parts.Length)
            {
                case 1:
                    result = new BritishPostcode(area, area == parts[0] ? null : parts[0]);
                    break;

                case 2:
                    var sector = string.Concat(parts[0], ' ', ToSector(parts[1]));
                    result = new BritishPostcode(area, parts[0], sector, value == sector ? null : value);
                    break;

                default:
                    return new BritishPostcode();
            }

            if (string.Equals(value.RemoveAny(' '), result.ToString().RemoveAny(' '), StringComparison.OrdinalIgnoreCase))
            {
                return result;
            }

            return new BritishPostcode();
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            var comparand = obj as BritishPostcode;
            if (ReferenceEquals(null, comparand))
            {
                return false;
            }

            return ToString() == comparand.ToString();
        }

        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }

        public override string ToString()
        {
            return "{0}{1}{2} {3}{4}".FormatWith(Area,
                                                 DistrictNumber.HasValue ? XmlConvert.ToString(DistrictNumber.Value) : string.Empty,
                                                 DistrictLetter.HasValue ? XmlConvert.ToString(DistrictLetter.Value) : string.Empty,
                                                 SectorNumber.HasValue ? XmlConvert.ToString(SectorNumber.Value) : string.Empty,
                                                 UnitCode).Trim();
        }

        public int CompareTo(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return 1;
            }

            var comparand = obj as BritishPostcode;
            if (ReferenceEquals(null, comparand))
            {
                throw new ArgumentOutOfRangeException("obj");
            }

            return Compare(this, comparand);
        }

        public int CompareTo(BritishPostcode other)
        {
            return ReferenceEquals(null, other)
                       ? 1
                       : Compare(this, other);
        }

        public bool Equals(BritishPostcode other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return ToString() == other.ToString();
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
                .Aggregate<char, string>(null,
                                         (x,
                                          c) => x + c);
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
                .Aggregate<char, string>(null,
                                         (x,
                                          c) => x + c);
#endif
        }

        private void SetDistrict(string district)
        {
            if (null == district)
            {
                return;
            }

            District = district;
            OutCode = district;

            var letter = District.Last();
            if (!char.IsDigit(letter))
            {
                DistrictLetter = letter;
            }

            var number = District.Substring(Area.Length);
            if (number.IsEmpty())
            {
                return;
            }

            if (DistrictLetter.HasValue)
            {
                number = number.Substring(0, number.Length - 1);
            }

            DistrictNumber = XmlConvert.ToInt32(number);
        }

        private void SetSector(string sector)
        {
            if (null == District)
            {
                return;
            }

            if (null == sector)
            {
                return;
            }

            Sector = sector;

            var number = Sector.Substring(District.Length).Trim();
            if (number.IsEmpty())
            {
                return;
            }

            SectorNumber = XmlConvert.ToInt32(number);
        }

        private void SetUnit(string unit)
        {
            if (null == Sector)
            {
                return;
            }

            if (null == unit)
            {
                return;
            }

            Unit = unit;
            UnitCode = Unit.Substring(Sector.Length);
            InCode = Unit.RemoveFromStart(OutCode, StringComparison.Ordinal).Trim();
        }
    }
}