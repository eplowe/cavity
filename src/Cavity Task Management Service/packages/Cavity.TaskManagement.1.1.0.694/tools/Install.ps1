param($installPath, $toolsPath, $package, $project)

$project.ProjectItems.Item("Cavity.TaskManagement.dll.config").Properties.Item("CopyToOutputDirectory").Value = 1