namespace Cavity
{
    public interface IGetPreviousMonth
    {
        Month Month();

        Month Year();

        Month Year(MonthOfYear month);

        Month Year(int month);
    }
}