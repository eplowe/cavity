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
                    SubBuildingName = data.ContainsKey("SBN") ? data["SBN"] : null,
                    BuildingName = data.ContainsKey("BNA") ? data["BNA"] : null,
                    BuildingNumber = data.ContainsKey("NUM") ? data["NUM"] : null,
                    DependentStreet = data.ContainsKey("DST") ? data["DST"] : null,
                    MainStreet = data.ContainsKey("STM") ? data["STM"] : null,
                    DoubleDependentLocality = data.ContainsKey("DDL") ? data["DDL"] : null,
                    DependentLocality = data.ContainsKey("DLO") ? data["DLO"] : null,
                    PostTown = data.ContainsKey("PTN") ? data["PTN"] : null,
                    PostOfficeBox = data.ContainsKey("POB") ? data["POB"] : null,
                    Postcode = data.ContainsKey("PCD") ? data["PCD"] : null
                },
                Organization = 
                {
                    Department = data.ContainsKey("ORD") ? data["ORD"] : null,
                    Name = data.ContainsKey("ORC") ? data["ORC"] : null
                },
                Category = data.ContainsKey("CAT") ? UserCategory.Resolve(data.Value<char>("CAT")) : null,
                DeliveryPointSuffix = data.ContainsKey("DPX") ? data["DPX"] : null,
                MultipleOccupancyCount = data.ContainsKey("MOC") ? data.TryValue<int>("MOC") : new int?(),
                MultipleResidencyRecordCount = data.ContainsKey("MRC") ? data.TryValue<int>("MRC") : new int?(),
                NumberOfDeliveryPoints = data.ContainsKey("NDP") ? data.TryValue<int>("NDP") : new int?(),
                Origin = data.ContainsKey("DTO") ? data.TryValue<char>("DTO") : new char?(),
                SortCode = data.ContainsKey("SCD") ? data.TryValue<int>("SCD") : new int?(),
                UniqueMultipleResidenceReferenceNumber = data.ContainsKey("UMR") ? data.TryValue<int>("UMR") : new int?(),
                UniqueDeliveryPointReferenceNumber = data.ContainsKey("URN") ? data.TryValue<int>("URN") : new int?()
            };

            return result;
        }
    }
}