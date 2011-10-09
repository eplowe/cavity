namespace Cavity.Models
{
    public abstract class DeliveryPoint : AddressLine
    {
        public AddressNumber Number { get; set; }
    }
}