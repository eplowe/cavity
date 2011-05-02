namespace Cavity.Transactions
{
    using System;
    using System.Diagnostics;
    using System.Diagnostics.CodeAnalysis;
    using System.Transactions;
    using Cavity.Diagnostics;

    public abstract class DurableEnlistmentNotification : IEnlistmentNotification
    {
        protected DurableEnlistmentNotification(Guid resourceManager,
                                                EnlistmentOptions enlistmentOptions)
        {
            Trace.WriteIf(Tracing.Enabled, string.Empty);
            Operation = new Operation(resourceManager);
            if (null == Transaction.Current)
            {
                throw new InvalidOperationException("There is no transaction scope to enlist with.");
            }

            Transaction.Current.EnlistDurable(resourceManager, this, enlistmentOptions);
            Transaction.Current.TransactionCompleted += OnTransactionCompleted;
        }

        public Operation Operation { get; private set; }

        public virtual void OnTransactionCompleted(object sender,
                                                   TransactionEventArgs e)
        {
            Trace.WriteIf(Tracing.Enabled, string.Empty);
        }

        public virtual void Commit(Enlistment enlistment)
        {
            Trace.WriteIf(Tracing.Enabled, string.Empty);
            if (null != enlistment)
            {
                Operation.Done(true);
                enlistment.Done();
            }
        }

        public virtual void InDoubt(Enlistment enlistment)
        {
            Trace.WriteIf(Tracing.Enabled, string.Empty);
            if (null != enlistment)
            {
                enlistment.Done();
            }
        }

        [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Justification = "Don't let exceptions leak out of the Prepare method.")]
        public virtual void Prepare(PreparingEnlistment preparingEnlistment)
        {
            Trace.WriteIf(Tracing.Enabled, string.Empty);
            if (null != preparingEnlistment)
            {
                try
                {
                    Operation.Info = Convert.ToBase64String(preparingEnlistment.RecoveryInformation());
                    if (ConfigureOperation() &&
                        Operation.Do())
                    {
                        preparingEnlistment.Prepared();
                        return;
                    }

                    preparingEnlistment.ForceRollback();
                }
                catch (Exception exception)
                {
                    Trace.TraceError("{0}", exception);
                    preparingEnlistment.ForceRollback(exception);
                }
            }
        }

        public virtual void Rollback(Enlistment enlistment)
        {
            Trace.WriteIf(Tracing.Enabled, string.Empty);
            if (null != enlistment)
            {
                if (Operation.Undo())
                {
                    Operation.Done(false);
                }

                enlistment.Done();
            }
        }

        protected abstract bool ConfigureOperation();
    }
}