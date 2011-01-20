namespace Cavity.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public sealed class BritishPostcode : ComparableObject
    {
        private static readonly IDictionary<string, string> _areaNames = new Dictionary<string, string>
        {
            {
                "AB", "Aberdeen"
                },
            {
                "AL", "St Albans"
                },
            {
                "B", "Birmingham"
                },
            {
                "BA", "Bath"
                },
            {
                "BB", "Blackburn"
                },
            {
                "BD", "Bradford"
                },
            {
                "BH", "Bournemouth"
                },
            {
                "BL", "Bolton"
                },
            {
                "BN", "Brighton"
                },
            {
                "BR", "Bromley"
                },
            {
                "BS", "Bristol"
                },
            {
                "BT", "Belfast"
                },
            {
                "CA", "Carlisle"
                },
            {
                "CB", "Cambridge"
                },
            {
                "CF", "Cardiff"
                },
            {
                "CH", "Chester"
                },
            {
                "CM", "Chelmsford"
                },
            {
                "CO", "Colchester"
                },
            {
                "CR", "Croydon"
                },
            {
                "CT", "Canterbury"
                },
            {
                "CV", "Coventry"
                },
            {
                "CW", "Crewe"
                },
            {
                "DA", "Dartford"
                },
            {
                "DD", "Dundee"
                },
            {
                "DE", "Derby"
                },
            {
                "DG", "Dumfries"
                },
            {
                "DH", "Durham"
                },
            {
                "DL", "Darlington"
                },
            {
                "DN", "Doncaster"
                },
            {
                "DT", "Dorchester"
                },
            {
                "DY", "Dudley"
                },
            {
                "E", "London"
                },
            {
                "EC", "London"
                },
            {
                "EH", "Edinburgh"
                },
            {
                "EN", "Enfield"
                },
            {
                "EX", "Exeter"
                },
            {
                "FK", "Falkirk"
                },
            {
                "FY", "Blackpool"
                },
            {
                "G", "Glasgow"
                },
            {
                "GL", "Gloucester"
                },
            {
                "GU", "Guildford"
                },
            {
                "HA", "Harrow"
                },
            {
                "HD", "Huddersfield"
                },
            {
                "HG", "Harrogate"
                },
            {
                "HP", "Hemel Hempstead"
                },
            {
                "HR", "Hereford"
                },
            {
                "HS", "Outer Hebrides"
                },
            {
                "HU", "Hull"
                },
            {
                "HX", "Halifax"
                },
            {
                "IG", "Ilford"
                },
            {
                "IP", "Ipswich"
                },
            {
                "IV", "Inverness"
                },
            {
                "KA", "Kilmarnock"
                },
            {
                "KT", "Kingston upon Thames"
                },
            {
                "KW", "Kirkwall"
                },
            {
                "KY", "Kirkcaldy"
                },
            {
                "L", "Liverpool"
                },
            {
                "LA", "Lancaster"
                },
            {
                "LD", "Llandrindod Wells"
                },
            {
                "LE", "Leicester"
                },
            {
                "LL", "Llandudno"
                },
            {
                "LN", "Lincoln"
                },
            {
                "LS", "Leeds"
                },
            {
                "LU", "Luton"
                },
            {
                "M", "Manchester"
                },
            {
                "ME", "Rochester"
                },
            {
                "MK", "Milton Keynes"
                },
            {
                "ML", "Motherwell"
                },
            {
                "N", "London"
                },
            {
                "NE", "Newcastle upon Tyne"
                },
            {
                "NG", "Nottingham"
                },
            {
                "NN", "Northampton"
                },
            {
                "NP", "Newport"
                },
            {
                "NR", "Norwich"
                },
            {
                "NW", "London"
                },
            {
                "OL", "Oldham"
                },
            {
                "OX", "Oxford"
                },
            {
                "PA", "Paisley"
                },
            {
                "PE", "Peterborough"
                },
            {
                "PH", "Perth"
                },
            {
                "PL", "Plymouth"
                },
            {
                "PO", "Portsmouth"
                },
            {
                "PR", "Preston"
                },
            {
                "RG", "Reading"
                },
            {
                "RH", "Redhill"
                },
            {
                "RM", "Romford"
                },
            {
                "S", "Sheffield"
                },
            {
                "SA", "Swansea"
                },
            {
                "SE", "London"
                },
            {
                "SG", "Stevenage"
                },
            {
                "SK", "Stockport"
                },
            {
                "SL", "Slough"
                },
            {
                "SM", "Sutton"
                },
            {
                "SN", "Swindon"
                },
            {
                "SO", "Southampton"
                },
            {
                "SP", "Salisbury"
                },
            {
                "SR", "Sunderland"
                },
            {
                "SS", "Southend-on-Sea"
                },
            {
                "ST", "Stoke-on-Trent"
                },
            {
                "SW", "London"
                },
            {
                "SY", "Shrewsbury"
                },
            {
                "TA", "Taunton"
                },
            {
                "TD", "Galashiels"
                },
            {
                "TF", "Telford"
                },
            {
                "TN", "Tonbridge"
                },
            {
                "TQ", "Torquay"
                },
            {
                "TR", "Truro"
                },
            {
                "TS", "Cleveland"
                },
            {
                "TW", "Twickenham"
                },
            {
                "UB", "Southall"
                },
            {
                "W", "London"
                },
            {
                "WA", "Warrington"
                },
            {
                "WC", "London"
                },
            {
                "WD", "Watford"
                },
            {
                "WF", "Wakefield"
                },
            {
                "WN", "Wigan"
                },
            {
                "WR", "Worcester"
                },
            {
                "WS", "Walsall"
                },
            {
                "WV", "Wolverhampton"
                },
            {
                "YO", "York"
                },
            {
                "ZE", "Lerwick"
                },
            {
                "GY", "Guernsey"
                },
            {
                "JE", "Jersey"
                },
            {
                "IM", "Isle of Man"
                }
        };

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

        public string AreaName
        {
            get
            {
                return null != Area && _areaNames.ContainsKey(Area) ? _areaNames[Area] : null;
            }
        }

        public string District { get; set; }

        public string Sector { get; set; }

        public string Unit { get; set; }

        public static implicit operator BritishPostcode(string value)
        {
            return ReferenceEquals(null, value) ? null : FromString(value);
        }

        public static BritishPostcode FromString(string value)
        {
            if (null == value)
            {
                throw new ArgumentNullException("value");
            }

            if (0 == value.Length)
            {
                return new BritishPostcode();
            }

            value = value.Trim().ToUpperInvariant();
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
            return value
                .TakeWhile(c => c >= 'A' && c <= 'Z')
                .Aggregate<char, string>(null,
                                         (current,
                                          c) => current + c);
        }

        private static string ToSector(IEnumerable<char> value)
        {
            return value
                .Where(c => c >= '0' && c <= '9')
                .Aggregate<char, string>(null,
                                         (current,
                                          c) => current + c);
        }
    }
}