<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmCreatePDFs.aspx.cs" Inherits="CreatingPDFs._Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
    <style type="text/css">
    body { font-family: Calibri; font-size:small; }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Button ID="btnFirstPDF" runat="server" Text="Create a basic PDF document and save to disk" onclick="btnFirstPDF_Click" /> <br /><br />
        <asp:Button ID="btnFirstPDF_ResponseOutput" runat="server" 
            Text="Create a basic PDF document and respond to user" 
            onclick="btnFirstPDF_ResponseOutput_Click" /> <br /><br />
            <asp:Button ID="btnPDFusingContentByte" runat="server" 
            Text="Write PDFs using exact positioing" 
            onclick="btnPDFusingContentByte_Click" /> <br /><br />
            <asp:Button ID="btnImagesAndGraphics" runat="server" 
            Text="Write PDFs using with graphics and images" onclick="btnImagesAndGraphics_Click" /> <br /><br />
            <asp:Button ID="btnCreateExampleInvoice" runat="server" 
            Text="Create an example PDF invoice from a XML file" 
            onclick="btnCreateExampleInvoice_Click" /> <br /><br />
        
        <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
    </div>
    </form>
</body>
</html>
