﻿namespace Cavity.Build.Sdk
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;
    using System.IO;
#if !NET20
    using System.Linq;
#endif
    using System.Text;
    using Cavity.Diagnostics;
    using Cavity.Win32;

    public abstract class CompilerBase
    {
        protected CompilerBase(FileInfo location)
        {
            Location = location;
        }

        protected CompilerBase(string toolName)
        {
            ToolName = toolName;
        }

        private FileInfo Location { get; set; }

        private string ToolName { get; set; }

        public static FileInfo ToApplicationPath(string name)
        {
            if (null == name)
            {
                throw new ArgumentNullException("name");
            }

            if (0 == name.Length)
            {
                throw new ArgumentOutOfRangeException("name");
            }

            const string key = @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SDKs\Windows";
            var version = RegistryFacade.GetValue(key, "CurrentVersion");
            if (null == version)
            {
                return null;
            }

            var installation = (string)RegistryFacade.GetValue(string.Format(CultureInfo.InvariantCulture, @"{0}\{1}", key, version), "InstallationFolder");
            if (null == installation)
            {
                return null;
            }

            var bin = new DirectoryInfo(Path.Combine(installation, "Bin"));
            if (!bin.Exists)
            {
                return null;
            }

            var exe = new FileInfo(Path.Combine(bin.FullName, name));

            return exe.Exists ? exe : null;
        }

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
                p.StartInfo = new ProcessStartInfo
                {
                    Arguments = ToArguments(ToFileNames(files)),
                    FileName = null == Location ? ToolName : Location.FullName,
                    RedirectStandardError = true,
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    WorkingDirectory = workingDirectory.FullName
                };
                p.Start();
                using (var reader = p.StandardOutput)
                {
                    result = reader.ReadToEnd();
                }

                if (0 != p.ExitCode)
                {
                    using (var reader = p.StandardError)
                    {
                        throw new InvalidOperationException(reader.ReadToEnd());
                    }
                }
            }

            return result;
        }

        public abstract string ToArguments(IEnumerable<string> files);
    }
}