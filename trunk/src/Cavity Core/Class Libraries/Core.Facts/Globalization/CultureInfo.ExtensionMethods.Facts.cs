namespace Cavity.Globalization
{
    using System;
    using System.Globalization;
    using System.Threading;

    using Xunit;

    public sealed class CultureInfoExtensionMethodsFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(typeof(CultureInfoExtensionMethods).IsStatic());
        }

        [Fact]
        public void op_SetCurrentCulture_CultureInfo()
        {
            var culture = Thread.CurrentThread.CurrentUICulture;

            try
            {
                var expected = new CultureInfo("fr");

                expected.SetCurrentCulture();

                var actual = Thread.CurrentThread.CurrentUICulture;

                Assert.Equal(expected, actual);
            }
            finally
            {
                Thread.CurrentThread.CurrentUICulture = culture;
            }
        }

        [Fact]
        public void op_SetCurrentCulture_CultureInfoInvariantCulture()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => CultureInfo.InvariantCulture.SetCurrentCulture());
        }

        [Fact]
        public void op_SetCurrentCulture_CultureInfoNull()
        {
            Assert.Throws<ArgumentNullException>(() => (null as CultureInfo).SetCurrentCulture());
        }
    }
}