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
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Windows;
using TextAlignment = iText.Layout.Properties.TextAlignment;
using VerticalAlignment = iText.Layout.Properties.VerticalAlignment;

namespace Raport.Services
{
    public static class RekapNilai
    {
        public static SQLiteDataAdapter adapter = new SQLiteDataAdapter();
        public static SQLiteCommandBuilder commandBuilder;
        
        public static void print(bool val)
        {
            populateTable(val);
            
        }
        public static void populateTable(bool val)
        {
            DataTable dt = new DataTable();
            DataTable dtnew = new DataTable();
            DataTable dt2 = new DataTable();
        bool isPengetahuan = val;
            string script = File.ReadAllText(@"Scripts/sql1.sql");
            string script2 = File.ReadAllText(@"Scripts/sql2.sql");
            adapter.SelectCommand = new SQLiteCommand(script, Connection.sqlite);
            commandBuilder = new SQLiteCommandBuilder(adapter);
            adapter.Fill(dt);
            adapter.SelectCommand = new SQLiteCommand(script2, Connection.sqlite);
            commandBuilder = new SQLiteCommandBuilder(adapter);
            adapter.Fill(dt2);
            dt.Columns.Add("bta_3", typeof(double));
            dt.Columns.Add("bta_4", typeof(double));
            dt.Columns.Add("no", typeof(int)).SetOrdinal(0);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dt.Rows[i]["bta_3"] = dt2.Rows[i]["bta_3"];
                dt.Rows[i]["bta_4"] = dt2.Rows[i]["bta_4"];
                dt.Rows[i]["no"] = i + 1;
            }
            if (isPengetahuan)
            {
                dt.Columns.Add("rata", typeof(double), "(agm_3+ pkn_3+ bi_3+ mtk_3+ ipa_3+ ips_3+ sbdp_3+ pjok_3+ bjr_3+ bing_3+ bta_3)/11");
                dt.Columns.Add("total", typeof(double), "agm_3+ pkn_3+ bi_3+ mtk_3+ ipa_3+ ips_3+ sbdp_3+ pjok_3+ bjr_3+ bing_3+ bta_3");
                dt.Columns.Add("rank", typeof(int));
                setRank(dt);
                DataView view = new DataView(dt);
                dtnew = view.ToTable("dt", true, "no","induk", "nama", "agm_3", "pkn_3", "bi_3", "mtk_3", "ipa_3", "ips_3", "sbdp_3", "pjok_3", "bjr_3", "bing_3", "bta_3", "rata", "total", "rank");
            }
            else
            {
                dt.Columns.Add("rata", typeof(double), "(agm_4+ pkn_4+ bi_4+ mtk_4+ ipa_4+ ips_4+ sbdp_4+ pjok_4+ bjr_4+ bing_4+ bta_4)/11");
                dt.Columns.Add("total", typeof(double), "agm_4+ pkn_4+ bi_4+ mtk_4+ ipa_4+ ips_4+ sbdp_4+ pjok_4+ bjr_4+ bing_4+ bta_4");
                dt.Columns.Add("rank", typeof(int));
                setRank(dt);
                DataView view = new DataView(dt);
                dtnew = view.ToTable("dt", true, "no","induk", "nama", "agm_4", "pkn_4", "bi_4", "mtk_4", "ipa_4", "ips_4", "sbdp_4", "pjok_4", "bjr_4", "bing_4", "bta_4", "rata", "total", "rank");
            }
            CreateReport(val,dtnew);
        }
        private static void setRank(DataTable target)
        {
            target.DefaultView.Sort = "total DESC";
            int rank = 1;
            foreach (DataRowView dr in target.DefaultView)
            {
                dr["rank"] = rank;
                rank++;
            }
        }
        public static PdfFont SetFont()
        {
            return PdfFontFactory.CreateFont(@"C:\Windows\Fonts\times.ttf");
        }
        public static PdfFont SetBold()
        {
            return PdfFontFactory.CreateFont(@"C:\Windows\Fonts\timesbd.ttf");
        }
        public static void CreateReport(bool val,DataTable dtnew)
        {
                bool isPengetahuan = val;
                PdfFont font = SetFont();
                PdfFont bold = SetBold();
                Paragraph mapel = new Paragraph();
                Paragraph semester = new Paragraph();
                Paragraph kelas = new Paragraph();
                Paragraph tahun = new Paragraph();
                Paragraph kkm = new Paragraph();
                Paragraph wali = new Paragraph();
                Table table = new Table(2, true);
                float[] header_width2 = { 1,1, 3, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1,1 };
                Table nilaikd = new Table(UnitValue.CreatePercentArray(header_width2));                
                System.Data.DataTable nilaitable = new System.Data.DataTable();
                string path = Constants.folderpath + Database.db_name + @"\Data Siswa";
                bool exists = Directory.Exists(path);
                string mp;
                if (!exists)
                    Directory.CreateDirectory(path);
                if (isPengetahuan) 
                {
                    mp = "REKAP NILAI ASPEK PENGETAHUAN";
                }
                else
                {
                    mp = "REKAP NILAI ASPEK KETERAMPILAN";
                }
                PdfWriter writer = new PdfWriter(path + @"\" + mp + ".pdf");
                PdfDocument pdf = new PdfDocument(writer);
                pdf.SetDefaultPageSize(PageSize.A4.Rotate());
                Document document = new Document(pdf);
                document.SetTopMargin(25);

                Paragraph header = new Paragraph("PEMERINTAH BANJARBARU\nDINAS PENDIDIKAN KOTA BANJARBARU")
                    .SetTextAlignment(TextAlignment.CENTER).SetFontSize(13)
                    .SetFont(font);
                header.Add(new Text("\nSD NEGERI 1 BANGKAL").SetFont(bold)).SetTextAlignment(TextAlignment.CENTER).SetFontSize(13);
                Paragraph title = new Paragraph(mp).SetTextAlignment(TextAlignment.CENTER).SetFontSize(13).SetFont(bold);                
                Paragraph newline = new Paragraph(new Text("\n"));
                LineSeparator ls = new LineSeparator(new SolidLine());

                mapel.Add("Wali kelas").SetFont(font);
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
            nilaikd.AddHeaderCell(new Cell(1, 1).Add(new Paragraph("No").SetTextAlignment(TextAlignment.CENTER)).SetVerticalAlignment(iText.Layout.Properties.VerticalAlignment.MIDDLE));
            nilaikd.AddHeaderCell(new Cell(1, 1).Add(new Paragraph("Induk").SetTextAlignment(TextAlignment.CENTER)).SetVerticalAlignment(iText.Layout.Properties.VerticalAlignment.MIDDLE));
                nilaikd.AddHeaderCell(new Cell(1, 1).Add(new Paragraph("Nama").SetTextAlignment(TextAlignment.CENTER)).SetVerticalAlignment(iText.Layout.Properties.VerticalAlignment.MIDDLE));
                nilaikd.AddHeaderCell(new Cell(1, 1).Add(new Paragraph("AGAMA").SetTextAlignment(TextAlignment.CENTER)).SetVerticalAlignment(iText.Layout.Properties.VerticalAlignment.MIDDLE));
                nilaikd.AddHeaderCell(new Cell(1, 1).Add(new Paragraph("PKN").SetTextAlignment(TextAlignment.CENTER)).SetVerticalAlignment(iText.Layout.Properties.VerticalAlignment.MIDDLE));
                nilaikd.AddHeaderCell(new Cell(1, 1).Add(new Paragraph("BI").SetTextAlignment(TextAlignment.CENTER)).SetVerticalAlignment(iText.Layout.Properties.VerticalAlignment.MIDDLE));
                nilaikd.AddHeaderCell(new Cell(1, 1).Add(new Paragraph("MTK").SetTextAlignment(TextAlignment.CENTER)).SetVerticalAlignment(iText.Layout.Properties.VerticalAlignment.MIDDLE));
                nilaikd.AddHeaderCell(new Cell(1, 1).Add(new Paragraph("IPA").SetTextAlignment(TextAlignment.CENTER)).SetVerticalAlignment(iText.Layout.Properties.VerticalAlignment.MIDDLE));
                nilaikd.AddHeaderCell(new Cell(1, 1).Add(new Paragraph("IPS").SetTextAlignment(TextAlignment.CENTER)).SetVerticalAlignment(iText.Layout.Properties.VerticalAlignment.MIDDLE));
                nilaikd.AddHeaderCell(new Cell(1, 1).Add(new Paragraph("SBdP").SetTextAlignment(TextAlignment.CENTER)).SetVerticalAlignment(iText.Layout.Properties.VerticalAlignment.MIDDLE));
                nilaikd.AddHeaderCell(new Cell(1, 1).Add(new Paragraph("PJOK").SetTextAlignment(TextAlignment.CENTER)).SetVerticalAlignment(iText.Layout.Properties.VerticalAlignment.MIDDLE));
                nilaikd.AddHeaderCell(new Cell(1, 1).Add(new Paragraph("B.BJR").SetTextAlignment(TextAlignment.CENTER)).SetVerticalAlignment(iText.Layout.Properties.VerticalAlignment.MIDDLE));
                nilaikd.AddHeaderCell(new Cell(1, 1).Add(new Paragraph("B.ING").SetTextAlignment(TextAlignment.CENTER)).SetVerticalAlignment(iText.Layout.Properties.VerticalAlignment.MIDDLE));
                nilaikd.AddHeaderCell(new Cell(1, 1).Add(new Paragraph("BTA").SetTextAlignment(TextAlignment.CENTER)).SetVerticalAlignment(iText.Layout.Properties.VerticalAlignment.MIDDLE));
                nilaikd.AddHeaderCell(new Cell(1, 1).Add(new Paragraph("Rata-rata").SetTextAlignment(TextAlignment.CENTER)).SetVerticalAlignment(iText.Layout.Properties.VerticalAlignment.MIDDLE));
                nilaikd.AddHeaderCell(new Cell(1, 1).Add(new Paragraph("Total Nilai").SetTextAlignment(TextAlignment.CENTER)).SetVerticalAlignment(iText.Layout.Properties.VerticalAlignment.MIDDLE));
                nilaikd.AddHeaderCell(new Cell(1, 1).Add(new Paragraph("Peringkat").SetTextAlignment(TextAlignment.CENTER)).SetVerticalAlignment(iText.Layout.Properties.VerticalAlignment.MIDDLE));
                foreach (DataRow r in dtnew.Rows)
                {
                    if (dtnew.Rows.Count > 0)
                    {
                        for (int h = 0; h < dtnew.Columns.Count; h++)
                        {
                            if (h==0 || h==1 || h == 16)
                            {
                                Cell cells = new Cell(1, 1).SetTextAlignment(TextAlignment.CENTER).Add(new Paragraph(r[h].ToString()).SetFont(font)).SetVerticalAlignment(iText.Layout.Properties.VerticalAlignment.MIDDLE);
                                cells.SetHeight(25);
                                nilaikd.AddCell(cells);
                            }
                            else if (h == 2)
                            {
                                Cell cells = new Cell(1, 1).SetTextAlignment(TextAlignment.LEFT).Add(new Paragraph("    " + r[h].ToString()).SetFont(font)).SetVerticalAlignment(iText.Layout.Properties.VerticalAlignment.MIDDLE);
                                cells.SetHeight(25);
                                nilaikd.AddCell(cells);

                            }                            
                            else
                            {
                                Cell cells = new Cell(1, 1).SetTextAlignment(TextAlignment.CENTER).Add(new Paragraph(Math.Round(Convert.ToDecimal(r[h]),2).ToString()).SetFont(font)).SetVerticalAlignment(iText.Layout.Properties.VerticalAlignment.MIDDLE);
                                cells.SetHeight(25);
                                nilaikd.AddCell(cells);
                        }
                            nilaikd.SetKeepTogether(false);
                        }

                    }
                }
                nilaikd.AddCell(new Cell(1, 3).SetTextAlignment(TextAlignment.CENTER).Add(new Paragraph("Nilai Rata-rata Kelas").SetHorizontalAlignment(iText.Layout.Properties.HorizontalAlignment.CENTER)).SetFont(bold)).SetHorizontalAlignment(iText.Layout.Properties.HorizontalAlignment.CENTER);
                List<string> rata_list = new List<string>();
                if (isPengetahuan)
                {
                    string[] rata_list2 = { "agm_3", "pkn_3", "bi_3", "mtk_3", "ipa_3", "ips_3", "sbdp_3", "pjok_3", "bjr_3", "bing_3", "bta_3", "rata", "Total" };
                    for (int i = 0; i < rata_list2.Length; i++)
                    {
                        rata_list.Add(rata_list2[i]);
                    }
                }
                else {
                string[] rata_list2 = { "agm_4", "pkn_4", "bi_4", "mtk_4", "ipa_4", "ips_4", "sbdp_4", "pjok_4", "bjr_4", "bing_4", "bta_4", "rata", "Total" };
                for (int i = 0; i < rata_list2.Length; i++)
                    {
                        rata_list.Add(rata_list2[i]);
                    }
                }
            
                for (int i = 0; i < rata_list.Count; i++)
                {
                    nilaikd.AddCell(Math.Round(kdp(rata_list[i], dtnew), 2).ToString()).SetTextAlignment(TextAlignment.CENTER).SetFont(bold).SetHorizontalAlignment(iText.Layout.Properties.HorizontalAlignment.CENTER);
                }
                nilaikd.AddCell("");

                nilaikd.SetFontSize(10);

                document.Add(header);
                document.Add(ls);
                document.Add(title);
                document.Add(table);
                document.Add(newline);
                document.Add(nilaikd);
                document.Close();
                Constants.openFolder(@"\Data Siswa");
        }
        private static decimal kdp(string target, DataTable tablenilai)
        {
            decimal kdp1 = tablenilai.Select().Where(p => p[target] != DBNull.Value && Convert.ToInt32(p[target]) > 0).Select(c => Convert.ToDecimal(c[target])).Average();
            return kdp1;
        }
    }
}
