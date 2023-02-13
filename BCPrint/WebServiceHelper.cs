using BCPrint;
using Microsoft.OData.Client;
using RestSharp;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.Json;
using static BCLRS.FileHelper;
using static BCPrint.LocalPrinterHelper;

namespace BCLRS
{

    public enum AuthType
    {
        Basic,
        oAuth
    }

    public class WebAuthentication
    {
        public string basicauthheader { get; set; }
        public AuthType serverauthentication { get; set; }
        string authurl;
        string redirecturl;
        string clientid;
        string clientsecret;
        string scope;
        JsonElement tokenjson;
        string token_auth;
        int tokenexpiresin;
        DateTime tokenacquired;
        DateTime tokenexpdatetime;
        public WebAuthentication(AuthType _serverauthentication, string _username, string _password, string _clientid, string _clientsecret, string _authurl, string _redirecturl, string _scope)
        {
            serverauthentication = _serverauthentication;
            clientid = _clientid;
            clientsecret = _clientsecret;
            authurl = _authurl;
            scope = _scope;
            redirecturl = _redirecturl;

            if (serverauthentication == AuthType.Basic)
            {
                basicauthheader = CalculateBasicAuthHeader(_username, _password);
            }

        }

        public bool GetToken()
        {
            //if the token still valid
            if (tokenexpdatetime > DateTime.Now)
            {
                return true;
            }

            //get new token
            var client = new RestClient(authurl);
            var request = new RestRequest();
            request.Method = Method.Post;
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddParameter("application/x-www-form-urlencoded", string.Format("grant_type=client_credentials&client_id={0}&client_secret={1}&scope={2}", clientid, clientsecret, scope), ParameterType.RequestBody);

            RestResponse response = client.Execute(request);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                if (response.Content != null)
                {
                    JsonDocument data = JsonDocument.Parse(response.Content);
                    JsonElement token = data.RootElement;
                    //tokenexpiresin = token.GetProperty("expires_in").GetInt32();
                    token_auth = token.GetProperty("access_token").GetString();
                    tokenexpiresin = token.GetProperty("expires_in").GetInt32();
                    tokenacquired = DateTime.Now;
                    tokenexpdatetime = DateTime.Now.AddSeconds(tokenexpiresin);
                    return true;
                }
            }
            else
            {

            }

            return false;
        }

        public bool RefreshToken()
        {
            var client = new RestClient(authurl);
            var request = new RestRequest();
            request.Method = Method.Post;
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddParameter("application/x-www-form-urlencoded", string.Format("grant_type=refresh_token&refresh_token={0}&redirect_uri={1}&client_id={2}&client_secret", token_auth, redirecturl, clientid, clientsecret), ParameterType.RequestBody);

            RestResponse response = client.Execute(request);

            if (response.StatusCode == HttpStatusCode.OK)
            { return true; }

            return true;
        }
        private string CalculateBasicAuthHeader(string username, string password)
        {
            var authbyte = System.Text.Encoding.UTF8.GetBytes(username + ":" + password);
            return Convert.ToBase64String(authbyte);
        }

        internal string GetBearer()
        {
            if (GetToken()) return token_auth;

            else return null;
        }

        public string GetTokenInfo()
        {
            return string.Format("Token expires: {0}\nToken:\n{1}", tokenexpdatetime, GetBearer());
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

    public class WebCommand
    {
        public Guid CommandGUID { get; set; }
        public String Command { get; set; }
        public bool Done { get; set; }

        public WebCommand(Guid _CommandGUID, String _Command, bool _Done)
        {
            CommandGUID = _CommandGUID;
            Command = _Command;
            Done = _Done;
        }
    }

    public class WebSetting
    {
        public string Key { get; set; }
        public string Value { get; set; }

        public WebSetting(string key, string value)
        {
            Key = key;
            Value = value;
        }
    }


    public class WebServiceHelper
    {
        string servicebaseurl;
        string mylocalprintingservicename;
        WebAuthentication webAuthentication;



        public WebServiceHelper(string _servicebaseurl, WebAuthentication _webAuthentication, string _mylocalprintingservicename)
        {
            servicebaseurl = _servicebaseurl;
            mylocalprintingservicename = _mylocalprintingservicename;
            webAuthentication = _webAuthentication;
            Initilaized = true;
        }


        public bool Initilaized { get; }
        public string ErrorText { get; set; }

        public bool RegisterInstance()
        {
            ErrorText = "";
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
                PageSettings pagesettings = GetPrinterPageInfo(printername);

                actionUri = new Uri(context.BaseUri, "BCLRSServiceFunctions('')/NAV.AddPrinterServicePrinter_594577777");

                parameres = new BodyOperationParameter[5] { new BodyOperationParameter("localService", mylocalprintingservicename), new BodyOperationParameter("printer", printername), new BodyOperationParameter("landscape", pagesettings.Landscape), new BodyOperationParameter("paperSize", pagesettings.PaperSize.ToString()), new BodyOperationParameter("paperTray", pagesettings.PaperSource.SourceName.ToString()) };
                try
                {
                    retval = context.Execute<bool>(actionUri, "POST", true, parameres);
                }
                catch (Exception eee)
                {
                    ErrorText = ErrorText + "\n" + eee.InnerException.Message;
                }
            }

            //add local folders
            return true;
        }

        private void InjectHeader(object sender, Microsoft.OData.Client.BuildingRequestEventArgs e)
        {
            //e.RequestUri = new Uri(e.RequestUri.ToString().Replace("V4/", "V4/Company('CRONUS%20UK%20Ltd.')/"));
            // e.RequestUri = new Uri(e.RequestUri.ToString()+ "?$filter=No eq '51600'");
            if (webAuthentication.serverauthentication == AuthType.Basic)
                e.Headers.Add("Authorization", "Basic " + webAuthentication.basicauthheader);
            else
                e.Headers.Add("Authorization", "Bearer " + webAuthentication.GetBearer());
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

        public List<WebCommand> GetWebCommands()
        {
            ErrorText = "";
            List<WebCommand> commands = new List<WebCommand>();

            var serviceRoot = servicebaseurl;
            var context = new BC.NAV(new Uri(serviceRoot));
            context.BuildingRequest += InjectHeader;

            try
            {
                var BCLRSEntries = context.BCLRSEntryList.AddQueryOption("$filter", "entryType eq 'Command to Client' and localServiceCode eq '" + mylocalprintingservicename + "' and entryCompleted eq false").Execute();

                foreach (var command in BCLRSEntries)
                {
                    var context2 = new BC.NAV(new Uri(serviceRoot));
                    context2.BuildingRequest += InjectHeader;

                    Uri actionUri = new Uri(context.BaseUri, "BCLRSServiceFunctions('')/NAV.GetEntryFile");
                    BodyOperationParameter[] parameres = { new BodyOperationParameter("documentGUID", command.EntryGUID) };
                    string base64result = context.Execute<string>(actionUri, "POST", true, parameres).First();
                    string commandtext = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(base64result));
                    commands.Add(new WebCommand(command.EntryGUID, commandtext, false));
                }

            }
            catch (Exception eee)
            {

                ErrorText = eee.InnerException.Message;
            }

            return commands;
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
                    bool upload = (document.ResourceType == "File Queue From Client");
                    bool enabled = Convert.ToBoolean(document.Enabled);
                    FileUploadAction fileUploadAction = FileUploadAction.KeepFile;

                    if (document.AfterImportFileAction.Contains("Delete Source")) fileUploadAction = FileUploadAction.DeleteFile;
                    if (document.AfterImportFileAction.Contains("Move to Archive")) fileUploadAction = FileUploadAction.ArchiveFile;

                    folders.Add(new BCFolder(document.No, document.LocalResourceCode, upload, enabled, fileUploadAction));
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

        public List<WebDocument> GetDocumentsForDownload(string localResourceFilter)
        {
            ErrorText = "";
            List<WebDocument> documents = new List<WebDocument>();

            var serviceRoot = servicebaseurl;
            var context = new BC.NAV(new Uri(serviceRoot));
            context.BuildingRequest += InjectHeader;

            try
            {
                var BCLRSEntries = context.BCLRSEntryList.AddQueryOption("$filter", "entryType eq 'File To Client' and localServiceCode eq '" + mylocalprintingservicename + "' and entryCompleted eq false and documentName ne '' and localResourceCode eq '" + localResourceFilter + "'").Execute();

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

        public void UploadSettingstoBC(List<WebSetting> websettings)
        {
            ErrorText = "";
            var serviceRoot = servicebaseurl;
            var context = new BC.NAV(new Uri(serviceRoot));
            context.BuildingRequest += InjectHeader;

            try
            {
                foreach (var websetting in websettings)
                {
                    var BCSettingLine = BC.BCLRSSetupLinesAPI.CreateBCLRSSetupLinesAPI(mylocalprintingservicename, websetting.Key);
                    BCSettingLine.SetupValue = websetting.Value;
                    if (websetting.Key.Contains("ApiKey"))
                        BCSettingLine.IsSensitive = true;
                    context.AddToBCLRSSetupLinesAPI(BCSettingLine);
                    context.SaveChanges(SaveChangesOptions.ContinueOnError);
                }
            }
            catch (Exception eee)
            {
                ErrorText = eee.InnerException.Message;
            }
        }
    }
}
