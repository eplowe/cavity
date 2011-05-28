namespace Cavity.Configuration
{
    using System;
    using System.IO;
    using System.Reflection;
    using Cavity.Data;
    using Cavity.IO;
    using Xunit;

    public sealed class ConfigFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(typeof(Config).IsStatic());
        }

        [Fact]
        public void op_Xml_Assembly()
        {
            var file = new FileInfo(GetType().Assembly.Location + ".xml");
            try
            {
                var expected = new DataCollection
                {
                    {
                        "foo", "bar"
                        }
                };
                file.Create(expected.XmlSerialize());
                var actual = Config.Xml<DataCollection>(GetType().Assembly);

                Assert.Equal(expected, actual);
            }
            finally
            {
                file.Refresh();
                if (file.Exists)
                {
                    file.Delete();
                }
            }
        }

        [Fact]
        public void op_Xml_AssemblyMissing()
        {
            Assert.Null(Config.Xml<DataCollection>(GetType().Assembly));
        }

        [Fact]
        public void op_Xml_AssemblyNull()
        {
            Assert.Throws<ArgumentNullException>(() => Config.Xml<DataCollection>(null as Assembly));
        }

        [Fact]
        public void op_Xml_FileInfo()
        {
            using (var temp = new TempDirectory())
            {
                var file = temp.Info.ToFile("example.xml");
                var expected = new DataCollection
                {
                    {
                        "foo", "bar"
                        }
                };

                file.Create(expected.XmlSerialize());
                var actual = Config.Xml<DataCollection>(file);

                Assert.Equal(expected, actual);
            }
        }

        [Fact]
        public void op_Xml_FileInfoMissing()
        {
            using (var temp = new TempDirectory())
            {
                Assert.Null(Config.Xml<DataCollection>(temp.Info.ToFile("example.xml")));
            }
        }

        [Fact]
        public void op_Xml_FileInfoNull()
        {
            Assert.Throws<ArgumentNullException>(() => Config.Xml<DataCollection>(null as FileInfo));
        }
    }
}