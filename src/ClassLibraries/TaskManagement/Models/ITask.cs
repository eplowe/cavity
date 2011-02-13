namespace Cavity.Models
{
    using Cavity.Data;

    public interface ITask
    {
        DataCollection Execute(DataCollection configuration);
    }
}