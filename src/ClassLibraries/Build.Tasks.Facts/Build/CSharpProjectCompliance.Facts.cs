namespace Cavity.Build
{
    using System.IO;
    using System.Reflection;
    using System.Xml;
    using Cavity.IO;
    using Microsoft.Build.Framework;
    using Microsoft.Build.Utilities;
    using Moq;
    using Xunit;

    public sealed class CSharpProjectComplianceFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<CSharpProjectCompliance>()
                            .DerivesFrom<Task>()
                            .IsConcreteClass()
                            .IsSealed()
                            .HasDefaultConstructor()
                            .IsNotDecorated()
                            .Result);
        }

        [Fact]
        public void ctor()
        {
            Assert.NotNull(new CSharpProjectCompliance());
        }

        [Fact]
        public void op_Execute_IEnumerable()
        {
            using (var file = new TempFile())
            {
                using (var resource = Assembly.GetExecutingAssembly().GetManifestResourceStream(@"Cavity.Build.CSharpProjectCompliance.xml"))
                {
                    if (null != resource)
                    {
                        using (var reader = new StreamReader(resource))
                        {
                            using (var stream = file.Info.Open(FileMode.Append, FileAccess.Write, FileShare.None))
                            {
                                using (var writer = new StreamWriter(stream))
                                {
                                    writer.Write(reader.ReadToEnd());
                                }
                            }
                        }
                    }
                }

                var obj = new CSharpProjectCompliance
                {
                    BuildEngine = new Mock<IBuildEngine>().Object,
                    Paths = new ITaskItem[]
                    {
                        new TaskItem(file.Info.FullName)
                    }
                };

                Assert.True(obj.Execute());
            }
        }

        [Fact]
        public void op_Execute_IEnumerableEmpty()
        {
            var obj = new CSharpProjectCompliance
            {
                BuildEngine = new Mock<IBuildEngine>().Object,
                Paths = new ITaskItem[]
                {
                }
            };

            Assert.False(obj.Execute());
        }

        [Fact]
        public void op_Execute_IEnumerableNull()
        {
            var obj = new CSharpProjectCompliance
            {
                BuildEngine = new Mock<IBuildEngine>().Object
            };

            Assert.False(obj.Execute());
        }

        [Fact]
        public void op_Execute_IEnumerable_whenAppDesignerFolderMissing()
        {
            using (var file = new TempFile())
            {
                using (var resource = Assembly.GetExecutingAssembly().GetManifestResourceStream(@"Cavity.Build.CSharpProjectCompliance.xml"))
                {
                    if (null != resource)
                    {
                        using (var reader = new StreamReader(resource))
                        {
                            var xml = new XmlDocument();
                            xml.LoadXml(reader.ReadToEnd());
                            var namespaces = new XmlNamespaceManager(xml.NameTable);
                            namespaces.AddNamespace("b", "http://schemas.microsoft.com/developer/msbuild/2003");
                            var node = xml.SelectSingleNode("/b:Project/b:PropertyGroup[not(@Condition)]/b:AppDesignerFolder", namespaces);
                            if (null != node &&
                                null != node.ParentNode)
                            {
                                node.ParentNode.RemoveChild(node);
                            }

                            using (var stream = file.Info.Open(FileMode.Append, FileAccess.Write, FileShare.None))
                            {
                                using (var writer = new StreamWriter(stream))
                                {
                                    writer.Write(xml.OuterXml);
                                }
                            }
                        }
                    }
                }

                var obj = new CSharpProjectCompliance
                {
                    BuildEngine = new Mock<IBuildEngine>().Object,
                    Paths = new ITaskItem[]
                    {
                        new TaskItem(file.Info.FullName)
                    }
                };

                Assert.False(obj.Execute());
            }
        }

        [Fact]
        public void op_Execute_IEnumerable_whenAssemblyOriginatorKeyFileMissing()
        {
            using (var file = new TempFile())
            {
                using (var resource = Assembly.GetExecutingAssembly().GetManifestResourceStream(@"Cavity.Build.CSharpProjectCompliance.xml"))
                {
                    if (null != resource)
                    {
                        using (var reader = new StreamReader(resource))
                        {
                            var xml = new XmlDocument();
                            xml.LoadXml(reader.ReadToEnd());
                            var namespaces = new XmlNamespaceManager(xml.NameTable);
                            namespaces.AddNamespace("b", "http://schemas.microsoft.com/developer/msbuild/2003");
                            var node = xml.SelectSingleNode("/b:Project/b:PropertyGroup[not(@Condition)]/b:AssemblyOriginatorKeyFile", namespaces);
                            if (null != node &&
                                null != node.ParentNode)
                            {
                                node.ParentNode.RemoveChild(node);
                            }

                            using (var stream = file.Info.Open(FileMode.Append, FileAccess.Write, FileShare.None))
                            {
                                using (var writer = new StreamWriter(stream))
                                {
                                    writer.Write(xml.OuterXml);
                                }
                            }
                        }
                    }
                }

                var obj = new CSharpProjectCompliance
                {
                    BuildEngine = new Mock<IBuildEngine>().Object,
                    Paths = new ITaskItem[]
                    {
                        new TaskItem(file.Info.FullName)
                    }
                };

                Assert.False(obj.Execute());
            }
        }

        [Fact]
        public void op_Execute_IEnumerable_whenCodeAnalysisDictionaryMissing()
        {
            using (var file = new TempFile())
            {
                using (var resource = Assembly.GetExecutingAssembly().GetManifestResourceStream(@"Cavity.Build.CSharpProjectCompliance.xml"))
                {
                    if (null != resource)
                    {
                        using (var reader = new StreamReader(resource))
                        {
                            var xml = new XmlDocument();
                            xml.LoadXml(reader.ReadToEnd());
                            var namespaces = new XmlNamespaceManager(xml.NameTable);
                            namespaces.AddNamespace("b", "http://schemas.microsoft.com/developer/msbuild/2003");
                            var node = xml.SelectSingleNode(@"/b:Project/b:ItemGroup/b:CodeAnalysisDictionary[@Include]", namespaces);
                            if (null != node &&
                                null != node.ParentNode)
                            {
                                node.ParentNode.RemoveChild(node);
                            }

                            using (var stream = file.Info.Open(FileMode.Append, FileAccess.Write, FileShare.None))
                            {
                                using (var writer = new StreamWriter(stream))
                                {
                                    writer.Write(xml.OuterXml);
                                }
                            }
                        }
                    }
                }

                var obj = new CSharpProjectCompliance
                {
                    BuildEngine = new Mock<IBuildEngine>().Object,
                    Paths = new ITaskItem[]
                    {
                        new TaskItem(file.Info.FullName)
                    }
                };

                Assert.False(obj.Execute());
            }
        }

        [Fact]
        public void op_Execute_IEnumerable_whenCodeAnalysisRuleSetMissing()
        {
            using (var file = new TempFile())
            {
                using (var resource = Assembly.GetExecutingAssembly().GetManifestResourceStream(@"Cavity.Build.CSharpProjectCompliance.xml"))
                {
                    if (null != resource)
                    {
                        using (var reader = new StreamReader(resource))
                        {
                            var xml = new XmlDocument();
                            xml.LoadXml(reader.ReadToEnd());
                            var namespaces = new XmlNamespaceManager(xml.NameTable);
                            namespaces.AddNamespace("b", "http://schemas.microsoft.com/developer/msbuild/2003");
                            var node = xml.SelectSingleNode("/b:Project/b:PropertyGroup[@Condition=\" '$(Configuration)|$(Platform)' == 'Debug|AnyCPU' \"]/b:CodeAnalysisRuleSet", namespaces);
                            if (null != node &&
                                null != node.ParentNode)
                            {
                                node.ParentNode.RemoveChild(node);
                            }

                            using (var stream = file.Info.Open(FileMode.Append, FileAccess.Write, FileShare.None))
                            {
                                using (var writer = new StreamWriter(stream))
                                {
                                    writer.Write(xml.OuterXml);
                                }
                            }
                        }
                    }
                }

                var obj = new CSharpProjectCompliance
                {
                    BuildEngine = new Mock<IBuildEngine>().Object,
                    Paths = new ITaskItem[]
                    {
                        new TaskItem(file.Info.FullName)
                    }
                };

                Assert.False(obj.Execute());
            }
        }

        [Fact]
        public void op_Execute_IEnumerable_whenErrorReportMissing()
        {
            using (var file = new TempFile())
            {
                using (var resource = Assembly.GetExecutingAssembly().GetManifestResourceStream(@"Cavity.Build.CSharpProjectCompliance.xml"))
                {
                    if (null != resource)
                    {
                        using (var reader = new StreamReader(resource))
                        {
                            var xml = new XmlDocument();
                            xml.LoadXml(reader.ReadToEnd());
                            var namespaces = new XmlNamespaceManager(xml.NameTable);
                            namespaces.AddNamespace("b", "http://schemas.microsoft.com/developer/msbuild/2003");
                            var node = xml.SelectSingleNode("/b:Project/b:PropertyGroup[@Condition=\" '$(Configuration)|$(Platform)' == 'Debug|AnyCPU' \"]/b:ErrorReport", namespaces);
                            if (null != node &&
                                null != node.ParentNode)
                            {
                                node.ParentNode.RemoveChild(node);
                            }

                            using (var stream = file.Info.Open(FileMode.Append, FileAccess.Write, FileShare.None))
                            {
                                using (var writer = new StreamWriter(stream))
                                {
                                    writer.Write(xml.OuterXml);
                                }
                            }
                        }
                    }
                }

                var obj = new CSharpProjectCompliance
                {
                    BuildEngine = new Mock<IBuildEngine>().Object,
                    Paths = new ITaskItem[]
                    {
                        new TaskItem(file.Info.FullName)
                    }
                };

                Assert.False(obj.Execute());
            }
        }

        [Fact]
        public void op_Execute_IEnumerable_whenIncludeAssemblyInfoMissing()
        {
            using (var file = new TempFile())
            {
                using (var resource = Assembly.GetExecutingAssembly().GetManifestResourceStream(@"Cavity.Build.CSharpProjectCompliance.xml"))
                {
                    if (null != resource)
                    {
                        using (var reader = new StreamReader(resource))
                        {
                            var xml = new XmlDocument();
                            xml.LoadXml(reader.ReadToEnd());
                            var namespaces = new XmlNamespaceManager(xml.NameTable);
                            namespaces.AddNamespace("b", "http://schemas.microsoft.com/developer/msbuild/2003");
                            var node = xml.SelectSingleNode(@"/b:Project/b:ItemGroup/b:Compile[@Include='Properties\AssemblyInfo.cs']", namespaces);
                            if (null != node &&
                                null != node.ParentNode)
                            {
                                node.ParentNode.RemoveChild(node);
                            }

                            using (var stream = file.Info.Open(FileMode.Append, FileAccess.Write, FileShare.None))
                            {
                                using (var writer = new StreamWriter(stream))
                                {
                                    writer.Write(xml.OuterXml);
                                }
                            }
                        }
                    }
                }

                var obj = new CSharpProjectCompliance
                {
                    BuildEngine = new Mock<IBuildEngine>().Object,
                    Paths = new ITaskItem[]
                    {
                        new TaskItem(file.Info.FullName)
                    }
                };

                Assert.False(obj.Execute());
            }
        }

        [Fact]
        public void op_Execute_IEnumerable_whenIncludeBuildMissing()
        {
            using (var file = new TempFile())
            {
                using (var resource = Assembly.GetExecutingAssembly().GetManifestResourceStream(@"Cavity.Build.CSharpProjectCompliance.xml"))
                {
                    if (null != resource)
                    {
                        using (var reader = new StreamReader(resource))
                        {
                            var xml = new XmlDocument();
                            xml.LoadXml(reader.ReadToEnd());
                            var namespaces = new XmlNamespaceManager(xml.NameTable);
                            namespaces.AddNamespace("b", "http://schemas.microsoft.com/developer/msbuild/2003");
                            var node = xml.SelectSingleNode(@"/b:Project/b:ItemGroup/b:Compile[@Include='..\..\Build.cs']", namespaces);
                            if (null != node &&
                                null != node.ParentNode)
                            {
                                node.ParentNode.RemoveChild(node);
                            }

                            using (var stream = file.Info.Open(FileMode.Append, FileAccess.Write, FileShare.None))
                            {
                                using (var writer = new StreamWriter(stream))
                                {
                                    writer.Write(xml.OuterXml);
                                }
                            }
                        }
                    }
                }

                var obj = new CSharpProjectCompliance
                {
                    BuildEngine = new Mock<IBuildEngine>().Object,
                    Paths = new ITaskItem[]
                    {
                        new TaskItem(file.Info.FullName)
                    }
                };

                Assert.False(obj.Execute());
            }
        }

        [Fact]
        public void op_Execute_IEnumerable_whenMicrosoftStyleCopTargetsMissing()
        {
            using (var file = new TempFile())
            {
                using (var resource = Assembly.GetExecutingAssembly().GetManifestResourceStream(@"Cavity.Build.CSharpProjectCompliance.xml"))
                {
                    if (null != resource)
                    {
                        using (var reader = new StreamReader(resource))
                        {
                            var xml = new XmlDocument();
                            xml.LoadXml(reader.ReadToEnd());
                            var namespaces = new XmlNamespaceManager(xml.NameTable);
                            namespaces.AddNamespace("b", "http://schemas.microsoft.com/developer/msbuild/2003");
                            var node = xml.SelectSingleNode(@"/b:Project/b:Import[@Project='$(MSBuildExtensionsPath)\Microsoft\StyleCop\v4.3\Microsoft.StyleCop.targets']", namespaces);
                            if (null != node &&
                                null != node.ParentNode)
                            {
                                node.ParentNode.RemoveChild(node);
                            }

                            using (var stream = file.Info.Open(FileMode.Append, FileAccess.Write, FileShare.None))
                            {
                                using (var writer = new StreamWriter(stream))
                                {
                                    writer.Write(xml.OuterXml);
                                }
                            }
                        }
                    }
                }

                var obj = new CSharpProjectCompliance
                {
                    BuildEngine = new Mock<IBuildEngine>().Object,
                    Paths = new ITaskItem[]
                    {
                        new TaskItem(file.Info.FullName)
                    }
                };

                Assert.False(obj.Execute());
            }
        }

        [Fact]
        public void op_Execute_IEnumerable_whenRunCodeAnalysisMissing()
        {
            using (var file = new TempFile())
            {
                using (var resource = Assembly.GetExecutingAssembly().GetManifestResourceStream(@"Cavity.Build.CSharpProjectCompliance.xml"))
                {
                    if (null != resource)
                    {
                        using (var reader = new StreamReader(resource))
                        {
                            var xml = new XmlDocument();
                            xml.LoadXml(reader.ReadToEnd());
                            var namespaces = new XmlNamespaceManager(xml.NameTable);
                            namespaces.AddNamespace("b", "http://schemas.microsoft.com/developer/msbuild/2003");
                            var node = xml.SelectSingleNode("/b:Project/b:PropertyGroup[@Condition=\" '$(Configuration)|$(Platform)' == 'Debug|AnyCPU' \"]/b:RunCodeAnalysis", namespaces);
                            if (null != node &&
                                null != node.ParentNode)
                            {
                                node.ParentNode.RemoveChild(node);
                            }

                            using (var stream = file.Info.Open(FileMode.Append, FileAccess.Write, FileShare.None))
                            {
                                using (var writer = new StreamWriter(stream))
                                {
                                    writer.Write(xml.OuterXml);
                                }
                            }
                        }
                    }
                }

                var obj = new CSharpProjectCompliance
                {
                    BuildEngine = new Mock<IBuildEngine>().Object,
                    Paths = new ITaskItem[]
                    {
                        new TaskItem(file.Info.FullName)
                    }
                };

                Assert.False(obj.Execute());
            }
        }

        [Fact]
        public void op_Execute_IEnumerable_whenSignAssemblyMissing()
        {
            using (var file = new TempFile())
            {
                using (var resource = Assembly.GetExecutingAssembly().GetManifestResourceStream(@"Cavity.Build.CSharpProjectCompliance.xml"))
                {
                    if (null != resource)
                    {
                        using (var reader = new StreamReader(resource))
                        {
                            var xml = new XmlDocument();
                            xml.LoadXml(reader.ReadToEnd());
                            var namespaces = new XmlNamespaceManager(xml.NameTable);
                            namespaces.AddNamespace("b", "http://schemas.microsoft.com/developer/msbuild/2003");
                            var node = xml.SelectSingleNode("/b:Project/b:PropertyGroup[not(@Condition)]/b:SignAssembly", namespaces);
                            if (null != node &&
                                null != node.ParentNode)
                            {
                                node.ParentNode.RemoveChild(node);
                            }

                            using (var stream = file.Info.Open(FileMode.Append, FileAccess.Write, FileShare.None))
                            {
                                using (var writer = new StreamWriter(stream))
                                {
                                    writer.Write(xml.OuterXml);
                                }
                            }
                        }
                    }
                }

                var obj = new CSharpProjectCompliance
                {
                    BuildEngine = new Mock<IBuildEngine>().Object,
                    Paths = new ITaskItem[]
                    {
                        new TaskItem(file.Info.FullName)
                    }
                };

                Assert.False(obj.Execute());
            }
        }

        [Fact]
        public void op_Execute_IEnumerable_whenStyleCopTreatErrorsAsWarningsMissing()
        {
            using (var file = new TempFile())
            {
                using (var resource = Assembly.GetExecutingAssembly().GetManifestResourceStream(@"Cavity.Build.CSharpProjectCompliance.xml"))
                {
                    if (null != resource)
                    {
                        using (var reader = new StreamReader(resource))
                        {
                            var xml = new XmlDocument();
                            xml.LoadXml(reader.ReadToEnd());
                            var namespaces = new XmlNamespaceManager(xml.NameTable);
                            namespaces.AddNamespace("b", "http://schemas.microsoft.com/developer/msbuild/2003");
                            var node = xml.SelectSingleNode("/b:Project/b:PropertyGroup[not(@Condition)]/b:StyleCopTreatErrorsAsWarnings", namespaces);
                            if (null != node &&
                                null != node.ParentNode)
                            {
                                node.ParentNode.RemoveChild(node);
                            }

                            using (var stream = file.Info.Open(FileMode.Append, FileAccess.Write, FileShare.None))
                            {
                                using (var writer = new StreamWriter(stream))
                                {
                                    writer.Write(xml.OuterXml);
                                }
                            }
                        }
                    }
                }

                var obj = new CSharpProjectCompliance
                {
                    BuildEngine = new Mock<IBuildEngine>().Object,
                    Paths = new ITaskItem[]
                    {
                        new TaskItem(file.Info.FullName)
                    }
                };

                Assert.False(obj.Execute());
            }
        }

        [Fact]
        public void op_Execute_IEnumerable_whenTreatWarningsAsErrorsMissing()
        {
            using (var file = new TempFile())
            {
                using (var resource = Assembly.GetExecutingAssembly().GetManifestResourceStream(@"Cavity.Build.CSharpProjectCompliance.xml"))
                {
                    if (null != resource)
                    {
                        using (var reader = new StreamReader(resource))
                        {
                            var xml = new XmlDocument();
                            xml.LoadXml(reader.ReadToEnd());
                            var namespaces = new XmlNamespaceManager(xml.NameTable);
                            namespaces.AddNamespace("b", "http://schemas.microsoft.com/developer/msbuild/2003");
                            var node = xml.SelectSingleNode("/b:Project/b:PropertyGroup[@Condition=\" '$(Configuration)|$(Platform)' == 'Debug|AnyCPU' \"]/b:TreatWarningsAsErrors", namespaces);
                            if (null != node &&
                                null != node.ParentNode)
                            {
                                node.ParentNode.RemoveChild(node);
                            }

                            using (var stream = file.Info.Open(FileMode.Append, FileAccess.Write, FileShare.None))
                            {
                                using (var writer = new StreamWriter(stream))
                                {
                                    writer.Write(xml.OuterXml);
                                }
                            }
                        }
                    }
                }

                var obj = new CSharpProjectCompliance
                {
                    BuildEngine = new Mock<IBuildEngine>().Object,
                    Paths = new ITaskItem[]
                    {
                        new TaskItem(file.Info.FullName)
                    }
                };

                Assert.False(obj.Execute());
            }
        }

        [Fact]
        public void op_Execute_IEnumerable_whenWarningLevelMissing()
        {
            using (var file = new TempFile())
            {
                using (var resource = Assembly.GetExecutingAssembly().GetManifestResourceStream(@"Cavity.Build.CSharpProjectCompliance.xml"))
                {
                    if (null != resource)
                    {
                        using (var reader = new StreamReader(resource))
                        {
                            var xml = new XmlDocument();
                            xml.LoadXml(reader.ReadToEnd());
                            var namespaces = new XmlNamespaceManager(xml.NameTable);
                            namespaces.AddNamespace("b", "http://schemas.microsoft.com/developer/msbuild/2003");
                            var node = xml.SelectSingleNode("/b:Project/b:PropertyGroup[@Condition=\" '$(Configuration)|$(Platform)' == 'Debug|AnyCPU' \"]/b:WarningLevel", namespaces);
                            if (null != node &&
                                null != node.ParentNode)
                            {
                                node.ParentNode.RemoveChild(node);
                            }

                            using (var stream = file.Info.Open(FileMode.Append, FileAccess.Write, FileShare.None))
                            {
                                using (var writer = new StreamWriter(stream))
                                {
                                    writer.Write(xml.OuterXml);
                                }
                            }
                        }
                    }
                }

                var obj = new CSharpProjectCompliance
                {
                    BuildEngine = new Mock<IBuildEngine>().Object,
                    Paths = new ITaskItem[]
                    {
                        new TaskItem(file.Info.FullName)
                    }
                };

                Assert.False(obj.Execute());
            }
        }

        [Fact]
        public void prop_Paths()
        {
            Assert.True(new PropertyExpectations<CSharpProjectCompliance>(p => p.Paths)
                            .IsAutoProperty<ITaskItem[]>()
                            .IsDecoratedWith<RequiredAttribute>()
                            .Result);
        }
    }
}