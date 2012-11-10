namespace Cavity
{
    public interface IGetNextMonth
    {
        Month Month();

        Month Year();

        Month Year(MonthOfYear month);

        Month Year(int month);
    }
}