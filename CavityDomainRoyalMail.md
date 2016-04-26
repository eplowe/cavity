## NuGet Package ##

http://nuget.org/List/Packages/Cavity.Domain.RoyalMail

## using Cavity.Models _[Facts](http://code.google.com/p/cavity/source/browse/#svn%2Ftrunk%2Fsrc%2FCavity%20Domain%20(Royal%20Mail)%2FClass%20Libraries%2FDomain.RoyalMail.Facts%2FModels)_ ##

[BritishPostcode](http://code.google.com/p/cavity/source/browse/trunk/src/Cavity%20Domain%20(Royal%20Mail)/Class%20Libraries/Domain.RoyalMail/Models/BritishPostcode.cs)

```
BritishPostcode postcode = "AB10 1AA";
```

[PostalAddressFileEntry](http://code.google.com/p/cavity/source/browse/trunk/src/Cavity%20Domain%20(Royal%20Mail)/Class%20Libraries/Domain.RoyalMail/Models/PostalAddressFileEntry.cs)

```
foreach (PostalAddressFileEntry entry in new CsvFile(@"PAF.csv"))
{
}
```

```
var entry = new PostalAddressFileEntry
{
    Address =
    {
        SubBuildingName = "Flat A",
        PostOfficeBox = string.Empty,
        BuildingName = "Long House",
        BuildingNumber = string.Empty,
        DependentStreet = string.Empty,
        MainStreet = "High Street",
        DoubleDependentLocality = string.Empty,
        DependentLocality = string.Empty,
        PostTown = "Bigton",
        Postcode = "AA1 2ZZ"
    },
    Organization = 
    {
        Department = string.Empty,
        Name = string.Empty
    },
    Category = 'R',
    DeliveryPointSuffix = "1A8",
    MultipleOccupancyCount = new int?(),
    MultipleResidencyRecordCount = new int?(),
    NumberOfDeliveryPoints = 1,
    Origin = 'P',
    SortCode = 12345,
    UniqueMultipleResidenceReferenceNumber = new int?(),
    UniqueDeliveryPointReferenceNumber = 12345678
};
```