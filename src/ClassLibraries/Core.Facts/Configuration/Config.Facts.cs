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
        public void op_Clear()
        {
            Config.Clear<DummyConfigurationSection>();
        }

        [Fact]
        public void op_ExeSection()
        {
            Assert.Throws<ArgumentNullException>(() => Config.ExeSection<DummyConfigurationSection>());
        }

        [Fact]
        public void op_ExeSection_Assembly()
        {
            Assert.NotNull(Config.ExeSection<DummyConfigurationSection>(GetType().Assembly));
        }

        [Fact]
        public void op_ExeSection_AssemblyNull()
        {
            Assert.Throws<ArgumentNullException>(() => Config.ExeSection<DummyConfigurationSection>(null));
        }

        [Fact]
        public void op_ExeSection_Assembly_whenNotConfigured()
        {
            Assert.Null(Config.ExeSection<Dummy3ConfigurationSection>(GetType().Assembly));
        }

        [Fact]
        public void op_ExeSection_Assembly_whenSectionGroup()
        {
            Assert.NotNull(Config.ExeSection<Dummy2ConfigurationSection>(GetType().Assembly));
        }

        [Fact]
        public void op_SectionHandler_string()
        {
            Assert.NotNull(Config.SectionHandler<DummyConfigurationSectionHandler>("facts/dummy.handler"));
        }

        [Fact]
        public void op_SectionHandler_stringEmpty()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => Config.SectionHandler<DummyConfigurationSectionHandler>(string.Empty));
        }

        [Fact]
        public void op_SectionHandler_stringMissing()
        {
            Assert.Null(Config.SectionHandler<DummyConfigurationSectionHandler>("missing"));
        }

        [Fact]
        public void op_SectionHandler_stringNull()
        {
            Assert.Throws<ArgumentNullException>(() => Config.SectionHandler<DummyConfigurationSectionHandler>(null));
        }

        [Fact]
        public void op_Section_string()
        {
            Assert.NotNull(Config.Section<DummyConfigurationSection>("dummy"));
        }

        [Fact]
        public void op_Section_stringEmpty()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => Config.Section<DummyConfigurationSection>(string.Empty));
        }

        [Fact]
        public void op_Section_stringNull()
        {
            Assert.Throws<ArgumentNullException>(() => Config.Section<DummyConfigurationSection>(null));
        }

        [Fact]
        public void op_Section_string_whenNotConfigured()
        {
            Assert.Null(Config.Section<DummyConfigurationSection>("missing"));
        }

        [Fact]
        public void op_Set_T_whenConfigurationSection()
        {
            try
            {
                var expected = new DummyConfigurationSection();
                using (var temp = new TempDirectory())
                {
                    Config.Set(expected);
                    Assert.Same(expected, Config.ExeSection<DummyConfigurationSection>());
                    Assert.Same(expected, Config.ExeSection<DummyConfigurationSection>(GetType().Assembly));
                    Assert.Same(expected, Config.Section<DummyConfigurationSection>("example"));
                    Assert.Same(expected, Config.Xml<DummyConfigurationSection>());
                    Assert.Same(expected, Config.Xml<DummyConfigurationSection>(GetType().Assembly));
                    Assert.Same(expected, Config.Xml<DummyConfigurationSection>(temp.Info.ToFile("example.xml")));
                }
            }
            finally
            {
                Config.Clear<DummyConfigurationSection>();
            }
        }

        [Fact]
        public void op_Set_T_whenConfigurationSectionHandler()
        {
            try
            {
                var expected = new DummyConfigurationSectionHandler();
                Config.Set(expected);
                Assert.Same(expected, Config.SectionHandler<DummyConfigurationSectionHandler>("example"));
            }
            finally
            {
                Config.Clear<DummyConfigurationSectionHandler>();
            }
        }

        [Fact]
        public void op_Xml()
        {
            var file = new FileInfo(typeof(DataCollection).Assembly.Location + ".xml");
            try
            {
                var expected = new DataCollection
                {
                    {
                        "foo", "bar"
                        }
                };
                file.Create(expected.XmlSerialize());
                var actual = Config.Xml<DataCollection>();

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
        public void op_Xml_Assembly()
        {
            var file = new FileInfo(typeof(DataCollection).Assembly.Location + ".xml");
            try
            {
                var expected = new DataCollection
                {
                    {
                        "foo", "bar"
                        }
                };
                file.Create(expected.XmlSerialize());
                var actual = Config.Xml<DataCollection>(typeof(DataCollection).Assembly);

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