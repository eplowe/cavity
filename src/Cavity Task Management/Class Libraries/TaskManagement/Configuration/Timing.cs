namespace Cavity.Configuration
{
    using System;
    using System.Diagnostics;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;
    using Cavity.Diagnostics;
    using Cavity.IO;
    using Cavity.Reflection;

    public static class Timing
    {
        private static readonly object _lock = new object();

        [SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Justification = "This design is intentional.")]
        public static DateTime Due<T>()
        {
            var file = ToFile(typeof(T), "wait");

            return file.Exists
                ? file.ReadToEnd().To<DateTime>()
                : DateTime.MinValue;
        }

        public static FileInfo ToFile(Type type, string extension)
        {
            Trace.WriteLineIf(Tracing.Is.TraceVerbose, string.Empty);
            if (null == type)
            {
                throw new ArgumentNullException("type");
            }

            if (null == extension)
            {
                throw new ArgumentNullException("extension");
            }

            extension = extension.Trim();
            if (0 == extension.Length)
            {
                throw new ArgumentOutOfRangeException("extension");
            }

            return new FileInfo(Path.Combine(type.Assembly.Directory().FullName, "{0}.{1}".FormatWith(type.Name, extension)));
        }

        [SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Justification = "This design is intentional.")]
        public static bool Wait<T>(TimeSpan duration)
        {
            lock (_lock)
            {
                var file = ToFile(typeof(T), "wait");
                if (file.Exists)
                {
                    var due = file.ReadToEnd().To<DateTime>();
                    if (due > DateTime.UtcNow)
                    {
                        return true;
                    }

                    file.Delete();
                }

                file.CreateNew(DateTime.UtcNow.Add(duration).ToXmlString());

                return false;
            }
        }
    }
}