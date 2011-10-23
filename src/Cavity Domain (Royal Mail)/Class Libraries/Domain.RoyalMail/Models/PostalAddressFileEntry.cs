namespace Cavity.Models
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Text;
    using System.Xml;
    using Cavity.Collections;
    using Cavity.Data;

    /// <summary>
    /// Represents an entry in the Royal Mail Postal Address File.
    /// </summary>
    /// <see href="http://www2.royalmail.com/marketing-services/address-management-unit/address-data-products/postcode-address-file-paf/details">
    /// Royal Mail Postcode Address File - over 28 million UK addresses
    /// </see>
    public class PostalAddressFileEntry : ComparableObject
    {
        public PostalAddressFileEntry()
        {
            Address = new BritishAddress();
            Organization = new Organization();
        }

        public virtual BritishAddress Address { get; private set; }

        public virtual IUserCategory Category { get; set; }

        public virtual string DeliveryPointSuffix { get; set; }

        public virtual int? MultipleOccupancyCount { get; set; }

        public virtual int? MultipleResidencyRecordCount { get; set; }

        public virtual int? NumberOfDeliveryPoints { get; set; }

        public virtual Organization Organization { get; private set; }

        public virtual char? Origin { get; set; }

        public virtual int? SortCode { get; set; }

        public virtual int? UniqueDeliveryPointReferenceNumber { get; set; }

        public virtual int? UniqueMultipleResidenceReferenceNumber { get; set; }

        public static implicit operator PostalAddressFileEntry(KeyStringDictionary data)
        {
            return FromKeyStringDictionary(data);
        }

        public static PostalAddressFileEntry FromKeyStringDictionary(KeyStringDictionary data)
        {
            if (null == data)
            {
                return null;
            }

            var result = new PostalAddressFileEntry
            {
                Address =
                    {
                        SubBuildingName = TryString(data, "SBN"), 
                        PostOfficeBox = TryString(data, "POB"), 
                        BuildingName = TryString(data, "BNA"), 
                        BuildingNumber = TryString(data, "NUM"), 
                        DependentStreet = TryString(data, "DST"), 
                        MainStreet = TryString(data, "STM"), 
                        DoubleDependentLocality = TryString(data, "DDL"), 
                        DependentLocality = TryString(data, "DLO"), 
                        PostTown = TryString(data, "PTN"), 
                        AdministrativeCounty = TryString(data, "CTA"), 
                        FormerPostalCounty = TryString(data, "CTP"), 
                        TraditionalCounty = TryString(data, "CTT"), 
                        Postcode = TryString(data, "PCD")
                    }, 
                Organization =
                    {
                        Department = TryString(data, "ORD"), 
                        Name = TryString(data, "ORC")
                    }, 
                Category = data.ContainsKey("CAT") && !string.IsNullOrEmpty(data["CAT"]) ? UserCategory.Resolve(data["CAT"][0]) : null, 
                DeliveryPointSuffix = TryString(data, "DPX"), 
                MultipleOccupancyCount = TryInt32(data, "MOC"), 
                MultipleResidencyRecordCount = TryInt32(data, "MRC"), 
                NumberOfDeliveryPoints = TryInt32(data, "NDP"), 
                Origin = data.ContainsKey("DTO") && !string.IsNullOrEmpty(data["DTO"]) ? data["DTO"][0] : new char?(), 
                SortCode = TryInt32(data, "SCD"), 
                UniqueMultipleResidenceReferenceNumber = TryInt32(data, "UMR"), 
                UniqueDeliveryPointReferenceNumber = TryInt32(data, "URN"), 
            };

            return result;
        }

        public override string ToString()
        {
            return ToString("ORD,ORC,SBN,BNA,POB,NUM,DST,STM,DDL,DLO,PTN,PCD,CTA,CTP,CTT,SCD,CAT,NDP,DPX,URN,MOC,MRC,UMR,DTO");
        }

        public string ToString(string columns)
        {
            if (null == columns)
            {
                throw new ArgumentNullException("columns");
            }

            if (0 == columns.Length)
            {
                throw new ArgumentOutOfRangeException("columns");
            }

            return ToString(columns.Split(',', StringSplitOptions.RemoveEmptyEntries));
        }

        [SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity", Justification = "TODO: Consider how to refactor away the switch statement.")]
        public string ToString(IEnumerable<string> columns)
        {
            if (null == columns)
            {
                throw new ArgumentNullException("columns");
            }

            var buffer = new StringBuilder();

            var first = true;
            foreach (var column in columns)
            {
                if (first)
                {
                    first = false;
                }
                else
                {
                    buffer.Append(',');
                }

                string value;
                switch (column)
                {
                    case "ORD":
                        value = Organization.Department;
                        break;
                    case "ORC":
                        value = Organization.Name;
                        break;
                    case "SBN":
                        value = Address.SubBuildingName;
                        break;
                    case "BNA":
                        value = Address.BuildingName;
                        break;
                    case "POB":
                        value = Address.PostOfficeBox;
                        break;
                    case "NUM":
                        value = Address.BuildingNumber;
                        break;
                    case "DST":
                        value = Address.DependentStreet;
                        break;
                    case "STM":
                        value = Address.MainStreet;
                        break;
                    case "DDL":
                        value = Address.DoubleDependentLocality;
                        break;
                    case "DLO":
                        value = Address.DependentLocality;
                        break;
                    case "PTN":
                        value = Address.PostTown;
                        break;
                    case "PCD":
                        value = Address.Postcode;
                        break;
                    case "CTA":
                        value = Address.AdministrativeCounty;
                        break;
                    case "CTP":
                        value = Address.FormerPostalCounty;
                        break;
                    case "CTT":
                        value = Address.TraditionalCounty;
                        break;
                    case "SCD":
                        value = NullableInt32String(SortCode);
                        break;
                    case "CAT":
                        value = null == Category ? string.Empty : Category.Code.ToString();
                        break;
                    case "NDP":
                        value = NullableInt32String(NumberOfDeliveryPoints);
                        break;
                    case "DPX":
                        value = DeliveryPointSuffix;
                        break;
                    case "URN":
                        value = NullableInt32String(UniqueDeliveryPointReferenceNumber);
                        break;
                    case "MOC":
                        value = NullableInt32String(MultipleOccupancyCount);
                        break;
                    case "MRC":
                        value = NullableInt32String(MultipleResidencyRecordCount);
                        break;
                    case "UMR":
                        value = NullableInt32String(UniqueMultipleResidenceReferenceNumber);
                        break;
                    case "DTO":
                        value = Origin.HasValue ? Origin.Value.ToString() : string.Empty;
                        break;
                    default:
                        throw new KeyNotFoundException(column);
                }

                buffer.Append(value.FormatCommaSeparatedValue());
            }

            return buffer.ToString();
        }

        private static string NullableInt32String(int? value)
        {
            if (!value.HasValue)
            {
                return string.Empty;
            }

            return XmlConvert.ToString(value.Value);
        }

        private static int? TryInt32(KeyStringDictionary data, 
                                     string key)
        {
            if (!data.ContainsKey(key))
            {
                return new int?();
            }

            return string.IsNullOrEmpty(data[key])
                       ? new int?()
                       : data.TryValue<int>(key);
        }

        private static string TryString(IDictionary<string, string> data, 
                                        string key)
        {
            return data.ContainsKey(key)
                       ? data[key]
                       : null;
        }
    }
}