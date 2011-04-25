namespace Cavity
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.ServiceProcess;
    using System.Threading;
    using Cavity.Diagnostics;
    using Cavity.Models;
    using Cavity.Properties;
    using Microsoft.Practices.ServiceLocation;

    internal partial class TaskManagementService : ServiceBase
    {
        public TaskManagementService()
        {
            LoggingSignature.Debug();
            InitializeComponent();
        }

        private IManageTasks Manager { get; set; }

        [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Justification = "This is to prevent exceptions leaking out.")]
        protected override void OnContinue()
        {
            using (var eventLog = new TaskManagementEventLog())
            {
                try
                {
                    LoggingSignature.Debug();
                    Manager.Continue();
                    eventLog.SuccessOnContinue();
                }
                catch (Exception exception)
                {
                    eventLog.FailureOnContinue();
                    LoggingSignature.Error(exception);
                }
            }
        }

        [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Justification = "This is to prevent exceptions leaking out.")]
        protected override void OnCustomCommand(int command)
        {
            using (var eventLog = new TaskManagementEventLog())
            {
                try
                {
                    LoggingSignature.Debug();
                    throw new NotSupportedException(string.Format(
                        Thread.CurrentThread.CurrentUICulture,
                        Resources.TaskManagementService_UnsupportedCustomCommand,
                        command));
                }
                catch (Exception exception)
                {
                    eventLog.FailureOnContinue();
                    LoggingSignature.Error(exception);
                }
            }
        }

        [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Justification = "This is to prevent exceptions leaking out.")]
        protected override void OnPause()
        {
            using (var eventLog = new TaskManagementEventLog())
            {
                try
                {
                    LoggingSignature.Debug();
                    Manager.Pause();
                    eventLog.SuccessOnPause();
                }
                catch (Exception exception)
                {
                    eventLog.FailureOnPause();
                    LoggingSignature.Error(exception);
                }
            }
        }

        [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Justification = "This is to prevent exceptions leaking out.")]
        protected override bool OnPowerEvent(PowerBroadcastStatus powerStatus)
        {
            using (var eventLog = new TaskManagementEventLog())
            {
                try
                {
                    LoggingSignature.Debug();
                    throw new NotSupportedException(string.Format(
                        Thread.CurrentThread.CurrentUICulture,
                        Resources.TaskManagementService_UnsupportedPowerEvent,
                        powerStatus.ToString("G")));
                }
                catch (Exception exception)
                {
                    eventLog.FailureOnPowerEvent(powerStatus);
                    LoggingSignature.Error(exception);
                }
            }

            return false;
        }

        [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Justification = "This is to prevent exceptions leaking out.")]
        protected override void OnSessionChange(SessionChangeDescription changeDescription)
        {
            using (var eventLog = new TaskManagementEventLog())
            {
                try
                {
                    LoggingSignature.Debug();
                    throw new NotSupportedException(string.Format(
                        Thread.CurrentThread.CurrentUICulture,
                        Resources.TaskManagementService_UnsupportedSessionChange,
                        changeDescription.Reason.ToString("G")));
                }
                catch (Exception exception)
                {
                    eventLog.FailureOnSessionChange(changeDescription);
                    LoggingSignature.Error(exception);
                }
            }
        }

        [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Justification = "This is to prevent exceptions leaking out.")]
        protected override void OnShutdown()
        {
            using (var eventLog = new TaskManagementEventLog())
            {
                try
                {
                    LoggingSignature.Debug();
                    Manager.Shutdown();
                    Manager = null;
                    eventLog.SuccessOnShutdown();
                }
                catch (Exception exception)
                {
                    eventLog.FailureOnShutdown();
                    LoggingSignature.Error(exception);
                }
            }
        }

        [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Justification = "This is to prevent exceptions leaking out.")]
        protected override void OnStart(string[] args)
        {
            using (var eventLog = new TaskManagementEventLog())
            {
                try
                {
                    LoggingSignature.Debug();
                    Manager = ServiceLocator.Current.GetInstance<IManageTasks>();
                    Manager.Start(args);
                    eventLog.SuccessOnStart();
                }
                catch (Exception exception)
                {
                    eventLog.FailureOnStart();
                    LoggingSignature.Error(exception);
                }
            }
        }

        [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Justification = "This is to prevent exceptions leaking out.")]
        protected override void OnStop()
        {
            using (var eventLog = new TaskManagementEventLog())
            {
                try
                {
                    LoggingSignature.Debug();
                    Manager.Stop();
                    Manager = null;
                    eventLog.SuccessOnStop();
                }
                catch (Exception exception)
                {
                    eventLog.FailureOnStop();
                    LoggingSignature.Error(exception);
                }
            }
        }
    }
}