namespace Cavity.IO
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;
    using System.Xml.XPath;

    public static class FileInfoExtensionMethods
    {
#if NET20
        public static FileInfo Append(FileInfo obj, 
                                      object value)
#else
        public static FileInfo Append(this FileInfo obj, 
                                      object value)
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
                        writer.Write(value.ToString());
                    }
                }
            }

            return obj;
        }

#if NET20
        public static FileInfo AppendLine(FileInfo obj, 
                                          object value)
#else
        public static FileInfo AppendLine(this FileInfo obj,
                                          object value)
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
                        writer.WriteLine(value.ToString());
                    }
                }
            }

            return obj;
        }

#if NET20
        public static FileInfo Create(FileInfo obj, 
                                  string value)
#else
        public static FileInfo Create(this FileInfo obj, 
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

            return obj;
        }

#if NET20
        public static FileInfo Create(FileInfo obj, 
                                  IXPathNavigable xml)
#else
        public static FileInfo Create(this FileInfo obj, 
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

            return obj;
        }

        [SuppressMessage("Microsoft.Naming", "CA1711:IdentifiersShouldNotHaveIncorrectSuffix", Justification = "This naming is intentional")]
#if NET20
        public static FileInfo CreateNew(FileInfo obj)
#else
        public static FileInfo CreateNew(this FileInfo obj)
#endif
        {
            return CreateNew(obj, null);
        }

        [SuppressMessage("Microsoft.Naming", "CA1711:IdentifiersShouldNotHaveIncorrectSuffix", Justification = "This naming is intentional")]
#if NET20
        public static FileInfo CreateNew(FileInfo obj, 
                                         object value)
#else
        public static FileInfo CreateNew(this FileInfo obj, 
                                         object value)
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
                        writer.Write(value.ToString());
                    }
                }
            }

            return obj;
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
        public static FileInfo Truncate(FileInfo obj, 
                                    string value)
#else
        public static FileInfo Truncate(this FileInfo obj, 
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

            return obj;
        }

#if NET20
        public static FileInfo Truncate(FileInfo obj, 
                                        IXPathNavigable xml)
#else
        public static FileInfo Truncate(this FileInfo obj,
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

            Truncate(obj, xml.CreateNavigator().OuterXml);

            return obj;
        }
    }
}