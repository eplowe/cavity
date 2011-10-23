namespace Cavity.Models
{
    using System;
    using System.Diagnostics;
    using System.Linq;
    using Cavity;
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
        public void opImplicit_PostalAddressFileEntry_KeyStringDictionaryNull()
        {
            PostalAddressFileEntry actual = null as KeyStringDictionary;
            
            Assert.Null(actual);
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
                new KeyStringPair("NUM", "1"),
                new KeyStringPair("DST", "Little Close"),
                new KeyStringPair("STM", "High Street"),
                new KeyStringPair("DDL", "Village"),
                new KeyStringPair("DLO", "Locality"),
                new KeyStringPair("PTN", "Town"),
                new KeyStringPair("POB", "PO Box 123"),
                new KeyStringPair("PCD", "AB10 1AA"),
                new KeyStringPair("SCD", "12345"),
                new KeyStringPair("CAT", "L")
            };

            Assert.Equal("Department", actual.Organization.Department);
            Assert.Equal("Organisation", actual.Organization.Name);
            Assert.Equal("Flat A", actual.Address.SubBuildingName);
            Assert.Equal("Building", actual.Address.BuildingName);
            Assert.Equal("1", actual.Address.BuildingNumber);
            Assert.Equal("Little Close", actual.Address.DependentStreet);
            Assert.Equal("High Street", actual.Address.MainStreet);
            Assert.Equal("Village", actual.Address.DoubleDependentLocality);
            Assert.Equal("Locality", actual.Address.DependentLocality);
            Assert.Equal("Town", actual.Address.PostTown);
            Assert.Equal("PO Box 123", actual.Address.PostOfficeBox);
            Assert.Equal("AB10 1AA", actual.Address.Postcode.Unit);
            Assert.Equal(12345, actual.SortCode);
            Assert.IsType<LargeUserCategory>(actual.Category);
        }

        [Fact]
        // [Fact(Skip = "This test is not to be run all the time.")]
        public void verify_against_full_postal_address_file()
        {
            foreach (PostalAddressFileEntry entry in new CsvFile(@"F:\Data\PAF (Postcoder)\2011-10\ACPC4135P1.csv"))
            {
                try
                {
                    Assert.NotNull(entry.Address.Postcode.Unit);
                }
                catch (Exception)
                {
                    Trace.WriteLine(entry);
                    throw;
                }
            }
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
        public void prop_Origin()
        {
            Assert.NotNull(new PropertyExpectations<PostalAddressFileEntry>(x => x.Origin)
                               .IsAutoProperty<DataOrigin>()
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