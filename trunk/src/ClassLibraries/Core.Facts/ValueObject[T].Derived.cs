namespace Cavity
{
    using System;
    using System.Globalization;

    public sealed class ValueObjectDerived : ValueObject<ValueObjectDerived>
    {
        public ValueObjectDerived()
        {
            RegisterProperty(x => x.DateTimeProperty);
            RegisterProperty(x => x.Int32Property);
            RegisterProperty(x => x.StringProperty);
        }

        public DateTime DateTimeProperty
        {
            get;
            set;
        }

        public int Int32Property
        {
            get;
            set;
        }

        public string StringProperty
        {
            get;
            set;
        }

        public void RegisterNullProperty()
        {
            RegisterProperty(null);
        }
    }
}