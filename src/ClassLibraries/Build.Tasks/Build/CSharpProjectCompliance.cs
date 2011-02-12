namespace Cavity.Build
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Xml;
    using System.Xml.XPath;
    using Cavity.Properties;
    using Cavity.Xml.XPath;
    using Microsoft.Build.Framework;
    using Microsoft.Build.Utilities;

    public sealed class CSharpProjectCompliance : Task
    {
        [Required]
        public ITaskItem[] Paths { get; set; }

        public override bool Execute()
        {
            return Execute(Paths);
        }

        private bool Execute(IEnumerable<ITaskItem> paths)
        {
            if (null == paths)
            {
                Log.LogError(Resources.CSharpProjectCompliance_PathsNull_Message);
                return false;
            }

            if (0 == paths.Count())
            {
                Log.LogWarning(Resources.CSharpProjectCompliance_PathsEmpty_Message);
                return false;
            }

            var result = false;
            if (0 == paths.Where(path => !Execute(path)).Count())
            {
                result = true;
            }

            return result;
        }

        private bool Execute(ITaskItem path)
        {
            return Execute(new FileInfo(path.ItemSpec));
        }

        private bool Execute(FileSystemInfo file)
        {
            Log.LogMessage(MessageImportance.Normal, Resources.CSharpProjectCompliance_File_Message, file.FullName);
            var xml = new XmlDocument();
            xml.Load(file.FullName);

            return Execute(xml);
        }

        private bool Execute(IXPathNavigable xml)
        {
            return Execute(xml.CreateNavigator());
        }

        private bool Execute(XPathNavigator navigator)
        {
            return Execute(navigator, navigator.NameTable);
        }

        private bool Execute(XPathNavigator navigator,
                             XmlNameTable nameTable)
        {
            var namespaces = new XmlNamespaceManager(nameTable);
            namespaces.AddNamespace("b", "http://schemas.microsoft.com/developer/msbuild/2003");

            return Execute(navigator, namespaces);
        }

        private bool Execute(XPathNavigator navigator,
                             IXmlNamespaceResolver namespaces)
        {
            var result = true;

            foreach (var xpath in new[]
            {
                "0=count(/b:Project/b:PropertyGroup[@Condition][not(b:WarningLevel[text()='4'])])",
                "0=count(/b:Project/b:PropertyGroup[@Condition][not(b:TreatWarningsAsErrors[text()='true'])])",
                "0=count(/b:Project/b:PropertyGroup[@Condition][not(b:RunCodeAnalysis[text()='true'])])",
                "0=count(/b:Project/b:PropertyGroup[@Condition][not(b:CodeAnalysisRuleSet[text()])])",
                "0=count(/b:Project/b:PropertyGroup[@Condition][not(b:ErrorReport[text()='prompt'])])",
                "1=count(/b:Project/b:PropertyGroup[not(@Condition)]/b:SignAssembly[text()='true'])",
                "1=count(/b:Project/b:PropertyGroup[not(@Condition)]/b:AssemblyOriginatorKeyFile[text()])",
                "1=count(/b:Project/b:PropertyGroup[not(@Condition)]/b:AppDesignerFolder[text()='Properties'])",
                "1=count(/b:Project/b:PropertyGroup[not(@Condition)]/b:StyleCopTreatErrorsAsWarnings[text()='false'])",
                @"1=count(/b:Project/b:Import[@Project='$(MSBuildExtensionsPath)\Microsoft\StyleCop\v4.3\Microsoft.StyleCop.targets'])",
                @"1=count(/b:Project/b:ItemGroup/b:Compile[@Include='Properties\AssemblyInfo.cs'])",
                @"1=count(/b:Project/b:ItemGroup/b:Compile[@Include='..\..\Build.cs']/b:Link[text()='Properties\Build.cs'])",
                @"1=count(/b:Project/b:ItemGroup/b:CodeAnalysisDictionary[@Include]/b:Link[text()])"
            })
            {
                if (!Execute(navigator, namespaces, xpath))
                {
                    result = false;
                }
            }

            return result;
        }

        private bool Execute(XPathNavigator navigator,
                             IXmlNamespaceResolver namespaces,
                             string xpath)
        {
            if (!navigator.Evaluate<bool>(xpath, namespaces))
            {
                Log.LogWarning(Resources.CSharpProjectCompliance_XPath_Message.FormatWith(xpath));
                return false;
            }

            return true;
        }
    }
}