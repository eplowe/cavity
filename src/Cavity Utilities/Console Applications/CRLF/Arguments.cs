namespace Cavity
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Threading.Tasks;

    using Cavity.Diagnostics;
    using Cavity.IO;
    using Cavity.Properties;

    public sealed class Arguments
    {
        public Arguments()
        {
            Specs = new List<FileSpec>();
        }

        public IList<FileSpec> Specs { get; private set; }

        public bool Help { get; private set; }

        public bool Quiet { get; private set; }

        public static Arguments Load(string[] args)
        {
            if (null == args || 0 == args.Length)
            {
                return new Arguments
                           {
                               Help = true
                           };
            }

            var result = new Arguments();
            foreach (var arg in args)
            {
                if (arg.EqualsAny(StringComparison.OrdinalIgnoreCase, "/?", "/help", "-help"))
                {
                    result.Help = true;
                }
                else if (arg.EqualsAny(StringComparison.OrdinalIgnoreCase, "/Q", "/quiet", "-quiet"))
                {
                    result.Quiet = true;
                }
                else
                {
                    result.Specs.Add(new FileSpec(arg));
                }
            }

            return result;
        }

        public void Process()
        {
            Trace.WriteLineIf(Tracing.Is.TraceVerbose, string.Empty);

            if (Help)
            {
                Console.WriteLine(Resources.UsageHelp);
                Console.WriteLine(string.Empty);
                return;
            }

            foreach (var spec in Specs)
            {
                Parallel.ForEach(spec, Process);
            }
        }

        private void Process(FileInfo file)
        {
            var changed = file.FixNewLine();
            if (!Quiet)
            {
                Console.WriteLine("[{0}] {1}".FormatWith(changed ? '¤' : ' ', file.FullName));
            }
        }
    }
}