namespace Cavity
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

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
                Process(file);
            }
        }

        private static void Process(FileSystemInfo file)
        {
            long count = 0;
            using (var stream = File.Open(file.FullName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                using (var reader = new StreamReader(stream))
                {
                    while (!reader.EndOfStream)
                    {
                        Console.WriteLine(reader.ReadLine());
                        count++;
                        if (10 == count)
                        {
                            return;
                        }
                    }
                }
            }
        }
    }
}