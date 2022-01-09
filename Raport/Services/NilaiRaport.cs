using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data.SQLite;
using Raport.Helper;
using System.Windows;

namespace Raport.Services
{
    public static class NilaiRaport
    {
        public static SQLiteDataAdapter adapter = new SQLiteDataAdapter();
        public static SQLiteCommandBuilder commandBuilder;
        private static DataSet dataset = new();
        public static void tempData()
        {
            dataset.Clear();
            KD("pkn3", "kd_pkn3", Database.kd_pkn3);
            KD("mtk3", "kd_mtk3", Database.kd_mtk3);
            KD("agm3", "kd_agm3", Database.kd_agm3);
            KD("bi3", "kd_bi3", Database.kd_bi3);
            KD("ipa3", "kd_ipa3", Database.kd_ipa3);
            KD("ips3", "kd_ips3", Database.kd_ips3);
            KD("sbdp3", "kd_sbdp3", Database.kd_sbdp3);
            KD("pjok3", "kd_pjok3", Database.kd_pjok3);
            KD("bjr3", "kd_bjr3", Database.kd_bjr3);
            KD("bing3", "kd_bing3", Database.kd_bing3);
            KD("bta3", "kd_bta3", Database.kd_bta3);

            KD("pkn4", "kd_pkn4", Database.kd_pkn4);
            KD("mtk4", "kd_mtk4", Database.kd_mtk4);
            KD("agm4", "kd_agm4", Database.kd_agm4);
            KD("bi4", "kd_bi4", Database.kd_bi4);
            KD("ipa4", "kd_ipa4", Database.kd_ipa4);
            KD("ips4", "kd_ips4", Database.kd_ips4);
            KD("sbdp4", "kd_sbdp4", Database.kd_sbdp4);
            KD("pjok4", "kd_pjok4", Database.kd_pjok4);
            KD("bjr4", "kd_bjr4", Database.kd_bjr4);
            KD("bing4", "kd_bing4", Database.kd_bing4);
            KD("bta4", "kd_bta4", Database.kd_bta4);
        }
        private static void KD(String target, String tablename, int limit)
        {
            try
            {
                string query = "select id,kd," + target + " from kompetensi_dasar";
                adapter.SelectCommand = new SQLiteCommand(query, Connection.sqlite);
                commandBuilder = new SQLiteCommandBuilder(adapter);
                adapter.Fill(dataset, tablename);

            }
            catch (SQLiteException ex)
            {
                Console.WriteLine(ex);
            }
        }
        public static void print()
        {
            //try { 
            tempData();
            DataTable dt = new DataTable();
            DataTable dtnew = new DataTable();
            DataTable dt2 = new DataTable();
            
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
            dt.Columns.Add("KI1_1", typeof(string));
            dt.Columns.Add("KI1_2", typeof(string));
            dt.Columns.Add("J_D_TJ", typeof(string));
            dt.Columns.Add("PD_S_PS", typeof(string));
            dt.Columns.Add("nm", typeof(string));
            dt.Columns.Add("nip", typeof(string));
            dt.Columns.Add("tahun", typeof(string));
            dt.Columns.Add("semester", typeof(string));
            dt.Columns.Add("kelas", typeof(string));
            dt.Columns.Add("jlh", typeof(string));

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dt.Rows[i]["bta_3"] = dt2.Rows[i]["bta_3"];
                dt.Rows[i]["bta_4"] = dt2.Rows[i]["bta_4"];
            }
            foreach (DataRow drRow in dt.Rows)

            {
                for (int i = 2; i < dt.Columns.Count - 1; i++)
                {
                    double rowValue;

                    if (double.TryParse(drRow[i].ToString(), out rowValue))
                    {

                        drRow[i] = Math.Round(rowValue);

                    }

                }
            }
            string[] mapel = { "agm", "pkn", "bi", "mtk", "ipa", "ips", "sbdp", "pjok", "bjr", "bing", "bta" };
            for (int i = 0; i < mapel.Length; i++)
            {
                for (int j = 0; j <= 9; j++)
                {
                    string name = "kd_" + mapel[i] + j;
                    string name2 = "kdes_" + mapel[i] + j;
                    dt.Columns.Add(name);
                    dt.Columns.Add(name2);
                    
                }
            }
            for (int i = 0; i < mapel.Length; i++)
            {
                dt.Columns.Add("pr_" + mapel[i] + "1");
                dt.Columns.Add("pr_" + mapel[i] + "2");
            }
            dt.Columns.Add("pramuka", typeof(string));
            dt.Columns.Add("karate", typeof(string));
            dt.Columns.Add("pmr", typeof(string));
            dt.Columns.Add("tinggi", typeof(string));
            dt.Columns.Add("berat_badan", typeof(string));
            dt.Columns.Add("penglihatan", typeof(string));
            dt.Columns.Add("pendengaran", typeof(string));
            dt.Columns.Add("gigi", typeof(string));
            dt.Columns.Add("lainnya", typeof(string));
            dt.Columns.Add("s", typeof(string));
            dt.Columns.Add("i", typeof(string));
            dt.Columns.Add("a", typeof(string));
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string[] mapel_tt = { Constants.agm_title, Constants.pkn_title, Constants.bi_title, Constants.mtk_title, Constants.ipa_title, Constants.ips_title, Constants.sbdp_title, Constants.pjok_title, Constants.bjr_title, Constants.bing_title, Constants.bta_title };
                string[] mapel_tt2 = { Constants.agm_title2, Constants.pkn_title2, Constants.bi_title2, Constants.mtk_title2, Constants.ipa_title2, Constants.ips_title2, Constants.sbdp_title2, Constants.pjok_title2, Constants.bjr_title2, Constants.bing_title2, Constants.bta_title2 };
                for (int j = 0; j < mapel.Length; j++)
                {
                    dt.Rows[i]["pr_" + mapel[j] + "1"] = predikat(dt.Rows[i][mapel[j] + "_3"].ToString());
                    dt.Rows[i]["pr_" + mapel[j] + "2"] = predikat(dt.Rows[i][mapel[j] + "_4"].ToString());
                    dt.Rows[i]["semester"] = Database.semester;
                    dt.Rows[i]["tahun"] = Database.tahun;
                    dt.Rows[i]["nm"] = Database.wali_kelas;
                    dt.Rows[i]["nip"] = Database.nip_wali_kelas;
                    dt.Rows[i]["kelas"] = Database.kelas;
                    dt.Rows[i]["pramuka"] = Connection.dataset.Tables[Constants.absen_title].Rows[i]["pramuka"].ToString();
                    dt.Rows[i]["pmr"] = Connection.dataset.Tables[Constants.absen_title].Rows[i]["pmr"].ToString();
                    dt.Rows[i]["karate"] = Connection.dataset.Tables[Constants.absen_title].Rows[i]["karate"].ToString();
                    dt.Rows[i]["tinggi"] = Connection.dataset.Tables[Constants.absen_title].Rows[i]["tinggi"].ToString();
                    dt.Rows[i]["berat_badan"] = Connection.dataset.Tables[Constants.absen_title].Rows[i]["berat_badan"].ToString();
                    dt.Rows[i]["penglihatan"] = Connection.dataset.Tables[Constants.absen_title].Rows[i]["penglihatan"].ToString();
                    dt.Rows[i]["pendengaran"] = Connection.dataset.Tables[Constants.absen_title].Rows[i]["pendengaran"].ToString();
                    dt.Rows[i]["gigi"] = Connection.dataset.Tables[Constants.absen_title].Rows[i]["gigi"].ToString();
                    dt.Rows[i]["lainnya"] = Connection.dataset.Tables[Constants.absen_title].Rows[i]["lainnya"].ToString();
                    dt.Rows[i]["s"] = Connection.dataset.Tables[Constants.absen_title].Rows[i]["s"].ToString();
                    dt.Rows[i]["i"] = Connection.dataset.Tables[Constants.absen_title].Rows[i]["i"].ToString();
                    dt.Rows[i]["a"] = Connection.dataset.Tables[Constants.absen_title].Rows[i]["a"].ToString();
                    dt.Rows[i]["jlh"] = dt.Rows.Count;
                    dt.Rows[i]["KI1_1"] = Sikap(Connection.dataset.Tables[Constants.sikap_title].Rows[i]["sikap1"].ToString());
                    dt.Rows[i]["KI1_2"] = Sikap(Connection.dataset.Tables[Constants.sikap_title].Rows[i]["sikap2"].ToString());
                    dt.Rows[i]["J_D_TJ"] = Sikap(Connection.dataset.Tables[Constants.sikap_title].Rows[i]["sikap3"].ToString());
                    dt.Rows[i]["PD_S_PS"] = Sikap(Connection.dataset.Tables[Constants.sikap_title].Rows[i]["sikap4"].ToString());
                    for (int k = 0; k <= 9; k++)
                    {
                        if (k < 5)
                        {
                            dt.Rows[i]["kdes_" + mapel[j] + k] = deskripsi(Connection.dataset.Tables[mapel_tt[j]].Rows[i]["kdp" + (k + 1)].ToString());
                            dt.Rows[i]["kdes_" + mapel[j] + (k + 5)] = deskripsi(Connection.dataset.Tables[mapel_tt2[j]].Rows[i]["kdk" + (k + 1)].ToString());
                            dt.Rows[i]["kd_" + mapel[j] + k] = dataset.Tables["kd_" + mapel[j] + "3"].Rows[k][mapel[j] + "3"].ToString();
                            dt.Rows[i]["kd_" + mapel[j] + (k + 5)] = dataset.Tables["kd_" + mapel[j] + "4"].Rows[k][mapel[j] + "4"].ToString();
                        }
                        else
                        {

                        }
                    }

                }
            }
            dt.Columns.Add("total", typeof(double), "agm_3+ pkn_3+ bi_3+ mtk_3+ ipa_3+ ips_3+ sbdp_3+ pjok_3+ bjr_3+ bing_3+ bta_3+agm_4+ pkn_4+ bi_4+ mtk_4+ ipa_4+ ips_4+ sbdp_4+ pjok_4+ bjr_4+ bing_4+ bta_4");
            dt.Columns.Add("peringkat", typeof(double));
            dt.DefaultView.Sort = "total DESC";
            int rank = 1;
            foreach (DataRowView dr in dt.DefaultView)
            {
                dr["peringkat"] = rank;
                rank++;
            }
            StringBuilder sb = new StringBuilder();

            IEnumerable<string> columnNames = dt.Columns.Cast<DataColumn>().
                                              Select(column => column.ColumnName);
            sb.AppendLine(string.Join(";", columnNames));

            foreach (DataRow row in dt.Rows)
            {
                IEnumerable<string> fields = row.ItemArray.Select(field => field.ToString());
                sb.AppendLine(string.Join(";", fields));
            }

            File.WriteAllText(@"Templates\TempData.csv", sb.ToString());
        //}
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.ToString());
        //    }
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
        }
        private static string deskripsi(string target)
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
            else if (target == "0")
            {
                result = " ";
            }
            return result;
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
    }
}
