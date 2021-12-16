using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Text;
using System.Windows;

namespace Raport.Helper
{
    public class Database
    {
        public static string db_name;
        public static string wali_kelas;
        public static string nip_wali_kelas;
        public static string kepala_sekolah;
        public static string nip_kepala_sekolah;
        public static string semester;
        public static string tahun;
        public static string kelas;
        public static int kd_agm3;
        public static int kd_agm4;
        public static int kd_pkn3;
        public static int kd_pkn4;
        public static int kd_bi3;
        public static int kd_bi4;
        public static int kd_mtk3;
        public static int kd_mtk4;
        public static int kd_ipa3;
        public static int kd_ipa4;
        public static int kd_ips3;
        public static int kd_ips4;
        public static int kd_sbdp3;
        public static int kd_sbdp4;
        public static int kd_pjok3;
        public static int kd_pjok4;
        public static int kd_bjr3;
        public static int kd_bjr4;
        public static int kd_bing3;
        public static int kd_bing4;
        public static int kd_bta3;
        public static int kd_bta4;
                
        public static void CreateDB()
        {
            if (!File.Exists(Constants.folderpath + db_name))
            {
                try
                {
                    using SQLiteConnection sql = new SQLiteConnection("Data Source=" + Constants.folderpath + db_name + ".db" + ";" + Constants.dbVersion);
                    sql.Open();                    
                    SQLiteCommand command = new SQLiteCommand(Constants.dasis_query, sql);
                    command.ExecuteNonQuery();
                    command.CommandText = Constants.absen_query;
                    command.ExecuteNonQuery();
                    command.CommandText = Constants.sikap_query;
                    command.ExecuteNonQuery();
                    CreateTable(command, Constants.agm);
                    CreateKDT(command, kd_agm3, kd_agm4, Constants.agm);
                    CreateTable(command, Constants.pkn);
                    CreateKDT(command, kd_pkn3, kd_pkn4, Constants.pkn);
                    CreateTable(command, Constants.mtk);
                    CreateKDT(command, kd_mtk3, kd_mtk4, Constants.mtk);
                    CreateTable(command, Constants.bi);
                    CreateKDT(command, kd_bi3, kd_bi4, Constants.bi);
                    CreateTable(command, Constants.ipa);
                    CreateKDT(command, kd_ipa3, kd_ipa4, Constants.ipa);
                    CreateTable(command, Constants.ips);
                    CreateKDT(command, kd_ips3, kd_ips4, Constants.ips);
                    CreateTable(command, Constants.sbdp);
                    CreateKDT(command, kd_sbdp3, kd_sbdp4, Constants.sbdp);
                    CreateTable(command, Constants.pjok);
                    CreateKDT(command, kd_pjok3, kd_pjok4, Constants.pjok);
                    CreateTable(command, Constants.bjr);
                    CreateKDT(command, kd_bjr3, kd_bjr4, Constants.bjr);
                    CreateTable(command, Constants.bing);
                    CreateKDT(command, kd_bing3, kd_bing4, Constants.bing);
                    CreateTable(command, Constants.bta);
                    CreateKDT(command, kd_bta3, kd_bta4, Constants.bta);
                    CreateTrigger(command);
                }
                catch(Exception ex)
                {
                    MessageBox.Show("Error, " + ex);
                }
            }
            else
            {
                MessageBox.Show("Raport sudah ada!");
            }

        }
        public static void CreateTable(SQLiteCommand command,string table_name)
        {
            command.CommandText = TableQuery(table_name);
            command.ExecuteNonQuery();            
        }
        public static void CreateKDT(SQLiteCommand command, int kdp, int kdk,string table_name)
        {
            for (int i = 1; i <= kdp; i++)
            {
                string q = "alter table " + table_name + " add kdp" + i + " double;";
                command.CommandText = q;
                command.ExecuteNonQuery();
            }
            for (int i = 1; i <= kdp; i++)
            {
                string q = "alter table " + table_name + " add tugas" + i + " double;";
                command.CommandText = q;
                command.ExecuteNonQuery();
            }
            for (int i = 1; i <= kdk; i++)
            {
                string q = "alter table " + table_name + " add kdk" + i + " double;";
                command.CommandText = q;
                command.ExecuteNonQuery();
            }
        }
        public static string TableQuery(string title)
        {
            string q = "CREATE TABLE " + title + " (id INTEGER PRIMARY KEY AUTOINCREMENT,induk VARCHAR(8) REFERENCES data_siswa(induk)" +
                " ON DELETE CASCADE ON UPDATE CASCADE,uts double, uas double); ";

            return q;
        }

        public static void CreateTrigger(SQLiteCommand command)
        {
            String[] table_list = {Constants.absen,Constants.sikap, Constants.agm, Constants.pkn, Constants.bi, Constants.mtk, Constants.ipa, Constants.ips
            , Constants.sbdp, Constants.pjok, Constants.bjr, Constants.bing, Constants.bta};

            for (int i=0;i < table_list.Length; i++)
            {
                string q = "CREATE TRIGGER " +
                    table_list[i] +
                    " AFTER INSERT ON data_siswa FOR EACH ROW BEGIN INSERT INTO " +
                    table_list[i] +
                    " (induk) VALUES(new.induk);END;";
                command.CommandText = q;
                command.ExecuteNonQuery();
            }
        }
    }
}
