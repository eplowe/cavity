namespace Cavity.Data
{
    public enum JsonNodeType
    {
        None = 0, 

        Array = 1, 

        Object = 2, 

        Name = 3, 

        NullValue = 4, 

        TrueValue = 5, 

        FalseValue = 6, 

        NumberValue = 7, 

        StringValue = 8, 

        EndObject = 9, 

        EndArray = 10
    }
}