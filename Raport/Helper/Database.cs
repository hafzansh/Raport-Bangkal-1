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
                    CreateTable(command, Constants.agm);
                    CreateKDT(command, kd_agm3, kd_agm4, Constants.agm);
                }
                catch(Exception ex)
                {
                    MessageBox.Show("Error, " + ex);
                }
            }
        }
        public static void CreateTable(SQLiteCommand command,string table_name)
        {
            command.CommandText = TableQuery(table_name);
            command.ExecuteNonQuery();            
        }
        public static void CreateKDT(SQLiteCommand command, int kdp, int kdk,string table_name)
        {
            for (int i = 1; i <= (kdp * 2); i++)
            {
                string q = "alter table " + table_name + " add kdp" + i + " double;";
                command.CommandText = q;
                command.ExecuteNonQuery();
            }
            for (int i = 1; i <= (kdp * 2); i++)
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
        public static string TableQuery(String title)
        {
            string q = "CREATE TABLE " + title + " (id INTEGER     PRIMARY KEY AUTOINCREMENT,induk VARCHAR(8) REFERENCES data_siswa(induk)" +
                " ON DELETE CASCADE ON UPDATE CASCADE); ";

            return q;
        }
    }
}
