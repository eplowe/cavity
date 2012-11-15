namespace Cavity
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using Xunit;

    public sealed class SerialFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(typeof(Serial).IsStatic());
        }

        [Fact]
        public void op_ForEach_IEnumerableOfTNull_ActionOfT()
        {
            var buffer = new StringBuilder();

            Assert.Throws<ArgumentNullException>(() => Serial.ForEach(null as IEnumerable<char>, c => buffer.Append(c)));
        }

        [Fact]
        public void op_ForEach_IEnumerableOfT_ActionOfT()
        {
            const string expected = "example";

            var buffer = new StringBuilder();
            Serial.ForEach(expected.ToCharArray(), c => buffer.Append(c));
            var actual = buffer.ToString();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_ForEach_IEnumerableOfT_ActionOfTNull()
        {
            const string expected = "example";

            Assert.Throws<ArgumentNullException>(() => Serial.ForEach(expected.ToCharArray(), null));
        }

        [Fact]
        public void op_Invoke_Actions()
        {
            var actual = Date.Today.UniversalTime;
            var expected = actual.Increment();

            Serial.Invoke(() => actual++);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_Invoke_ActionsNull()
        {
            Assert.Throws<ArgumentNullException>(() => Serial.Invoke(null));
        }
    }
}