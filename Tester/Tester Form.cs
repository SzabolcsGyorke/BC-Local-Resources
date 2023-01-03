﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;
using BCPrint;
using System.IO;
using static BCLRS.FileHelper;
using BCLRS;
using System.Diagnostics;

namespace Tester
{
    public partial class frm_tester : Form
    {
        WebServiceHelper wshelper;
        WebAuthentication webAuthentication;
        List<WebDocument> documents;
        List<BCFolder> folders;
        AuthType authtype = AuthType.Basic;

        public frm_tester()
        {
            InitializeComponent();
        }

        private void btn_getLocalPrinters_Click(object sender, EventArgs e)
        {
            lb_printers.Items.Clear();
            ArrayList localprinters = LocalPrinterHelper.GetLocalPrinters();
            foreach (string printername in localprinters)
            {
                lb_printers.Items.Add(printername);
            }

        }


        private void btn_print_Click(object sender, EventArgs e)
        {
            if (lb_printers.SelectedIndex < 0)
            {
                return;
            }

            if (tb_pdffilename.Text == "")
            {
                return;
            }

            BCPrint.LocalPrinterHelper.SendPdfFileToPrinter(lb_printers.Items[lb_printers.SelectedIndex].ToString(), tb_pdffilename.Text);
        }



        private void frm_tester_Load(object sender, EventArgs e)
        {
            cb_authtype.SelectedIndex = 0;
            tb_baseurl.Text = Properties.Settings.Default.BaseUrl.ToString();
            tb_username.Text = Properties.Settings.Default.UserName.ToString();
            tb_webkey.Text = Properties.Settings.Default.ApiKey.ToString();
            tb_instance.Text = Properties.Settings.Default.Instance.ToString();
            cb_authtype.SelectedIndex = (Properties.Settings.Default.AuthType == AuthType.Basic.ToString()) ? 1 : 2;
            tb_dwfolder1.Text = Properties.Settings.Default.DownloadFolder1.ToString();
            tb_scope.Text = Properties.Settings.Default.Scope;
            tb_authurl.Text = Properties.Settings.Default.AuthUrl.ToString();
            tb_redirecturl.Text = Properties.Settings.Default.RedirectURL.ToString();
            tb_timeinterval.Text = "10";
            cb_folderdirection.SelectedIndex = 0;

            if (cb_authtype.SelectedIndex == 0) authtype = AuthType.Basic;
            else authtype = AuthType.oAuth;


            UpdateConnectionDetails(authtype);

            ArrayList localprinters = LocalPrinterHelper.GetLocalPrinters();
            foreach (string printername in localprinters)
            {
                cb_printer.Items.Add(printername);
                lb_printers.Items.Add(printername);
            }

            if (cb_printer.Items.Count > 0)
            {
                cb_printer.SelectedIndex = 1;
                lb_printers.SelectedIndex = 1;
            }
        }

        private void UpdateConnectionDetails(AuthType authtype)
        {
            if (authtype == AuthType.oAuth)
            {
                btn_tokeninfo.Visible = true;
                lbl_user.Text = "Client ID";
                lbl_password.Text = "Client Secret";
                lbl_authurl.Visible = true;
                tb_authurl.Visible = true;
                lbl_redirecturl.Visible = true;
                tb_redirecturl.Visible = true;
                tb_scope.Visible = true;
                lbl_scope.Visible = true;
            }

            if (authtype == AuthType.Basic)
            {
                btn_tokeninfo.Visible = false;
                lbl_user.Text = "Username";
                lbl_password.Text = "Password";
                lbl_authurl.Visible = false;
                tb_authurl.Visible = false;
                lbl_redirecturl.Visible = false;
                tb_redirecturl.Visible = false;
                tb_scope.Visible = false;
                lbl_scope.Visible = false;
            }
        }

        private void btn_getdocuments_Click(object sender, EventArgs e)
        {
            InitAuthentication();

            documents = wshelper.GetDocumentsForPrint();

            lst_documents.Items.Clear();
            if (documents.Count > 0)
            {
                foreach (WebDocument doc in documents)
                {
                    lst_documents.Items.Add(new ListViewItem(new[] { doc.DocumentGUID.ToString(), doc.DocumentName, doc.PrinterName, doc.Copies.ToString(), doc.UseRaw.ToString() }));
                }
            }
            else
            {
                if (wshelper.ErrorText != "")
                {
                    MessageBox.Show(wshelper.ErrorText, "Error During WS Query...", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show("Nothing to print!", "BC Print", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            //frm_tester.ActiveForm.UseWaitCursor = false;
        }

        private void btn_openfilelist_Click_1(object sender, EventArgs e)
        {
            OpenFileDialog openfiledlg = new OpenFileDialog();
            if (openfiledlg.ShowDialog() == DialogResult.OK)
            {
                tb_pdffilename.Text = openfiledlg.FileName;
            }
        }

        private void btn_printdoc_Click(object sender, EventArgs e)
        {
            // lst_documents.Items[lst_documents.se]

            foreach (ListViewItem selecteddoc in lst_documents.SelectedItems)
            {
                string tempFile = Path.GetTempFileName();
                wshelper.GetBCDocument(Guid.Parse(selecteddoc.SubItems[0].Text), tempFile);
                LocalPrinterHelper.SendPdfFileToPrinter(cb_printer.Text, tempFile, selecteddoc.SubItems[1].Text);
                wshelper.SetBCDocumentComplete(Guid.Parse(selecteddoc.SubItems[0].Text));
            }
        }

        private void InitAuthentication()
        {
            if (webAuthentication == null)
            {
                webAuthentication = new WebAuthentication(authtype, tb_username.Text, tb_webkey.Text, tb_username.Text, tb_webkey.Text, tb_authurl.Text, tb_redirecturl.Text, tb_scope.Text);
            }
            if (wshelper == null)
            {
                wshelper = new WebServiceHelper(tb_baseurl.Text, webAuthentication, tb_instance.Text);
            }
        }

        private void btn_registerinstance_Click(object sender, EventArgs e)
        {
            InitAuthentication();

            wshelper.RegisterInstance();
        }

        private void btn_updateheartbeat_Click(object sender, EventArgs e)
        {
            InitAuthentication();
            wshelper.UpdateBCHeartbeat();
        }

        private void btn_savefile_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem selecteddoc in lst_documents.SelectedItems)
            {
                //string tempFile =  Path.  //Path.GetTempFileName();
                SaveFileDialog savefiledlg = new SaveFileDialog();
                savefiledlg.FileName = selecteddoc.SubItems[1].Text + ".pdf";
                if (savefiledlg.ShowDialog() == DialogResult.OK)
                {
                    string tempFile = savefiledlg.FileName;
                    if (wshelper.GetBCDocument(Guid.Parse(selecteddoc.SubItems[0].Text), tempFile) == false)
                        MessageBox.Show(wshelper.ErrorText);

                }
            }
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void tb_baseurl_TextChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.BaseUrl = tb_baseurl.Text;
            Properties.Settings.Default.Save();
        }



        private void tb_webkey_TextChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.ApiKey = tb_webkey.Text;
            Properties.Settings.Default.Save();
        }

        private void tb_username_TextChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.UserName = tb_username.Text;
            Properties.Settings.Default.Save();
        }

        private void TPage_File_Click(object sender, EventArgs e)
        {

        }

        private void btn_updatefolders_Click(object sender, EventArgs e)
        {
            InitAuthentication();

            wshelper.RegisterFolder(tb_dwfolder1.Text, 1);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            InitAuthentication();
            BCLRS.FileHelper filehelper = new FileHelper(wshelper);
            filehelper.SyncBCFolders();

        }



        private void btn_tokeninfo_Click(object sender, EventArgs e)
        {
            MessageBox.Show(webAuthentication.GetTokenInfo(), "Token", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void tokenInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Token info");
        }

        private void lst_documents_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lst_documents.SelectedItems.Count > 0)
            {
                ListViewItem selecteddoc = lst_documents.SelectedItems[0];
                cb_printer.SelectedIndex = cb_printer.FindString(selecteddoc.SubItems[2].Text);
            }
        }

        private void tb_dwfolder1_TextChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.DownloadFolder1 = tb_dwfolder1.Text;
            Properties.Settings.Default.Save();
        }


        private void btn_getFolders_Click(object sender, EventArgs e)
        {
            InitAuthentication();

            folders = wshelper.GetBCFolders();

            lst_folders.Items.Clear();
            if (folders.Count > 0)
            {
                foreach (BCFolder folder in folders)
                {
                    lst_folders.Items.Add(new ListViewItem(new[] { folder.Folder, folder.Upload.ToString(), folder.Enabled.ToString() }));
                }
            }
            else
            {
                if (wshelper.ErrorText != "")
                {
                    MessageBox.Show(wshelper.ErrorText, "Error During WS Query...", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }

        private void btn_folderbrowse_Click_1(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            DialogResult result = fbd.ShowDialog();

            if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
            {
                tb_dwfolder1.Text = fbd.SelectedPath;
            }
        }

        private void btn_starttimer_Click(object sender, EventArgs e)
        {
            int timeinmiliseconds = Convert.ToInt32(tb_timeinterval.Text) * 1000;
            timer_Job.Interval = timeinmiliseconds;
            timer_Job.Enabled = !timer_Job.Enabled;

            btn_starttimer.BackColor = (timer_Job.Enabled) ? Color.LawnGreen : SystemColors.Control;
            btn_starttimer.Text = (timer_Job.Enabled) ? "Stop" : "Start";
            lbl_remtime.Text = (timer_Job.Enabled) ? "0" : "";
            cb_heartbeat.Checked = timer_Job.Enabled;
        }

        private void timer_Job_Tick(object sender, EventArgs e)
        {
            int numberoftick;

            if (int.TryParse(lbl_remtime.Text, out numberoftick))
                lbl_remtime.Text = (numberoftick + 1).ToString();


            InitAuthentication();
            //update heartbeat
            if (cb_heartbeat.Checked)
            {

                if (wshelper.UpdateBCHeartbeat())
                    lst_timerlog.Items.Add(new ListViewItem(new[] { DateTime.Now.ToString(), "Update Heart Beat", "OK" }));
                else
                    lst_timerlog.Items.Add(new ListViewItem(new[] { DateTime.Now.ToString(), "Update Heart Beat", wshelper.ErrorText }));


            }
            //Syncfiles
            if (cb_timedfilesync.Checked)
            {
                BCLRS.FileHelper filehelper = new FileHelper(wshelper);
                if (filehelper.SyncBCFolders())
                    lst_timerlog.Items.Add(new ListViewItem(new[] { DateTime.Now.ToString(), "Sync folders", "OK" }));
                else
                    lst_timerlog.Items.Add(new ListViewItem(new[] { DateTime.Now.ToString(), "Sync folders", filehelper.ErrorText }));
            }
            //Print
            if (cb_timedprint.Checked)
            {
                documents = wshelper.GetDocumentsForPrint();
                lst_timerlog.Items.Add(new ListViewItem(new[] { DateTime.Now.ToString(), "Document query", wshelper.ErrorText }));
                lst_documents.Items.Clear();
                if (documents.Count > 0)
                {
                    lst_timerlog.Items.Add(new ListViewItem(new[] { DateTime.Now.ToString(), "Document query", String.Format("Document(s) found: {0}", documents.Count) }));
                    foreach (WebDocument doc in documents)
                    {
                        string tempFile = Path.GetTempFileName();
                        if (wshelper.GetBCDocument(doc.DocumentGUID, tempFile))
                        {
                            LocalPrinterHelper.SendPdfFileToPrinter(cb_printer.Text, tempFile, doc.DocumentName);
                            wshelper.SetBCDocumentComplete(doc.DocumentGUID);
                            lst_timerlog.Items.Add(new ListViewItem(new[] { DateTime.Now.ToString(), "Document query", String.Format("Printed: {0}", doc.DocumentName) }));
                        }
                        else
                            lst_timerlog.Items.Add(new ListViewItem(new[] { DateTime.Now.ToString(), "Document query", wshelper.ErrorText }));
                    }
                }
                else
                {
                    if (wshelper.ErrorText != "")
                    {
                        lst_timerlog.Items.Add(new ListViewItem(new[] { DateTime.Now.ToString(), "Printing", wshelper.ErrorText }));
                    }
                }
            }
            //command
            if (cb_command.Checked)
            {
                List<WebCommand> commands = wshelper.GetWebCommands();

                lst_commands.Items.Clear();
                if (commands.Count > 0)
                {
                    foreach (WebCommand command in commands)
                    {
                        ExecuteCommand(command.Command);
                        wshelper.SetBCDocumentComplete(command.CommandGUID);
                        lst_timerlog.Items.Add(new ListViewItem(new[] { DateTime.Now.ToString(), "Command", command.Command }));
                    }
                }
            }
        }

        private void btn_markascompleted_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem selecteddoc in lst_documents.SelectedItems)
            {
                wshelper.SetBCDocumentComplete(Guid.Parse(selecteddoc.SubItems[0].Text));
            }
        }

        private void tb_authurl_TextChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.AuthUrl = tb_authurl.Text;
            Properties.Settings.Default.Save();
        }

        private void tb_redirecturl_TextChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.RedirectURL = tb_redirecturl.Text;
            Properties.Settings.Default.Save();
        }

        private void tb_scope_TextChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.Scope = tb_scope.Text;
            Properties.Settings.Default.Save();
        }

        private void btn_updatefolders_Click_1(object sender, EventArgs e)
        {
            InitAuthentication();

            wshelper.RegisterFolder(tb_dwfolder1.Text, cb_folderdirection.SelectedIndex + 1);
        }

        private void btn_resetauth_Click(object sender, EventArgs e)
        {
            wshelper = null;
            webAuthentication = null;
        }

        private void cb_authtype_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cb_authtype.SelectedIndex == 0)
            {
                authtype = AuthType.Basic;
                Properties.Settings.Default.AuthType = authtype.ToString();
                Properties.Settings.Default.Save();
            }
            else
            {
                authtype = AuthType.oAuth;
                Properties.Settings.Default.AuthType = authtype.ToString();
                Properties.Settings.Default.Save();
            }
            UpdateConnectionDetails(authtype);
        }

        private void btn_getcommands_Click(object sender, EventArgs e)
        {
            InitAuthentication();
            List<WebCommand> commands = wshelper.GetWebCommands();

            lst_commands.Items.Clear();
            if (commands.Count > 0)
            {
                foreach (WebCommand command in commands)
                {
                    lst_commands.Items.Add(new ListViewItem(new[] { command.CommandGUID.ToString(), command.Command, command.Done.ToString() }));
                }
            }
        }

        private void btn_exexcommand_Click(object sender, EventArgs e)
        {
            InitAuthentication();
            foreach (ListViewItem selecteddoc in lst_commands.SelectedItems)
            {
                ExecuteCommand(selecteddoc.SubItems[1].Text);
                wshelper.SetBCDocumentComplete(Guid.Parse(selecteddoc.SubItems[0].Text));
                selecteddoc.SubItems[2].Text = "true";
            }
        }

        private void ExecuteCommand(string _command)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = _command;

            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            startInfo.CreateNoWindow = true;
            startInfo.ErrorDialog = false;
            startInfo.UseShellExecute = false;

            Process process = Process.Start(startInfo);
        }



        private void lst_timerlog_MouseDoubleClick(object sender, MouseEventArgs e)
        {

        }
    }
}
