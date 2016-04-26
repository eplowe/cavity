I have published some [guidance on building Cavity on your local machine](BuildCavity.md).

I am in the process of a significant re-organisation of Cavity to reflect my decision to publish the various libraries on [nuget.org](http://nuget.org/List/Search?searchTerm=author%3A%20Alan%20Dean)

I've written a couple of blog posts on my experiences:
  * [Simple NuGet Packaging](http://alandean.blogspot.com/2011/10/simple-nuget-packaging.html)
  * [Not-so-simple NuGet Packaging](http://alandean.blogspot.com/2011/10/not-so-simple-nuget-packaging.html)

Here are the libraries published so far:

## [Cavity Core](CavityCore.md) ##

The core Cavity library.

## [Cavity Commands](CavityCommands.md) ##

Provides an XML serializable implementation of the Command design pattern.

## [Cavity Commands (File System)](CavityCommandsFileSystem.md) ##

Contains various file system command types.

## [Cavity Commands Transactions](CavityTransactionsCommands.md) ##

Provides durable file-based transaction scope enlistment for use in conjunction with Cavity Commands.

## [Cavity Data (ACE)](CavityDataAce.md) ##

Provides data access using the Microsoft Access Database Engine (currently read-only).

## [Cavity Data (CSV)](CavityDataCsv.md) ##

Provides data acess for comma separated (CSV) files (currently read-only).

## [Cavity Data (CSV) xUnit Extensions](CavityDataCsvXunit.md) ##

Provides xUnit extensions for comma separated (CSV) files.

## [Cavity Data (HTML) xUnit Extensions](CavityDataHtmlXunit.md) ##

Provides xUnit extensions for HTML files and data.

## [Cavity Data (JSON) xUnit Extensions](CavityDataJsonXunit.md) ##

Provides xUnit extensions for JSON files and data.

## [Cavity Data (XML) xUnit Extensions](CavityDataXmlXunit.md) ##

Provides xUnit extensions for XML files and data.

## [Cavity Domain](CavityDomain.md) ##

Provides domain models and supporting types.

## [Cavity Domain (Royal Mail)](CavityDomainRoyalMail.md) ##

Provides Royal Mail domain models and supporting types.

## [Cavity log4net trace listener](CavityDiagnosticsLog4Net.md) ##

A trace listener for log4net, allowing provider-agnostic tracing.

## [Cavity Repository](CavityRepository.md) ##

Describes the abstractions of a data repository.

## [Cavity Repository (XML File System)](CavityRepositoryFileSystemXml.md) ##

Provides an XML file-based Cavity Repository implementation.

## [Cavity Repository Testing](CavityTestingRepository.md) ##

Provides a standard test suite to verify the behaviour of Cavity Repository implementations.

## [Cavity Service Location](CavityServiceLocation.md) ##

Provides XML configuration assistance for IoC providers.

## [Cavity Unit Testing](CavityTestingUnit.md) ##

Fluent API for asserting types and properties.