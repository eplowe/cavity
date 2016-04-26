## NuGet Packages ##

http://nuget.org/List/Packages/Cavity.Data.Csv

## using Cavity.Data _[Facts](http://code.google.com/p/cavity/source/browse/#svn%2Ftrunk%2Fsrc%2FCavity%20Data%20(CSV)%2FClass%20Libraries%2FData.Csv.Facts%2FData)_ ##

[CsvFile](http://code.google.com/p/cavity/source/browse/trunk/src/Cavity%20Data%20(CSV)/Class%20Libraries/Data.Csv/Data/CsvFile.cs)

```
foreach (KeyStringDictionary entry in new CsvFile(@"C:\example.csv"))
{
    var value = entry.Value<string>("COLUMN NAME");
}
```

_Extension Methods_

  * `FormatCommaSeparatedValue()`

```
string value = "example".FormatCommaSeparatedValue();
```