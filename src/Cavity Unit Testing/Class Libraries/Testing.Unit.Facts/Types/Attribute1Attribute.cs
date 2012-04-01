namespace Cavity.Types
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    [SuppressMessage("Microsoft.Performance", "CA1813:AvoidUnsealedAttributes", Justification = "This is for testing purposes.")]
    [AttributeUsage(AttributeTargets.All)]
    public class Attribute1Attribute : Attribute
    {
    }
}