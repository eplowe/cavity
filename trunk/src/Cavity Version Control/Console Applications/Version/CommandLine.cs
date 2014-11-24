namespace Cavity
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Diagnostics.CodeAnalysis;
    using System.Reflection;
    using System.Security;
    using Cavity.Properties;

#if NET20 || NET35
    using System.Security.Permissions;
#endif

    public sealed class CommandLine
    {
        private CommandLine()
        {
            Parameters = new List<string>();
        }

        private CommandLine(IEnumerable<string> parameters)
        {
            Parameters = parameters;
        }

        public bool Help
        {
            get
            {
                return null != this["?"];
            }
        }

        public bool Info
        {
            get
            {
                return null != this["i"];
            }
        }

        public IEnumerable<string> Parameters { get; private set; }

        public string this[string key]
        {
            get
            {
                foreach (var parameter in Parameters)
                {
                    if (string.Equals(parameter, string.Concat('/', key), StringComparison.OrdinalIgnoreCase))
                    {
                        return string.Empty;
                    }

                    if (parameter.StartsWith(string.Concat('/', key, ':'), StringComparison.OrdinalIgnoreCase))
                    {
                        return parameter.Substring(parameter.IndexOf(':') + 1);
                    }
                }

                return null;
            }
        }

#if NET20 || NET35
        [SecurityPermission(SecurityAction.LinkDemand, UnmanagedCode = true)]
#else
        [SuppressMessage("Microsoft.Security", "CA2122:DoNotIndirectlyExposeMethodsWithLinkDemands", Justification = "CA2122 rule has not been fully updated to work with level 2 transparency.")]
        [SecurityCritical]
#endif
        public static CommandLine Load(ICollection<string> args)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var info = FileVersionInfo.GetVersionInfo(assembly.Location).ProductVersion;
            Console.WriteLine(Resources.Splash, info);
            if (null == args)
            {
                return new CommandLine();
            }

            var result = new CommandLine(args);
            if (!result.Help)
            {
                Console.WriteLine(string.Empty);
                Console.WriteLine(Resources.Usage);
                Console.WriteLine(string.Empty);
                Console.WriteLine(Resources.UsageDetails);
                Console.WriteLine(string.Empty);
                Console.WriteLine(Resources.Parameters);
                Console.WriteLine(string.Empty);
                Console.WriteLine(Resources.ParametersHelp);
                Console.WriteLine(Resources.ParametersInfo);
                Console.WriteLine(string.Empty);
                Console.WriteLine(Resources.HelpLink);
            }

            return result;
        }
    }
}