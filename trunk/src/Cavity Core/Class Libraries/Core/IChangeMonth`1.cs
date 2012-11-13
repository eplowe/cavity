namespace Cavity
{
    using System.Diagnostics.CodeAnalysis;

    public interface IChangeMonth<out T>
    {
        T Month(int value);

        [SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "To", Justification = "This naming is intentional.")]
        T To(MonthOfYear month);

        T Year(int value);
    }
}