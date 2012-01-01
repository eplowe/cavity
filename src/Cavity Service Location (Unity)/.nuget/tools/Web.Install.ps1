param($installPath, $toolsPath, $package, $project)

$project.ProjectItems.Item("unity.config").Properties.Item("CopyToOutputDirectory").Value = 1
