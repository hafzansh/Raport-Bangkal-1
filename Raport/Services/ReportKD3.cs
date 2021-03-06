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
    class ReportKD3
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
            try
            {
                DataTable tablenilai = tablenilai2.Copy();
            tablenilai.Columns.Add("Rata", typeof(double), $"(kdp1+kdp2+kdp3+kdp4+kdp5+tugas1+tugas2+tugas3+tugas4+tugas5)/{bagi*2}").SetOrdinal(13);
            tablenilai.Columns.Add("Total", typeof(double), "(Rata*0.6)+((uts+uas)*0.4)/2").SetOrdinal(16);
            tablenilai.Columns.Add("Keterangan", typeof(string)).SetOrdinal(17);            
            for (int i = 0; i < tablenilai.Rows.Count; i++)
            {
                string result;
                if (Convert.ToDouble(tablenilai.Rows[i][16]) <= kk)
                {
                    result = "Tidak Lulus";
                }
                else
                {
                    result = "Lulus";
                }
                tablenilai.Rows[i][17] = result;
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
            float[] header_width2 = { 1, 1, 3, 1, 1, 1, 1, 1, 1,1, 1, 1, 1, 1, 1, 1,1,1 };
            Table kd = new Table(UnitValue.CreatePercentArray(header_width));
            Table nilaikd = new Table(UnitValue.CreatePercentArray(header_width2));
            System.Data.DataTable kdtable = new System.Data.DataTable();
            System.Data.DataTable nilaitable = new System.Data.DataTable();
            string path = Constants.folderpath + Database.db_name + @"\Nilai KD3";
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
            Paragraph aspek = new Paragraph("ASPEK : PENGETAHUAN").SetTextAlignment(TextAlignment.LEFT).SetFontSize(11).SetFont(bold);
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
                                kd.AddCell(cells.SetVerticalAlignment(VerticalAlignment.MIDDLE)).SetVerticalAlignment(VerticalAlignment.MIDDLE);
                            }
                        else
                        {
                            Cell cells = new Cell(1, 1).SetTextAlignment(TextAlignment.LEFT).Add(new Paragraph(r[h].ToString()).SetFont(font));
                                kd.AddCell(cells.SetVerticalAlignment(VerticalAlignment.MIDDLE)).SetVerticalAlignment(VerticalAlignment.MIDDLE);
                            }

                        kd.SetKeepTogether(true);
                    }
                }
            }
            kd.SetFontSize(9);
            nilaikd.AddHeaderCell(new Cell(2, 1).Add(new Paragraph("No").SetTextAlignment(TextAlignment.CENTER)).SetVerticalAlignment(iText.Layout.Properties.VerticalAlignment.MIDDLE));
            nilaikd.AddHeaderCell(new Cell(2, 1).Add(new Paragraph("Induk").SetTextAlignment(TextAlignment.CENTER)).SetVerticalAlignment(iText.Layout.Properties.VerticalAlignment.MIDDLE));
            nilaikd.AddHeaderCell(new Cell(2, 1).Add(new Paragraph("Nama").SetTextAlignment(TextAlignment.CENTER)).SetVerticalAlignment(iText.Layout.Properties.VerticalAlignment.MIDDLE));
            nilaikd.AddHeaderCell(new Cell(1, 5).Add(new Paragraph("Nilai Ulangan Harian").SetTextAlignment(TextAlignment.CENTER)).SetVerticalAlignment(iText.Layout.Properties.VerticalAlignment.MIDDLE));
            nilaikd.AddHeaderCell(new Cell(1, 5).Add(new Paragraph("Nilai Tugas").SetTextAlignment(TextAlignment.CENTER)).SetVerticalAlignment(iText.Layout.Properties.VerticalAlignment.MIDDLE));
            nilaikd.AddHeaderCell(new Cell(2, 1).Add(new Paragraph("Rata").SetTextAlignment(TextAlignment.CENTER)).SetVerticalAlignment(iText.Layout.Properties.VerticalAlignment.MIDDLE));
            nilaikd.AddHeaderCell(new Cell(2, 1).Add(new Paragraph("UTS").SetTextAlignment(TextAlignment.CENTER)).SetVerticalAlignment(iText.Layout.Properties.VerticalAlignment.MIDDLE));
            nilaikd.AddHeaderCell(new Cell(2, 1).Add(new Paragraph("UAS").SetTextAlignment(TextAlignment.CENTER)).SetVerticalAlignment(iText.Layout.Properties.VerticalAlignment.MIDDLE));
            nilaikd.AddHeaderCell(new Cell(2, 1).Add(new Paragraph("Total Nilai").SetTextAlignment(TextAlignment.CENTER)).SetVerticalAlignment(iText.Layout.Properties.VerticalAlignment.MIDDLE));
            nilaikd.AddHeaderCell(new Cell(2, 1).Add(new Paragraph("Keterangan").SetTextAlignment(TextAlignment.CENTER)).SetVerticalAlignment(iText.Layout.Properties.VerticalAlignment.MIDDLE));
            nilaikd.AddHeaderCell("KD1").SetTextAlignment(TextAlignment.CENTER).SetVerticalAlignment(iText.Layout.Properties.VerticalAlignment.MIDDLE);
            nilaikd.AddHeaderCell("KD2");
            nilaikd.AddHeaderCell("KD3");
            nilaikd.AddHeaderCell("KD4");
            nilaikd.AddHeaderCell("KD5");
            nilaikd.AddHeaderCell("T1");
            nilaikd.AddHeaderCell("T2");
            nilaikd.AddHeaderCell("T3");
            nilaikd.AddHeaderCell("T4");
            nilaikd.AddHeaderCell("T5");
            foreach (DataRow r in tablenilai.Rows)
            {
                if (tablenilai.Rows.Count > 0)
                {
                    for (int h = 0; h < tablenilai.Columns.Count; h++)
                    {
                        if (h == 2)
                        {
                            Cell cells = new Cell(1, 1).SetTextAlignment(TextAlignment.LEFT).Add(new Paragraph("    " + r[h].ToString()).SetFont(font)).SetVerticalAlignment(iText.Layout.Properties.VerticalAlignment.MIDDLE);
                            cells.SetHeight(25);
                            nilaikd.AddCell(cells);

                        }
                        else if (h == 16 || h==13)
                        {
                            Cell cells = new Cell(1, 1).SetTextAlignment(TextAlignment.CENTER).Add(new Paragraph(Math.Round(System.Convert.ToDouble(r[h]),2).ToString()).SetFont(font)).SetVerticalAlignment(iText.Layout.Properties.VerticalAlignment.MIDDLE);
                            cells.SetHeight(25);
                            nilaikd.AddCell(cells);
                        }
                        else
                        {
                            Cell cells = new Cell(1, 1).SetTextAlignment(TextAlignment.CENTER).Add(new Paragraph(r[h].ToString()).SetFont(font)).SetVerticalAlignment(iText.Layout.Properties.VerticalAlignment.MIDDLE);
                            cells.SetHeight(25);
                            nilaikd.AddCell(cells);
                        }
                        nilaikd.SetKeepTogether(false);
                    }

                }
            }
            nilaikd.AddCell(new Cell(1, 3).Add(new Paragraph("Nilai Rata-rata Kelas")).SetFont(bold)).SetHorizontalAlignment(iText.Layout.Properties.HorizontalAlignment.CENTER);
            string[] rata_list = { "kdp1", "kdp2", "kdp3", "kdp4", "kdp5", "tugas1", "tugas2", "tugas3", "tugas4", "tugas5","Rata","uts", "uas","Total" };
            for (int i = 0; i < rata_list.Length; i++)
            {
                nilaikd.AddCell(Math.Round(kdp(rata_list[i], tablenilai), 2).ToString()).SetFont(bold);
            }
            nilaikd.AddCell("");

            nilaikd.SetFontSize(8);
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
            document.Add(title);
            document.Add(table);
            document.Add(aspek);
            document.Add(kd);
            document.Add(newline);
            document.Add(nilaikd);
            document.Close();
                Constants.openFolder(@"\Nilai KD3");
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
