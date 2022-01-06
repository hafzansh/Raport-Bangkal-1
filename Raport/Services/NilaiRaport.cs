using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data.SQLite;
using Raport.Helper;

namespace Raport.Services
{
    public static class NilaiRaport
    {
        public static SQLiteDataAdapter adapter = new SQLiteDataAdapter();
        public static SQLiteCommandBuilder commandBuilder;
        public static void print()
        {
            DataTable dt = new DataTable();
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
                            dt.Rows[i]["kdes_" + mapel[j] + (k+5)] = deskripsi(Connection.dataset.Tables[mapel_tt2[j]].Rows[i]["kdk" + (k + 1)].ToString());
                            dt.Rows[i]["kd_" + mapel[j] + k] = Connection.dataset.Tables["kd_" + mapel[j] + "3"].Rows[k][mapel[j] + "3"].ToString();
                            dt.Rows[i]["kd_" + mapel[j] + (k + 5)] = Connection.dataset.Tables["kd_" + mapel[j] + "4"].Rows[k][mapel[j] + "4"].ToString();
                        }
                        else
                        {
                            
                        }
                    }
                    
                }
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

            File.WriteAllText("test.csv", sb.ToString());
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
