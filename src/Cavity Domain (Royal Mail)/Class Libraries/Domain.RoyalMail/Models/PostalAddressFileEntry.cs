namespace Cavity.Models
{
    using Cavity.Collections;

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

        public virtual DataOrigin Origin { get; set; }

        public virtual string DeliveryPointSuffix { get; set; }

        public virtual int? MultipleOccupancyCount { get; set; }

        public virtual int? MultipleResidencyRecordCount { get; set; }

        public virtual int? NumberOfDeliveryPoints { get; set; }

        public virtual Organization Organization { get; private set; }

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
                    SubBuildingName = data["SBN"],
                    BuildingName = data["BNA"],
                    BuildingNumber = data["NUM"],
                    DependentStreet = data["DST"],
                    MainStreet = data["STM"],
                    DoubleDependentLocality = data["DDL"],
                    DependentLocality = data["DLO"],
                    PostTown = data["PTN"],
                    PostOfficeBox = data["POB"],
                    Postcode = data["PCD"]
                },
                Organization = 
                {
                    Department = data["ORD"],
                    Name = data["ORC"]
                },
                Category = UserCategory.Resolve(data.Value<char>("CAT")),
                SortCode = data.Value<int>("SCD")
            };

            return result;
        }
    }
}