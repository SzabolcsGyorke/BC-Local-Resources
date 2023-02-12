using System;
using System.Diagnostics;
using System.IO;

namespace BCPrint
{
    internal class PdfPrinter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PdfFilePrinter"/> class.
        /// </summary>
        public PdfPrinter()
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="PdfFilePrinter"/> class.
        /// </summary>
        /// <param name="pdfFileName">Name of the PDF file.</param>
        public PdfPrinter(string pdfFileName)
        {
            PdfFileName = pdfFileName;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PdfFilePrinter"/> class.
        /// </summary>
        /// <param name="pdfFileName">Name of the PDF file.</param>
        /// <param name="printerName">Name of the printer.</param>
        public PdfPrinter(string pdfFileName, string printerName)
        {
            _pdfFileName = pdfFileName;
            _printerName = printerName;
        }



        /// <summary>
        /// Gets or sets the name of the PDF file to print.
        /// </summary>
        public string PdfFileName
        {
            get { return _pdfFileName; }
            set { _pdfFileName = value; }
        }
        string _pdfFileName;

        /// <summary>
        /// Gets or sets the name of the printer. 
        /// </summary>
        /// <value>The name of the printer.</value>
        public string PrinterName
        {
            get { return _printerName; }
            set { _printerName = value; }
        }
        string _printerName;

        /// <summary>
        /// Gets or sets the working directory.
        /// </summary>
        public string WorkingDirectory
        {
            get { return _workingDirectory; }
            set { _workingDirectory = value; }
        }
        string _workingDirectory;

        /// <summary>
        /// Prints the PDF file.
        /// </summary>
        public void Print()
        {
            Print(-1);
        }

        /// <summary>
        /// Prints the PDF file.
        /// </summary>
        /// <param name="milliseconds">The number of milliseconds to wait for completing the print job.</param>
        public void Print(int milliseconds)
        {
            if (String.IsNullOrEmpty(_printerName))
                _printerName = _defaultPrinterName;

            if (String.IsNullOrEmpty(_pdfReaderPath))
                throw new InvalidOperationException("No full qualified path to AcroRd32.exe or Acrobat.exe or FoxitPDFReader.exe is set.");

            if (String.IsNullOrEmpty(_printerName))
                throw new InvalidOperationException("No printer name set.");

            // Check whether file exists.
            string fqName;
            if (!string.IsNullOrEmpty(_workingDirectory))
                fqName = Path.Combine(_workingDirectory, _pdfFileName);
            else
                fqName = Path.Combine(Directory.GetCurrentDirectory(), _pdfFileName);
            if (!File.Exists(fqName))
                throw new InvalidOperationException(String.Format("The file {0} does not exist.", fqName));



            // TODO: Check whether printer exists.

            try
            {
                DoSomeVeryDirtyHacksToMakeItWork();

                //acrord32.exe /t          <- seems to work best
                //acrord32.exe /h /p       <- some swear by this version
                //
                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.FileName = _pdfReaderPath;
                string args = String.Format("/t \"{0}\" \"{1}\"", _pdfFileName, _printerName);
                //Debug.WriteLine(args);
                startInfo.Arguments = args;
                startInfo.WindowStyle = ProcessWindowStyle.Hidden;
                startInfo.CreateNoWindow = true;
                startInfo.ErrorDialog = false;
                startInfo.UseShellExecute = false;
                if (!String.IsNullOrEmpty(_workingDirectory))
                    startInfo.WorkingDirectory = _workingDirectory;

                Process process = Process.Start(startInfo);
                if (!process.WaitForExit(milliseconds))
                {
                    // Kill pdf reader
                    process.Kill();
                }
            }
            catch (Exception eee)
            {
                throw eee;
            }
        }

        /// <summary>
        /// For reasons only Adobe knows the Reader seams to open and shows the document instead of printing it
        /// when it was not already running.
        /// </summary>
        void DoSomeVeryDirtyHacksToMakeItWork()
        {
            if (_runningAcro != null)
            {
                if (!_runningAcro.HasExited)
                    return;
                _runningAcro.Dispose();
                _runningAcro = null;
            }
            // Is any PDF Reader instance running?
            Process[] processes = Process.GetProcesses();
            int count = processes.Length;
            for (int idx = 0; idx < count; idx++)
            {
                try
                {
                    Process process = processes[idx];
                    ProcessModule module = process.MainModule;

                    if (String.Compare(Path.GetFileName(module.FileName), Path.GetFileName(_pdfReaderPath), StringComparison.OrdinalIgnoreCase) == 0)
                    {
 
                        _runningAcro = process;
                        break;
                    }
                }
                catch { }
            }
            if (_runningAcro == null)
            {
                //Start a pdfreader session
                _runningAcro = Process.Start(_pdfReaderPath);
                if (_runningAcro != null)
                    _runningAcro.WaitForInputIdle();
            }
        }
        static Process _runningAcro;

        /// <summary>
        /// Gets or sets the PDF reader path.
        /// </summary>
        public string PdfReaderPath
        {
            get { return _pdfReaderPath; }
            set { _pdfReaderPath = value; }
        }
        static string _pdfReaderPath;

        /// <summary>
        /// Gets or sets the name of the default printer. 
        /// </summary>
        static public string DefaultPrinterName
        {
            get { return _defaultPrinterName; }
            set { _defaultPrinterName = value; }
        }
        static string _defaultPrinterName;
    }
}
