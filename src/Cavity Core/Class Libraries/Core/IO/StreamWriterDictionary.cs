namespace Cavity.IO
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;
#if !NET20
    using System.Linq;
#endif
    using System.Runtime.Serialization;
    using System.Security.Permissions;
    using System.Text;

    [Serializable]
    public class StreamWriterDictionary : Dictionary<string, StreamWriter>, 
                                          IDisposable
    {
        public StreamWriterDictionary()
        {
            Access = FileAccess.Write;
            Mode = FileMode.OpenOrCreate;
            Share = FileShare.None;
        }

        public StreamWriterDictionary(string firstLine)
            : this()
        {
            FirstLine = firstLine;
        }

        protected StreamWriterDictionary(SerializationInfo info, 
                                         StreamingContext context)
            : base(info, context)
        {
        }

        ~StreamWriterDictionary()
        {
            Dispose(false);
        }

        public FileAccess Access { get; set; }

        public string FirstLine { get; set; }

        public FileMode Mode { get; set; }

        public FileShare Share { get; set; }

        private bool Disposed { get; set; }

        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        public override void GetObjectData(SerializationInfo info, 
                                           StreamingContext context)
        {
            base.GetObjectData(info, context);
        }

        public virtual StreamWriter Item(FileInfo file)
        {
            return Item(file, FirstLine);
        }

        public virtual StreamWriter Item(string fileName)
        {
            return Item(fileName, Mode, Access, Share);
        }

        public virtual StreamWriter Item(string fileName, 
                                         FileMode mode, 
                                         FileAccess access, 
                                         FileShare share)
        {
            return Item(fileName, FirstLine, mode, access, share);
        }

        [SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters", Justification = "Only files are supported.")]
        public virtual StreamWriter Item(FileInfo file, 
                                         string firstLine)
        {
            if (null == file)
            {
                throw new ArgumentNullException("file");
            }

            return Item(file.FullName, firstLine, Mode, Access, Share);
        }

        public virtual StreamWriter Item(string fileName, 
                                         string firstLine)
        {
            return Item(fileName, firstLine, Mode, Access, Share);
        }

        public virtual StreamWriter Item(string fileName, 
                                         string firstLine, 
                                         FileMode mode, 
                                         FileAccess access, 
                                         FileShare share)
        {
            return Item(new FileInfo(fileName), firstLine, mode, access, share);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!Disposed && disposing)
            {
#if NET20
                foreach (var item in this)
#else
                foreach (var item in this.Where(x => null != x.Value))
#endif
                {
#if NET20
                    if (null == item.Value)
                    {
                        continue;
                    }
#endif

                    item.Value.Dispose();
                }
            }

            Disposed = true;
        }

        private StreamWriter Item(FileInfo file, 
                                  string firstLine, 
                                  FileMode mode, 
                                  FileAccess access, 
                                  FileShare share)
        {
            if (Disposed)
            {
                throw new InvalidOperationException("This object has been disposed.");
            }

            if (!ContainsKey(file.FullName))
            {
                var exists = file.Exists;
                var writer = new StreamWriter(file.Open(mode, access, share), Encoding.UTF8);
                Add(file.FullName, writer);
                if (null != firstLine)
                {
                    switch (mode)
                    {
                        case FileMode.Append:
                            if (!exists)
                            {
                                writer.WriteLine(firstLine);
                            }

                            break;

                        default:
                            writer.WriteLine(firstLine);
                            break;
                    }
                }
            }

            return this[file.FullName];
        }
    }
}