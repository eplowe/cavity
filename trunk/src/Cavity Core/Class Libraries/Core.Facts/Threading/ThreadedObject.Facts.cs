namespace Cavity.Threading
{
    using System.Threading;

    using Xunit;

    public sealed class ThreadedObjectFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<ThreadedObject>().DerivesFrom<DisposableObject>()
                                                              .IsAbstractBaseClass()
                                                              .IsNotDecorated()
                                                              .Result);
        }

        [Fact]
        public void prop_CancellationToken()
        {
            Assert.True(new PropertyExpectations<ThreadedObject>(x => x.CancellationToken)
                            .TypeIs<CancellationToken>()
                            .Result);
        }

        [Fact]
        public void prop_CancellationToken_set()
        {
            var expected = new CancellationToken();
            ThreadedObject obj = new DerivedThreadedObject
                                     {
                                         CancellationToken = expected
                                     };

            var actual = obj.CancellationToken;

            Assert.Equal(expected, actual);
        }
    }
}