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
        public static int kkm_agm;
        public static int kkm_pkn;
        public static int kkm_bi;
        public static int kkm_mtk;
        public static int kkm_ipa;
        public static int kkm_ips;
        public static int kkm_sbdp;
        public static int kkm_pjok;
        public static int kkm_bjr;
        public static int kkm_bing;
        public static int kkm_bta;


        public static void CreateDB()
        {
            bool exists = Directory.Exists(Constants.folderpath);

            if (!exists)
                Directory.CreateDirectory(Constants.folderpath);

            if (!File.Exists(Constants.folderpath + db_name))
            {
                try
                {
                    using SQLiteConnection sql = new SQLiteConnection("Data Source=" + Constants.folderpath + db_name + ".db" + ";Foreign Key Constraints = On;" + Constants.dbVersion);
                    sql.Open();                    
                    SQLiteCommand command = new SQLiteCommand(Constants.dasis_query, sql);
                    command.ExecuteNonQuery();
                    command.CommandText = Constants.absen_query;
                    command.ExecuteNonQuery();
                    command.CommandText = Constants.sikap_query;
                    command.ExecuteNonQuery();
                    command.CommandText = Constants.app_query;
                    command.ExecuteNonQuery();
                    string script = File.ReadAllText(Environment.CurrentDirectory + @"\Scripts\create_view.sql");
                    command.CommandText = script;
                    command.ExecuteNonQuery();
                    CreateTable(command, Constants.agm);
                    CreateKDT(command,Constants.agm);
                    CreateTable(command, Constants.pkn);
                    CreateKDT(command,Constants.pkn);
                    CreateTable(command, Constants.mtk);
                    CreateKDT(command,Constants.mtk);
                    CreateTable(command, Constants.bi);
                    CreateKDT(command,Constants.bi);
                    CreateTable(command, Constants.ipa);
                    CreateKDT(command,Constants.ipa);
                    CreateTable(command, Constants.ips);
                    CreateKDT(command,Constants.ips);
                    CreateTable(command, Constants.sbdp);
                    CreateKDT(command,Constants.sbdp);
                    CreateTable(command, Constants.pjok);
                    CreateKDT(command,Constants.pjok);
                    CreateTable(command, Constants.bjr);
                    CreateKDT(command,Constants.bjr);
                    CreateTable(command, Constants.bing);
                    CreateKDT(command,Constants.bing);
                    CreateTable(command, Constants.bta);
                    CreateKDT(command,Constants.bta);
                    CreateTrigger(command);
                    InsertAppSettings(command);
                    command.CommandText = Constants.kd_query;
                    command.ExecuteNonQuery();
                    InsertKD(command);
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
        public static void InsertKD(SQLiteCommand command)
        {
            for (int i = 1; i <= 5; i++)
            {
                string query = "insert into kompetensi_dasar (id,kd) values (null,'Kompetensi Dasar " + i + "');";
                command.CommandText = query;
                command.ExecuteNonQuery();
            }
            
        }
        public static void InsertAppSettings(SQLiteCommand command)
        {
            string query = "insert into app_settings values (NULL,'" +
            Database.wali_kelas + "' , '" + nip_wali_kelas + "' , '" + kepala_sekolah + "' , '" + nip_kepala_sekolah + "' , '" + semester + "' , '" + tahun + "' , '" +
            kelas + "'," + kd_agm3 + "," + kd_agm4 + "," + kd_pkn3 + "," + kd_pkn4 + "," + kd_bi3 + "," + kd_bi4 + "," + kd_mtk3 + "," +
            kd_mtk4 + "," + kd_ipa3 + "," + kd_ipa4 + "," + kd_ips3 + "," + kd_ips4 + "," + kd_sbdp3 + "," + kd_sbdp4 + "," + kd_pjok3 + "," +
            kd_pjok4 + "," + kd_bjr3 + "," + kd_bjr4 + "," + kd_bing3 + "," + kd_bing4 + "," + kd_bta3 + "," + kd_bta4 + ",70,70,70,70,70,70,70,70,70,70,70)";
            command.CommandText = query;
            command.ExecuteNonQuery();
        }
        public static void CreateTable(SQLiteCommand command,string table_name)
        {
            command.CommandText = TableQuery(table_name);
            command.ExecuteNonQuery();            
        }
        public static void CreateKDT(SQLiteCommand command,string table_name)
        {
            for (int i = 1; i <= 5; i++)
            {
                string q = "alter table " + table_name + " add kdp" + i + " double default (0);";
                command.CommandText = q;
                command.ExecuteNonQuery();
            }
            for (int i = 1; i <= 5; i++)
            {
                string q = "alter table " + table_name + " add tugas" + i + " double default (0);";
                command.CommandText = q;
                command.ExecuteNonQuery();
            }
            for (int i = 1; i <= 5; i++)
            {
                string q = "alter table " + table_name + " add kdk" + i + " double default (0);";
                command.CommandText = q;
                command.ExecuteNonQuery();
            }
        }
        public static string TableQuery(string title)
        {
            string q = "CREATE TABLE " + title + " (id INTEGER PRIMARY KEY AUTOINCREMENT,induk VARCHAR(8) REFERENCES data_siswa(induk)" +
                " ON DELETE CASCADE ON UPDATE CASCADE,uts double default (0), uas double default (0)); ";

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
