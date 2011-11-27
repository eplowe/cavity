namespace Cavity.IO
{
    using System;
    using System.IO;
    using Cavity.Models;
    using Cavity.Threading;
    using Xunit;

    public sealed class FileReceiverTaskOfTFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<FileReceiverTask<DummyIProcessFile>>()
                            .DerivesFrom<StandardTask>()
                            .IsAbstractBaseClass()
                            .IsNotDecorated()
                            .Result);
        }

        [Fact]
        public void ctor()
        {
            var obj = new DerivedFileReceiverTask();

            Assert.Null(obj.Folder);
            Assert.Equal(SearchOption.AllDirectories, obj.SearchOption);
            Assert.Equal("*.*", obj.SearchPattern);
        }

        [Fact]
        public void ctor_DirectoryInfo()
        {
            using (var temp = new TempDirectory())
            {
                var obj = new DerivedFileReceiverTask(temp.Info);

                Assert.Equal(temp.Info, obj.Folder);
                Assert.Equal(SearchOption.AllDirectories, obj.SearchOption);
                Assert.Equal("*.*", obj.SearchPattern);
            }
        }

        [Fact]
        public void ctor_DirectoryInfoNull_string_SearchOption()
        {
            Assert.NotNull(new DerivedFileReceiverTask(null, "*.txt", SearchOption.TopDirectoryOnly));
        }

        [Fact]
        public void ctor_DirectoryInfo_stringEmpty_SearchOption()
        {
            using (var temp = new TempDirectory())
            {
                Assert.NotNull(new DerivedFileReceiverTask(temp.Info, string.Empty, SearchOption.TopDirectoryOnly));
            }
        }

        [Fact]
        public void ctor_DirectoryInfo_stringNull_SearchOption()
        {
            using (var temp = new TempDirectory())
            {
                Assert.NotNull(new DerivedFileReceiverTask(temp.Info, null, SearchOption.TopDirectoryOnly));
            }
        }

        [Fact]
        public void ctor_DirectoryInfo_string_SearchOption()
        {
            using (var temp = new TempDirectory())
            {
                Assert.NotNull(new DerivedFileReceiverTask(temp.Info, "*.txt", SearchOption.TopDirectoryOnly));
            }
        }

        [Fact]
        public void op_Run()
        {
            using (var temp = new TempDirectory())
            {
                var file = temp.Info.ToFile("example.txt");
                file.CreateNew();
                using (var obj = new DerivedFileReceiverTask(temp.Info, "*.txt", SearchOption.TopDirectoryOnly))
                {
                    obj.Run();

                    file.Refresh();
                    Assert.False(file.Exists);
                }
            }
        }

        [Fact]
        public void op_Run_whenEmptyPatternSpecified()
        {
            using (var temp = new TempDirectory())
            {
                using (var obj = new DerivedFileReceiverTask(temp.Info, string.Empty, SearchOption.TopDirectoryOnly))
                {
                    obj.Run();
                }
            }
        }

        [Fact]
        public void op_Run_whenNoFolderSpecified()
        {
            using (var obj = new DerivedFileReceiverTask(null, "*.csv", SearchOption.TopDirectoryOnly))
            {
                Assert.Throws<InvalidOperationException>(() => obj.Run());
            }
        }

        [Fact]
        public void op_Run_whenNoPatternMatch()
        {
            using (var temp = new TempDirectory())
            {
                var file = temp.Info.ToFile("example.txt");
                file.CreateNew();
                using (var obj = new DerivedFileReceiverTask(temp.Info, "*.csv", SearchOption.TopDirectoryOnly))
                {
                    obj.Run();

                    file.Refresh();
                    Assert.True(file.Exists);
                }
            }
        }

        [Fact]
        public void op_Run_whenNoPatternSpecified()
        {
            using (var temp = new TempDirectory())
            {
                using (var obj = new DerivedFileReceiverTask(temp.Info, null, SearchOption.TopDirectoryOnly))
                {
                    Assert.Throws<ArgumentNullException>(() => obj.Run());
                }
            }
        }

        [Fact]
        public void prop_Data()
        {
            Assert.True(new PropertyExpectations<FileReceiverTask<DummyIProcessFile>>(x => x.Data)
                            .TypeIs<dynamic>()
                            .Result);
        }

        [Fact]
        public void prop_Folder()
        {
            Assert.True(new PropertyExpectations<FileReceiverTask<DummyIProcessFile>>(x => x.Folder)
                            .TypeIs<DirectoryInfo>()
                            .IsNotDecorated()
                            .Result);
        }

        [Fact]
        public void prop_SearchOption()
        {
            Assert.True(new PropertyExpectations<FileReceiverTask<DummyIProcessFile>>(x => x.SearchOption)
                            .TypeIs<SearchOption>()
                            .IsNotDecorated()
                            .Result);
        }

        [Fact]
        public void prop_SearchPattern()
        {
            Assert.True(new PropertyExpectations<FileReceiverTask<DummyIProcessFile>>(x => x.SearchPattern)
                            .TypeIs<string>()
                            .IsNotDecorated()
                            .Result);
        }
    }
}