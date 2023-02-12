using System.ServiceProcess;

namespace BC_Local_Service
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {

            //Application.SetCompatibleTextRenderingDefault(false);
            PortableSettingsProvider.SettingsFileName = "settings.config";
            PortableSettingsProvider.SettingsDirectory = PortableSettingsProvider.SettingsDirectory + "Configuration";
            System.IO.Directory.CreateDirectory(PortableSettingsProvider.SettingsDirectory);
            PortableSettingsProvider.ApplyProvider(BC_Print_Service.Properties.Settings.Default);

            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                            new BCLocalService()
            };
            ServiceBase.Run(ServicesToRun); 
        }
    }
}
