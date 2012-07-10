namespace Cavity.Data
{
    internal sealed class JsonWriterState
    {
        internal JsonWriterState(JsonNodeType parent)
        {
            Parent = parent;
        }

        internal JsonNodeType Parent { get; set; }

        internal JsonNodeType Previous { get; set; }
    }
}