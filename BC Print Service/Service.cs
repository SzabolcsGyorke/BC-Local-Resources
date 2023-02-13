using BCLRS;
using BCPrint;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.ServiceProcess;
using System.Timers;

namespace BC_Local_Service
{
    public partial class BCLocalService : ServiceBase
    {

        private int eventId = 1;
        WebServiceHelper wshelper;
        WebAuthentication webAuthentication;
        List<WebDocument> documents;
        List<BCFolder> folders;
        AuthType authtype = AuthType.Basic;
        private int sucsessfulsyncs = 1000;

        public enum ServiceState
        {
            SERVICE_STOPPED = 0x00000001,
            SERVICE_START_PENDING = 0x00000002,
            SERVICE_STOP_PENDING = 0x00000003,
            SERVICE_RUNNING = 0x00000004,
            SERVICE_CONTINUE_PENDING = 0x00000005,
            SERVICE_PAUSE_PENDING = 0x00000006,
            SERVICE_PAUSED = 0x00000007,
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct ServiceStatus
        {
            public int dwServiceType;
            public ServiceState dwCurrentState;
            public int dwControlsAccepted;
            public int dwWin32ExitCode;
            public int dwServiceSpecificExitCode;
            public int dwCheckPoint;
            public int dwWaitHint;
        };

        [DllImport("advapi32.dll", SetLastError = true)]
        private static extern bool SetServiceStatus(System.IntPtr handle, ref ServiceStatus serviceStatus);

        public BCLocalService()
        {
            InitializeComponent();
            string servicebaseurl = BC_Print_Service.Properties.Settings.Default.BaseUrl.ToString(); //Properties.Settings.Default.BaseUrl.ToString();
            string username = BC_Print_Service.Properties.Settings.Default.UserName.ToString();
            string apikey = BC_Print_Service.Properties.Settings.Default.ApiKey.ToString();
            string mylocalprintingservicename = BC_Print_Service.Properties.Settings.Default.Instance.ToString();
            string authurl = BC_Print_Service.Properties.Settings.Default.AuthUrl.ToString();
            string redirecturl = BC_Print_Service.Properties.Settings.Default.RedirectURL.ToString();
            string scope = BC_Print_Service.Properties.Settings.Default.Scope.ToString();

            authtype = (BC_Print_Service.Properties.Settings.Default.AuthType == AuthType.Basic.ToString()) ? AuthType.Basic : AuthType.oAuth;

            if (webAuthentication == null)
            {
                webAuthentication = new WebAuthentication(authtype, username, apikey, username, apikey, authurl, redirecturl, scope);
            }
            if (wshelper == null)
            {
                wshelper = new WebServiceHelper(servicebaseurl, webAuthentication, mylocalprintingservicename);
            }

            //Thread.Sleep(20000);

            eventLog1 = new System.Diagnostics.EventLog();
            if (!System.Diagnostics.EventLog.SourceExists(mylocalprintingservicename))
            {
                System.Diagnostics.EventLog.CreateEventSource(
                     mylocalprintingservicename, "BC Local Resources Service");
            }
            eventLog1.Source = mylocalprintingservicename;
            eventLog1.Log = "BC Local Resources Service";

        }

        protected override void OnStart(string[] args)
        {
            eventLog1.WriteEntry("Service Started. - Timer set to 10 seconds");

            // Update the service state to Start Pending.
            ServiceStatus serviceStatus = new ServiceStatus();
            serviceStatus.dwCurrentState = ServiceState.SERVICE_START_PENDING;
            serviceStatus.dwWaitHint = 100000;
            SetServiceStatus(this.ServiceHandle, ref serviceStatus);

            // Set up a timer that triggers every minute.
            System.Timers.Timer timer = new System.Timers.Timer();
            timer.Interval = 10000; // 10 seconds
            timer.Elapsed += new ElapsedEventHandler(this.OnTimer);
            timer.Start();

            // Update the service state to Running.
            serviceStatus.dwCurrentState = ServiceState.SERVICE_RUNNING;
            SetServiceStatus(this.ServiceHandle, ref serviceStatus);
        }

        private void OnTimer(object sender, ElapsedEventArgs e)
        {
            //eventLog1.WriteEntry("Monitoring the System", EventLogEntryType.Information, eventId++);
            List<WebDocument> documents;


            try
            {
                //Service maintenance
                if (!wshelper.UpdateBCHeartbeat())
                    eventLog1.WriteEntry("Heartbeat Update Error " + wshelper.ErrorText, EventLogEntryType.Error, eventId++);


                if (wshelper.PrinterUpdateRequested())
                {
                    eventLog1.WriteEntry("Service update reqested by BC", EventLogEntryType.Information, eventId++);
                    wshelper.RegisterInstance();
                    sucsessfulsyncs = 0;
                }

                //Syncfiles
                BCLRS.FileHelper filehelper = new FileHelper(wshelper);
                if (!filehelper.SyncBCFolders())
                    eventLog1.WriteEntry("File Sync. Error " + wshelper.ErrorText, EventLogEntryType.Error, eventId++);


                //Get and print documents
                documents = wshelper.GetDocumentsForPrint();
                if (documents.Count > 0)
                {
                    foreach (WebDocument doc in documents)
                    {
                        string tempFile = Path.GetTempFileName();
                        if (wshelper.GetBCDocument(doc.DocumentGUID, tempFile))
                        {
                            LocalPrinterHelper.pdfprinertype = (BC_Print_Service.Properties.Settings.Default.PdfPrinter.ToString() == PdfPrinterType.FoxIt.ToString()) ? PdfPrinterType.FoxIt : PdfPrinterType.AdobeAcrobat;
                            LocalPrinterHelper.SendPdfFileToPrinter(doc.PrinterName, tempFile, doc.DocumentName);
                            wshelper.SetBCDocumentComplete(doc.DocumentGUID);

                        }
                        else
                            eventLog1.WriteEntry("Document Query " + wshelper.ErrorText, EventLogEntryType.Error, eventId++);
                    }
                }
                else
                {
                    if (wshelper.ErrorText != "")
                    {
                        eventLog1.WriteEntry("Printing " + wshelper.ErrorText, EventLogEntryType.Error, eventId++);
                    }
                }

                //command

                List<WebCommand> commands = wshelper.GetWebCommands();
                if (commands.Count > 0)
                {
                    foreach (WebCommand command in commands)
                    {
                        ExecuteCommand(command.Command);
                        wshelper.SetBCDocumentComplete(command.CommandGUID);
                    }
                }

            }
            catch (Exception eee)
            {
                eventLog1.WriteEntry(eee.InnerException.ToString(), EventLogEntryType.Error, eventId++);
            }
        }

        private void ExecuteCommand(string _command)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo();
            eventLog1.WriteEntry(_command, EventLogEntryType.Information, eventId++);
            string[] comparts = _command.Split(new char[] { '^' });
            if (comparts.Length == 2)
            {
                startInfo.FileName = comparts[0];
                startInfo.Arguments = comparts[1];
            }
            else
            {
                startInfo.FileName = _command;
            }

            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            startInfo.CreateNoWindow = true;
            startInfo.ErrorDialog = false;
            startInfo.UseShellExecute = false;

            try
            {
                Process process = Process.Start(startInfo);
            }
            catch (Exception eee)
            {
                eventLog1.WriteEntry(eee.InnerException.ToString(), EventLogEntryType.Error, eventId++);
            }
        }
        protected override void OnStop()
        {
            // Update the service state to Stop Pending.
            ServiceStatus serviceStatus = new ServiceStatus();
            serviceStatus.dwCurrentState = ServiceState.SERVICE_STOP_PENDING;
            serviceStatus.dwWaitHint = 100000;
            SetServiceStatus(this.ServiceHandle, ref serviceStatus);

            eventLog1.WriteEntry("Service Stopped");

            // Update the service state to Stopped.
            serviceStatus.dwCurrentState = ServiceState.SERVICE_STOPPED;
            SetServiceStatus(this.ServiceHandle, ref serviceStatus);
        }

        protected override void OnContinue()
        {
            eventLog1.WriteEntry("In OnContinue.");
        }
    }
}
