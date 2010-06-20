namespace Cavity.Net
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using Cavity;
    using Xunit;

    public sealed class RequestLineFacts
    {
        [Fact]
        public void type_definition()
        {
            Assert.True(new TypeExpectations<RequestLine>()
                .DerivesFrom<object>()
                .IsConcreteClass()
                .IsSealed()
                .NoDefaultConstructor()
                .IsNotDecorated()
                .Implements<IComparable>()
                .Result);
        }

        [Fact]
        public void ctor_HttpVersion_string_stringAbsolute_string()
        {
            Assert.NotNull(new RequestLine("GET", "http://www.example.com/", "HTTP/1.1"));
        }

        [Fact]
        public void ctor_HttpVersion_string_stringRelative_string()
        {
            Assert.NotNull(new RequestLine("GET", "/", "HTTP/1.1"));
        }

        [Fact]
        public void prop_Method()
        {
            Assert.True(new PropertyExpectations<RequestLine>("Method")
                .TypeIs<string>()
                .ArgumentNullException()
                .ArgumentOutOfRangeException(string.Empty)
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
        public void prop_RequestUri()
        {
            Assert.True(new PropertyExpectations<RequestLine>("RequestUri")
                .TypeIs<string>()
                .ArgumentNullException()
                .ArgumentOutOfRangeException(string.Empty)
                .Set("*")
                .Set("http://www.example.com/")
                .Set("/")
                .IsDecoratedWith<SuppressMessageAttribute>()
                .Result);
        }

        [Fact]
        public void prop_Version()
        {
            Assert.True(new PropertyExpectations<RequestLine>("Version")
                .TypeIs<HttpVersion>()
                .ArgumentNullException()
                .Set(new HttpVersion(1, 0))
                .Set(new HttpVersion(1, 1))
                .IsNotDecorated()
                .Result);
        }

        [Fact]
        public void opImplicit_stringNull_RequestLine()
        {
            string expected = null;
            string actual = null as RequestLine;

            Assert.Equal<string>(expected, actual);
        }

        [Fact]
        public void opImplicit_string_RequestLine()
        {
            string expected = "GET / HTTP/1.1";
            string actual = new RequestLine("GET", "/", "HTTP/1.1");

            Assert.Equal<string>(expected, actual);
        }

        [Fact]
        public void opImplicit_RequestLine_stringNull()
        {
            RequestLine obj = null as string;

            Assert.Null(obj);
        }

        [Fact]
        public void opImplicit_RequestLine_stringEmpty()
        {
            RequestLine expected;

            Assert.Throws<FormatException>(() => expected = string.Empty);
        }

        [Fact]
        public void opImplicit_RequestLine_string()
        {
            RequestLine expected = "GET / HTTP/1.1";
            RequestLine actual = new RequestLine("GET", "/", "HTTP/1.1");

            Assert.Equal<RequestLine>(expected, actual);
        }

        [Fact]
        public void opEquality_RequestLine_RequestLine_whenTrue()
        {
            RequestLine operand1 = new RequestLine("GET", "/", "HTTP/1.1");
            RequestLine operand2 = new RequestLine("GET", "/", "HTTP/1.1");

            Assert.True(operand1 == operand2);
        }

        [Fact]
        public void opEquality_RequestLine_RequestLine_whenFalse()
        {
            RequestLine operand1 = new RequestLine("GET", "/foo", "HTTP/1.1");
            RequestLine operand2 = new RequestLine("GET", "/bar", "HTTP/1.1");

            Assert.False(operand1 == operand2);
        }

        [Fact]
        public void opEquality_RequestLine_RequestLine_whenSame()
        {
            RequestLine operand1 = new RequestLine("GET", "/", "HTTP/1.1");
            RequestLine operand2 = operand1;

            Assert.True(operand1 == operand2);
        }

        [Fact]
        public void opEquality_RequestLineNull_RequestLine()
        {
            RequestLine operand1 = null;
            RequestLine operand2 = new RequestLine("GET", "/", "HTTP/1.1");

            Assert.False(operand1 == operand2);
        }

        [Fact]
        public void opEquality_RequestLine_RequestLineNull()
        {
            RequestLine operand1 = new RequestLine("GET", "/", "HTTP/1.1");
            RequestLine operand2 = null;

            Assert.False(operand1 == operand2);
        }

        [Fact]
        public void opInequality_RequestLine_RequestLine_whenTrue()
        {
            RequestLine operand1 = new RequestLine("GET", "/foo", "HTTP/1.1");
            RequestLine operand2 = new RequestLine("GET", "/bar", "HTTP/1.1");

            Assert.True(operand1 != operand2);
        }

        [Fact]
        public void opInequality_RequestLine_RequestLine_whenFalse()
        {
            RequestLine operand1 = new RequestLine("GET", "/", "HTTP/1.1");
            RequestLine operand2 = new RequestLine("GET", "/", "HTTP/1.1");

            Assert.False(operand1 != operand2);
        }

        [Fact]
        public void opInequality_RequestLine_RequestLine_whenSame()
        {
            RequestLine operand1 = new RequestLine("GET", "/", "HTTP/1.1");
            RequestLine operand2 = operand1;

            Assert.False(operand1 != operand2);
        }

        [Fact]
        public void opInequality_RequestLineNull_RequestLine()
        {
            RequestLine operand1 = null;
            RequestLine operand2 = new RequestLine("GET", "/", "HTTP/1.1");

            Assert.True(operand1 != operand2);
        }

        [Fact]
        public void opInequality_RequestLine_RequestLineNull()
        {
            RequestLine operand1 = new RequestLine("GET", "/", "HTTP/1.1");
            RequestLine operand2 = null;

            Assert.True(operand1 != operand2);
        }

        [Fact]
        public void opLesser_RequestLine_RequestLine_whenSame()
        {
            RequestLine operand1 = new RequestLine("GET", "/", "HTTP/1.1");
            RequestLine operand2 = operand1;

            Assert.False(operand1 < operand2);
        }

        [Fact]
        public void opLesser_RequestLine_RequestLine_whenTrue()
        {
            RequestLine operand1 = new RequestLine("GET", "/bar", "HTTP/1.1");
            RequestLine operand2 = new RequestLine("GET", "/foo", "HTTP/1.1");

            Assert.True(operand1 < operand2);
        }

        [Fact]
        public void opLesser_RequestLine_RequestLine_whenFalse()
        {
            RequestLine operand1 = new RequestLine("GET", "/foo", "HTTP/1.1");
            RequestLine operand2 = new RequestLine("GET", "/bar", "HTTP/1.1");

            Assert.False(operand1 < operand2);
        }

        [Fact]
        public void opLesser_RequestLineNull_RequestLine()
        {
            RequestLine operand1 = null;
            RequestLine operand2 = new RequestLine("GET", "/", "HTTP/1.1");

            Assert.True(operand1 < operand2);
        }

        [Fact]
        public void opLesser_RequestLine_RequestLineNull()
        {
            RequestLine operand1 = new RequestLine("GET", "/", "HTTP/1.1");
            RequestLine operand2 = null;

            Assert.False(operand1 < operand2);
        }

        [Fact]
        public void opGreater_RequestLine_RequestLine_whenSame()
        {
            RequestLine operand1 = new RequestLine("GET", "/", "HTTP/1.1");
            RequestLine operand2 = operand1;

            Assert.False(operand1 > operand2);
        }

        [Fact]
        public void opGreater_RequestLine_RequestLine_whenTrue()
        {
            RequestLine operand1 = new RequestLine("GET", "/foo", "HTTP/1.1");
            RequestLine operand2 = new RequestLine("GET", "/bar", "HTTP/1.1");

            Assert.True(operand1 > operand2);
        }

        [Fact]
        public void opGreater_RequestLine_RequestLine_whenFalse()
        {
            RequestLine operand1 = new RequestLine("GET", "/bar", "HTTP/1.1");
            RequestLine operand2 = new RequestLine("GET", "/foo", "HTTP/1.1");

            Assert.False(operand1 > operand2);
        }

        [Fact]
        public void opGreater_RequestLineNull_RequestLine()
        {
            RequestLine operand1 = null;
            RequestLine operand2 = new RequestLine("GET", "/", "HTTP/1.1");

            Assert.False(operand1 > operand2);
        }

        [Fact]
        public void opGreater_RequestLine_RequestLineNull()
        {
            RequestLine operand1 = new RequestLine("GET", "/", "HTTP/1.1");
            RequestLine operand2 = null;

            Assert.True(operand1 > operand2);
        }

        [Fact]
        public void op_Compare_RequestLine_RequestLine_Equal()
        {
            RequestLine comparand1 = new RequestLine("GET", "/", "HTTP/1.1");
            RequestLine comparand2 = new RequestLine("GET", "/", "HTTP/1.1");

            Assert.Equal<int>(0, RequestLine.Compare(comparand1, comparand2));
        }

        [Fact]
        public void op_Compare_RequestLine_RequestLine_whenSame()
        {
            RequestLine comparand1 = new RequestLine("GET", "/", "HTTP/1.1");
            RequestLine comparand2 = comparand1;

            Assert.Equal<int>(0, RequestLine.Compare(comparand1, comparand2));
        }

        [Fact]
        public void op_Compare_RequestLineNull_RequestLine()
        {
            RequestLine comparand1 = null;
            RequestLine comparand2 = new RequestLine("GET", "/", "HTTP/1.1");

            Assert.True(RequestLine.Compare(comparand1, comparand2) < 0);
        }

        [Fact]
        public void op_Compare_RequestLine_RequestLineNull()
        {
            RequestLine comparand1 = new RequestLine("GET", "/", "HTTP/1.1");
            RequestLine comparand2 = null;

            Assert.True(RequestLine.Compare(comparand1, comparand2) > 0);
        }

        [Fact]
        public void op_Compare_RequestLineGreater_RequestLine()
        {
            RequestLine comparand1 = new RequestLine("GET", "/foo", "HTTP/1.1");
            RequestLine comparand2 = new RequestLine("GET", "/bar", "HTTP/1.1");

            Assert.True(RequestLine.Compare(comparand1, comparand2) > 0);
        }

        [Fact]
        public void op_Compare_RequestLineLesser_RequestLine()
        {
            RequestLine comparand1 = new RequestLine("GET", "/bar", "HTTP/1.1");
            RequestLine comparand2 = new RequestLine("GET", "/foo", "HTTP/1.1");

            Assert.True(RequestLine.Compare(comparand1, comparand2) < 0);
        }

        [Fact]
        public void op_Parse_string_whenGetRelative()
        {
            RequestLine expected = new RequestLine("GET", "/", "HTTP/1.1");
            RequestLine actual = RequestLine.Parse("GET / HTTP/1.1");

            Assert.Equal<RequestLine>(expected, actual);
        }

        [Fact]
        public void op_Parse_stringNull()
        {
            Assert.Throws<ArgumentNullException>(() => RequestLine.Parse(null as string));
        }

        [Fact]
        public void op_Parse_stringEmpty()
        {
            Assert.Throws<FormatException>(() => RequestLine.Parse(string.Empty));
        }

        [Fact]
        public void op_Parse_string_whenMissingHttpVersion()
        {
            Assert.Throws<FormatException>(() => RequestLine.Parse("GET /"));
        }

        [Fact]
        public void op_Parse_string_whenMissingHttpMethod()
        {
            Assert.Throws<FormatException>(() => RequestLine.Parse("/ HTTP/1.1"));
        }

        [Fact]
        public void op_Parse_string_whenMissingRequestUri()
        {
            Assert.Throws<FormatException>(() => RequestLine.Parse("GET HTTP/1.1"));
        }

        [Fact]
        public void op_Parse_string_whenCR()
        {
            Assert.Throws<FormatException>(() => RequestLine.Parse("999 Foo \r Bar"));
        }

        [Fact]
        public void op_Parse_string_whenLR()
        {
            Assert.Throws<FormatException>(() => RequestLine.Parse("999 Foo \n Bar"));
        }

        [Fact]
        public void op_CompareTo_objectNull()
        {
            Assert.True(new RequestLine("GET", "/", "HTTP/1.1").CompareTo(null) > 0);
        }

        [Fact]
        public void op_CompareTo_objectSame()
        {
            RequestLine value = new RequestLine("GET", "/", "HTTP/1.1");

            Assert.Equal<int>(0, value.CompareTo(value));
        }

        [Fact]
        public void op_CompareTo_object()
        {
            RequestLine left = new RequestLine("GET", "/", "HTTP/1.1");
            RequestLine right = new RequestLine("GET", "/", "HTTP/1.1");

            Assert.Equal<int>(0, left.CompareTo(right));
        }

        [Fact]
        public void op_CompareTo_objectLesser()
        {
            RequestLine left = new RequestLine("GET", "/bar", "HTTP/1.1");
            RequestLine right = new RequestLine("GET", "/foo", "HTTP/1.1");

            Assert.True(left.CompareTo(right) < 0);
        }

        [Fact]
        public void op_CompareTo_objectGreater()
        {
            RequestLine left = new RequestLine("GET", "/foo", "HTTP/1.1");
            RequestLine right = new RequestLine("GET", "/bar", "HTTP/1.1");

            Assert.True(left.CompareTo(right) > 0);
        }

        [Fact]
        public void op_CompareTo_objectInt32()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new RequestLine("GET", "/", "HTTP/1.1").CompareTo(123));
        }

        [Fact]
        public void op_Equals_object()
        {
            RequestLine obj = new RequestLine("GET", "/", "HTTP/1.1");

            Assert.True(new RequestLine("GET", "/", "HTTP/1.1").Equals(obj));
        }

        [Fact]
        public void op_Equals_object_this()
        {
            RequestLine obj = new RequestLine("GET", "/", "HTTP/1.1");

            Assert.True(obj.Equals(obj));
        }

        [Fact]
        public void op_Equals_objectNull()
        {
            Assert.False(new RequestLine("GET", "/", "HTTP/1.1").Equals(null));
        }

        [Fact]
        public void op_Equals_objectString()
        {
            Assert.False(new RequestLine("GET", "/", "HTTP/1.1").Equals("GET / HTTP/1.1"));
        }

        [Fact]
        public void op_GetHashCode()
        {
            RequestLine obj = new RequestLine("GET", "/", "HTTP/1.1");

            int expected = obj.ToString().GetHashCode();
            int actual = obj.GetHashCode();

            Assert.Equal<int>(expected, actual);
        }

        [Fact]
        public void op_ToString()
        {
            string expected = "HEAD / HTTP/1.1";
            string actual = new RequestLine("HEAD", "/", "HTTP/1.1").ToString();

            Assert.Equal<string>(expected, actual);
        }
    }
}