namespace Cavity.Net
{
    using System.Globalization;
    using System.Reflection;
    using Cavity;
    using Xunit;

    public sealed class UserAgentFacts
    {
        [Fact]
        public void type_definition()
        {
            Assert.True(new TypeExpectations<UserAgent>()
                .DerivesFrom<object>()
                .IsConcreteClass()
                .IsSealed()
                .HasDefaultConstructor()
                .IsNotDecorated()
                .Result);
        }

        [Fact]
        public void ctor()
        {
            Assert.NotNull(new UserAgent());
        }

        [Fact]
        public void op_DefaultValue()
        {
            string expected = string.Format(
                CultureInfo.CurrentUICulture,
                "CavityHttpClient/{0}.{1} (+http://code.google.com/p/cavity/)",
                Assembly.GetExecutingAssembly().GetName().Version.Major,
                Assembly.GetExecutingAssembly().GetName().Version.Minor);
            string actual = UserAgent.DefaultValue();

            Assert.Equal<string>(expected, actual);
        }

        [Fact]
        public void op_DefaultValue_int_int()
        {
            string expected = "CavityHttpClient/1.0 (+http://code.google.com/p/cavity/)";
            string actual = UserAgent.DefaultValue(1, 0);

            Assert.Equal<string>(expected, actual);
        }

        [Fact]
        public void prop_Value()
        {
            Assert.NotNull(new PropertyExpectations<UserAgent>("Value")
                .TypeIs<string>()
                .DefaultValueIs(UserAgent.DefaultValue())
                .IsNotDecorated()
                .Result);
        }

        [Fact]
        public void op_ToString()
        {
            string expected = UserAgent.DefaultValue();
            string actual = new UserAgent().ToString();

            Assert.Equal<string>(expected, actual);
        }
    }
}