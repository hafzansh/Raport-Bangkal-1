using iText.IO.Font;
using iText.IO.Font.Constants;
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
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Windows;
using TextAlignment = iText.Layout.Properties.TextAlignment;
using VerticalAlignment = iText.Layout.Properties.VerticalAlignment;

namespace Raport.Services
{
    class ReportKD4
    {
        public static PdfFont SetFont()
        {
            return PdfFontFactory.CreateFont(@"C:\Windows\Fonts\times.ttf");
        }
        public static PdfFont SetBold()
        {
            return PdfFontFactory.CreateFont(@"C:\Windows\Fonts\timesbd.ttf");
        }
        public static void CreateReport(DataTable tablekd, DataTable tablenilai2, string mp, string smt, string kl, string th, string wl, double kk,int bagi)
        {
            try { 
            DataTable tablenilai = tablenilai2.Copy();
            tablenilai.Columns.Add("Rata", typeof(double), $"(kdk1+kdk2+kdk3+kdk4+kdk5)/{bagi}").SetOrdinal(8);
            tablenilai.Columns.Add("Keterangan", typeof(string)).SetOrdinal(9);
            for (int i = 0; i < tablenilai.Rows.Count; i++)
            {
                string result;
                if (Convert.ToDouble(tablenilai.Rows[i][8]) <= kk)
                {
                    result = "Tidak Lulus";
                }
                else
                {
                    result = "Lulus";
                }
                tablenilai.Rows[i][9] = result;
            }
            PdfFont font = SetFont();
            PdfFont bold = SetBold();
            Paragraph mapel = new Paragraph();
            Paragraph semester = new Paragraph();
            Paragraph kelas = new Paragraph();
            Paragraph tahun = new Paragraph();
            Paragraph kkm = new Paragraph();
            Paragraph wali = new Paragraph();
            Table table = new Table(2, true);
            float[] header_width = { 2, 8 };
            float[] header_width2 = { 1, 1, 6, 1, 1, 1, 1,1,1, 1 };
            Table kd = new Table(UnitValue.CreatePercentArray(header_width));
            Table nilaikd = new Table(UnitValue.CreatePercentArray(header_width2));
            System.Data.DataTable kdtable = new System.Data.DataTable();
            System.Data.DataTable nilaitable = new System.Data.DataTable();
            string path = Constants.folderpath + Database.db_name + @"\Nilai KD4";
            bool exists = Directory.Exists(path);
            if (!exists)
                Directory.CreateDirectory(path);
            PdfWriter writer = new PdfWriter(path + @"\" + mp + ".pdf");
            PdfDocument pdf = new PdfDocument(writer);
            pdf.SetDefaultPageSize(PageSize.A4.Rotate());
            Document document = new Document(pdf);
            document.SetTopMargin(25);

            Paragraph header = new Paragraph("PEMERINTAH BANJARBARU\nDINAS PENDIDIKAN KOTA BANJARBARU")
                .SetTextAlignment(TextAlignment.CENTER).SetFontSize(13)
                .SetFont(font);
            header.Add(new Text("\nSD NEGERI 1 BANGKAL").SetFont(bold)).SetTextAlignment(TextAlignment.CENTER).SetFontSize(13);
            Paragraph title = new Paragraph("DAFTAR NILAI SISWA").SetTextAlignment(TextAlignment.CENTER).SetFontSize(13).SetFont(font);
            Paragraph aspek = new Paragraph("ASPEK : KETERAMPILAN").SetTextAlignment(TextAlignment.LEFT).SetFontSize(11).SetFont(bold);
            Paragraph newline = new Paragraph(new Text("\n"));
            LineSeparator ls = new LineSeparator(new SolidLine());

            mapel.Add("Mata Pelajaran").SetFont(font);
            mapel.Add(new Tab());
            mapel.Add(new Text(": " + mp).SetFont(bold));

            semester.Add(new Tab());
            semester.Add("Semester").SetFont(font);

            semester.Add(" :  " + smt);

            kelas.Add("Kelas").SetFont(font);
            kelas.Add(new Tab());
            kelas.Add(new Tab());
            kelas.Add(": " + kl);

            tahun.Add(new Tab());
            tahun.Add("Tahun Ajaran").SetFont(font);

            tahun.Add(" : " + th);

            wali.Add("Wali Kelas").SetFont(font);
            wali.Add(new Tab());
            wali.Add(": " + wl);

            kkm.Add(new Tab());
            kkm.Add("KKM").SetFont(font);

            kkm.Add(" : " + kk);

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
            Cell cell31 = new Cell(1, 1)
               .SetTextAlignment(TextAlignment.LEFT).SetFontSize(12)
               .Add(wali)
               .SetBorder(Border.NO_BORDER);
            Cell cell32 = new Cell(1, 1)
               .SetTextAlignment(TextAlignment.RIGHT).SetFontSize(12)
               .Add(kkm)
               .SetBorder(Border.NO_BORDER);
            table.SetWidth(UnitValue.CreatePercentValue(100));
            table.AddCell(cell11);
            table.AddCell(cell12);
            table.AddCell(cell21);
            table.AddCell(cell22);
            table.AddCell(cell31);
            table.AddCell(cell32);
            kd.SetWidth(UnitValue.CreatePercentValue(100));
            kd.SetFixedLayout();
            nilaikd.SetWidth(UnitValue.CreatePercentValue(100));
            nilaikd.SetFixedLayout();
            string[] header_title = { "", "KOMPETENSI DASAR", "Isikan Kompetensi Dasar Aspek Pengetahuan" };
            for (int h = 1; h < tablekd.Columns.Count; h++)
            {
                Cell cells = new Cell(1, 1).SetTextAlignment(TextAlignment.CENTER).SetVerticalAlignment(iText.Layout.Properties.VerticalAlignment.MIDDLE).Add(new Paragraph(header_title[h]).SetFont(bold).SetBackgroundColor(ColorConstants.LIGHT_GRAY));
                kd.AddCell(cells);
            }
            foreach (DataRow r in tablekd.Rows)
            {
                if (tablekd.Rows.Count > 0)
                {

                    for (int h = 1; h < tablekd.Columns.Count; h++)
                    {
                        if (h == 1)
                        {
                            Cell cells = new Cell(1, 1).SetTextAlignment(TextAlignment.CENTER).Add(new Paragraph(r[h].ToString()).SetFont(font));
                            kd.AddCell(cells);
                        }
                        else
                        {
                            Cell cells = new Cell(1, 1).SetTextAlignment(TextAlignment.LEFT).Add(new Paragraph(r[h].ToString()).SetFont(font));
                            kd.AddCell(cells);
                        }

                        kd.SetKeepTogether(true);
                    }
                }
            }
            kd.SetFontSize(9);
            nilaikd.AddHeaderCell(new Cell(2, 1).Add(new Paragraph("No").SetTextAlignment(TextAlignment.CENTER)).SetVerticalAlignment(iText.Layout.Properties.VerticalAlignment.MIDDLE));
            nilaikd.AddHeaderCell(new Cell(2, 1).Add(new Paragraph("Induk").SetTextAlignment(TextAlignment.CENTER)).SetVerticalAlignment(iText.Layout.Properties.VerticalAlignment.MIDDLE));
            nilaikd.AddHeaderCell(new Cell(2, 1).Add(new Paragraph("Nama").SetTextAlignment(TextAlignment.CENTER)).SetVerticalAlignment(iText.Layout.Properties.VerticalAlignment.MIDDLE));
            nilaikd.AddHeaderCell(new Cell(1, 5).Add(new Paragraph("Unjuk Kerja / Proyek / Portofolio").SetTextAlignment(TextAlignment.CENTER)).SetVerticalAlignment(iText.Layout.Properties.VerticalAlignment.MIDDLE));
            nilaikd.AddHeaderCell(new Cell(2, 1).Add(new Paragraph("Rata-rata").SetTextAlignment(TextAlignment.CENTER)).SetVerticalAlignment(iText.Layout.Properties.VerticalAlignment.MIDDLE));
            nilaikd.AddHeaderCell(new Cell(2, 1).Add(new Paragraph("Keterangan").SetTextAlignment(TextAlignment.CENTER)).SetVerticalAlignment(iText.Layout.Properties.VerticalAlignment.MIDDLE));
            nilaikd.AddHeaderCell("KD1").SetTextAlignment(TextAlignment.CENTER).SetVerticalAlignment(iText.Layout.Properties.VerticalAlignment.MIDDLE);
            nilaikd.AddHeaderCell("KD2");
            nilaikd.AddHeaderCell("KD3");
            nilaikd.AddHeaderCell("KD4");
            nilaikd.AddHeaderCell("KD5");
            foreach (DataRow r in tablenilai.Rows)
            {
                if (tablenilai.Rows.Count > 0)
                {
                    for (int h = 0; h < tablenilai.Columns.Count; h++)
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
            nilaikd.AddCell(new Cell(1, 3).Add(new Paragraph("Nilai Rata-rata Kelas")).SetFont(bold)).SetHorizontalAlignment(iText.Layout.Properties.HorizontalAlignment.CENTER);
            string[] rata_list = { "kdk1", "kdk2", "kdk3", "kdk4", "kdk5","Rata" };
            for (int i = 0; i < rata_list.Length; i++)
            {
                nilaikd.AddCell(Math.Round(kdp(rata_list[i], tablenilai), 2).ToString()).SetFont(bold);
            }

            nilaikd.AddCell("");
            nilaikd.SetFontSize(9);

            document.Add(header);
            document.Add(ls);
            document.Add(title);
            document.Add(table);
            document.Add(aspek);
            document.Add(kd);
            document.Add(newline);
            document.Add(nilaikd);
            document.Close();
                Constants.openFolder(@"\Nilai KD4");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Data masih kosong");
            }
        }
        private static decimal kdp(string target, DataTable tablenilai)
        {
            decimal kdp1 = tablenilai.Select().Where(p => p[target] != DBNull.Value && Convert.ToInt32(p[target]) >= 0).Select(c => Convert.ToDecimal(c[target])).Average();
            return kdp1;
        }
    }
}
