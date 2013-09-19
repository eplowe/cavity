namespace Cavity.Models
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Runtime.Serialization;
    using System.Text;

    using Cavity.Collections;

    [SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix", Justification = "This naming is intentional.")]
    [Serializable]
    public class BritishAddress : KeyStringDictionary
    {
        public BritishAddress()
        {
        }

        protected BritishAddress(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        public virtual string AdministrativeCounty
        {
            get
            {
                return ContainsKey("CTA") ? this["CTA"] : null;
            }

            set
            {
                this["CTA"] = value;
            }
        }

        public virtual string BuildingName
        {
            get
            {
                return ContainsKey("BNA") ? this["BNA"] : null;
            }

            set
            {
                this["BNA"] = value;
            }
        }

        public virtual string BuildingNumber
        {
            get
            {
                return ContainsKey("NUM") ? this["NUM"] : null;
            }

            set
            {
                this["NUM"] = value;
            }
        }

        public virtual string DependentLocality
        {
            get
            {
                return ContainsKey("DLO") ? this["DLO"] : null;
            }

            set
            {
                this["DLO"] = value;
            }
        }

        public virtual string DependentStreet
        {
            get
            {
                return ContainsKey("DST") ? this["DST"] : null;
            }

            set
            {
                this["DST"] = value;
            }
        }

        public virtual string DoubleDependentLocality
        {
            get
            {
                return ContainsKey("DDL") ? this["DDL"] : null;
            }

            set
            {
                this["DDL"] = value;
            }
        }

        public virtual string FormerPostalCounty
        {
            get
            {
                return ContainsKey("CTP") ? this["CTP"] : null;
            }

            set
            {
                this["CTP"] = value;
            }
        }

        public virtual string MainStreet
        {
            get
            {
                return ContainsKey("STM") ? this["STM"] : null;
            }

            set
            {
                this["STM"] = value;
            }
        }

        public virtual string PostOfficeBox
        {
            get
            {
                return ContainsKey("POB") ? this["POB"] : null;
            }

            set
            {
                this["POB"] = value;
            }
        }

        public virtual string PostTown
        {
            get
            {
                return ContainsKey("PTN") ? this["PTN"] : null;
            }

            set
            {
                this["PTN"] = value;
            }
        }

        public virtual BritishPostcode Postcode
        {
            get
            {
                return ContainsKey("PCD") ? this["PCD"] : null;
            }

            set
            {
                this["PCD"] = value;
            }
        }

        public virtual string SubBuildingName
        {
            get
            {
                return ContainsKey("SBN") ? this["SBN"] : null;
            }

            set
            {
                this["SBN"] = value;
            }
        }

        public virtual string TraditionalCounty
        {
            get
            {
                return ContainsKey("CTT") ? this["CTT"] : null;
            }

            set
            {
                this["CTT"] = value;
            }
        }

        public static MarketingAddress ToMarketingFormat(KeyStringDictionary entry)
        {
            if (null == entry)
            {
                throw new ArgumentNullException("entry");
            }

            var clone = entry.Clone<KeyStringDictionary>();
            var result = new MarketingAddress
                {
                    { "ADDRESS 1", string.Empty },
                    { "ADDRESS 2", string.Empty },
                    { "ADDRESS 3", string.Empty },
                    { "ADDRESS 4", string.Empty },
                    { "ADDRESS 5", string.Empty },
                    { "ADDRESS 6", string.Empty },
                    { "POSTCODE", clone["PCD"] },
                };

            if (clone["SBN"].ContainsText())
            {
                result["ADDRESS 1"] = clone["SBN"];
            }

            if (clone["BNA"].ContainsText())
            {
                foreach (var column in "ADDRESS 1,ADDRESS 2".Split(',')
                                                            .Where(column => result[column].IsEmpty()))
                {
                    result[column] = clone["BNA"];
                    break;
                }
            }

            if (clone["NUM"].ContainsText())
            {
                foreach (var column in "ADDRESS 1,ADDRESS 2,ADDRESS 3".Split(',')
                                                                      .Where(column => result[column].IsEmpty()))
                {
                    var value = clone["NUM"];
                    if (clone["DST"].ContainsText())
                    {
                        value = "{0} {1}".FormatWith(value, clone["DST"]);
                        clone["DST"] = string.Empty;
                    }
                    else if (clone["STM"].ContainsText())
                    {
                        value = "{0} {1}".FormatWith(value, clone["STM"]);
                        clone["STM"] = string.Empty;
                    }

                    result[column] = value;
                    break;
                }
            }

            foreach (var source in "DST,STM,DDL,DLO,PTN".Split(',')
                                                        .Where(source => clone[source].ContainsText()))
            {
                const string keys = "ADDRESS 1,ADDRESS 2,ADDRESS 3,ADDRESS 4,ADDRESS 5,ADDRESS 6";
                foreach (var destination in keys.Split(',')
                                                .Where(destination => result[destination].IsNullOrEmpty()))
                {
                    result[destination] = clone[source];
                    break;
                }
            }

            return result;
        }

        public override string ToString()
        {
            var buffer = new StringBuilder();

            foreach (var value in new[]
                                      {
                                          SubBuildingName, BuildingName, PostOfficeBox
                                      }.Where(value => !string.IsNullOrEmpty(value)))
            {
                buffer.AppendLine(value);
            }

            if (!string.IsNullOrEmpty(BuildingNumber))
            {
                buffer.Append(BuildingNumber);
                buffer.Append(' ');
            }

            foreach (var value in new[]
                                      {
                                          DependentStreet, MainStreet, DoubleDependentLocality, DependentLocality, PostTown, (string)Postcode, TraditionalCounty
                                      }.Where(value => !string.IsNullOrEmpty(value)))
            {
                buffer.AppendLine(value);
            }

            return buffer.ToString();
        }
    }
}