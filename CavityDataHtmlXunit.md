## NuGet Packages ##

http://nuget.org/List/Packages/Cavity.Data.Html.Xunit

## using Cavity.Data _[Facts](http://code.google.com/p/cavity/source/browse/#svn%2Ftrunk%2Fsrc%2FCavity%20Data%20(HTML)%2FClass%20Libraries%2FData.Html.Xunit.Facts%2FData)_ ##

[HtmlFileAttribute](http://code.google.com/p/cavity/source/browse/trunk/src/Cavity%20Data%20(HTML)/Class%20Libraries/Data.Html.Xunit/Data/HtmlFileAttribute.cs)

To get the HTML tables in a dataset:

```
[Theory]
[HtmlFile("example.html")]
public void test(DataSet data)
{
    ...
}
```

or to get the native HTML:

```
[Theory]
[HtmlFile("example.html")]
public void test(HtmlDocument html)
{
    ...
}
```

```
[Theory]
[HtmlFile("one.html", "two.html")]
public void test(HtmlDocument one, HtmlDocument two)
{
    ...
}
```

or to get the XPath types:

```
[Theory]
[HtmlFile("example.html")]
public void test(IXPathNavigable html)
{
    ...
}
```

```
[Theory]
[HtmlFile("example.html")]
public void test(XPathNavigator html)
{
    ...
}
```

[HtmlUriAttribute](http://code.google.com/p/cavity/source/browse/trunk/src/Cavity%20Data%20(HTML)/Class%20Libraries/Data.Html.Xunit/Data/HtmlUriAttribute.cs)

To get the HTML tables in a dataset:

```
[Theory]
[HtmlUri("http://developer.yahoo.com/yui/examples/datasource/datasource_table_to_array.html")]
public void test(DataSet data)
{
    ...
}
```

or to get the native HTML:

```
[Theory]
[HtmlUri("http://www.alan-dean.com/example.html")]
public void test(HtmlDocument html)
{
    ...
}
```

```
[Theory]
[HtmlUri("http://www.alan-dean.com/one.html", "http://www.alan-dean.com/two.html")]
public void test(HtmlDocument one, HtmlDocument two)
{
    ...
}
```

or to get the XPath types:

```
[Theory]
[HtmlUri("http://www.alan-dean.com/example.html")]
public void test(IXPathNavigable html)
{
    ...
}
```

```
[Theory]
[HtmlUri("http://www.alan-dean.com/example.html")]
public void test(XPathNavigator html)
{
    ...
}
```