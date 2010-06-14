namespace Cavity.Fluent
{
    public interface ITestObjectConstruction
    {
        ITestObject HasDefaultConstructor();

        ITestObject NoDefaultConstructor();
    }
}