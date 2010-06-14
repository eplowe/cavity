namespace Cavity
{
    using System.Collections.ObjectModel;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using Cavity.Fluent;
    using Cavity.Tests;

    public sealed class ObjectExpectations<T> : ITestObjectStyle, ITestObject
    {
        public ObjectExpectations()
        {
            this.Items = new Collection<ITestExpectation>();
        }

        bool ITestObject.Result
        {
            get
            {
                return 0 == this.Items.Where(x => !x.Check()).Count();
            }
        }

        private Collection<ITestExpectation> Items
        {
            get;
            set;
        }

        [SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Justification = "Inference brings no benefit here.")]
        public ITestObjectStyle DerivesFrom<TBase>()
        {
            this.Items.Add(new BaseClassTest<T>(typeof(TBase)));
            return this;
        }

        ITestObject ITestObjectStyle.IsAbstractBaseClass()
        {
            this.Items.Add(new AbstractBaseClassTest<T>());
            return this;
        }
    }
}