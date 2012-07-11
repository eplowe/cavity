namespace Cavity.Data
{
    internal sealed class JsonReaderState
    {
        internal JsonReaderState(JsonNodeType current)
        {
            Current = current;
        }

        internal JsonNodeType Current { get; set; }
    }
}