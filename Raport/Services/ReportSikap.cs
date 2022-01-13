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
    public static class ReportSikap
    {
        public static void CreateReport()
        {
            try
            {

                DataTable originalAbsen = Connection.dataset.Tables[Constants.sikap_title].Copy();
                originalAbsen.Columns.Add("no", typeof(int));
                for (int i = 0; i < originalAbsen.Rows.Count; i++)
                {
                    originalAbsen.Rows[i]["no"] = i + 1;
                }
                DataView view = new DataView(originalAbsen);
                DataTable newAbsen = new();
                newAbsen = view.ToTable("originalAbsen", true, "no", "induk", "nama", "sikap1", "sikap2", "sikap3", "sikap4");
                PdfFont font = iTextFont.SetFont();
                PdfFont bold = iTextFont.SetBold();
                Paragraph mapel = new Paragraph();
                Paragraph semester = new Paragraph();
                Paragraph kelas = new Paragraph();
                Paragraph tahun = new Paragraph();
                Paragraph kkm = new Paragraph();
                Paragraph wali = new Paragraph();
                Table table = new Table(2, true);
                float[] header_width = { 1, 1, 3, 3, 3, 3, 3 };
                Table nilaikd = new Table(UnitValue.CreatePercentArray(header_width));
                System.Data.DataTable kdtable = new System.Data.DataTable();
                System.Data.DataTable nilaitable = new System.Data.DataTable();
                string path = Constants.folderpath + Database.db_name + @"\Data Siswa";
                bool exists = Directory.Exists(path);
                if (!exists)
                    Directory.CreateDirectory(path);
                PdfWriter writer = new PdfWriter(path + @"\DATA SIKAP SISWA.pdf");
                PdfDocument pdf = new PdfDocument(writer);
                pdf.SetDefaultPageSize(PageSize.A4.Rotate());
                Document document = new Document(pdf);
                document.SetTopMargin(25);

                Paragraph header = new Paragraph("PEMERINTAH BANJARBARU\nDINAS PENDIDIKAN KOTA BANJARBARU")
                    .SetTextAlignment(TextAlignment.CENTER).SetFontSize(13)
                    .SetFont(font);
                header.Add(new Text("\nSD NEGERI 1 BANGKAL").SetFont(bold)).SetTextAlignment(TextAlignment.CENTER).SetFontSize(13);
                Paragraph aspek = new Paragraph("DATA SIKAP SISWA").SetTextAlignment(TextAlignment.CENTER).SetFontSize(16).SetFont(bold);
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
                nilaikd.AddHeaderCell(new Cell(1, 1).Add(new Paragraph("No").SetTextAlignment(TextAlignment.CENTER)).SetVerticalAlignment(iText.Layout.Properties.VerticalAlignment.MIDDLE));
                nilaikd.AddHeaderCell(new Cell(1, 1).Add(new Paragraph("Induk").SetTextAlignment(TextAlignment.CENTER)).SetVerticalAlignment(iText.Layout.Properties.VerticalAlignment.MIDDLE));
                nilaikd.AddHeaderCell(new Cell(1, 1).Add(new Paragraph("Nama").SetTextAlignment(TextAlignment.CENTER)).SetVerticalAlignment(iText.Layout.Properties.VerticalAlignment.MIDDLE));
                //nilaikd.AddHeaderCell(new Cell(1, 4).Add(new Paragraph("Deksripsi Sikap").SetTextAlignment(TextAlignment.CENTER)).SetVerticalAlignment(iText.Layout.Properties.VerticalAlignment.MIDDLE));
                nilaikd.AddHeaderCell("Berdoa dengan Khusuk sebelum dan sesudah memulai Aktivitas").SetTextAlignment(TextAlignment.CENTER);
                nilaikd.AddHeaderCell("Perilaku Bersyukur dan Toleransi Beragama");
                nilaikd.AddHeaderCell("Jujur, Disiplin dan Tanggung Jawab");
                nilaikd.AddHeaderCell("Percaya Diri, Santun dan Peduli Sesama");

                foreach (DataRow r in newAbsen.Rows)
                {
                    if (newAbsen.Rows.Count > 0)
                    {
                        for (int h = 0; h < newAbsen.Columns.Count; h++)
                        {
                            if (h == 0 || h==1)
                            {
                                Cell cells = new Cell(1, 1).SetTextAlignment(TextAlignment.CENTER).Add(new Paragraph("    " + r[h].ToString()).SetFont(font)).SetVerticalAlignment(iText.Layout.Properties.VerticalAlignment.MIDDLE);
                                cells.SetHeight(20);
                                nilaikd.AddCell(cells);

                            }else if (h == 2)
                            {
                                Cell cells = new Cell(1, 1).SetTextAlignment(TextAlignment.LEFT).Add(new Paragraph("    " + r[h].ToString()).SetFont(font)).SetVerticalAlignment(iText.Layout.Properties.VerticalAlignment.MIDDLE);
                                cells.SetHeight(20);
                                nilaikd.AddCell(cells);

                            }
                            else
                            {
                                Cell cells = new Cell(1, 1).SetTextAlignment(TextAlignment.CENTER).Add(new Paragraph(setSikap(r[h].ToString()).ToString()).SetFont(font)).SetVerticalAlignment(iText.Layout.Properties.VerticalAlignment.MIDDLE);
                                cells.SetHeight(20);
                                nilaikd.AddCell(cells);
                            }
                            nilaikd.SetKeepTogether(true);
                        }

                    }
                }

                nilaikd.SetFontSize(9);
                header.Add(new Text("\nJln. Mistar Cokrukosumo Rt. 3 Rw. 1 Bangkal, Kecamatan Cempaka Kode Pos 70732").SetFont(font)).SetTextAlignment(TextAlignment.CENTER).SetFontSize(13);
                header.Add(new Text("\nKota Banjarbaru Kalimantan Selatan").SetFont(font)).SetTextAlignment(TextAlignment.CENTER).SetFontSize(13);
                string ImageFile = @"Resources\tut_logo_full.png";
                string ImageFile2 = @"Resources\bjb_logo.png";
                iText.IO.Image.ImageData data = iText.IO.Image.ImageDataFactory.Create(ImageFile);
                iText.IO.Image.ImageData data2 = iText.IO.Image.ImageDataFactory.Create(ImageFile2);
                Image img = new Image(data);
                Image img2 = new Image(data2);
                img2.SetFixedPosition(40, 475);
                img.SetHeight(90);
                img.SetFixedPosition(710, 475);
                img2.SetHeight(90);
                document.Add(img);
                document.Add(img2);
                document.Add(header);
                document.Add(ls);
                document.Add(aspek);
                document.Add(table);
                document.Add(newline);
                document.Add(nilaikd);
                document.Close();
                Constants.openFolder(@"\Data Siswa");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Data masih kosong, Detail = " + ex.ToString());
            }
        }
        private static string setSikap(string target)
        {
            string result = "Sikap";
            if (double.Parse(target) == 4)
            {
                result = "Sangat Baik";
            }
            else if (double.Parse(target) == 3)
            {
                result = "Baik";
            }
            else if (double.Parse(target) == 2)
            {
                result = "Cukup";
            }
            else if (double.Parse(target) == 1)
            {
                result = "Perlu Bimbingan";
            }
            return result;
        }
    }
}
