namespace Cavity.Fluent
{
    public interface ITestObjectStyle
    {
        ITestObject IsAbstractBaseClass();

        ITestObjectSealed IsConcreteClass();
    }
}