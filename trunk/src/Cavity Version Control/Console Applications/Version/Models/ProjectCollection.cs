namespace Cavity.Models
{
    using System.Collections.ObjectModel;
    using System.IO;

    public sealed class ProjectCollection : Collection<Project>
    {
        private ProjectCollection()
        {
        }

        public static ProjectCollection Load(string working)
        {
            return Load(new DirectoryInfo(working));
        }

        public void Add(FileInfo project)
        {
            Add(Project.Load(project));
        }

        private static ProjectCollection Load(DirectoryInfo working)
        {
            var result = new ProjectCollection();

            foreach (var file in working.EnumerateFiles("*.csproj", SearchOption.AllDirectories))
            {
                result.Add(file);
            }

            return result;
        }
    }
}