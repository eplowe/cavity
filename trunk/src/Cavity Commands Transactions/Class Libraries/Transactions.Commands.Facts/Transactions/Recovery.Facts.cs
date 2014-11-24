namespace Cavity.Transactions
{
    using System;
    using System.Linq;
    using Cavity.IO;
    using Xunit;

    public sealed class RecoveryFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(typeof(Recovery).IsStatic());
        }

        [Fact]
        public void op_Exclude_OperationNull_bool()
        {
            Assert.Throws<ArgumentNullException>(() => Recovery.Exclude(null, false));
        }

        [Fact]
        public void op_Exclude_Operation_bool()
        {
            try
            {
                using (var temp = new TempDirectory())
                {
                    Recovery.MasterDirectory = temp.Info.ToDirectory("Recovery");
                    var operation = new Operation(Guid.NewGuid())
                                        {
                                            Info = Guid.NewGuid().ToString()
                                        };

                    Recovery.Include(operation);
                    Recovery.Exclude(operation, false);

                    var expected = Recovery.ItemFile(operation, "Rollback").FullName;
                    foreach (var actual in Recovery.MasterFile(operation).Lines())
                    {
                        Assert.NotEqual(expected, actual);
                    }
                }
            }
            finally
            {
                Recovery.MasterDirectory = null;
            }
        }

        [Fact]
        public void op_Exclude_Operations_bool()
        {
            try
            {
                using (var temp = new TempDirectory())
                {
                    var resourceManager = Guid.NewGuid();
                    Recovery.MasterDirectory = temp.Info.ToDirectory("Recovery");
                    var operation1 = new Operation(resourceManager)
                                         {
                                             Info = Guid.NewGuid().ToString()
                                         };

                    Recovery.Include(operation1);

                    var operation2 = new Operation(resourceManager)
                                         {
                                             Info = Guid.NewGuid().ToString()
                                         };

                    Recovery.Include(operation2);
                    Recovery.Exclude(operation1, true);
                    var expected = Recovery.ItemFile(operation1, "Commit").FullName;
                    foreach (var actual in Recovery.MasterFile(operation1).Lines())
                    {
                        Assert.NotEqual(expected, actual);
                    }
                }
            }
            finally
            {
                Recovery.MasterDirectory = null;
            }
        }

        [Fact]
        public void op_Include_Operation()
        {
            try
            {
                using (var temp = new TempDirectory())
                {
                    Recovery.MasterDirectory = temp.Info.ToDirectory("Recovery");
                    var operation = new Operation(Guid.NewGuid())
                                        {
                                            Info = Guid.NewGuid().ToString()
                                        };

                    Recovery.Include(operation);

                    var expected = Recovery.ItemFile(operation).FullName;
                    foreach (var actual in Recovery.MasterFile(operation).Lines())
                    {
                        Assert.Equal(expected, actual);
                    }
                }
            }
            finally
            {
                Recovery.MasterDirectory = null;
            }
        }

        [Fact]
        public void op_Include_OperationNull()
        {
            Assert.Throws<ArgumentNullException>(() => Recovery.Include(null));
        }

        [Fact]
        public void op_Include_Operations()
        {
            try
            {
                using (var temp = new TempDirectory())
                {
                    var resourceManager = Guid.NewGuid();
                    Recovery.MasterDirectory = temp.Info.ToDirectory("Recovery");
                    var operation = new Operation(resourceManager)
                                        {
                                            Info = Guid.NewGuid().ToString()
                                        };

                    Recovery.Include(operation);
                    var first = Recovery.ItemFile(operation).FullName;
                    Assert.Equal(first, Recovery.MasterFile(operation).Lines().First());

                    operation = new Operation(resourceManager)
                                    {
                                        Info = Guid.NewGuid().ToString()
                                    };

                    Recovery.Include(operation);
                    var last = Recovery.ItemFile(operation).FullName;
                    Assert.Equal(first, Recovery.MasterFile(operation).Lines().First());
                    Assert.Equal(last, Recovery.MasterFile(operation).Lines().Last());
                }
            }
            finally
            {
                Recovery.MasterDirectory = null;
            }
        }

        [Fact]
        public void op_ItemFile_Operation()
        {
            var operation = new Operation(Guid.NewGuid())
                                {
                                    Info = Guid.NewGuid().ToString()
                                };

            var id = operation.Identity;

            var expected = Recovery.MasterDirectory.ToDirectory(id.ResourceManager).ToFile("{0}.xml".FormatWith(id.Instance)).FullName;
            var actual = Recovery.ItemFile(operation).FullName;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_ItemFile_OperationNull()
        {
            Assert.Throws<ArgumentNullException>(() => Recovery.ItemFile(null));
        }

        [Fact]
        public void op_ItemFile_OperationNull_string()
        {
            Assert.Throws<ArgumentNullException>(() => Recovery.ItemFile(null, "Example"));
        }

        [Fact]
        public void op_ItemFile_Operation_string()
        {
            var operation = new Operation(Guid.NewGuid())
                                {
                                    Info = Guid.NewGuid().ToString()
                                };

            var id = operation.Identity;

            var expected = Recovery.MasterDirectory.ToDirectory(id.ResourceManager).ToDirectory("Example").ToFile("{0}.xml".FormatWith(id.Instance)).FullName;
            var actual = Recovery.ItemFile(operation, "Example").FullName;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_ItemFile_Operation_stringNull()
        {
            var operation = new Operation(Guid.NewGuid())
                                {
                                    Info = Guid.NewGuid().ToString()
                                };

            var id = operation.Identity;

            var expected = Recovery.MasterDirectory.ToDirectory(id.ResourceManager).ToFile("{0}.xml".FormatWith(id.Instance)).FullName;
            var actual = Recovery.ItemFile(operation, null).FullName;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_MasterFile_Operation()
        {
            var operation = new Operation(Guid.NewGuid())
                                {
                                    Info = Guid.NewGuid().ToString()
                                };

            var id = operation.Identity;

            var expected = Recovery.MasterDirectory.ToFile("{0}.master".FormatWith(id.ResourceManager)).FullName;
            var actual = Recovery.MasterFile(operation).FullName;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_MasterFile_OperationNull()
        {
            Assert.Throws<ArgumentNullException>(() => Recovery.MasterFile(null));
        }
    }
}