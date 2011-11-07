namespace Cavity.Web.Mvc
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Net.Mime;
    using System.Web;
    using Cavity;
    using Cavity.Globalization;
    using Moq;
    using Xunit;

    public sealed class AcceptLanguageFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<AcceptLanguage>()
                .DerivesFrom<object>()
                .IsConcreteClass()
                .IsUnsealed()
                .HasDefaultConstructor()
                .IsNotDecorated()
                .Result);
        }

        [Fact]
        public void ctor()
        {
            Assert.NotNull(new AcceptLanguage());
        }

        [Fact]
        public void ctor_IEnumerableLanguage()
        {
            Assert.NotNull(new AcceptLanguage(new[] { new Language("en") }));
        }

        [Fact]
        public void ctor_IEnumerableLanguageEmpty()
        {
            Assert.NotNull(new AcceptLanguage(new List<Language>()));
        }

        [Fact]
        public void ctor_IEnumerableLanguageNull()
        {
            Assert.Throws<ArgumentNullException>(() => new AcceptLanguage(null));
        }

        [Fact]
        public void prop_Languages()
        {
            Assert.True(new PropertyExpectations<AcceptLanguage>(x => x.Languages)
                .TypeIs<Collection<Language>>()
                .DefaultValueIsNotNull()
                .Result);
        }

        [Fact]
        public void opImplicit_AcceptLanguage_string()
        {
            const string expected = "en";

            AcceptLanguage obj = expected;

            Assert.Equal(1, obj.Languages.Count);
            Assert.Equal<Language>(expected, obj.Languages[0]);
        }

        [Fact]
        public void opImplicit_AcceptLanguage_stringEmpty()
        {
            AcceptLanguage obj = string.Empty;

            Assert.Equal(1, obj.Languages.Count);
            Assert.Equal<Language>("*", obj.Languages[0]);
        }

        [Fact]
        public void opImplicit_AcceptLanguage_stringNull()
        {
            AcceptLanguage obj = null as string;

            Assert.Equal(1, obj.Languages.Count);
            Assert.Equal<Language>("*", obj.Languages[0]);
        }

        [Fact]
        public void op_FromString_stringEmpty()
        {
            const string expected = "*";
            var actual = AcceptLanguage.FromString(string.Empty).Languages[0];

            Assert.Equal<Language>(expected, actual);
        }

        [Fact]
        public void op_FromString_stringNull()
        {
            const string expected = "*";
            var actual = AcceptLanguage.FromString(null).Languages[0];

            Assert.Equal<Language>(expected, actual);
        }

        [Fact]
        public void op_FromString_string()
        {
            const string expected = "en";
            var actual = AcceptLanguage.FromString(expected).Languages[0];

            Assert.Equal<Language>(expected, actual);
        }

        [Fact]
        public void op_FromString_stringSemicolon()
        {
            const string expected = "en";
            var actual = AcceptLanguage.FromString(expected + ";q=0.4").Languages[0];

            Assert.Equal<Language>(expected, actual);
        }

        [Fact]
        public void op_FromString_stringAcceptLanguage()
        {
            const string value = "da, en-gb;q=0.8, en;q=0.7";
            var obj = AcceptLanguage.FromString(value);

            Assert.Equal(3, obj.Languages.Count);

            Assert.Equal<Language>("da", obj.Languages[0]);
            Assert.Equal<Language>("en-gb", obj.Languages[1]);
            Assert.Equal<Language>("en", obj.Languages[2]);
        }

        [Fact]
        public void op_FromString_stringAcceptLanguageDisordered()
        {
            const string value = "*, en, en-gb;q=0.4, en-us";
            var obj = AcceptLanguage.FromString(value);

            Assert.Equal(4, obj.Languages.Count);

            Assert.Equal<Language>("en-us", obj.Languages[0]);
            Assert.Equal<Language>("en", obj.Languages[1]);
            Assert.Equal<Language>("en-gb", obj.Languages[2]);
            Assert.Equal<Language>("*", obj.Languages[3]);
        }

        [Fact]
        public void op_Negotiate_HttpRequestBase_Type()
        {
            var obj = AcceptLanguage.FromString("da, en-gb;q=0.8, en;q=0.7");

            var request = new Mock<HttpRequestBase>();
            request
                .SetupGet(x => x.Path)
                .Returns("/test")
                .Verifiable();
            request
                .SetupGet(x => x.RawUrl)
                .Returns("http://example.com/test")
                .Verifiable();

            const string expected = "/test.en";
            var actual = (obj.Negotiate(request.Object, typeof(DummyController)) as SeeOtherResult).Location;

            Assert.Equal(expected, actual);

            request.VerifyAll();
        }

        [Fact]
        public void op_Negotiate_HttpRequestBaseNull_Type()
        {
            Assert.Throws<ArgumentNullException>(() => new AcceptLanguage().Negotiate(null, typeof(DummyController)));
        }

        [Fact]
        public void op_Negotiate_HttpRequestBase_TypeNull()
        {
            Assert.Throws<ArgumentNullException>(() => new AcceptLanguage().Negotiate(new Mock<HttpRequestBase>().Object, null));
        }

        [Fact]
        public void op_Negotiate_HttpRequestBase_TypeInvalid()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new AcceptLanguage().Negotiate(new Mock<HttpRequestBase>().Object, typeof(string)));
        }
    }
}