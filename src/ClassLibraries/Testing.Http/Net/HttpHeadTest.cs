namespace Cavity.Net
{
    using System;

    public sealed class HttpHeadTest : ITestHttpExpectation
    {
        public HttpHeadTest(HttpRequestDefinition definition)
        {
            if (null == definition)
            {
                throw new ArgumentNullException("definition");
            }
            else if (!"GET".Equals(definition.Method, StringComparison.OrdinalIgnoreCase))
            {
                throw new ArgumentOutOfRangeException("definition");
            }

            Definition = definition;
        }

        private HttpRequestDefinition Definition { get; set; }

        bool ITestHttpExpectation.Check(Response response)
        {
            if (null == response)
            {
                throw new ArgumentNullException("response");
            }

            return false;
        }
    }
}