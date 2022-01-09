using iText.Kernel.Colors;
using iText.Kernel.Font;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Draw;
using iText.Layout;
using iText.Layout.Borders;
using iText.Layout.Element;
using iText.Layout.Properties;
using Raport.Helper;
using System;
using System.Data;
using System.IO;
using System.Windows;
using TextAlignment = iText.Layout.Properties.TextAlignment;
using VerticalAlignment = iText.Layout.Properties.VerticalAlignment;
namespace Raport.Services
{
    public static class ReportAbsen
    {
        public static void CreateReport() { 
            try
            {
                
                DataTable originalAbsen = Connection.dataset.Tables[Constants.absen_title].Copy();
                originalAbsen.Columns.Add("no", typeof(int));
                originalAbsen.Columns.Add("total", typeof(int),"s+i+a");

                for (int i=0; i < originalAbsen.Rows.Count; i++)
                {
                    originalAbsen.Rows[i]["no"] = i + 1;
                }
                DataView view = new DataView(originalAbsen);
                DataTable newAbsen = new();
                newAbsen = view.ToTable("originalAbsen", true,"no","induk","nama","s","i","a","total");
                PdfFont font = iTextFont.SetFont();
                PdfFont bold = iTextFont.SetBold();
                Paragraph mapel = new Paragraph();
                Paragraph semester = new Paragraph();
                Paragraph kelas = new Paragraph();
                Paragraph tahun = new Paragraph();
                Paragraph kkm = new Paragraph();
                Paragraph wali = new Paragraph();
                Table table = new Table(2, true);                
                float[] header_width = { 1, 1, 3, 1, 1, 1, 1 };                
                Table nilaikd = new Table(UnitValue.CreatePercentArray(header_width));
                System.Data.DataTable kdtable = new System.Data.DataTable();
                System.Data.DataTable nilaitable = new System.Data.DataTable();
                string path = Constants.folderpath + Database.db_name + @"\Data Siswa";
                bool exists = Directory.Exists(path);
                if (!exists)
                    Directory.CreateDirectory(path);
                PdfWriter writer = new PdfWriter(path + @"\DATA ABSENSI SISWA.pdf");
                PdfDocument pdf = new PdfDocument(writer);
                pdf.SetDefaultPageSize(PageSize.A4);
                Document document = new Document(pdf);
                document.SetTopMargin(25);

                Paragraph header = new Paragraph("PEMERINTAH BANJARBARU\nDINAS PENDIDIKAN KOTA BANJARBARU")
                    .SetTextAlignment(TextAlignment.CENTER).SetFontSize(13)
                    .SetFont(font);
                header.Add(new Text("\nSD NEGERI 1 BANGKAL").SetFont(bold)).SetTextAlignment(TextAlignment.CENTER).SetFontSize(13);                
                Paragraph aspek = new Paragraph("DATA ABSENSI KELAS").SetTextAlignment(TextAlignment.CENTER).SetFontSize(16).SetFont(bold);
                Paragraph newline = new Paragraph(new Text("\n"));
                LineSeparator ls = new LineSeparator(new SolidLine());

                mapel.Add("Wali Kelas").SetFont(font);
                mapel.Add(new Tab());
                mapel.Add(new Text(": " + Database.wali_kelas).SetFont(bold));

                semester.Add(new Tab());
                semester.Add("Semester").SetFont(font);

                semester.Add(" :  " + Database.semester);

                kelas.Add("Kelas").SetFont(font);
                kelas.Add(new Tab());
                kelas.Add(new Tab());
                kelas.Add(": " + Database.kelas);

                tahun.Add(new Tab());
                tahun.Add("Tahun Ajaran").SetFont(font);

                tahun.Add(" : " + Database.tahun);
                

                Cell cell11 = new Cell(1, 1)
                   .SetTextAlignment(TextAlignment.LEFT).SetFontSize(12)
                   .Add(mapel)
                   .SetBorder(Border.NO_BORDER);
                Cell cell12 = new Cell(1, 1)
                   .SetTextAlignment(TextAlignment.RIGHT)
                   .Add(semester)
                   .SetBorder(Border.NO_BORDER);
                Cell cell21 = new Cell(1, 1)
                   .SetTextAlignment(TextAlignment.LEFT).SetFontSize(12)
                   .Add(kelas)
                   .SetBorder(Border.NO_BORDER);
                Cell cell22 = new Cell(1, 1)
                   .SetTextAlignment(TextAlignment.RIGHT)
                   .Add(tahun)
                   .SetBorder(Border.NO_BORDER);
                
                table.SetWidth(UnitValue.CreatePercentValue(100));
                table.AddCell(cell11);
                table.AddCell(cell12);
                table.AddCell(cell21);
                table.AddCell(cell22);                                
                nilaikd.SetWidth(UnitValue.CreatePercentValue(100));
                nilaikd.SetFixedLayout();
                string[] header_title = { "", "KOMPETENSI DASAR", "Isikan Kompetensi Dasar Aspek Pengetahuan" };                
                nilaikd.AddHeaderCell(new Cell(2, 1).Add(new Paragraph("No").SetTextAlignment(TextAlignment.CENTER)).SetVerticalAlignment(iText.Layout.Properties.VerticalAlignment.MIDDLE));
                nilaikd.AddHeaderCell(new Cell(2, 1).Add(new Paragraph("Induk").SetTextAlignment(TextAlignment.CENTER)).SetVerticalAlignment(iText.Layout.Properties.VerticalAlignment.MIDDLE));
                nilaikd.AddHeaderCell(new Cell(2, 1).Add(new Paragraph("Nama").SetTextAlignment(TextAlignment.CENTER)).SetVerticalAlignment(iText.Layout.Properties.VerticalAlignment.MIDDLE));
                nilaikd.AddHeaderCell(new Cell(1, 3).Add(new Paragraph("Absensi Siswa").SetTextAlignment(TextAlignment.CENTER)).SetVerticalAlignment(iText.Layout.Properties.VerticalAlignment.MIDDLE));
                nilaikd.AddHeaderCell(new Cell(2, 1).Add(new Paragraph("Total").SetTextAlignment(TextAlignment.CENTER)).SetVerticalAlignment(iText.Layout.Properties.VerticalAlignment.MIDDLE));                
                nilaikd.AddHeaderCell("Sakit").SetTextAlignment(TextAlignment.CENTER);
                nilaikd.AddHeaderCell("Izin");
                nilaikd.AddHeaderCell("Alpa");
                
                foreach (DataRow r in newAbsen.Rows)
                {
                    if (newAbsen.Rows.Count > 0)
                    {
                        for (int h = 0; h < newAbsen.Columns.Count; h++)
                        {
                            if (h == 2)
                            {
                                Cell cells = new Cell(1, 1).SetTextAlignment(TextAlignment.LEFT).Add(new Paragraph("    " + r[h].ToString()).SetFont(font)).SetVerticalAlignment(iText.Layout.Properties.VerticalAlignment.MIDDLE);
                                cells.SetHeight(20);
                                nilaikd.AddCell(cells);

                            }
                            else
                            {
                                Cell cells = new Cell(1, 1).SetTextAlignment(TextAlignment.CENTER).Add(new Paragraph(r[h].ToString()).SetFont(font)).SetVerticalAlignment(iText.Layout.Properties.VerticalAlignment.MIDDLE);
                                cells.SetHeight(20);
                                nilaikd.AddCell(cells);
                            }
                            nilaikd.SetKeepTogether(false);
                        }

                    }
                }                
                
                nilaikd.SetFontSize(9);

                document.Add(header);
                document.Add(ls);
                document.Add(aspek);
                document.Add(table);                
                document.Add(nilaikd);
                document.Close();
                Constants.openFolder(@"\Data Siswa");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Data masih kosong, Detail = "+ex.ToString());
            }
        }
    }
}
