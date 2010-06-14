namespace Cavity.Fluent
{
    public interface ITestObjectSealed
    {
        ITestObjectConstruction IsSealed();

        ITestObjectConstruction IsUnsealed();
    }
}