namespace Cavity.Data
{
    public enum JsonNodeType
    {
        None = 0,

        Object,

        EndObject,

        Array,

        EndArray,

        Name,

        StringValue,

        NumberValue,

        NullValue,

        TrueValue,

        FalseValue
    }
}