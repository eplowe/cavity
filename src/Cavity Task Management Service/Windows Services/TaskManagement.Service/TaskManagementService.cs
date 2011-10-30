namespace Cavity
{
    using System;
    using System.Diagnostics;
    using System.Diagnostics.CodeAnalysis;
    using System.ServiceProcess;
    using System.Threading;
    using Cavity.Diagnostics;
    using Cavity.Properties;
    using Cavity.Threading;
    using Microsoft.Practices.ServiceLocation;

    internal partial class TaskManagementService : ServiceBase
    {
        public TaskManagementService()
        {
            Trace.WriteLineIf(Tracing.Is.TraceVerbose, string.Empty);
            InitializeComponent();
            Disposed += delegate
            {
                OnDispose();
            };
        }

        private string[] Args { get; set; }

        private IManageTasks Manager { get; set; }

        [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Justification = "This is to prevent exceptions leaking out.")]
        protected override void OnContinue()
        {
            using (var eventLog = new TaskManagementEventLog())
            {
                try
                {
                    Trace.WriteLineIf(Tracing.Is.TraceVerbose, string.Empty);
                    if (null == Manager)
                    {
                        throw new InvalidOperationException();
                    }

                    Manager.Continue();
                    eventLog.SuccessOnContinue();
                }
                catch (Exception exception)
                {
                    eventLog.FailureOnContinue();
                    Trace.TraceError("{0}", exception);
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
                    Trace.WriteLineIf(Tracing.Is.TraceVerbose, "command={0}".FormatWith(command));
                    throw new NotSupportedException(string.Format(
                        Thread.CurrentThread.CurrentUICulture,
                        Resources.TaskManagementService_UnsupportedCustomCommand,
                        command));
                }
                catch (Exception exception)
                {
                    eventLog.FailureOnContinue();
                    Trace.TraceError("{0}", exception);
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
                    Trace.WriteLineIf(Tracing.Is.TraceVerbose, string.Empty);
                    if (null == Manager)
                    {
                        throw new InvalidOperationException();
                    }

                    Manager.Pause();
                    eventLog.SuccessOnPause();
                }
                catch (Exception exception)
                {
                    eventLog.FailureOnPause();
                    Trace.TraceError("{0}", exception);
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
                    Trace.WriteLineIf(Tracing.Is.TraceVerbose, "powerStatus={0}".FormatWith(powerStatus.ToString("G")));
                    switch (powerStatus)
                    {
                        case PowerBroadcastStatus.QuerySuspend:
                            OnPause();
                            break;

                        case PowerBroadcastStatus.Suspend:
                            OnStop();
                            break;

                        case PowerBroadcastStatus.QuerySuspendFailed:
                        case PowerBroadcastStatus.ResumeAutomatic:
                        case PowerBroadcastStatus.ResumeCritical:
                        case PowerBroadcastStatus.ResumeSuspend:
                            if (null == Manager)
                            {
                                OnStart(Args);
                            }
                            else
                            {
                                OnContinue();
                            }

                            break;

                        default:
                            break;
                    }
                }
                catch (Exception exception)
                {
                    eventLog.FailureOnPowerEvent(powerStatus);
                    Trace.TraceError("{0}", exception);
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
                    Trace.WriteLineIf(Tracing.Is.TraceVerbose, "changeDescription.Reason={0}".FormatWith(changeDescription.Reason.ToString("G")));
                    throw new NotSupportedException(string.Format(
                        Thread.CurrentThread.CurrentUICulture,
                        Resources.TaskManagementService_UnsupportedSessionChange,
                        changeDescription.Reason.ToString("G")));
                }
                catch (Exception exception)
                {
                    eventLog.FailureOnSessionChange(changeDescription);
                    Trace.TraceError("{0}", exception);
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
                    Trace.WriteLineIf(Tracing.Is.TraceVerbose, string.Empty);
                    if (null == Manager)
                    {
                        return;
                    }

                    Manager.Shutdown();
                    OnDispose();
                    eventLog.SuccessOnShutdown();
                }
                catch (Exception exception)
                {
                    eventLog.FailureOnShutdown();
                    Trace.TraceError("{0}", exception);
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
                    var combo = string.Empty;
                    if (null != args)
                    {
                        foreach (var arg in args)
                        {
                            combo = "{0}{1}\"{2}\"".FormatWith(combo, null == combo ? string.Empty : ", ", arg);
                        }
                    }

                    Trace.WriteLineIf(Tracing.Is.TraceVerbose, "args={0}".FormatWith(combo));
                    if (null != Manager)
                    {
                        throw new InvalidOperationException();
                    }

                    Args = args;
                    Manager = ServiceLocator.Current.GetInstance<IManageTasks>();
                    Manager.Start(args);
                    eventLog.SuccessOnStart();
                }
                catch (Exception exception)
                {
                    eventLog.FailureOnStart();
                    Trace.TraceError("{0}", exception);
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
                    Trace.WriteLineIf(Tracing.Is.TraceVerbose, string.Empty);
                    if (null == Manager)
                    {
                        return;
                    }

                    Manager.Stop();
                    OnDispose();
                    eventLog.SuccessOnStop();
                }
                catch (Exception exception)
                {
                    eventLog.FailureOnStop();
                    Trace.TraceError("{0}", exception);
                }
            }
        }

        private void OnDispose()
        {
            Trace.WriteLineIf(Tracing.Is.TraceVerbose, string.Empty);
            if (null == Manager)
            {
                return;
            }

            Manager.Stop();
            Manager.Dispose();
            Manager = null;
        }
    }
}