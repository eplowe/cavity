﻿namespace Cavity.IO
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;
    using System.Xml.XPath;

    public static class FileInfoExtensionMethods
    {
#if NET20
        public static void Append(FileInfo obj,
                                  string value)
#else
        public static void Append(this FileInfo obj,
                                  string value)
#endif
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

#if NET20
        public static void AppendLine(FileInfo obj,
                                      string value)
#else
        public static void AppendLine(this FileInfo obj,
                                      string value)
#endif
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

#if NET20
        public static void Create(FileInfo obj,
                                  string value)
#else
        public static void Create(this FileInfo obj,
                                  string value)
#endif
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

#if NET20
        public static void Create(FileInfo obj,
                                  IXPathNavigable xml)
#else
        public static void Create(this FileInfo obj,
                                  IXPathNavigable xml)
#endif
        {
            if (null == obj)
            {
                throw new ArgumentNullException("obj");
            }

            if (null == xml)
            {
                throw new ArgumentNullException("xml");
            }

            Create(obj, xml.CreateNavigator().OuterXml);
        }

        [SuppressMessage("Microsoft.Naming", "CA1711:IdentifiersShouldNotHaveIncorrectSuffix", Justification = "This naming is intentional")]
#if NET20
        public static void CreateNew(FileInfo obj)
#else
        public static void CreateNew(this FileInfo obj)
#endif
        {
            CreateNew(obj, null);
        }

        [SuppressMessage("Microsoft.Naming", "CA1711:IdentifiersShouldNotHaveIncorrectSuffix", Justification = "This naming is intentional")]
#if NET20
        public static void CreateNew(FileInfo obj,
                                     string value)
#else
        public static void CreateNew(this FileInfo obj,
                                     string value)
#endif
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

#if NET20
        public static IEnumerable<string> Lines(FileInfo obj)
#else
        public static IEnumerable<string> Lines(this FileInfo obj)
#endif
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

#if NET20
        public static string ReadToEnd(FileInfo obj)
#else
        public static string ReadToEnd(this FileInfo obj)
#endif
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

#if NET20
        public static void Truncate(FileInfo obj,
                                    string value)
#else
        public static void Truncate(this FileInfo obj,
                                    string value)
#endif
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