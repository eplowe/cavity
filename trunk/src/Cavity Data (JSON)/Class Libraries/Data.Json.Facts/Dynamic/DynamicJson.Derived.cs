namespace Cavity.Dynamic
{
    public sealed class DerivedDynamicJson : DynamicJson
    {
        public DerivedDynamicJson()
        {
            Data["Foo"] = "bar";
        }
    }
}