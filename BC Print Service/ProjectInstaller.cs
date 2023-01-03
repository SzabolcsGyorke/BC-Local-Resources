using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;
using System.Threading.Tasks;

namespace BC_Print_Service
{
    [RunInstaller(true)]
    public partial class ProjectInstaller : System.Configuration.Install.Installer
    {
        public ProjectInstaller()
        {
            InitializeComponent();
        }

        private void serviceInstaller1_BeforeInstall(object sender, InstallEventArgs e)
        {
            this.serviceInstaller1.DisplayName = this.serviceInstaller1.DisplayName.ToString() + "TEST01";
            serviceInstaller1.ServiceName = serviceInstaller1.ServiceName.ToString() + "TEST01";

        }

       
    }
}
