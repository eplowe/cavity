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
                .DerivesFrom<object>()
                .IsConcreteClass()
                .IsSealed()
                .HasDefaultConstructor()
                .IsNotDecorated()
                .Implements<IComparable>()
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
        public void opImplicit_stringNull_UserAgent()
        {
            string expected = null;
            string actual = null as UserAgent;

            Assert.Equal<string>(expected, actual);
        }

        [Fact]
        public void opImplicit_stringEmpty_UserAgent()
        {
            string expected = string.Empty;
            string actual = new UserAgent(expected);

            Assert.Equal<string>(expected, actual);
        }

        [Fact]
        public void opImplicit_string_UserAgent()
        {
            string expected = "value";
            string actual = new UserAgent(expected);

            Assert.Equal<string>(expected, actual);
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
        public void opEquality_UserAgent_UserAgent_whenTrue()
        {
            UserAgent operand1 = new UserAgent();
            UserAgent operand2 = new UserAgent();

            Assert.True(operand1 == operand2);
        }

        [Fact]
        public void opEquality_UserAgent_UserAgent_whenFalse()
        {
            UserAgent operand1 = new UserAgent("foo");
            UserAgent operand2 = new UserAgent("bar");

            Assert.False(operand1 == operand2);
        }

        [Fact]
        public void opEquality_UserAgent_UserAgent_whenSame()
        {
            UserAgent operand1 = new UserAgent();
            UserAgent operand2 = operand1;

            Assert.True(operand1 == operand2);
        }

        [Fact]
        public void opEquality_UserAgentNull_UserAgent()
        {
            UserAgent operand1 = null;
            UserAgent operand2 = new UserAgent();

            Assert.False(operand1 == operand2);
        }

        [Fact]
        public void opEquality_UserAgent_UserAgentNull()
        {
            UserAgent operand1 = new UserAgent();
            UserAgent operand2 = null;

            Assert.False(operand1 == operand2);
        }

        [Fact]
        public void opInequality_UserAgent_UserAgent_whenTrue()
        {
            UserAgent operand1 = new UserAgent("foo");
            UserAgent operand2 = new UserAgent("bar");

            Assert.True(operand1 != operand2);
        }

        [Fact]
        public void opInequality_UserAgent_UserAgent_whenFalse()
        {
            UserAgent operand1 = new UserAgent();
            UserAgent operand2 = new UserAgent();

            Assert.False(operand1 != operand2);
        }

        [Fact]
        public void opInequality_UserAgent_UserAgent_whenSame()
        {
            UserAgent operand1 = new UserAgent();
            UserAgent operand2 = operand1;

            Assert.False(operand1 != operand2);
        }

        [Fact]
        public void opInequality_UserAgentNull_UserAgent()
        {
            UserAgent operand1 = null;
            UserAgent operand2 = new UserAgent();

            Assert.True(operand1 != operand2);
        }

        [Fact]
        public void opInequality_UserAgent_UserAgentNull()
        {
            UserAgent operand1 = new UserAgent();
            UserAgent operand2 = null;

            Assert.True(operand1 != operand2);
        }

        [Fact]
        public void opLesser_UserAgent_UserAgent_whenSame()
        {
            UserAgent operand1 = new UserAgent();
            UserAgent operand2 = operand1;

            Assert.False(operand1 < operand2);
        }

        [Fact]
        public void opLesser_UserAgent_UserAgent_whenTrue()
        {
            UserAgent operand1 = new UserAgent("bar");
            UserAgent operand2 = new UserAgent("foo");

            Assert.True(operand1 < operand2);
        }

        [Fact]
        public void opLesser_UserAgent_UserAgent_whenFalse()
        {
            UserAgent operand1 = new UserAgent("foo");
            UserAgent operand2 = new UserAgent("bar");

            Assert.False(operand1 < operand2);
        }

        [Fact]
        public void opLesser_UserAgentNull_UserAgent()
        {
            UserAgent operand1 = null;
            UserAgent operand2 = new UserAgent();

            Assert.True(operand1 < operand2);
        }

        [Fact]
        public void opLesser_UserAgent_UserAgentNull()
        {
            UserAgent operand1 = new UserAgent();
            UserAgent operand2 = null;

            Assert.False(operand1 < operand2);
        }

        [Fact]
        public void opGreater_UserAgent_UserAgent_whenSame()
        {
            UserAgent operand1 = new UserAgent();
            UserAgent operand2 = operand1;

            Assert.False(operand1 > operand2);
        }

        [Fact]
        public void opGreater_UserAgent_UserAgent_whenTrue()
        {
            UserAgent operand1 = new UserAgent("foo");
            UserAgent operand2 = new UserAgent("bar");

            Assert.True(operand1 > operand2);
        }

        [Fact]
        public void opGreater_UserAgent_UserAgent_whenFalse()
        {
            UserAgent operand1 = new UserAgent("bar");
            UserAgent operand2 = new UserAgent("foo");

            Assert.False(operand1 > operand2);
        }

        [Fact]
        public void opGreater_UserAgentNull_UserAgent()
        {
            UserAgent operand1 = null;
            UserAgent operand2 = new UserAgent();

            Assert.False(operand1 > operand2);
        }

        [Fact]
        public void opGreater_UserAgent_UserAgentNull()
        {
            UserAgent operand1 = new UserAgent();
            UserAgent operand2 = null;

            Assert.True(operand1 > operand2);
        }

        [Fact]
        public void op_Compare_UserAgent_UserAgent_Equal()
        {
            UserAgent comparand1 = new UserAgent();
            UserAgent comparand2 = new UserAgent();

            Assert.Equal<int>(0, UserAgent.Compare(comparand1, comparand2));
        }

        [Fact]
        public void op_Compare_UserAgent_UserAgent_whenSame()
        {
            UserAgent comparand1 = new UserAgent();
            UserAgent comparand2 = comparand1;

            Assert.Equal<int>(0, UserAgent.Compare(comparand1, comparand2));
        }

        [Fact]
        public void op_Compare_UserAgentNull_UserAgent()
        {
            UserAgent comparand1 = null;
            UserAgent comparand2 = new UserAgent();

            Assert.True(UserAgent.Compare(comparand1, comparand2) < 0);
        }

        [Fact]
        public void op_Compare_UserAgent_UserAgentNull()
        {
            UserAgent comparand1 = new UserAgent();
            UserAgent comparand2 = null;

            Assert.True(UserAgent.Compare(comparand1, comparand2) > 0);
        }

        [Fact]
        public void op_Compare_UserAgentGreater_UserAgent()
        {
            UserAgent comparand1 = new UserAgent("foo");
            UserAgent comparand2 = new UserAgent("bar");

            Assert.True(UserAgent.Compare(comparand1, comparand2) > 0);
        }

        [Fact]
        public void op_Compare_UserAgentLesser_UserAgent()
        {
            UserAgent comparand1 = new UserAgent("bar");
            UserAgent comparand2 = new UserAgent("foo");

            Assert.True(UserAgent.Compare(comparand1, comparand2) < 0);
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
        public void op_CompareTo_objectNull()
        {
            Assert.True(new UserAgent().CompareTo(null) > 0);
        }

        [Fact]
        public void op_CompareTo_objectSame()
        {
            UserAgent value = new UserAgent("value");

            Assert.Equal<int>(0, value.CompareTo(value));
        }

        [Fact]
        public void op_CompareTo_object()
        {
            UserAgent left = new UserAgent("value");
            UserAgent right = new UserAgent("value");

            Assert.Equal<int>(0, left.CompareTo(right));
        }

        [Fact]
        public void op_CompareTo_objectLesser()
        {
            UserAgent left = new UserAgent("bar");
            UserAgent right = new UserAgent("foo");

            Assert.True(left.CompareTo(right) < 0);
        }

        [Fact]
        public void op_CompareTo_objectGreater()
        {
            UserAgent left = new UserAgent("foo");
            UserAgent right = new UserAgent("bar");

            Assert.True(left.CompareTo(right) > 0);
        }

        [Fact]
        public void op_CompareTo_objectInt32()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new UserAgent().CompareTo(123));
        }

        [Fact]
        public void op_Equals_object()
        {
            UserAgent obj = new UserAgent();

            Assert.True(new UserAgent().Equals(obj));
        }

        [Fact]
        public void op_Equals_object_this()
        {
            UserAgent obj = new UserAgent();

            Assert.True(obj.Equals(obj));
        }

        [Fact]
        public void op_Equals_objectNull()
        {
            Assert.False(new UserAgent().Equals(null));
        }

        [Fact]
        public void op_Equals_objectString()
        {
            Assert.False(new UserAgent("value").Equals("value"));
        }

        [Fact]
        public void op_GetHashCode()
        {
            UserAgent obj = new UserAgent("value");

            int expected = obj.ToString().GetHashCode();
            int actual = obj.GetHashCode();

            Assert.Equal<int>(expected, actual);
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