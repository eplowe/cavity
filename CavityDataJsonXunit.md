## NuGet Packages ##

http://nuget.org/List/Packages/Cavity.Data.Json.Xunit

## using Cavity.Data _[Facts](http://code.google.com/p/cavity/source/browse/#svn%2Ftrunk%2Fsrc%2FCavity%20Data%20(JSON)%2FClass%20Libraries%2FData.Json.Xunit.Facts%2FData)_ ##

[JsonDataAttribute](http://code.google.com/p/cavity/source/browse/trunk/src/Cavity%20Data%20(JSON)/Class%20Libraries/Data.Json.Xunit/Data/JsonDataAttribute.cs)

To get the Json.NET object:

```
[Theory]
[JsonData("{ \"Value\": 999 }")]
public void test(JObject obj)
{
    ...
}
```

```
[Theory]
[JsonData("{ \"Value\": 1 }", "{ \"Value\": 2 }")]
public void test(JObject one, JObject two)
{
    ...
}
```

Or deserialize the JSON:

```
[Theory]
[JsonData("example.json")]
public void test(Class1 obj)
{
    ...
}
```

```
[Theory]
[JsonData("{ \"Value\": 1 }", "{ \"Value\": 2 }")]
public void test(Class1 one, Class1 two)
{
    ...
}
```

[JsonFileAttribute](http://code.google.com/p/cavity/source/browse/trunk/src/Cavity%20Data%20(JSON)/Class%20Libraries/Data.Json.Xunit/Data/JsonFileAttribute.cs)

To get the Json.NET object:

```
[Theory]
[JsonFile("example.json")]
public void test(JObject obj)
{
    ...
}
```

```
[Theory]
[JsonFile("one.json", "two.json")]
public void test(JObject one, JObject two)
{
    ...
}
```

Or deserialize the JSON:

```
[Theory]
[JsonFile("example.json")]
public void test(Class1 obj)
{
    ...
}
```

```
[Theory]
[JsonFile("one.json", "two.json")]
public void test(Class1 one, Class1 two)
{
    ...
}
```

[JsonUriAttribute](http://code.google.com/p/cavity/source/browse/trunk/src/Cavity%20Data%20(JSON)/Class%20Libraries/Data.Json.Xunit/Data/JsonUriAttribute.cs)

To get the Json.NET object:

```
[Theory]
[JsonUri("http://www.alan-dean.com/example.json")]
public void test(JObject obj)
{
    ...
}
```

```
[Theory]
[JsonUri("http://www.alan-dean.com/one.json", "http://www.alan-dean.com/two.json")]
public void test(JObject one, JObject two)
{
    ...
}
```

Or deserialize the JSON:

```
[Theory]
[JsonUri("http://www.alan-dean.com/example.json")]
public void test(Class1 obj)
{
    ...
}
```

```
[Theory]
[JsonUri("http://www.alan-dean.com/one.json", "http://www.alan-dean.com/two.json")]
public void test(Class1 one, Class1 two)
{
    ...
}
```