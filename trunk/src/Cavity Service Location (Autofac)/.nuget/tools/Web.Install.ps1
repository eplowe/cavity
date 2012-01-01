param($installPath, $toolsPath, $package, $project)

$project.ProjectItems.Item("autofac.config").Properties.Item("CopyToOutputDirectory").Value = 1
