﻿namespace Cavity.Configuration
{
    using System;
    using Cavity.Data;
    using Cavity.IO;
    using Xunit;

    public sealed class ConfigXmlFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<ConfigXml>()
                            .DerivesFrom<object>()
                            .IsConcreteClass()
                            .IsSealed()
                            .NoDefaultConstructor()
                            .IsNotDecorated()
                            .Result);
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
                var actual = ConfigXml.Load<DataCollection>(file);

                Assert.Equal(expected, actual.Value);
            }
        }

        [Fact]
        public void op_Xml_FileInfoMissing()
        {
            using (var temp = new TempDirectory())
            {
                Assert.NotNull(ConfigXml.Load<DataCollection>(temp.Info.ToFile("example.xml")));
            }
        }

        [Fact]
        public void op_Xml_FileInfoNull()
        {
            Assert.Throws<ArgumentNullException>(() => ConfigXml.Load<DataCollection>(null));
        }

        [Fact]
        public void prop_Changed()
        {
            Assert.True(new PropertyExpectations<ConfigXml>(x => x.Changed)
                            .TypeIs<bool>()
                            .IsNotDecorated()
                            .Result);
        }

        [Fact]
        public void prop_Value()
        {
            Assert.True(new PropertyExpectations<ConfigXml>(x => x.Value)
                            .TypeIs<object>()
                            .IsNotDecorated()
                            .Result);
        }
    }
}