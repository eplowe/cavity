namespace Cavity.Configuration
{
    using System.Collections.Generic;
    using System.Configuration;
    using System.IO;
    using System.Xml;
    using Xunit;

    public sealed class ConfiguredDirectoryFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<ConfiguredDirectory>()
                            .DerivesFrom<object>()
                            .IsConcreteClass()
                            .IsSealed()
                            .HasDefaultConstructor()
                            .IsNotDecorated()
                            .Implements<IConfigurationSectionHandler>()
                            .Result);
        }

        [Fact]
        public void ctor()
        {
            Assert.NotNull(new ConfiguredDirectory());
        }

        [Fact]
        public void op_Create_object_object_XmlNode()
        {
            var xml = new XmlDocument();
            xml.LoadXml(@"<directories><directory name='Example'>C:\Example</directory></directories>");

            var actual = new ConfiguredDirectory().Create(null, null, xml.DocumentElement) as IDictionary<string, DirectoryInfo>;

            Assert.Equal(@"C:\Example", actual["Example"].FullName);
        }

        [Fact]
        public void op_Create_object_object_XmlNodeEmpty()
        {
            var xml = new XmlDocument();
            xml.LoadXml("<directories />");

            var actual = new ConfiguredDirectory().Create(null, null, xml.DocumentElement) as IDictionary<string, DirectoryInfo>;

            Assert.Empty(actual);
        }

        [Fact]
        public void op_Create_object_object_XmlNodeNull()
        {
            Assert.Throws<ConfigurationErrorsException>(() => new ConfiguredDirectory().Create(null, null, null));
        }

        [Fact]
        public void op_Create_object_object_XmlNode_whenAlternativeElementName()
        {
            var xml = new XmlDocument();
            xml.LoadXml("<foo />");

            var actual = new ConfiguredDirectory().Create(null, null, xml.DocumentElement);

            Assert.IsAssignableFrom<IDictionary<string, DirectoryInfo>>(actual);
        }

        [Fact]
        public void op_Item_string()
        {
            var actual = ConfiguredDirectory.Item("Temp");

            Assert.Equal(@"C:\Temp", actual.FullName);
        }

        [Fact]
        public void op_Settings()
        {
            Assert.NotNull(ConfiguredDirectory.Settings());
        }

        [Fact]
        public void op_Settings_string()
        {
            Assert.NotNull(ConfiguredDirectory.Settings("directories"));
        }

        [Fact]
        public void prop_Mock()
        {
            try
            {
                var mock = new Dictionary<string, DirectoryInfo>
                {
                    {
                        "Windows", new DirectoryInfo(@"C:\Windows")
                        }
                };
                ConfiguredDirectory.Mock = mock;

                Assert.Equal(@"C:\Windows", ConfiguredDirectory.Item("Windows").FullName);
            }
            finally
            {
                ConfiguredDirectory.Mock = null;
            }
        }
    }
}