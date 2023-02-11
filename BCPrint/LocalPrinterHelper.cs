using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Collections;
using System.IO;
using RawPrint;
using System.Drawing.Printing;

namespace BCPrint
{
    public class LocalPrinterHelper
    {
        public static ArrayList GetLocalPrinters()
        {
            ArrayList retunarray = new ArrayList();

            foreach (string printerName in System.Drawing.Printing.PrinterSettings.InstalledPrinters)
            {
                retunarray.Add(printerName);
            }


            return retunarray;
        }

        public static PageSettings GetPrinterPageInfo(String printerName)
        {
            PrinterSettings settings;

            // If printer name is not set, look for default printer
            if (String.IsNullOrEmpty(printerName))
            {
                foreach (var printer in PrinterSettings.InstalledPrinters)
                {
                    settings = new PrinterSettings();

                    settings.PrinterName = printer.ToString();

                    if (settings.IsDefaultPrinter)
                        return settings.DefaultPageSettings;
                }

                return null; // <- No default printer  
            }

            // printer by its name 
            settings = new PrinterSettings();

            settings.PrinterName = printerName;
            

            return settings.DefaultPageSettings;
        }

        public static void SendPdfFileToPrinter(string printername, string filenameandpath, string documentname, int copies)
        {
            Printer rawprint = new Printer();
            for (int i = 1; i <= copies; i++)
            {
                if (_useRawPrint)
                    rawprint.PrintRawFile(printername, filenameandpath, documentname, false);
                else
                {
                    PdfPrinter acrobatPrinter = new PdfPrinter(filenameandpath, printername);
                    acrobatPrinter.PdfReaderPath = GetFoxItPath();
                    acrobatPrinter.Print(10000); //10 seconds wait befor close the printer
                }
            }

        }

        private static string GetAcrobatPath()
        {
            string programFiles = Environment.ExpandEnvironmentVariables("%ProgramW6432%");
            string acrobatpath = Path.Combine(programFiles, "Adobe\\Acrobat DC\\Acrobat\\Acrobat.exe");

            return acrobatpath;
        }

        private static string GetFoxItPath()
        {
            string programFiles = Environment.ExpandEnvironmentVariables("%ProgramFiles(x86)%");
            string foxitpath = Path.Combine(programFiles, "Foxit Software\\Foxit PDF Reader\\FoxitPDFReader.exe");

            return foxitpath;
        }

        public static void SendPdfFileToPrinter(string printername, string filenameandpath, string documentname)
        {

            SendPdfFileToPrinter(printername, filenameandpath, documentname, 1);
        }

        public static void SendPdfFileToPrinter(string printername, string filenameandpath)
        {

            String filename = Path.GetFileName(filenameandpath);
            SendPdfFileToPrinter(printername, filenameandpath, filename);

        }

        /// <summary>
        /// Gets or sets the flag UseRawPrint - if true the print will be sent to the printer as raw data.
        /// </summary>
        static public bool UseRawPrint
        {
            get { return _useRawPrint; }
            set { _useRawPrint = value; }
        }
        static bool _useRawPrint;
    }

}
