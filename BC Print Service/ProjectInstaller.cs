using System.ComponentModel;

namespace BC_Print_Service
{
    [RunInstaller(true)]
    public partial class ProjectInstaller : System.Configuration.Install.Installer
    {
        public ProjectInstaller()
        {
            InitializeComponent();
        }

        private void serviceProcessInstaller1_AfterInstall(object sender, System.Configuration.Install.InstallEventArgs e)
        {

        }

        private void serviceInstaller1_AfterInstall(object sender, System.Configuration.Install.InstallEventArgs e)
        {

        }
    }
}
