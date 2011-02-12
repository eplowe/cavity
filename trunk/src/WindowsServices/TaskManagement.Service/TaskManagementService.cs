namespace Cavity
{
    using System.ServiceProcess;

    internal partial class TaskManagementService : ServiceBase
    {
        public TaskManagementService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
        }

        protected override void OnStop()
        {
        }
    }
}