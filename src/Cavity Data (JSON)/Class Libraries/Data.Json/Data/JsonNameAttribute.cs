namespace Cavity.Data
{
    using System;

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public sealed class JsonNameAttribute : Attribute
    {
        private string _name;

        public JsonNameAttribute(string name)
            : this()
        {
            Name = name;
        }

        private JsonNameAttribute()
        {
        }

        public string Name
        {
            get
            {
                return _name;
            }

            private set
            {
                if (null == value)
                {
                    throw new ArgumentNullException("value");
                }

                if (0 == value.Length)
                {
                    throw new ArgumentOutOfRangeException("value");
                }

                _name = value;
            }
        }
    }
}