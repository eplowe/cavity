namespace Cavity.Net
{
    using System;
    using Cavity;
    using Xunit;

    public sealed class HttpVersionFacts
    {
        [Fact]
        public void type_definition()
        {
            Assert.True(new TypeExpectations<HttpVersion>()
                .DerivesFrom<object>()
                .IsConcreteClass()
                .IsSealed()
                .NoDefaultConstructor()
                .IsNotDecorated()
                .Implements<IComparable>()
                .Result);
        }

        [Fact]
        public void ctor_int_int()
        {
            Assert.NotNull(new HttpVersion(1, 0));
        }

        [Fact]
        public void prop_Major()
        {
            Assert.True(new PropertyExpectations<HttpVersion>("Major")
                .TypeIs<int>()
                .ArgumentOutOfRangeException(-1)
                .Set(0)
                .Set(1)
                .Set(2)
                .Set(3)
                .Set(4)
                .Set(5)
                .Set(6)
                .Set(7)
                .Set(8)
                .Set(9)
                .ArgumentOutOfRangeException(10)
                .IsNotDecorated()
                .Result);
        }

        [Fact]
        public void prop_Minor()
        {
            Assert.True(new PropertyExpectations<HttpVersion>("Minor")
                .TypeIs<int>()
                .ArgumentOutOfRangeException(-1)
                .Set(0)
                .Set(1)
                .Set(2)
                .Set(3)
                .Set(4)
                .Set(5)
                .Set(6)
                .Set(7)
                .Set(8)
                .Set(9)
                .ArgumentOutOfRangeException(10)
                .IsNotDecorated()
                .Result);
        }

        [Fact]
        public void opImplicit_stringNull_HttpVersion()
        {
            string expected = null;
            string actual = null as HttpVersion;

            Assert.Equal<string>(expected, actual);
        }

        [Fact]
        public void opImplicit_string_HttpVersion()
        {
            string expected = "HTTP/1.0";
            string actual = new HttpVersion(1, 0);

            Assert.Equal<string>(expected, actual);
        }

        [Fact]
        public void opImplicit_HttpVersion_stringNull()
        {
            HttpVersion obj = null as string;

            Assert.Null(obj);
        }

        [Fact]
        public void opImplicit_HttpVersion_stringEmpty()
        {
            HttpVersion expected;

            Assert.Throws<FormatException>(() => expected = string.Empty);
        }

        [Fact]
        public void opImplicit_HttpVersion_string()
        {
            HttpVersion expected = "HTTP/1.0";
            HttpVersion actual = new HttpVersion(1, 0);

            Assert.Equal<HttpVersion>(expected, actual);
        }

        [Fact]
        public void opEquality_HttpVersion_HttpVersion_whenTrue()
        {
            HttpVersion operand1 = new HttpVersion(1, 0);
            HttpVersion operand2 = new HttpVersion(1, 0);

            Assert.True(operand1 == operand2);
        }

        [Fact]
        public void opEquality_HttpVersion_HttpVersion_whenFalse()
        {
            HttpVersion operand1 = new HttpVersion(1, 1);
            HttpVersion operand2 = new HttpVersion(1, 0);

            Assert.False(operand1 == operand2);
        }

        [Fact]
        public void opEquality_HttpVersion_HttpVersion_whenSame()
        {
            HttpVersion operand1 = new HttpVersion(1, 0);
            HttpVersion operand2 = operand1;

            Assert.True(operand1 == operand2);
        }

        [Fact]
        public void opEquality_HttpVersionNull_HttpVersion()
        {
            HttpVersion operand1 = null;
            HttpVersion operand2 = new HttpVersion(1, 0);

            Assert.False(operand1 == operand2);
        }

        [Fact]
        public void opEquality_HttpVersion_HttpVersionNull()
        {
            HttpVersion operand1 = new HttpVersion(1, 0);
            HttpVersion operand2 = null;

            Assert.False(operand1 == operand2);
        }

        [Fact]
        public void opInequality_HttpVersion_HttpVersion_whenTrue()
        {
            HttpVersion operand1 = new HttpVersion(1, 1);
            HttpVersion operand2 = new HttpVersion(1, 0);

            Assert.True(operand1 != operand2);
        }

        [Fact]
        public void opInequality_HttpVersion_HttpVersion_whenFalse()
        {
            HttpVersion operand1 = new HttpVersion(1, 0);
            HttpVersion operand2 = new HttpVersion(1, 0);

            Assert.False(operand1 != operand2);
        }

        [Fact]
        public void opInequality_HttpVersion_HttpVersion_whenSame()
        {
            HttpVersion operand1 = new HttpVersion(1, 0);
            HttpVersion operand2 = operand1;

            Assert.False(operand1 != operand2);
        }

        [Fact]
        public void opInequality_HttpVersionNull_HttpVersion()
        {
            HttpVersion operand1 = null;
            HttpVersion operand2 = new HttpVersion(1, 0);

            Assert.True(operand1 != operand2);
        }

        [Fact]
        public void opInequality_HttpVersion_HttpVersionNull()
        {
            HttpVersion operand1 = new HttpVersion(1, 0);
            HttpVersion operand2 = null;

            Assert.True(operand1 != operand2);
        }

        [Fact]
        public void opLesser_HttpVersion_HttpVersion_whenSame()
        {
            HttpVersion operand1 = new HttpVersion(1, 0);
            HttpVersion operand2 = operand1;

            Assert.False(operand1 < operand2);
        }

        [Fact]
        public void opLesser_HttpVersion_HttpVersion_whenTrue()
        {
            HttpVersion operand1 = new HttpVersion(1, 0);
            HttpVersion operand2 = new HttpVersion(1, 1);

            Assert.True(operand1 < operand2);
        }

        [Fact]
        public void opLesser_HttpVersion_HttpVersion_whenFalse()
        {
            HttpVersion operand1 = new HttpVersion(1, 1);
            HttpVersion operand2 = new HttpVersion(1, 0);

            Assert.False(operand1 < operand2);
        }

        [Fact]
        public void opLesser_HttpVersionNull_HttpVersion()
        {
            HttpVersion operand1 = null;
            HttpVersion operand2 = new HttpVersion(1, 0);

            Assert.True(operand1 < operand2);
        }

        [Fact]
        public void opLesser_HttpVersion_HttpVersionNull()
        {
            HttpVersion operand1 = new HttpVersion(1, 0);
            HttpVersion operand2 = null;

            Assert.False(operand1 < operand2);
        }

        [Fact]
        public void opGreater_HttpVersion_HttpVersion_whenSame()
        {
            HttpVersion operand1 = new HttpVersion(1, 0);
            HttpVersion operand2 = operand1;

            Assert.False(operand1 > operand2);
        }

        [Fact]
        public void opGreater_HttpVersion_HttpVersion_whenTrue()
        {
            HttpVersion operand1 = new HttpVersion(1, 1);
            HttpVersion operand2 = new HttpVersion(1, 0);

            Assert.True(operand1 > operand2);
        }

        [Fact]
        public void opGreater_HttpVersion_HttpVersion_whenFalse()
        {
            HttpVersion operand1 = new HttpVersion(1, 0);
            HttpVersion operand2 = new HttpVersion(1, 1);

            Assert.False(operand1 > operand2);
        }

        [Fact]
        public void opGreater_HttpVersionNull_HttpVersion()
        {
            HttpVersion operand1 = null;
            HttpVersion operand2 = new HttpVersion(1, 0);

            Assert.False(operand1 > operand2);
        }

        [Fact]
        public void opGreater_HttpVersion_HttpVersionNull()
        {
            HttpVersion operand1 = new HttpVersion(1, 0);
            HttpVersion operand2 = null;

            Assert.True(operand1 > operand2);
        }

        [Fact]
        public void op_Compare_HttpVersion_HttpVersion_Equal()
        {
            HttpVersion comparand1 = new HttpVersion(1, 0);
            HttpVersion comparand2 = new HttpVersion(1, 0);

            Assert.Equal<int>(0, HttpVersion.Compare(comparand1, comparand2));
        }

        [Fact]
        public void op_Compare_HttpVersion_HttpVersion_whenSame()
        {
            HttpVersion comparand1 = new HttpVersion(1, 0);
            HttpVersion comparand2 = comparand1;

            Assert.Equal<int>(0, HttpVersion.Compare(comparand1, comparand2));
        }

        [Fact]
        public void op_Compare_HttpVersionNull_HttpVersion()
        {
            HttpVersion comparand1 = null;
            HttpVersion comparand2 = new HttpVersion(1, 0);

            Assert.True(HttpVersion.Compare(comparand1, comparand2) < 0);
        }

        [Fact]
        public void op_Compare_HttpVersion_HttpVersionNull()
        {
            HttpVersion comparand1 = new HttpVersion(1, 0);
            HttpVersion comparand2 = null;

            Assert.True(HttpVersion.Compare(comparand1, comparand2) > 0);
        }

        [Fact]
        public void op_Compare_HttpVersionGreater_HttpVersion()
        {
            HttpVersion comparand1 = new HttpVersion(1, 1);
            HttpVersion comparand2 = new HttpVersion(1, 0);

            Assert.True(HttpVersion.Compare(comparand1, comparand2) > 0);
        }

        [Fact]
        public void op_Compare_HttpVersionLesser_HttpVersion()
        {
            HttpVersion comparand1 = new HttpVersion(1, 0);
            HttpVersion comparand2 = new HttpVersion(1, 1);

            Assert.True(HttpVersion.Compare(comparand1, comparand2) < 0);
        }

        [Fact]
        public void op_Parse_string()
        {
            HttpVersion expected = new HttpVersion(1, 0);
            HttpVersion actual = HttpVersion.Parse("HTTP/1.0");

            Assert.Equal<HttpVersion>(expected, actual);
        }

        [Fact]
        public void op_Parse_stringNull()
        {
            Assert.Throws<ArgumentNullException>(() => HttpVersion.Parse(null as string));
        }

        [Fact]
        public void op_Parse_stringEmpty()
        {
            Assert.Throws<FormatException>(() => HttpVersion.Parse(string.Empty));
        }

        [Fact]
        public void op_Parse_string_Invalid()
        {
            Assert.Throws<FormatException>(() => HttpVersion.Parse("1.0"));
        }

        [Fact]
        public void op_CompareTo_objectNull()
        {
            Assert.True(new HttpVersion(1, 0).CompareTo(null) > 0);
        }

        [Fact]
        public void op_CompareTo_objectSame()
        {
            HttpVersion value = new HttpVersion(1, 0);

            Assert.Equal<int>(0, value.CompareTo(value));
        }

        [Fact]
        public void op_CompareTo_object()
        {
            HttpVersion left = new HttpVersion(1, 0);
            HttpVersion right = new HttpVersion(1, 0);

            Assert.Equal<int>(0, left.CompareTo(right));
        }

        [Fact]
        public void op_CompareTo_objectLesser()
        {
            HttpVersion left = new HttpVersion(1, 0);
            HttpVersion right = new HttpVersion(1, 1);

            Assert.True(left.CompareTo(right) < 0);
        }

        [Fact]
        public void op_CompareTo_objectGreater()
        {
            HttpVersion left = new HttpVersion(1, 1);
            HttpVersion right = new HttpVersion(1, 0);

            Assert.True(left.CompareTo(right) > 0);
        }

        [Fact]
        public void op_CompareTo_objectInt32()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new HttpVersion(1, 0).CompareTo(123));
        }

        [Fact]
        public void op_Equals_object()
        {
            HttpVersion obj = new HttpVersion(1, 0);

            Assert.True(new HttpVersion(1, 0).Equals(obj));
        }

        [Fact]
        public void op_Equals_object_this()
        {
            HttpVersion obj = new HttpVersion(1, 0);

            Assert.True(obj.Equals(obj));
        }

        [Fact]
        public void op_Equals_objectNull()
        {
            Assert.False(new HttpVersion(1, 0).Equals(null));
        }

        [Fact]
        public void op_Equals_objectString()
        {
            Assert.False(new HttpVersion(1, 0).Equals("1.0"));
        }

        [Fact]
        public void op_GetHashCode()
        {
            HttpVersion obj = new HttpVersion(1, 0);

            int expected = obj.ToString().GetHashCode();
            int actual = obj.GetHashCode();

            Assert.Equal<int>(expected, actual);
        }

        [Fact]
        public void op_ToString()
        {
            string expected = "HTTP/1.0";
            string actual = new HttpVersion(1, 0).ToString();

            Assert.Equal<string>(expected, actual);
        }
    }
}