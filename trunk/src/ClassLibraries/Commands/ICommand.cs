namespace Cavity
{
    public interface ICommand
    {
        bool Act();

        bool Revert();
    }
}