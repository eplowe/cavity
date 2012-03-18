namespace Cavity.Models
{
    using System.Linq;
    using System.Text;

    public class BritishAddress : ComparableObject
    {
        public virtual string AdministrativeCounty { get; set; }

        public virtual string BuildingName { get; set; }

        public virtual string BuildingNumber { get; set; }

        public virtual string DependentLocality { get; set; }

        public virtual string DependentStreet { get; set; }

        public virtual string DoubleDependentLocality { get; set; }

        public virtual string FormerPostalCounty { get; set; }

        public virtual string MainStreet { get; set; }

        public virtual string PostOfficeBox { get; set; }

        public virtual string PostTown { get; set; }

        public virtual BritishPostcode Postcode { get; set; }

        public virtual string SubBuildingName { get; set; }

        public virtual string TraditionalCounty { get; set; }

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