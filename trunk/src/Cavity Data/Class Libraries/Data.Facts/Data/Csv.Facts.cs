namespace Cavity.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Cavity.Collections;

    using Xunit;

    public sealed class CsvFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(typeof(Csv).IsStatic());
        }

        [Fact]
        public void op_Header_KeyStringDictionary()
        {
            var obj = new KeyStringDictionary
                          {
                              new KeyStringPair("A,B", string.Empty), 
                              new KeyStringPair("C", string.Empty)
                          };

            const string expected = "\"A,B\",C";
            var actual = Csv.Header(obj);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_Header_KeyStringDictionaryEmpty()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => Csv.Header(new KeyStringDictionary()));
        }

        [Fact]
        public void op_Header_KeyStringDictionaryNull()
        {
            Assert.Throws<ArgumentNullException>(() => Csv.Header(null));
        }

        [Fact]
        public void op_Header_KeyStringDictionary_whenEmptyValue()
        {
            var obj = new KeyStringDictionary
                          {
                              new KeyStringPair(string.Empty, "x"), 
                              new KeyStringPair("A,B", "x")
                          };

            const string expected = ",\"A,B\"";
            var actual = Csv.Header(obj);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_Line_IEnumerableString()
        {
            var obj = new List<string>
                          {
                              "123", 
                              "left,right"
                          };

            const string expected = "123,\"left,right\"";
            var actual = Csv.Line(obj);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_Line_IEnumerableStringEmpty()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => Csv.Line(new List<string>()));
        }

        [Fact]
        public void op_Line_IEnumerableStringNull()
        {
            Assert.Throws<ArgumentNullException>(() => Csv.Line(null as IEnumerable<string>));
        }

        [Fact]
        public void op_Line_IEnumerableString_whenEmptyValue()
        {
            var obj = new List<string>
                          {
                              string.Empty, 
                              "left,right"
                          };

            const string expected = ",\"left,right\"";
            var actual = Csv.Line(obj);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_Line_KeyStringDictionary()
        {
            var obj = new KeyStringDictionary
                          {
                              new KeyStringPair("A", "123"), 
                              new KeyStringPair("B", "left,right")
                          };

            const string expected = "123,\"left,right\"";
            var actual = Csv.Line(obj);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_Line_KeyStringDictionaryEmpty()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => Csv.Line(new KeyStringDictionary()));
        }

        [Fact]
        public void op_Line_KeyStringDictionaryNull()
        {
            Assert.Throws<ArgumentNullException>(() => Csv.Line(null as KeyStringDictionary));
        }

        [Fact]
        public void op_Line_KeyStringDictionaryNull_IListOfString()
        {
            var columns = new List<string>
                              {
                                  "A"
                              };
            Assert.Throws<ArgumentNullException>(() => Csv.Line(null, columns));
        }

        [Fact]
        public void op_Line_KeyStringDictionaryNull_string()
        {
            Assert.Throws<ArgumentNullException>(() => Csv.Line(null, "A,C"));
        }

        [Fact]
        public void op_Line_KeyStringDictionary_IListOfString()
        {
            var obj = new KeyStringDictionary
                          {
                              new KeyStringPair("A", "123"), 
                              new KeyStringPair("B", "ignore"), 
                              new KeyStringPair("C", "left,right")
                          };

            const string expected = "123,\"left,right\"";
            var actual = Csv.Line(obj, "A,C".Split(',').ToList());

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_Line_KeyStringDictionary_IListOfStringEmpty()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => Csv.Line(new KeyStringDictionary(), new List<string>()));
        }

        [Fact]
        public void op_Line_KeyStringDictionary_IListOfStringNull()
        {
            Assert.Throws<ArgumentNullException>(() => Csv.Line(new KeyStringDictionary(), null as IList<string>));
        }

        [Fact]
        public void op_Line_KeyStringDictionary_IListOfString_whenColumnsNotFound()
        {
            Assert.Throws<KeyNotFoundException>(() => Csv.Line(new KeyStringDictionary(), "A,B".Split(',').ToList()));
        }

        [Fact]
        public void op_Line_KeyStringDictionary_string()
        {
            var obj = new KeyStringDictionary
                          {
                              new KeyStringPair("A", "123"), 
                              new KeyStringPair("B", "ignore"), 
                              new KeyStringPair("C", "left,right")
                          };

            const string expected = "123,\"left,right\"";
            var actual = Csv.Line(obj, "A,C");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_Line_KeyStringDictionary_stringEmpty()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => Csv.Line(new KeyStringDictionary(), string.Empty));
        }

        [Fact]
        public void op_Line_KeyStringDictionary_stringNull()
        {
            Assert.Throws<ArgumentNullException>(() => Csv.Line(new KeyStringDictionary(), null as string));
        }

        [Fact]
        public void op_Line_KeyStringDictionary_string_whenColumnsNotFound()
        {
            Assert.Throws<KeyNotFoundException>(() => Csv.Line(new KeyStringDictionary(), "A"));
        }

        [Fact]
        public void op_Line_KeyStringDictionary_whenEmptyValue()
        {
            var obj = new KeyStringDictionary
                          {
                              new KeyStringPair("A", string.Empty), 
                              new KeyStringPair("B", "left,right")
                          };

            const string expected = ",\"left,right\"";
            var actual = Csv.Line(obj);

            Assert.Equal(expected, actual);
        }
    }
}