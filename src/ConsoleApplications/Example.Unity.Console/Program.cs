namespace Example
{
#if !NET20
    using System;
    using Cavity.Configuration;
    using Example.Properties;
    using Microsoft.Practices.ServiceLocation;
#endif

    public static class Program
    {
        public static void Main()
        {
#if !NET20
            Config.ExeSection<ServiceLocation>().Provider.Configure();
            if (ServiceLocator.Current.GetInstance<ITest>().Test("value"))
            {
                Console.WriteLine(Resources.Test_Passed);
            }
            else
            {
                throw new NotImplementedException(Resources.Test_Failed);
            }
#endif
        }
    }
}