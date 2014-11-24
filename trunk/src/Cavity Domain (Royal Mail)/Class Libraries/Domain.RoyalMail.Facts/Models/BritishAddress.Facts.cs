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
        public void op_ToMarketingFormat_KeyStringDictionary()
        {
            var entry = new KeyStringDictionary
                            {
                                { "SBN", "Flat 1" },
                                { "BNA", "Tall House" },
                                { "NUM", "123" },
                                { "DST", "Little Lane" },
                                { "STM", "High Street" },
                                { "DDL", "Local Wood" },
                                { "DLO", "Wide Area" },
                                { "PTN", "Postal Town" },
                                { "PCD", "AA1 2ZZ" },
                            };

            var obj = BritishAddress.ToMarketingFormat(entry);

            Assert.Equal("Flat 1", obj["ADDRESS 1"]);
            Assert.Equal("Tall House", obj["ADDRESS 2"]);
            Assert.Equal("123 Little Lane", obj["ADDRESS 3"]);
            Assert.Equal("High Street", obj["ADDRESS 4"]);
            Assert.Equal("Local Wood", obj["ADDRESS 5"]);
            Assert.Equal("Wide Area", obj["ADDRESS 6"]);
            Assert.Equal("AA1 2ZZ", obj["POSTCODE"]);
        }

        [Fact]
        public void op_ToMarketingFormat_KeyStringDictionaryNull()
        {
            Assert.Throws<ArgumentNullException>(() => BritishAddress.ToMarketingFormat(null));
        }

        [Fact]
        public void op_ToMarketingFormat_KeyStringDictionary_emptyBNA()
        {
            var entry = new KeyStringDictionary
                            {
                                { "SBN", string.Empty },
                                { "BNA", string.Empty },
                                { "NUM", "123" },
                                { "DST", "Little Lane" },
                                { "STM", "High Street" },
                                { "DDL", "Local Wood" },
                                { "DLO", "Wide Area" },
                                { "PTN", "Postal Town" },
                                { "PCD", "AA1 2ZZ" },
                            };

            var obj = BritishAddress.ToMarketingFormat(entry);

            Assert.Equal("123 Little Lane", obj["ADDRESS 1"]);
            Assert.Equal("High Street", obj["ADDRESS 2"]);
            Assert.Equal("Local Wood", obj["ADDRESS 3"]);
            Assert.Equal("Wide Area", obj["ADDRESS 4"]);
            Assert.Equal("Postal Town", obj["ADDRESS 5"]);
            Assert.Equal(string.Empty, obj["ADDRESS 6"]);
            Assert.Equal("AA1 2ZZ", obj["POSTCODE"]);
        }

        [Fact]
        public void op_ToMarketingFormat_KeyStringDictionary_emptyDDL()
        {
            var entry = new KeyStringDictionary
                            {
                                { "SBN", string.Empty },
                                { "BNA", string.Empty },
                                { "NUM", string.Empty },
                                { "DST", string.Empty },
                                { "STM", string.Empty },
                                { "DDL", string.Empty },
                                { "DLO", "Wide Area" },
                                { "PTN", "Postal Town" },
                                { "PCD", "AA1 2ZZ" },
                            };

            var obj = BritishAddress.ToMarketingFormat(entry);

            Assert.Equal("Wide Area", obj["ADDRESS 1"]);
            Assert.Equal("Postal Town", obj["ADDRESS 2"]);
            Assert.Equal(string.Empty, obj["ADDRESS 3"]);
            Assert.Equal(string.Empty, obj["ADDRESS 4"]);
            Assert.Equal(string.Empty, obj["ADDRESS 5"]);
            Assert.Equal(string.Empty, obj["ADDRESS 6"]);
            Assert.Equal("AA1 2ZZ", obj["POSTCODE"]);
        }

        [Fact]
        public void op_ToMarketingFormat_KeyStringDictionary_emptyDLO()
        {
            var entry = new KeyStringDictionary
                            {
                                { "SBN", string.Empty },
                                { "BNA", string.Empty },
                                { "NUM", string.Empty },
                                { "DST", string.Empty },
                                { "STM", string.Empty },
                                { "DDL", string.Empty },
                                { "DLO", string.Empty },
                                { "PTN", "Postal Town" },
                                { "PCD", "AA1 2ZZ" },
                            };

            var obj = BritishAddress.ToMarketingFormat(entry);

            Assert.Equal("Postal Town", obj["ADDRESS 1"]);
            Assert.Equal(string.Empty, obj["ADDRESS 2"]);
            Assert.Equal(string.Empty, obj["ADDRESS 3"]);
            Assert.Equal(string.Empty, obj["ADDRESS 4"]);
            Assert.Equal(string.Empty, obj["ADDRESS 5"]);
            Assert.Equal(string.Empty, obj["ADDRESS 6"]);
            Assert.Equal("AA1 2ZZ", obj["POSTCODE"]);
        }

        [Fact]
        public void op_ToMarketingFormat_KeyStringDictionary_emptyDST()
        {
            var entry = new KeyStringDictionary
                            {
                                { "SBN", string.Empty },
                                { "BNA", string.Empty },
                                { "NUM", "123" },
                                { "DST", string.Empty },
                                { "STM", "High Street" },
                                { "DDL", "Local Wood" },
                                { "DLO", "Wide Area" },
                                { "PTN", "Postal Town" },
                                { "PCD", "AA1 2ZZ" },
                            };

            var obj = BritishAddress.ToMarketingFormat(entry);

            Assert.Equal("123 High Street", obj["ADDRESS 1"]);
            Assert.Equal("Local Wood", obj["ADDRESS 2"]);
            Assert.Equal("Wide Area", obj["ADDRESS 3"]);
            Assert.Equal("Postal Town", obj["ADDRESS 4"]);
            Assert.Equal(string.Empty, obj["ADDRESS 5"]);
            Assert.Equal(string.Empty, obj["ADDRESS 6"]);
            Assert.Equal("AA1 2ZZ", obj["POSTCODE"]);
        }

        [Fact]
        public void op_ToMarketingFormat_KeyStringDictionary_emptyNUM()
        {
            var entry = new KeyStringDictionary
                            {
                                { "SBN", string.Empty },
                                { "BNA", string.Empty },
                                { "NUM", string.Empty },
                                { "DST", string.Empty },
                                { "STM", "High Street" },
                                { "DDL", "Local Wood" },
                                { "DLO", "Wide Area" },
                                { "PTN", "Postal Town" },
                                { "PCD", "AA1 2ZZ" },
                            };

            var obj = BritishAddress.ToMarketingFormat(entry);

            Assert.Equal("High Street", obj["ADDRESS 1"]);
            Assert.Equal("Local Wood", obj["ADDRESS 2"]);
            Assert.Equal("Wide Area", obj["ADDRESS 3"]);
            Assert.Equal("Postal Town", obj["ADDRESS 4"]);
            Assert.Equal(string.Empty, obj["ADDRESS 5"]);
            Assert.Equal(string.Empty, obj["ADDRESS 6"]);
            Assert.Equal("AA1 2ZZ", obj["POSTCODE"]);
        }

        [Fact]
        public void op_ToMarketingFormat_KeyStringDictionary_emptyPTN()
        {
            var entry = new KeyStringDictionary
                            {
                                { "SBN", string.Empty },
                                { "BNA", string.Empty },
                                { "NUM", string.Empty },
                                { "DST", string.Empty },
                                { "STM", string.Empty },
                                { "DDL", string.Empty },
                                { "DLO", string.Empty },
                                { "PTN", string.Empty },
                                { "PCD", "AA1 2ZZ" },
                            };

            var obj = BritishAddress.ToMarketingFormat(entry);

            Assert.Equal(string.Empty, obj["ADDRESS 1"]);
            Assert.Equal(string.Empty, obj["ADDRESS 2"]);
            Assert.Equal(string.Empty, obj["ADDRESS 3"]);
            Assert.Equal(string.Empty, obj["ADDRESS 4"]);
            Assert.Equal(string.Empty, obj["ADDRESS 5"]);
            Assert.Equal(string.Empty, obj["ADDRESS 6"]);
            Assert.Equal("AA1 2ZZ", obj["POSTCODE"]);
        }

        [Fact]
        public void op_ToMarketingFormat_KeyStringDictionary_emptySBN()
        {
            var entry = new KeyStringDictionary
                            {
                                { "SBN", string.Empty },
                                { "BNA", "Tall House" },
                                { "NUM", "123" },
                                { "DST", "Little Lane" },
                                { "STM", "High Street" },
                                { "DDL", "Local Wood" },
                                { "DLO", "Wide Area" },
                                { "PTN", "Postal Town" },
                                { "PCD", "AA1 2ZZ" },
                            };

            var obj = BritishAddress.ToMarketingFormat(entry);

            Assert.Equal("Tall House", obj["ADDRESS 1"]);
            Assert.Equal("123 Little Lane", obj["ADDRESS 2"]);
            Assert.Equal("High Street", obj["ADDRESS 3"]);
            Assert.Equal("Local Wood", obj["ADDRESS 4"]);
            Assert.Equal("Wide Area", obj["ADDRESS 5"]);
            Assert.Equal("Postal Town", obj["ADDRESS 6"]);
            Assert.Equal("AA1 2ZZ", obj["POSTCODE"]);
        }

        [Fact]
        public void op_ToMarketingFormat_KeyStringDictionary_emptySTM()
        {
            var entry = new KeyStringDictionary
                            {
                                { "SBN", string.Empty },
                                { "BNA", string.Empty },
                                { "NUM", string.Empty },
                                { "DST", string.Empty },
                                { "STM", string.Empty },
                                { "DDL", "Local Wood" },
                                { "DLO", "Wide Area" },
                                { "PTN", "Postal Town" },
                                { "PCD", "AA1 2ZZ" },
                            };

            var obj = BritishAddress.ToMarketingFormat(entry);

            Assert.Equal("Local Wood", obj["ADDRESS 1"]);
            Assert.Equal("Wide Area", obj["ADDRESS 2"]);
            Assert.Equal("Postal Town", obj["ADDRESS 3"]);
            Assert.Equal(string.Empty, obj["ADDRESS 4"]);
            Assert.Equal(string.Empty, obj["ADDRESS 5"]);
            Assert.Equal(string.Empty, obj["ADDRESS 6"]);
            Assert.Equal("AA1 2ZZ", obj["POSTCODE"]);
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