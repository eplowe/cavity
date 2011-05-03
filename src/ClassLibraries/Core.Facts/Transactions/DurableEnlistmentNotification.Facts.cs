namespace Cavity.Transactions
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Threading;
    using System.Transactions;
    using Cavity.Commands;
    using Cavity.IO;
    using Xunit;

    public sealed class DurableEnlistmentNotificationFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<DurableEnlistmentNotification>()
                            .DerivesFrom<object>()
                            .IsAbstractBaseClass()
                            .IsNotDecorated()
                            .Result);
        }

#if DEBUG
        [Fact]
        public void ctor_Guid_EnlistmentOptions()
        {
            using (new TransactionScope())
            {
                Assert.NotNull(new DerivedDurableEnlistmentNotification(Guid.NewGuid(), EnlistmentOptions.None));
            }
        }
#endif

        [Fact]
        public void prop_Operation()
        {
            Assert.NotNull(new PropertyExpectations<DurableEnlistmentNotification>(p => p.Operation)
                               .TypeIs<Operation>()
                               .IsNotDecorated()
                               .Result);
        }

#if DEBUG
        [Fact]
        [Comment("Transaction: Prepare, Completed, Commit")]
        public void transaction_Complete()
        {
            try
            {
                using (var temp = new TempDirectory())
                {
                    var path = temp.Info.ToDirectory("example").FullName;
                    Recovery.MasterDirectory = temp.Info.ToDirectory("Recovery");
                    using (var scope = new TransactionScope())
                    {
                        var obj = new DerivedDurableEnlistmentNotification(Guid.NewGuid(), EnlistmentOptions.None);
                        obj.Operation.Commands.Add(new DirectoryCreateCommand(path));

                        scope.Complete();
                    }

                    Assert.True(new DirectoryInfo(path).Exists);
                    Thread.Sleep(1000);
                }
            }
            finally
            {
                Recovery.MasterDirectory = null;
            }
        }
#endif

#if DEBUG
        [Fact]
        [Comment("Transaction: Completed, Rollback")]
        public void transaction_withoutComplete()
        {
            try
            {
                using (var temp = new TempDirectory())
                {
                    var path = temp.Info.ToDirectory("example").FullName;
                    Recovery.MasterDirectory = temp.Info.ToDirectory("Recovery");
                    using (new TransactionScope())
                    {
                        var obj = new DerivedDurableEnlistmentNotification(Guid.NewGuid(), EnlistmentOptions.None);
                        obj.Operation.Commands.Add(new DirectoryCreateCommand(path));
                    }

                    Assert.False(new DirectoryInfo(path).Exists);
                }
            }
            finally
            {
                Recovery.MasterDirectory = null;
            }
        }
#endif

        [Fact]
        public void transaction_Missing()
        {
            Assert.Throws<InvalidOperationException>(() => new DerivedDurableEnlistmentNotification(Guid.NewGuid(), EnlistmentOptions.None));
        }
    }
}