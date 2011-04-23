namespace Cavity.Diagnostics
{
    using System;
    using System.Diagnostics;
    using System.ServiceProcess;
    using log4net;

    public sealed class TaskManagementEventLog : EventLog
    {
        public TaskManagementEventLog()
            : base("Cavity", ".", "Task Management")
        {
            Trace = LogManager.GetLogger(GetType());
        }

        private ILog Trace { get; set; }

        public void FailureOnContinue(Exception exception)
        {
            if (Trace.IsErrorEnabled)
            {
                Trace.Error(new StackFrame().GetMethod().Name, exception);
            }

            WriteEvent(new EventInstance(0xC00003F7, 2, EventLogEntryType.Error));
        }

        public void FailureOnCustomCommand(Exception exception,
                                           int command)
        {
            if (Trace.IsErrorEnabled)
            {
                Trace.Error(new StackFrame().GetMethod().Name, exception);
            }

            WriteEvent(new EventInstance(0xC00003F8, 2, EventLogEntryType.Error), command);
        }

        public void FailureOnPause(Exception exception)
        {
            if (Trace.IsErrorEnabled)
            {
                Trace.Error(new StackFrame().GetMethod().Name, exception);
            }

            WriteEvent(new EventInstance(0xC00003F6, 2, EventLogEntryType.Error));
        }

        public void FailureOnPowerEvent(Exception exception,
                                        PowerBroadcastStatus powerStatus)
        {
            if (Trace.IsErrorEnabled)
            {
                Trace.Error(new StackFrame().GetMethod().Name, exception);
            }

            WriteEvent(new EventInstance(0xC00003F9, 2, EventLogEntryType.Error), powerStatus.ToString("G"));
        }

        public void FailureOnSessionChange(Exception exception,
                                           SessionChangeDescription changeDescription)
        {
            if (Trace.IsErrorEnabled)
            {
                Trace.Error(new StackFrame().GetMethod().Name, exception);
            }

            WriteEvent(new EventInstance(0xC00003FA, 2, EventLogEntryType.Error), changeDescription.Reason.ToString("G"));
        }

        public void FailureOnShutdown(Exception exception)
        {
            if (Trace.IsErrorEnabled)
            {
                Trace.Error(new StackFrame().GetMethod().Name, exception);
            }

            WriteEvent(new EventInstance(0xC00003F5, 2, EventLogEntryType.Error));
        }

        public void FailureOnStart(Exception exception)
        {
            if (Trace.IsErrorEnabled)
            {
                Trace.Error(new StackFrame().GetMethod().Name, exception);
            }

            WriteEvent(new EventInstance(0xC00003F3, 2, EventLogEntryType.Error));
        }

        public void FailureOnStop(Exception exception)
        {
            if (Trace.IsErrorEnabled)
            {
                Trace.Error(new StackFrame().GetMethod().Name, exception);
            }

            WriteEvent(new EventInstance(0xC00003F4, 2, EventLogEntryType.Error));
        }

        public void SuccessOnContinue()
        {
            WriteEvent(new EventInstance(0x000003ED, 2));
        }

        public void SuccessOnCustomCommand(int command)
        {
            WriteEvent(new EventInstance(0x000003EE, 2), command);
        }

        public void SuccessOnPause()
        {
            WriteEvent(new EventInstance(0x000003EC, 2));
        }

        public void SuccessOnPowerEvent(PowerBroadcastStatus powerStatus)
        {
            WriteEvent(new EventInstance(0x000003EF, 2), powerStatus.ToString("G"));
        }

        public void SuccessOnSessionChange(SessionChangeDescription changeDescription)
        {
            WriteEvent(new EventInstance(0x000003F0, 2), changeDescription.Reason.ToString("G"));
        }

        public void SuccessOnShutdown()
        {
            WriteEvent(new EventInstance(0x000003EB, 2));
        }

        public void SuccessOnStart()
        {
            WriteEvent(new EventInstance(0x000003E9, 2));
        }

        public void SuccessOnStop()
        {
            WriteEvent(new EventInstance(0x000003EA, 2));
        }
    }
}