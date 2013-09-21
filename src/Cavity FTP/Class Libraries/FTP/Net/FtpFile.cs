namespace Cavity.Net
{
    using System;
    using System.Diagnostics;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;
    using System.Linq;
    using System.Net;
    using Cavity.Collections;
    using Cavity.Diagnostics;
    using Cavity.IO;

    public class FtpFile
    {
        private DateTime? _lastModified;

        private long? _size;

        public FtpFile(FtpDirectory directory,
                       string path)
        {
            if (null == directory)
            {
                throw new ArgumentNullException("directory");
            }

            if (null == path)
            {
                throw new ArgumentNullException("path");
            }

            if (path.Trim().IsEmpty())
            {
                throw new ArgumentOutOfRangeException("path");
            }

            Trace.WriteLineIf(Tracing.Is.TraceVerbose, "[FTP] directory={0} path={1}".FormatWith(directory.Location, path));

            Directory = directory;
            Path = path;
        }

        public FtpDirectory Directory { get; private set; }

        public DateTime LastModified
        {
            get
            {
                if (_lastModified.HasValue)
                {
                    return _lastModified.Value;
                }

                var request = FtpDirectory.ToFtpWebRequest(Directory.Location, Path, Directory.Credentials);
                request.Method = WebRequestMethods.Ftp.GetDateTimestamp;
                request.UsePassive = Directory.Passive;

                using (var response = (FtpWebResponse)request.GetResponse())
                {
                    _lastModified = response.LastModified.ToUniversalTime();
                }

                return _lastModified.Value;
            }
        }

        public string Name
        {
            get
            {
                return Path.Split('/').LastOrDefault();
            }
        }

        public string Path { get; set; }

        public long Size
        {
            get
            {
                if (_size.HasValue)
                {
                    return _size.Value;
                }

                var request = FtpDirectory.ToFtpWebRequest(Directory.Location, Path, Directory.Credentials);
                request.Method = WebRequestMethods.Ftp.GetFileSize;
                request.UsePassive = Directory.Passive;

                using (var response = (FtpWebResponse)request.GetResponse())
                {
                    _size = response.ContentLength;
                }

                return _size.Value;
            }
        }

        public void Delete()
        {
            var request = FtpDirectory.ToFtpWebRequest(Directory.Location, Path, Directory.Credentials);
            request.Method = WebRequestMethods.Ftp.DeleteFile;
            request.UsePassive = Directory.Passive;

            using (var response = (FtpWebResponse)request.GetResponse())
            {
                Trace.TraceInformation("[FTP] {0} was deleted: {1} {2}".FormatWith(request.RequestUri, response.StatusCode, response.StatusDescription.RemoveFromEnd(Environment.NewLine, StringComparison.Ordinal)));
            }
        }

        [SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters", Justification = "I want type safety.")]
        public FileInfo Download(DirectoryInfo local)
        {
            if (null == local)
            {
                throw new ArgumentNullException("local");
            }

            var request = FtpDirectory.ToFtpWebRequest(Directory.Location, Path, Directory.Credentials);
            request.Method = WebRequestMethods.Ftp.DownloadFile;
            request.UsePassive = Directory.Passive;
            request.UseBinary = true;

            using (var response = (FtpWebResponse)request.GetResponse())
            {
                using (var stream = response.GetResponseStream())
                {
                    if (null == stream)
                    {
                        throw new InvalidOperationException();
                    }

                    using (var temp = new CurrentTempDirectory())
                    {
                        var file = temp.Info.ToFile(Name);
                        using (var writer = file.ToWriteStream(FileMode.CreateNew))
                        {
                            stream.CopyTo(writer);
                        }

                        if (file.Length.IsNot(Size))
                        {
                            throw new InvalidOperationException();
                        }

                        Trace.WriteLineIf(Tracing.Is.TraceVerbose, "[FTP] source={0} destination={1}".FormatWith(request.RequestUri.AbsoluteUri, local.FullName));

                        file.MoveTo(local.ToFile(Name).FullName);
                        file.SetDate(LastModified);

                        return file;
                    }
                }
            }
        }
    }
}