namespace Cavity.Build.Sdk
{
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;

    /// <summary>
    /// Encapsulates access to the Platform SDK linking compiler.
    /// </summary>
    /// <seealso href="http://msdn.microsoft.com/library/t2fck18t">
    /// Linking
    /// </seealso>
    public sealed class LinkCompiler : CompilerBase
    {
        private LinkCompiler(string location)
            : base(location)
        {
        }

        public static LinkCompiler Current
        {
            get
            {
                return new LinkCompiler("LINK.exe");
            }
        }

        public string Machine { get; set; }

        public FileInfo Out { get; set; }

        public override string ToArguments(IEnumerable<string> files)
        {
            var switches = string.Format(
                CultureInfo.InvariantCulture,
                "-dll -noentry -machine:{1} -out:\"{0}\"",
                Out.FullName,
                Machine ?? "X86");

            return ToArguments(switches, files);
        }
    }
}