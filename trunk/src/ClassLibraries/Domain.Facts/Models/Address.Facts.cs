namespace Cavity.Models
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
#if !NET20
    using System.Linq;
#endif
    using Moq;
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

        [Fact]
        public void op_ToString()
        {
            const string expected = "example";
            var renderer = new Mock<IRenderAddress>();
            renderer
                .Setup(x => x.ToString(It.IsAny<Address>()))
                .Returns(expected)
                .Verifiable();
            var obj = new Address(renderer.Object);

            Assert.Equal(expected, obj.ToString());
        }

        [Fact]
        public void op_ToString_whenDefaultRenderer()
        {
            const string expected = "Flat A, 123";
            var obj = new Address
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

            Assert.Equal(expected, obj.ToString());
        }

        [Fact]
        public void op_ToString_whenDefaultRenderer_andEmpty()
        {
            var expected = string.Empty;
            var obj = new Address();

            Assert.Equal(expected, obj.ToString());
        }
    }
}