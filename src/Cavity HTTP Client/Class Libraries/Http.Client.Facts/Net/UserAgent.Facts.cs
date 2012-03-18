namespace Cavity.Net
{
    using System;
    using System.Reflection;

    using Xunit;

    public sealed class UserAgentFacts
    {
        [Fact]
        public void a_definition()
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
        public void ctor_string()
        {
            Assert.NotNull(new UserAgent("value"));
        }

        [Fact]
        public void ctor_stringEmpty()
        {
            Assert.NotNull(new UserAgent(string.Empty));
        }

        [Fact]
        public void ctor_stringNull()
        {
            Assert.Throws<ArgumentNullException>(() => new UserAgent(null));
        }

        [Fact]
        public void opImplicit_UserAgent_string()
        {
            var expected = new UserAgent("value");
            UserAgent actual = "value";

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void opImplicit_UserAgent_stringEmpty()
        {
            var expected = new UserAgent(string.Empty);
            UserAgent actual = string.Empty;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void opImplicit_UserAgent_stringNull()
        {
            Assert.Null((UserAgent)null);
        }

        [Fact]
        public void op_Format()
        {
            var expected = "CavityHttpClient/{0}.{1} (+http://code.google.com/p/cavity/)".FormatWith(
                Assembly.GetExecutingAssembly().GetName().Version.Major, 
                Assembly.GetExecutingAssembly().GetName().Version.Minor);
            var actual = UserAgent.Format();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_Format_int_int()
        {
            const string expected = "CavityHttpClient/1.0 (+http://code.google.com/p/cavity/)";
            var actual = UserAgent.Format(1, 0);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_FromString_string()
        {
            var expected = new UserAgent("value");
            var actual = UserAgent.FromString("value");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_FromString_stringEmpty()
        {
            var expected = new UserAgent(string.Empty);
            var actual = UserAgent.FromString(string.Empty);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_FromString_stringNull()
        {
            Assert.Throws<ArgumentNullException>(() => UserAgent.FromString(null));
        }

        [Fact]
        public void op_ToString()
        {
            const string expected = "value";
            var actual = new UserAgent(expected).ToString();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void prop_Value()
        {
            Assert.True(new PropertyExpectations<UserAgent>(p => p.Value)
                            .TypeIs<string>()
                            .DefaultValueIs(UserAgent.Format())
                            .ArgumentNullException()
                            .Set("value")
                            .IsNotDecorated()
                            .Result);
        }
    }
}