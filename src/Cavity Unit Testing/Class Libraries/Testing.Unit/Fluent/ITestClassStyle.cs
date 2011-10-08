namespace Cavity.Fluent
{
    public interface ITestClassStyle
    {
        ITestType IsAbstractBaseClass();

        ITestClassSealed IsConcreteClass();
    }
}