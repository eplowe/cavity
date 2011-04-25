namespace Cavity
{
#if NET40
    using System;
    using System.ServiceProcess;
    using Cavity.Configuration;
    using Cavity.Diagnostics;
#endif

    public static class Program
    {
        public static void Main()
        {
#if NET40
            log4net.Config.XmlConfigurator.Configure();
            AppDomain.CurrentDomain.UnhandledException += OnUnhandledException;
            Config.ExeSection<ServiceLocation>().Provider.Configure();
            ServiceBase.Run(new ServiceBase[]
            {
                new TaskManagementService()
            });
#endif
        }

#if NET40
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
#endif
    }
}