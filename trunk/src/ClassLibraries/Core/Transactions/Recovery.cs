////namespace Cavity.Transactions
////{
////    using System;
////    using System.Collections.Generic;
////    using System.Diagnostics;
////    using System.Diagnostics.CodeAnalysis;
////    using System.IO;
////    using System.Linq;
////    using Cavity.Diagnostics;
////    using Cavity.IO;

////    public static class Recovery
////    {
////        private static readonly object _this = new object();

////        private static DirectoryInfo _directory;

////        public static DirectoryInfo MasterDirectory
////        {
////            get
////            {
////                return _directory ?? new DirectoryInfo(Path.Combine(Path.GetTempPath(), "Transactions"));
////            }

////            set
////            {
////                _directory = value;
////            }
////        }

////        public static void Exclude(Operation operation,
////                                   bool rollback)
////        {
////            Trace.WriteIf(Tracing.Enabled, string.Empty);
////            Amend(operation, rollback);
////        }

////        public static void Include(Operation operation)
////        {
////            Trace.WriteIf(Tracing.Enabled, string.Empty);
////            Amend(operation, null);
////        }

////        public static FileInfo ItemFile(Operation operation)
////        {
////            return ItemFile(operation, null);
////        }

////        public static FileInfo ItemFile(Operation operation,
////                                        string outcome)
////        {
////            Trace.WriteIf(Tracing.Enabled, string.Empty);
////            if (null == operation)
////            {
////                throw new ArgumentNullException("operation");
////            }

////            return ItemDirectory(operation, outcome).ToFile("{0}.xml".FormatWith(operation.Identity.Instance));
////        }

////        public static FileInfo MasterFile(Operation operation)
////        {
////            Trace.WriteIf(Tracing.Enabled, string.Empty);
////            if (null == operation)
////            {
////                throw new ArgumentNullException("operation");
////            }

////            return MasterDirectory.ToFile("{0}.master".FormatWith(operation.Identity.ResourceManager));
////        }

////        private static void Amend(Operation operation,
////                                  bool? success)
////        {
////            Trace.WriteIf(Tracing.Enabled, string.Empty);
////            if (null == operation)
////            {
////                throw new ArgumentNullException("operation");
////            }

////            if (null == _directory)
////            {
////                Trace.WriteIf(Tracing.Enabled, string.Empty);
////            }

////            lock (_this)
////            {
////                var master = MasterFile(operation);
////                if (!master.Directory.Exists)
////                {
////                    master.Directory.Create();
////                }

////                var file = Save(operation, success);
////                var items = new HashSet<string>();
////                if (master.Exists)
////                {
////                    foreach (var line in master
////                        .Lines()
////                        .Where(x => !string.IsNullOrEmpty(x) && !x.Equals(file.FullName, StringComparison.OrdinalIgnoreCase)))
////                    {
////                        if (!items.Contains(line))
////                        {
////                            items.Add(line);
////                        }
////                    }
////                }

////                using (var stream = master.Open(FileMode.Create, FileAccess.Write, FileShare.Read))
////                {
////                    using (var writer = new StreamWriter(stream))
////                    {
////                        foreach (var item in items)
////                        {
////                            writer.WriteLine(item);
////                        }

////                        if (!success.HasValue)
////                        {
////                            writer.WriteLine(file.FullName);
////                        }
////                    }
////                }
////            }
////        }

////        private static DirectoryInfo ItemDirectory(Operation operation,
////                                                   string outcome)
////        {
////            var dir = MasterDirectory.ToDirectory(operation.Identity.ResourceManager);

////            return string.IsNullOrEmpty(outcome)
////                       ? dir
////                       : dir.ToDirectory(outcome);
////        }

////        private static FileInfo Save(Operation operation,
////                                     bool? success)
////        {
////            Trace.WriteIf(Tracing.Enabled, string.Empty);
////            var file = ItemFile(operation);
////            if (!file.Directory.Exists)
////            {
////                file.Directory.Create();
////            }

////            file.Create(operation.XmlSerialize());
////            if (success.HasValue)
////            {
////                var source = file.FullName;
////                file = ItemDirectory(operation, success.Value ? "Commit" : "Rollback").ToFile(file.Name);
////                if (!file.Directory.Exists)
////                {
////                    file.Directory.Create();
////                }

////                File.Move(source, file.FullName);
////            }

////            return file;
////        }
////    }
////}