namespace Cavity.Data
{
    public interface IRecord<T> : IRecord
    {
        T Value { get; set; }
    }
}