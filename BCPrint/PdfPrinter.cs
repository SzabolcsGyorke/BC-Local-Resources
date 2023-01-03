using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        /// Gets or sets the name of the printer. A typical name looks like '\\myserver\HP LaserJet PCL5'.
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
                throw new InvalidOperationException("No full qualified path to AcroRd32.exe or Acrobat.exe is set.");

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
                    // Kill Adobe Reader/Acrobat
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
            // Is any Adobe Reader/Acrobat running?
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
                        // Yes: Fine, we can print.
                        _runningAcro = process;
                        break;
                    }
                }
                catch { }
            }
            if (_runningAcro == null)
            {
                // No: Start an Adobe Reader/Acrobat.
                // If you are within ASP.NET, good luck...
                _runningAcro = Process.Start(_pdfReaderPath);
                if (_runningAcro != null)
                    _runningAcro.WaitForInputIdle();
            }
        }
        static Process _runningAcro;

        /// <summary>
        /// Gets or sets the Adobe Reader or Adobe Acrobat path.
        /// A typical name looks like 'C:\Program Files\Adobe\Adobe Reader 7.0\AcroRd32.exe'.
        /// </summary>
        public string PdfReaderPath
        {
            get { return _pdfReaderPath; }
            set { _pdfReaderPath = value; }
        }
        static string _pdfReaderPath;

        /// <summary>
        /// Gets or sets the name of the default printer. A typical name looks like '\\myserver\HP LaserJet PCL5'.
        /// </summary>
        static public string DefaultPrinterName
        {
            get { return _defaultPrinterName; }
            set { _defaultPrinterName = value; }
        }
        static string _defaultPrinterName;
    }
}
