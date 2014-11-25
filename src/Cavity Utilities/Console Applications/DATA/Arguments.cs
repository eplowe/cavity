namespace Cavity
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Globalization;
    using System.IO;
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
                    result.Input = arg.Substring(4).Trim();
                }
                else if (arg.StartsWithAny(StringComparison.OrdinalIgnoreCase, "/out:", "-out:"))
                {
                    result.Output = arg.Substring(5).Trim();
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
                Process(file);
            }
        }

        private void Process(FileInfo file)
        {
            var input = string.IsNullOrWhiteSpace(Input)
                            ? file.Extension.Substring(1)
                            : Input;
            var destination = file.ChangeExtension(".{0}".FormatWith(Output));
            switch (input.ToUpperInvariant())
            {
                case "CSV":
                    Process(new CsvDataSheet(file), destination);
                    break;

                case "TSV":
                    Process(new TsvDataSheet(file), destination);
                    break;

                default:
                    throw new FormatException("{0} is not a supported input data format.".FormatWith(input));
            }
        }

        private void Process(IEnumerable<KeyStringDictionary> sheet, FileInfo destination)
        {
            if (destination.Exists)
            {
                throw new InvalidOperationException("{0} already exists.".FormatWith(destination.FullName));
            }
            
            switch (Output.ToUpperInvariant())
            {
                case "CSV":
                    Csv.Save(sheet, destination);
                    break;

                case "TSV":
                    Tsv.Save(sheet, destination);
                    break;

                default:
                    throw new FormatException("{0} is not a supported output data format.".FormatWith(Output));
            }
        }
    }
}