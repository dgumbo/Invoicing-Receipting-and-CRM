using System.Linq;
using Microsoft.EntityFrameworkCore;
using invoice_demo_app.Pdf.models;
using invoice_app;
using PdfSharp.Pdf;
using System.Collections.Generic;
using PdfSharp;
using PdfSharp.Drawing;
using System.Text;
using invoice_demo_app.Models;
using System.IO; 
using System.Drawing;
using System.Drawing.Imaging;
/**
*
* @author dgumbo
*/
namespace invoice_demo_app.invoice.service.impl
{
    // Microsoft.AspNetCore.Mvc.api 
    public class InvoiceServiceImpl : InvoiceService
    {
        private readonly AppDbContext appDbContext;

        private int maxRowSize = 23;
        private int maxPageWithSummation = 16;
        private int breakPoint = 12;

        private readonly float POINTS_PER_INCH = 72;
        private float MM_PER_INCH { get { return 1 / (10 * 2.54f) * POINTS_PER_INCH; } }

        private double DOCUMENT_WIDTH;
        private double DOCUMENT_HEIGHT;

        private double DOCUMENT_TOP_MARGIN;
        private double DOCUMENT_RIGHT_MARGIN;
        private double DOCUMENT_BOTTOM_MARGIN;
        private double DOCUMENT_LEFT_MARGIN;
        private double DOCUMENT_PRINTABLE_WIDTH { get { return DOCUMENT_WIDTH - DOCUMENT_RIGHT_MARGIN - DOCUMENT_LEFT_MARGIN; } }
        private double DOCUMENT_PRINTABLE_HEIGHT { get { return DOCUMENT_HEIGHT - DOCUMENT_TOP_MARGIN - DOCUMENT_BOTTOM_MARGIN; } }

        private double COL1_START_X { get { return DOCUMENT_LEFT_MARGIN; } }
        private double COL1_WIDTH { get { return (DOCUMENT_WIDTH - DOCUMENT_LEFT_MARGIN - DOCUMENT_RIGHT_MARGIN) * 7 / 10; } }
        private double COL2_START_X { get { return COL1_START_X + COL1_WIDTH; } }
        private double COL2_WIDTH { get { return (DOCUMENT_WIDTH - DOCUMENT_LEFT_MARGIN - DOCUMENT_RIGHT_MARGIN) * 3 / 10; } }

        private readonly XBrush brush = XBrushes.Black;

        public InvoiceServiceImpl(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
            appDbContext.Invoice
                .Include(i => i.InvoiceLines)
                .Include(i => i.ShipTo)
                .Include(i => i.BillTo)
                .Include("InvoiceLines.Product").ToList();
        }

        public DbSet<Invoice> GetDbSet()
        {
            return this.appDbContext.Invoice;
        }

        public AppDbContext GetDbContext()
        {
            return this.appDbContext;
        }

        public PdfDocument PrintDocument(Invoice invoice)
        {
            DOCUMENT_TOP_MARGIN = 50;
            DOCUMENT_RIGHT_MARGIN = 50;
            DOCUMENT_BOTTOM_MARGIN = 50;
            DOCUMENT_LEFT_MARGIN = 50;

            var pdfDocument = new PdfDocument();

            PdfPage pdfPage = pdfDocument.AddPage();
            XGraphics gfx = XGraphics.FromPdfPage(pdfPage);

            pdfPage.Size = PageSize.A4;
            DOCUMENT_WIDTH = pdfPage.Width.Point;
            DOCUMENT_HEIGHT = pdfPage.Height.Point;

            double nextStartY = DOCUMENT_TOP_MARGIN;

            PrintHeader(gfx, invoice);

            PrintInvoiceCaption(gfx, nextStartY);

            bool hasLogo = true;
            nextStartY = PrintLetterHead(pdfDocument, gfx, nextStartY, hasLogo);

            Address billingAddress = invoice.BillTo;
            nextStartY = PrintBillingData(gfx, nextStartY, billingAddress);

            nextStartY = PrintInvoiceMetadata(gfx, nextStartY, invoice);

            int numPrintedRows = 0;
            int rowsLeft = invoice.InvoiceLines.Count;

            nextStartY = PrintServicesListHeaderRow(gfx, nextStartY);
            nextStartY += 45;

            double totalCost = 0;
            foreach (InvoiceLine invoiceRow in invoice.InvoiceLines)
            {
                numPrintedRows++;
                bool odd = numPrintedRows % 2 == 0;

                nextStartY = PrintInvoiceRow(gfx, nextStartY, invoiceRow, odd);
                totalCost = invoiceRow.addTotal(totalCost);
                if (NewPageRequired(numPrintedRows, rowsLeft))
                {
                    rowsLeft += numPrintedRows;
                    numPrintedRows = 0;
                    maxRowSize = 30;
                    maxPageWithSummation = 23;
                    breakPoint = 18;
                    nextStartY = 660;
                    pdfPage = NewPage(pdfDocument, PageSize.A4, nextStartY, rowsLeft, invoice
                    );
                }
            }

            bool includeVat = false;
            nextStartY = PrintServicesListSummary(gfx, totalCost, nextStartY, includeVat);
            nextStartY = PrintTermsAndConditions(gfx, nextStartY, invoice);
            nextStartY = PrintPaymentDetails(gfx, nextStartY, invoice);
            PrintFooter(gfx);

            return pdfDocument;
        }

        private void PrintHeader(XGraphics printer, Invoice invoice)
        {
            XFont textFont = new XFont("Times", 12, XFontStyle.Regular);
            XSize size = printer.MeasureString(invoice.Number, textFont);

            printer.DrawString(invoice.Number, textFont, XBrushes.DarkSlateGray, new XPoint(DOCUMENT_WIDTH - size.Width - DOCUMENT_RIGHT_MARGIN, DOCUMENT_TOP_MARGIN - size.Height * 2));
        }

        private void PrintInvoiceCaption(XGraphics printer, double iStartY)
        {
            XFont invoiceCaptionFont = new XFont("Times", 18, XFontStyle.Bold);

            string sStr = "Invoice";
            XSize size = printer.MeasureString(sStr, invoiceCaptionFont);

            printer.DrawString(sStr, invoiceCaptionFont, brush, new XPoint(DOCUMENT_WIDTH - size.Width - DOCUMENT_RIGHT_MARGIN, iStartY));
        }

        private double PrintLetterHead(PdfDocument pdfDocument, XGraphics printer, double invoiceStartY, bool bHasLogo)
        {
            double invoiceStartX = bHasLogo ? 165 : DOCUMENT_LEFT_MARGIN;

            XFont titleFont = new XFont("Times", 16, XFontStyle.Bold);
            XBrush brush = XBrushes.Black;
              
            string sCompanyName = "Heritage Innovative Solutions";
            printer.DrawString(sCompanyName, titleFont, brush, new XPoint(invoiceStartX, invoiceStartY));
            invoiceStartY += printer.MeasureString(sCompanyName, titleFont).Height;

            XFont font = new XFont("Times", 11, XFontStyle.Regular);

            printer.DrawString("Stand Number 14657", font, brush, new XPoint(invoiceStartX, invoiceStartY));
            invoiceStartY += printer.MeasureString(sCompanyName, font).Height * 1.05;

            printer.DrawString("Galloway Park, Norton, Zimbabwe", font, brush, new XPoint(invoiceStartX, invoiceStartY));
            invoiceStartY += printer.MeasureString(sCompanyName, font).Height * 1.05;


            Bitmap imgBit = invoice_app.FontAwesome.Instance.GetImage(new invoice_app.FontAwesome.Properties(invoice_app.FontAwesome.Type.Phone) { ForeColor = Color.Blue, Size = 64, BackColor = Color.White });
            MemoryStream streamPhoneBit = new MemoryStream();
            imgBit.Save(streamPhoneBit, ImageFormat.Tiff);

            XImage xImage1 = XImage.FromStream(streamPhoneBit);
            printer.DrawImage(xImage1, new XRect(invoiceStartX , invoiceStartY-7, 9, 9)); 

            printer.DrawString("+263 773 632 856", font, brush, new XPoint(invoiceStartX + 12, invoiceStartY));
            invoiceStartY += printer.MeasureString(sCompanyName, font).Height * 1.05;

            Bitmap imgBit2 = invoice_app.FontAwesome.Instance.GetImage(new invoice_app.FontAwesome.Properties(invoice_app.FontAwesome.Type.Globe ) { ForeColor = Color.Blue, Size = 64, BackColor = Color.White });
            MemoryStream streamPhoneBit2 = new MemoryStream();
            imgBit2.Save(streamPhoneBit2, ImageFormat.Tiff);
             
            XImage xImage2 = XImage.FromStream(streamPhoneBit2);
            printer.DrawImage(xImage2, new XRect(invoiceStartX, invoiceStartY-8, 9, 9));

            printer.DrawString("http://hisolutions.co.zw", font, brush, new XPoint(invoiceStartX + 12, invoiceStartY));

            return invoiceStartY;
        }


        private double PrintBillingData(XGraphics printer, double startY, Address address)
        {
            string sStr = "Bill To";

            XFont headerFont = new XFont("Times", 16, XFontStyle.Bold);
            startY += printer.MeasureString(sStr, headerFont).Height*2;
            printer.DrawString(sStr, headerFont, brush, new XPoint(DOCUMENT_RIGHT_MARGIN, startY));

            XFont font = new XFont("Times", 11, XFontStyle.Regular);
            if (address.hasFullName())
            {
                startY += 14;
                printer.DrawString(address.Fullname, font, brush, new XPoint(DOCUMENT_RIGHT_MARGIN, startY));
            }
            if (address.hasAddress1())
            {
                startY += 14;
                printer.DrawString(address.Address1, font, brush, new XPoint(DOCUMENT_RIGHT_MARGIN, startY));
            }
            if (address.hasAddress2())
            {
                startY += 14;
                printer.DrawString(address.Address2, font, brush, new XPoint(DOCUMENT_RIGHT_MARGIN, startY));
            }
            if (address.hasAddress3())
            {
                startY += 14;
                printer.DrawString(address.Address3, font, brush, new XPoint(DOCUMENT_RIGHT_MARGIN, startY));
            }

            if (address.hasCity())
            {
                startY += 14;
                printer.DrawString(address.City, font, brush, new XPoint(DOCUMENT_RIGHT_MARGIN, startY));
            }

            if (address.hasCountry())
            {
                startY += 14;
                printer.DrawString(address.Country, font, brush, new XPoint(DOCUMENT_RIGHT_MARGIN, startY));
            }

            return startY;
        }

        private double PrintInvoiceRow(XGraphics printer, double startY, InvoiceLine invoiceRow, bool odd)
        {
            int topMargin = 5;
            int rowHeight = 15;

            XColor strokeColor = XColor.FromArgb(100, 100, 100);
            XPen strokePen = new XPen(strokeColor);

            XFont font = new XFont("Times", 10, XFontStyle.Regular);

            List<string> lines = new List<string>();

            string[] returnSplit = invoiceRow.Product.Name.Split("\n");

            List<string> breaks = returnSplit.ToList();

            foreach (string brr in breaks)
            {
                string br = brr;
                StringBuilder sb = new StringBuilder();
                br = br.Replace("\n", " ");
                br = br.Replace("\r", " ");

                foreach (string s in br.Split(" "))
                {
                    XSize size = printer.MeasureString(sb.ToString() + s + " ", font);
                    if (size.Width > COL1_WIDTH - 10)
                    {
                        lines.Add(sb.ToString());
                        sb = new StringBuilder();
                        sb.Append("  ");
                    }
                    sb.Append(s).Append(" ");
                }

                if (sb.Length >= 1)
                {
                    lines.Add(sb.ToString());
                }
            }

            XRect r1 = new XRect(COL1_START_X, startY - 10, COL1_WIDTH, rowHeight * lines.Count + topMargin);
            XRect r2 = new XRect(COL2_START_X, startY - 10, COL2_WIDTH, rowHeight * lines.Count + topMargin);
            if (odd)
                printer.DrawRectangles(strokePen, new XSolidBrush(XColor.FromArgb(230, 230, 230)), new XRect[] { r1, r2 });
            else
                printer.DrawRectangles(strokePen, new XRect[] { r1, r2 });

            startY += topMargin;
            double yPos = startY; // + rowHeight - rowHeight * 2 / 5;
            foreach (string line in lines)
            {
                printer.DrawString(line, font, brush, new XPoint(COL1_START_X + 10, yPos));
                yPos += rowHeight;
            }

            startY += rowHeight * lines.Count;

            yPos = startY - rowHeight * lines.Count * 2 / 5 - 10;
            printer.DrawString(invoiceRow.getTotalstring(), font, brush, new XPoint(COL2_START_X + 10, yPos));

            return startY;
        }

        private double PrintServicesListSummary(XGraphics printer, double totalCost, double summaryStartY, bool includeVat)
        {
            double summaryRowHeight = 0;
            double subTotal = totalCost * 0.8f;
            double vatValue = totalCost * 0.2f;

            XPen strokePen = new XPen(XColor.FromArgb(100, 100, 100));

            XFont font = new XFont("Times", 12, XFontStyle.Bold);
            string sDecimalFormat = "###,###.00";

            XSize size = XSize.Empty;
            if (includeVat)
            {
                // /* Print Subtotal */ 
                printer.DrawRectangle(strokePen, new XRect(COL2_START_X, summaryStartY, COL2_WIDTH, summaryRowHeight));

                size = printer.MeasureString("Sub Total : ", font);
                printer.DrawString("Sub Total : ", font, brush, new XPoint(COL2_START_X - size.Width, summaryStartY + summaryRowHeight * 1 / 4), XStringFormats.CenterLeft);
                printer.DrawString("$ " + subTotal.ToString(sDecimalFormat), font, brush, new XPoint(COL2_START_X + 10, summaryStartY + summaryRowHeight * 1 / 4), XStringFormats.CenterLeft);

                summaryStartY += summaryRowHeight;
                // /* End Print Subtotal */

                // /* Print VAT */
                //contents.addRect(firstRectEndX, summaryStartY, secondRectEndX, summaryRowHeight);
                //contents.stroke();

                //summaryPrinter.putTextToTheRight(summaryLabelRightX, summaryStartY + summaryRowHeight * 1 / 4, "Vat : ");
                //summaryPrinter.putTextToTheRight(docEndX - 10, summaryStartY + summaryRowHeight * 1 / 4, "$ " + vatValue.ToString(sDecimalFormat));

                summaryStartY += summaryRowHeight;
                // /* End Print VAT */
            }

            /* Print Total */
            size = printer.MeasureString("Sub Total : ", font);
            summaryRowHeight = size.Height * 3;

            printer.DrawRectangle(strokePen, new XRect(COL2_START_X, summaryStartY - summaryRowHeight * 1 / 4, COL2_WIDTH, summaryRowHeight));

            printer.DrawString("Total : ", font, brush, new XPoint(COL2_START_X - size.Width, summaryStartY + summaryRowHeight * 1 / 4), XStringFormats.CenterLeft);
            printer.DrawString("$ " + totalCost.ToString(sDecimalFormat), font, brush, new XPoint(COL2_START_X + 10, summaryStartY + summaryRowHeight * 1 / 4), XStringFormats.CenterLeft);

            summaryStartY += summaryRowHeight;
            /* End Print Total */

            return summaryStartY;
        }

        private PdfPage NewPage(PdfDocument pdfDocument, PageSize pageSize, double Y_Start, int numRows, Invoice invoice)
        {
            // One page in Portrait...
            PdfPage page = pdfDocument.AddPage();
            page.Size = pageSize;

            XGraphics gfx = XGraphics.FromPdfPage(page);

            bool hasLogo = false;
            PrintLetterHead(pdfDocument, gfx, DOCUMENT_TOP_MARGIN, hasLogo);
            PrintInvoiceMetadata(gfx, DOCUMENT_TOP_MARGIN, invoice);
            double nextStartY = PrintServicesListHeaderRow(gfx, Y_Start);

            return page;
        }

        private double PrintTermsAndConditions(XGraphics printer, double startY, Invoice invoice)
        {
            string termsAndConditions = "Quotation Valid for 21 Days. Quotation Valid for 21 Days. Quotation Valid for 21 Days. Quotation Valid for 21 Days. Quotation Valid for 21 Days. Quotation Valid for 21 Days. Quotation Valid for 21 Days. ";

            XBrush brush = XBrushes.Black;
            XFont termsAndConditionsHeaderFont = new XFont("Times", 10, XFontStyle.Bold);
            XFont termsAndConditionsValueFont = new XFont("Times", 9, XFontStyle.Regular);

            string sStr = "Terms & Conditions";
            startY += printer.MeasureString(sStr, termsAndConditionsHeaderFont).Height * 2;
            printer.DrawString(sStr, termsAndConditionsHeaderFont, brush, new XPoint(DOCUMENT_LEFT_MARGIN, startY));
            //startY += printer.MeasureString(sStr, termsAndConditionsHeaderFont).Height * 2.2;

            List<string> footerLines = new List<string>();

            List<string> breaks = (termsAndConditions.Split("\n")).ToList();
            foreach (string br in breaks)
            {
                StringBuilder sb = new StringBuilder();
                foreach (string s in br.Split(" "))
                {
                    XSize size = printer.MeasureString(sb.ToString() + s + " ", termsAndConditionsValueFont);
                    if (size.Width > DOCUMENT_PRINTABLE_WIDTH)
                    {
                        footerLines.Add(sb.ToString());
                        sb = new StringBuilder();
                        sb.Append("  ");
                    }
                    sb.Append(s).Append(" ");
                }

                if (sb.Length >= 1)
                {
                    footerLines.Add(sb.ToString());
                }
            }

            // double yPos = startY - rowHeight + rowHeight * 2 / 5;
            foreach (string line in footerLines)
            {
                startY += printer.MeasureString(line, termsAndConditionsValueFont).Height * 1.1;
                printer.DrawString(line, termsAndConditionsValueFont, brush, new XPoint(DOCUMENT_LEFT_MARGIN, startY));
            }

            return startY;
        }

        private double PrintPaymentDetails(XGraphics printer, double startY, Invoice invoice)
        {
            XBrush brush = XBrushes.Black;
            XFont paymentDetailsHeaderFont = new XFont("Times", 11, XFontStyle.Bold);
            XFont paymentDetailsValueFont = new XFont("Times", 9, XFontStyle.Regular);

            string sStr = "Terms & Conditions";
            startY += printer.MeasureString(sStr, paymentDetailsHeaderFont).Height * 2;
            printer.DrawString(sStr, paymentDetailsHeaderFont, brush, new XPoint(DOCUMENT_LEFT_MARGIN, startY));
            //startY += printer.MeasureString(sStr, paymentDetailsHeaderFont).Height;

            List<string> footerLines = new List<string>();

            List<string> breaks = (invoice.PaymentDetails.Split("\n")).ToList();
            foreach (string br in breaks)
            {
                StringBuilder sb = new StringBuilder();
                foreach (string s in br.Split(" "))
                {
                    XSize size = printer.MeasureString(sb.ToString() + s + " ", paymentDetailsValueFont);
                    if (size.Width > DOCUMENT_PRINTABLE_WIDTH)
                    {
                        footerLines.Add(sb.ToString());
                        sb = new StringBuilder();
                        sb.Append("  ");
                    }
                    sb.Append(s).Append(" ");
                }

                if (sb.Length >= 1)
                    footerLines.Add(sb.ToString());
            }

            footerLines.ForEach(line =>
            {
                startY += printer.MeasureString(line, paymentDetailsValueFont).Height * 1.1;
                printer.DrawString(line, paymentDetailsValueFont, brush, new XPoint(DOCUMENT_LEFT_MARGIN, startY));
            });

            return startY;
        }

        private void PrintFooter(XGraphics printer)
        {
            string sStr = "* This document serves as a quotation for above listed Services / Products.";

            XFont invoiceFont = new XFont("Times", 8, XFontStyle.Italic);
            XBrush brush = XBrushes.Black;

            double startY = DOCUMENT_HEIGHT - /*DOCUMENT_BOTTOM_MARGIN  +*/ printer.MeasureString(sStr, invoiceFont).Height * 2;
            printer.DrawString(sStr, invoiceFont, brush, new XPoint(DOCUMENT_LEFT_MARGIN, startY));
        }

        private double PrintInvoiceMetadata(XGraphics printer, double headerStartY, Invoice invoice)
        {
            int headerStartX = 50;
            headerStartY += 20;

            XFont font = new XFont("Times", 10, XFontStyle.Regular);
            XBrush brush = XBrushes.Black;

            printer.DrawString("Number : ", font, brush, new XRect(headerStartX, headerStartY, 65, 15), XStringFormats.CenterLeft);
            printer.DrawString(invoice.Number, font, brush, new XRect(headerStartX + 65, headerStartY, 65, 15), XStringFormats.CenterLeft);
            headerStartY += 15;

            printer.DrawString("Date : ", font, brush, new XRect(headerStartX, headerStartY, 65, 15), XStringFormats.CenterLeft);
            printer.DrawString(invoice.Date.ToString("dd MMMM yyyy"), font, brush, new XRect(headerStartX + 65, headerStartY, 65, 15), XStringFormats.CenterLeft);

            return headerStartY;
        }

        private double PrintServicesListHeaderRow(XGraphics printer, double nextStartY)
        {
            nextStartY += 40;
            int headerRowHeight = 35;

            XFont font = new XFont("Times", 12, XFontStyle.Bold);
            XBrush brush = XBrushes.Black;

            XPen strokePen = new XPen(XColor.FromArgb(100, 100, 100));
            XSolidBrush fillColor = new XSolidBrush(XColor.FromArgb(230, 230, 230));

            printer.DrawRectangles(strokePen, fillColor, new XRect[] {
                new XRect(COL1_START_X, nextStartY, COL1_WIDTH, headerRowHeight),
                new XRect(COL2_START_X, nextStartY, COL2_WIDTH, headerRowHeight)
            });

            double textY = nextStartY + (headerRowHeight * 7 / 10); // + printer.MeasureString("Description", font).Height; 

            printer.DrawString("Description", font, brush, new XPoint(COL1_START_X + 10, textY));
            printer.DrawString("Amount", font, brush, new XPoint(COL2_START_X + 10, textY));

            return nextStartY;
        }

        private bool NewPageRequired(int numPrintedRows, int rowsLeft)
        {
            if (numPrintedRows >= this.maxRowSize)
            {
                return true;
            }
            if (this.maxPageWithSummation < rowsLeft && rowsLeft < this.maxRowSize)
            {
                if (numPrintedRows >= this.breakPoint)
                {
                    return true;
                }
            }
            return false;
        }
         

    }
}