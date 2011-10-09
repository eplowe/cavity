namespace Cavity
{
    using System;

    [AttributeUsage(AttributeTargets.All)]
    public sealed class CommentAttribute : Attribute
    {
        public CommentAttribute(string value)
            : this()
        {
            Value = value;
        }

        private CommentAttribute()
        {
        }

        public string Value { get; private set; }
    }
}