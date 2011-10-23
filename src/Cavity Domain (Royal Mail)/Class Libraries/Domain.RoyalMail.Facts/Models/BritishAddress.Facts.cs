namespace Cavity.Models
{
    using System;
    using Cavity;
    using Xunit;

    public sealed class BritishAddressFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<BritishAddress>()
                .DerivesFrom<ComparableObject>()
                .IsConcreteClass()
                .IsUnsealed()
                .HasDefaultConstructor()
                .IsNotDecorated()
                .Result);
        }

        [Fact]
        public void ctor()
        {
            Assert.NotNull(new BritishAddress());
        }

        [Fact]
        public void prop_BuildingName()
        {
            Assert.NotNull(new PropertyExpectations<BritishAddress>(x => x.BuildingName)
                               .IsAutoProperty<string>()
                               .IsNotDecorated()
                               .Result);
        }

        [Fact]
        public void prop_BuildingNumber()
        {
            Assert.NotNull(new PropertyExpectations<BritishAddress>(x => x.BuildingNumber)
                               .IsAutoProperty<string>()
                               .IsNotDecorated()
                               .Result);
        }

        [Fact]
        public void prop_DependentLocality()
        {
            Assert.NotNull(new PropertyExpectations<BritishAddress>(x => x.DependentLocality)
                               .IsAutoProperty<string>()
                               .IsNotDecorated()
                               .Result);
        }

        [Fact]
        public void prop_DependentStreet()
        {
            Assert.NotNull(new PropertyExpectations<BritishAddress>(x => x.DependentStreet)
                               .IsAutoProperty<string>()
                               .IsNotDecorated()
                               .Result);
        }

        [Fact]
        public void prop_DoubleDependentLocality()
        {
            Assert.NotNull(new PropertyExpectations<BritishAddress>(x => x.DoubleDependentLocality)
                               .IsAutoProperty<string>()
                               .IsNotDecorated()
                               .Result);
        }

        [Fact]
        public void prop_MainStreet()
        {
            Assert.NotNull(new PropertyExpectations<BritishAddress>(x => x.MainStreet)
                               .IsAutoProperty<string>()
                               .IsNotDecorated()
                               .Result);
        }

        [Fact]
        public void prop_Postcode()
        {
            Assert.NotNull(new PropertyExpectations<BritishAddress>(x => x.Postcode)
                               .IsAutoProperty<BritishPostcode>()
                               .IsNotDecorated()
                               .Result);
        }

        [Fact]
        public void prop_PostOfficeBox()
        {
            Assert.NotNull(new PropertyExpectations<BritishAddress>(x => x.PostOfficeBox)
                               .IsAutoProperty<string>()
                               .IsNotDecorated()
                               .Result);
        }

        [Fact]
        public void prop_PostTown()
        {
            Assert.NotNull(new PropertyExpectations<BritishAddress>(x => x.PostTown)
                               .IsAutoProperty<string>()
                               .IsNotDecorated()
                               .Result);
        }

        [Fact]
        public void prop_SubBuildingName()
        {
            Assert.NotNull(new PropertyExpectations<BritishAddress>(x => x.SubBuildingName)
                               .IsAutoProperty<string>()
                               .IsNotDecorated()
                               .Result);
        }
    }
}