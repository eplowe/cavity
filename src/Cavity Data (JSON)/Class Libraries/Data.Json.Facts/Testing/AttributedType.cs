namespace Cavity.Testing
{
    using Cavity.Data;

    public sealed class AttributedType
    {
        [JsonIgnore]
        public string Ignore { get; set; }

        [JsonName("example")]
        public string Value { get; set; }
    }
}