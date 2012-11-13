namespace Cavity
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    public interface IGetTimeZone<out T>
    {
        T LocalTime { get; }

        T UniversalTime { get; }

#if !NET20
        [SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "For", Justification = "This naming is intentional.")]
        T For(TimeZoneInfo value);
#endif
    }
}