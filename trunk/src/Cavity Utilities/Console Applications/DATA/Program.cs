namespace Cavity
{
    using System;
    using System.Diagnostics;

    using Cavity.Properties;

    public static class Program
    {
        public static void Main(string[] args)
        {
            log4net.Config.XmlConfigurator.Configure();
            try
            {
                Arguments.Load(args).Process();
            }
            catch (Exception exception)
            {
                Trace.TraceError("{0}", exception);
                Console.WriteLine("{0}: {1}".FormatWith(exception.GetType().Name, exception.Message));
            }

#if DEBUG
            Console.WriteLine(Resources.PressAnyKeyToExit);
            Console.ReadKey();
#endif
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