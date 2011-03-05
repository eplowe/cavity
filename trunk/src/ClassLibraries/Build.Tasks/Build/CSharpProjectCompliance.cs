namespace Cavity.Build
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
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
        public ITaskItem[] Projects { get; set; }

        [Required]
        [SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "XPath", Justification = "Following the Microsoft naming convention.")]
        public string XPath { get; set; }

        public override bool Execute()
        {
            Log.LogMessage(MessageImportance.Normal, Resources.CSharpProjectCompliance_Execute_Message, XPath);
            return Execute(Projects);
        }

        private bool Execute(IEnumerable<ITaskItem> projects)
        {
            if (null == projects)
            {
                Log.LogError(Resources.CSharpProjectCompliance_PathsNull_Message);
                return false;
            }

            if (0 == projects.Count())
            {
                Log.LogWarning(Resources.CSharpProjectCompliance_PathsEmpty_Message);
                return false;
            }

            var result = false;
            if (0 == projects.Where(path => !Execute(path)).Count())
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
            var xml = new XmlDocument();
            xml.Load(file.FullName);

            return Execute(file, xml);
        }

        private bool Execute(FileSystemInfo file,
                             IXPathNavigable xml)
        {
            return Execute(file, xml.CreateNavigator());
        }

        private bool Execute(FileSystemInfo file,
                             XPathNavigator navigator)
        {
            return Execute(file, navigator, navigator.NameTable);
        }

        private bool Execute(FileSystemInfo file,
                             XPathNavigator navigator,
                             XmlNameTable nameTable)
        {
            var namespaces = new XmlNamespaceManager(nameTable);
            namespaces.AddNamespace("b", "http://schemas.microsoft.com/developer/msbuild/2003");

            return Execute(file, navigator, namespaces);
        }

        private bool Execute(FileSystemInfo file,
                             XPathNavigator navigator,
                             IXmlNamespaceResolver namespaces)
        {
            ////var result = true;

            ////foreach (var xpath in new[]
            ////{
            ////    "0=count(/b:Project/b:PropertyGroup[@Condition][not(b:WarningLevel[text()='4'])])",
            ////    "0=count(/b:Project/b:PropertyGroup[@Condition][not(b:TreatWarningsAsErrors[text()='true'])])",
            ////    "0=count(/b:Project/b:PropertyGroup[@Condition][not(b:RunCodeAnalysis[text()='true'])])",
            ////    "0=count(/b:Project/b:PropertyGroup[@Condition][not(b:CodeAnalysisRuleSet[text()])])",
            ////    "0=count(/b:Project/b:PropertyGroup[@Condition][not(b:ErrorReport[text()='prompt'])])",
            ////    "1=count(/b:Project/b:PropertyGroup[not(@Condition)]/b:SignAssembly[text()])",
            ////    "1=count(/b:Project/b:PropertyGroup[not(@Condition)]/b:AssemblyOriginatorKeyFile[text()])",
            ////    "1=count(/b:Project/b:PropertyGroup[not(@Condition)]/b:AppDesignerFolder[text()='Properties'])",
            ////    "1=count(/b:Project/b:PropertyGroup[not(@Condition)]/b:StyleCopTreatErrorsAsWarnings[text()='false'])",
            ////    @"1=count(/b:Project/b:Import[@Project='$(MSBuildExtensionsPath)\Microsoft\StyleCop\v4.3\Microsoft.StyleCop.targets'])",
            ////    @"1=count(/b:Project/b:ItemGroup/b:Compile[@Include='Properties\AssemblyInfo.cs'])",
            ////    @"1=count(/b:Project/b:ItemGroup/b:Compile[@Include='..\..\Build.cs']/b:Link[text()='Properties\Build.cs'])",
            ////    @"1=count(/b:Project/b:ItemGroup/b:CodeAnalysisDictionary[@Include]/b:Link[text()])"
            ////})
            ////{
            ////    if (!Execute(file, navigator, namespaces, xpath))
            ////    {
            ////        result = false;
            ////    }
            ////}
            if (!navigator.Evaluate<bool>(XPath, namespaces))
            {
                Log.LogError(Resources.CSharpProjectCompliance_XPath_Message, file, XPath);
                return false;
            }

            return true;
        }
    }
}