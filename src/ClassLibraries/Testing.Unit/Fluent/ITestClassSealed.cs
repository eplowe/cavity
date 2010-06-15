namespace Cavity.Fluent
{
    public interface ITestClassSealed
    {
        ITestClassConstruction IsSealed();

        ITestClassConstruction IsUnsealed();
    }
}