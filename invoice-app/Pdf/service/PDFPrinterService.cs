//using System;

//namespace invoice_demo_app.invoice.service
//{
//    public class PDFPrinterService
//    {
//        private PDPageContentStream contents;
//        private PDFont font;
//        private int fontSize;
//        private Color color;

//        public PDFPrinterService(PDPageContentStream contents, PDFont font, int fontSize)
//            : this(contents, font, fontSize, Color.BLACK)
//        {        }

//        public PDFPrinterService(PDPageContentStream contents, PDFont font, int fontSize, Color color)
//        {
//            this.contents = contents;
//            this.font = font;
//            this.fontSize = fontSize;
//            this.color = color;
//        }

//        public void putText(int x, int y, string text) //throws IOException
//        {
//            contents.setNonStrokingColor(color);
//            contents.beginText();
//            contents.setFont(font, fontSize);
//            //contents.drawLine( .newLineAtOffset(x, y);
//            contents.drawString(text);
//            contents.endText();
//        }

//        public int widthOfText(string text) 




//        {
//            return (int)Math.Round((font.getStringWidth(text) / 1000f) * this.fontSize);
//        }

//        public void putTextToTheRight(int x, int y, string text) 
//        {
//            this.putText(x - widthOfText(text), y, text);
//        }
//    }
//}