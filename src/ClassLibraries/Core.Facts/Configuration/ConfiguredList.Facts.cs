namespace Cavity.Configuration
{
    using System.Collections.Generic;
    using System.Configuration;
    using System.Linq;
    using System.Xml;
    using Cavity.Collections.Generic;
    using Xunit;

    public sealed class ConfiguredListFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<ConfiguredList>()
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
            Assert.NotNull(new ConfiguredList());
        }

        [Fact]
        public void op_Create_object_object_XmlNode()
        {
            var xml = new XmlDocument();
            xml.LoadXml("<lists><list name='Example'><item>Foo</item><item>Bar</item></list></lists>");

            var actual = new ConfiguredList().Create(null, null, xml.DocumentElement) as MultitonCollection<string, IEnumerable<string>>;

            Assert.Equal("Foo", actual["Example"].First());
            Assert.Equal("Bar", actual["Example"].Last());
        }

        [Fact]
        public void op_Create_object_object_XmlNodeEmpty()
        {
            var xml = new XmlDocument();
            xml.LoadXml("<lists />");

            var actual = new ConfiguredList().Create(null, null, xml.DocumentElement) as MultitonCollection<string, IEnumerable<string>>;

            Assert.Empty(actual);
        }

        [Fact]
        public void op_Create_object_object_XmlNodeNull()
        {
            Assert.Throws<ConfigurationErrorsException>(() => new ConfiguredList().Create(null, null, null));
        }

        [Fact]
        public void op_Create_object_object_XmlNode_whenAlternativeElementName()
        {
            var xml = new XmlDocument();
            xml.LoadXml("<foo />");

            var actual = new ConfiguredList().Create(null, null, xml.DocumentElement);

            Assert.IsAssignableFrom<MultitonCollection<string, IEnumerable<string>>>(actual);
        }

        [Fact]
        public void op_Items_string()
        {
            var actual = ConfiguredList.Items("Example");

            Assert.Equal("Foo", actual.First());
            Assert.Equal("Bar", actual.Last());
        }

        [Fact]
        public void op_Settings()
        {
            Assert.NotNull(ConfiguredList.Settings());
        }

        [Fact]
        public void op_Settings_string()
        {
            Assert.NotNull(ConfiguredList.Settings("lists"));
        }

        [Fact]
        public void prop_Mock()
        {
            try
            {
                ConfiguredList.Mock["Example"] = new List<string>
                {
                    "One"
                };

                Assert.Equal("One", ConfiguredList.Items("Example").First());
            }
            finally
            {
                ConfiguredList.Mock = null;
            }
        }
    }
}