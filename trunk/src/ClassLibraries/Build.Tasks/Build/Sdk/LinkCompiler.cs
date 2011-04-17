namespace Cavity.Build.Sdk
{
    using System.Collections.Generic;
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
            return ToArguments("-dll -noentry -machine:{1} -out:\"{0}\"".FormatWith(Out.FullName, Machine ?? "X86"), files);
        }
    }
}