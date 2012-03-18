namespace Cavity
{
    using System;
    using System.Diagnostics;
    using System.ServiceProcess;

    using Cavity.Configuration;
    using Cavity.Diagnostics;

    public static class Program
    {
        public static void Main()
        {
            try
            {
                log4net.Config.XmlConfigurator.Configure();
                Trace.WriteIf(Tracing.Is.TraceVerbose, string.Empty);
                AppDomain.CurrentDomain.UnhandledException += OnUnhandledException;
                Config.ExeSection<ServiceLocation>().Provider.Configure();
                ServiceBase.Run(new ServiceBase[]
                                    {
                                        new TaskManagementService()
                                    });
            }
            catch (Exception exception)
            {
                Trace.TraceError("{0}", exception);
            }
        }

        public static void OnUnhandledException(object sender, 
                                                UnhandledExceptionEventArgs e)
        {
            if (null == e)
            {
                Trace.TraceWarning(string.Empty);
                return;
            }

            Trace.TraceError("{0}", e.ExceptionObject);
        }
    }
}