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
            if (ServiceLocator.Current.GetInstance<ITest>().Test("value"))
            {
                Console.WriteLine(Resources.Test_Passed);
            }
            else
            {
                throw new NotImplementedException(Resources.Test_Failed);
            }
        }
    }
}