using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using System.Reflection;

namespace test1
{
    class Program
    {
        static void Main(string[] args)
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