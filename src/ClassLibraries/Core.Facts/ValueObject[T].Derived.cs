namespace Cavity
{
    using System;

    public sealed class ValueObjectDerived : ValueObject<ValueObjectDerived>
    {
        public ValueObjectDerived()
        {
            this.RegisterProperty(x => x.DateTimeProperty);
            this.RegisterProperty(x => x.Int32Property);
            this.RegisterProperty(x => x.StringProperty);
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
            this.RegisterProperty(null);
        }
    }
}