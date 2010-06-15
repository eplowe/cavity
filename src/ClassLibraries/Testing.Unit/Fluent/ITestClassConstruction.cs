namespace Cavity.Fluent
{
    public interface ITestClassConstruction
    {
        ITestType HasDefaultConstructor();

        ITestType NoDefaultConstructor();
    }
}