namespace Cavity.Models
{
    using System;
    using System.Collections.Generic;
    using Cavity;
    using Xunit;

    public sealed class AddressRendererFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<AddressRenderer>()
                .DerivesFrom<object>()
                .IsConcreteClass()
                .IsSealed()
                .NoDefaultConstructor()
                .IsNotDecorated()
                .Implements<IRenderAddress>()
                .Result);
        }

        [Fact]
        public void prop_Default()
        {
            Assert.IsAssignableFrom<AddressRenderer>(AddressRenderer.Default);
        }

        [Fact]
        public void op_ToString_IEnumerableAddressLine()
        {
            var address = new List<IAddressLine>
            {
                new Building
                {
                    Original = "123"
                },
                new Door
                {
                    Original = "Flat A"
                }
            };

            const string expected = "Flat A, 123";
            var actual = AddressRenderer.Default.ToString(address);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_ToString_IEnumerableAddressLineEmpty()
        {
            var expected = string.Empty;
            var actual = AddressRenderer.Default.ToString(new List<IAddressLine>());

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_ToString_IEnumerableAddressLineNull()
        {
            Assert.Throws<ArgumentNullException>(() => AddressRenderer.Default.ToString(null));
        }
    }
}