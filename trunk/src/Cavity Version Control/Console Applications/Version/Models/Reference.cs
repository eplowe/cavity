namespace Cavity.Models
{
    public sealed class Reference
    {
        public Reference()
        {
        }

        public Reference(string include,
                         string hint)
        {
            Include = include;
            Hint = hint;
        }

        public string Hint { get; set; }

        public string Include { get; set; }
    }
}