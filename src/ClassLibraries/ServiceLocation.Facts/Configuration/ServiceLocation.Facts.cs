namespace Cavity.Configuration
{
    using System.Configuration;
    using System.Xml;
    using Cavity;
    using Xunit;

    public sealed class ServiceLocationFacts
    {
        [Fact]
        public void type_definition()
        {
            Assert.True(new TypeExpectations<ServiceLocation>()
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
            Assert.NotNull(new ServiceLocation());
        }

        [Fact]
        public void op_Settings()
        {
            Assert.Null(ServiceLocation.Settings());
        }

        [Fact]
        public void op_Settings_string()
        {
            Assert.Null(ServiceLocation.Settings("serviceLocation"));
        }

        [Fact]
        public void op_Create_object_object_XmlNode()
        {
            var xml = new XmlDocument();
            xml.LoadXml("<serviceLocation type='Cavity.Configuration.ISetLocatorProviderDummy, Cavity.ServiceLocation.Facts' />");

            var actual = new ServiceLocation().Create(null, null, xml.DocumentElement) as ISetLocatorProvider;

            Assert.IsAssignableFrom<ISetLocatorProviderDummy>(actual);
        }

        [Fact]
        public void op_Create_object_object_XmlNode_whenAlternativeElementName()
        {
            var xml = new XmlDocument();
            xml.LoadXml("<foo type='Cavity.Configuration.ISetLocatorProviderDummy, Cavity.ServiceLocation.Facts' />");

            var actual = new ServiceLocation().Create(null, null, xml.DocumentElement) as ISetLocatorProvider;

            Assert.IsAssignableFrom<ISetLocatorProviderDummy>(actual);
        }

        [Fact]
        public void op_Create_object_object_XmlNodeNull()
        {
            Assert.Throws<ConfigurationErrorsException>(() => new ServiceLocation().Create(null, null, null));
        }

        [Fact]
        public void op_Create_object_object_XmlNodeEmpty()
        {
            var xml = new XmlDocument();
            xml.LoadXml("<serviceLocation />");

            Assert.Throws<ConfigurationErrorsException>(() => new ServiceLocation().Create(null, null, xml.DocumentElement));
        }

        [Fact]
        public void op_Create_object_object_XmlNode_whenTypeAttributeEmpty()
        {
            var xml = new XmlDocument();
            xml.LoadXml("<serviceLocation type='' />");

            Assert.Throws<ConfigurationErrorsException>(() => new ServiceLocation().Create(null, null, xml.DocumentElement));
        }

        [Fact]
        public void op_Create_object_object_XmlNode_whenTypeAttributeInvalid()
        {
            var xml = new XmlDocument();
            xml.LoadXml("<serviceLocation type='System.DateTime' />");

            Assert.Throws<ConfigurationErrorsException>(() => new ServiceLocation().Create(null, null, xml.DocumentElement));
        }
    }
}