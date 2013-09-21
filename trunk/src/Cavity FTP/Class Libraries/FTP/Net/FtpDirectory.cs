namespace Cavity.Net
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Diagnostics;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;
    using System.Net;
    using System.Net.Security;
    using System.Security;
    using System.Security.Cryptography.X509Certificates;
    using System.Security.Policy;
    using Cavity.Diagnostics;

    [SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix", Justification = "This naming is intentional.")]
    public class FtpDirectory : Collection<FtpFile>
    {
        public ICredentials Credentials { get; private set; }

        public Uri Location { get; private set; }

        public bool Passive { get; set; }

        public bool Secure { get; set; }

        public static IEnumerable<FtpFile> Load(Uri location,
            ICredentials credentials,
            bool passive,
            bool secure)
        {
            if (null == location)
            {
                throw new ArgumentNullException("location");
            }

            Trace.WriteLineIf(Tracing.Is.TraceVerbose, "[FTP] location={0}".FormatWith(location.AbsoluteUri));

            var result = new FtpDirectory
                {
                    Credentials = credentials,
                    Location = location,
                    Passive = passive,
                    Secure = secure,
                };

            return result.Load();
        }

        internal static FtpWebRequest ToFtpWebRequest(Uri location,
            string name,
            ICredentials credentials)
        {
            if (null == location)
            {
                throw new ArgumentNullException("location");
            }

            Trace.WriteLineIf(Tracing.Is.TraceVerbose, "[FTP] location={0} name={1}".FormatWith(location.AbsoluteUri, name));

            return ToFtpWebRequest(new Uri(location, name), credentials);
        }

        internal static FtpWebRequest ToFtpWebRequest(Uri location,
            ICredentials credentials)
        {
            if (null == location)
            {
                throw new ArgumentNullException("location");
            }

            Trace.WriteLineIf(Tracing.Is.TraceVerbose, "[FTP] location={0}".FormatWith(location.AbsoluteUri));

            var request = (FtpWebRequest)WebRequest.Create(location);
            request.Credentials = credentials;

            return request;
        }

        private static bool CertificateValidation(object sender,
            X509Certificate certificate,
            X509Chain chain,
            SslPolicyErrors sslPolicyErrors)
        {
            if (SslPolicyErrors.RemoteCertificateChainErrors == (sslPolicyErrors & SslPolicyErrors.RemoteCertificateChainErrors))
            {
                return true;
            }

            if (SslPolicyErrors.RemoteCertificateNameMismatch == (sslPolicyErrors & SslPolicyErrors.RemoteCertificateNameMismatch))
            {
                var zone = Zone.CreateFromUrl(((HttpWebRequest)sender).RequestUri.ToString());
                return SecurityZone.Intranet == zone.SecurityZone ||
                       SecurityZone.MyComputer == zone.SecurityZone;
            }

            return false;
        }

        private IEnumerable<FtpFile> Load()
        {
            var request = ToFtpWebRequest(Location, Credentials);
            request.Method = WebRequestMethods.Ftp.ListDirectory;
            request.UsePassive = Passive;
            if (Secure)
            {
                ServicePointManager.ServerCertificateValidationCallback = CertificateValidation;
                request.EnableSsl = true;
            }

            try
            {
                using (var response = (FtpWebResponse)request.GetResponse())
                {
                    using (var stream = response.GetResponseStream())
                    {
                        if (null == stream)
                        {
                            throw new InvalidOperationException();
                        }

                        using (var reader = new StreamReader(stream))
                        {
                            while (reader.EndOfStream.IsFalse())
                            {
                                var line = reader.ReadLine();
                                if (null == line)
                                {
                                    continue;
                                }

                                if (line.Contains('.'))
                                {
                                    Add(new FtpFile(this, line));
                                }
                            }
                        }
                    }
                }
            }
            catch (WebException)
            {
                Trace.TraceError(Location.AbsoluteUri);
                throw;
            }

            return this;
        }
    }
}