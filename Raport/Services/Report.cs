using iText.IO.Image;
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
using System.Globalization;
using System.IO;
using System.Windows;
using TextAlignment = iText.Layout.Properties.TextAlignment;
using VerticalAlignment = iText.Layout.Properties.VerticalAlignment;

namespace Raport.Services
{
    class Report
    {
        private static bool status = false;
        private static bool status2 = false;
        public static DataTable table;
        public static PdfFont SetFont()
        {
            return PdfFontFactory.CreateFont(@"C:\Windows\Fonts\times.ttf");
        }
        public static PdfFont SetBold()
        {
            return PdfFontFactory.CreateFont(@"C:\Windows\Fonts\timesbd.ttf");
        }
        public static void CreateDasis(DataTable dtc)
        {
            try {
                DataTable dt = new();
                dt.Clear();
                dt = dtc.Copy();
            PdfFont font = SetFont();
            PdfFont bold = SetBold();
            string path = Constants.folderpath + Database.db_name + @"\Data Siswa";
            bool exists = Directory.Exists(path);
            if (!exists)
                Directory.CreateDirectory(path);
            PdfWriter writer = new PdfWriter(path + @"\DATA SISWA.pdf");
            PdfDocument pdf = new PdfDocument(writer);
            pdf.SetDefaultPageSize(PageSize.A3.Rotate());
            Document document = new Document(pdf);
            document.SetTopMargin(20);

            Paragraph header = new Paragraph("PEMERINTAH BANJARBARU\nDINAS PENDIDIKAN KOTA BANJARBARU")
                .SetTextAlignment(TextAlignment.CENTER).SetFontSize(13)
                .SetFont(font);
            header.Add(new Text("\nSD NEGERI 1 BANGKAL").SetFont(bold)).SetTextAlignment(TextAlignment.CENTER).SetFontSize(13);
            
            Paragraph title = new Paragraph("DATA SISWA").SetTextAlignment(TextAlignment.CENTER).SetFontSize(18).SetFont(bold);
            Table tables = new Table(UnitValue.CreatePercentArray(Constants.header_width));
            LineSeparator ls = new LineSeparator(new SolidLine());
                tables.SetWidth(UnitValue.CreatePercentValue(100));
            //tables.SetFixedLayout();
            Paragraph newline = new Paragraph(new Text("\n"));            
            Paragraph semester = new Paragraph();
            Paragraph kelas = new Paragraph();
            Paragraph tahun = new Paragraph();            
            Paragraph wali = new Paragraph();


                Table head = new Table(2, true);
            wali.Add("Wali Kelas").SetFont(font);
            wali.Add(new Tab());
            wali.Add(new Text(": " + Database.wali_kelas).SetFont(bold));

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
               .SetTextAlignment(TextAlignment.LEFT)
               .Add(wali)
               .SetBorder(Border.NO_BORDER);
            Cell cell12 = new Cell(1, 1)
               .SetTextAlignment(TextAlignment.RIGHT)
               .Add(semester)
               .SetBorder(Border.NO_BORDER);
            Cell cell21 = new Cell(1, 1)
               .SetTextAlignment(TextAlignment.LEFT)
               .Add(kelas)
               .SetBorder(Border.NO_BORDER);
            Cell cell22 = new Cell(1, 1)
               .SetTextAlignment(TextAlignment.RIGHT)
               .Add(tahun)
               .SetBorder(Border.NO_BORDER);
            head.SetWidth(UnitValue.CreatePercentValue(100));
            head.AddCell(cell11);
            head.AddCell(cell12);
            head.AddCell(cell21);
            head.AddCell(cell22);
            head.SetFontSize(16);
                dt.Columns.Add("no", typeof(int)).SetOrdinal(0);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dt.Rows[i]["no"] = i + 1;
                }
                for (int h = 0; h < dt.Columns.Count; h++)
            {                    
                Cell cells = new Cell(1, 1).SetTextAlignment(TextAlignment.CENTER).SetBackgroundColor(ColorConstants.LIGHT_GRAY).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph(Constants.header_title[h]));
                tables.AddHeaderCell(cells);
            }
            foreach (DataRow r in dt.Rows)
            {
                if (dt.Rows.Count > 0)
                {
                    for (int h = 0; h < dt.Columns.Count; h++)
                    {
                            if (h == 4)
                            {
                                DateTime date = DateTime.Parse(r[h].ToString(), CultureInfo.CreateSpecificCulture("en-US"));
                            //DateTime date = Convert.ToDateTime(r[h]);
                                Cell cells = new Cell(1, 1).SetTextAlignment(TextAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph(date.ToString("dd/MM/yyyy")));
                                cells.SetHeight(40);
                                tables.AddCell(cells);
                            }
                            else if (h==2)
                            {
                                Cell cells = new Cell(1, 1).SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("         "+r[h].ToString()));
                                cells.SetHeight(40);
                                tables.AddCell(cells);
                            }
                            else
                            {
                                Cell cells = new Cell(1, 1).SetTextAlignment(TextAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph(r[h].ToString()));
                                cells.SetHeight(40);
                                tables.AddCell(cells);
                            }
                        
                    }
                }
            }
                
                tables.SetFontSize(10);
                header.Add(new Text("\nJln. Mistar Cokrukosumo Rt. 3 Rw. 1 Bangkal, Kecamatan Cempaka Kode Pos 70732").SetFont(font)).SetTextAlignment(TextAlignment.CENTER).SetFontSize(13);
                header.Add(new Text("\nKota Banjarbaru Kalimantan Selatan").SetFont(font)).SetTextAlignment(TextAlignment.CENTER).SetFontSize(13);
                string ImageFile = @"Resources\tut_logo_full.png";
                string ImageFile2 = @"Resources\bjb_logo.png";
                iText.IO.Image.ImageData data = iText.IO.Image.ImageDataFactory.Create(ImageFile);
                iText.IO.Image.ImageData data2 = iText.IO.Image.ImageDataFactory.Create(ImageFile2);
                Image img = new Image(data);
                Image img2 = new Image(data2);
                img2.SetFixedPosition(40, 725);
                img.SetHeight(90);
                img.SetFixedPosition(1070, 725);
                img2.SetHeight(90);
                document.Add(img);
                document.Add(img2);
                document.Add(header);
            document.Add(ls);
            document.Add(title);
            document.Add(head);
            document.Add(newline);
            document.Add(tables);
            document.Close();
                Constants.openFolder(@"\Data Siswa");
            }catch (Exception ex)
            {
                MessageBox.Show("Data nilai masih belum lengkap!, Detail "+ ex.ToString());
            }
        }
        public static void createRaport(int i)
        {
            try
            {
                int val = 0;
                DataTable table2 = new DataTable();
                DataTable kd = new DataTable();
                DataTable sikap = new DataTable();
                table = table2;
                bool exists = Directory.Exists(Constants.folderpath + Database.db_name +@"\Nilai Raport");

                if (!exists)
                    Directory.CreateDirectory(Constants.folderpath + Database.db_name + @"\Nilai Raport");
                string query = "SELECT induk,nama FROM data_siswa";
                Connection.adapter.SelectCommand = new SQLiteCommand(query, Connection.sqlite);
                Connection.commandBuilder = new SQLiteCommandBuilder(Connection.adapter);
                Connection.adapter.Fill(table);

                string query2 = "SELECT * from kompetensi_dasar";
                Connection.adapter.SelectCommand = new SQLiteCommand(query2, Connection.sqlite);
                Connection.commandBuilder = new SQLiteCommandBuilder(Connection.adapter);
                Connection.adapter.Fill(kd);

                string query3 = "SELECT * from data_sikap";
                Connection.adapter.SelectCommand = new SQLiteCommand(query3, Connection.sqlite);
                Connection.commandBuilder = new SQLiteCommandBuilder(Connection.adapter);
                Connection.adapter.Fill(sikap);
                Constants.rowC = table.Rows.Count;
                fillHeader();
                fillTable();
                status = true;
                status2 = true;
                object filename = Environment.CurrentDirectory + @"\Templates\Raport.docx";

                var wordApp = new Microsoft.Office.Interop.Word.Application();
                var wrdSelect = wordApp.Selection;
                var document = new Microsoft.Office.Interop.Word.Document();

                //wordApp.Visible = true;
                Modal.ProgressModal(progress =>
                {
                        progress.Report(val + 8.25);
                        document = wordApp.Documents.Add(filename);
                        string[] mapel = { "agm", "pkn", "bi", "mtk", "ipa", "ips", "sbdp", "pjok", "bjr", "bing", "bta" };
                        string[] mapel_tt = { Constants.agm_title, Constants.pkn_title, Constants.bi_title, Constants.mtk_title, Constants.ipa_title, Constants.ips_title, Constants.sbdp_title, Constants.pjok_title, Constants.bjr_title, Constants.bing_title, Constants.bta_title };
                        string[] mapel_tt2 = { Constants.agm_title2, Constants.pkn_title2, Constants.bi_title2, Constants.mtk_title2, Constants.ipa_title2, Constants.ips_title2, Constants.sbdp_title2, Constants.pjok_title2, Constants.bjr_title2, Constants.bing_title2, Constants.bta_title2 };
                        for (int j = 0; j < mapel.Length; j++)
                        {
                            foreach (Microsoft.Office.Interop.Word.Field _field in document.Fields)
                            {                            
                                if (_field.Code.Text.Contains(mapel[j] + "_3"))
                                {
                                    _field.Select();
                                    wordApp.Selection.TypeText(table.Rows[i][mapel[j] + "3"].ToString());
                                }
                                else if (_field.Code.Text.Contains("pr_"+mapel[j]+"1"))
                                {
                                    _field.Select();
                                    wordApp.Selection.TypeText(predikat(table.Rows[i][mapel[j] + "3"].ToString()));
                                    
                                }
                                else if (_field.Code.Text.Contains("kdes_"+mapel[j]+"1"))
                                {
                                    _field.Select();
                                    wordApp.Selection.TypeText(deskripsi(Connection.dataset.Tables[mapel_tt[j]].Rows[i]["kdp1"].ToString()));
                                }
                                else if (_field.Code.Text.Contains("kd_"+mapel[j]+"1"))
                                {
                                    _field.Select();
                                    wordApp.Selection.TypeText(kd.Rows[0][mapel[j]+"3"].ToString());
                                }

                                else if (_field.Code.Text.Contains("kdes_" + mapel[j] + "2"))
                                {
                                    _field.Select();
                                    wordApp.Selection.TypeText(deskripsi(Connection.dataset.Tables[mapel_tt[j]].Rows[i]["kdp2"].ToString()));
                                }
                                else if (_field.Code.Text.Contains("kd_" + mapel[j] + "2"))
                                {
                                    _field.Select();
                                    wordApp.Selection.TypeText(kd.Rows[1][mapel[j] + "3"].ToString());
                                }

                                else if (_field.Code.Text.Contains("kdes_" + mapel[j] + "3"))
                                {
                                    _field.Select();
                                    wordApp.Selection.TypeText(deskripsi(Connection.dataset.Tables[mapel_tt[j]].Rows[i]["kdp3"].ToString()));
                                }
                                else if (_field.Code.Text.Contains("kd_" + mapel[j] + "3"))
                                {
                                    _field.Select();
                                    wordApp.Selection.TypeText(kd.Rows[2][mapel[j] + "3"].ToString());
                                }

                                else if (_field.Code.Text.Contains("kdes_" + mapel[j] + "4"))
                                {
                                    _field.Select();
                                    wordApp.Selection.TypeText(deskripsi(Connection.dataset.Tables[mapel_tt[j]].Rows[i]["kdp4"].ToString()));
                                }
                                else if (_field.Code.Text.Contains("kd_" + mapel[j] + "4"))
                                {
                                    _field.Select();
                                    wordApp.Selection.TypeText(kd.Rows[3][mapel[j] + "3"].ToString());
                                }

                                else if (_field.Code.Text.Contains("kdes_" + mapel[j] + "5"))
                                {
                                    _field.Select();
                                    wordApp.Selection.TypeText(deskripsi(Connection.dataset.Tables[mapel_tt[j]].Rows[i]["kdp5"].ToString()));
                                }
                                else if (_field.Code.Text.Contains("kd_" + mapel[j] + "5"))
                                {
                                    _field.Select();
                                    wordApp.Selection.TypeText(kd.Rows[4][mapel[j] + "3"].ToString());
                                }


                                else if (_field.Code.Text.Contains(mapel[j] + "_4"))
                                {
                                    _field.Select();
                                    wordApp.Selection.TypeText(table.Rows[i][mapel[j] + "4"].ToString());
                                }
                                else if (_field.Code.Text.Contains("pr_" + mapel[j] + "2"))
                                {
                                    _field.Select();
                                    wordApp.Selection.TypeText(predikat(table.Rows[i][mapel[j] + "4"].ToString()));

                                }
                                else if (_field.Code.Text.Contains("kdes_" + mapel[j] + "6"))
                                {
                                    _field.Select();
                                    wordApp.Selection.TypeText(deskripsi(Connection.dataset.Tables[mapel_tt2[j]].Rows[i]["kdk1"].ToString()));
                                }
                                else if (_field.Code.Text.Contains("kd_" + mapel[j] + "6"))
                                {
                                    _field.Select();
                                    wordApp.Selection.TypeText(kd.Rows[0][mapel[j] + "4"].ToString());
                                }

                                else if (_field.Code.Text.Contains("kdes_" + mapel[j] + "7"))
                                {
                                    _field.Select();
                                    wordApp.Selection.TypeText(deskripsi(Connection.dataset.Tables[mapel_tt2[j]].Rows[i]["kdk2"].ToString()));
                                }
                                else if (_field.Code.Text.Contains("kd_" + mapel[j] + "7"))
                                {
                                    _field.Select();
                                    wordApp.Selection.TypeText(kd.Rows[1][mapel[j] + "4"].ToString());
                                }

                                else if (_field.Code.Text.Contains("kdes_" + mapel[j] + "8"))
                                {
                                    _field.Select();
                                    wordApp.Selection.TypeText(deskripsi(Connection.dataset.Tables[mapel_tt2[j]].Rows[i]["kdk3"].ToString()));
                                }
                                else if (_field.Code.Text.Contains("kd_" + mapel[j] + "8"))
                                {
                                    _field.Select();
                                    wordApp.Selection.TypeText(kd.Rows[2][mapel[j] + "4"].ToString());
                                }

                                else if (_field.Code.Text.Contains("kdes_" + mapel[j] + "9"))
                                {
                                    _field.Select();
                                    wordApp.Selection.TypeText(deskripsi(Connection.dataset.Tables[mapel_tt2[j]].Rows[i]["kdk4"].ToString()));
                                }
                                else if (_field.Code.Text.Contains("kd_" + mapel[j] + "9"))
                                {
                                    _field.Select();
                                    wordApp.Selection.TypeText(kd.Rows[3][mapel[j] + "4"].ToString());
                                }

                                else if (_field.Code.Text.Contains("kdes_" + mapel[j] + "0"))
                                {
                                    _field.Select();
                                    wordApp.Selection.TypeText(deskripsi(Connection.dataset.Tables[mapel_tt2[j]].Rows[i]["kdk5"].ToString()));
                                }
                                else if (_field.Code.Text.Contains("kd_" + mapel[j] + "0"))
                                {
                                    _field.Select();
                                    wordApp.Selection.TypeText(kd.Rows[4][mapel[j] + "4"].ToString());
                                }
                                else if (_field.Code.Text.Contains("Induk"))
                                {
                                    _field.Select();
                                    wordApp.Selection.TypeText(table.Rows[i]["induk"].ToString());
                                }
                                else if (_field.Code.Text.Contains("nama"))
                                {
                                    _field.Select();
                                    wordApp.Selection.TypeText(table.Rows[i]["nama"].ToString());
                                }
                                else if (_field.Code.Text.Contains("Peringkat"))
                                {
                                    _field.Select();
                                    wordApp.Selection.TypeText(table.Rows[i]["Rank"].ToString());
                                }
                                else if (_field.Code.Text.Contains("jlh"))
                                {
                                    _field.Select();
                                    wordApp.Selection.TypeText(Constants.rowC.ToString());
                                }
                                else if (_field.Code.Text.Contains("kelas"))
                                {
                                    _field.Select();
                                    wordApp.Selection.TypeText(Database.kelas);
                                }
                                else if (_field.Code.Text.Contains("semester"))
                                {
                                    _field.Select();
                                    wordApp.Selection.TypeText(Database.semester);
                                }
                                else if (_field.Code.Text.Contains("tahun"))
                                {
                                    _field.Select();
                                    wordApp.Selection.TypeText(Database.tahun);
                                }
                                else if (_field.Code.Text.Contains("nm"))
                                {
                                    _field.Select();
                                    wordApp.Selection.TypeText(Database.wali_kelas);
                                }
                                else if (_field.Code.Text.Contains("nip"))
                                {
                                    _field.Select();
                                    wordApp.Selection.TypeText(Database.nip_wali_kelas);
                                }
                                else if (_field.Code.Text.Contains("KI1_1"))
                                {
                                    _field.Select();
                                    wordApp.Selection.TypeText(Sikap(sikap.Rows[i]["sikap1"].ToString()));
                                }
                                else if (_field.Code.Text.Contains("KI1_2"))
                                {
                                    _field.Select();
                                    wordApp.Selection.TypeText(Sikap(sikap.Rows[i]["sikap2"].ToString()));
                                }
                                else if (_field.Code.Text.Contains("J_D_TJ"))
                                {
                                    _field.Select();
                                    wordApp.Selection.TypeText(Sikap(sikap.Rows[i]["sikap3"].ToString()));
                                }
                                else if (_field.Code.Text.Contains("PD_S_PS"))
                                {
                                    _field.Select();
                                    wordApp.Selection.TypeText(Sikap(sikap.Rows[i]["sikap4"].ToString()));
                                }

                            }                        
                            val += 9;
                            progress.Report(val);
                        };
                        document.ExportAsFixedFormat(Constants.folderpath + Database.db_name + @"\Nilai Raport\" + table.Rows[i]["nama"] + ".PDF", Microsoft.Office.Interop.Word.WdExportFormat.wdExportFormatPDF);
                        document.Close(Microsoft.Office.Interop.Word.WdSaveOptions.wdDoNotSaveChanges, Microsoft.Office.Interop.Word.WdOriginalFormat.wdOriginalDocumentFormat, false);
                    
                    wordApp.Quit();
                });
                table.Dispose();
                table2.Dispose();
                kd.Dispose();
                val = 0;
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show("Ada kesalahan" + ex);
            }
        }
        private static string predikat(string target)
        {
            string result = "Predikat";
            if (double.Parse(target) > 90)
            {
                result = "A";
            }
            else if (double.Parse(target) >= 80)
            {
                result = "B";
            }
            else if (double.Parse(target) >= 70)
            {
                result = "C";
            }
            else if (double.Parse(target) >= 10)
            {
                result = "D";
            }
            else
            {
                result = " ";
            } 
            return result;
        }        
        private static string Sikap(string target)
        {
            string result = "Sikap";
            if (double.Parse(target) == 4)
            {
                result = "Sangat Baik Dalam";
            }
            else if (double.Parse(target) == 3)
            {
                result = "Baik Dalam";
            }
            else if (double.Parse(target) == 2)
            {
                result = "Cukup Dalam";
            }
            else if (double.Parse(target) == 1)
            {
                result = "Perlu Bimbingan Dalam";
            }
            return result;
        }        private static string deskripsi(string target)
        {
            string result = "Deskripsi";
            if (double.Parse(target) > 85)
            {
                result = "Sangat Baik Dalam";
            }
            else if (double.Parse(target) >= 75)
            {
                result = "Baik Dalam";
            }
            else if (double.Parse(target) >= 65)
            {
                result = "Cukup Dalam";
            }
            else if (double.Parse(target) > 10)
            {
                result = "Perlu Bimbingan Dalam";
            }
            else if(target == null)
            {
                result = " ";
            }
            return result;
        }
        private static void fillTable()
        {
            fillData();
            
        }
        private static void fillData()
        {
            List<string> title = new List<string>() {
                "view_mp_agama","view_mp_pkn","view_mp_bi","view_mp_mtk","view_mp_ipa","view_mp_ips",
                "view_mp_sbdp","view_mp_pjok","view_mp_bjr","view_mp_bing","view_mp_bta"};
            //Modal.ProgressModal(progress =>
            //{
                addData(title[0], 3, 4);
                //progress.Report(title[0]);
                addData(title[1], 5, 6);
                //progress.Report(title[1]);
                addData(title[2], 7, 8);
                //progress.Report(title[2]);
                addData(title[3], 9, 10);
                //progress.Report(title[3]);
                addData(title[4], 11, 12);
                //progress.Report(title[4]);
                addData(title[5], 13, 14);
                //progress.Report(title[5]);
                addData(title[6], 15, 16);
                //progress.Report(title[6]);
                addData(title[7], 17, 18);
                //progress.Report(title[7]);
                addData(title[8], 19, 20);
                //progress.Report(title[8]);
                addData(title[9], 21, 22);
                //progress.Report(title[9]);
                addData(title[10], 23, 24);
                //progress.Report(title[10]);
            //});
            for (int i = 0; i < Constants.rowC; i++)
            {
                table.Rows[i][0] = i + 1;
            }
            for (int i = 0; i < Constants.rowC; i++)
            {
                long a = 0;
                for (int j = 0; j < Constants.header_list.Length; j++)
                {
                    a += table.Rows[i].Field<long>(Constants.header_list[j]);
                }
                table.Rows[i][25] = a;
            }
            table.DefaultView.Sort = "total DESC";
            int rank = 1;
            foreach (DataRowView dr in table.DefaultView)
            {
                dr["Rank"] = rank;
                rank++;
            }
            table.DefaultView.Sort = "No ASC";
            table.AcceptChanges();
        }
        private static void addData(string target, int c, int d)
        {
            Connection.sqlite.Open();
            string query = "SELECT nilai1,nilai2 from " + target;
            var cmd = new SQLiteCommand(query, Connection.sqlite);
            using SQLiteDataReader rdr = cmd.ExecuteReader();
            List<double> list1 = new List<double>();
            List<double> list2 = new List<double>();
            while (rdr.Read())
            {
                var a = rdr.GetDouble(0);
                var b = rdr.GetDouble(1);
                list1.Add(a);
                list2.Add(b);
            }

            Connection.sqlite.Close();
            for (int j = 0; j < Constants.rowC; j++)
            {
                table.Rows[j][c] = list1[j];
                table.Rows[j][d] = list2[j];
            }
        }
        private static void fillHeader()
        {

                table.Columns.Add("No", typeof(int)).SetOrdinal(0);
                table.Columns.Add("agm3", typeof(long));
                table.Columns.Add("agm4", typeof(long));
                table.Columns.Add("pkn3", typeof(long));
                table.Columns.Add("pkn4", typeof(long));
                table.Columns.Add("bi3", typeof(long));
                table.Columns.Add("bi4", typeof(long));
                table.Columns.Add("mtk3", typeof(long));
                table.Columns.Add("mtk4", typeof(long));
                table.Columns.Add("ipa3", typeof(long));
                table.Columns.Add("ipa4", typeof(long));
                table.Columns.Add("ips3", typeof(long));
                table.Columns.Add("ips4", typeof(long));
                table.Columns.Add("sbdp3", typeof(long));
                table.Columns.Add("sbdp4", typeof(long));
                table.Columns.Add("pjok3", typeof(long));
                table.Columns.Add("pjok4", typeof(long));
                table.Columns.Add("bjr3", typeof(long));
                table.Columns.Add("bjr4", typeof(long));
                table.Columns.Add("bing3", typeof(long));
                table.Columns.Add("bing4", typeof(long));
                table.Columns.Add("bta3", typeof(long));
                table.Columns.Add("bta4", typeof(long));
                table.Columns.Add("total", typeof(long));
                table.Columns.Add("Rank", typeof(int));
                                  
        }
    }

}
