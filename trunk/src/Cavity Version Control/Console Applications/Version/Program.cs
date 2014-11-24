namespace Cavity
{
    using System;
    using System.Collections.Generic;
    using Cavity.Models;
    using Cavity.Properties;

    public static class Program
    {
        public static void Main(string[] args)
        {
            Process(CommandLine.Load(args), Environment.CurrentDirectory);
        }

        public static void Process(CommandLine args,
                                   string directory)
        {
            if (null == args)
            {
                throw new ArgumentNullException("args");
            }

            if (args.Help)
            {
                return;
            }

            var projects = ProjectCollection.Load(directory);
            if (args.Info)
            {
                EmitInformation(projects);
            }

#if DEBUG
            Console.WriteLine(Resources.PressEnterToExit);
            Console.Read();
#endif
        }

        private static void EmitInformation(IEnumerable<Project> projects)
        {
            var color = Console.ForegroundColor;

            foreach (var project in projects)
            {
                Console.WriteLine(project.Location.FullName);
                foreach (var package in project.Packages)
                {
                    var reference = project.PackageReference(package);
                    if (null == reference)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine(Resources.PackageUnused, package.Id, package.Version);
                        Console.ForegroundColor = color;
                        continue;
                    }

                    if (reference.Version != package.Version)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(Resources.PackageUnused, package.Id, package.Version);
                        Console.ForegroundColor = color;
                        continue;
                    }

                    Console.WriteLine(Resources.PackageInfo, package.Id, package.Version);
                }
            }
        }
    }
}