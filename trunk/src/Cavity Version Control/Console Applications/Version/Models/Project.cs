namespace Cavity.Models
{
    using System.IO;

    public sealed class Project
    {
        private Project()
        {
        }

        public FileInfo CSharp { get; set; }

        public static Project Load(FileInfo project)
        {
            return new Project
                       {
                           CSharp = project
                       };
        }
    }
}