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
            ServiceLocation.Settings().Configure();
            if (null == ServiceLocator.Current.GetInstance<ITest>())
            {
                throw new NotImplementedException(Resources.Test_Failed);
            }
            else
            {
                Console.WriteLine(Resources.Test_Passed);
            }
#endif
        }
    }
}