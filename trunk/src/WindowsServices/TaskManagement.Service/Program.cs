namespace Cavity
{
    using System;
    using System.Configuration;
    using System.Reflection;
    using System.ServiceProcess;
    using Cavity.Configuration;
    using Cavity.Diagnostics;

    public static class Program
    {
        public static void Main()
        {
            log4net.Config.XmlConfigurator.Configure();
            AppDomain.CurrentDomain.UnhandledException += OnUnhandledException;
            Config.ExeSection<ServiceLocation>().Provider.Configure();
            ServiceBase.Run(new ServiceBase[]
            {
                new TaskManagementService()
            });
        }

        public static void OnUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            if (null == e)
            {
                LoggingSignature.Debug();
                return;
            }

            if (e.IsTerminating)
            {
                LoggingSignature.Fatal(e.ExceptionObject as Exception);
            }
            else
            {
                LoggingSignature.Error(e.ExceptionObject as Exception);
            }
        }
    }
}