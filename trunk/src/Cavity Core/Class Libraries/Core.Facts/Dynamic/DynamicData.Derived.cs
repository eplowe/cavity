namespace Cavity.Dynamic
{
    public sealed class DerivedDynamicData : DynamicData
    {
        public DerivedDynamicData()
        {
            Data["Foo"] = "bar";
        }
    }
}