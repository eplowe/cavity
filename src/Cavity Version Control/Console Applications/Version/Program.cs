namespace Cavity
{
    using System;

    using Cavity.Models;

    public static class Program
    {
        public static void Main(string[] args)
        {
            Process(CommandLine.Load(args));
        }

        public static void Process(CommandLine args)
        {
            if (null == args)
            {
                throw new ArgumentNullException("args");
            }

            if (args.Help)
            {
                return;
            }

            if (args.Info)
            {
                Console.WriteLine(Environment.CurrentDirectory);
                foreach (var project in ProjectCollection.Load(Environment.CurrentDirectory))
                {
                    Console.WriteLine(project.CSharp.FullName);
                }
            }

            Console.Read();
        }
    }
}