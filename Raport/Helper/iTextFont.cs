using iText.Kernel.Font;

namespace Raport.Helper
{
    public static class iTextFont
    {
        public static PdfFont SetFont()
        {
            return PdfFontFactory.CreateFont(@"C:\Windows\Fonts\times.ttf");
        }
        public static PdfFont SetBold()
        {
            return PdfFontFactory.CreateFont(@"C:\Windows\Fonts\timesbd.ttf");
        }
    }
}
