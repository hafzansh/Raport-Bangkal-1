using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using Raport.Helper;
using System.Data;

namespace Raport.Services
{
    class Report
    {
        public static void CreateDasis(DataTable dt)
        {
            PdfWriter writer = new PdfWriter("F:\\demo.pdf");
            PdfDocument pdf = new PdfDocument(writer);
            pdf.SetDefaultPageSize(PageSize.A3.Rotate());
            Document document = new Document(pdf);
            Paragraph header = new Paragraph("Data Siswa").SetTextAlignment(TextAlignment.CENTER).SetFontSize(20);
            Paragraph newline = new Paragraph(new Text("\n"));
            Table tables = new Table(UnitValue.CreatePercentArray(Constants.header_width));
            tables.SetWidth(UnitValue.CreatePercentValue(100));
            tables.SetFixedLayout();

            for (int h = 0; h < dt.Columns.Count; h++)
            {
                Cell cells = new Cell(1, 1).SetTextAlignment(TextAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph(Constants.header_title[h]));
                tables.AddCell(cells);
            }
            foreach (DataRow r in dt.Rows)
            {
                if (dt.Rows.Count > 0)
                {
                    for (int h = 0; h < dt.Columns.Count; h++)
                    {
                        Cell cells = new Cell(1, 1).SetTextAlignment(TextAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph(r[h].ToString()));
                        cells.SetHeight(40);
                        tables.AddCell(cells);
                        tables.SetKeepTogether(true);
                    }
                }
            }

            document.Add(header);
            document.Add(newline);
            document.Add(tables);
            document.Close();
        }
    }

}
