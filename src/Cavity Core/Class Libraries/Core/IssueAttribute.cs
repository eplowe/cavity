namespace Cavity
{
    using System;

    [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
    public sealed class IssueAttribute : Attribute
    {
        public IssueAttribute(string description)
            : this()
        {
            Description = description;
        }

        private IssueAttribute()
        {
        }

        public string Description { get; private set; }

        public string Reference { get; set; }
    }
}