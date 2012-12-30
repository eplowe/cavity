namespace Cavity.Models
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Xml;

    using Cavity.Diagnostics;

    public sealed class BritishPostcode : IComparable,
                                          IComparable<BritishPostcode>,
                                          IEquatable<BritishPostcode>
    {
        private static readonly IList<string> _areas = "AB,AL,B,BA,BB,BD,BH,BL,BN,BR,BS,BT,CA,CB,CF,CH,CM,CO,CR,CT,CV,CW,DA,DD,DE,DG,DH,DL,DN,DT,DY,E,EC,EH,EN,EX,FK,FY,G,GIR,GL,GU,GY,HA,HD,HG,HP,HR,HS,HU,HX,IG,IM,IP,IV,JE,KA,KT,KW,KY,L,LA,LD,LE,LL,LN,LS,LU,M,ME,MK,ML,N,NE,NG,NN,NP,NR,NW,OL,OX,PA,PE,PH,PL,PO,PR,RG,RH,RM,S,SA,SE,SG,SK,SL,SM,SN,SO,SP,SR,SS,ST,SW,SY,TA,TD,TF,TN,TQ,TR,TS,TW,UB,W,WA,WC,WD,WF,WN,WR,WS,WV,YO,ZE".Split(',').ToList();

        private BritishPostcode()
        {
        }

        private BritishPostcode(Parts parts)
        {
            Values = parts;
            if (!parts.DistrictNumber.HasValue)
            {
                return;
            }

            parts.DistrictNumber = -1 == parts.DistrictNumber.Value ? null : parts.DistrictNumber;
            District = Area.Append(parts.DistrictNumber.HasValue ? XmlConvert.ToString(parts.DistrictNumber.Value) : string.Empty)
                           .Append(parts.DistrictLetter.HasValue ? XmlConvert.ToString(parts.DistrictLetter.Value) : string.Empty);
            OutCode = District;
            if (!parts.Sector.HasValue)
            {
                return;
            }

            Sector = "{0} {1}".FormatWith(District, parts.Sector.Value);
            if (null == parts.Unit)
            {
                return;
            }

            Unit = Sector.Append(parts.Unit);
            InCode = "{0}{1}".FormatWith(parts.Sector.Value, parts.Unit);
        }

        public string Area
        {
            get
            {
                return Values.Area;
            }
        }

        public string District { get; private set; }

        public string InCode { get; private set; }

        public string OutCode { get; private set; }

        public string Sector { get; private set; }

        public string Unit { get; private set; }

        private Parts Values { get; set; }

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
            return ReferenceEquals(null, value)
                       ? null
                       : FromString(value);
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

            var comparison = Parts.Comparison(comparand1.Values.Area, comparand2.Values.Area);
            if (comparison.HasValue)
            {
                return comparison.Value;
            }

            comparison = Parts.Comparison(comparand1.Values.DistrictNumber, comparand2.Values.DistrictNumber);
            if (comparison.HasValue)
            {
                return comparison.Value;
            }

            comparison = Parts.Comparison(comparand1.Values.DistrictLetter, comparand2.Values.DistrictLetter);
            if (comparison.HasValue)
            {
                return comparison.Value;
            }

            comparison = Parts.Comparison(comparand1.Values.Sector, comparand2.Values.Sector);
            if (comparison.HasValue)
            {
                return comparison.Value;
            }

            comparison = Parts.Comparison(comparand1.Values.Unit, comparand2.Values.Unit);
            return comparison.HasValue
                       ? comparison.Value
                       : 0;
        }

        public static BritishPostcode FromString(string value)
        {
            Trace.WriteIf(Tracing.Is.TraceVerbose, value);
            if (null == value)
            {
                throw new ArgumentNullException("value");
            }

            value = value.NormalizeWhiteSpace()
                         .Trim()
                         .ToUpperInvariant()
                         .Where(c => ' '.Equals(c) || char.IsLetterOrDigit(c))
                         .Aggregate(string.Empty, (current,
                                                   c) => current + c);
            if (0 == value.Length)
            {
                return new BritishPostcode();
            }

            return PostalUnit(value) ?? new BritishPostcode();
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
            foreach (var value in new[] { Unit, Sector, District, Area }.Where(value => null != value))
            {
                return value;
            }

            return string.Empty;
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

        private static BritishPostcode PostalArea(MutableString value,
                                                  Parts parts)
        {
            if (!_areas.Contains(value))
            {
                return null;
            }

            parts.Area = value;

            return new BritishPostcode(parts);
        }

        private static BritishPostcode PostalDistrict(MutableString value,
                                                      Parts parts,
                                                      bool space)
        {
            if ("GIR" == value)
            {
                parts.Area = "GIR";
                parts.DistrictNumber = -1;
                return new BritishPostcode(parts);
            }

            if (_areas.Contains(value))
            {
                parts.Area = value;
                parts.DistrictNumber = parts.Sector;
                parts.Sector = null;
                return new BritishPostcode(parts);
            }

            if (char.IsLetter(value[value.Length - 1]))
            {
                parts.DistrictLetter = value[value.Length - 1];
                value.Truncate(value.Length - 1);
            }

            var index = -1;
            for (var i = 0; i < value.Length; i++)
            {
                if (!char.IsDigit(value[i]))
                {
                    continue;
                }

                index = i;
                break;
            }

            if (index.In(-1, 0))
            {
                return null;
            }

            var number = value.Substring(index);
            if (2 < number.Length)
            {
                return null;
            }

            if (number.Any(char.IsLetter))
            {
                return null;
            }

            if (!space && !parts.DistrictLetter.HasValue && parts.Sector.HasValue && number.Length.In(0, 1))
            {
                value.Append(XmlConvert.ToString(parts.Sector.Value));
                number += parts.Sector.Value;
                parts.Sector = null;
            }

            parts.DistrictNumber = XmlConvert.ToInt32(number);
            value.Truncate(value.Length - number.Length);

            return PostalArea(value, parts);
        }

        private static BritishPostcode PostalSector(MutableString value,
                                                    Parts parts)
        {
            var sector = value[value.Length - 1];
            if (char.IsDigit(sector))
            {
                parts.Sector = XmlConvert.ToInt32("{0}".FormatWith(sector));
                value.Truncate(value.Length - 1);
            }

            var space = false;
            if (' ' == value[value.Length - 1])
            {
                space = true;
                value.Truncate(value.Length - 1);
            }

            return PostalDistrict(value, parts, space);
        }

        private static BritishPostcode PostalUnit(MutableString value)
        {
            if (8 < value.Length)
            {
                return null;
            }

            if (!char.IsLetter(value[0]))
            {
                return null;
            }

            var parts = new Parts();

            if (_areas.Contains(value))
            {
                parts.Area = value;
                return new BritishPostcode(parts);
            }

            if (1 == value.Length)
            {
                return null;
            }

            var unit = value.Substring(value.Length - 2);
            if (char.IsLetter(unit[0]) && char.IsLetter(unit[1]))
            {
                parts.Unit = unit;
                value.Truncate(value.Length - 2);
            }

            return 0 == value.Length
                       ? null
                       : PostalSector(value, parts);
        }

        private struct Parts
        {
            public string Area { get; set; }

            public char? DistrictLetter { get; set; }

            public int? DistrictNumber { get; set; }

            public int? Sector { get; set; }

            public string Unit { get; set; }

            public static int? Comparison(int? one,
                                          int? two)
            {
                if (one.HasValue || two.HasValue)
                {
                    if (!one.HasValue)
                    {
                        return -1;
                    }

                    if (!two.HasValue)
                    {
                        return 1;
                    }

                    if (one.Value != two.Value)
                    {
                        return one.Value.CompareTo(two.Value) > 0 ? 1 : -1;
                    }
                }

                return null;
            }

            public static int? Comparison(string one,
                                          string two)
            {
                var comparison = string.Compare(one, two, StringComparison.OrdinalIgnoreCase);
                if (0 == comparison)
                {
                    return null;
                }

                return comparison > 0 ? 1 : -1;
            }
        }
    }
}