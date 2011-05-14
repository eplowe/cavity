////namespace Cavity.Transactions
////{
////    using System;
////    using System.Transactions;

////    public sealed class DerivedDurableEnlistmentNotification : DurableEnlistmentNotification
////    {
////        public DerivedDurableEnlistmentNotification(Guid identifier,
////                                                    EnlistmentOptions enlistmentOptions)
////            : base(identifier, enlistmentOptions)
////        {
////        }

////        protected override bool ConfigureOperation()
////        {
////            return true;
////        }
////    }
////}