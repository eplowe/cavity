namespace Cavity.IO
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;
    using System.Xml.XPath;

    public static class FileInfoExtensionMethods
    {
        public static void Append(this FileInfo obj,
                                  string value)
        {
            if (null == obj)
            {
                throw new ArgumentNullException("obj");
            }

            using (var stream = obj.Open(FileMode.Append, FileAccess.Write, FileShare.Read))
            {
                using (var writer = new StreamWriter(stream))
                {
                    if (null != value)
                    {
                        writer.Write(value);
                    }
                }
            }
        }

        public static void AppendLine(this FileInfo obj,
                                      string value)
        {
            if (null == obj)
            {
                throw new ArgumentNullException("obj");
            }

            using (var stream = obj.Open(FileMode.Append, FileAccess.Write, FileShare.Read))
            {
                using (var writer = new StreamWriter(stream))
                {
                    writer.WriteLine(value);
                }
            }
        }

        public static void Create(this FileInfo obj,
                                  string value)
        {
            if (null == obj)
            {
                throw new ArgumentNullException("obj");
            }

            using (var stream = obj.Open(FileMode.Create, FileAccess.Write, FileShare.Read))
            {
                using (var writer = new StreamWriter(stream))
                {
                    if (null != value)
                    {
                        writer.Write(value);
                    }
                }
            }
        }

        public static void Create(this FileInfo obj,
                                  IXPathNavigable xml)
        {
            if (null == obj)
            {
                throw new ArgumentNullException("obj");
            }

            if (null == xml)
            {
                throw new ArgumentNullException("xml");
            }

            obj.Create(xml.CreateNavigator().OuterXml);
        }

        [SuppressMessage("Microsoft.Naming", "CA1711:IdentifiersShouldNotHaveIncorrectSuffix", Justification = "This naming is intentional")]
        public static void CreateNew(this FileInfo obj)
        {
            obj.CreateNew(null);
        }

        [SuppressMessage("Microsoft.Naming", "CA1711:IdentifiersShouldNotHaveIncorrectSuffix", Justification = "This naming is intentional")]
        public static void CreateNew(this FileInfo obj,
                                     string value)
        {
            if (null == obj)
            {
                throw new ArgumentNullException("obj");
            }

            using (var stream = obj.Open(FileMode.CreateNew, FileAccess.Write, FileShare.Read))
            {
                using (var writer = new StreamWriter(stream))
                {
                    if (null != value)
                    {
                        writer.Write(value);
                    }
                }
            }
        }

        public static IEnumerable<string> Lines(this FileInfo obj)
        {
            if (null == obj)
            {
                throw new ArgumentNullException("obj");
            }

            using (var stream = obj.Open(FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                using (var reader = new StreamReader(stream))
                {
                    while (reader.Peek() >
                           -1)
                    {
                        yield return reader.ReadLine();
                    }
                }
            }
        }

        public static string ReadToEnd(this FileInfo obj)
        {
            if (null == obj)
            {
                throw new ArgumentNullException("obj");
            }

            using (var stream = obj.Open(FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                using (var reader = new StreamReader(stream))
                {
                    return reader.ReadToEnd();
                }
            }
        }

        public static void Truncate(this FileInfo obj,
                                    string value)
        {
            if (null == obj)
            {
                throw new ArgumentNullException("obj");
            }

            using (var stream = obj.Open(FileMode.Truncate, FileAccess.Write, FileShare.Read))
            {
                using (var writer = new StreamWriter(stream))
                {
                    if (null != value)
                    {
                        writer.Write(value);
                    }
                }
            }
        }
    }
}