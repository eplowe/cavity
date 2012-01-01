param($installPath, $toolsPath, $package, $project)

$project.ProjectItems.Item("StructureMap.config").Properties.Item("CopyToOutputDirectory").Value = 1
