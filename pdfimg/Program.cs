using Ghostscript.NET;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pdfimg
{
    class Program
    {
        static void Main(string[] args)
        {
            //test1();
            test2();
        }

        const string gsFile = @"C:\Program Files\gs\gs9.53.3\bin\gswin64.exe";
        static void test1()
        {
            string inputPDFFile = @"C:\test\SpeakOut.pdf";
            string outputImagesPath = @"C:\test\images";

            String ars = "-dNOPAUSE -sDEVICE=jpeg -r102.4 -o" + outputImagesPath + "%d.jpg -sPAPERSIZE=a4 " + inputPDFFile;
            Process proc = new Process();
            proc.StartInfo.FileName = gsFile;
            proc.StartInfo.Arguments = ars;
            proc.StartInfo.CreateNoWindow = true;
            proc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            proc.Start();
            proc.WaitForExit();
        }

        static void test2()
        {
            int PageNumber = 1;
            string inputPDFFile = @"C:\test\SpeakOut.pdf";
            string outputImagesPath = @"C:\test\images";

            GhostscriptPngDevice dev = new GhostscriptPngDevice(GhostscriptPngDeviceType.Png256);
            dev.GraphicsAlphaBits = GhostscriptImageDeviceAlphaBits.V_4;
            dev.TextAlphaBits = GhostscriptImageDeviceAlphaBits.V_4;
            dev.ResolutionXY = new GhostscriptImageDeviceResolution(290, 290);
            dev.InputFiles.Add(inputPDFFile);
            dev.Pdf.FirstPage = PageNumber;
            dev.Pdf.LastPage = PageNumber;
            //dev.CustomSwitches.Add("-dDOINTERPOLATE");
            dev.CustomSwitches.Add("-dNOPAUSE");
            dev.OutputPath = outputImagesPath;
            dev.Process();

        }

    }
}
