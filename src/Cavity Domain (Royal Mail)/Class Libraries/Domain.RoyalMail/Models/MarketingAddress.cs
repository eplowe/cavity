namespace Cavity.Models
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Runtime.Serialization;
    using Cavity.Collections;

    [SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix", Justification = "This naming is intentional.")]
    [Serializable]
    public class MarketingAddress : KeyStringDictionary
    {
        private static readonly IList<string> _keys = "ADDRESS 1,ADDRESS 2,ADDRESS 3,ADDRESS 4,ADDRESS 5,ADDRESS 6,POSTCODE".Split(',');

        public MarketingAddress()
        {
        }

        protected MarketingAddress(SerializationInfo info,
                                   StreamingContext context)
            : base(info, context)
        {
        }

        public virtual string Address1
        {
            get
            {
                return ContainsKey("ADDRESS 1") ? this["ADDRESS 1"] : null;
            }

            set
            {
                this["ADDRESS 1"] = value;
            }
        }

        public virtual string Address2
        {
            get
            {
                return ContainsKey("ADDRESS 2") ? this["ADDRESS 2"] : null;
            }

            set
            {
                this["ADDRESS 2"] = value;
            }
        }

        public virtual string Address3
        {
            get
            {
                return ContainsKey("ADDRESS 3") ? this["ADDRESS 3"] : null;
            }

            set
            {
                this["ADDRESS 3"] = value;
            }
        }

        public virtual string Address4
        {
            get
            {
                return ContainsKey("ADDRESS 4") ? this["ADDRESS 4"] : null;
            }

            set
            {
                this["ADDRESS 4"] = value;
            }
        }

        public virtual string Address5
        {
            get
            {
                return ContainsKey("ADDRESS 5") ? this["ADDRESS 5"] : null;
            }

            set
            {
                this["ADDRESS 5"] = value;
            }
        }

        public virtual string Address6
        {
            get
            {
                return ContainsKey("ADDRESS 6") ? this["ADDRESS 6"] : null;
            }

            set
            {
                this["ADDRESS 6"] = value;
            }
        }

        public virtual string FullAddress
        {
            get
            {
                var value = new MutableString();

                foreach (var key in _keys.Where(ContainsKey))
                {
                    if (Empty(key))
                    {
                        continue;
                    }

                    if (value.ContainsText())
                    {
                        value.Append(", ");
                    }

                    value.Append(this[key]);
                }

                return value;
            }
        }

        public virtual BritishPostcode Postcode
        {
            get
            {
                return ContainsKey("POSTCODE") ? this["POSTCODE"] : null;
            }

            set
            {
                this["POSTCODE"] = value;
            }
        }
    }
}