namespace Cavity.Models
{
    public sealed class LargeUserCategory : IUserCategory
    {
        public char Code
        {
            get
            {
                return 'L';
            }
        }

        public string Name
        {
            get
            {
                return "Large";
            }
        }
    }
}