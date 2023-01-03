namespace Tester
{
    partial class frm_tester
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem("x");
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_tester));
            this.tc = new System.Windows.Forms.TabControl();
            this.TPage_Print = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btn_savefile = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.cb_printer = new System.Windows.Forms.ComboBox();
            this.btn_printdoc = new System.Windows.Forms.Button();
            this.lst_documents = new System.Windows.Forms.ListView();
            this.GUID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Document = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Printer = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Copies = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.UseRawPrint = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btn_getdocuments = new System.Windows.Forms.Button();
            this.tpage_local = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.lb_printers = new System.Windows.Forms.ComboBox();
            this.btn_print = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_openfilelist = new System.Windows.Forms.Button();
            this.tb_pdffilename = new System.Windows.Forms.TextBox();
            this.TPage_File = new System.Windows.Forms.TabPage();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.btn_folderbrowse = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.btn_updatefolders = new System.Windows.Forms.Button();
            this.tb_dwfolder1 = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.lst_folders = new System.Windows.Forms.ListView();
            this.Folder = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Upload = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Enabled = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btn_getFolders = new System.Windows.Forms.Button();
            this.btn_uploadfolder1 = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.tb_scope = new System.Windows.Forms.TextBox();
            this.lbl_scope = new System.Windows.Forms.Label();
            this.tb_redirecturl = new System.Windows.Forms.TextBox();
            this.lbl_redirecturl = new System.Windows.Forms.Label();
            this.tb_authurl = new System.Windows.Forms.TextBox();
            this.lbl_authurl = new System.Windows.Forms.Label();
            this.btn_tokeninfo = new System.Windows.Forms.Button();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tokenInfoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.azureDefultsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label5 = new System.Windows.Forms.Label();
            this.rb_oauth = new System.Windows.Forms.RadioButton();
            this.rb_basicauth = new System.Windows.Forms.RadioButton();
            this.btn_updateheartbeat = new System.Windows.Forms.Button();
            this.btn_registerinstance = new System.Windows.Forms.Button();
            this.tb_webkey = new System.Windows.Forms.TextBox();
            this.lbl_password = new System.Windows.Forms.Label();
            this.tb_username = new System.Windows.Forms.TextBox();
            this.lbl_user = new System.Windows.Forms.Label();
            this.tb_baseurl = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tb_instance = new System.Windows.Forms.TextBox();
            this.lbl_instance = new System.Windows.Forms.Label();
            this.btn_markascompleted = new System.Windows.Forms.Button();
            this.TPage_Timer = new System.Windows.Forms.TabPage();
            this.timer_Job = new System.Windows.Forms.Timer(this.components);
            this.tb_timeinterval = new System.Windows.Forms.TextBox();
            this.lbl_timeinterval = new System.Windows.Forms.Label();
            this.btn_starttimer = new System.Windows.Forms.Button();
            this.lbl_remtime = new System.Windows.Forms.Label();
            this.cb_timedprint = new System.Windows.Forms.CheckBox();
            this.cb_timedfilesync = new System.Windows.Forms.CheckBox();
            this.lst_timerlog = new System.Windows.Forms.ListView();
            this.col_datetime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.col_func = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.col_status = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cb_heartbeat = new System.Windows.Forms.CheckBox();
            this.webDocumentBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.tc.SuspendLayout();
            this.TPage_Print.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tpage_local.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.TPage_File.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.TPage_Timer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.webDocumentBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // tc
            // 
            this.tc.Controls.Add(this.TPage_Print);
            this.tc.Controls.Add(this.tpage_local);
            this.tc.Controls.Add(this.TPage_File);
            this.tc.Controls.Add(this.TPage_Timer);
            this.tc.Location = new System.Drawing.Point(18, 404);
            this.tc.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tc.Name = "tc";
            this.tc.SelectedIndex = 0;
            this.tc.Size = new System.Drawing.Size(609, 518);
            this.tc.TabIndex = 8;
            // 
            // TPage_Print
            // 
            this.TPage_Print.Controls.Add(this.groupBox1);
            this.TPage_Print.Location = new System.Drawing.Point(4, 29);
            this.TPage_Print.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.TPage_Print.Name = "TPage_Print";
            this.TPage_Print.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.TPage_Print.Size = new System.Drawing.Size(601, 485);
            this.TPage_Print.TabIndex = 0;
            this.TPage_Print.Text = "Printig";
            this.TPage_Print.UseVisualStyleBackColor = true;
            this.TPage_Print.Click += new System.EventHandler(this.tabPage1_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.groupBox1.Controls.Add(this.btn_markascompleted);
            this.groupBox1.Controls.Add(this.btn_savefile);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.cb_printer);
            this.groupBox1.Controls.Add(this.btn_printdoc);
            this.groupBox1.Controls.Add(this.lst_documents);
            this.groupBox1.Controls.Add(this.btn_getdocuments);
            this.groupBox1.Location = new System.Drawing.Point(9, 9);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox1.Size = new System.Drawing.Size(562, 451);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Printing";
            // 
            // btn_savefile
            // 
            this.btn_savefile.Location = new System.Drawing.Point(366, 305);
            this.btn_savefile.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btn_savefile.Name = "btn_savefile";
            this.btn_savefile.Size = new System.Drawing.Size(177, 35);
            this.btn_savefile.TabIndex = 15;
            this.btn_savefile.Text = "Save File";
            this.btn_savefile.UseVisualStyleBackColor = true;
            this.btn_savefile.Click += new System.EventHandler(this.btn_savefile_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(10, 354);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(55, 20);
            this.label6.TabIndex = 12;
            this.label6.Text = "Printer";
            // 
            // cb_printer
            // 
            this.cb_printer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_printer.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cb_printer.FormattingEnabled = true;
            this.cb_printer.Location = new System.Drawing.Point(140, 349);
            this.cb_printer.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cb_printer.Name = "cb_printer";
            this.cb_printer.Size = new System.Drawing.Size(402, 28);
            this.cb_printer.TabIndex = 11;
            // 
            // btn_printdoc
            // 
            this.btn_printdoc.Location = new System.Drawing.Point(366, 391);
            this.btn_printdoc.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btn_printdoc.Name = "btn_printdoc";
            this.btn_printdoc.Size = new System.Drawing.Size(177, 35);
            this.btn_printdoc.TabIndex = 10;
            this.btn_printdoc.Text = "Print Document";
            this.btn_printdoc.UseVisualStyleBackColor = true;
            this.btn_printdoc.Click += new System.EventHandler(this.btn_printdoc_Click);
            // 
            // lst_documents
            // 
            this.lst_documents.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.GUID,
            this.Document,
            this.Printer,
            this.Copies,
            this.UseRawPrint});
            this.lst_documents.FullRowSelect = true;
            this.lst_documents.HideSelection = false;
            this.lst_documents.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1});
            this.lst_documents.Location = new System.Drawing.Point(15, 76);
            this.lst_documents.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.lst_documents.MultiSelect = false;
            this.lst_documents.Name = "lst_documents";
            this.lst_documents.Size = new System.Drawing.Size(526, 219);
            this.lst_documents.TabIndex = 9;
            this.lst_documents.UseCompatibleStateImageBehavior = false;
            this.lst_documents.View = System.Windows.Forms.View.Details;
            this.lst_documents.SelectedIndexChanged += new System.EventHandler(this.lst_documents_SelectedIndexChanged);
            // 
            // GUID
            // 
            this.GUID.Text = "GUID";
            this.GUID.Width = 95;
            // 
            // Document
            // 
            this.Document.Text = "Document";
            this.Document.Width = 262;
            // 
            // Printer
            // 
            this.Printer.Text = "Printer";
            // 
            // Copies
            // 
            this.Copies.Text = "Copies";
            // 
            // UseRawPrint
            // 
            this.UseRawPrint.Text = "Use Raw Print";
            // 
            // btn_getdocuments
            // 
            this.btn_getdocuments.Location = new System.Drawing.Point(366, 29);
            this.btn_getdocuments.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btn_getdocuments.Name = "btn_getdocuments";
            this.btn_getdocuments.Size = new System.Drawing.Size(177, 37);
            this.btn_getdocuments.TabIndex = 8;
            this.btn_getdocuments.Text = "Get Documents";
            this.btn_getdocuments.UseVisualStyleBackColor = true;
            this.btn_getdocuments.Click += new System.EventHandler(this.btn_getdocuments_Click);
            // 
            // tpage_local
            // 
            this.tpage_local.Controls.Add(this.groupBox2);
            this.tpage_local.Location = new System.Drawing.Point(4, 29);
            this.tpage_local.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tpage_local.Name = "tpage_local";
            this.tpage_local.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tpage_local.Size = new System.Drawing.Size(601, 485);
            this.tpage_local.TabIndex = 1;
            this.tpage_local.Text = "Local functions";
            this.tpage_local.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.lb_printers);
            this.groupBox2.Controls.Add(this.btn_print);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.btn_openfilelist);
            this.groupBox2.Controls.Add(this.tb_pdffilename);
            this.groupBox2.Location = new System.Drawing.Point(9, 9);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox2.Size = new System.Drawing.Size(562, 163);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Local Printing Functions";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(10, 34);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(55, 20);
            this.label7.TabIndex = 14;
            this.label7.Text = "Printer";
            // 
            // lb_printers
            // 
            this.lb_printers.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.lb_printers.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.lb_printers.FormattingEnabled = true;
            this.lb_printers.Location = new System.Drawing.Point(140, 29);
            this.lb_printers.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.lb_printers.Name = "lb_printers";
            this.lb_printers.Size = new System.Drawing.Size(402, 28);
            this.lb_printers.TabIndex = 13;
            // 
            // btn_print
            // 
            this.btn_print.Location = new System.Drawing.Point(366, 111);
            this.btn_print.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btn_print.Name = "btn_print";
            this.btn_print.Size = new System.Drawing.Size(177, 35);
            this.btn_print.TabIndex = 9;
            this.btn_print.Text = "Print";
            this.btn_print.UseVisualStyleBackColor = true;
            this.btn_print.Click += new System.EventHandler(this.btn_print_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 75);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 20);
            this.label1.TabIndex = 8;
            this.label1.Text = "PDF File";
            // 
            // btn_openfilelist
            // 
            this.btn_openfilelist.Location = new System.Drawing.Point(510, 71);
            this.btn_openfilelist.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btn_openfilelist.Name = "btn_openfilelist";
            this.btn_openfilelist.Size = new System.Drawing.Size(33, 31);
            this.btn_openfilelist.TabIndex = 7;
            this.btn_openfilelist.Text = "...";
            this.btn_openfilelist.UseVisualStyleBackColor = true;
            this.btn_openfilelist.Click += new System.EventHandler(this.btn_openfilelist_Click_1);
            // 
            // tb_pdffilename
            // 
            this.tb_pdffilename.Location = new System.Drawing.Point(140, 71);
            this.tb_pdffilename.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tb_pdffilename.Name = "tb_pdffilename";
            this.tb_pdffilename.Size = new System.Drawing.Size(360, 26);
            this.tb_pdffilename.TabIndex = 6;
            // 
            // TPage_File
            // 
            this.TPage_File.Controls.Add(this.groupBox4);
            this.TPage_File.Controls.Add(this.lst_folders);
            this.TPage_File.Controls.Add(this.btn_getFolders);
            this.TPage_File.Controls.Add(this.btn_uploadfolder1);
            this.TPage_File.Location = new System.Drawing.Point(4, 29);
            this.TPage_File.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.TPage_File.Name = "TPage_File";
            this.TPage_File.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.TPage_File.Size = new System.Drawing.Size(601, 485);
            this.TPage_File.TabIndex = 2;
            this.TPage_File.Text = "File Services";
            this.TPage_File.UseVisualStyleBackColor = true;
            this.TPage_File.Click += new System.EventHandler(this.TPage_File_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.btn_folderbrowse);
            this.groupBox4.Controls.Add(this.comboBox1);
            this.groupBox4.Controls.Add(this.btn_updatefolders);
            this.groupBox4.Controls.Add(this.tb_dwfolder1);
            this.groupBox4.Controls.Add(this.label8);
            this.groupBox4.Location = new System.Drawing.Point(8, 356);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(586, 121);
            this.groupBox4.TabIndex = 31;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Register Folder";
            // 
            // btn_folderbrowse
            // 
            this.btn_folderbrowse.FlatAppearance.BorderSize = 0;
            this.btn_folderbrowse.Location = new System.Drawing.Point(380, 27);
            this.btn_folderbrowse.Margin = new System.Windows.Forms.Padding(0);
            this.btn_folderbrowse.Name = "btn_folderbrowse";
            this.btn_folderbrowse.Size = new System.Drawing.Size(34, 28);
            this.btn_folderbrowse.TabIndex = 37;
            this.btn_folderbrowse.Text = "...";
            this.btn_folderbrowse.UseVisualStyleBackColor = true;
            this.btn_folderbrowse.Click += new System.EventHandler(this.btn_folderbrowse_Click_1);
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.Items.AddRange(new object[] {
            "Upload from Client",
            "Download from BC"});
            this.comboBox1.Location = new System.Drawing.Point(423, 27);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(156, 28);
            this.comboBox1.TabIndex = 36;
            // 
            // btn_updatefolders
            // 
            this.btn_updatefolders.Location = new System.Drawing.Point(403, 78);
            this.btn_updatefolders.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btn_updatefolders.Name = "btn_updatefolders";
            this.btn_updatefolders.Size = new System.Drawing.Size(176, 35);
            this.btn_updatefolders.TabIndex = 35;
            this.btn_updatefolders.Text = "Register";
            this.btn_updatefolders.UseVisualStyleBackColor = true;
            // 
            // tb_dwfolder1
            // 
            this.tb_dwfolder1.Location = new System.Drawing.Point(65, 27);
            this.tb_dwfolder1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tb_dwfolder1.Name = "tb_dwfolder1";
            this.tb_dwfolder1.Size = new System.Drawing.Size(317, 26);
            this.tb_dwfolder1.TabIndex = 34;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(3, 30);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(54, 20);
            this.label8.TabIndex = 33;
            this.label8.Text = "Folder";
            // 
            // lst_folders
            // 
            this.lst_folders.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Folder,
            this.Upload,
            this.Enabled});
            this.lst_folders.FullRowSelect = true;
            this.lst_folders.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lst_folders.HideSelection = false;
            this.lst_folders.Location = new System.Drawing.Point(13, 58);
            this.lst_folders.Name = "lst_folders";
            this.lst_folders.Size = new System.Drawing.Size(578, 237);
            this.lst_folders.TabIndex = 30;
            this.lst_folders.UseCompatibleStateImageBehavior = false;
            this.lst_folders.View = System.Windows.Forms.View.Details;
            // 
            // Folder
            // 
            this.Folder.Text = "Folder";
            this.Folder.Width = 281;
            // 
            // Upload
            // 
            this.Upload.Text = "Upload";
            this.Upload.Width = 80;
            // 
            // Enabled
            // 
            this.Enabled.Text = "Enabled";
            this.Enabled.Width = 97;
            // 
            // btn_getFolders
            // 
            this.btn_getFolders.Location = new System.Drawing.Point(414, 10);
            this.btn_getFolders.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btn_getFolders.Name = "btn_getFolders";
            this.btn_getFolders.Size = new System.Drawing.Size(177, 37);
            this.btn_getFolders.TabIndex = 29;
            this.btn_getFolders.Text = "Get BC Folders";
            this.btn_getFolders.UseVisualStyleBackColor = true;
            this.btn_getFolders.Click += new System.EventHandler(this.btn_getFolders_Click);
            // 
            // btn_uploadfolder1
            // 
            this.btn_uploadfolder1.Location = new System.Drawing.Point(414, 303);
            this.btn_uploadfolder1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btn_uploadfolder1.Name = "btn_uploadfolder1";
            this.btn_uploadfolder1.Size = new System.Drawing.Size(177, 35);
            this.btn_uploadfolder1.TabIndex = 28;
            this.btn_uploadfolder1.Text = "Syncronize files";
            this.btn_uploadfolder1.UseVisualStyleBackColor = true;
            this.btn_uploadfolder1.Click += new System.EventHandler(this.button1_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.tb_scope);
            this.groupBox3.Controls.Add(this.lbl_scope);
            this.groupBox3.Controls.Add(this.tb_redirecturl);
            this.groupBox3.Controls.Add(this.lbl_redirecturl);
            this.groupBox3.Controls.Add(this.tb_authurl);
            this.groupBox3.Controls.Add(this.lbl_authurl);
            this.groupBox3.Controls.Add(this.btn_tokeninfo);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.rb_oauth);
            this.groupBox3.Controls.Add(this.rb_basicauth);
            this.groupBox3.Controls.Add(this.btn_updateheartbeat);
            this.groupBox3.Controls.Add(this.btn_registerinstance);
            this.groupBox3.Controls.Add(this.tb_webkey);
            this.groupBox3.Controls.Add(this.lbl_password);
            this.groupBox3.Controls.Add(this.tb_username);
            this.groupBox3.Controls.Add(this.lbl_user);
            this.groupBox3.Controls.Add(this.tb_baseurl);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Location = new System.Drawing.Point(18, 55);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox3.Size = new System.Drawing.Size(603, 339);
            this.groupBox3.TabIndex = 9;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Connection";
            // 
            // tb_scope
            // 
            this.tb_scope.Location = new System.Drawing.Point(160, 237);
            this.tb_scope.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tb_scope.Name = "tb_scope";
            this.tb_scope.Size = new System.Drawing.Size(380, 26);
            this.tb_scope.TabIndex = 37;
            // 
            // lbl_scope
            // 
            this.lbl_scope.AutoSize = true;
            this.lbl_scope.Location = new System.Drawing.Point(12, 240);
            this.lbl_scope.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_scope.Name = "lbl_scope";
            this.lbl_scope.Size = new System.Drawing.Size(55, 20);
            this.lbl_scope.TabIndex = 36;
            this.lbl_scope.Text = "Scope";
            // 
            // tb_redirecturl
            // 
            this.tb_redirecturl.Location = new System.Drawing.Point(160, 201);
            this.tb_redirecturl.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tb_redirecturl.Name = "tb_redirecturl";
            this.tb_redirecturl.Size = new System.Drawing.Size(380, 26);
            this.tb_redirecturl.TabIndex = 35;
            // 
            // lbl_redirecturl
            // 
            this.lbl_redirecturl.AutoSize = true;
            this.lbl_redirecturl.Location = new System.Drawing.Point(12, 204);
            this.lbl_redirecturl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_redirecturl.Name = "lbl_redirecturl";
            this.lbl_redirecturl.Size = new System.Drawing.Size(106, 20);
            this.lbl_redirecturl.TabIndex = 34;
            this.lbl_redirecturl.Text = "Redirect URL";
            // 
            // tb_authurl
            // 
            this.tb_authurl.Location = new System.Drawing.Point(160, 165);
            this.tb_authurl.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tb_authurl.Name = "tb_authurl";
            this.tb_authurl.Size = new System.Drawing.Size(380, 26);
            this.tb_authurl.TabIndex = 33;
            // 
            // lbl_authurl
            // 
            this.lbl_authurl.AutoSize = true;
            this.lbl_authurl.Location = new System.Drawing.Point(12, 168);
            this.lbl_authurl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_authurl.Name = "lbl_authurl";
            this.lbl_authurl.Size = new System.Drawing.Size(140, 20);
            this.lbl_authurl.TabIndex = 32;
            this.lbl_authurl.Text = "Authorization URL";
            // 
            // btn_tokeninfo
            // 
            this.btn_tokeninfo.ContextMenuStrip = this.contextMenuStrip1;
            this.btn_tokeninfo.Location = new System.Drawing.Point(77, 292);
            this.btn_tokeninfo.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btn_tokeninfo.Name = "btn_tokeninfo";
            this.btn_tokeninfo.Size = new System.Drawing.Size(162, 35);
            this.btn_tokeninfo.TabIndex = 31;
            this.btn_tokeninfo.Text = "Token Info";
            this.btn_tokeninfo.UseVisualStyleBackColor = true;
            this.btn_tokeninfo.Click += new System.EventHandler(this.btn_tokeninfo_Click);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tokenInfoToolStripMenuItem,
            this.azureDefultsToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(191, 68);
            // 
            // tokenInfoToolStripMenuItem
            // 
            this.tokenInfoToolStripMenuItem.Name = "tokenInfoToolStripMenuItem";
            this.tokenInfoToolStripMenuItem.Size = new System.Drawing.Size(190, 32);
            this.tokenInfoToolStripMenuItem.Text = "Token Info";
            this.tokenInfoToolStripMenuItem.Click += new System.EventHandler(this.tokenInfoToolStripMenuItem_Click);
            // 
            // azureDefultsToolStripMenuItem
            // 
            this.azureDefultsToolStripMenuItem.Name = "azureDefultsToolStripMenuItem";
            this.azureDefultsToolStripMenuItem.Size = new System.Drawing.Size(190, 32);
            this.azureDefultsToolStripMenuItem.Text = "Azure Defults";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(8, 56);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(112, 20);
            this.label5.TabIndex = 30;
            this.label5.Text = "Authentication";
            // 
            // rb_oauth
            // 
            this.rb_oauth.AutoSize = true;
            this.rb_oauth.Location = new System.Drawing.Point(271, 52);
            this.rb_oauth.Name = "rb_oauth";
            this.rb_oauth.Size = new System.Drawing.Size(77, 24);
            this.rb_oauth.TabIndex = 29;
            this.rb_oauth.TabStop = true;
            this.rb_oauth.Text = "oAuth";
            this.rb_oauth.UseVisualStyleBackColor = true;
            this.rb_oauth.CheckedChanged += new System.EventHandler(this.rb_oauth_CheckedChanged);
            // 
            // rb_basicauth
            // 
            this.rb_basicauth.AutoSize = true;
            this.rb_basicauth.Location = new System.Drawing.Point(160, 53);
            this.rb_basicauth.Name = "rb_basicauth";
            this.rb_basicauth.Size = new System.Drawing.Size(73, 24);
            this.rb_basicauth.TabIndex = 28;
            this.rb_basicauth.TabStop = true;
            this.rb_basicauth.Text = "Basic";
            this.rb_basicauth.UseVisualStyleBackColor = true;
            this.rb_basicauth.CheckedChanged += new System.EventHandler(this.rb_basicauth_CheckedChanged);
            // 
            // btn_updateheartbeat
            // 
            this.btn_updateheartbeat.Location = new System.Drawing.Point(247, 292);
            this.btn_updateheartbeat.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btn_updateheartbeat.Name = "btn_updateheartbeat";
            this.btn_updateheartbeat.Size = new System.Drawing.Size(162, 35);
            this.btn_updateheartbeat.TabIndex = 24;
            this.btn_updateheartbeat.Text = "Update Heartbeat";
            this.btn_updateheartbeat.UseVisualStyleBackColor = true;
            this.btn_updateheartbeat.Click += new System.EventHandler(this.btn_updateheartbeat_Click);
            // 
            // btn_registerinstance
            // 
            this.btn_registerinstance.Location = new System.Drawing.Point(418, 292);
            this.btn_registerinstance.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btn_registerinstance.Name = "btn_registerinstance";
            this.btn_registerinstance.Size = new System.Drawing.Size(177, 37);
            this.btn_registerinstance.TabIndex = 23;
            this.btn_registerinstance.Text = "Register Instance";
            this.btn_registerinstance.UseVisualStyleBackColor = true;
            this.btn_registerinstance.Click += new System.EventHandler(this.btn_registerinstance_Click);
            // 
            // tb_webkey
            // 
            this.tb_webkey.Location = new System.Drawing.Point(160, 125);
            this.tb_webkey.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tb_webkey.Name = "tb_webkey";
            this.tb_webkey.Size = new System.Drawing.Size(380, 26);
            this.tb_webkey.TabIndex = 20;
            this.tb_webkey.TextChanged += new System.EventHandler(this.tb_webkey_TextChanged);
            // 
            // lbl_password
            // 
            this.lbl_password.AutoSize = true;
            this.lbl_password.Location = new System.Drawing.Point(13, 131);
            this.lbl_password.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_password.Name = "lbl_password";
            this.lbl_password.Size = new System.Drawing.Size(78, 20);
            this.lbl_password.TabIndex = 19;
            this.lbl_password.Text = "Password";
            // 
            // tb_username
            // 
            this.tb_username.Location = new System.Drawing.Point(160, 89);
            this.tb_username.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tb_username.Name = "tb_username";
            this.tb_username.Size = new System.Drawing.Size(380, 26);
            this.tb_username.TabIndex = 18;
            this.tb_username.TextChanged += new System.EventHandler(this.tb_username_TextChanged);
            // 
            // lbl_user
            // 
            this.lbl_user.AutoSize = true;
            this.lbl_user.Location = new System.Drawing.Point(13, 95);
            this.lbl_user.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_user.Name = "lbl_user";
            this.lbl_user.Size = new System.Drawing.Size(83, 20);
            this.lbl_user.TabIndex = 17;
            this.lbl_user.Text = "Username";
            // 
            // tb_baseurl
            // 
            this.tb_baseurl.Location = new System.Drawing.Point(138, 19);
            this.tb_baseurl.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tb_baseurl.Name = "tb_baseurl";
            this.tb_baseurl.Size = new System.Drawing.Size(402, 26);
            this.tb_baseurl.TabIndex = 16;
            this.tb_baseurl.TextChanged += new System.EventHandler(this.tb_baseurl_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 24);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(90, 20);
            this.label2.TabIndex = 15;
            this.label2.Text = "oData URL";
            // 
            // tb_instance
            // 
            this.tb_instance.Location = new System.Drawing.Point(156, 12);
            this.tb_instance.Name = "tb_instance";
            this.tb_instance.Size = new System.Drawing.Size(402, 26);
            this.tb_instance.TabIndex = 10;
            // 
            // lbl_instance
            // 
            this.lbl_instance.AutoSize = true;
            this.lbl_instance.Location = new System.Drawing.Point(30, 13);
            this.lbl_instance.Name = "lbl_instance";
            this.lbl_instance.Size = new System.Drawing.Size(71, 20);
            this.lbl_instance.TabIndex = 11;
            this.lbl_instance.Text = "Instance";
            // 
            // btn_markascompleted
            // 
            this.btn_markascompleted.Location = new System.Drawing.Point(15, 303);
            this.btn_markascompleted.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btn_markascompleted.Name = "btn_markascompleted";
            this.btn_markascompleted.Size = new System.Drawing.Size(177, 35);
            this.btn_markascompleted.TabIndex = 16;
            this.btn_markascompleted.Text = "Mark as Completed";
            this.btn_markascompleted.UseVisualStyleBackColor = true;
            this.btn_markascompleted.Click += new System.EventHandler(this.btn_markascompleted_Click);
            // 
            // TPage_Timer
            // 
            this.TPage_Timer.Controls.Add(this.cb_heartbeat);
            this.TPage_Timer.Controls.Add(this.lst_timerlog);
            this.TPage_Timer.Controls.Add(this.cb_timedfilesync);
            this.TPage_Timer.Controls.Add(this.cb_timedprint);
            this.TPage_Timer.Controls.Add(this.lbl_remtime);
            this.TPage_Timer.Controls.Add(this.btn_starttimer);
            this.TPage_Timer.Controls.Add(this.lbl_timeinterval);
            this.TPage_Timer.Controls.Add(this.tb_timeinterval);
            this.TPage_Timer.Location = new System.Drawing.Point(4, 29);
            this.TPage_Timer.Name = "TPage_Timer";
            this.TPage_Timer.Padding = new System.Windows.Forms.Padding(3);
            this.TPage_Timer.Size = new System.Drawing.Size(601, 485);
            this.TPage_Timer.TabIndex = 3;
            this.TPage_Timer.Text = "Timer";
            this.TPage_Timer.UseVisualStyleBackColor = true;
            // 
            // timer_Job
            // 
            this.timer_Job.Tick += new System.EventHandler(this.timer_Job_Tick);
            // 
            // tb_timeinterval
            // 
            this.tb_timeinterval.Location = new System.Drawing.Point(156, 15);
            this.tb_timeinterval.Name = "tb_timeinterval";
            this.tb_timeinterval.Size = new System.Drawing.Size(100, 26);
            this.tb_timeinterval.TabIndex = 0;
            // 
            // lbl_timeinterval
            // 
            this.lbl_timeinterval.AutoSize = true;
            this.lbl_timeinterval.Location = new System.Drawing.Point(12, 18);
            this.lbl_timeinterval.Name = "lbl_timeinterval";
            this.lbl_timeinterval.Size = new System.Drawing.Size(136, 20);
            this.lbl_timeinterval.TabIndex = 1;
            this.lbl_timeinterval.Text = "Time interval (sec)";
            // 
            // btn_starttimer
            // 
            this.btn_starttimer.Location = new System.Drawing.Point(414, 10);
            this.btn_starttimer.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btn_starttimer.Name = "btn_starttimer";
            this.btn_starttimer.Size = new System.Drawing.Size(177, 37);
            this.btn_starttimer.TabIndex = 38;
            this.btn_starttimer.Text = "Start";
            this.btn_starttimer.UseVisualStyleBackColor = false;
            this.btn_starttimer.Click += new System.EventHandler(this.btn_starttimer_Click);
            // 
            // lbl_remtime
            // 
            this.lbl_remtime.AutoSize = true;
            this.lbl_remtime.Location = new System.Drawing.Point(262, 18);
            this.lbl_remtime.Name = "lbl_remtime";
            this.lbl_remtime.Size = new System.Drawing.Size(0, 20);
            this.lbl_remtime.TabIndex = 39;
            // 
            // cb_timedprint
            // 
            this.cb_timedprint.AutoSize = true;
            this.cb_timedprint.Location = new System.Drawing.Point(13, 55);
            this.cb_timedprint.Name = "cb_timedprint";
            this.cb_timedprint.Size = new System.Drawing.Size(88, 24);
            this.cb_timedprint.TabIndex = 40;
            this.cb_timedprint.Text = "Printing";
            this.cb_timedprint.UseVisualStyleBackColor = true;
            // 
            // cb_timedfilesync
            // 
            this.cb_timedfilesync.AutoSize = true;
            this.cb_timedfilesync.Location = new System.Drawing.Point(126, 55);
            this.cb_timedfilesync.Name = "cb_timedfilesync";
            this.cb_timedfilesync.Size = new System.Drawing.Size(103, 24);
            this.cb_timedfilesync.TabIndex = 41;
            this.cb_timedfilesync.Text = "File Sync.";
            this.cb_timedfilesync.UseVisualStyleBackColor = true;
            // 
            // lst_timerlog
            // 
            this.lst_timerlog.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.col_datetime,
            this.col_func,
            this.col_status});
            this.lst_timerlog.FullRowSelect = true;
            this.lst_timerlog.GridLines = true;
            this.lst_timerlog.HideSelection = false;
            this.lst_timerlog.Location = new System.Drawing.Point(12, 85);
            this.lst_timerlog.MultiSelect = false;
            this.lst_timerlog.Name = "lst_timerlog";
            this.lst_timerlog.Size = new System.Drawing.Size(583, 394);
            this.lst_timerlog.Sorting = System.Windows.Forms.SortOrder.Descending;
            this.lst_timerlog.TabIndex = 42;
            this.lst_timerlog.UseCompatibleStateImageBehavior = false;
            this.lst_timerlog.View = System.Windows.Forms.View.Details;
            // 
            // col_datetime
            // 
            this.col_datetime.Text = "DateTime";
            this.col_datetime.Width = 108;
            // 
            // col_func
            // 
            this.col_func.Text = "Function";
            this.col_func.Width = 136;
            // 
            // col_status
            // 
            this.col_status.Text = "Status";
            this.col_status.Width = 350;
            // 
            // cb_heartbeat
            // 
            this.cb_heartbeat.AutoSize = true;
            this.cb_heartbeat.Location = new System.Drawing.Point(235, 55);
            this.cb_heartbeat.Name = "cb_heartbeat";
            this.cb_heartbeat.Size = new System.Drawing.Size(107, 24);
            this.cb_heartbeat.TabIndex = 43;
            this.cb_heartbeat.Text = "Heartbeat";
            this.cb_heartbeat.UseVisualStyleBackColor = true;
            // 
            // webDocumentBindingSource
            // 
            this.webDocumentBindingSource.DataSource = typeof(BCLRS.WebDocument);
            // 
            // frm_tester
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(642, 936);
            this.Controls.Add(this.lbl_instance);
            this.Controls.Add(this.tb_instance);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.tc);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.Name = "frm_tester";
            this.Text = "Test BC Local Resources";
            this.Load += new System.EventHandler(this.frm_tester_Load);
            this.tc.ResumeLayout(false);
            this.TPage_Print.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tpage_local.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.TPage_File.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.TPage_Timer.ResumeLayout(false);
            this.TPage_Timer.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.webDocumentBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tc;
        private System.Windows.Forms.TabPage TPage_Print;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btn_savefile;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cb_printer;
        private System.Windows.Forms.Button btn_printdoc;
        private System.Windows.Forms.ListView lst_documents;
        private System.Windows.Forms.ColumnHeader GUID;
        private System.Windows.Forms.ColumnHeader Document;
        private System.Windows.Forms.ColumnHeader Printer;
        private System.Windows.Forms.ColumnHeader Copies;
        private System.Windows.Forms.Button btn_getdocuments;
        private System.Windows.Forms.TabPage tpage_local;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox lb_printers;
        private System.Windows.Forms.Button btn_print;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_openfilelist;
        private System.Windows.Forms.TextBox tb_pdffilename;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btn_updateheartbeat;
        private System.Windows.Forms.Button btn_registerinstance;
        private System.Windows.Forms.TextBox tb_webkey;
        private System.Windows.Forms.Label lbl_password;
        private System.Windows.Forms.TextBox tb_username;
        private System.Windows.Forms.Label lbl_user;
        private System.Windows.Forms.TextBox tb_baseurl;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TabPage TPage_File;
        private System.Windows.Forms.Button btn_uploadfolder1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.RadioButton rb_oauth;
        private System.Windows.Forms.RadioButton rb_basicauth;
        private System.Windows.Forms.TextBox tb_instance;
        private System.Windows.Forms.Label lbl_instance;
        private System.Windows.Forms.Button btn_tokeninfo;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem tokenInfoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem azureDefultsToolStripMenuItem;
        private System.Windows.Forms.TextBox tb_scope;
        private System.Windows.Forms.Label lbl_scope;
        private System.Windows.Forms.TextBox tb_redirecturl;
        private System.Windows.Forms.Label lbl_redirecturl;
        private System.Windows.Forms.TextBox tb_authurl;
        private System.Windows.Forms.Label lbl_authurl;
        private System.Windows.Forms.ColumnHeader UseRawPrint;
        private System.Windows.Forms.BindingSource webDocumentBindingSource;
        private System.Windows.Forms.ListView lst_folders;
        private System.Windows.Forms.ColumnHeader Folder;
        private System.Windows.Forms.ColumnHeader Upload;
        private System.Windows.Forms.ColumnHeader Enabled;
        private System.Windows.Forms.Button btn_getFolders;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button btn_folderbrowse;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Button btn_updatefolders;
        private System.Windows.Forms.TextBox tb_dwfolder1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btn_markascompleted;
        private System.Windows.Forms.TabPage TPage_Timer;
        private System.Windows.Forms.Button btn_starttimer;
        private System.Windows.Forms.Label lbl_timeinterval;
        private System.Windows.Forms.TextBox tb_timeinterval;
        private System.Windows.Forms.Timer timer_Job;
        private System.Windows.Forms.Label lbl_remtime;
        private System.Windows.Forms.ListView lst_timerlog;
        private System.Windows.Forms.ColumnHeader col_datetime;
        private System.Windows.Forms.ColumnHeader col_func;
        private System.Windows.Forms.ColumnHeader col_status;
        private System.Windows.Forms.CheckBox cb_timedfilesync;
        private System.Windows.Forms.CheckBox cb_timedprint;
        private System.Windows.Forms.CheckBox cb_heartbeat;
    }
}

