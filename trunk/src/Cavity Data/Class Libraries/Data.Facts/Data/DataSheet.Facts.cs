namespace Cavity.Data
{
    using System.Diagnostics.CodeAnalysis;

    using Cavity;

    using Xunit;

    [SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "DataSheet", Justification = "This is the correct casing.")]
    public sealed class DataSheetFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<DataSheet>()
                            .DerivesFrom<object>()
                            .IsAbstractBaseClass()
                            .IsNotDecorated()
                            .Implements<IDataSheet>()
                            .Result);
        }

        [Fact]
        public void ctor()
        {
            Assert.NotNull(new DerivedDataSheet());
        }

        [Fact]
        public void op_AsOfT()
        {
            Assert.Empty(new DerivedDataSheet().As<TestDataEntry>());
        }

        [Fact]
        public void op_GetEnumerator()
        {
            Assert.Empty(new DerivedDataSheet());
        }

        [Fact]
        public void prop_Title()
        {
            Assert.True(new PropertyExpectations<DataSheet>(x => x.Title)
                            .IsNotDecorated()
                            .TypeIs<string>()
                            .Result);
        }
    }
}