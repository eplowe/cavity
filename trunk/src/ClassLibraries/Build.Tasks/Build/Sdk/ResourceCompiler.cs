namespace Cavity.Build.Sdk
{
    using System.Collections.Generic;
    using System.IO;

    /// <summary>
    /// Encapsulates access to the Platform SDK resource compiler.
    /// </summary>
    /// <seealso href="http://msdn.microsoft.com/library/aa381042">
    /// Resource Compiler
    /// </seealso>
    public sealed class ResourceCompiler : CompilerBase
    {
        private ResourceCompiler(FileInfo location)
            : base(location)
        {
        }

        public static ResourceCompiler Current
        {
            get
            {
                var exe = ToApplicationPath("RC.exe");
                return exe.Exists
                           ? new ResourceCompiler(exe)
                           : null;
            }
        }

        public override string ToArguments(IEnumerable<string> files)
        {
            return ToArguments("-r", files);
        }
    }
}