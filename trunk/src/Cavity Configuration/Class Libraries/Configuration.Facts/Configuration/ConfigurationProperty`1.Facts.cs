namespace Cavity.Configuration
{
    using System;
    using System.Configuration;
    using Cavity;
    using Xunit;

    public sealed class ConfigurationPropertyOfTFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(typeof(ConfigurationProperty<AbsoluteUri>).IsStatic());
        }

        [Fact]
        public void op_Item_string()
        {
            var actual = ConfigurationProperty<AbsoluteUri>.Item("example");

            Assert.Equal("example", actual.Name);
            Assert.Equal(typeof(AbsoluteUri), actual.Type);
            Assert.Null(actual.DefaultValue);
            Assert.IsType<ConfigurationConverter<AbsoluteUri>>(actual.Converter);
            Assert.IsType<ConfigurationValidator<AbsoluteUri>>(actual.Validator);
            Assert.True(actual.IsRequired);
        }

        [Fact]
        public void op_Item_stringEmpty()
        {
            Assert.Throws<ArgumentException>(() => ConfigurationProperty<AbsoluteUri>.Item(string.Empty));
        }

        [Fact]
        public void op_Item_stringNull()
        {
            Assert.Throws<ArgumentException>(() => ConfigurationProperty<AbsoluteUri>.Item(null));
        }

        [Fact]
        public void op_Item_string_ConfigurationPropertyOptions()
        {
            var actual = ConfigurationProperty<AbsoluteUri>.Item("example", ConfigurationPropertyOptions.IsKey);

            Assert.Equal("example", actual.Name);
            Assert.Equal(typeof(AbsoluteUri), actual.Type);
            Assert.Null(actual.DefaultValue);
            Assert.IsType<ConfigurationConverter<AbsoluteUri>>(actual.Converter);
            Assert.IsType<ConfigurationValidator<AbsoluteUri>>(actual.Validator);
            Assert.False(actual.IsRequired);
            Assert.True(actual.IsKey);
        }

        [Fact]
        public void op_Item_stringEmpty_ConfigurationPropertyOptions()
        {
            Assert.Throws<ArgumentException>(() => ConfigurationProperty<AbsoluteUri>.Item(string.Empty, ConfigurationPropertyOptions.IsKey));
        }

        [Fact]
        public void op_Item_stringNull_ConfigurationPropertyOptions()
        {
            Assert.Throws<ArgumentException>(() => ConfigurationProperty<AbsoluteUri>.Item(null, ConfigurationPropertyOptions.IsKey));
        }

        [Fact]
        public void op_Item_string_T()
        {
            var actual = ConfigurationProperty<AbsoluteUri>.Item("example", new AbsoluteUri("http://example.com/"));

            Assert.Equal("example", actual.Name);
            Assert.Equal(typeof(AbsoluteUri), actual.Type);
            Assert.Equal(new AbsoluteUri("http://example.com/"), actual.DefaultValue);
            Assert.IsType<ConfigurationConverter<AbsoluteUri>>(actual.Converter);
            Assert.IsType<ConfigurationValidator<AbsoluteUri>>(actual.Validator);
            Assert.True(actual.IsRequired);
        }

        [Fact]
        public void op_Item_stringEmpty_T()
        {
            Assert.Throws<ArgumentException>(() => ConfigurationProperty<AbsoluteUri>.Item(string.Empty, new AbsoluteUri("http://example.com/")));
        }

        [Fact]
        public void op_Item_stringNull_T()
        {
            Assert.Throws<ArgumentException>(() => ConfigurationProperty<AbsoluteUri>.Item(null, new AbsoluteUri("http://example.com/")));
        }

        [Fact]
        public void op_Item_string_T_ConfigurationPropertyOptions()
        {
            var actual = ConfigurationProperty<AbsoluteUri>.Item("example", new AbsoluteUri("http://example.com/"), ConfigurationPropertyOptions.IsKey);

            Assert.Equal("example", actual.Name);
            Assert.Equal(typeof(AbsoluteUri), actual.Type);
            Assert.Equal(new AbsoluteUri("http://example.com/"), actual.DefaultValue);
            Assert.IsType<ConfigurationConverter<AbsoluteUri>>(actual.Converter);
            Assert.IsType<ConfigurationValidator<AbsoluteUri>>(actual.Validator);
            Assert.False(actual.IsRequired);
            Assert.True(actual.IsKey);
        }

        [Fact]
        public void op_Item_stringEmpty_T_ConfigurationPropertyOptions()
        {
            Assert.Throws<ArgumentException>(() => ConfigurationProperty<AbsoluteUri>.Item(string.Empty, new AbsoluteUri("http://example.com/"), ConfigurationPropertyOptions.IsKey));
        }

        [Fact]
        public void op_Item_stringNull_T_ConfigurationPropertyOptions()
        {
            Assert.Throws<ArgumentException>(() => ConfigurationProperty<AbsoluteUri>.Item(null, new AbsoluteUri("http://example.com/"), ConfigurationPropertyOptions.IsKey));
        }
    }
}