namespace Cavity.Models
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Xml.XPath;

    using Cavity.IO;
    using Cavity.Testing;
    using Cavity.Xml.XPath;

    using Xunit;

    public sealed class ProjectFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<Project>()
                            .DerivesFrom<object>()
                            .IsConcreteClass()
                            .IsSealed()
                            .NoDefaultConstructor()
                            .IsNotDecorated()
                            .Result);
        }

        [Fact]
        public void op_Load_FileInfo()
        {
            using (var temp = new TempDirectory())
            {
                var location = temp.Info.ToFile("example.csproj");
                ProjectFile.Create(location);

                var obj = Project.Load(location);

                Assert.Equal(location, obj.Location);

                var navigator = obj.Xml.CreateNavigator();

                Assert.True(navigator.Evaluate<bool>("1 = count(/b:Project)", Project.ToNamespaces(navigator)));
            }
        }

        [Fact]
        public void op_Load_FileInfoMissing()
        {
            using (var temp = new TempDirectory())
            {
                // ReSharper disable AccessToDisposedClosure
                Assert.Throws<FileNotFoundException>(() => Project.Load(temp.Info.ToFile("example.csproj")));

                // ReSharper restore AccessToDisposedClosure
            }
        }

        [Fact]
        public void op_Load_FileInfoNull()
        {
            Assert.Throws<ArgumentNullException>(() => Project.Load(null));
        }

        [Fact]
        public void op_PackageReference_Package()
        {
            using (var temp = new TempDirectory())
            {
                var reference = new Reference("Example", @"..\..\packages\Example.1.2.3.4\net40\example.dll");
                var obj = ProjectFile.Create(temp.Info.ToFile("example.csproj"), reference);
                PackageFile.Create(obj);

                var actual = obj.PackageReference(new Package("example", "1.2.3.4"));

                Assert.Equal("example", actual.Id);
                Assert.Equal("1.2.3.4", actual.Version);
            }
        }

        [Fact]
        public void op_PackageReference_Package_whenFullyNamed()
        {
            using (var temp = new TempDirectory())
            {
                var reference = new Reference("Example, Version=1.2.3.4", @"..\..\packages\Example.1.2.3.4\net40\example.dll");
                var obj = ProjectFile.Create(temp.Info.ToFile("example.csproj"), reference);
                PackageFile.Create(obj);

                var actual = obj.PackageReference(new Package("example", "1.2.3.4"));

                Assert.Equal("example", actual.Id);
                Assert.Equal("1.2.3.4", actual.Version);
            }
        }

        [Fact]
        public void op_PackageReference_Package_whenDifferentVersion()
        {
            using (var temp = new TempDirectory())
            {
                var reference = new Reference("Example", @"..\..\packages\Example.9.8.7.6\net40\example.dll");
                var obj = ProjectFile.Create(temp.Info.ToFile("example.csproj"), reference);
                PackageFile.Create(obj);

                var actual = obj.PackageReference(new Package("example", "1.2.3.4"));

                Assert.Equal("example", actual.Id);
                Assert.Equal("9.8.7.6", actual.Version);
            }
        }

        [Fact]
        public void op_PackageReference_Package_whenMissing()
        {
            using (var temp = new TempDirectory())
            {
                var obj = ProjectFile.Create(temp.Info.ToFile("example.csproj"));
                PackageFile.Create(obj);

                Assert.Null(obj.PackageReference(new Package("example", "1.2.3.4")));
            }
        }

        [Fact]
        public void op_PackageReference_PackageNull()
        {
            using (var temp = new TempDirectory())
            {
                var obj = ProjectFile.Create(temp.Info.ToFile("example.csproj"));

                Assert.Throws<ArgumentNullException>(() => obj.PackageReference(null));
            }
        }

        [Fact]
        public void op_ToNamespaces_XPathNavigatorNull()
        {
            Assert.Throws<ArgumentNullException>(() => Project.ToNamespaces(null as XPathNavigator));
        }

        [Fact]
        public void prop_Location()
        {
            Assert.True(new PropertyExpectations<Project>(x => x.Location)
                            .IsAutoProperty<FileInfo>()
                            .IsNotDecorated()
                            .Result);
        }

        [Fact]
        public void prop_Packages()
        {
            Assert.True(new PropertyExpectations<Project>(x => x.Packages)
                            .TypeIs<IEnumerable<Package>>()
                            .IsNotDecorated()
                            .Result);
        }

        [Fact]
        public void prop_Packages_get_whenPackageConfig()
        {
            using (var temp = new TempDirectory())
            {
                var expected = new Package("example", "1.2.3.4");

                var obj = ProjectFile.Create(temp.Info.ToFile("example.csproj"));
                PackageFile.Create(obj, expected);

                var actual = obj.Packages.ToList();

                Assert.Equal(1, actual.Count);
                Assert.Equal(expected.Id, actual[0].Id);
                Assert.Equal(expected.Version, actual[0].Version);
            }
        }

        [Fact]
        public void prop_Packages_get_whenPackageAttributesMissing()
        {
            using (var temp = new TempDirectory())
            {
                var obj = ProjectFile.Create(temp.Info.ToFile("example.csproj"));
                obj.Location.Directory.ToFile("packages.config").Create("<packages><package/></packages>");

                Assert.Empty(obj.Packages);
            }
        }

        [Fact]
        public void prop_Packages_get_whenPackageConfigEmpty()
        {
            using (var temp = new TempDirectory())
            {
                var obj = ProjectFile.Create(temp.Info.ToFile("example.csproj"));
                PackageFile.Create(obj);

                Assert.Empty(obj.Packages);
            }
        }

        [Fact]
        public void prop_Packages_get_whenPackageConfigMissing()
        {
            using (var temp = new TempDirectory())
            {
                var obj = ProjectFile.Create(temp.Info.ToFile("example.csproj"));

                Assert.Empty(obj.Packages);
            }
        }

        [Fact]
        public void prop_References()
        {
            Assert.True(new PropertyExpectations<Project>(x => x.References)
                            .TypeIs<IEnumerable<Reference>>()
                            .IsNotDecorated()
                            .Result);
        }

        [Fact]
        public void prop_References_get_whenEmpty()
        {
            using (var temp = new TempDirectory())
            {
                var obj = ProjectFile.Create(temp.Info.ToFile("example.csproj"));

                Assert.Empty(obj.References);
            }
        }

        [Fact]
        public void prop_References_get_whenHintPathMissing()
        {
            using (var temp = new TempDirectory())
            {
                var expected = new Reference("example", null);

                var obj = ProjectFile.Create(temp.Info.ToFile("example.csproj"), expected);

                var actual = obj.References.ToList();

                Assert.Equal(1, actual.Count);
                Assert.Equal(expected.Include, actual[0].Include);
                Assert.Null(expected.Hint);
            }
        }

        [Fact]
        public void prop_References_get()
        {
            using (var temp = new TempDirectory())
            {
                var expected = new Reference("example", @"..\..\packages\1.2.3.4\net40\example.dll");

                var obj = ProjectFile.Create(temp.Info.ToFile("example.csproj"), expected);

                var actual = obj.References.ToList();

                Assert.Equal(1, actual.Count);
                Assert.Equal(expected.Include, actual[0].Include);
                Assert.Equal(expected.Hint, actual[0].Hint);
            }
        }

        [Fact]
        public void prop_Xml()
        {
            Assert.True(new PropertyExpectations<Project>(x => x.Xml)
                            .TypeIs<IXPathNavigable>()
                            .IsNotDecorated()
                            .Result);
        }
    }
}