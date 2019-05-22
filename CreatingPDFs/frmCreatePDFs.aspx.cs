// Micke Blomquist, 2011-03-11
// Downloaded from http://www.c-sharpcorner.com
// www.mbase.se
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

// Make sure these are added if you copy the code to another solution
// and that you reference the iTextSharp dll by rightclicking the reference folder
// and use Add reference
using System.IO;
using System.Data;
// Namspaces and classes from iTextSharp
using iTextSharp;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace CreatingPDFs
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        BaseFont f_cb = BaseFont.CreateFont("c:\\windows\\fonts\\calibrib.ttf", BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
        BaseFont f_cn = BaseFont.CreateFont("c:\\windows\\fonts\\calibri.ttf", BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
        

        protected void btnFirstPDF_Click(object sender, EventArgs e)
        {
            try
            {
                using (System.IO.FileStream fs = new FileStream(Server.MapPath("pdf") + "\\" + "First PDF document.pdf", FileMode.Create))
                {
                    Document document = new Document(PageSize.A4, 25, 25, 30, 30);
                    PdfWriter writer = PdfWriter.GetInstance(document, fs);

                    // Add meta information to the document
                    document.AddAuthor("Nguyễn Văn Thịnh");
                    //document.AddCreator("Animal 4D+ là ứng dụng cho phép tạo ra mô hình 3D của các loài động vật ngay trên màn hình smartphone, với các hoạt động, cử chỉ y như thật. Đặc biệt ứng dụng sẽ đọc to tên gọi của các loài động vật bằng tiếng Anh, để giúp trẻ có thể nghe rõ và học theo. Animal 4D+ không chỉ giúp trẻ em khám phá được thông tin về các loài động vật, mà còn giúp tạo được hứng thú trong việc học và tiếp cận tiếng Anh");
                    document.AddKeywords("PDF tutorial education");
                    document.AddSubject("Document subject - Describing the steps creating a PDF document");
                    document.AddTitle("The document title - PDF creation using iTextSharp");

                    // Open the document to enable you to write to the document
                    document.Open();

                    // Add a simple and wellknown phrase to the document
                    //for (int x = 0; x != 100; x++)
                    //{
                    document.Add(new Paragraph("Animal 4D+ là ứng dụng cho phép tạo ra mô hình 3D của các loài động vật ngay trên màn hình smartphone, với các hoạt động, cử chỉ y như thật. Đặc biệt ứng dụng sẽ đọc to tên gọi của các loài động vật bằng tiếng Anh, để giúp trẻ có thể nghe rõ và học theo. Animal 4D+ không chỉ giúp trẻ em khám phá được thông tin về các loài động vật, mà còn giúp tạo được hứng thú trong việc học và tiếp cận tiếng Anh"));
                    //}

                    // Close the document
                    document.Close();
                    writer.Close();
                    fs.Close();

                    lblMsg.Text = "Document saved to the pdf folder.";
                }
            }
            catch
            {
            }
        }

        protected void btnFirstPDF_ResponseOutput_Click(object sender, EventArgs e)
        {
            try
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    // Creae the document object, assigning the page margins
                    Document document = new Document(PageSize.A4, 25, 25, 30, 30);
                    PdfWriter writer = PdfWriter.GetInstance(document, ms);
                    // Open the document, enabeling writing to the document
                    document.Open();
                    document.Add(new Paragraph("Hello World"));
                    document.Close();
                    writer.Close();
                    ms.Close();
                    Response.ContentType = "pdf/application";
                    Response.AddHeader("content-disposition", "attachment;filename=First PDF document.pdf");
                    Response.OutputStream.Write(ms.GetBuffer(), 0, ms.GetBuffer().Length);
                }
            }
            catch
            {
            }
        }

        protected void btnPDFusingContentByte_Click(object sender, EventArgs e)
        {
            try
            {
                using (System.IO.FileStream fs = new FileStream(Server.MapPath("pdf") + "\\" + "Using exact positioning - X&Y Schema.pdf", FileMode.Create))
                {
                    Document document = new Document(PageSize.A4, 25, 25, 30, 30);
                    PdfWriter writer = PdfWriter.GetInstance(document, fs);

                    // Open the document to enable you to write to the document
                    document.Open();

                    PdfContentByte cb = writer.DirectContent;
                    cb.SetFontAndSize(f_cn, 9);
                    // First we must activate writing
                    cb.BeginText();

                    // Let's create a simple example over the positioing location using two loops,
                    // one vertical and one horizontal, displaying position X&Y value
                    int row = 1;
                    for (int y = 0; y != 70; y++)
                    {
                        cb.SetTextMatrix(10, row);
                        cb.ShowText("Y: " + row.ToString());
                        row += 12;
                    }
                    int col = 35;
                    for (int x = 0; x != 22; x++)
                    {
                        cb.SetTextMatrix(col, 829);
                        cb.ShowText("X: " + col.ToString());
                        col += 25;
                    }

                    cb.EndText();
                    // Close the document
                    document.Close();
                    writer.Close();
                    fs.Close();

                    lblMsg.Text = "Document saved to the pdf folder.";
                }
            }
            catch
            {
            }
        }

        protected void btnImagesAndGraphics_Click(object sender, EventArgs e)
        {
            try
            {
                using (System.IO.FileStream fs = new FileStream(Server.MapPath("pdf") + "\\" + "PDF with graphics and images.pdf", FileMode.Create))
                {
                    Document document = new Document(PageSize.A4, 25, 25, 30, 30);
                    PdfWriter writer = PdfWriter.GetInstance(document, fs);

                    // Add meta information to the document
                    document.AddAuthor("Mikael Blomquist");
                    document.AddCreator("Sample application using iTestSharp");
                    document.AddKeywords("PDF tutorial education");
                    document.AddSubject("Document subject - Describing the steps creating a PDF document");
                    document.AddTitle("The document title - PDF creation using iTextSharp");

                    // Open the document to enable you to write to the document
                    document.Open();

                    // Makes it possible to add text to a specific place in the document using 
                    // a X & Y placement syntax.
                    PdfContentByte cb = writer.DirectContent;
                    cb.SetFontAndSize(f_cb, 16);
                    // First we must activate writing
                    cb.BeginText();

                    // Write text at a certain position
                    cb.SetTextMatrix(25, 810); // Left, Top
                    cb.ShowText("Hello World");

                    // Change the font and the size
                    cb.SetFontAndSize(f_cn, 9);
                    // Here is to write text that are aligned
                    cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "This text is left aligned", 200, 800, 0);
                    cb.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, "This text is right aligned", 200, 788, 0);

                    cb.EndText();

                    // Add an image to a fixed position from a URL
                    iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance("http://www.c-sharpcorner.com/App_Themes/CSharp/Images/CSSiteLogo.gif");
                    img.SetAbsolutePosition(50, 647);
                    img.ScaleAbsolute(216, 70);
                    cb.AddImage(img);
                    // Move and resize the image
                    img.SetAbsolutePosition(385, 733);
                    img.ScalePercent(50);
                    cb.AddImage(img);
                    // Add an image to a fixed position from disk
                    iTextSharp.text.Image png = iTextSharp.text.Image.GetInstance(Server.MapPath("img.png"));
                    png.SetAbsolutePosition(200, 450);
                    cb.AddImage(png);

                    // Draw a line by setting the line width and position
                    cb.SetLineWidth(0f);
                    cb.MoveTo(30, 650);
                    cb.LineTo(570, 650);
                    cb.Stroke();

                    // Draw a rectangle
                    cb.Rectangle(350, 550, 150, 150);
                    cb.Stroke();

                    // Close the document
                    document.Close();
                    writer.Close();
                    fs.Close();

                    lblMsg.Text = "Document saved to the pdf folder.";
                }
            }
            catch (Exception rror)
            {
                lblMsg.Text = rror.Message;
            }
        }

        protected void btnCreateExampleInvoice_Click(object sender, EventArgs e)
        {
            try
            {
                // Read the sample XML file using the contructor, giving the file path
                Invoice invoice = new Invoice(Server.MapPath("invoices") + "\\invoice_123456.xml");
                // Create references for each of the on-row tables to make it easier to access the values
                // in the table using syntax like this: drHead["invoiceId"].ToString()
                DataRow drHead = invoice.GetInvoiceHeader().Rows[0];
                DataRow drTotal = invoice.GetInvoiceTotal().Rows[0];
                DataRow drPayee = invoice.GetInvoicePayeeInfo().Rows[0];

                using (System.IO.FileStream fs = new FileStream(Server.MapPath("invoices") + "\\" + "Invoice_" + drHead["invoiceId"].ToString() + ".pdf", FileMode.Create))
                {
                    Document document = new Document(PageSize.A4, 25, 25, 30, 1);
                    PdfWriter writer = PdfWriter.GetInstance(document, fs);

                    // Add meta information to the document
                    document.AddAuthor("Mikael Blomquist");
                    document.AddCreator("Sample application using iTestSharp");
                    document.AddKeywords("PDF tutorial education");
                    document.AddSubject("Describing the steps creating a PDF document");
                    document.AddTitle("PDF creation using iTextSharp - Sample invoice");

                    // Open the document to enable you to write to the document
                    document.Open();

                    // Makes it possible to add text to a specific place in the document using 
                    // a X & Y placement syntax.
                    PdfContentByte cb = writer.DirectContent;
                    // Add a footer template to the document
                    cb.AddTemplate(PdfFooter(cb, drPayee), 30, 1);

                    // Add a logo to the invoice
                    iTextSharp.text.Image png = iTextSharp.text.Image.GetInstance(Server.MapPath("mbase_emc2.png"));
                    png.ScaleAbsolute(200, 55);
                    png.SetAbsolutePosition(40, 750);
                    cb.AddImage(png);

                    // First we must activate writing
                    cb.BeginText();

                    

                    // First we write out the header information

                    // Start with the invoice type header
                    writeText(cb, drHead["invoiceType"].ToString(), 350, 820, f_cb, 14);
                    // HEader details; invoice number, invoice date, due date and customer Id
                    writeText(cb, "Invoice No", 350, 800, f_cb, 10);
                    writeText(cb, drHead["invoiceId"].ToString(), 420, 800, f_cn, 10);
                    writeText(cb, "Invoice date", 350, 788, f_cb, 10);
                    writeText(cb, drHead["invoiceDate"].ToString(), 420, 788, f_cn, 10);
                    writeText(cb, "Due date", 350, 776, f_cb, 10);
                    writeText(cb, drHead["dueDate"].ToString(), 420, 776, f_cn, 10);
                    writeText(cb, "Customer", 350, 764, f_cb, 10);
                    writeText(cb, drHead["customerId"].ToString(), 420, 764, f_cn, 10);


                    // Delivery address details
                    int left_margin = 40;
                    int top_margin = 720;
                    writeText(cb, "Delivery address", left_margin, top_margin, f_cb, 10);
                    writeText(cb, drHead["delCustomerName"].ToString(), left_margin, top_margin-12, f_cn, 10);
                    writeText(cb, drHead["delAddress1"].ToString(), left_margin, top_margin-24, f_cn, 10);
                    writeText(cb, drHead["delAddress2"].ToString(), left_margin, top_margin-36, f_cn, 10);
                    writeText(cb, drHead["delAddress3"].ToString(), left_margin, top_margin-48, f_cn, 10);
                    writeText(cb, drHead["delZipcode"].ToString(), left_margin, top_margin-60, f_cn, 10);
                    writeText(cb, drHead["delCity"].ToString() + ", " + drHead["delCountry"].ToString(), left_margin+65, top_margin-60, f_cn, 10);

                    // Invoice address
                    left_margin = 350;
                    writeText(cb, "Invoice address", left_margin, top_margin, f_cb, 10);
                    writeText(cb, drHead["invCustomerName"].ToString(), left_margin, top_margin - 12, f_cn, 10);
                    writeText(cb, drHead["invAddress1"].ToString(), left_margin, top_margin - 24, f_cn, 10);
                    writeText(cb, drHead["invAddress2"].ToString(), left_margin, top_margin - 36, f_cn, 10);
                    writeText(cb, drHead["invAddress3"].ToString(), left_margin, top_margin - 48, f_cn, 10);
                    writeText(cb, drHead["invZipcode"].ToString(), left_margin, top_margin - 60, f_cn, 10);
                    writeText(cb, drHead["invCity"].ToString() + ", " + drHead["invCountry"].ToString(), left_margin + 65, top_margin - 60, f_cn, 10);

                    // Write out invoice terms details
                    left_margin = 40;
                    top_margin = 620;
                    writeText(cb, "Payment terms", left_margin, top_margin, f_cb, 10);
                    writeText(cb, drHead["payTerms"].ToString(), left_margin, top_margin - 12, f_cn, 10);
                    writeText(cb, "Delivery terms", left_margin+200, top_margin, f_cb, 10);
                    writeText(cb, drHead["delTerms"].ToString(), left_margin+200, top_margin - 12, f_cn, 10);
                    writeText(cb, "Transport terms", left_margin+350, top_margin, f_cb, 10);
                    writeText(cb, drHead["delTransportTerms"].ToString(), left_margin + 350, top_margin - 12, f_cn, 10);
                    // Move down
                    left_margin = 40;
                    top_margin = 590;
                    writeText(cb, "Order reference", left_margin, top_margin, f_cb, 10);
                    writeText(cb, drHead["orderReference"].ToString(), left_margin, top_margin - 12, f_cn, 10);
                    writeText(cb, "Customer marking", left_margin + 200, top_margin, f_cb, 10);
                    writeText(cb, drHead["customerMarking"].ToString(), left_margin + 200, top_margin - 12, f_cn, 10);
                    writeText(cb, "Salesman", left_margin + 350, top_margin, f_cb, 10);
                    writeText(cb, drHead["salesman"].ToString(), left_margin + 350, top_margin - 12, f_cn, 10);

                    // NOTE! You need to call the EndText() method before we can write graphics to the document!
                    cb.EndText();
                    // Separate the header from the rows with a line
                    // Draw a line by setting the line width and position
                    cb.SetLineWidth(0f);
                    cb.MoveTo(40, 570);
                    cb.LineTo(560, 570);
                    cb.Stroke();
                    // Don't forget to call the BeginText() method when done doing graphics!
                    cb.BeginText();

                    // Before we write the lines, it's good to assign a "last position to write"
                    // variable to validate against if we need to make a page break while outputting.
                    // Change it to 510 to write to test a page break; the fourth line on a new page
                    int lastwriteposition = 100;

                    // Loop thru the rows in the rows table
                    // Start by writing out the line headers
                    top_margin = 550;
                    left_margin = 40;
                    // Line headers
                    writeText(cb, "Item", left_margin, top_margin, f_cb, 10);
                    writeText(cb, "Description", left_margin+70, top_margin, f_cb, 10);
                    cb.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, "Qty", left_margin + 415, top_margin, 0);
                    writeText(cb, "Unit", left_margin + 420, top_margin, f_cb, 10);
                    cb.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, "Price", left_margin + 495, top_margin, 0);
                    writeText(cb, "Curr", left_margin+500, top_margin, f_cb, 10);

                    // First item line position starts here
                    top_margin = 538;

                    // Loop thru the table of items and set the linespacing to 12 points.
                    // Note that we use the -= operator, the coordinates goes from the bottom of the page!
                    foreach (DataRow drItem in invoice.GetInvoiceRows().Rows)
                    {
                        writeText(cb, drItem["itemId"].ToString(), left_margin, top_margin, f_cn, 10);
                        writeText(cb, drItem["itemDescription"].ToString(), left_margin + 70, top_margin, f_cn, 10);
                        cb.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, drItem["invoicedQuantity"].ToString(), left_margin + 415, top_margin, 0);
                        writeText(cb, drItem["unit"].ToString(), left_margin + 420, top_margin, f_cn, 10);
                        cb.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, drItem["price"].ToString(), left_margin + 495, top_margin, 0);
                        writeText(cb, drItem["currency"].ToString(), left_margin + 500, top_margin, f_cn, 10);

                        // This is the line spacing, if you change the font size, you might want to change this as well.
                        top_margin -= 12;

                        // Implement a page break function, checking if the write position has reached the lastwriteposition
                        if(top_margin <= lastwriteposition)
                        {
                            // We need to end the writing before we change the page
                            cb.EndText();
                            // Make the page break
                            document.NewPage();
                            // Start the writing again
                            cb.BeginText();
                            // Assign the new write location on page two!
                            // Here you might want to implement a new header function for the new page
                            top_margin = 780;
                        }
                    }

                    // Okay, write out the totals table
                    // Here you might want to do some page break scenarios, as well:
                    // Example:
                    // Calculate how many rows you are about to print and see if they fit before the lastwriteposition, 
                    // then decide how to do; write some on first page, then the rest on second page or perhaps all the 
                    // total lines after the page break.
                    // We are not doing this here, we just write them out 80 points below the last writed item row

                    top_margin -= 80;
                    left_margin = 350;

                    // First the headers
                    writeText(cb, "Invoice line totals", left_margin, top_margin, f_cb, 10);
                    writeText(cb, "Freight fee", left_margin, top_margin-12, f_cb, 10);
                    writeText(cb, "VAT amount", left_margin, top_margin-24, f_cb, 10);
                    writeText(cb, "Invoice grand total", left_margin, top_margin-48, f_cb, 10);
                    // Move right to write out the values
                    left_margin = 540;
                    // Write out the invoice currency and values in regular text
                    cb.SetFontAndSize(f_cn, 10);
                    string curr = drTotal["currency"].ToString();
                    cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, curr, left_margin, top_margin, 0);
                    cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, curr, left_margin, top_margin-12, 0);
                    cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, curr, left_margin, top_margin-24, 0);
                    cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, curr, left_margin, top_margin-48, 0);
                    left_margin = 535;
                    cb.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, drTotal["invoicedAmount"].ToString(), left_margin, top_margin, 0);
                    cb.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, drTotal["freightCharge"].ToString(), left_margin, top_margin - 12, 0);
                    cb.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, drTotal["VAT"].ToString(), left_margin, top_margin - 24, 0);
                    cb.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, drTotal["totalAmount"].ToString(), left_margin, top_margin-48, 0);

                    // End the writing of text
                    cb.EndText();

                    // Close the document, the writer and the filestream!
                    document.Close();
                    writer.Close();
                    fs.Close();

                    lblMsg.Text = "Invoiced saved to the invoice folder. Good job!";
                }
            }
            catch(Exception rror)
            {
                lblMsg.Text = rror.Message;
            }
        }

        // This is the method writing text to the content byte
        private void writeText(PdfContentByte cb, string Text, int X, int Y, BaseFont font, int Size)
        {
            cb.SetFontAndSize(font, Size);
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, Text, X, Y, 0);
        }


        private PdfTemplate PdfFooter(PdfContentByte cb, DataRow drFoot)
        {
            // Create the template and assign height
            PdfTemplate tmpFooter = cb.CreateTemplate(580, 70);
            // Move to the bottom left corner of the template
            tmpFooter.MoveTo(1, 1);
            // Place the footer content
            tmpFooter.Stroke();
            // Begin writing the footer
            tmpFooter.BeginText();
            // Set the font and size
            tmpFooter.SetFontAndSize(f_cn, 8);
            // Write out details from the payee table
            tmpFooter.ShowTextAligned(PdfContentByte.ALIGN_LEFT, drFoot["supplier"].ToString(), 0, 53, 0);
            tmpFooter.ShowTextAligned(PdfContentByte.ALIGN_LEFT, drFoot["address1"].ToString(), 0, 45, 0);
            tmpFooter.ShowTextAligned(PdfContentByte.ALIGN_LEFT, drFoot["address2"].ToString(), 0, 37, 0);
            tmpFooter.ShowTextAligned(PdfContentByte.ALIGN_LEFT, drFoot["address3"].ToString(), 0, 29, 0);
            tmpFooter.ShowTextAligned(PdfContentByte.ALIGN_LEFT, drFoot["zip"].ToString() + " " + drFoot["city"].ToString() + " " + drFoot["country"].ToString(), 0, 21, 0);
            // Bold text for ther headers
            tmpFooter.SetFontAndSize(f_cb, 8);
            tmpFooter.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "Phone", 215, 53, 0);
            tmpFooter.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "Mail", 215, 45, 0);
            tmpFooter.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "Web", 215, 37, 0);
            tmpFooter.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "Legal info", 400, 53, 0);
            // Regular text for infomation fields
            tmpFooter.SetFontAndSize(f_cn, 8);
            tmpFooter.ShowTextAligned(PdfContentByte.ALIGN_LEFT, drFoot["phone"].ToString(), 265, 53, 0);
            tmpFooter.ShowTextAligned(PdfContentByte.ALIGN_LEFT, drFoot["mail"].ToString(), 265, 45, 0);
            tmpFooter.ShowTextAligned(PdfContentByte.ALIGN_LEFT, drFoot["web"].ToString(), 265, 37, 0);
            tmpFooter.ShowTextAligned(PdfContentByte.ALIGN_LEFT, drFoot["xtrainfo"].ToString(), 400, 45, 0);
            // End text
            tmpFooter.EndText();
            // Stamp a line above the page footer
            cb.SetLineWidth(0f);
            cb.MoveTo(30, 60);
            cb.LineTo(570, 60);
            cb.Stroke();
            // Return the footer template
            return tmpFooter;
        }


    }
}
