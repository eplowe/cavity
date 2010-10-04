namespace Cavity.Net
{
    using System;
    using System.Globalization;
    using Xunit;

    public sealed class IResponseContentLanguageFacts
    {
        [Fact]
        public void IResponseContentLanguage_op_HasContentLanguage_CultureInfo()
        {
            try
            {
                CultureInfo language = null;
                var value = (new IResponseContentLanguageDummy() as IResponseContentLanguage).HasContentLanguage(language);
                Assert.NotNull(value);
            }
            catch (NotSupportedException)
            {
            }
        }

        [Fact]
        public void IResponseContentLanguage_op_HasContentLanguage_string()
        {
            try
            {
                string language = null;
                var value = (new IResponseContentLanguageDummy() as IResponseContentLanguage).HasContentLanguage(language);
                Assert.NotNull(value);
            }
            catch (NotSupportedException)
            {
            }
        }

        [Fact]
        public void IResponseContentLanguage_op_IgnoreContentLanguage()
        {
            try
            {
                var value = (new IResponseContentLanguageDummy() as IResponseContentLanguage).IgnoreContentLanguage();
                Assert.NotNull(value);
            }
            catch (NotSupportedException)
            {
            }
        }

        [Fact]
        public void a_definition()
        {
            Assert.True(typeof(IResponseContentLanguage).IsInterface);
        }
    }
}