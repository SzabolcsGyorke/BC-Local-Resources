using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Timers;
using System.Runtime.InteropServices;
using BCPrint;
using System.Threading;
using BCLRS;

namespace BC_Print_Service
{
    public partial class BCPrintService : ServiceBase
    {

        private int eventId = 1;
        WebServiceHelper wshelper;

        string servicebaseurl = "";
        string username = "";
        string apikey = "";
        string mylocalprintingservicename = "";
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

        public BCPrintService()
        {
            InitializeComponent();
            string servicebaseurl = Properties.Settings.Default.BaseUrl.ToString();
            string username = Properties.Settings.Default.UserName.ToString();
            string apikey = Properties.Settings.Default.ApiKey.ToString();
            string mylocalprintingservicename = Properties.Settings.Default.Instance.ToString();

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
            if (wshelper == null)
            {
            //    wshelper = new WebServiceHelper(servicebaseurl, username, apikey, mylocalprintingservicename);
            }

            try
            {
                //Service maintenance
                wshelper.UpdateBCHeartbeat();
                if (wshelper.PrinterUpdateRequested())
                {
                    eventLog1.WriteEntry("Service update reqested by BC", EventLogEntryType.Information, eventId++);
                    wshelper.RegisterInstance();
                    sucsessfulsyncs = 0;
                }
                //Update Printers every 1000th HB
                if (sucsessfulsyncs == 1000)
                {
                    eventLog1.WriteEntry("Services updated", EventLogEntryType.Information, eventId++);
                    wshelper.RegisterInstance();
                    sucsessfulsyncs = 0;
                }
                if (wshelper.ErrorText == "") { 
                     sucsessfulsyncs++;
                } else
                {
                    eventLog1.WriteEntry(wshelper.ErrorText, EventLogEntryType.Error, eventId++);
                }
                //Get and print documents
                documents = wshelper.GetDocumentsForPrint();
                if (documents.Count > 0)
                {
                    foreach (WebDocument doc in documents)
                    {
                        string tempFile = System.IO.Path.GetTempFileName();
                        wshelper.GetBCDocument(doc.DocumentGUID, tempFile);
                        LocalPrinterHelper.SendPdfFileToPrinter(doc.PrinterName, tempFile, doc.DocumentName);
                        wshelper.SetBCDocumentComplete(doc.DocumentGUID);
                        if (wshelper.ErrorText != "")
                        {
                            eventLog1.WriteEntry(wshelper.ErrorText, EventLogEntryType.Error, eventId++);
                        }
                    }
                }
                else
                {
                    if (wshelper.ErrorText != "")
                    {
                        eventLog1.WriteEntry(wshelper.ErrorText, EventLogEntryType.Error, eventId++);
                    }
                }
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
