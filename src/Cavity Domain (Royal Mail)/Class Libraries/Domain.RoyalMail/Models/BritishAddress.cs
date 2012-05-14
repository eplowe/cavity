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