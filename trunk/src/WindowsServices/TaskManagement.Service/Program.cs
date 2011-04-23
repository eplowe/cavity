namespace Cavity
{
    using System.ServiceProcess;

    public static class Program
    {
        public static void Main()
        {
            log4net.Config.BasicConfigurator.Configure();
            log4net.LogManager.GetLogger(typeof(Program)).Info("Program starting.");
            ServiceBase.Run(new ServiceBase[]
            {
                new TaskManagementService()
            });
        }
    }
}