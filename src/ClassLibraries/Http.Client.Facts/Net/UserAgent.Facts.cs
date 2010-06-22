namespace Cavity.Net
{
    using System;
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
                .DerivesFrom<ComparableObject>()
                .IsConcreteClass()
                .IsSealed()
                .HasDefaultConstructor()
                .IsNotDecorated()
                .Implements<IUserAgent>()
                .Result);
        }

        [Fact]
        public void ctor()
        {
            Assert.NotNull(new UserAgent());
        }

        [Fact]
        public void ctor_stringNull()
        {
            Assert.Throws<ArgumentNullException>(() => new UserAgent(null as string));
        }

        [Fact]
        public void ctor_stringEmpty()
        {
            Assert.NotNull(new UserAgent(string.Empty));
        }

        [Fact]
        public void ctor_string()
        {
            Assert.NotNull(new UserAgent("value"));
        }

        [Fact]
        public void prop_Value()
        {
            Assert.NotNull(new PropertyExpectations<UserAgent>("Value")
                .TypeIs<string>()
                .DefaultValueIs(UserAgent.Format())
                .ArgumentNullException()
                .Set("value")
                .IsNotDecorated()
                .Result);
        }

        [Fact]
        public void opImplicit_UserAgent_stringNull()
        {
            UserAgent obj = null as string;

            Assert.Null(obj);
        }

        [Fact]
        public void opImplicit_UserAgent_stringEmpty()
        {
            UserAgent expected = string.Empty;
            UserAgent actual = new UserAgent(string.Empty);

            Assert.Equal<UserAgent>(expected, actual);
        }

        [Fact]
        public void opImplicit_UserAgent_string()
        {
            UserAgent expected = "value";
            UserAgent actual = new UserAgent("value");

            Assert.Equal<UserAgent>(expected, actual);
        }

        [Fact]
        public void op_Format()
        {
            string expected = string.Format(
                CultureInfo.CurrentUICulture,
                "CavityHttpClient/{0}.{1} (+http://code.google.com/p/cavity/)",
                Assembly.GetExecutingAssembly().GetName().Version.Major,
                Assembly.GetExecutingAssembly().GetName().Version.Minor);
            string actual = UserAgent.Format();

            Assert.Equal<string>(expected, actual);
        }

        [Fact]
        public void op_Format_int_int()
        {
            string expected = "CavityHttpClient/1.0 (+http://code.google.com/p/cavity/)";
            string actual = UserAgent.Format(1, 0);

            Assert.Equal<string>(expected, actual);
        }

        [Fact]
        public void op_Parse_stringNull()
        {
            Assert.Throws<ArgumentNullException>(() => UserAgent.Parse(null as string));
        }

        [Fact]
        public void op_Parse_stringEmpty()
        {
            UserAgent expected = new UserAgent(string.Empty);
            UserAgent actual = UserAgent.Parse(string.Empty);

            Assert.Equal<UserAgent>(expected, actual);
        }

        [Fact]
        public void op_Parse_string()
        {
            UserAgent expected = new UserAgent("value");
            UserAgent actual = UserAgent.Parse("value");

            Assert.Equal<UserAgent>(expected, actual);
        }

        [Fact]
        public void op_ToString()
        {
            string expected = "value";
            string actual = new UserAgent(expected).ToString();

            Assert.Equal<string>(expected, actual);
        }
    }
}