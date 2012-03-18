namespace Cavity.Models
{
    using Cavity.Threading;

    using Xunit;

    public sealed class IProcessFileFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<IProcessFile>()
                            .IsInterface()
                            .Implements<IThreadedObject>()
                            .Result);
        }
    }
}