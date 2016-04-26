## NuGet Packages ##

http://nuget.org/List/Packages/Cavity.Data.Csv.Xunit

## using Cavity.Data _[Facts](http://code.google.com/p/cavity/source/browse/#svn%2Ftrunk%2Fsrc%2FCavity%20Data%20(CSV)%2FClass%20Libraries%2FData.Csv.Xunit.Facts%2FData)_ ##

[CsvFileAttribute](http://code.google.com/p/cavity/source/browse/trunk/src/Cavity%20Data%20(CSV)/Class%20Libraries/Data.Csv.Xunit/Data/CsvFileAttribute.cs)

```
[Theory]
[CsvFile("example.csv")]
public void test(CsvFile csv)
{
    foreach (var entry in csv)
    {
    	...
    }
}
```

```
[Theory]
[CsvFile("one.csv", "two.csv")]
public void test(CsvFile one, CsvFile two)
{
    foreach (var entry in csv)
    {
    	...
    }
}
```

```
[Theory]
[CsvFile("example.csv")]
public void test(DataTable table)
{
    foreach (DataRow row in table.Rows)
    {
    	...
    }
}
```

```
[Theory]
[CsvFile("one.csv", "two.csv")]
public void test(DataTable one, DataTable two)
{
    foreach (DataRow row in one.Rows)
    {
    	...
    }
}
```

```
[Theory]
[CsvFile("one.csv", "two.csv")]
public void test(DataSet one)
{
    foreach (DataRow row in data.Tables["one.csv"].Rows)
    {
    	...
    }
}
```

[CsvUriAttribute](http://code.google.com/p/cavity/source/browse/trunk/src/Cavity%20Data%20(CSV)/Class%20Libraries/Data.Csv.Xunit/Data/CsvUriAttribute.cs)

```
[Theory]
[CsvUri("http://www.alan-dean.com/example.csv")]
public void test(CsvFile csv)
{
    foreach (var entry in csv)
    {
    	...
    }
}
```

```
[Theory]
[CsvUri("http://www.alan-dean.com/one.csv", "http://www.alan-dean.com/two.csv")]
public void test(CsvFile one, CsvFile two)
{
    foreach (var entry in csv)
    {
    	...
    }
}
```

```
[Theory]
[CsvUri("http://www.alan-dean.com/example.csv")]
public void test(DataTable table)
{
    foreach (DataRow row in table.Rows)
    {
    	...
    }
}
```

```
[Theory]
[CsvUri("http://www.alan-dean.com/one.csv", "http://www.alan-dean.com/two.csv")]
public void test(DataTable one, DataTable two)
{
    foreach (DataRow row in one.Rows)
    {
    	...
    }
}
```

```
[Theory]
[CsvUri("http://www.alan-dean.com/one.csv", "http://www.alan-dean.com/two.csv")]
public void test(DataSet one)
{
    foreach (DataRow row in data.Tables[0].Rows)
    {
    	...
    }
}
```