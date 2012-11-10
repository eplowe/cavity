namespace Cavity
{
    using System;
    using System.Globalization;

    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    public sealed class UserStoryAttribute : Attribute
    {
        public string AsA { get; set; }

        public string IWant { get; set; }

        public string SoThat { get; set; }

        public override string ToString()
        {
            return string.Format(
                CultureInfo.InvariantCulture, 
                "As a {0}, I want {1} so that {2}.", 
                AsA, 
                IWant, 
                SoThat);
        }
    }
}