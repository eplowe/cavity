namespace Cavity.Data
{
    using System;
    using Cavity;
    using Moq;
    using Xunit;

    public sealed class RecordFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<Record>()
                .DerivesFrom<ValueObject<Record>>()
                .IsAbstractBaseClass()
                .IsNotDecorated()
                .Implements<IRecord>()
                .Result);
        }

        [Fact]
        public void prop_Cacheability()
        {
            Assert.True(new PropertyExpectations<Record>(x => x.Cacheability)
                            .TypeIs<string>()
                            .XmlIgnore()
                            .Result);
        }

        [Fact]
        public void prop_Created()
        {
            Assert.True(new PropertyExpectations<Record>(x => x.Created)
                            .TypeIs<DateTime?>()
                            .XmlIgnore()
                            .Result);
        }

        [Fact]
        public void prop_Etag()
        {
            Assert.True(new PropertyExpectations<Record>(x => x.Etag)
                            .TypeIs<string>()
                            .XmlIgnore()
                            .Result);
        }

        [Fact]
        public void prop_Expiration()
        {
            Assert.True(new PropertyExpectations<Record>(x => x.Expiration)
                            .TypeIs<string>()
                            .XmlIgnore()
                            .Result);
        }

        [Fact]
        public void prop_Key()
        {
            Assert.True(new PropertyExpectations<Record>(x => x.Key)
                            .TypeIs<AlphaDecimal?>()
                            .XmlIgnore()
                            .Result);
        }

        [Fact]
        public void prop_Modified()
        {
            Assert.True(new PropertyExpectations<Record>(x => x.Modified)
                            .TypeIs<DateTime?>()
                            .XmlIgnore()
                            .Result);
        }

        [Fact]
        public void prop_Status()
        {
            Assert.True(new PropertyExpectations<Record>(x => x.Status)
                            .TypeIs<int?>()
                            .XmlIgnore()
                            .Result);
        }

        [Fact]
        public void prop_Urn()
        {
            Assert.True(new PropertyExpectations<Record>(x => x.Urn)
                            .TypeIs<AbsoluteUri>()
                            .XmlIgnore()
                            .Result);
        }
    }
}