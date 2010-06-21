﻿namespace Cavity.Net
{
    using System;
    using Xunit;

    public sealed class IHttpHeaderFacts
    {
        [Fact]
        public void type_definition()
        {
            Assert.True(typeof(IHttpHeader).IsInterface);
        }

        [Fact]
        public void IHttpHeader_Name_get()
        {
            try
            {
                Token value = (new IHttpHeaderDummy() as IHttpHeader).Name;
                Assert.True(false);
            }
            catch (NotSupportedException)
            {
            }
        }

        [Fact]
        public void IHttpHeader_Value_get()
        {
            try
            {
                string value = (new IHttpHeaderDummy() as IHttpHeader).Value;
                Assert.True(false);
            }
            catch (NotSupportedException)
            {
            }
        }
    }
}