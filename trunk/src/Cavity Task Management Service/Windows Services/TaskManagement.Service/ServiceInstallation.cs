namespace Cavity
{
    using System.ComponentModel;
    using System.Configuration.Install;

    [RunInstaller(true)]
    public partial class ServiceInstallation : Installer
    {
        public ServiceInstallation()
        {
            InitializeComponent();
        }
    }
}