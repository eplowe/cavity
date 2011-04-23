namespace Cavity.Models
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    public interface IManageTasks
    {
        [SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Continue", Justification = "Derived from the service naming convention.")]
        void Continue();

        void Pause();

        void Shutdown();

        void Start(IEnumerable<string> args);

        [SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Stop", Justification = "Derived from the service naming convention.")]
        void Stop();
    }
}