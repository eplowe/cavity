namespace Cavity.Data
{
    public interface IVerifyRepository<T>
    {
        void Verify(IRepository<T> repository);
    }
}