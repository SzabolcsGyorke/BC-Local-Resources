using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCLRS
{
    public class BCFolder
    {
        public String FolderNO { get; set; }
        public String Folder { get; set; }
        public bool Upload { get; set; }
        public bool Enabled { get; set; }
        public FileHelper.FileUploadAction UploadAction { get; set; }

        public BCFolder(String _FolderNO, String _Folder, bool _Upload, bool _Enabled, FileHelper.FileUploadAction fileUploadAction)
        {
            FolderNO = _FolderNO;
            Folder = _Folder;
            Upload = _Upload;
            Enabled = _Enabled;
            UploadAction = fileUploadAction;
        }
    }
    public class FileHelper
    {
        WebServiceHelper wshelper;
        public string ErrorText { get; set; }
        public FileHelper(WebServiceHelper _wshelper)
        {
            if (_wshelper == null)
            {
                throw new ArgumentNullException();
            }

            wshelper = _wshelper;
        }
        public enum FileUploadAction
        {
            Error,
            KeepFile,
            ArchiveFile,
            DeleteFile
        }

        public bool SyncBCFolders()
        {
            List<BCFolder> folders;
            folders = wshelper.GetBCFolders();
            ErrorText = string.Empty;
            if (folders.Count > 0)
            {
                foreach (BCFolder folder in folders)
                {
                    if (folder.Enabled)
                    {
                        if (folder.Upload)
                        {
                            if (!UploadBCFolder(folder)) { return false; }
                        } else
                        {
                            if (!DownloadBCFolder(folder)) return false;
                        }
                    }
                }
            }


            return true;
        }

        private bool DownloadBCFolder(BCFolder folder)
        {
            List<WebDocument> FilesToDownload = wshelper.GetDocumentsForDownload();
            if (FilesToDownload.Count > 0)
            {
                foreach(WebDocument document in FilesToDownload)
                {
                    document.DocumentName = Path.Combine(folder.Folder, document.DocumentName);
                    if (wshelper.GetBCDocument(document))
                        wshelper.SetBCDocumentComplete(document.DocumentGUID);
                    else
                    {
                        ErrorText = wshelper.ErrorText;
                        return false;
                    }
                }
            }
            return true;
        }

        public bool UploadBCFolder(BCFolder folder)
        {
            DirectoryInfo d = new DirectoryInfo(folder.Folder);
            FileInfo[] Files = d.GetFiles("*.*");
            
            foreach (FileInfo file in Files) {
                var retval = wshelper.UploadFile(file.FullName.ToString());
                if (retval == FileUploadAction.Error)
                {
                    ErrorText = wshelper.ErrorText;
                    return false;
                }

                if (retval == FileUploadAction.DeleteFile) 
                { 
                    File.Delete(file.FullName);
                }

                if (retval == FileUploadAction.ArchiveFile) 
                {
                
                }
            }
            return true;
        }
        
    }
}
