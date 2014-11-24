namespace Cavity.Collections
{
    using System;
    using System.Collections.Generic;
    using Xunit;

    public sealed class ICollectionExtensionMethodsFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(typeof(ICollectionExtensionMethods).IsStatic());
        }

        [Fact]
        public void op_Append_ICollectionOfTNull_paramsOfT()
        {
            Assert.Throws<ArgumentNullException>(() => (null as ICollection<string>).Append("a,b,c".Split(',')));
        }

        [Fact]
        public void op_Append_ICollectionOfT_paramsOfT()
        {
            const string expected = "a,b,c";

            var list = new List<string>();

            list.Append(expected.Split(','));

            var actual = list.Concat(',');

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_Append_ICollectionOfT_paramsOfTEmpty()
        {
            var list = new List<string>();

            list.Append(new List<string>().ToArray());

            Assert.Empty(list);
        }

        [Fact]
        public void op_Append_ICollectionOfT_paramsOfTMissing()
        {
            var list = new List<string>();

            list.Append();

            Assert.Empty(list);
        }

        [Fact]
        public void op_Append_ICollectionOfT_paramsOfTNull()
        {
            Assert.Throws<ArgumentNullException>(() => new List<string>().Append(null));
        }

        [Fact]
        public void op_NotContains_ICollectionOfTNull_T()
        {
            Assert.Throws<ArgumentNullException>(() => (null as ICollection<string>).NotContains("example"));
        }

        [Fact]
        public void op_NotContains_ICollectionOfT_T()
        {
            var list = new List<string>
                           {
                               "example"
                           };

            Assert.False(list.NotContains("example"));
            Assert.True(list.NotContains("test"));
        }

        [Fact]
        public void op_TryAdd_ICollectionOfTNull_paramsOfT()
        {
            Assert.Throws<ArgumentNullException>(() => (null as ICollection<string>).TryAdd("example"));
        }

        [Fact]
        public void op_TryAdd_ICollectionOfT_T_whenFalse()
        {
            var list = new List<string>
                           {
                               "example"
                           };

            Assert.False(list.TryAdd("example"));
        }

        [Fact]
        public void op_TryAdd_ICollectionOfT_T_whenTrue()
        {
            var list = new HashSet<string>();

            Assert.True(list.TryAdd("example"));
        }
    }
}