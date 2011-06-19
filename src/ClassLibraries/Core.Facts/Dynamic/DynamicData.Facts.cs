namespace Cavity.Dynamic
{
    using System;
    using System.Collections.Generic;
    using System.Dynamic;
    using Cavity;
    using Xunit;

    public sealed class DynamicDataFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<DynamicData>()
                .DerivesFrom<DynamicObject>()
                .IsConcreteClass()
                .IsSealed()
                .HasDefaultConstructor()
                .IsNotDecorated()
                .Result);
        }

        [Fact]
        public void ctor()
        {
            Assert.NotNull(new DynamicData());
        }

        [Fact]
        public void example()
        {
            const string expected = "example";

            dynamic obj = new DynamicData();
            obj.Example = expected;

            var actual = obj.Example;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void example_whenNotSet()
        {
            dynamic obj = new DynamicData();

            Assert.Throws<KeyNotFoundException>(() => obj.Example);
        }

        [Fact]
        public void op_GetDynamicMemberNames()
        {
            dynamic obj = new DynamicData();
            obj.Example = string.Empty;

            foreach (var actual in ((DynamicData)obj).GetDynamicMemberNames())
            {
                Assert.Equal("Example", actual);
            }
        }

        [Fact]
        public void op_TryGetMember_GetMemberBinder_object()
        {
            object result;
            Assert.Throws<ArgumentNullException>(() => new DynamicData().TryGetMember(null, out result));
        }

        [Fact]
        public void op_TrySetMember_SetMemberBinder_object()
        {
            Assert.Throws<ArgumentNullException>(() => new DynamicData().TrySetMember(null, "example"));
        }
    }
}