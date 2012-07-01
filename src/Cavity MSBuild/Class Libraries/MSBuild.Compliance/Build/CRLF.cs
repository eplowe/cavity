namespace Cavity.Build
{
    using System.Diagnostics.CodeAnalysis;
    using System.IO;

    using Cavity.IO;

    using Microsoft.Build.Framework;
    using Microsoft.Build.Utilities;

    [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "CRLF", Justification = "This is the intended casing.")]
    public sealed class CRLF : Task
    {
        [Required]
        public ITaskItem[] Files { get; set; }

        public override bool Execute()
        {
            foreach (var item in Files)
            {
                var changed = false;
                try
                {
                    changed = new FileInfo(item.ItemSpec).FixNewLine();
                }
                finally
                {
                    Log.LogMessage(MessageImportance.Normal, "[{0}] {1}".FormatWith(changed ? '¤' : ' ', item.ItemSpec));
                }
            }

            return true;
        }
    }
}