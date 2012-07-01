namespace Cavity.IO
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;
#if !NET20
    using System.Linq;
#endif
    using System.Text;
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
        public static FileInfo DeduplicateLines(FileInfo obj)
#else
        public static FileInfo DeduplicateLines(this FileInfo obj)
#endif
        {
            if (null == obj)
            {
                throw new ArgumentNullException("obj");
            }

#if NET20
            var lines = new List<string>();
            foreach (var line in Lines(obj))
            {
                if (!lines.Contains(line))
                {
                    lines.Add(line);
                }
            }
#else
            var lines = new HashSet<string>();
            foreach (var line in obj.Lines().Where(line => !lines.Contains(line)))
            {
                lines.Add(line);
            }
#endif

            var buffer = new StringBuilder();
            foreach (var line in lines)
            {
                buffer.AppendLine(line);
            }

#if NET20
            return Truncate(obj, buffer.ToString());
#else
            return obj.Truncate(buffer.ToString());
#endif
        }

        [SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters", Justification = "I want type safety here.")]
#if NET20
        public static bool FixNewLine(FileInfo file)
#else
        public static bool FixNewLine(this FileInfo file)
#endif
        {
            if (null == file)
            {
                throw new ArgumentNullException("file");
            }

            if (!file.Exists)
            {
                throw new FileNotFoundException(file.FullName);
            }

            var changed = false;
            using (var temp = new TempFile())
            {
                using (var tempStream = File.Open(temp.Info.FullName, FileMode.Open, FileAccess.Write, FileShare.Read))
                {
                    using (var tempWriter = new StreamWriter(tempStream))
                    {
                        using (var stream = File.Open(file.FullName, FileMode.Open, FileAccess.Read, FileShare.Read))
                        {
                            using (var reader = new StreamReader(stream))
                            {
                                var cr = false;
                                while (true)
                                {
                                    var i = reader.Read();
                                    if (-1 == i)
                                    {
                                        break;
                                    }

                                    switch (i)
                                    {
                                        case '\r':
                                            cr = true;
                                            break;

                                        case '\n':
                                            tempWriter.Write("\r\n");
                                            if (cr)
                                            {
                                                cr = false;
                                            }
                                            else
                                            {
                                                changed = true;
                                            }

                                            break;

                                        default:
                                            if (cr)
                                            {
                                                tempWriter.Write("\r");
                                                cr = false;
                                            }

                                            tempWriter.Write((char)i);
                                            break;
                                    }
                                }
                            }
                        }
                    }
                }

                if (changed)
                {
                    temp.Info.CopyTo(file.FullName, true);
                }
            }

            return changed;
        }
#if NET20
        [SuppressMessage("Microsoft.Performance", "CA1804:RemoveUnusedLocals", MessageId = "line", Justification = "There is no other way to count the lines.")]
        public static int LineCount(FileInfo obj)
        {
            var result = 0;
            foreach (var line in Lines(obj))
            {
                result++;
            }

            return result;
        }
#else
        public static int LineCount(this FileInfo obj)
        {
            return obj.Lines().Count();
        }
#endif

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