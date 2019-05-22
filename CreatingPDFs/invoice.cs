using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace CreatingPDFs
{
    public class Invoice
    {
        public Invoice(string XML_Filepath)
        {
            xml_file = XML_Filepath;
            readXML();
        }

        private string xml_file;
        private DataSet dsInvoice;


        private void readXML()
        {
            try
            {
                dsInvoice = new DataSet();
                dsInvoice.ReadXml(xml_file);
            }
            catch
            {
                throw new Exception("The filepath " + xml_file + " does not exist, please supply a valid path to the XML file");
            }
        }

        public DataTable GetInvoiceHeader()
        {
            try
            {
                return dsInvoice.Tables["invoice_header"];
            }
            catch
            {
                throw new Exception("There are no invoice_header table defined in the XML file " + xml_file );
            }
        }

        public DataTable GetInvoiceRows()
        {
            try
            {
                return dsInvoice.Tables["invoice_rows"];
            }
            catch
            {
                throw new Exception("There are no invoice_rows table defined in the XML file " + xml_file);
            }
        }

        public DataTable GetInvoiceTotal()
        {
            try
            {
                return dsInvoice.Tables["invoice_total"];
            }
            catch
            {
                throw new Exception("There are no invoice_total table defined in the XML file " + xml_file);
            }
        }

        public DataTable GetInvoicePayeeInfo()
        {
            try
            {
                return dsInvoice.Tables["invoice_payeeinfo"];
            }
            catch
            {
                throw new Exception("There are no invoice_payeeinfo table defined in the XML file " + xml_file);
            }
        }

    }
}
