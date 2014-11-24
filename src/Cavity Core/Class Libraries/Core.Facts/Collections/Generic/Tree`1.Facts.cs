namespace Cavity.Collections.Generic
{
    using System;
    using Xunit;

    public sealed class TreeOfTFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<Tree<int>>().DerivesFrom<object>()
                                                         .IsConcreteClass()
                                                         .IsUnsealed()
                                                         .HasDefaultConstructor()
                                                         .IsNotDecorated()
                                                         .Result);
        }

        [Fact]
        public void ctor()
        {
            Assert.NotNull(new Tree<int>());
        }

        [Fact]
        public void ctor_T()
        {
            Assert.NotNull(new Tree<int>(123));
        }

        [Fact]
        public void op_Add_T()
        {
            var obj = new Tree<string>();

            const string expected = "example";
            var actual = obj.Add(expected);

            Assert.Same(expected, actual.Value);
            Assert.Same(obj, actual.Parent);
            Assert.Empty(actual);
            Assert.NotEmpty(obj);
            Assert.Equal(1, obj.Count);
        }

        [Fact]
        public void op_Add_Tree()
        {
            var obj = new Tree<int>();

            var expected = new Tree<int>();
            var actual = obj.Add(expected);

            Assert.Same(expected, actual);
            Assert.Same(obj, actual.Parent);
            Assert.Empty(actual);
            Assert.NotEmpty(obj);
            Assert.Equal(1, obj.Count);
        }

        [Fact]
        public void op_Add_TreeNull()
        {
            Assert.Throws<ArgumentNullException>(() => new Tree<int>().Add(null));
        }

        [Fact]
        public void op_Clear()
        {
            var obj = new Tree<int>
                          {
                              new Tree<int>()
                          };

            obj.Clear();

            Assert.Empty(obj);
        }

        [Fact]
        public void op_Contains_Tree()
        {
            var expected = new Tree<int>();
            var obj = new Tree<int>
                          {
                              expected
                          };

            Assert.True(obj.Contains(expected));
        }

        [Fact]
        public void op_Contains_TreeNull()
        {
            Assert.Throws<ArgumentNullException>(() => new Tree<int>().Contains(null));
        }

        [Fact]
        public void op_Contains_Tree_whenNotChild()
        {
            Assert.False(new Tree<int>().Contains(new Tree<int>()));
        }

        [Fact]
        public void op_GetEnumerator()
        {
            var expected = new Tree<int>();
            var obj = new Tree<int>
                          {
                              expected
                          };

            foreach (var actual in obj)
            {
                Assert.Same(expected, actual);
                Assert.Same(obj, actual.Parent);
                Assert.Empty(actual);
            }
        }

        [Fact]
        public void op_Remove_Tree()
        {
            var expected = new Tree<int>();
            var obj = new Tree<int>
                          {
                              expected
                          };

            obj.Remove(expected);

            Assert.Null(expected.Parent);
            Assert.Empty(obj);
        }

        [Fact]
        public void op_Remove_TreeNull()
        {
            Assert.Throws<ArgumentNullException>(() => new Tree<int>().Remove(null));
        }

        [Fact]
        public void op_Remove_Tree_whenNotChild()
        {
            new Tree<int>().Remove(new Tree<int>());
        }

        [Fact]
        public void prop_Count()
        {
            Assert.True(new PropertyExpectations<Tree<int>>(x => x.Count)
                            .TypeIs<int>()
                            .DefaultValueIs(0)
                            .IsNotDecorated()
                            .Result);
        }

        [Fact]
        public void prop_Parent()
        {
            Assert.True(new PropertyExpectations<Tree<int>>(x => x.Parent)
                            .TypeIs<Tree<int>>()
                            .DefaultValueIsNull()
                            .IsNotDecorated()
                            .Result);
        }

        [Fact]
        public void prop_Value()
        {
            Assert.True(new PropertyExpectations<Tree<int>>(x => x.Value)
                            .IsAutoProperty<int>()
                            .IsNotDecorated()
                            .Result);
        }
    }
}