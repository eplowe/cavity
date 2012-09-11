namespace Cavity
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading.Tasks;

    using Cavity.Collections;
    using Cavity.Data;
    using Cavity.Diagnostics;
    using Cavity.IO;
    using Cavity.Properties;

    public sealed class Arguments
    {
        public Arguments()
        {
            Specs = new List<FileSpec>();
            Output = "CSV";
        }

        public IList<FileSpec> Specs { get; private set; }

        public bool Help { get; private set; }

        public string Input { get; private set; }

        public string Output { get; private set; }

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
                else if (arg.StartsWithAny(StringComparison.OrdinalIgnoreCase, "/in:", "-in:"))
                {
                    result.Input = arg.Substring(4);
                }
                else if (arg.StartsWithAny(StringComparison.OrdinalIgnoreCase, "/out:", "-out:"))
                {
                    result.Output = arg.Substring(5);
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

            Console.WriteLine("/in:{0}".FormatWith(Input));
            Console.WriteLine("/out:{0}".FormatWith(Output));

            foreach (var file in Specs.SelectMany(spec => spec))
            {
                DataFile.From(file, Input);
            }
        }
    }
}