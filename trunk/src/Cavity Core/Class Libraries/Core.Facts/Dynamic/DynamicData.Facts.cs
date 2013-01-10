namespace Cavity.Dynamic
{
    using System;
    using System.Dynamic;

    using Xunit;

    public sealed class DynamicDataFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<DynamicData>().DerivesFrom<DynamicObject>()
                                                           .IsConcreteClass()
                                                           .IsUnsealed()
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
        public void derived()
        {
            const string expected = "bar";

            dynamic obj = new DerivedDynamicData();

            var actual = obj.Foo;

            Assert.Equal(expected, actual);
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

            Assert.Null(obj.Example);
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

            // ReSharper disable AssignNullToNotNullAttribute
            Assert.Throws<ArgumentNullException>(() => new DynamicData().TryGetMember(null, out result));

            // ReSharper restore AssignNullToNotNullAttribute
        }

        [Fact]
        public void op_TrySetMember_SetMemberBinder_object()
        {
            // ReSharper disable AssignNullToNotNullAttribute
            Assert.Throws<ArgumentNullException>(() => new DynamicData().TrySetMember(null, "example"));

            // ReSharper restore AssignNullToNotNullAttribute
        }
    }
}