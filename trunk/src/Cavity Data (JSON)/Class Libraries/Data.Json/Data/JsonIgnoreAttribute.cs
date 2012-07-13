namespace Cavity.Data
{
    using System;

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public sealed class JsonIgnoreAttribute : Attribute
    {
    }
}