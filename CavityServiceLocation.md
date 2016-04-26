The following guide assumes service location of the `ITest` interface resolving the `Tester` class:

```
namespace Example
{
    using System;

    public interface ITest
    {
        bool Test(string actual);
    }

    public sealed class Tester : ITest
    {
        public Tester(string expected)
        {
            this.Expected = expected;
        }

        public string Expected
        {
            get;
            set;
        }

        public bool Test(string actual)
        {
            return string.Equals(this.Expected, actual, StringComparison.Ordinal);
        }
    }
}
```

The abstraction enabled by Cavity Service Location allows the following code to be compiled and then configured later to the IoC provider of your choice:

```
namespace Example
{
    using System;
    using Cavity.Configuration;
    using Microsoft.Practices.ServiceLocation;

    public static class Program
    {
        public static void Main()
        {
            // this line dynamically configures the container of choice
            ServiceLocation.Settings().Configure();

            // then you can call the generic service locator
            ServiceLocator.Current.GetInstance<ITest>().Test("value");
        }
    }
}
```

Choose from the following configuration options:

## [Autofac](http://code.google.com/p/autofac/) ##

http://nuget.org/List/Packages/Cavity.ServiceLocation.Autofac

`app.config`
```
<?xml version="1.0" encoding="utf-8" ?>
<configuration>
  <configSections>
    <section
      name="autofac"
      type="Autofac.Configuration.SectionHandler, Autofac.Configuration" />
    <section
      name="serviceLocation"
      type="Cavity.Configuration.ServiceLocation, Cavity.ServiceLocation"/>
  </configSections>
  <autofac configSource="autofac.config" />
  <serviceLocation type="Cavity.Configuration.XmlServiceLocatorProvider, Cavity.ServiceLocation.Autofac" />
</configuration>
```

`autofac.config`
```
<?xml version="1.0" encoding="utf-8" ?>
<autofac defaultAssembly="Example">
  <components>
    <component
      type="Example.Tester"
      service="Example.ITest" >
      <parameters>
        <parameter name="expected" value="value" />
      </parameters>
    </component>
  </components>
</autofac>
```

## [Castle Windsor](http://www.castleproject.org/container/) ##

http://nuget.org/List/Packages/Cavity.ServiceLocation.CastleWindsor

`app.config`
```
<?xml version="1.0" encoding="utf-8" ?>
<configuration>
  <configSections>
    <section
      name="castle"
      type="Castle.Windsor.Configuration.AppDomain.CastleSectionHandler, Castle.Windsor"/>
    <section
      name="serviceLocation"
      type="Cavity.Configuration.ServiceLocation, Cavity.ServiceLocation"/>
  </configSections>
  <castle configSource="castle.config" />
  <serviceLocation type="Cavity.Configuration.XmlServiceLocatorProvider, Cavity.ServiceLocation.CastleWindsor" />
</configuration>
```

`castle.config`
```
<?xml version="1.0" encoding="utf-8" ?>
<castle>
  <components>
    <component
      id="test"
      service="Example.ITest, Example"
      type="Example.Tester, Example">
      <parameters>
        <expected>value</expected>
      </parameters>
    </component>
  </components>
</castle>
```

## [StructureMap](http://structuremap.github.com/structuremap/) ##

http://nuget.org/List/Packages/Cavity.ServiceLocation.StructureMap

`app.config`
```
<?xml version="1.0" encoding="utf-8" ?>
<configuration>
  <configSections>
    <section
      name="StructureMap"
      type="StructureMap.Configuration.StructureMapConfigurationSection, StructureMap" />
    <section
      name="serviceLocation"
      type="Cavity.Configuration.ServiceLocation, Cavity.ServiceLocation"/>
  </configSections>
  <StructureMap configSource="StructureMap.config" />
  <serviceLocation type="Cavity.Configuration.XmlServiceLocatorProvider, Cavity.ServiceLocation.StructureMap" />
</configuration>
```

`StructureMap.config`
```
<?xml version="1.0" encoding="utf-8" ?>
<StructureMap MementoStyle="Attribute">
  <DefaultInstance
    PluginType="Example.ITest, Example"
    PluggedType="Example.Tester, Example"
    Scope="Singleton"
    expected="value" />
</StructureMap>
```

## [Unity](http://unity.codeplex.com/) ##

http://nuget.org/List/Packages/Cavity.ServiceLocation.Unity

`app.config`
```
<?xml version="1.0" encoding="utf-8" ?>
<configuration>
  <configSections>
    <section
      name="unity"
      type="Microsoft.Practices.Unity.Configuration.UnityConfigurationSection, Microsoft.Practices.Unity.Configuration" />
    <section
      name="serviceLocation"
      type="Cavity.Configuration.ServiceLocation, Cavity.ServiceLocation"/>
  </configSections>
  <unity configSource="unity.config" />
  <serviceLocation type="Cavity.Configuration.XmlServiceLocatorProvider, Cavity.ServiceLocation.Unity" />
</configuration>
```

`unity.config`
```
<?xml version="1.0" encoding="utf-8" ?>
<unity>
  <typeAliases>
    <typeAlias
      alias="singleton"
      type="Microsoft.Practices.Unity.ContainerControlledLifetimeManager, Microsoft.Practices.Unity" />
    <typeAlias
      alias="ITest"
      type="Example.ITest, Example" />
    <typeAlias
      alias="Tester"
      type="Example.Tester, Example" />
  </typeAliases>
  <containers>
    <container name="container">
      <type type="ITest" mapTo="Tester">
        <lifetime type="singleton" />
        <constructor>
          <param name="expected">
            <value value="value"/>
          </param>
        </constructor>
      </type>
    </container>
  </containers>
</unity>
```