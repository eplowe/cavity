## NuGet Package ##

http://nuget.org/List/Packages/Cavity.Domain

## using Cavity.Collections _[Facts](http://code.google.com/p/cavity/source/browse/#svn%2Ftrunk%2Fsrc%2FCavity%20Domain%2FClass%20Libraries%2FDomain.Facts)_ ##

[SynonymCollection](http://code.google.com/p/cavity/source/browse/trunk/src/Cavity%20Domain/Class%20Libraries/Domain/Collections/SynonymCollection.cs)

```
var synonyms = new SynonymCollection(NormalizationComparer.OrdinalIgnoreCase)
{
    "Example"
};

if (synonyms.Contains("EXAMPLE"))
{
}
```

## using Cavity.Models _[Facts](http://code.google.com/p/cavity/source/browse/#svn%2Ftrunk%2Fsrc%2FCavity%20Domain%2FClass%20Libraries%2FDomain.Facts%2FModels%253Fstate%253Dclosed)_ ##

[Lexicon](http://code.google.com/p/cavity/source/browse/trunk/src/Cavity%20Domain/Class%20Libraries/Domain/Models/Lexicon.cs)

```
var lexicon = new Lexicon(NormalizationComparer.OrdinalIgnoreCase);
lexicon.Add("1").Synonyms.Add("One");

if (lexicon.Contains("one"))
{
}

string canonical = lexicon.ToCanonicalForm("one");
```

```
var lexicon = new Lexicon(NormalizationComparer.OrdinalIgnoreCase);
lexicon
    .Add(string.Concat("Foo", '\u00A0', "Bar"))
    .Synonyms.Add(string.Concat("Left", '\u00A0', "Right"));

lexicon.Invoke(x => x.NormalizeWhiteSpace());
```

[Telephone](http://code.google.com/p/cavity/source/browse/trunk/src/Cavity%20Domain/Class%20Libraries/Domain/Models/Telephone.cs)

```
Telephone tel = "+441111222333";
```

```
Telephone tel = "00441111222333";
```

```
Telephone tel = "(01111) 222-333";
```

```
Telephone tel = "(01111) 222-333 at home";
```

```
Telephone tel = "+1 (222) 333-4444";
```