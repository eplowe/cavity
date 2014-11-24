namespace Cavity.Collections.Generic
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Drawing;
    using System.Linq;
    using System.Text;
    using Xunit;
    using Xunit.Extensions;

    public sealed class MatrixOfTFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<Matrix<int>>().DerivesFrom<object>()
                                                           .IsConcreteClass()
                                                           .IsUnsealed()
                                                           .HasDefaultConstructor()
                                                           .IsNotDecorated()
                                                           .Implements<IEnumerable<int>>()
                                                           .Implements<IEquatable<Matrix<int>>>()
                                                           .Result);
        }

        [Fact]
        public void ctor()
        {
            Assert.NotNull(new Matrix<decimal>());
        }

        [Fact]
        public void ctor_Size()
        {
            const int expected = 10;
            var actual = new Matrix<decimal>(new Size(5, 2)).Count;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ctor_int_int()
        {
            const int expected = 10;
            var actual = new Matrix<decimal>(2, 5).Count;

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(-1, 0)]
        [InlineData(0, -1)]
        public void ctor_int_int_whenArgumentOutOfRangeException(int width,
                                                                 int height)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new Matrix<decimal>(width, height));
        }

        [Fact]
        public void opEquality_MatrixOfTNull_MatrixOfT()
        {
            Matrix<decimal> obj = null;
            var comparand = new Matrix<decimal>();

            // ReSharper disable ConditionIsAlwaysTrueOrFalse
            Assert.False(obj == comparand);

            // ReSharper restore ConditionIsAlwaysTrueOrFalse
        }

        [Fact]
        public void opEquality_MatrixOfT_MatrixOfTNull()
        {
            var obj = new Matrix<decimal>();
            Matrix<decimal> comparand = null;

            // ReSharper disable ConditionIsAlwaysTrueOrFalse
            Assert.False(obj == comparand);

            // ReSharper restore ConditionIsAlwaysTrueOrFalse
        }

        [Fact]
        public void opEquality_MatrixOfT_MatrixOfTSame()
        {
            var obj = new Matrix<decimal>();
            var comparand = obj;

            Assert.True(obj == comparand);
        }

        [Fact]
        public void opEquality_MatrixOfT_MatrixOfT_whenFalse()
        {
            var obj = new Matrix<decimal>(1, 1);
            obj[0, 0] = decimal.One;

            var comparand = new Matrix<decimal>(1, 1);
            comparand[0, 0] = decimal.MinusOne;

            Assert.False(obj == comparand);
        }

        [Fact]
        public void opEquality_MatrixOfT_MatrixOfT_whenTrue()
        {
            var obj = new Matrix<decimal>(1, 1);
            obj[0, 0] = decimal.One;

            var comparand = new Matrix<decimal>(1, 1);
            comparand[0, 0] = decimal.One;

            Assert.True(obj == comparand);
        }

        [Theory]
        [InlineData(0, 0)]
        [InlineData(2, 5)]
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "x", Justification = "This name is correct.")]
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "y", Justification = "This name is correct.")]
        public void opIndexer_Point(int x,
                                    int y)
        {
            const string expected = "example";
            var matrix = new Matrix<string>(10, 10);

            matrix[new Point(x, y)] = expected;

            var actual = matrix[new Point(x, y)];

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(-1, 0)]
        [InlineData(0, -1)]
        [InlineData(1, 0)]
        [InlineData(0, 1)]
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "x", Justification = "This name is correct.")]
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "y", Justification = "This name is correct.")]
        public void opIndexer_Point_get_whenArgumentOutOfRangeException(int x,
                                                                        int y)
        {
            var matrix = new Matrix<string>(1, 1);

            Assert.Throws<ArgumentOutOfRangeException>(() => matrix[new Point(x, y)]);
        }

        [Fact]
        public void opIndexer_Point_get_whenDefaultClass()
        {
            var matrix = new Matrix<string>(1, 1);

            Assert.Null(matrix[new Point(0, 0)]);
        }

        [Fact]
        public void opIndexer_Point_get_whenDefaultStruct()
        {
            var matrix = new Matrix<decimal>(1, 1);

            Assert.Equal(decimal.Zero, matrix[new Point(0, 0)]);
        }

        [Theory]
        [InlineData(-1, 0)]
        [InlineData(0, -1)]
        [InlineData(1, 0)]
        [InlineData(0, 1)]
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "x", Justification = "This name is correct.")]
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "y", Justification = "This name is correct.")]
        public void opIndexer_Point_set_whenArgumentOutOfRangeException(int x,
                                                                        int y)
        {
            var matrix = new Matrix<string>(1, 1);

            Assert.Throws<ArgumentOutOfRangeException>(() => matrix[new Point(x, y)] = "example");
        }

        [Fact]
        public void opIndexer_Point_set_whenReplace()
        {
            const string expected = "replacement";

            var matrix = new Matrix<string>(1, 1);
            matrix[new Point(0, 0)] = "example";
            matrix[new Point(0, 0)] = expected;

            var actual = matrix[new Point(0, 0)];

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(0, 0)]
        [InlineData(2, 5)]
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "x", Justification = "This name is correct.")]
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "y", Justification = "This name is correct.")]
        public void opIndexer_int_int(int x,
                                      int y)
        {
            const string expected = "example";
            var matrix = new Matrix<string>(10, 10);

            matrix[x, y] = expected;

            var actual = matrix[x, y];

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(-1, 0)]
        [InlineData(0, -1)]
        [InlineData(1, 0)]
        [InlineData(0, 1)]
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "x", Justification = "This name is correct.")]
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "y", Justification = "This name is correct.")]
        public void opIndexer_int_int_get_whenArgumentOutOfRangeException(int x,
                                                                          int y)
        {
            var matrix = new Matrix<string>(1, 1);

            Assert.Throws<ArgumentOutOfRangeException>(() => matrix[x, y]);
        }

        [Fact]
        public void opIndexer_int_int_get_whenDefaultClass()
        {
            var matrix = new Matrix<string>(1, 1);

            Assert.Null(matrix[0, 0]);
        }

        [Fact]
        public void opIndexer_int_int_get_whenDefaultStruct()
        {
            var matrix = new Matrix<decimal>(1, 1);

            Assert.Equal(decimal.Zero, matrix[0, 0]);
        }

        [Theory]
        [InlineData(-1, 0)]
        [InlineData(0, -1)]
        [InlineData(1, 0)]
        [InlineData(0, 1)]
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "x", Justification = "This name is correct.")]
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "y", Justification = "This name is correct.")]
        public void opIndexer_int_int_set_whenArgumentOutOfRangeException(int x,
                                                                          int y)
        {
            var matrix = new Matrix<string>(1, 1);

            Assert.Throws<ArgumentOutOfRangeException>(() => matrix[x, y] = "example");
        }

        [Fact]
        public void opIndexer_int_int_set_whenReplace()
        {
            const string expected = "replacement";

            var matrix = new Matrix<string>(1, 1);
            matrix[0, 0] = "example";
            matrix[0, 0] = expected;

            var actual = matrix[0, 0];

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void opInequality_MatrixOfTNull_MatrixOfT()
        {
            Matrix<decimal> obj = null;
            var comparand = new Matrix<decimal>();

            // ReSharper disable ConditionIsAlwaysTrueOrFalse
            Assert.True(obj != comparand);

            // ReSharper restore ConditionIsAlwaysTrueOrFalse
        }

        [Fact]
        public void opInequality_MatrixOfT_MatrixOfTNull()
        {
            var obj = new Matrix<decimal>();
            Matrix<decimal> comparand = null;

            // ReSharper disable ConditionIsAlwaysTrueOrFalse
            Assert.True(obj != comparand);

            // ReSharper restore ConditionIsAlwaysTrueOrFalse
        }

        [Fact]
        public void opInequality_MatrixOfT_MatrixOfTSame()
        {
            var obj = new Matrix<decimal>();
            var comparand = obj;

            Assert.False(obj != comparand);
        }

        [Fact]
        public void opInequality_MatrixOfT_MatrixOfT_whenFalse()
        {
            var obj = new Matrix<decimal>(1, 1);
            obj[0, 0] = decimal.One;

            var comparand = new Matrix<decimal>(1, 1);
            comparand[0, 0] = decimal.One;

            Assert.False(obj != comparand);
        }

        [Fact]
        public void opInequality_MatrixOfT_MatrixOfT_whenTrue()
        {
            var obj = new Matrix<decimal>(1, 1);
            obj[0, 0] = decimal.One;

            var comparand = new Matrix<decimal>(1, 1);
            comparand[0, 0] = decimal.MinusOne;

            Assert.True(obj != comparand);
        }

        [Fact]
        public void op_Clear()
        {
            var matrix = new Matrix<string>(1, 1);
            matrix[0, 0] = "example";
            matrix.Clear();

            Assert.Null(matrix[0, 0]);
        }

        [Fact]
        public void op_Column_int()
        {
            var matrix = new Matrix<string>(3, 3);
            matrix[0, 0] = "0,0";
            matrix[0, 1] = "0,1";
            matrix[0, 2] = "0,2";
            matrix[1, 0] = "1,0";
            matrix[1, 1] = "1,1";
            matrix[1, 2] = "1,2";
            matrix[2, 0] = "2,0";
            matrix[2, 1] = "2,1";
            matrix[2, 2] = "2,2";

            for (var x = 0; x < 3; x++)
            {
                var y = 0;
                foreach (var actual in matrix.Column(x))
                {
                    var expected = "{0},{1}".FormatWith(x, y);

                    Assert.Equal(expected, actual);
                    y++;
                }
            }
        }

        [Fact]
        public void op_Column_intNegative()
        {
            var matrix = new Matrix<string>();

            Assert.Throws<ArgumentOutOfRangeException>(() => matrix.Column(-1).ToList());
        }

        [Fact]
        public void op_Column_int_whenEmpty()
        {
            var matrix = new Matrix<string>();

            Assert.Throws<ArgumentOutOfRangeException>(() => matrix.Column(0).ToList());
        }

        [Fact]
        public void op_Equals_MatrixOfTNull()
        {
            Matrix<decimal> comparand = null;

            // ReSharper disable ExpressionIsAlwaysNull
            Assert.False(new Matrix<decimal>().Equals(comparand));

            // ReSharper restore ExpressionIsAlwaysNull
        }

        [Fact]
        public void op_Equals_MatrixOfTSame()
        {
            var matrix = new Matrix<decimal>();

            // ReSharper disable EqualExpressionComparison
            Assert.True(matrix.Equals(matrix));

            // ReSharper restore EqualExpressionComparison
        }

        [Fact]
        public void op_Equals_MatrixOfT_whenFalse()
        {
            var matrix = new Matrix<decimal>(1, 1);
            matrix[0, 0] = decimal.One;

            var comparand = new Matrix<decimal>(1, 1);
            comparand[0, 0] = decimal.MinusOne;

            Assert.False(matrix.Equals(comparand));
        }

        [Fact]
        public void op_Equals_MatrixOfT_whenTrue()
        {
            var matrix = new Matrix<decimal>(1, 1);
            matrix[0, 0] = decimal.One;

            var comparand = new Matrix<decimal>(1, 1);
            comparand[0, 0] = decimal.One;

            Assert.True(matrix.Equals(comparand));
        }

        [Fact]
        public void op_Equals_objectNull()
        {
            Assert.False(new Matrix<decimal>().Equals(null as object));
        }

        [Fact]
        public void op_Equals_objectSame()
        {
            var matrix = new Matrix<decimal>();

            // ReSharper disable EqualExpressionComparison
            Assert.True(matrix.Equals(matrix as object));

            // ReSharper restore EqualExpressionComparison
        }

        [Fact]
        public void op_Equals_object_whenDifferentType()
        {
            var matrix = new Matrix<decimal>();

            var comparand = new Matrix<int>();

            Assert.False(matrix.Equals(comparand));
        }

        [Fact]
        public void op_Equals_object_whenFalse()
        {
            var matrix = new Matrix<decimal>(1, 1);
            matrix[0, 0] = decimal.One;

            var comparand = new Matrix<decimal>(1, 1);
            comparand[0, 0] = decimal.MinusOne;

            Assert.False(matrix.Equals(comparand as object));
        }

        [Fact]
        public void op_Equals_object_whenTrue()
        {
            var matrix = new Matrix<decimal>(1, 1);
            matrix[0, 0] = decimal.One;

            var comparand = new Matrix<decimal>(1, 1);
            comparand[0, 0] = decimal.One;

            Assert.True(matrix.Equals(comparand as object));
        }

        [Fact]
        public void op_GetEnumerator()
        {
            var matrix = new Matrix<string>(3, 3);
            matrix[0, 0] = "0,0";
            matrix[0, 1] = "0,1";
            matrix[0, 2] = "0,2";
            matrix[1, 0] = "1,0";
            matrix[1, 1] = "1,1";
            matrix[1, 2] = "1,2";
            matrix[2, 0] = "2,0";
            matrix[2, 1] = "2,1";
            matrix[2, 2] = "2,2";

            var queue = new Queue<string>();
            queue.Enqueue("0,0");
            queue.Enqueue("1,0");
            queue.Enqueue("2,0");
            queue.Enqueue("0,1");
            queue.Enqueue("1,1");
            queue.Enqueue("2,1");
            queue.Enqueue("0,2");
            queue.Enqueue("1,2");
            queue.Enqueue("2,2");

            foreach (var actual in matrix)
            {
                var expected = queue.Dequeue();

                Assert.Equal(expected, actual);
            }
        }

        [Fact]
        public void op_GetHashCode()
        {
            var expected = "-1\r\n".GetHashCode();

            var actual = new Matrix<decimal>(1, 1);
            actual[0, 0] = decimal.MinusOne;

            Assert.Equal(expected, actual.GetHashCode());
        }

        [Fact]
        public void op_GetHashCode_whenEmpty()
        {
            var expected = string.Empty.GetHashCode();

            var actual = new Matrix<decimal>();

            Assert.Equal(expected, actual.GetHashCode());
        }

        [Fact]
        public void op_Resize_Size()
        {
            const string expected = "example";

            var matrix = new Matrix<string>(1, 1);

            Assert.Throws<ArgumentOutOfRangeException>(() => matrix[2, 2]);

            matrix.Resize(new Size(3, 3));

            matrix[2, 2] = expected;

            var actual = matrix[2, 2];

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(-1, 0)]
        [InlineData(0, -1)]
        public void op_Resize_Size_whenArgumentOutOfRangeException(int width,
                                                                   int height)
        {
            var matrix = new Matrix<string>(1, 1);

            Assert.Throws<ArgumentOutOfRangeException>(() => matrix.Resize(new Size(width, height)));
        }

        [Theory]
        [InlineData(2, 2)]
        [InlineData(2, 3)]
        [InlineData(3, 2)]
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "x", Justification = "This name is correct.")]
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "y", Justification = "This name is correct.")]
        public void op_Resize_Size_whenShrink(int x,
                                              int y)
        {
            var matrix = new Matrix<string>(3, 3);

            matrix[2, 2] = "example";

            matrix.Resize(new Size(2, 2));

            matrix.Resize(new Size(3, 3));

            Assert.Null(matrix[2, 2]);
        }

        [Fact]
        public void op_Resize_int_int()
        {
            const string expected = "example";

            var matrix = new Matrix<string>(1, 1);

            Assert.Throws<ArgumentOutOfRangeException>(() => matrix[2, 2]);

            matrix.Resize(3, 3);

            matrix[2, 2] = expected;

            var actual = matrix[2, 2];

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(-1, 0)]
        [InlineData(0, -1)]
        public void op_Resize_int_int_whenArgumentOutOfRangeException(int width,
                                                                      int height)
        {
            var matrix = new Matrix<string>(1, 1);

            Assert.Throws<ArgumentOutOfRangeException>(() => matrix.Resize(width, height));
        }

        [Fact]
        public void op_Row_int()
        {
            var matrix = new Matrix<string>(3, 3);
            matrix[0, 0] = "0,0";
            matrix[0, 1] = "0,1";
            matrix[0, 2] = "0,2";
            matrix[1, 0] = "1,0";
            matrix[1, 1] = "1,1";
            matrix[1, 2] = "1,2";
            matrix[2, 0] = "2,0";
            matrix[2, 1] = "2,1";
            matrix[2, 2] = "2,2";

            for (var y = 0; y < 3; y++)
            {
                var x = 0;
                foreach (var actual in matrix.Row(y))
                {
                    var expected = "{0},{1}".FormatWith(x, y);

                    Assert.Equal(expected, actual);
                    x++;
                }
            }
        }

        [Fact]
        public void op_Row_intNegative()
        {
            var matrix = new Matrix<string>();

            Assert.Throws<ArgumentOutOfRangeException>(() => matrix.Row(-1).ToList());
        }

        [Fact]
        public void op_Row_int_whenEmpty()
        {
            var matrix = new Matrix<string>();

            Assert.Throws<ArgumentOutOfRangeException>(() => matrix.Row(0).ToList());
        }

        [Fact]
        public void op_ToString()
        {
            var expected = new StringBuilder();
            expected.AppendLine("-1\t-1.1\t-1.2");
            expected.AppendLine("0 \t0.1 \t0.2 ");
            expected.AppendLine("1 \t1.1 \t1.2 ");

            var actual = new Matrix<decimal>(3, 3);
            actual[0, 0] = decimal.MinusOne;
            actual[0, 1] = decimal.Zero;
            actual[0, 2] = decimal.One;
            actual[1, 0] = -1.1m;
            actual[1, 1] = 0.1m;
            actual[1, 2] = 1.1m;
            actual[2, 0] = -1.2m;
            actual[2, 1] = 0.2m;
            actual[2, 2] = 1.2m;

            Assert.Equal(expected.ToString(), actual.ToString());
        }

        [Fact]
        public void op_ToString_whenEmpty()
        {
            var expected = string.Empty;

            var actual = new Matrix<decimal>();

            Assert.Equal(expected, actual.ToString());
        }

        [Fact]
        public void op_ToString_whenNulls()
        {
            var expected = new StringBuilder();
            expected.AppendLine("-1\t-1.1\t   ");
            expected.AppendLine("  \t0.1 \t0.2");
            expected.AppendLine("1 \t    \t1.2");

            var actual = new Matrix<string>(3, 3);
            actual[0, 0] = "-1";
            actual[0, 2] = "1";
            actual[1, 0] = "-1.1";
            actual[1, 1] = "0.1";
            actual[2, 1] = "0.2";
            actual[2, 2] = "1.2";

            Assert.Equal(expected.ToString(), actual.ToString());
        }

        [Fact]
        public void prop_Count()
        {
            Assert.True(new PropertyExpectations<Matrix<string>>(x => x.Count)
                            .TypeIs<int>()
                            .DefaultValueIs(0)
                            .IsNotDecorated()
                            .Result);
        }

        [Theory]
        [InlineData(0, 0, 0)]
        [InlineData(0, 0, 1)]
        [InlineData(0, 1, 0)]
        [InlineData(1, 1, 1)]
        [InlineData(4, 2, 2)]
        public void prop_Count_get(int expected,
                                   int height,
                                   int width)
        {
            var actual = new Matrix<decimal>(height, width).Count;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void prop_Height()
        {
            Assert.True(new PropertyExpectations<Matrix<string>>(x => x.Height)
                            .TypeIs<int>()
                            .DefaultValueIs(0)
                            .IsNotDecorated()
                            .ArgumentOutOfRangeException(-1)
                            .Result);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(5)]
        public void prop_Height_get(int expected)
        {
            var actual = new Matrix<decimal>(1, expected).Height;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void prop_Height_set()
        {
            const string expected = "example";

            var matrix = new Matrix<string>(1, 1);

            Assert.Throws<ArgumentOutOfRangeException>(() => matrix[0, 2]);

            matrix.Height = 3;

            matrix[0, 2] = expected;

            var actual = matrix[0, 2];

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void prop_Height_setIncrement()
        {
            const string expected = "example";

            var matrix = new Matrix<string>(1, 1);
            matrix[0, 0] = expected;
            matrix.Height++;

            var actual = matrix[0, 0];

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void prop_IsEmpty()
        {
            Assert.True(new PropertyExpectations<Matrix<string>>(x => x.IsEmpty)
                            .TypeIs<bool>()
                            .DefaultValueIs(true)
                            .IsNotDecorated()
                            .Result);
        }

        [Theory]
        [InlineData(true, 0, 0)]
        [InlineData(true, 0, 1)]
        [InlineData(true, 1, 0)]
        [InlineData(false, 1, 1)]
        public void prop_IsEmpty_get(bool expected,
                                     int height,
                                     int width)
        {
            var actual = new Matrix<decimal>(height, width).IsEmpty;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void prop_IsSquare()
        {
            Assert.True(new PropertyExpectations<Matrix<string>>(x => x.IsSquare)
                            .TypeIs<bool>()
                            .DefaultValueIs(true)
                            .IsNotDecorated()
                            .Result);
        }

        [Theory]
        [InlineData(true, 0, 0)]
        [InlineData(false, 0, 1)]
        [InlineData(false, 1, 0)]
        [InlineData(true, 1, 1)]
        [InlineData(false, 1, 2)]
        [InlineData(true, 2, 2)]
        public void prop_IsSquare_get(bool expected,
                                      int height,
                                      int width)
        {
            var actual = new Matrix<decimal>(height, width).IsSquare;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void prop_Width()
        {
            Assert.True(new PropertyExpectations<Matrix<string>>(x => x.Width)
                            .TypeIs<int>()
                            .DefaultValueIs(0)
                            .IsNotDecorated()
                            .ArgumentOutOfRangeException(-1)
                            .Result);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(5)]
        public void prop_Width_get(int expected)
        {
            var actual = new Matrix<decimal>(expected, 1).Width;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void prop_Width_set()
        {
            const string expected = "example";

            var matrix = new Matrix<string>(1, 1);

            Assert.Throws<ArgumentOutOfRangeException>(() => matrix[2, 0]);

            matrix.Width = 3;

            matrix[2, 0] = expected;

            var actual = matrix[2, 0];

            Assert.Equal(expected, actual);
        }
    }
}