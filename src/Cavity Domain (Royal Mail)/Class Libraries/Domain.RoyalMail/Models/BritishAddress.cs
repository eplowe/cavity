namespace Cavity.Models
{
    public class BritishAddress : ComparableObject
    {
        public virtual string BuildingName { get; set; }

        public virtual string BuildingNumber { get; set; }

        public virtual string DependentLocality { get; set; }

        public virtual string DependentStreet { get; set; }

        public virtual string DoubleDependentLocality { get; set; }

        public virtual string MainStreet { get; set; }

        public virtual BritishPostcode Postcode { get; set; }

        public virtual string PostOfficeBox { get; set; }

        public virtual string PostTown { get; set; }

        public virtual string SubBuildingName { get; set; }
    }
}