namespace Cavity.Models
{
    using System;

    using Cavity.Collections;

    using Xunit;

    public sealed class BritishAddressFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<BritishAddress>()
                            .DerivesFrom<KeyStringDictionary>()
                            .IsConcreteClass()
                            .IsUnsealed()
                            .HasDefaultConstructor()
                            .Serializable()
                            .Result);
        }

        [Fact]
        public void ctor()
        {
            Assert.NotNull(new BritishAddress());
        }

        [Fact]
        public void op_ToString()
        {
            var expected = string.Empty;
            var actual = new BritishAddress().ToString();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_ToString_whenFull()
        {
            var obj = new BritishAddress
                          {
                              SubBuildingName = "Flat A", 
                              BuildingName = "Big House", 
                              PostOfficeBox = "PO Box 123", 
                              BuildingNumber = "12", 
                              DependentStreet = "Little Close", 
                              MainStreet = "High Street", 
                              DoubleDependentLocality = "Local Village", 
                              DependentLocality = "Locality", 
                              PostTown = "Bigton", 
                              Postcode = "AB1 2ZZ", 
                              TraditionalCounty = "Countyshire"
                          };

            var expected = "Flat A{0}Big House{0}PO Box 123{0}12 Little Close{0}High Street{0}Local Village{0}Locality{0}Bigton{0}AB1 2ZZ{0}Countyshire{0}".FormatWith(Environment.NewLine);
            var actual = obj.ToString();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_ToString_whenMainStreetName()
        {
            var obj = new BritishAddress
                          {
                              BuildingName = "Big House", 
                              MainStreet = "High Street", 
                              PostTown = "Bigton", 
                              Postcode = "AB1 2ZZ", 
                              TraditionalCounty = "Countyshire"
                          };

            var expected = "Big House{0}High Street{0}Bigton{0}AB1 2ZZ{0}Countyshire{0}".FormatWith(Environment.NewLine);
            var actual = obj.ToString();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_ToString_whenMainStreetNumber()
        {
            var obj = new BritishAddress
                          {
                              BuildingName = "Big House", 
                              BuildingNumber = "12", 
                              MainStreet = "High Street", 
                              PostTown = "Bigton", 
                              Postcode = "AB1 2ZZ", 
                              TraditionalCounty = "Countyshire"
                          };

            var expected = "Big House{0}12 High Street{0}Bigton{0}AB1 2ZZ{0}Countyshire{0}".FormatWith(Environment.NewLine);
            var actual = obj.ToString();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_ToString_whenNoCounty()
        {
            var obj = new BritishAddress
                          {
                              BuildingName = "Big House", 
                              MainStreet = "High Street", 
                              PostTown = "Bigton", 
                              Postcode = "AB1 2ZZ"
                          };

            var expected = "Big House{0}High Street{0}Bigton{0}AB1 2ZZ{0}".FormatWith(Environment.NewLine);
            var actual = obj.ToString();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void prop_AdministrativeCounty()
        {
            Assert.NotNull(new PropertyExpectations<BritishAddress>(x => x.AdministrativeCounty)
                               .IsAutoProperty<string>()
                               .IsNotDecorated()
                               .Result);
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
        public void prop_FormerPostalCounty()
        {
            Assert.NotNull(new PropertyExpectations<BritishAddress>(x => x.FormerPostalCounty)
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
        public void prop_Postcode()
        {
            Assert.NotNull(new PropertyExpectations<BritishAddress>(x => x.Postcode)
                               .IsAutoProperty<BritishPostcode>()
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

        [Fact]
        public void prop_TraditionalCounty()
        {
            Assert.NotNull(new PropertyExpectations<BritishAddress>(x => x.TraditionalCounty)
                               .IsAutoProperty<string>()
                               .IsNotDecorated()
                               .Result);
        }
    }
}