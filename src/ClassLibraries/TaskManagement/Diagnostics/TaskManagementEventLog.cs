namespace Cavity.Diagnostics
{
    using System;
    using System.Diagnostics;
    using System.Reflection;
    using System.ServiceProcess;
    using log4net;

    public sealed class TaskManagementEventLog : EventLog
    {
        [ThreadStatic]
        private static readonly ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public TaskManagementEventLog()
            : base("Cavity", ".", "Task Management")
        {
        }

        public void FailureOnContinue(Exception exception)
        {
            WriteEvent(new EventInstance(0xC00003F7, 2, EventLogEntryType.Error));
            LogException(new StackFrame(), exception);
        }

        public void FailureOnCustomCommand(Exception exception,
                                           int command)
        {
            WriteEvent(new EventInstance(0xC00003F8, 2, EventLogEntryType.Error), command);
            LogException(new StackFrame(), exception);
        }

        public void FailureOnPause(Exception exception)
        {
            WriteEvent(new EventInstance(0xC00003F6, 2, EventLogEntryType.Error));
            LogException(new StackFrame(), exception);
        }

        public void FailureOnPowerEvent(Exception exception,
                                        PowerBroadcastStatus powerStatus)
        {
            WriteEvent(new EventInstance(0xC00003F9, 2, EventLogEntryType.Error), powerStatus.ToString("G"));
            LogException(new StackFrame(), exception);
        }

        public void FailureOnSessionChange(Exception exception,
                                           SessionChangeDescription changeDescription)
        {
            WriteEvent(new EventInstance(0xC00003FA, 2, EventLogEntryType.Error), changeDescription.Reason.ToString("G"));
            LogException(new StackFrame(), exception);
        }

        public void FailureOnShutdown(Exception exception)
        {
            WriteEvent(new EventInstance(0xC00003F5, 2, EventLogEntryType.Error));
            LogException(new StackFrame(), exception);
        }

        public void FailureOnStart(Exception exception)
        {
            WriteEvent(new EventInstance(0xC00003F3, 2, EventLogEntryType.Error));
            LogException(new StackFrame(), exception);
        }

        public void FailureOnStop(Exception exception)
        {
            WriteEvent(new EventInstance(0xC00003F4, 2, EventLogEntryType.Error));
            LogException(new StackFrame(), exception);
        }

        public void LoggingNotConfigured()
        {
            WriteEvent(new EventInstance(0x8000044C, 2, EventLogEntryType.Warning));
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

        private void LogException(StackFrame frame, Exception exception)
        {
            if (null == _log)
            {
                LoggingNotConfigured();
                return;
            }

            if (_log.IsErrorEnabled)
            {
                _log.Error(frame.GetMethod().Name, exception);
            }
        }
    }
}