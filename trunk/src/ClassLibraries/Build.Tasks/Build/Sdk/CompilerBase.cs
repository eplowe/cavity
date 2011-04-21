namespace Cavity.Build.Sdk
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;
#if !NET20
    using System.Linq;
#endif
    using System.Text;
    using Cavity.Diagnostics;

    public abstract class CompilerBase
    {
        protected CompilerBase(FileInfo location)
        {
            Location = location;
        }

        private FileInfo Location { get; set; }

        public static string ToArguments(string switches,
                                         IEnumerable<string> files)
        {
            if (null == files)
            {
                throw new ArgumentNullException("files");
            }

#if !NET20
            if (0 == files.Count())
            {
                throw new ArgumentOutOfRangeException("files");
            }
#endif

            var buffer = new StringBuilder();
            if (null != switches)
            {
                buffer.Append(switches.Trim());
                buffer.Append(' ');
            }

            foreach (var file in files)
            {
                buffer.Append(file);
                buffer.Append(' ');
            }

            return buffer.ToString().Trim();
        }

        public static IEnumerable<string> ToFileNames(IEnumerable<FileInfo> files)
        {
#if NET20
            foreach (var file in files)
            {
                yield return file.Name;
            }
#else
            return files.Select(file => file.Name);
#endif
        }
        
        [SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters", Justification = "I do not want directories.")]
        public string Compile(DirectoryInfo workingDirectory,
                              IEnumerable<FileInfo> files)
        {
            return Compile(workingDirectory, files, 9000);
        }

        [SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters", Justification = "I do not want directories.")]
        public string Compile(DirectoryInfo workingDirectory,
                              IEnumerable<FileInfo> files,
                              int? wait)
        {
            if (null == workingDirectory)
            {
                throw new ArgumentNullException("workingDirectory");
            }

            if (null == files)
            {
                throw new ArgumentNullException("files");
            }

#if !NET20
            if (0 == files.Count())
            {
                throw new ArgumentOutOfRangeException("files");
            }
#endif

            string result;
            using (var p = ProcessFacade.Current)
            {
                var args = ToArguments(ToFileNames(files));
                p.StartInfo = new ProcessStartInfo
                {
                    Arguments = args,
                    CreateNoWindow = true,
                    FileName = Location.FullName,
                    RedirectStandardError = true,
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    WindowStyle = ProcessWindowStyle.Hidden,
                    WorkingDirectory = workingDirectory.FullName
                };
                if (wait.HasValue)
                {
                    p.WaitForExit(wait.Value);
                }
                else
                {
                    p.Start();
                }

                if (0 != p.ExitCode)
                {
                    using (var reader = p.StandardOutput)
                    {
                        result = reader.ReadToEnd();
                    }
                }
                else
                {
                    using (var reader = p.StandardError)
                    {
                        var message = string.Concat(Location.FullName, ' ', args, Environment.NewLine, reader.ReadToEnd());
                        throw new Win32Exception(message);
                    }
                }
            }

            return result;
        }

        public abstract string ToArguments(IEnumerable<string> files);
    }
}