## NuGet Packages ##

http://nuget.org/List/Packages/Cavity.Data.Xml.Xunit

## using Cavity.Data _[Facts](http://code.google.com/p/cavity/source/browse/#svn%2Ftrunk%2Fsrc%2FCavity%20Data%20(XML)%2FClass%20Libraries%2FData.Xml.Xunit.Facts%2FData)_ ##

[XmlDataAttribute](http://code.google.com/p/cavity/source/browse/trunk/src/Cavity%20Data%20(XML)/Class%20Libraries/Data.Xml.Xunit/Data/XmlDataAttribute.cs)

To get the native XML types:

```
[Theory]
[XmlData("<example />")]
public void test(XmlDocument xml)
{
    ...
}
```

```
[Theory]
[XmlData("<example />")]
public void test(IXPathNavigable xml)
{
    ...
}
```

```
[Theory]
[XmlData("<example />")]
public void test(XPathNavigator xml)
{
    ...
}
```

```
[Theory]
[XmlData("<example />")]
public void test(XDocument xml)
{
    ...
}
```

```
[Theory]
[XmlData("<one />", "<two />")]
public void test(XmlDocument one, XmlDocument two)
{
    ...
}
```

or deserialize the Xml:

```
[Theory]
[XmlData("<example />")]
public void test(Example obj)
{
    ...
}
```

```
[Theory]
[XmlData("<one />", "<two />")]
public void test(Class1 one, Class2 two)
{
    ...
}
```

[XmlFileAttribute](http://code.google.com/p/cavity/source/browse/trunk/src/Cavity%20Data%20(Xml)/Class%20Libraries/Data.Xml.Xunit/Data/XmlFileAttribute.cs)

To get the native XML types:

```
[Theory]
[XmlFile("example.xml")]
public void test(XmlDocument xml)
{
    ...
}
```

```
[Theory]
[XmlFile("example.xml")]
public void test(IXPathNavigable xml)
{
    ...
}
```

```
[Theory]
[XmlFile("example.xml")]
public void test(XPathNavigator xml)
{
    ...
}
```

```
[Theory]
[XmlFile("example.xml")]
public void test(XDocument xml)
{
    ...
}
```

```
[Theory]
[XmlFile("one.xml", "two.xml")]
public void test(XmlDocument one, XmlDocument two)
{
    ...
}
```

or deserialize the Xml:

```
[Theory]
[XmlFile("example.xml")]
public void test(Example obj)
{
    ...
}
```

```
[Theory]
[XmlFile("one.xml", "two.xml")]
public void test(Class1 one, Class2 two)
{
    ...
}
```

or deserialize a serialized DataSet:

```
[Theory]
[XmlFile("dataset.xml")]
public void test(DataSet data)
{
    ...
}
```

[XmlUriAttribute](http://code.google.com/p/cavity/source/browse/trunk/src/Cavity%20Data%20(Xml)/Class%20Libraries/Data.Xml.Xunit/Data/XmlUriAttribute.cs)

To get the native XML types:

```
[Theory]
[XmlUri("http://www.alan-dean.com/example.xml")]
public void test(XmlDocument xml)
{
    ...
}
```

```
[Theory]
[XmlUri("http://www.alan-dean.com/example.xml")]
public void test(IXPathNavigable xml)
{
    ...
}
```

```
[Theory]
[XmlUri("http://www.alan-dean.com/example.xml")]
public void test(XPathNavigator xml)
{
    ...
}
```

```
[Theory]
[XmlUri("http://www.alan-dean.com/example.xml")]
public void test(XDocument xml)
{
    ...
}
```

```
[Theory]
[XmlUri("http://www.alan-dean.com/one.xml", "http://www.alan-dean.com/two.xml")]
public void test(XmlDocument one, XmlDocument two)
{
    ...
}
```

or deserialize the Xml:

```
[Theory]
[XmlUri("http://www.alan-dean.com/example.xml")]
public void test(Example obj)
{
    ...
}
```

```
[Theory]
[XmlUri("http://www.alan-dean.com/one.xml", "http://www.alan-dean.com/two.xml")]
public void test(Class1 one, Class2 two)
{
    ...
}
```

or deserialize a serialized DataSet:

```
[Theory]
[XmlUri("http://www.alan-dean.com/dataset.xml")]
public void test(DataSet data)
{
    ...
}
```