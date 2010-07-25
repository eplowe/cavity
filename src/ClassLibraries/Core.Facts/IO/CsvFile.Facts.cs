﻿namespace Cavity.IO
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.IO;
    using Xunit;

    public sealed class CsvFileFacts
    {
        [Fact]
        public void ctor_FileInfo()
        {
            Assert.NotNull(new CsvFile(new FileInfo("test.csv")));
        }

        [Fact]
        public void ctor_FileInfoNull()
        {
            Assert.Throws<ArgumentNullException>(() => new CsvFile(null));
        }

        [Fact]
        public void op_GetEnumerator()
        {
            var file = new FileInfo(Path.GetTempFileName());
            try
            {
                using (var stream = file.Open(FileMode.Append, FileAccess.Write, FileShare.None))
                {
                    using (var writer = new StreamWriter(stream))
                    {
                        writer.WriteLine("name");
                        writer.WriteLine("value");
                    }
                }

                foreach (var item in new CsvFile(file))
                {
                    Assert.Equal("value", item["name"]);
                }
            }
            finally
            {
                if (file.Exists)
                {
                    file.Delete();
                }
            }
        }

        [Fact]
        public void op_IEnumerable_GetEnumerator()
        {
            var file = new FileInfo(Path.GetTempFileName());
            try
            {
                using (var stream = file.Open(FileMode.Append, FileAccess.Write, FileShare.None))
                {
                    using (var writer = new StreamWriter(stream))
                    {
                        writer.WriteLine("name");
                        writer.WriteLine("value");
                    }
                }

                foreach (var item in new CsvFile(file) as IEnumerable)
                {
                    var entry = (IDictionary<string, string>)item;
                    Assert.Equal("value", entry["name"]);
                }
            }
            finally
            {
                if (file.Exists)
                {
                    file.Delete();
                }
            }
        }

        [Fact]
        public void prop_Info()
        {
            Assert.NotNull(new PropertyExpectations<CsvFile>("Info")
                               .TypeIs<FileInfo>()
                               .IsNotDecorated()
                               .Result);
        }

        [Fact]
        public void type_definition()
        {
            Assert.True(new TypeExpectations<CsvFile>()
                            .DerivesFrom<object>()
                            .IsConcreteClass()
                            .IsSealed()
                            .NoDefaultConstructor()
                            .IsNotDecorated()
                            .Implements<IEnumerable<IDictionary<string, string>>>()
                            .Result);
        }
    }
}