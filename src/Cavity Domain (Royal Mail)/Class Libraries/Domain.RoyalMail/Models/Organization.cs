namespace Cavity.Models
{
    using System;

    public class Organization : ComparableObject
    {
        public virtual string Department { get; set; }

        public virtual string Name { get; set; }

        public override string ToString()
        {
            return "{0}{1}{2}".FormatWith(Name,
                                          string.IsNullOrEmpty(Department) ? string.Empty : Environment.NewLine,
                                          Department);
        }
    }
}