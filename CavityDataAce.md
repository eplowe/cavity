## NuGet Packages ##

http://nuget.org/List/Packages/Cavity.Data.Ace.x86

http://nuget.org/List/Packages/Cavity.Data.Ace.x64

## using Cavity.Data _[Facts](http://code.google.com/p/cavity/source/browse/#svn%2Ftrunk%2Fsrc%2FCavity%20Data%20(ACE)%2FClass%20Libraries%2FData.Ace.Facts%2FData)_ ##

[ExcelFile](http://code.google.com/p/cavity/source/browse/trunk/src/Cavity%20Data%20(ACE)/Class%20Libraries/Data.Ace/Data/ExcelFile.cs)

```
foreach (ExcelWorksheet worksheet in new ExcelFile(file))
{
    var name = worksheet.Name;
}
```

[ExcelWorksheet](http://code.google.com/p/cavity/source/browse/trunk/src/Cavity%20Data%20(ACE)/Class%20Libraries/Data.Ace/Data/ExcelWorksheet.cs)

```
var file = new FileInfo("NameValue.xlsx");

foreach (KeyStringDictionary entry in new ExcelWorksheet(file, "Sheet1$"))
{
    var value = entry.Value<string>("COLUMN NAME");
}
```