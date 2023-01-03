using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Net;
using System.IO;
using BC;
using Microsoft.OData.Client;
using static BCLRS.FileHelper;
using BCLRS;
using BCPrint;
using RestSharp;

namespace BCLRS
{ 

    public enum AuthType
    {
        Basic,
        oAuth
    }

    public class WebAuthentication
    {
        string servicebaseurl;
        string mylocalprintingservicename;
        AuthType serverauthentication;
        string basicauthheader;
        string authurl;
        string redirecturl;
        string clientid;
        string clientsecret;
        string token;
        DateTime tokenexpiry;
        public WebAuthentication()
        {

        }

        public void GetToken()
        {
            var client = new RestClient(authurl);
            var request = new RestRequest();
            request.Method = Method.Post;
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddParameter("application/x-www-form-urlencoded", string.Format("grant_type=client_credentials&client_id={0}&client_secret={1}", clientid, clientsecret), ParameterType.RequestBody);
            RestResponse response = client.Execute(request);

            token = response.Content.ToString();
        }
    }

    public class WebDocument
    {
        public Guid DocumentGUID { get; set; }
        public String DocumentName { get; set; }
        public String PrinterName { get; set; }
        public int Copies { get; set; }

        public bool UseRaw { get; set; }

        public WebDocument(Guid _DocumentGUID, String _DocumentName)
        {
            DocumentGUID = _DocumentGUID;
            DocumentName = _DocumentName;
        }

        public WebDocument(Guid _DocumentGUID, String _DocumentName, String _PrinterName, int _Copies, bool useRaw)
        {
            DocumentGUID = _DocumentGUID;
            DocumentName = _DocumentName;
            PrinterName = _PrinterName;
            Copies = _Copies;
            UseRaw = useRaw;
        }
    }

   

    public class WebServiceHelper
    {
        string servicebaseurl;
        string mylocalprintingservicename;
        AuthType serverauthentication;
        string basicauthheader;


        public WebServiceHelper(string _servicebaseurl, AuthType _serverauthentication, string _username, string _password, string _mylocalprintingservicename)
        {
            servicebaseurl = _servicebaseurl;
            mylocalprintingservicename = _mylocalprintingservicename;
            serverauthentication = _serverauthentication;
            basicauthheader = CalculateBasicAuthHeader(_username, _password);
            Initilaized = true;
        }

        private string CalculateBasicAuthHeader(string username, string password)
        {
            var authbyte = System.Text.Encoding.UTF8.GetBytes(username + ":" + password);
            return Convert.ToBase64String(authbyte);
        }

        public bool Initilaized { get; }
        public string ErrorText { get; set; }

        public void RegisterInstance()
        {
            var serviceRoot = servicebaseurl;
            var context = new BC.NAV(new Uri(serviceRoot));
            context.BuildingRequest += InjectHeader;

            Uri actionUri = new Uri(context.BaseUri, "BCLRSServiceFunctions('')/NAV.RegisterPrinterService");

            BodyOperationParameter[] parameres = { new BodyOperationParameter("localService", mylocalprintingservicename) };
            var retval = context.Execute<bool>(actionUri, "POST", true, parameres);
            //add printers
            ArrayList localprinters = LocalPrinterHelper.GetLocalPrinters();
            foreach (string printername in localprinters)
            {
                actionUri = new Uri(context.BaseUri, "BCLRSServiceFunctions('')/NAV.AddPrinterServicePrinter");

                parameres = new BodyOperationParameter[2] { new BodyOperationParameter("localService", mylocalprintingservicename), new BodyOperationParameter("printer", printername) };
                retval = context.Execute<bool>(actionUri, "POST", true, parameres);
            }

            //add local folders

        }

        private void InjectHeader(object sender, Microsoft.OData.Client.BuildingRequestEventArgs e)
        {
            //e.RequestUri = new Uri(e.RequestUri.ToString().Replace("V4/", "V4/Company('CRONUS%20UK%20Ltd.')/"));
            // e.RequestUri = new Uri(e.RequestUri.ToString()+ "?$filter=No eq '51600'");
            if (serverauthentication == AuthType.Basic)
                e.Headers.Add("Authorization", "Basic " + basicauthheader);
            //e.Headers.Add("Authorization", "Basic YWRtaW46UEBzc3cwcmQ=");
        }

        public bool RegisterFolder(string foldername, int direction)
        {
            if (foldername == "")
                return true;

            ErrorText = "";
            var serviceRoot = servicebaseurl;
            var context = new BC.NAV(new Uri(serviceRoot));
            context.BuildingRequest += InjectHeader;

            Uri actionUri = new Uri(context.BaseUri, "BCLRSServiceFunctions('')/NAV.AddFileService");

            BodyOperationParameter[] parameres = new BodyOperationParameter[3] { new BodyOperationParameter("localService", mylocalprintingservicename), new BodyOperationParameter("fileQueue", foldername), new BodyOperationParameter("direction", direction) };

            try
            {
                var retval = context.Execute<bool>(actionUri, "POST", true, parameres);
            }
            catch (Exception eee)
            {
                ErrorText = eee.Message;
            }


            return true;
        }

        public List<WebDocument> GetDocumentsForPrint()
        {
            ErrorText = "";
            List<WebDocument> documents = new List<WebDocument>();

            var serviceRoot = servicebaseurl;
            var context = new BC.NAV(new Uri(serviceRoot));
            context.BuildingRequest += InjectHeader;

            try
            {
                var BCLRSEntries = context.BCLRSEntryList.AddQueryOption("$filter", "entryType eq 'Print' and localServiceCode eq '" + mylocalprintingservicename + "' and entryCompleted eq false").Execute();
                
                foreach (var document in BCLRSEntries)
                {
                    int copies = Convert.ToInt32(document.PrintCopies); //???
                    bool useraw = Convert.ToBoolean(document.UseRawPrint);
                    documents.Add(new WebDocument(document.EntryGUID, document.DocumentName, document.LocalResourceCode, copies, useraw));
                }

            }
            catch (Exception eee)
            {

                ErrorText = eee.Message;
            }

            return documents;
        }

        public List<BCFolder> GetBCFolders()
        {
            ErrorText = "";
            List<BCFolder> folders = new List<BCFolder>();

            var serviceRoot = servicebaseurl;
            var context = new BC.NAV(new Uri(serviceRoot));
            context.BuildingRequest += InjectHeader;

            try
            {
                var BCLRSEntries = context.BCLRSServicesAPI.AddQueryOption("$filter", "resourceType ne 'Printer' and localServiceCode eq '" + mylocalprintingservicename + "'").Execute();

                foreach (var document in BCLRSEntries)
                {
                    bool upload = (document.ResourceType == "File Queue From Client") ;
                    bool enabled = Convert.ToBoolean(document.Enabled);
                    FileUploadAction fileUploadAction = FileUploadAction.KeepFile;

                    if (document.AfterImportFileAction.Contains("Delete Source")) fileUploadAction = FileUploadAction.DeleteFile;
                    if (document.AfterImportFileAction.Contains("Move to Archive")) fileUploadAction = FileUploadAction.ArchiveFile;

                    folders.Add(new BCFolder(document.No,document.LocalResourceCode,upload,enabled,fileUploadAction));
                }

            }
            catch (Exception eee)
            {

                ErrorText = eee.InnerException.Message;
            }

            return folders;
        }

        public bool GetBCDocument(WebDocument document)
        {

            return GetBCDocument(document.DocumentGUID, document.DocumentName);
        }

        public bool GetBCDocument(Guid DocumentGuid, string DocumentName)
        {
            ErrorText = "";
            var serviceRoot = servicebaseurl;
            var context = new BC.NAV(new Uri(serviceRoot));
            context.BuildingRequest += InjectHeader;

            Uri actionUri = new Uri(context.BaseUri, "BCLRSServiceFunctions('')/NAV.GetEntryFile");

            BodyOperationParameter[] parameres = { new BodyOperationParameter("documentGUID", DocumentGuid) };

            string base64result = "";
            try
            {

                base64result = context.Execute<string>(actionUri, "POST", true, parameres).First();

                File.WriteAllBytes(DocumentName, Convert.FromBase64String(base64result));
            }
            catch (Exception eee)
            {

                ErrorText = eee.Message;
                return false;
            }


            return true;
        }

        private void AddBlobRef(object sender, BuildingRequestEventArgs e)
        {
            throw new NotImplementedException();
        }

        public void SetBCDocumentComplete(Guid DocumentGuid)
        {
            ErrorText = "";
            var serviceRoot = servicebaseurl;
            var context = new BC.NAV(new Uri(serviceRoot));
            context.BuildingRequest += InjectHeader;

            Uri actionUri = new Uri(context.BaseUri, "BCLRSServiceFunctions('')/NAV.SetDocumentComplete");

            BodyOperationParameter[] parameres = { new BodyOperationParameter("documentGUID", DocumentGuid) };

            try
            {
                var retval = context.Execute<bool>(actionUri, "POST", true, parameres);
            }
            catch (Exception eee)
            {
                ErrorText = eee.Message;
            }

        }

        public bool UpdateBCHeartbeat()
        {
            ErrorText = "";
            var serviceRoot = servicebaseurl;
            var context = new BC.NAV(new Uri(serviceRoot));
            context.BuildingRequest += InjectHeader;

            Uri actionUri = new Uri(context.BaseUri, "BCLRSServiceFunctions('')/NAV.UpdateHeartBeat");

            BodyOperationParameter[] parameres = { new BodyOperationParameter("localService", mylocalprintingservicename) };

            try
            {
                var retval = context.Execute<bool>(actionUri, "POST", true, parameres);
            }
            catch (Exception eee)
            {
                ErrorText = eee.InnerException.Message;
                return false;
            }
            return true;
        }
        public bool PrinterUpdateRequested()
        {
            ErrorText = "";
            var serviceRoot = servicebaseurl;
            var context = new BC.NAV(new Uri(serviceRoot));
            context.BuildingRequest += InjectHeader;

            Uri actionUri = new Uri(context.BaseUri, "BCLRSServiceFunctions('')/NAV.ServiceUpdateRequested");

            BodyOperationParameter[] parameres = { new BodyOperationParameter("localService", mylocalprintingservicename) };
            try
            {
                var retval = context.Execute<bool>(actionUri, "POST", true, parameres).First();
                return retval;
            }
            catch (Exception eee)
            {
                ErrorText = eee.Message;
            }
            return false;
        }

        public FileUploadAction UploadFile(string filepath)
        {
            string folder = Path.GetDirectoryName(filepath);
            string filename = Path.GetFileName(filepath);
            FileStream file = File.OpenRead(filepath);
            byte[] buffer = new byte[file.Length];
            file.Read(buffer, 0, buffer.Length);
            file.Close();
            string base64data = Convert.ToBase64String(buffer);

            ErrorText = "";
            var serviceRoot = servicebaseurl;
            var context = new BC.NAV(new Uri(serviceRoot));
            context.BuildingRequest += InjectHeader;

            Uri actionUri = new Uri(context.BaseUri, "BCLRSServiceFunctions('')/NAV.UploadEntry");

            BodyOperationParameter[] parameres = new BodyOperationParameter[4] { new BodyOperationParameter("localService", mylocalprintingservicename), new BodyOperationParameter("fileQueue", folder), new BodyOperationParameter("description", filename), new BodyOperationParameter("base64Result", base64data) };
            try
            {
                int fileaction = context.Execute<int>(actionUri, "POST", true, parameres).First();

                if (fileaction == 0) { return FileUploadAction.KeepFile; }
                if (fileaction == 1 | fileaction == 3) { return FileUploadAction.DeleteFile; }
                if (fileaction == 2 | fileaction == 4) { return FileUploadAction.ArchiveFile; }
            }
            catch (Exception eee)
            {
                ErrorText = eee.InnerException.Message;
                return FileUploadAction.Error;
            }

            return FileUploadAction.KeepFile;
        }

        public List<WebDocument> GetDocumentsForDownload()
        {
            ErrorText = "";
            List<WebDocument> documents = new List<WebDocument>();

            var serviceRoot = servicebaseurl;
            var context = new BC.NAV(new Uri(serviceRoot));
            context.BuildingRequest += InjectHeader;

            try
            {
                var BCLRSEntries = context.BCLRSEntryList.AddQueryOption("$filter", "entryType eq 'File To Client' and localServiceCode eq '" + mylocalprintingservicename + "' and entryCompleted eq false and documentName ne ''").Execute();

                foreach (var document in BCLRSEntries)
                {
                    documents.Add(new WebDocument(document.EntryGUID, document.DocumentName, document.LocalResourceCode, 1, false));
                }

            }
            catch (Exception eee)
            {

                ErrorText = eee.InnerException.Message;
            }

            return documents;
        }
    }
}
