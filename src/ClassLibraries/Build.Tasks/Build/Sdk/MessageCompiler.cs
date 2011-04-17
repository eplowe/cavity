namespace Cavity.Build.Sdk
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;
    using System.Linq;
    using System.Text;
    using Cavity.Diagnostics;
    using Cavity.IO;
    using Cavity.Win32;

    public sealed class MessageCompiler
    {
        private MessageCompiler(FileInfo location)
        {
            Location = location;
        }

        public static MessageCompiler Current
        {
            get
            {
                const string key = @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SDKs\Windows";
                var version = RegistryFacade.GetValue(key, "CurrentVersion");
                if (null == version)
                {
                    return null;
                }

                var installation = (string)RegistryFacade.GetValue(@"{0}\{1}".FormatWith(key, version), "InstallationFolder");
                if (null == installation)
                {
                    return null;
                }

                var bin = new DirectoryInfo(Path.Combine(installation, "Bin"));
                if (!bin.Exists)
                {
                    return null;
                }

                var exe = new FileInfo(Path.Combine(bin.FullName, "MC.exe"));
                return exe.Exists
                    ? new MessageCompiler(exe)
                    : null;
            }
        }

        public FileInfo Location { get; private set; }

        [SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters", Justification = "I do not want directories.")]
        public string Compile(DirectoryInfo outputPath, IEnumerable<FileInfo> files)
        {
            if (null == outputPath)
            {
                throw new ArgumentNullException("outputPath");
            }

            if (null == files)
            {
                throw new ArgumentNullException("files");
            }

            if (0 == files.Count())
            {
                throw new ArgumentOutOfRangeException("files");
            }

            string result;
            using (var temp = new TempDirectory())
            {
                foreach (var file in files)
                {
                    var source = file.FullName;
                    var destination = new FileInfo(Path.Combine(temp.Info.FullName, file.Name)).FullName;

                    if (outputPath.Exists)
                    {
                        File.Copy(source, destination);
                    }
                }

                using (var p = ProcessFacade.Current)
                {
                    p.StartInfo = new ProcessStartInfo
                    {
                        Arguments = ToArguments(ToFiles(files)),
                        FileName = Location.FullName,
                        RedirectStandardError = true,
                        RedirectStandardOutput = true,
                        UseShellExecute = false,
                        WorkingDirectory = temp.Info.FullName
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
            }

            return result;
        }

        private static string ToArguments(IEnumerable<string> files)
        {
            var buffer = new StringBuilder();
            buffer.Append("-u -U ");
            foreach (var file in files)
            {
                buffer.Append(file);
            }

            return buffer.ToString();
        }

        private static IEnumerable<string> ToFiles(IEnumerable<FileInfo> files)
        {
            return files.Select(file => file.Name);
        }
    }
}