namespace Cavity.IO
{
    using System;
    using System.IO;
    using Xunit;

    public sealed class CsvStreamReaderFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<CsvStreamReader>()
                            .DerivesFrom<StreamReader>()
                            .IsConcreteClass()
                            .IsSealed()
                            .NoDefaultConstructor()
                            .IsNotDecorated()
                            .Result);
        }

        [Fact]
        public void ctor()
        {
            using (var stream = new MemoryStream())
            {
                Assert.NotNull(new CsvStreamReader(stream));
            }
        }

        [Fact]
        public void op_ReadEntry()
        {
            using (var stream = new MemoryStream())
            {
                using (var writer = new StreamWriter(stream))
                {
                    writer.WriteLine("A,B");
                    writer.WriteLine("1A,1B");
                    writer.Flush();
                    stream.Position = 0;
                    using (var reader = new CsvStreamReader(stream))
                    {
                        var actual = reader.ReadEntry();
                        Assert.Equal(2, reader.LineNumber);
                        Assert.Equal(1, reader.EntryNumber);
                        Assert.Equal("1A", actual["A"]);
                        Assert.Equal("1B", actual["B"]);
                    }
                }
            }
        }

        [Fact]
        public void op_ReadEntry_whenEmbeddedComma()
        {
            using (var stream = new MemoryStream())
            {
                using (var writer = new StreamWriter(stream))
                {
                    writer.WriteLine("A,B");
                    writer.WriteLine("\"1,A\",\"1,B\"");
                    writer.Flush();
                    stream.Position = 0;
                    using (var reader = new CsvStreamReader(stream))
                    {
                        var actual = reader.ReadEntry();
                        Assert.Equal(2, reader.LineNumber);
                        Assert.Equal(1, reader.EntryNumber);
                        Assert.Equal("1,A", actual["A"]);
                        Assert.Equal("1,B", actual["B"]);
                    }
                }
            }
        }

        [Fact]
        public void op_ReadEntry_whenEmbeddedLeadingAndTrailingSpaces()
        {
            using (var stream = new MemoryStream())
            {
                using (var writer = new StreamWriter(stream))
                {
                    writer.WriteLine(" A , B ");
                    writer.WriteLine("\" 1A \", 1B ");
                    writer.Flush();
                    stream.Position = 0;
                    using (var reader = new CsvStreamReader(stream))
                    {
                        var actual = reader.ReadEntry();
                        Assert.Equal(2, reader.LineNumber);
                        Assert.Equal(1, reader.EntryNumber);
                        Assert.Equal(" 1A ", actual["A"]);
                        Assert.Equal("1B", actual["B"]);
                    }
                }
            }
        }

        [Fact]
        public void op_ReadEntry_whenEmbeddedQuotation()
        {
            using (var stream = new MemoryStream())
            {
                using (var writer = new StreamWriter(stream))
                {
                    writer.WriteLine("A,B,C");
                    writer.WriteLine("\"\"\"1A\",\"1\"\"B\",\"1C\"\"\"");
                    writer.Flush();
                    stream.Position = 0;
                    using (var reader = new CsvStreamReader(stream))
                    {
                        var actual = reader.ReadEntry();
                        Assert.Equal(2, reader.LineNumber);
                        Assert.Equal(1, reader.EntryNumber);
                        Assert.Equal("\"1A", actual["A"]);
                        Assert.Equal("1\"B", actual["B"]);
                        Assert.Equal("1C\"", actual["C"]);
                    }
                }
            }
        }

        [Fact]
        public void op_ReadEntry_whenEmptyLine()
        {
            using (var stream = new MemoryStream())
            {
                using (var writer = new StreamWriter(stream))
                {
                    writer.WriteLine("A,B");
                    writer.WriteLine(string.Empty);
                    writer.WriteLine("1A,1B");
                    writer.Flush();
                    stream.Position = 0;
                    using (var reader = new CsvStreamReader(stream))
                    {
                        var actual = reader.ReadEntry();
                        Assert.Equal(3, reader.LineNumber);
                        Assert.Equal(1, reader.EntryNumber);
                        Assert.Equal("1A", actual["A"]);
                        Assert.Equal("1B", actual["B"]);
                    }
                }
            }
        }

        [Fact]
        public void op_ReadEntry_whenLeadingAndTrailingSpaces()
        {
            using (var stream = new MemoryStream())
            {
                using (var writer = new StreamWriter(stream))
                {
                    writer.WriteLine(" A , B ");
                    writer.WriteLine(" 1A , 1B ");
                    writer.Flush();
                    stream.Position = 0;
                    using (var reader = new CsvStreamReader(stream))
                    {
                        var actual = reader.ReadEntry();
                        Assert.Equal(2, reader.LineNumber);
                        Assert.Equal(1, reader.EntryNumber);
                        Assert.Equal("1A", actual["A"]);
                        Assert.Equal("1B", actual["B"]);
                    }
                }
            }
        }

        [Fact]
        public void op_ReadEntry_whenLineBreak()
        {
            using (var stream = new MemoryStream())
            {
                using (var writer = new StreamWriter(stream))
                {
                    writer.WriteLine("A,B,C");
                    writer.WriteLine("1A,\"Line1");
                    writer.WriteLine("Line2\nLine3\",1C");
                    writer.Flush();
                    stream.Position = 0;
                    using (var reader = new CsvStreamReader(stream))
                    {
                        var actual = reader.ReadEntry();
                        Assert.Equal(4, reader.LineNumber);
                        Assert.Equal(1, reader.EntryNumber);
                        Assert.Equal("1A", actual["A"]);
                        Assert.Equal("Line1" + Environment.NewLine + "Line2" + Environment.NewLine + "Line3", actual["B"]);
                        Assert.Equal("1C", actual["C"]);
                    }
                }
            }
        }

        [Fact]
        public void op_ReadEntry_whenMissingColumnItem()
        {
            using (var stream = new MemoryStream())
            {
                using (var writer = new StreamWriter(stream))
                {
                    writer.WriteLine("A,B,C");
                    writer.WriteLine("1,2");
                    writer.Flush();
                    stream.Position = 0;
                    using (var reader = new CsvStreamReader(stream))
                    {
                        Assert.Throws<FormatException>(() => reader.ReadEntry());
                    }
                }
            }
        }

        [Fact]
        public void op_ReadEntry_whenQuotation()
        {
            using (var stream = new MemoryStream())
            {
                using (var writer = new StreamWriter(stream))
                {
                    writer.WriteLine("A,B");
                    writer.WriteLine("\"1A\",\"1B\"");
                    writer.Flush();
                    stream.Position = 0;
                    using (var reader = new CsvStreamReader(stream))
                    {
                        var actual = reader.ReadEntry();
                        Assert.Equal(2, reader.LineNumber);
                        Assert.Equal(1, reader.EntryNumber);
                        Assert.Equal("1A", actual["A"]);
                        Assert.Equal("1B", actual["B"]);
                    }
                }
            }
        }

        [Fact]
        public void op_ReadEntry_whenTrailingNewLine()
        {
            using (var stream = new MemoryStream())
            {
                using (var writer = new StreamWriter(stream))
                {
                    writer.WriteLine("A,B");
                    writer.WriteLine("1A,1B");
                    writer.WriteLine(string.Empty);
                    writer.Flush();
                    stream.Position = 0;
                    using (var reader = new CsvStreamReader(stream))
                    {
                        reader.ReadEntry();
                        var actual = reader.ReadEntry();
                        Assert.Equal(3, reader.LineNumber);
                        Assert.Equal(1, reader.EntryNumber);
                        Assert.Empty(actual);
                    }
                }
            }
        }

        [Fact]
        public void op_ReadEntry_whenUnixLF()
        {
            using (var stream = new MemoryStream())
            {
                using (var writer = new StreamWriter(stream))
                {
                    writer.WriteLine("A,B\n1A,1B");
                    writer.Flush();
                    stream.Position = 0;
                    using (var reader = new CsvStreamReader(stream))
                    {
                        var actual = reader.ReadEntry();
                        Assert.Equal(2, reader.LineNumber);
                        Assert.Equal(1, reader.EntryNumber);
                        Assert.Equal("1A", actual["A"]);
                        Assert.Equal("1B", actual["B"]);
                    }
                }
            }
        }

        [Fact]
        public void prop_EntryNumber()
        {
            Assert.NotNull(new PropertyExpectations<CsvStreamReader>("EntryNumber")
                               .TypeIs<int>()
                               .IsNotDecorated()
                               .Result);
        }

        [Fact]
        public void prop_Header()
        {
            Assert.NotNull(new PropertyExpectations<CsvStreamReader>("Header")
                               .TypeIs<string>()
                               .IsNotDecorated()
                               .Result);
        }

        [Fact]
        public void prop_Line()
        {
            Assert.NotNull(new PropertyExpectations<CsvStreamReader>("Line")
                               .TypeIs<string>()
                               .IsNotDecorated()
                               .Result);
        }

        [Fact]
        public void prop_LineNumber()
        {
            Assert.NotNull(new PropertyExpectations<CsvStreamReader>("LineNumber")
                               .TypeIs<int>()
                               .IsNotDecorated()
                               .Result);
        }
    }
}