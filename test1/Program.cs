using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using System.Reflection;
using iTextSharp.text.pdf.parser;
using System.Globalization;
using System.Drawing.Imaging;

namespace test1
{
    class Program
    {
        static void Main(string[] args)
        {
            //test3();

            ExtractImagesFromPDF(@"C:\test\39.pdf", @"C:\test\");
        }

        static void ExtractImagesFromPDF(string sourcePdf, string outputPath)
        {
            // NOTE:  This will only get the first image it finds per page.
            PdfReader pdf = new PdfReader(sourcePdf);
            RandomAccessFileOrArray raf = new iTextSharp.text.pdf.RandomAccessFileOrArray(sourcePdf);

            try
            {
                for (int pageNumber = 1; pageNumber <= pdf.NumberOfPages; pageNumber++)
                {
                    PdfDictionary pg = pdf.GetPageN(pageNumber);

                    // recursively search pages, forms and groups for images.
                    PdfObject obj = FindImageInPDFDictionary(pg);
                    if (obj != null)
                    {

                        int XrefIndex = Convert.ToInt32(((PRIndirectReference)obj).Number.ToString(System.Globalization.CultureInfo.InvariantCulture));
                        PdfObject pdfObj = pdf.GetPdfObject(XrefIndex);
                        PdfStream pdfStrem = (PdfStream)pdfObj;
                        byte[] bytes = PdfReader.GetStreamBytesRaw((PRStream)pdfStrem);
                        if ((bytes != null))
                        {
                            string path = Path.Combine(outputPath, String.Format(@"{0}.jpg", pageNumber));

                            using (System.IO.MemoryStream memStream = new System.IO.MemoryStream(bytes))
                            {
                                memStream.Position = 0;

                                var img = System.Drawing.Image.FromStream(memStream, true);
                                img.Save(path, ImageFormat.Jpeg);

                                ////System.Drawing.Image img = System.Drawing.Image.FromStream(memStream);
                                ////// must save the file while stream is open.
                                ////if (!Directory.Exists(outputPath))
                                ////    Directory.CreateDirectory(outputPath);


                                //////System.Drawing.Imaging.EncoderParameters parms = new System.Drawing.Imaging.EncoderParameters(1);
                                //////parms.Param[0] = new System.Drawing.Imaging.EncoderParameter(System.Drawing.Imaging.Encoder.Compression, 0);
                                //////System.Drawing.Imaging.ImageCodecInfo jpegEncoder = Utilities.GetImageEncoder("JPEG");
                                //////img.Save(path, jpegEncoder, parms);

                                ////img.Save(path, ImageFormat.Jpeg);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ;
            }
            finally
            {
                pdf.Close();
                raf.Close();
            }
        }

        static List<PdfObject> list = new List<PdfObject>() { };
        private static PdfObject FindImageInPDFDictionary(PdfDictionary pg)
        {
            PdfDictionary res = (PdfDictionary)PdfReader.GetPdfObject(pg.Get(PdfName.RESOURCES));
            PdfDictionary xobj = (PdfDictionary)PdfReader.GetPdfObject(res.Get(PdfName.XOBJECT));
            if (xobj != null)
            {
                foreach (PdfName name in xobj.Keys)
                {
                    PdfObject obj = xobj.Get(name);
                    if (obj.IsIndirect())
                    {
                        PdfDictionary tg = (PdfDictionary)PdfReader.GetPdfObject(obj);
                        PdfName type = (PdfName)PdfReader.GetPdfObject(tg.Get(PdfName.SUBTYPE));

                        //image at the root of the pdf
                        if (PdfName.IMAGE.Equals(type))
                        {
                            if (obj.Length > 1000)
                                return obj;

                            return null;
                        }// image inside a form
                        else if (PdfName.FORM.Equals(type))
                        {
                            return FindImageInPDFDictionary(tg);
                        } //image inside a group
                        else if (PdfName.GROUP.Equals(type))
                        {
                            return FindImageInPDFDictionary(tg);
                        }
                    }
                }
            }

            return null;
        }

        //public IEnumerable<System.Drawing.Image> ExtractImagesFromPDF(string sourcePdf)
        //{
        //    // NOTE:  This will only get the first image it finds per page.
        //    var pdf = new PdfReader(sourcePdf);
        //    var raf = new RandomAccessFileOrArray(sourcePdf);

        //    try
        //    {
        //        for (int pageNum = 1; pageNum <= pdf.NumberOfPages; pageNum++)
        //        {
        //            PdfDictionary pg = pdf.GetPageN(pageNum);

        //            ////// recursively search pages, forms and groups for images.
        //            ////PdfObject obj = ExtractImagesFromPDF_FindImageInPDFDictionary(pg);
        //            ////if (obj != null)
        //            ////{
        //            ////    int XrefIndex = Convert.ToInt32(((PRIndirectReference)obj).Number.ToString(CultureInfo.InvariantCulture));
        //            ////    PdfObject pdfObj = pdf.GetPdfObject(XrefIndex);
        //            ////    PdfStream pdfStrem = (PdfStream)pdfObj;
        //            ////    PdfImageObject pdfImage = new PdfImageObject((PRStream)pdfStrem);
        //            ////    System.Drawing.Image img = pdfImage.GetDrawingImage();
        //            ////    yield return img;
        //            ////}
        //        }
        //    }
        //    finally
        //    {
        //        pdf.Close();
        //        raf.Close();
        //    }
        //}

        static void test3()
        {
            string filepath = @"C:\test\SpeakOut.pdf";

            iTextSharp.text.pdf.PdfReader reader = null;
            int currentPage = 1;
            int pageCount = 0;
            //string filepath_New = filepath + "\\PDFDestination\\";

            System.Text.UTF8Encoding encoding = new System.Text.UTF8Encoding();
            //byte[] arrayofPassword = encoding.GetBytes(ExistingFilePassword);
            reader = new iTextSharp.text.pdf.PdfReader(filepath);
            reader.RemoveUnusedObjects();
            pageCount = reader.NumberOfPages;
            string ext = System.IO.Path.GetExtension(filepath);
            for (int i = 1; i <= pageCount; i++)
            {
                iTextSharp.text.pdf.PdfReader reader1 = new iTextSharp.text.pdf.PdfReader(filepath);
                string outfile = filepath.Replace((System.IO.Path.GetFileName(filepath)), (System.IO.Path.GetFileName(filepath).Replace(".pdf", "") + "_" + i.ToString()) + ext);
                reader1.RemoveUnusedObjects();
                iTextSharp.text.Document doc = new iTextSharp.text.Document(reader.GetPageSizeWithRotation(currentPage));
                iTextSharp.text.pdf.PdfCopy pdfCpy = new iTextSharp.text.pdf.PdfCopy(doc, new System.IO.FileStream(outfile, System.IO.FileMode.Create));
                doc.Open();
                for (int j = 1; j <= 1; j++)
                {
                    iTextSharp.text.pdf.PdfImportedPage page = pdfCpy.GetImportedPage(reader1, currentPage);
                    pdfCpy.SetFullCompression();
                    pdfCpy.AddPage(page);
                    currentPage += 1;
                }
                doc.Close();
                pdfCpy.Close();
                reader1.Close();
                reader.Close();

            }
        }

        static void test_2()
        {
            byte[] pdf; // result will be here

            var cssText = File.ReadAllText(@"pdf\asset\PawnInsurrance\PawnInsurrance.css");
            var html = File.ReadAllText(@"pdf\giay-yeu-cau-bao-hiem.html");

            using (var memoryStream = new MemoryStream())
            {
                var document = new Document(PageSize.A4, 50, 50, 60, 60);
                var writer = PdfWriter.GetInstance(document, memoryStream);
                document.Open();

                using (var cssMemoryStream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(cssText)))
                {
                    using (var htmlMemoryStream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(html)))
                    {
                        //XMLWorkerHelper.GetInstance().ParseXHtml(writer, document, htmlMemoryStream, cssMemoryStream);
                    }
                }

                document.Close();

                pdf = memoryStream.ToArray();
            }
        }

        static void test_1()
        {
            //Create our document object
            Document Doc = new Document(PageSize.LETTER);

            string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            //Create our file stream
            using (FileStream fs = new FileStream(Path.Combine(path, "arialuni.pdf"), FileMode.Create, FileAccess.Write, FileShare.Read))
            {
                //Bind PDF writer to document and stream
                PdfWriter writer = PdfWriter.GetInstance(Doc, fs);

                //Open document for writing
                Doc.Open();

                //Add a page
                Doc.NewPage();

                //Full path to the Unicode Arial file
                //string ARIALUNI_TFF = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "ARIALUNI.TTF");
                //string ARIALUNI_TFF = Path.Combine(path, "arialunicodems.ttf");
                string ARIALUNI_TFF = Path.Combine(path, "arialuni.ttf");

                //Create a base font object making sure to specify IDENTITY-H
                BaseFont bf = BaseFont.CreateFont(ARIALUNI_TFF, BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);

                //Create a specific font object
                Font f = new Font(bf, 12, Font.NORMAL);

                //Write some text, the last character is 0x0278 - LATIN SMALL LETTER PHI
                Doc.Add(new Phrase("Animal 4D+ là ứng dụng cho phép tạo ra mô hình 3D của các loài động vật ngay trên màn hình smartphone, với các hoạt động, cử chỉ y như thật. Đặc biệt ứng dụng sẽ đọc to tên gọi của các loài động vật bằng tiếng Anh, để giúp trẻ có thể nghe rõ và học theo. Animal 4D+ không chỉ giúp trẻ em khám phá được thông tin về các loài động vật, mà còn giúp tạo được hứng thú trong việc học và tiếp cận tiếng Anh", f));

                //Write some more text, the last character is 0x0682 - ARABIC LETTER HAH WITH TWO DOTS VERTICAL ABOVE
                Doc.Add(new Phrase("Nguyễn Văn Thịnh", f));

                //Close the PDF
                Doc.Close();
            }
        }
    }
}