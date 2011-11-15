param($installPath, $toolsPath, $package, $project)

$project.ProjectItems.Item("castle.config").Properties.Item("CopyToOutputDirectory").Value = 1
