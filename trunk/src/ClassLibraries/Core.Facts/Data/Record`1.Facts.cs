namespace Cavity.Data
{
    using System;
    using Xunit;

    public sealed class RecordOfTFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<Record<int>>()
                            .DerivesFrom<ValueObject<Record<int>>>()
                            .IsConcreteClass()
                            .IsSealed()
                            .HasDefaultConstructor()
                            .IsNotDecorated()
                            .Implements<IRecord<int>>()
                            .Result);
        }

        [Fact]
        public void prop_Cacheability()
        {
            Assert.True(new PropertyExpectations<Record<int>>(x => x.Cacheability)
                            .TypeIs<string>()
                            .XmlIgnore()
                            .Result);
        }

        [Fact]
        public void prop_Created()
        {
            Assert.True(new PropertyExpectations<Record<int>>(x => x.Created)
                            .TypeIs<DateTime?>()
                            .XmlIgnore()
                            .Result);
        }

        [Fact]
        public void prop_Etag()
        {
            Assert.True(new PropertyExpectations<Record<int>>(x => x.Etag)
                            .TypeIs<string>()
                            .XmlIgnore()
                            .Result);
        }

        [Fact]
        public void prop_Expiration()
        {
            Assert.True(new PropertyExpectations<Record<int>>(x => x.Expiration)
                            .TypeIs<string>()
                            .XmlIgnore()
                            .Result);
        }

        [Fact]
        public void prop_Key()
        {
            Assert.True(new PropertyExpectations<Record<int>>(x => x.Key)
                            .TypeIs<AlphaDecimal?>()
                            .XmlIgnore()
                            .Result);
        }

        [Fact]
        public void prop_Modified()
        {
            Assert.True(new PropertyExpectations<Record<int>>(x => x.Modified)
                            .TypeIs<DateTime?>()
                            .XmlIgnore()
                            .Result);
        }

        [Fact]
        public void prop_Status()
        {
            Assert.True(new PropertyExpectations<Record<int>>(x => x.Status)
                            .TypeIs<int?>()
                            .XmlIgnore()
                            .Result);
        }

        [Fact]
        public void prop_Urn()
        {
            Assert.True(new PropertyExpectations<Record<int>>(x => x.Urn)
                            .TypeIs<AbsoluteUri>()
                            .XmlIgnore()
                            .Result);
        }

        [Fact]
        public void prop_Value()
        {
            Assert.True(new PropertyExpectations<Record<int>>(x => x.Value)
                            .TypeIs<int>()
                            .XmlIgnore()
                            .Result);
        }
    }
}