namespace Cavity.IO
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;

    public static class FileInfoExtensionMethods
    {
        [SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters", Justification = "I want type safety here.")]
        public static bool FixNewLine(this FileInfo file)
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
    }
}