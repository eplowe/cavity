namespace Cavity.Models
{
    public sealed class ResidentialUserCategory : IUserCategory
    {
        public char Code
        {
            get
            {
                return 'R';
            }
        }

        public string Name
        {
            get
            {
                return "Residential";
            }
        }
    }
}