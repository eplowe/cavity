namespace Cavity.Models
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using Xunit;

    public sealed class AddressFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<Address>()
                            .DerivesFrom<object>()
                            .IsConcreteClass()
                            .IsUnsealed()
                            .HasDefaultConstructor()
                            .IsNotDecorated()
                            .Implements<IEnumerable<IAddressLine>>()
                            .Result);
        }

        [Fact]
        public void ctor()
        {
            Assert.NotNull(new Address());
        }

        [Fact]
        public void op_Add_IAddressLine()
        {
            var obj = new Address();
            var expected = new Thoroughfare();
            obj.Add(expected);

            Assert.Same(expected, obj.First());
        }

        [Fact]
        public void op_Add_IAddressLineNull()
        {
            Assert.Throws<ArgumentNullException>(() => new Address().Add(null));
        }

        [Fact]
        public void op_GetEnumerator()
        {
            var expected = new Thoroughfare();
            var obj = new Address
            {
                expected
            };

            Assert.Same(expected, obj.First());
        }

        [Fact]
        public void op_GetEnumerator_ofIAddressLine()
        {
            IEnumerable obj = new Address();

            Assert.NotNull(obj.GetEnumerator());
        }

        [Fact]
        public void op_Line_ofBuilding()
        {
            var expected = new Building();
            var obj = new Address
            {
                new Thoroughfare(),
                expected
            };

            Assert.Same(expected, obj.Line<Building>());
        }

        [Fact]
        public void op_Line_ofBuilding_whenMissing()
        {
            var obj = new Address
            {
                new Thoroughfare()
            };

            Assert.Null(obj.Line<Building>());
        }
    }
}