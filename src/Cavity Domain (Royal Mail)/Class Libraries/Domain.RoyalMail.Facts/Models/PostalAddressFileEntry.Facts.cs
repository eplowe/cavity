namespace Cavity.Models
{
    using System;
    using System.Collections.Generic;
    using Cavity.Collections;
    using Cavity.Data;
    using Xunit;

    public sealed class PostalAddressFileEntryFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<PostalAddressFileEntry>()
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
            Assert.NotNull(new PostalAddressFileEntry());
        }

        [Fact]
        public void opImplicit_PostalAddressFileEntry_KeyStringDictionary()
        {
            PostalAddressFileEntry actual = new KeyStringDictionary
            {
                new KeyStringPair("ORD", "Department"), 
                new KeyStringPair("ORC", "Organisation"), 
                new KeyStringPair("SBN", "Flat A"), 
                new KeyStringPair("BNA", "Building"), 
                new KeyStringPair("POB", "PO Box 123"), 
                new KeyStringPair("NUM", "1"), 
                new KeyStringPair("DST", "Little Close"), 
                new KeyStringPair("STM", "High Street"), 
                new KeyStringPair("DDL", "Village"), 
                new KeyStringPair("DLO", "Locality"), 
                new KeyStringPair("PTN", "Town"), 
                new KeyStringPair("CTA", "Countyshire"), 
                new KeyStringPair("CTP", "Postalshire"), 
                new KeyStringPair("CTT", "Oldshire"), 
                new KeyStringPair("PCD", "AB10 1AA"), 
                new KeyStringPair("SCD", "12345"), 
                new KeyStringPair("CAT", "L"), 
                new KeyStringPair("NDP", "12"), 
                new KeyStringPair("DPX", "1A8"), 
                new KeyStringPair("URN", "123456789"), 
                new KeyStringPair("MOC", "9999"), 
                new KeyStringPair("MRC", "9876"), 
                new KeyStringPair("UMR", "987654321"), 
                new KeyStringPair("DTO", "P")
            };

            Assert.Equal("Department", actual.Organization.Department);
            Assert.Equal("Organisation", actual.Organization.Name);
            Assert.Equal("Flat A", actual.Address.SubBuildingName);
            Assert.Equal("PO Box 123", actual.Address.PostOfficeBox);
            Assert.Equal("Building", actual.Address.BuildingName);
            Assert.Equal("1", actual.Address.BuildingNumber);
            Assert.Equal("Little Close", actual.Address.DependentStreet);
            Assert.Equal("High Street", actual.Address.MainStreet);
            Assert.Equal("Village", actual.Address.DoubleDependentLocality);
            Assert.Equal("Locality", actual.Address.DependentLocality);
            Assert.Equal("Town", actual.Address.PostTown);
            Assert.Equal("Countyshire", actual.Address.AdministrativeCounty);
            Assert.Equal("Postalshire", actual.Address.FormerPostalCounty);
            Assert.Equal("Oldshire", actual.Address.TraditionalCounty);
            Assert.Equal("AB10 1AA", actual.Address.Postcode.Unit);
            Assert.IsType<LargeUserCategory>(actual.Category);
            Assert.Equal("1A8", actual.DeliveryPointSuffix);
            Assert.Equal(9999, actual.MultipleOccupancyCount);
            Assert.Equal(9876, actual.MultipleResidencyRecordCount);
            Assert.Equal(12, actual.NumberOfDeliveryPoints);
            Assert.Equal('P', actual.Origin);
            Assert.Equal(12345, actual.SortCode);
            Assert.Equal(123456789, actual.UniqueDeliveryPointReferenceNumber);
            Assert.Equal(987654321, actual.UniqueMultipleResidenceReferenceNumber);
        }

        [Fact]
        public void opImplicit_PostalAddressFileEntry_KeyStringDictionaryEmpty()
        {
            PostalAddressFileEntry actual = new KeyStringDictionary();

            Assert.Null(actual.Organization.Department);
            Assert.Null(actual.Organization.Name);
            Assert.Null(actual.Address.SubBuildingName);
            Assert.Null(actual.Address.PostOfficeBox);
            Assert.Null(actual.Address.BuildingName);
            Assert.Null(actual.Address.BuildingNumber);
            Assert.Null(actual.Address.DependentStreet);
            Assert.Null(actual.Address.MainStreet);
            Assert.Null(actual.Address.DoubleDependentLocality);
            Assert.Null(actual.Address.DependentLocality);
            Assert.Null(actual.Address.PostTown);
            Assert.Null(actual.Address.AdministrativeCounty);
            Assert.Null(actual.Address.FormerPostalCounty);
            Assert.Null(actual.Address.TraditionalCounty);
            Assert.Null(actual.Address.Postcode);
            Assert.Null(actual.Category);
            Assert.Null(actual.DeliveryPointSuffix);
            Assert.False(actual.MultipleOccupancyCount.HasValue);
            Assert.False(actual.MultipleResidencyRecordCount.HasValue);
            Assert.False(actual.NumberOfDeliveryPoints.HasValue);
            Assert.False(actual.Origin.HasValue);
            Assert.False(actual.SortCode.HasValue);
            Assert.False(actual.UniqueDeliveryPointReferenceNumber.HasValue);
            Assert.False(actual.UniqueMultipleResidenceReferenceNumber.HasValue);
        }

        [Fact]
        public void opImplicit_PostalAddressFileEntry_KeyStringDictionaryEmptyValues()
        {
            PostalAddressFileEntry actual = new KeyStringDictionary
            {
                new KeyStringPair("ORD", string.Empty), 
                new KeyStringPair("ORC", string.Empty), 
                new KeyStringPair("SBN", string.Empty), 
                new KeyStringPair("BNA", string.Empty), 
                new KeyStringPair("POB", string.Empty), 
                new KeyStringPair("NUM", string.Empty), 
                new KeyStringPair("DST", string.Empty), 
                new KeyStringPair("STM", string.Empty), 
                new KeyStringPair("DDL", string.Empty), 
                new KeyStringPair("DLO", string.Empty), 
                new KeyStringPair("PTN", string.Empty), 
                new KeyStringPair("CTA", string.Empty), 
                new KeyStringPair("CTP", string.Empty), 
                new KeyStringPair("CTT", string.Empty), 
                new KeyStringPair("PCD", string.Empty), 
                new KeyStringPair("SCD", string.Empty), 
                new KeyStringPair("CAT", string.Empty), 
                new KeyStringPair("NDP", string.Empty), 
                new KeyStringPair("DPX", string.Empty), 
                new KeyStringPair("URN", string.Empty), 
                new KeyStringPair("MOC", string.Empty), 
                new KeyStringPair("MRC", string.Empty), 
                new KeyStringPair("UMR", string.Empty), 
                new KeyStringPair("DTO", string.Empty)
            };

            Assert.Equal(string.Empty, actual.Organization.Department);
            Assert.Equal(string.Empty, actual.Organization.Name);
            Assert.Equal(string.Empty, actual.Address.SubBuildingName);
            Assert.Equal(string.Empty, actual.Address.PostOfficeBox);
            Assert.Equal(string.Empty, actual.Address.BuildingName);
            Assert.Equal(string.Empty, actual.Address.BuildingNumber);
            Assert.Equal(string.Empty, actual.Address.DependentStreet);
            Assert.Equal(string.Empty, actual.Address.MainStreet);
            Assert.Equal(string.Empty, actual.Address.DoubleDependentLocality);
            Assert.Equal(string.Empty, actual.Address.DependentLocality);
            Assert.Equal(string.Empty, actual.Address.AdministrativeCounty);
            Assert.Equal(string.Empty, actual.Address.FormerPostalCounty);
            Assert.Equal(string.Empty, actual.Address.TraditionalCounty);
            Assert.Equal(string.Empty, actual.Address.PostTown);
            Assert.Null(actual.Address.Postcode.Area);
            Assert.Null(actual.Category);
            Assert.Equal(string.Empty, actual.DeliveryPointSuffix);
            Assert.False(actual.MultipleOccupancyCount.HasValue);
            Assert.False(actual.MultipleResidencyRecordCount.HasValue);
            Assert.False(actual.NumberOfDeliveryPoints.HasValue);
            Assert.False(actual.Origin.HasValue);
            Assert.False(actual.SortCode.HasValue);
            Assert.False(actual.UniqueDeliveryPointReferenceNumber.HasValue);
            Assert.False(actual.UniqueMultipleResidenceReferenceNumber.HasValue);
        }

        [Fact]
        public void opImplicit_PostalAddressFileEntry_KeyStringDictionaryNull()
        {
            PostalAddressFileEntry actual = null as KeyStringDictionary;

            Assert.Null(actual);
        }

        [Fact]
        public void op_ToString()
        {
            PostalAddressFileEntry entry = new KeyStringDictionary
            {
                new KeyStringPair("ORD", "A"), 
                new KeyStringPair("ORC", "B"), 
                new KeyStringPair("SBN", "C"), 
                new KeyStringPair("BNA", "D"), 
                new KeyStringPair("POB", "E"), 
                new KeyStringPair("NUM", "F"), 
                new KeyStringPair("DST", "G"), 
                new KeyStringPair("STM", "H"), 
                new KeyStringPair("DDL", "I"), 
                new KeyStringPair("DLO", "J"), 
                new KeyStringPair("PTN", "K"), 
                new KeyStringPair("PCD", "AB10 1AA"), 
                new KeyStringPair("CTA", "L"), 
                new KeyStringPair("CTP", "M"), 
                new KeyStringPair("CTT", "N"), 
                new KeyStringPair("SCD", "1"), 
                new KeyStringPair("CAT", "L"), 
                new KeyStringPair("NDP", "2"), 
                new KeyStringPair("DPX", "3"), 
                new KeyStringPair("URN", "4"), 
                new KeyStringPair("MOC", "5"), 
                new KeyStringPair("MRC", "6"), 
                new KeyStringPair("UMR", "7"), 
                new KeyStringPair("DTO", "P")
            };

            const string expected = "A,B,C,D,E,F,G,H,I,J,K,AB10 1AA,L,M,N,1,L,2,3,4,5,6,7,P";
            var actual = entry.ToString();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_ToString_string()
        {
            var entry = new PostalAddressFileEntry
            {
                Address =
                    {
                        SubBuildingName = "A", 
                        PostOfficeBox = string.Empty, 
                        BuildingName = "B", 
                        BuildingNumber = "C", 
                        DependentStreet = "D", 
                        MainStreet = "E", 
                        DoubleDependentLocality = "F", 
                        DependentLocality = "G", 
                        PostTown = "H", 
                        Postcode = "AA1 2ZZ"
                    }, 
                Organization =
                    {
                        Department = string.Empty, 
                        Name = string.Empty
                    }
            };

            const string expected = "A,B,C,D,E,F,G,H,AA1 2ZZ";
            var actual = entry.ToString("SBN,BNA,NUM,DST,STM,DDL,DLO,PTN,PCD");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_ToString_stringEmpty()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new PostalAddressFileEntry().ToString(string.Empty));
        }

        [Fact]
        public void op_ToString_stringInvalid()
        {
            Assert.Throws<KeyNotFoundException>(() => new PostalAddressFileEntry().ToString("INVALID"));
        }

        [Fact]
        public void op_ToString_stringNull()
        {
            Assert.Throws<ArgumentNullException>(() => new PostalAddressFileEntry().ToString(null as string));
        }

        [Fact]
        public void op_ToString_whenEmpty()
        {
            PostalAddressFileEntry entry = new KeyStringDictionary();

            const string expected = ",,,,,,,,,,,,,,,,,,,,,,,";
            var actual = entry.ToString();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_ToString_whenEmptyValues()
        {
            PostalAddressFileEntry entry = new KeyStringDictionary
            {
                new KeyStringPair("ORD", string.Empty), 
                new KeyStringPair("ORC", string.Empty), 
                new KeyStringPair("SBN", string.Empty), 
                new KeyStringPair("BNA", string.Empty), 
                new KeyStringPair("POB", string.Empty), 
                new KeyStringPair("NUM", string.Empty), 
                new KeyStringPair("DST", string.Empty), 
                new KeyStringPair("STM", string.Empty), 
                new KeyStringPair("DDL", string.Empty), 
                new KeyStringPair("DLO", string.Empty), 
                new KeyStringPair("PTN", string.Empty), 
                new KeyStringPair("PCD", string.Empty), 
                new KeyStringPair("CTA", string.Empty), 
                new KeyStringPair("CTP", string.Empty), 
                new KeyStringPair("CTT", string.Empty), 
                new KeyStringPair("SCD", string.Empty), 
                new KeyStringPair("CAT", string.Empty), 
                new KeyStringPair("NDP", string.Empty), 
                new KeyStringPair("DPX", string.Empty), 
                new KeyStringPair("URN", string.Empty), 
                new KeyStringPair("MOC", string.Empty), 
                new KeyStringPair("MRC", string.Empty), 
                new KeyStringPair("UMR", string.Empty), 
                new KeyStringPair("DTO", string.Empty)
            };

            const string expected = ",,,,,,,,,,,,,,,,,,,,,,,";
            var actual = entry.ToString();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void prop_Address()
        {
            Assert.NotNull(new PropertyExpectations<PostalAddressFileEntry>(x => x.Address)
                               .TypeIs<BritishAddress>()
                               .DefaultValueIsNotNull()
                               .IsNotDecorated()
                               .Result);
        }

        [Fact]
        public void prop_Category()
        {
            Assert.NotNull(new PropertyExpectations<PostalAddressFileEntry>(x => x.Category)
                               .IsAutoProperty<IUserCategory>()
                               .IsNotDecorated()
                               .Result);
        }

        [Fact]
        public void prop_DeliveryPointSuffix()
        {
            Assert.NotNull(new PropertyExpectations<PostalAddressFileEntry>(x => x.DeliveryPointSuffix)
                               .IsAutoProperty<string>()
                               .IsNotDecorated()
                               .Result);
        }

        [Fact]
        public void prop_MultipleOccupancyCount()
        {
            Assert.NotNull(new PropertyExpectations<PostalAddressFileEntry>(x => x.MultipleOccupancyCount)
                               .IsAutoProperty<int?>()
                               .IsNotDecorated()
                               .Result);
        }

        [Fact]
        public void prop_MultipleResidencyRecordCount()
        {
            Assert.NotNull(new PropertyExpectations<PostalAddressFileEntry>(x => x.MultipleResidencyRecordCount)
                               .IsAutoProperty<int?>()
                               .IsNotDecorated()
                               .Result);
        }

        [Fact]
        public void prop_NumberOfDeliveryPoints()
        {
            Assert.NotNull(new PropertyExpectations<PostalAddressFileEntry>(x => x.NumberOfDeliveryPoints)
                               .IsAutoProperty<int?>()
                               .IsNotDecorated()
                               .Result);
        }

        [Fact]
        public void prop_Organization()
        {
            Assert.NotNull(new PropertyExpectations<PostalAddressFileEntry>(x => x.Organization)
                               .TypeIs<Organization>()
                               .DefaultValueIsNotNull()
                               .IsNotDecorated()
                               .Result);
        }

        [Fact]
        public void prop_Origin()
        {
            Assert.NotNull(new PropertyExpectations<PostalAddressFileEntry>(x => x.Origin)
                               .IsAutoProperty<char?>()
                               .IsNotDecorated()
                               .Result);
        }

        [Fact]
        public void prop_SortCode()
        {
            Assert.NotNull(new PropertyExpectations<PostalAddressFileEntry>(x => x.SortCode)
                               .IsAutoProperty<int?>()
                               .IsNotDecorated()
                               .Result);
        }

        [Fact]
        public void prop_UniqueDeliveryPointReferenceNumber()
        {
            Assert.NotNull(new PropertyExpectations<PostalAddressFileEntry>(x => x.UniqueDeliveryPointReferenceNumber)
                               .IsAutoProperty<int?>()
                               .IsNotDecorated()
                               .Result);
        }

        [Fact]
        public void prop_UniqueMultipleResidenceReferenceNumber()
        {
            Assert.NotNull(new PropertyExpectations<PostalAddressFileEntry>(x => x.UniqueMultipleResidenceReferenceNumber)
                               .IsAutoProperty<int?>()
                               .IsNotDecorated()
                               .Result);
        }
    }
}