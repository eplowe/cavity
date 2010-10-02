namespace Cavity.Net
{
    using System;
    using System.Globalization;
    using Xunit;

    public sealed class IRequestAcceptLanguageFacts
    {
        [Fact]
        public void IRequestAcceptLanguage_op_AcceptAnyLanguage()
        {
            try
            {
                var value = (new IRequestAcceptLanguageDummy() as IRequestAcceptLanguage).AcceptAnyLanguage();
                Assert.NotNull(value);
            }
            catch (NotSupportedException)
            {
            }
        }

        [Fact]
        public void IRequestAcceptLanguage_op_AcceptLanguage_CultureInfo()
        {
            try
            {
                CultureInfo language = null;
                var value = (new IRequestAcceptLanguageDummy() as IRequestAcceptLanguage).AcceptLanguage(language);
                Assert.NotNull(value);
            }
            catch (NotSupportedException)
            {
            }
        }

        [Fact]
        public void IRequestAcceptLanguage_op_AcceptLanguage_string()
        {
            try
            {
                string language = null;
                var value = (new IRequestAcceptLanguageDummy() as IRequestAcceptLanguage).AcceptLanguage(language);
                Assert.NotNull(value);
            }
            catch (NotSupportedException)
            {
            }
        }

        [Fact]
        public void a_definition()
        {
            Assert.True(typeof(IRequestAcceptLanguage).IsInterface);
        }
    }
}