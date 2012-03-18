namespace Cavity.Configuration
{
    using System;
    using System.IO;

    using Cavity.IO;
    using Cavity.Reflection;
    using Cavity.Threading;

    using Xunit;

    public sealed class TimingFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(typeof(Timing).IsStatic());
        }

        [Fact]
        public void op_Due()
        {
            var file = Timing.ToFile(typeof(StandardTask), "wait");
            try
            {
                var expected = DateTime.UtcNow.AddMinutes(-1);
                file.CreateNew(expected.ToXmlString());

                var actual = Timing.Due<StandardTask>();

                Assert.Equal(expected, actual);
            }
            finally
            {
                file.Delete();
            }
        }

        [Fact]
        public void op_ToFile_TypeNull_string()
        {
            Assert.Throws<ArgumentNullException>(() => Timing.ToFile(null, "example"));
        }

        [Fact]
        public void op_ToFile_Type_string()
        {
            var type = typeof(StandardTask);

            var expected = Path.Combine(type.Assembly.Directory().FullName, "StandardTask.example");
            var actual = Timing.ToFile(type, "example").FullName;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_ToFile_Type_stringEmpty()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => Timing.ToFile(typeof(StandardTask), string.Empty));
        }

        [Fact]
        public void op_ToFile_Type_stringNull()
        {
            Assert.Throws<ArgumentNullException>(() => Timing.ToFile(typeof(StandardTask), null));
        }

        [Fact]
        public void op_Wait_TimeSpan()
        {
            var file = Timing.ToFile(typeof(StandardTask), "wait");
            try
            {
                Assert.False(Timing.Wait<StandardTask>(TimeSpan.FromHours(1)));

                var due = file.ReadToEnd().To<DateTime>();

                Assert.True(DateTime.UtcNow < due);
            }
            finally
            {
                file.Delete();
            }
        }

        [Fact]
        public void op_Wait_TimeSpan_whenDue()
        {
            var file = Timing.ToFile(typeof(StandardTask), "wait");
            try
            {
                file.CreateNew(DateTime.UtcNow.AddMinutes(-1).ToXmlString());

                Assert.False(Timing.Wait<StandardTask>(TimeSpan.FromMinutes(1)));

                var due = file.ReadToEnd().To<DateTime>();

                Assert.True(DateTime.UtcNow < due);
            }
            finally
            {
                file.Delete();
            }
        }

        [Fact]
        public void op_Wait_TimeSpan_whenNotDue()
        {
            var file = Timing.ToFile(typeof(StandardTask), "wait");
            try
            {
                var expected = DateTime.UtcNow.AddMinutes(1).ToXmlString();

                file.CreateNew(expected);

                Assert.True(Timing.Wait<StandardTask>(TimeSpan.FromHours(1)));

                var actual = file.ReadToEnd();

                Assert.Equal(expected, actual);
            }
            finally
            {
                file.Delete();
            }
        }

        [Fact]
        public void op_Wait_whenNoFile()
        {
            var expected = DateTime.MinValue;
            var actual = Timing.Due<StandardTask>();

            Assert.Equal(expected, actual);
        }
    }
}