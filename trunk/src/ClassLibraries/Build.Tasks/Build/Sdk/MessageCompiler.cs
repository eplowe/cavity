namespace Cavity.Build.Sdk
{
    using System.Collections.Generic;
    using System.IO;

    /// <summary>
    /// Encapsulates access to the Platform SDK message compiler.
    /// </summary>
    /// <seealso href="http://msdn.microsoft.com/library/aa385638">
    /// Message Compiler (MC.exe)
    /// </seealso>
    public sealed class MessageCompiler : CompilerBase
    {
        private MessageCompiler(FileInfo location)
            : base(location)
        {
        }

        public static MessageCompiler Current
        {
            get
            {
                var exe = ToApplicationPath("MC.exe");
                return exe.Exists
                           ? new MessageCompiler(exe)
                           : null;
            }
        }

        public override string ToArguments(IEnumerable<string> files)
        {
            return ToArguments("-u -U", files);
        }
    }
}