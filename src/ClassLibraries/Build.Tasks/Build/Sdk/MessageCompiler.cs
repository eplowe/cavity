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
                return new MessageCompiler(ToApplicationPath("MC.exe"));
            }
        }

        public override string ToArguments(IEnumerable<string> files)
        {
            return ToArguments("-u -U", files);
        }
    }
}