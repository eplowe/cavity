namespace Cavity
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Cavity.Data;
    using Cavity.IO;
    using Cavity.Properties;

    public sealed class Arguments
    {
        public Arguments()
        {
            Specs = new List<FileSpec>();
        }

        public bool Help { get; private set; }

        public IList<FileSpec> Specs { get; private set; }

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
                switch (arg.ToUpperInvariant())
                {
                    case "/?":
                    case "/HELP":
                    case "-HELP":
                        result.Help = true;
                        break;

                    default:
                        result.Specs.Add(new FileSpec(arg));
                        break;
                }
            }

            return result;
        }

        public void Process()
        {
            if (Help)
            {
                Console.WriteLine(Resources.UsageHelp);
                Console.WriteLine(string.Empty);
                return;
            }

            foreach (var file in Specs.SelectMany(spec => spec))
            {
                Process(new TsvDataSheet(file));
            }

            Console.WriteLine(new string('=', 30));
        }

        private static void Process(TsvDataSheet sheet)
        {
            Console.WriteLine(Resources.TsvFileInfo, sheet.Info.FullName);
            var count = 0;
            foreach (var entry in sheet)
            {
                if (0 == count)
                {
                    var columns = new StringBuilder();
                    foreach (var key in entry.Keys)
                    {
                        var format = 0 == columns.Length
                            ? "Columns: "
                            : "{0}         ".FormatWith(Environment.NewLine);
                        columns.Append(format + key);
                    }

                    Console.WriteLine(columns);
                }

                count++;
            }

            Console.WriteLine(Resources.TsvEntryCount, count);
        }
    }
}