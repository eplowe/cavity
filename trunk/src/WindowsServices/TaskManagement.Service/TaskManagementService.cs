namespace Cavity
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.ServiceProcess;
    using System.Threading;
    using Cavity.Diagnostics;
    using Cavity.Models;
    using Cavity.Properties;

    internal partial class TaskManagementService : ServiceBase
    {
        public TaskManagementService()
        {
            InitializeComponent();
        }

        private IManageTasks Manager { get; set; }

        [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Justification = "This is to prevent exceptions leaking out.")]
        protected override void OnContinue()
        {
            try
            {
                using (var log = new TaskManagementEventLog())
                {
                    Manager.Continue();
                    log.SuccessOnContinue();
                }
            }
            catch (Exception exception)
            {
                using (var log = new TaskManagementEventLog())
                {
                    log.FailureOnContinue(exception);
                }
            }
        }

        [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Justification = "This is to prevent exceptions leaking out.")]
        protected override void OnCustomCommand(int command)
        {
            try
            {
                throw new NotSupportedException(string.Format(
                    Thread.CurrentThread.CurrentUICulture,
                    Resources.TaskManagementService_UnsupportedCustomCommand,
                    command));
            }
            catch (Exception exception)
            {
                using (var log = new TaskManagementEventLog())
                {
                    log.FailureOnContinue(exception);
                }
            }
        }

        [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Justification = "This is to prevent exceptions leaking out.")]
        protected override void OnPause()
        {
            try
            {
                using (var log = new TaskManagementEventLog())
                {
                    Manager.Pause();
                    log.SuccessOnPause();
                }
            }
            catch (Exception exception)
            {
                using (var log = new TaskManagementEventLog())
                {
                    log.FailureOnPause(exception);
                }
            }
        }

        [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Justification = "This is to prevent exceptions leaking out.")]
        protected override bool OnPowerEvent(PowerBroadcastStatus powerStatus)
        {
            try
            {
                throw new NotSupportedException(string.Format(
                    Thread.CurrentThread.CurrentUICulture,
                    Resources.TaskManagementService_UnsupportedPowerEvent,
                    powerStatus.ToString("G")));
            }
            catch (Exception exception)
            {
                using (var log = new TaskManagementEventLog())
                {
                    log.FailureOnPowerEvent(exception, powerStatus);
                }
            }

            return false;
        }

        [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Justification = "This is to prevent exceptions leaking out.")]
        protected override void OnSessionChange(SessionChangeDescription changeDescription)
        {
            try
            {
                throw new NotSupportedException(string.Format(
                    Thread.CurrentThread.CurrentUICulture,
                    Resources.TaskManagementService_UnsupportedSessionChange,
                    changeDescription.Reason.ToString("G")));
            }
            catch (Exception exception)
            {
                using (var log = new TaskManagementEventLog())
                {
                    log.FailureOnSessionChange(exception, changeDescription);
                }
            }
        }

        [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Justification = "This is to prevent exceptions leaking out.")]
        protected override void OnShutdown()
        {
            try
            {
                using (var log = new TaskManagementEventLog())
                {
                    Manager.Shutdown();
                    Manager = null;
                    log.SuccessOnShutdown();
                }
            }
            catch (Exception exception)
            {
                using (var log = new TaskManagementEventLog())
                {
                    log.FailureOnShutdown(exception);
                }
            }
        }

        [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Justification = "This is to prevent exceptions leaking out.")]
        protected override void OnStart(string[] args)
        {
            try
            {
                using (var log = new TaskManagementEventLog())
                {
                    Manager = new TaskManager();
                    Manager.Start(args);
                    log.SuccessOnStart();
                }
            }
            catch (Exception exception)
            {
                using (var log = new TaskManagementEventLog())
                {
                    log.FailureOnStart(exception);
                }
            }
        }

        [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Justification = "This is to prevent exceptions leaking out.")]
        protected override void OnStop()
        {
            try
            {
                using (var log = new TaskManagementEventLog())
                {
                    Manager.Stop();
                    Manager = null;
                    log.SuccessOnStop();
                }
            }
            catch (Exception exception)
            {
                using (var log = new TaskManagementEventLog())
                {
                    log.FailureOnStop(exception);
                }
            }
        }
    }
}