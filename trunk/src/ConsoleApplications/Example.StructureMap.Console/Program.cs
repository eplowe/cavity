namespace Example
{
    using System;
    using Cavity.Configuration;
    using Example.Properties;
    using Microsoft.Practices.ServiceLocation;

    public static class Program
    {
        public static void Main()
        {
            ServiceLocation.Settings().Configure();
            if (null == ServiceLocator.Current.GetInstance<ITest>())
            {
                throw new NotImplementedException(Resources.Test_Failed);
            }
            else
            {
                Console.WriteLine(Resources.Test_Passed);
            }
        }
    }
}