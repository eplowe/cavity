## NuGet Package ##

http://nuget.org/List/Packages/Cavity.Core

## using Cavity _[Facts](http://code.google.com/p/cavity/source/browse/#svn%2Ftrunk%2Fsrc%2FCavity%20Core%2FClass%20Libraries%2FCore.Facts)_ ##

[AbsoluteUri](http://code.google.com/p/cavity/source/browse/trunk/src/Cavity%20Core/Class%20Libraries/Core/AbsoluteUri.cs)

```
AbsoluteUri uri = "http://example.com";
```

[AlphaDecimal](http://code.google.com/p/cavity/source/browse/trunk/src/Cavity%20Core/Class%20Libraries/Core/AlphaDecimal.cs)

```
var token = AlphaDecimal.Random();
```

[CommentAttribute](http://code.google.com/p/cavity/source/browse/trunk/src/Cavity%20Core/Class%20Libraries/Core/CommentAttribute.cs)

```
[Comment("Comment on a class")]
class Example
{
    [Comment("Comment on a property")]
    int Foo { get; set; }

    [Comment("Comment on a method")]
    void Bar()
    {
    }
}
```

[ComparableObject](http://code.google.com/p/cavity/source/browse/trunk/src/Cavity%20Core/Class%20Libraries/Core/ComparableObject.cs)

```
class Example : ComparableObject
{
    public int Value { get; set; }

    public override string ToString()
    {
        return Value.ToString();
    }
}
```

[DisposableObject](http://code.google.com/p/cavity/source/browse/trunk/src/Cavity%20Core/Class%20Libraries/Core/DisposableObject.cs)

```
class Example : DisposableObject
{
    protected override void OnDispose()
    {
        // dispose unmanaged resources
    }
}
```

[RelativeUri](http://code.google.com/p/cavity/source/browse/trunk/src/Cavity%20Core/Class%20Libraries/Core/RelativeUri.cs)

```
RelativeUri uri = "/index.html";
```

[ValueObject&lt;T&gt;](http://code.google.com/p/cavity/source/browse/trunk/src/Cavity%20Core/Class%20Libraries/Core/ValueObject%601.cs)

```
class Example : ValueObject<Example>
{
    public Example()
    {
        RegisterProperty(x => x.Value);
    }

    public int Value { get; set; }
}
```

_Extension Methods_

[char](http://code.google.com/p/cavity/source/browse/trunk/src/Cavity%20Core/Class%20Libraries/Core/Char.ExtensionMethods.cs)

  * `IsWhiteSpace()`

```
if (' '.IsWhiteSpace())
{
}
```

[DateTime](http://code.google.com/p/cavity/source/browse/trunk/src/Cavity%20Core/Class%20Libraries/Core/DateTime.ExtensionMethods.cs)

  * `ToFileName()`

```
var file = new FileInfo(DateTime.UtcNow.ToFileName());
```

  * `ToLocalTime()`

```
var pst = TimeZoneInfo.FindSystemTimeZoneById("Pacific Standard Time");

DateTime local = DateTime.UtcNow.ToLocalTime(pst);
```

[Generic](http://code.google.com/p/cavity/source/browse/trunk/src/Cavity%20Core/Class%20Libraries/Core/Generic.ExtensionMethods.cs)

  * `In<T>()`

```
if ('a'.In('a', 'b', 'c'))
{
}
```

  * `EqualsOneOf<T>()`

```
if (0.EqualsOneOf(1, 2, 3))
{
}
```

  * `IsBoundedBy<T>()`

```
if (2.IsBoundedBy(1, 3))
{
}
```

[object](http://code.google.com/p/cavity/source/browse/trunk/src/Cavity%20Core/Class%20Libraries/Core/Object.ExtensionMethods.cs)

  * `ToXmlString()`

```
string value = DateTime.UtcNow.ToXmlString();
```

  * `XmlSerialize()`

```
IXPathNavigable xml = new Example().XmlSerialize();
```

[string](http://code.google.com/p/cavity/source/browse/trunk/src/Cavity%20Core/Class%20Libraries/Core/String.ExtensionMethods.cs)

  * `Contains()`

```
if ("abc".Contains("B", StringComparison.OrdinalIgnoreCase))
{
}
```

  * `IsNullOrWhiteSpace()`

```
if ("  ".IsNullOrWhiteSpace())
{
}
```

  * `EndsWithAny()`

```
if ("example".EndsWithAny(StringComparison.OrdinalIgnoreCase, "this", "or", "that"))
{
}
```

  * `EqualsAny()`

```
if ("fish".EqualsAny(StringComparison.Ordinal, "cat", "dog"))
{
}
```

  * `FormatWith()`

```
string value = "{0:0,0} units of {1} sold.".FormatWith(12345, "stuff");
```

  * `LevenshteinDistance()`

```
if (2 > "Ant".LevenshteinDistance("Aunt"))
{
}
```

  * `NormalizeWhiteSpace()`

```
var nbsp = '\u00A0'.ToString(); // No-Break Space

string space = nbsp.NormalizeWhiteSpace();
```

  * `RemoveAny()`

```
string value = "a.b,c".RemoveAny('.', ',');
```

  * `RemoveAnyDigits()`

```
string value = "a01234b56789c".RemoveAnyDigits();
```

  * `RemoveAnyWhiteSpace()`

```
string value = "e x a m p l e".RemoveAnyWhiteSpace();
```

  * `RemoveDefiniteArticle()`

```
string value = "The Example".RemoveDefiniteArticle();
```

  * `RemoveFromEnd()`

```
string value = "Smith, Mr.".RemoveFromEnd(", Mr.", StringComparison.Ordinal);
```

  * `RemoveFromStart()`

```
string value = "+4402012341234".RemoveFromStart("+44", StringComparison.Ordinal);
```

  * `Replace()`

```
string value = "a->b->c".Replace("->", " to ", StringComparison.OrdinalIgnoreCase);
```

  * `ReplaceAllWith()`

```
string value = "Mr Smith".ReplaceAllWith("Mr.", StringComparison.OrdinalIgnoreCase, "Mr", "Mister");
```

  * `SameIndexesOfEach()`

```
if ("Abba".SameIndexesOfEach('A', 'a'))
{
}
```

  * `Split()`

```
string[] value = "a;;b".Split(';', StringSplitOptions.RemoveEmptyEntries);
```

  * `StartsOrEndsWith()`

```
if ("Abba".StartsOrEndsWith('A', 'z'))
{
}
```

  * `StartsWithAny()`

```
if ("Mr. Smith".StartsWithAny(StringComparison.Ordinal, "Mr.", "Mrs.", "Miss", "Ms"))
{
}
```

  * `To<T>()`

```
int value = "123".To<int>();
```

  * `TryTo<T>()`

```
int? value = "123".TryTo<int?>();
```

  * `ToTitleCase()`

```
string value = "an example".ToTitleCase();
```

  * `XmlDeserialize<T>()`

```
var value = "<example />".XmlDeserialize<Example>();
```

## using Cavity.Collections _[Facts](http://code.google.com/p/cavity/source/browse/#svn%2Ftrunk%2Fsrc%2FCavity%20Core%2FClass%20Libraries%2FCore.Facts%2FCollections)_ ##

[KeyStringDictionary](http://code.google.com/p/cavity/source/browse/trunk/src/Cavity%20Core/Class%20Libraries/Core/Collections/KeyStringDictionary.cs)

```
var dictionary = new KeyStringDictionary
{
    new KeyStringPair("key", "value")
};

string value = dictionary[0];

string value = dictionary["key"];

int value = dictionary.Value<int>("key");

int? value = dictionary.TryValue<int?>("key");
```

[TranslationDictionary&lt;T&gt;](http://code.google.com/p/cavity/source/browse/trunk/src/Cavity%20Core/Class%20Libraries/Core/Collections/TranslationDictionary%601.cs)

```
var autumn = new TranslationDictionary<string>
{
    new Translation<string>("Autumn", "en-GB"),
    new Translation<string>("Fall", "en-US"),
    new Translation<string>("l'Automne", "fr-FR")
};

string value = autumn[Thread.CurrentThread.CurrentUICulture];
```

_Extension Methods_

[IEnumerable](http://code.google.com/p/cavity/source/browse/trunk/src/Cavity%20Core/Class%20Libraries/Core/Collections/IEnumerable.ExtensionMethods.cs)

  * `Concat()`

```
var letters = new[]
{
    "a", "b", "c"
};

string value = letters.Concat(" or ");
```

  * `IsEmpty()`

```
if (new List<int>().IsEmpty())
{
}
```

  * `ToQueue<T>()`

```
Queue<int> value = new List<int>().ToQueue();
```

  * `ToStack<T>()`

```
Stack<int> value = new List<int>().ToStack();
```

## using Cavity.Collections.Generic _[Facts](http://code.google.com/p/cavity/source/browse/#svn%2Ftrunk%2Fsrc%2FCavity%20Core%2FClass%20Libraries%2FCore.Facts%2FCollections%2FGeneric)_ ##

[MultitonCollection&lt;T&gt;](http://code.google.com/p/cavity/source/browse/trunk/src/Cavity%20Core/Class%20Libraries/Core/Collections/Generic/MultitonCollection%601.cs)

```
var multiton = new MultitonCollection<int, MultitonCollection<string, Example>>();

multiton[123]["abc"] = new Example();
```

[Tree&lt;T&gt;](http://code.google.com/p/cavity/source/browse/trunk/src/Cavity%20Core/Class%20Libraries/Core/Collections/Generic/Tree%601.cs)

```
var tree = new Tree<int>(123);

tree.Add(456).Add(new Tree<int>(789));
```

## using Cavity.Configuration _[Facts](http://code.google.com/p/cavity/source/browse/#svn%2Ftrunk%2Fsrc%2FCavity%20Core%2FClass%20Libraries%2FCore.Facts%2FConfiguration)_ ##

[Config](http://code.google.com/p/cavity/source/browse/trunk/src/Cavity%20Core/Class%20Libraries/Core/Configuration/Config.cs)

```
try
{
    var mock = new Mock<MyConfigurationSection>();
    Config.Set(mock);

    // test code here
}
finally
{
    Config.Clear<MyConfigurationSection>();
}
```

```
var settings = Config.ExeSection<MyConfigurationSection>());
```

```
var settings = Config.Section<MyConfigurationSection>("example"));
```

```
IConfigurationSectionHandler settings = Config.SectionHandler<MyConfigurationSectionHandler>("example"));
```

```
var example = Config.Xml<Example>();
```

[PathConfigurationSection](http://code.google.com/p/cavity/source/browse/trunk/src/Cavity%20Core/Class%20Libraries/Core/Configuration/PathConfigurationSection.cs)

```
var paths = Config.ExeSection<PathConfigurationSection>();

DirectoryInfo temp = paths.Directory("temp");
FileInfo example = paths.File("example");
```

app.config
```
<?xml version="1.0" encoding="utf-8"?>
<configuration>
  <configSections>
    <section name="paths"
             type="Cavity.Configuration.PathConfigurationSection, Cavity.Core" />
  </configSections>
  <paths configSource="paths.config" />
</configuration>
```

paths.config
```
<?xml version="1.0" encoding="utf-8"?>
<paths>
  <directories>
    <add name="temp" directory="C:\Temp" />
  </directories>
  <files>
    <add name="example" file="C:\example.txt" />
  </files>
</paths>
```

## using Cavity.Data _[Facts](http://code.google.com/p/cavity/source/browse/#svn%2Ftrunk%2Fsrc%2FCavity%20Core%2FClass%20Libraries%2FCore.Facts%2FData)_ ##

[DataCollection](http://code.google.com/p/cavity/source/browse/trunk/src/Cavity%20Core/Class%20Libraries/Core/Data/DataCollection.cs)

```
var data = new DataCollection
{
    { "name", "1" },
    new KeyStringPair("name", "2")
};

string named = data["name"];
string indexed = data[0];
```

[KeyStringPair](http://code.google.com/p/cavity/source/browse/trunk/src/Cavity%20Core/Class%20Libraries/Core/Data/KeyStringPair.cs)

```
var pair = new KeyStringPair("name", "value");
```

## using Cavity.Dynamic _[Facts](http://code.google.com/p/cavity/source/browse/#svn%2Ftrunk%2Fsrc%2FCavity%20Core%2FClass%20Libraries%2FCore.Facts%2FDynamic)_ ##

[DynamicData](http://code.google.com/p/cavity/source/browse/trunk/src/Cavity%20Core/Class%20Libraries/Core/Dynamic/DynamicData.cs)

```
dynamic data = new DynamicData();

var example = data.Example;
```

## using Cavity.IO _[Facts](http://code.google.com/p/cavity/source/browse/#svn%2Ftrunk%2Fsrc%2FCavity%20Core%2FClass%20Libraries%2FCore.Facts%2FIO)_ ##

[TempDirectory](http://code.google.com/p/cavity/source/browse/trunk/src/Cavity%20Core/Class%20Libraries/Core/IO/TempDirectory.cs)

```
using (var temp = new TempDirectory())
{
    DirectoryInfo directory = temp.Info;
}
```

[TempFile](http://code.google.com/p/cavity/source/browse/trunk/src/Cavity%20Core/Class%20Libraries/Core/IO/TempFile.cs)

```
using (var temp = new TempFile())
{
    FileInfo file = temp.Info;
}
```

_Extension Methods_

  * `ToDirectory()`

```
DirectoryInfo directory = new DirectoryInfo("C:\\")
    .ToDirectory("Example", true);
```

  * `ToFile()`

```
FileInfo file = new DirectoryInfo("C:\\")
    .ToFile("Example.txt");
```

  * `Append()`

```
new DirectoryInfo("C:\\")
    .ToFile("Example.txt")
    .Append("text");
```

  * `AppendLine()`

```
new DirectoryInfo("C:\\")
    .ToFile("Example.txt")
    .AppendLine("text");
```

  * `Create()`

```
new DirectoryInfo("C:\\")
    .ToFile("Example.txt")
    .Create("text");
```

```
new DirectoryInfo("C:\\")
    .ToFile("Example.xml")
    .Create(new Example().XmlSerialize());
```

  * `CreateNew()`

```
new DirectoryInfo("C:\\")
    .ToFile("Example.txt")
    .CreateNew();
```

```
new DirectoryInfo("C:\\")
    .ToFile("Example.txt")
    .CreateNew("text");
```

  * `Lines()`

```
var file = new DirectoryInfo("C:\\")
    .ToFile("Example.txt");
foreach (var line in file.Lines())
{
}
```

  * `ReadToEnd()`

```
var text = new DirectoryInfo("C:\\")
    .ToFile("Example.txt")
    .ReadToEnd();
```

  * `Truncate()`

```
new DirectoryInfo("C:\\")
    .ToFile("Example.txt")
    .Truncate("text");
```

## using Cavity.Security.Cryptography _[Facts](http://code.google.com/p/cavity/source/browse/#svn%2Ftrunk%2Fsrc%2FCavity%20Core%2FClass%20Libraries%2FCore.Facts%2FSecurity%2FCryptography)_ ##

[MD5Hash](http://code.google.com/p/cavity/source/browse/trunk/src/Cavity%20Core/Class%20Libraries/Core/Security/Cryptography/MD5Hash.cs)

```
MD5Hash value = MD5Hash.Compute("example");
```

```
MD5Hash value = MD5Hash.Compute(new Uri("http://jigsaw.w3.org/HTTP/h-content-md5.html"));
```

```
MD5Hash value = MD5Hash.Compute(new FileInfo("jigsaw.html"));
```

## using Cavity.Xml.XPath _[Facts](http://code.google.com/p/cavity/source/browse/#svn%2Ftrunk%2Fsrc%2FCavity%20Core%2FClass%20Libraries%2FCore.Facts%2FXml%2FXPath)_ ##

_Extension Methods_

  * `Evaluate<T>()`

```
var xml = new XmlDocument();
xml.LoadXml("<foo>bar</foo>");

if (xml.CreateNavigator().Evaluate<bool>("1=count(/foo[text()='bar'])"))
{
}
```

```
var xml = new XmlDocument();
xml.LoadXml("<foo xmlns='urn:example'>bar</foo>");

var namespaces = new XmlNamespaceManager(xml.NameTable);
namespaces.AddNamespace("x", "urn:example");

if (xml.CreateNavigator().Evaluate<bool>("1=count(/x:foo[text()='bar'])"))
{
}
```