namespace Cavity
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;

    using Cavity.Diagnostics;
    using Cavity.IO;
    using Cavity.Properties;

    public sealed class Arguments
    {
        public Arguments()
        {
            Files = new List<FileInfo>();
        }

        public IList<FileInfo> Files { get; private set; }

        public bool Help { get; private set; }

        public bool Quiet { get; private set; }

        public static Arguments Load(string[] args)
        {
            if (null == args)
            {
                return new Arguments
                           {
                               Help = true
                           };
            }

            if (0 == args.Length)
            {
                return new Arguments
                {
                    Help = true
                };
            }

            var result = new Arguments();
            if (args.Any(arg => arg.EqualsAny(StringComparison.OrdinalIgnoreCase, "/?", "/help", "-help")))
            {
                return new Arguments
                           {
                               Help = true
                           };
            }

            foreach (var arg in args)
            {
                if (string.Equals("/Q", arg, StringComparison.OrdinalIgnoreCase))
                {
                    result.Quiet = true;
                    continue;
                }

                var name = Name.Load(arg);
                foreach (var file in name.Files)
                {
                    result.Files.Add(file);
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

            if (0 == Files.Count)
            {
                if (!Quiet)
                {
                    Console.WriteLine(Resources.NoFiles);
                }

                return;
            }

            foreach (var file in Files)
            {
                var changed = file.FixNewLine();
                if (!Quiet)
                {
                    Console.WriteLine("[{0}] {1}".FormatWith(changed ? 'Δ' : ' ', file.FullName));
                }
            }
        }
    }
}