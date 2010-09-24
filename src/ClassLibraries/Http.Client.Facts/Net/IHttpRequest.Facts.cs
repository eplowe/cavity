﻿namespace Cavity.Net
{
    using System;
    using Xunit;

    public sealed class IHttpRequestFacts
    {
        [Fact]
        public void IHttpRequest_AbsoluteUri_get()
        {
            try
            {
                var value = (new IHttpRequestDummy() as IHttpRequest).AbsoluteUri;
                Assert.NotNull(value);
            }
            catch (NotSupportedException)
            {
            }
        }

        [Fact]
        public void IHttpRequest_RequestLine_get()
        {
            try
            {
                var value = (new IHttpRequestDummy() as IHttpRequest).RequestLine;
                Assert.NotNull(value);
            }
            catch (NotSupportedException)
            {
            }
        }

        [Fact]
        public void a_definition()
        {
            Assert.True(typeof(IHttpRequest).IsInterface);
        }

        [Fact]
        public void is_IHttpMessage()
        {
            Assert.True(typeof(IHttpRequest).Implements(typeof(IHttpMessage)));
        }
    }
}