namespace Cavity.Net
{
    using System;
    using Cavity;
    using Xunit;

    public sealed class HttpMethodFacts
    {
        [Fact]
        public void type_definition()
        {
            Assert.True(new TypeExpectations<HttpMethod>()
                .DerivesFrom<object>()
                .IsConcreteClass()
                .IsSealed()
                .NoDefaultConstructor()
                .IsNotDecorated()
                .Implements<IComparable>()
                .Result);
        }

        [Fact]
        public void ctor_stringNull()
        {
            Assert.Throws<ArgumentNullException>(() => new HttpMethod(null as string));
        }

        [Fact]
        public void ctor_stringEmpty()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new HttpMethod(string.Empty));
        }

        [Fact]
        public void ctor_string()
        {
            Assert.NotNull(new HttpMethod("GET"));
        }

        [Fact]
        public void prop_Value()
        {
            Assert.True(new PropertyExpectations<HttpMethod>("Value")
                .TypeIs<string>()
                .ArgumentOutOfRangeException(string.Empty)
                .FormatException("FOO BAR")
                .FormatException("FOO123BAR")
                .Set("OPTIONS")
                .Set("GET")
                .Set("HEAD")
                .Set("POST")
                .Set("PUT")
                .Set("DELETE")
                .Set("TRACE")
                .Set("CONNECT")
                .IsNotDecorated()
                .Result);
        }

        [Fact]
        public void opImplicit_stringNull_HttpMethod()
        {
            string expected = null;
            string actual = null as HttpMethod;

            Assert.Equal<string>(expected, actual);
        }

        [Fact]
        public void opImplicit_string_HttpMethod()
        {
            string expected = "GET";
            string actual = new HttpMethod(expected);

            Assert.Equal<string>(expected, actual);
        }

        [Fact]
        public void opImplicit_HttpMethod_stringNull()
        {
            HttpMethod obj = null as string;

            Assert.Null(obj);
        }

        [Fact]
        public void opImplicit_HttpMethod_stringEmpty()
        {
            HttpMethod expected;

            Assert.Throws<ArgumentOutOfRangeException>(() => expected = string.Empty);
        }

        [Fact]
        public void opImplicit_HttpMethod_string()
        {
            HttpMethod expected = "GET";
            HttpMethod actual = new HttpMethod("GET");

            Assert.Equal<HttpMethod>(expected, actual);
        }

        [Fact]
        public void opEquality_HttpMethod_HttpMethod_whenTrue()
        {
            HttpMethod operand1 = new HttpMethod("GET");
            HttpMethod operand2 = new HttpMethod("GET");

            Assert.True(operand1 == operand2);
        }

        [Fact]
        public void opEquality_HttpMethod_HttpMethod_whenFalse()
        {
            HttpMethod operand1 = new HttpMethod("HEAD");
            HttpMethod operand2 = new HttpMethod("GET");

            Assert.False(operand1 == operand2);
        }

        [Fact]
        public void opEquality_HttpMethod_HttpMethod_whenSame()
        {
            HttpMethod operand1 = new HttpMethod("GET");
            HttpMethod operand2 = operand1;

            Assert.True(operand1 == operand2);
        }

        [Fact]
        public void opEquality_HttpMethodNull_HttpMethod()
        {
            HttpMethod operand1 = null;
            HttpMethod operand2 = new HttpMethod("GET");

            Assert.False(operand1 == operand2);
        }

        [Fact]
        public void opEquality_HttpMethod_HttpMethodNull()
        {
            HttpMethod operand1 = new HttpMethod("GET");
            HttpMethod operand2 = null;

            Assert.False(operand1 == operand2);
        }

        [Fact]
        public void opInequality_HttpMethod_HttpMethod_whenTrue()
        {
            HttpMethod operand1 = new HttpMethod("HEAD");
            HttpMethod operand2 = new HttpMethod("GET");

            Assert.True(operand1 != operand2);
        }

        [Fact]
        public void opInequality_HttpMethod_HttpMethod_whenFalse()
        {
            HttpMethod operand1 = new HttpMethod("GET");
            HttpMethod operand2 = new HttpMethod("GET");

            Assert.False(operand1 != operand2);
        }

        [Fact]
        public void opInequality_HttpMethod_HttpMethod_whenSame()
        {
            HttpMethod operand1 = new HttpMethod("GET");
            HttpMethod operand2 = operand1;

            Assert.False(operand1 != operand2);
        }

        [Fact]
        public void opInequality_HttpMethodNull_HttpMethod()
        {
            HttpMethod operand1 = null;
            HttpMethod operand2 = new HttpMethod("GET");

            Assert.True(operand1 != operand2);
        }

        [Fact]
        public void opInequality_HttpMethod_HttpMethodNull()
        {
            HttpMethod operand1 = new HttpMethod("GET");
            HttpMethod operand2 = null;

            Assert.True(operand1 != operand2);
        }

        [Fact]
        public void opLesser_HttpMethod_HttpMethod_whenSame()
        {
            HttpMethod operand1 = new HttpMethod("GET");
            HttpMethod operand2 = operand1;

            Assert.False(operand1 < operand2);
        }

        [Fact]
        public void opLesser_HttpMethod_HttpMethod_whenTrue()
        {
            HttpMethod operand1 = new HttpMethod("GET");
            HttpMethod operand2 = new HttpMethod("HEAD");

            Assert.True(operand1 < operand2);
        }

        [Fact]
        public void opLesser_HttpMethod_HttpMethod_whenFalse()
        {
            HttpMethod operand1 = new HttpMethod("HEAD");
            HttpMethod operand2 = new HttpMethod("GET");

            Assert.False(operand1 < operand2);
        }

        [Fact]
        public void opLesser_HttpMethodNull_HttpMethod()
        {
            HttpMethod operand1 = null;
            HttpMethod operand2 = new HttpMethod("GET");

            Assert.True(operand1 < operand2);
        }

        [Fact]
        public void opLesser_HttpMethod_HttpMethodNull()
        {
            HttpMethod operand1 = new HttpMethod("GET");
            HttpMethod operand2 = null;

            Assert.False(operand1 < operand2);
        }

        [Fact]
        public void opGreater_HttpMethod_HttpMethod_whenSame()
        {
            HttpMethod operand1 = new HttpMethod("GET");
            HttpMethod operand2 = operand1;

            Assert.False(operand1 > operand2);
        }

        [Fact]
        public void opGreater_HttpMethod_HttpMethod_whenTrue()
        {
            HttpMethod operand1 = new HttpMethod("HEAD");
            HttpMethod operand2 = new HttpMethod("GET");

            Assert.True(operand1 > operand2);
        }

        [Fact]
        public void opGreater_HttpMethod_HttpMethod_whenFalse()
        {
            HttpMethod operand1 = new HttpMethod("GET");
            HttpMethod operand2 = new HttpMethod("HEAD");

            Assert.False(operand1 > operand2);
        }

        [Fact]
        public void opGreater_HttpMethodNull_HttpMethod()
        {
            HttpMethod operand1 = null;
            HttpMethod operand2 = new HttpMethod("GET");

            Assert.False(operand1 > operand2);
        }

        [Fact]
        public void opGreater_HttpMethod_HttpMethodNull()
        {
            HttpMethod operand1 = new HttpMethod("GET");
            HttpMethod operand2 = null;

            Assert.True(operand1 > operand2);
        }

        [Fact]
        public void op_Compare_HttpMethod_HttpMethod_Equal()
        {
            HttpMethod comparand1 = new HttpMethod("GET");
            HttpMethod comparand2 = new HttpMethod("GET");

            Assert.Equal<int>(0, HttpMethod.Compare(comparand1, comparand2));
        }

        [Fact]
        public void op_Compare_HttpMethod_HttpMethod_whenSame()
        {
            HttpMethod comparand1 = new HttpMethod("GET");
            HttpMethod comparand2 = comparand1;

            Assert.Equal<int>(0, HttpMethod.Compare(comparand1, comparand2));
        }

        [Fact]
        public void op_Compare_HttpMethodNull_HttpMethod()
        {
            HttpMethod comparand1 = null;
            HttpMethod comparand2 = new HttpMethod("GET");

            Assert.True(HttpMethod.Compare(comparand1, comparand2) < 0);
        }

        [Fact]
        public void op_Compare_HttpMethod_HttpMethodNull()
        {
            HttpMethod comparand1 = new HttpMethod("GET");
            HttpMethod comparand2 = null;

            Assert.True(HttpMethod.Compare(comparand1, comparand2) > 0);
        }

        [Fact]
        public void op_Compare_HttpMethodGreater_HttpMethod()
        {
            HttpMethod comparand1 = new HttpMethod("HEAD");
            HttpMethod comparand2 = new HttpMethod("GET");

            Assert.True(HttpMethod.Compare(comparand1, comparand2) > 0);
        }

        [Fact]
        public void op_Compare_HttpMethodLesser_HttpMethod()
        {
            HttpMethod comparand1 = new HttpMethod("GET");
            HttpMethod comparand2 = new HttpMethod("HEAD");

            Assert.True(HttpMethod.Compare(comparand1, comparand2) < 0);
        }

        [Fact]
        public void op_CompareTo_objectNull()
        {
            Assert.True(new HttpMethod("GET").CompareTo(null) > 0);
        }

        [Fact]
        public void op_CompareTo_objectSame()
        {
            HttpMethod value = new HttpMethod("GET");

            Assert.Equal<int>(0, value.CompareTo(value));
        }

        [Fact]
        public void op_CompareTo_object()
        {
            HttpMethod left = new HttpMethod("GET");
            HttpMethod right = new HttpMethod("GET");

            Assert.Equal<int>(0, left.CompareTo(right));
        }

        [Fact]
        public void op_CompareTo_objectLesser()
        {
            HttpMethod left = new HttpMethod("GET");
            HttpMethod right = new HttpMethod("HEAD");

            Assert.True(left.CompareTo(right) < 0);
        }

        [Fact]
        public void op_CompareTo_objectGreater()
        {
            HttpMethod left = new HttpMethod("HEAD");
            HttpMethod right = new HttpMethod("GET");

            Assert.True(left.CompareTo(right) > 0);
        }

        [Fact]
        public void op_CompareTo_objectInt32()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new HttpMethod("GET").CompareTo("GET"));
        }

        [Fact]
        public void op_Equals_object()
        {
            HttpMethod obj = new HttpMethod("GET");

            Assert.True(new HttpMethod("GET").Equals(obj));
        }

        [Fact]
        public void op_Equals_object_this()
        {
            HttpMethod obj = new HttpMethod("GET");

            Assert.True(obj.Equals(obj));
        }

        [Fact]
        public void op_Equals_objectNull()
        {
            Assert.False(new HttpMethod("GET").Equals(null));
        }

        [Fact]
        public void op_Equals_objectString()
        {
            Assert.False(new HttpMethod("GET").Equals("GET"));
        }

        [Fact]
        public void op_GetHashCode()
        {
            HttpMethod obj = new HttpMethod("GET");

            int expected = obj.ToString().GetHashCode();
            int actual = obj.GetHashCode();

            Assert.Equal<int>(expected, actual);
        }

        [Fact]
        public void op_ToString()
        {
            string expected = "GET";
            string actual = new HttpMethod(expected).ToString();

            Assert.Equal<string>(expected, actual);
        }
    }
}