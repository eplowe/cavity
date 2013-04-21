namespace Cavity
{
    using System;

    using Cavity.Properties;

    public static class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                Arguments.Load(args).Process();
            }
            catch (Exception exception)
            {
                Console.WriteLine(Resources.ExceptionFormat, exception.GetType().Name, exception.Message);
            }
        }

        public static void OnUnhandledException(object sender,
                                                UnhandledExceptionEventArgs e)
        {
            if (null == e)
            {
                return;
            }

            Console.WriteLine(e.ExceptionObject);
        }
    }
}