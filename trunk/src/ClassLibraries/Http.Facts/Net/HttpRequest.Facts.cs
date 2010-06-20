namespace Cavity.Net
{
    using System;
    using Cavity;
    using Xunit;

    public sealed class HttpRequestFacts
    {
        [Fact]
        public void type_definition()
        {
            Assert.True(new TypeExpectations<HttpRequest>()
                .DerivesFrom<object>()
                .IsConcreteClass()
                .IsSealed()
                .NoDefaultConstructor()
                .IsNotDecorated()
                .Implements<IComparable>()
                .Implements<IHttpRequest>()
                .Result);
        }

        [Fact]
        public void ctor_RequestLineNull()
        {
            Assert.Throws<ArgumentNullException>(() => new HttpRequest(null as RequestLine));
        }

        [Fact]
        public void ctor_RequestLine()
        {
            Assert.NotNull(new HttpRequest(new RequestLine("GET", "/", "HTTP/1.1")));
        }

        [Fact]
        public void prop_RequestLine()
        {
            Assert.NotNull(new PropertyExpectations<HttpRequest>("RequestLine")
                .TypeIs<RequestLine>()
                .ArgumentNullException()
                .Set(new RequestLine("GET", "/", "HTTP/1.1"))
                .IsNotDecorated()
                .Result);
        }

        [Fact]
        public void opImplicit_stringNull_HttpRequest()
        {
            string expected = null;
            string actual = null as HttpRequest;

            Assert.Equal<string>(expected, actual);
        }

        [Fact]
        public void opImplicit_string_HttpRequest()
        {
            string expected = "GET / HTTP/1.1";
            string actual = new HttpRequest(expected);

            Assert.Equal<string>(expected, actual);
        }

        [Fact]
        public void opImplicit_HttpRequest_stringNull()
        {
            HttpRequest obj = null as string;

            Assert.Null(obj);
        }

        [Fact]
        public void opImplicit_HttpRequest_stringEmpty()
        {
            HttpRequest expected;

            Assert.Throws<FormatException>(() => expected = string.Empty);
        }

        [Fact]
        public void opImplicit_HttpRequest_string()
        {
            HttpRequest expected = "GET / HTTP/1.1";
            HttpRequest actual = new HttpRequest("GET / HTTP/1.1");

            Assert.Equal<HttpRequest>(expected, actual);
        }

        [Fact]
        public void opEquality_HttpRequest_HttpRequest_whenTrue()
        {
            HttpRequest operand1 = new HttpRequest("GET / HTTP/1.1");
            HttpRequest operand2 = new HttpRequest("GET / HTTP/1.1");

            Assert.True(operand1 == operand2);
        }

        [Fact]
        public void opEquality_HttpRequest_HttpRequest_whenFalse()
        {
            HttpRequest operand1 = new HttpRequest("GET /foo HTTP/1.1");
            HttpRequest operand2 = new HttpRequest("GET /bar HTTP/1.1");

            Assert.False(operand1 == operand2);
        }

        [Fact]
        public void opEquality_HttpRequest_HttpRequest_whenSame()
        {
            HttpRequest operand1 = new HttpRequest("GET / HTTP/1.1");
            HttpRequest operand2 = operand1;

            Assert.True(operand1 == operand2);
        }

        [Fact]
        public void opEquality_HttpRequestNull_HttpRequest()
        {
            HttpRequest operand1 = null;
            HttpRequest operand2 = new HttpRequest("GET / HTTP/1.1");

            Assert.False(operand1 == operand2);
        }

        [Fact]
        public void opEquality_HttpRequest_HttpRequestNull()
        {
            HttpRequest operand1 = new HttpRequest("GET / HTTP/1.1");
            HttpRequest operand2 = null;

            Assert.False(operand1 == operand2);
        }

        [Fact]
        public void opInequality_HttpRequest_HttpRequest_whenTrue()
        {
            HttpRequest operand1 = new HttpRequest("GET /foo HTTP/1.1");
            HttpRequest operand2 = new HttpRequest("GET /bar HTTP/1.1");

            Assert.True(operand1 != operand2);
        }

        [Fact]
        public void opInequality_HttpRequest_HttpRequest_whenFalse()
        {
            HttpRequest operand1 = new HttpRequest("GET / HTTP/1.1");
            HttpRequest operand2 = new HttpRequest("GET / HTTP/1.1");

            Assert.False(operand1 != operand2);
        }

        [Fact]
        public void opInequality_HttpRequest_HttpRequest_whenSame()
        {
            HttpRequest operand1 = new HttpRequest("GET / HTTP/1.1");
            HttpRequest operand2 = operand1;

            Assert.False(operand1 != operand2);
        }

        [Fact]
        public void opInequality_HttpRequestNull_HttpRequest()
        {
            HttpRequest operand1 = null;
            HttpRequest operand2 = new HttpRequest("GET / HTTP/1.1");

            Assert.True(operand1 != operand2);
        }

        [Fact]
        public void opInequality_HttpRequest_HttpRequestNull()
        {
            HttpRequest operand1 = new HttpRequest("GET / HTTP/1.1");
            HttpRequest operand2 = null;

            Assert.True(operand1 != operand2);
        }

        [Fact]
        public void opLesser_HttpRequest_HttpRequest_whenSame()
        {
            HttpRequest operand1 = new HttpRequest("GET / HTTP/1.1");
            HttpRequest operand2 = operand1;

            Assert.False(operand1 < operand2);
        }

        [Fact]
        public void opLesser_HttpRequest_HttpRequest_whenTrue()
        {
            HttpRequest operand1 = new HttpRequest("GET /bar HTTP/1.1");
            HttpRequest operand2 = new HttpRequest("GET /foo HTTP/1.1");

            Assert.True(operand1 < operand2);
        }

        [Fact]
        public void opLesser_HttpRequest_HttpRequest_whenFalse()
        {
            HttpRequest operand1 = new HttpRequest("GET /foo HTTP/1.1");
            HttpRequest operand2 = new HttpRequest("GET /bar HTTP/1.1");

            Assert.False(operand1 < operand2);
        }

        [Fact]
        public void opLesser_HttpRequestNull_HttpRequest()
        {
            HttpRequest operand1 = null;
            HttpRequest operand2 = new HttpRequest("GET / HTTP/1.1");

            Assert.True(operand1 < operand2);
        }

        [Fact]
        public void opLesser_HttpRequest_HttpRequestNull()
        {
            HttpRequest operand1 = new HttpRequest("GET / HTTP/1.1");
            HttpRequest operand2 = null;

            Assert.False(operand1 < operand2);
        }

        [Fact]
        public void opGreater_HttpRequest_HttpRequest_whenSame()
        {
            HttpRequest operand1 = new HttpRequest("GET / HTTP/1.1");
            HttpRequest operand2 = operand1;

            Assert.False(operand1 > operand2);
        }

        [Fact]
        public void opGreater_HttpRequest_HttpRequest_whenTrue()
        {
            HttpRequest operand1 = new HttpRequest("GET /foo HTTP/1.1");
            HttpRequest operand2 = new HttpRequest("GET /bar HTTP/1.1");

            Assert.True(operand1 > operand2);
        }

        [Fact]
        public void opGreater_HttpRequest_HttpRequest_whenFalse()
        {
            HttpRequest operand1 = new HttpRequest("GET /bar HTTP/1.1");
            HttpRequest operand2 = new HttpRequest("GET /foo HTTP/1.1");

            Assert.False(operand1 > operand2);
        }

        [Fact]
        public void opGreater_HttpRequestNull_HttpRequest()
        {
            HttpRequest operand1 = null;
            HttpRequest operand2 = new HttpRequest("GET / HTTP/1.1");

            Assert.False(operand1 > operand2);
        }

        [Fact]
        public void opGreater_HttpRequest_HttpRequestNull()
        {
            HttpRequest operand1 = new HttpRequest("GET / HTTP/1.1");
            HttpRequest operand2 = null;

            Assert.True(operand1 > operand2);
        }

        [Fact]
        public void op_Compare_HttpRequest_HttpRequest_Equal()
        {
            HttpRequest comparand1 = new HttpRequest("GET / HTTP/1.1");
            HttpRequest comparand2 = new HttpRequest("GET / HTTP/1.1");

            Assert.Equal<int>(0, HttpRequest.Compare(comparand1, comparand2));
        }

        [Fact]
        public void op_Compare_HttpRequest_HttpRequest_whenSame()
        {
            HttpRequest comparand1 = new HttpRequest("GET / HTTP/1.1");
            HttpRequest comparand2 = comparand1;

            Assert.Equal<int>(0, HttpRequest.Compare(comparand1, comparand2));
        }

        [Fact]
        public void op_Compare_HttpRequestNull_HttpRequest()
        {
            HttpRequest comparand1 = null;
            HttpRequest comparand2 = new HttpRequest("GET / HTTP/1.1");

            Assert.True(HttpRequest.Compare(comparand1, comparand2) < 0);
        }

        [Fact]
        public void op_Compare_HttpRequest_HttpRequestNull()
        {
            HttpRequest comparand1 = new HttpRequest("GET / HTTP/1.1");
            HttpRequest comparand2 = null;

            Assert.True(HttpRequest.Compare(comparand1, comparand2) > 0);
        }

        [Fact]
        public void op_Compare_HttpRequestGreater_HttpRequest()
        {
            HttpRequest comparand1 = new HttpRequest("GET /foo HTTP/1.1");
            HttpRequest comparand2 = new HttpRequest("GET /bar HTTP/1.1");

            Assert.True(HttpRequest.Compare(comparand1, comparand2) > 0);
        }

        [Fact]
        public void op_Compare_HttpRequestLesser_HttpRequest()
        {
            HttpRequest comparand1 = new HttpRequest("GET /bar HTTP/1.1");
            HttpRequest comparand2 = new HttpRequest("GET /foo HTTP/1.1");

            Assert.True(HttpRequest.Compare(comparand1, comparand2) < 0);
        }

        [Fact]
        public void op_Parse_stringNull()
        {
            Assert.Throws<ArgumentNullException>(() => HttpRequest.Parse(null as string));
        }

        [Fact]
        public void op_Parse_stringEmpty()
        {
            Assert.Throws<FormatException>(() => HttpRequest.Parse(string.Empty));
        }

        [Fact]
        public void op_Parse_string()
        {
            HttpRequest expected = new HttpRequest("GET / HTTP/1.1");
            HttpRequest actual = HttpRequest.Parse("GET / HTTP/1.1");

            Assert.Equal<HttpRequest>(expected, actual);
        }

        [Fact]
        public void op_CompareTo_objectNull()
        {
            Assert.True(new HttpRequest("GET / HTTP/1.1").CompareTo(null) > 0);
        }

        [Fact]
        public void op_CompareTo_objectSame()
        {
            HttpRequest value = new HttpRequest("GET / HTTP/1.1");

            Assert.Equal<int>(0, value.CompareTo(value));
        }

        [Fact]
        public void op_CompareTo_object()
        {
            HttpRequest left = new HttpRequest("GET / HTTP/1.1");
            HttpRequest right = new HttpRequest("GET / HTTP/1.1");

            Assert.Equal<int>(0, left.CompareTo(right));
        }

        [Fact]
        public void op_CompareTo_objectLesser()
        {
            HttpRequest left = new HttpRequest("GET /bar HTTP/1.1");
            HttpRequest right = new HttpRequest("GET /foo HTTP/1.1");

            Assert.True(left.CompareTo(right) < 0);
        }

        [Fact]
        public void op_CompareTo_objectGreater()
        {
            HttpRequest left = new HttpRequest("GET /foo HTTP/1.1");
            HttpRequest right = new HttpRequest("GET /bar HTTP/1.1");

            Assert.True(left.CompareTo(right) > 0);
        }

        [Fact]
        public void op_CompareTo_objectInt32()
        {
            string value = "GET / HTTP/1.1";

            Assert.Throws<ArgumentOutOfRangeException>(() => new HttpRequest(value).CompareTo(value));
        }

        [Fact]
        public void op_Equals_object()
        {
            HttpRequest obj = new HttpRequest("GET / HTTP/1.1");

            Assert.True(new HttpRequest("GET / HTTP/1.1").Equals(obj));
        }

        [Fact]
        public void op_Equals_object_this()
        {
            HttpRequest obj = new HttpRequest("GET / HTTP/1.1");

            Assert.True(obj.Equals(obj));
        }

        [Fact]
        public void op_Equals_objectNull()
        {
            Assert.False(new HttpRequest("GET / HTTP/1.1").Equals(null));
        }

        [Fact]
        public void op_Equals_objectString()
        {
            string value = "GET / HTTP/1.1";

            Assert.False(new HttpRequest(value).Equals(value));
        }

        [Fact]
        public void op_GetHashCode()
        {
            HttpRequest obj = new HttpRequest("GET / HTTP/1.1");

            int expected = obj.ToString().GetHashCode();
            int actual = obj.GetHashCode();

            Assert.Equal<int>(expected, actual);
        }

        [Fact]
        public void op_ToResponse_IHttpClient()
        {
            var obj = new HttpRequest("GET / HTTP/1.1");

            Assert.Throws<NotSupportedException>(() => obj.ToResponse(new HttpClient()));
        }

        [Fact]
        public void op_ToResponse_IHttpClientNull()
        {
            var obj = new HttpRequest("GET / HTTP/1.1");

            Assert.Throws<ArgumentNullException>(() => obj.ToResponse(null as IHttpClient));
        }

        [Fact]
        public void op_ToString()
        {
            string expected = "GET / HTTP/1.1";
            string actual = new HttpRequest("GET / HTTP/1.1").ToString();

            Assert.Equal<string>(expected, actual);
        }
    }
}