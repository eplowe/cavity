namespace Cavity.Data
{
    public interface IJsonSerializable
    {
        void ReadJson(JsonReader reader);
    }
}