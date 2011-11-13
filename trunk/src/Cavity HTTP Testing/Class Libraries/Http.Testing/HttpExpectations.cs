namespace Cavity.Net
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Diagnostics.CodeAnalysis;

    [SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix", Justification = "This isn't fundamentally a collection.")]
    public sealed class HttpExpectations : Collection<HttpExpectation>, IHttpExpectations
    {
        private static readonly string[] _request = new[]
        {
            ">request>{0}".FormatWith(Environment.NewLine)
        };

        private static readonly string[] _response = new[]
        {
            "<response<{0}".FormatWith(Environment.NewLine)
        };

        public HttpExpectations(IEnumerable<HttpExpectation> expectations)
        {
            if (null == expectations)
            {
                throw new ArgumentNullException("expectations");
            }

            foreach (var expectation in expectations)
            {
                Add(expectation);
            }
        }

        public HttpExpectations()
        {
        }

        public static IHttpExpectations Load(string value)
        {
            if (null == value)
            {
                throw new ArgumentNullException("value");
            }

            if (0 == value.Length)
            {
                throw new ArgumentOutOfRangeException("value");
            }

            return new HttpExpectations(FromString(value));
        }

        private static HttpExpectation FromPart(string value)
        {
            var parts = value.Split(_response, StringSplitOptions.RemoveEmptyEntries);
            if (2 != parts.Length)
            {
                throw new FormatException();
            }

            var request = parts[0]
                .RemoveFromEnd(Environment.NewLine, StringComparison.Ordinal);
            var index = parts[1].IndexOf("<<", StringComparison.Ordinal);
            if (-1 == index)
            {
                throw new FormatException();
            }

            var response = parts[1]
                .Substring(0, index)
                .RemoveFromEnd(Environment.NewLine, StringComparison.Ordinal);
            return new HttpExpectation
            {
                Exchange = new HttpExchange
                {
                    Request = HttpRequest.FromString(request), 
                    Response = HttpResponse.FromString(response)
                }
            };
        }

        private static IEnumerable<HttpExpectation> FromString(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                yield break;
            }

            var parts = value.Split(_request, StringSplitOptions.RemoveEmptyEntries);
            foreach (var part in parts)
            {
                yield return FromPart(part);
            }
        }
    }
}