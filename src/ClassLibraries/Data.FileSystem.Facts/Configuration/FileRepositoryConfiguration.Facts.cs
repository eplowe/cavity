namespace Cavity.Configuration
{
    using System;
    using System.Configuration;
    using System.IO;
    using System.Xml;
    using Cavity;
    using Cavity.IO;
    using Xunit;

    public sealed class FileRepositoryConfigurationFacts
    {
        [Fact]
        public void type_definition()
        {
            Assert.True(new TypeExpectations<FileRepositoryConfiguration>()
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
            Assert.NotNull(new FileRepositoryConfiguration());
        }

        [Fact]
        public void op_Create_object_object_XmlNode()
        {
            const string expected = @"C:\Temp";

            var xml = new XmlDocument();
            xml.LoadXml("<fileRepository directory='{0}' />".FormatWith(expected));

            var actual = new FileRepositoryConfiguration().Create(null, null, xml.DocumentElement) as DirectoryInfo;

            if (null == actual)
            {
                Assert.NotNull(actual);
            }
            else
            {
                Assert.Equal(actual.FullName, expected);
            }
        }

        [Fact]
        public void op_Create_object_object_XmlNodeEmpty()
        {
            var xml = new XmlDocument();
            xml.LoadXml("<fileRepository />");

            Assert.Throws<ConfigurationErrorsException>(() => new FileRepositoryConfiguration().Create(null, null, xml.DocumentElement));
        }

        [Fact]
        public void op_Create_object_object_XmlNodeNull()
        {
            Assert.Throws<ConfigurationErrorsException>(() => new FileRepositoryConfiguration().Create(null, null, null));
        }

        [Fact]
        public void op_Create_object_object_XmlNode_whenAlternativeElementName()
        {
            const string expected = @"C:\Temp";

            var xml = new XmlDocument();
            xml.LoadXml("<cavity.repository directory='{0}' />".FormatWith(expected));

            var actual = new FileRepositoryConfiguration().Create(null, null, xml.DocumentElement) as DirectoryInfo;

            if (null == actual)
            {
                Assert.NotNull(actual);
            }
            else
            {
                Assert.Equal(actual.FullName, expected);
            }
        }

        [Fact]
        public void op_Create_object_object_XmlNode_whenDirectoryAttributeEmpty()
        {
            var xml = new XmlDocument();
            xml.LoadXml("<cavity.repository directory='' />");

            Assert.Throws<ConfigurationErrorsException>(() => new FileRepositoryConfiguration().Create(null, null, xml.DocumentElement));
        }

        [Fact]
        public void op_Create_object_object_XmlNode_whenDirectoryAttributeInvalid()
        {
            var xml = new XmlDocument();
            xml.LoadXml("<cavity.repository directory='invalid ? character' />");

            Assert.Throws<ConfigurationErrorsException>(() => new FileRepositoryConfiguration().Create(null, null, xml.DocumentElement));
        }

        [Fact]
        public void op_Directory()
        {
            Assert.Null(FileRepositoryConfiguration.Directory());
        }

        [Fact]
        public void op_Directory_string()
        {
            Assert.Null(FileRepositoryConfiguration.Directory("cavity.repository"));
        }

        [Fact]
        public void prop_Mock()
        {
            try
            {
                using (var expected = new TempDirectory())
                {
                    FileRepositoryConfiguration.Mock = expected.Info;
                    var actual = FileRepositoryConfiguration.Directory();

                    Assert.Same(expected.Info, actual);
                }
            }
            finally
            {
                FileRepositoryConfiguration.Mock = null;
            }
        }
    }
}