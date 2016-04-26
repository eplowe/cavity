## NuGet Package ##

http://nuget.org/List/Packages/Cavity.Testing.Unit

## Guide to asserting expectations about properties ##

**Asserting an auto-implemented property**

```
        [Fact]
        public void value_definition()
        {
            Assert.True(new PropertyExpectations<Class1>(x => x.Value)
                .IsAutoProperty<string>()
                .Result);
        }
```

**Asserting an auto-implemented property with a custom default value**

```
        [Fact]
        public void value_definition()
        {
            Assert.True(new PropertyExpectations<Class1>(x => x.Value)
                .IsAutoProperty<string>("default")
                .Result);
        }
```

**Asserting an auto-implemented property decorated with [XmlAttribute("value")]**

```
        [Fact]
        public void value_definition()
        {
            Assert.True(new PropertyExpectations<Class1>(x => x.Value)
                .IsAutoProperty<string>()
                .XmlAttribute("attribute")
                .Result);
        }
```

**Asserting a validated property**

```
        [Fact]
        public void value_definition()
        {
            Assert.True(new PropertyExpectations<Class1>(x => x.Value)
                .TypeIs<string>()
                .DefaultValueIs("default")
                .Set("example")
                .ArgumentNullException()
                .ArgumentOutOfRangeException(string.Empty)
                .FormatException("invalid")
                .IsNotDecorated()
                .Result);
        }
```

## Guide to asserting expectations about types ##

**Asserting an interface**

```
        [Fact]
        public void type_definition()
        {
            Assert.True(new TypeExpectations<Interface1>()
                .IsInterface()
                .Result);
        }
```

**Asserting a value type**

```
        [Fact]
        public void type_definition()
        {
            Assert.True(new TypeExpectations<Struct1>()
                .IsValueType()
                .Implements<IFoo>()
                .IsDecoratedWith<CustomAttribute>()
                .Result);
        }
```

**Asserting an abstract class**

```
        [Fact]
        public void type_definition()
        {
            Assert.True(new TypeExpectations<Class1>()
                .DerivesFrom<object>()
                .IsAbstractBaseClass()
                .Implements<IFoo>()
                .IsDecoratedWith<CustomAttribute>()
                .Result);
        }
```

**Asserting a concrete class**

```
        [Fact]
        public void type_definition()
        {
            Assert.True(new TypeExpectations<Class1>()
                .DerivesFrom<object>()
                .IsConcreteClass()
                .IsUnsealed()
                .HasDefaultConstructor()
                .Implements<IFoo>()
                .IsDecoratedWith<CustomAttribute>()
                .Result);
        }
```

**Asserting a serializable class**

```
        [Fact]
        public void type_definition()
        {
            Assert.True(new TypeExpectations<Class1>()
                .DerivesFrom<object>()
                .IsConcreteClass()
                .IsUnsealed()
                .HasDefaultConstructor()
                .Serializable()
                .Result);
        }
```

**Asserting a class with custom XML serialization**

```
        [Fact]
        public void type_definition()
        {
            Assert.True(new TypeExpectations<Class1>()
                .DerivesFrom<object>()
                .IsConcreteClass()
                .IsUnsealed()
                .HasDefaultConstructor()
                .XmlRoot("root")
                .Implements<IXmlSerializable>()
                .Result);
        }
```