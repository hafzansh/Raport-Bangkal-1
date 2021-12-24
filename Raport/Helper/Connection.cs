using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Raport.Helper
{
    
    public class Connection
    {
        public static SQLiteDataAdapter adapter = new SQLiteDataAdapter();        
        public static DataTable table = new DataTable();
        public static DataSet dataset = new DataSet();
        public static SQLiteCommandBuilder commandBuilder;        
        private static string kd = "kdp1,kdp2,kdp3,kdp4,kdp5,tugas1,tugas2,tugas3,tugas4,tugas5,";
        private static string kdk = "kdk1,kdk2,kdk3,kdk4,kdk5";
        public static SQLiteConnection sqlite = new SQLiteConnection("Data Source=" + Constants.folderpath + Constants.dbName +".db" +";Foreign Key Constraints=On;" + Constants.dbVersion);
        public static void setConnection()
        {
            try
            {

                    Connection.DBConnection(Constants.dasis, Constants.dasis_title);
                    Connection.MP_KD3(Constants.mtk, Constants.mtk_title);
                    Connection.MP_KD3(Constants.pkn, Constants.pkn_title);
                    Connection.MP_KD3(Constants.agm, Constants.agm_title);
                    Connection.MP_KD3(Constants.ipa, Constants.ipa_title);
                    Connection.MP_KD3(Constants.ips, Constants.ips_title);
                    Connection.MP_KD3(Constants.bi, Constants.bi_title);
                    Connection.MP_KD3(Constants.sbdp, Constants.sbdp_title);
                    Connection.MP_KD3(Constants.pjok, Constants.pjok_title);
                    Connection.MP_KD3(Constants.bjr, Constants.bjr_title);
                    Connection.MP_KD3(Constants.bing, Constants.bing_title);
                    Connection.MP_KD3(Constants.bta, Constants.bta_title);

                    Connection.MP_KD4(Constants.mtk, Constants.mtk_title2);
                    Connection.MP_KD4(Constants.pkn, Constants.pkn_title2);
                    Connection.MP_KD4(Constants.agm, Constants.agm_title2);
                    Connection.MP_KD4(Constants.ipa, Constants.ipa_title2);
                    Connection.MP_KD4(Constants.ips, Constants.ips_title2);
                    Connection.MP_KD4(Constants.bi, Constants.bi_title2);
                    Connection.MP_KD4(Constants.sbdp, Constants.sbdp_title2);
                    Connection.MP_KD4(Constants.pjok, Constants.pjok_title2);
                    Connection.MP_KD4(Constants.bjr, Constants.bjr_title2);
                    Connection.MP_KD4(Constants.bing, Constants.bing_title2);
                    Connection.MP_KD4(Constants.bta, Constants.bta_title2);

                    Connection.KD("pkn3", "kd_pkn3", Database.kd_pkn3);
                    Connection.KD("mtk3", "kd_mtk3", Database.kd_mtk3);
                    Connection.KD("agm3", "kd_agm3", Database.kd_agm3);
                    Connection.KD("bi3", "kd_bi3", Database.kd_bi3);
                    Connection.KD("ipa3", "kd_ipa3", Database.kd_ipa3);
                    Connection.KD("ips3", "kd_ips3", Database.kd_ips3);
                    Connection.KD("sbdp3", "kd_sbdp3", Database.kd_sbdp3);
                    Connection.KD("pjok3", "kd_pjok3", Database.kd_pjok3);
                    Connection.KD("bjr3", "kd_bjr3", Database.kd_bjr3);
                    Connection.KD("bing3", "kd_bing3", Database.kd_bing3);
                    Connection.KD("bta3", "kd_bta3", Database.kd_bta3);

                    Connection.KD("pkn4", "kd_pkn4", Database.kd_pkn4);
                    Connection.KD("mtk4", "kd_mtk4", Database.kd_mtk4);
                    Connection.KD("agm4", "kd_agm4", Database.kd_agm4);
                    Connection.KD("bi4", "kd_bi4", Database.kd_bi4);
                    Connection.KD("ipa4", "kd_ipa4", Database.kd_ipa4);
                    Connection.KD("ips4", "kd_ips4", Database.kd_ips4);
                    Connection.KD("sbdp4", "kd_sbdp4", Database.kd_sbdp4);
                    Connection.KD("pjok4", "kd_pjok4", Database.kd_pjok4);
                    Connection.KD("bjr4", "kd_bjr4", Database.kd_bjr4);
                    Connection.KD("bing4", "kd_bing4", Database.kd_bing4);
                    Connection.KD("bta4", "kd_bta4", Database.kd_bta4);
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Koneksi Database error, " + ex);
            }
        }
        public static void DBConnection(String q, String tablename)
        {
            try
            {                
                string query = "SELECT * FROM " + q;
                adapter.SelectCommand = new SQLiteCommand(query, sqlite);
                commandBuilder = new SQLiteCommandBuilder(adapter);
                adapter.Fill(dataset, tablename);

            }
            catch (SQLiteException ex)
            {
                Console.WriteLine(ex);
            }
        }
        public static void MP_KD3(String target,String tablename)
        {
            try
            {                
                string query = "SELECT distinct " +target + ".id, " + target + ".induk, data_siswa.nama, " + kd + target + ".uts, " + target + ".uas FROM " + target + " inner join data_siswa on " + target + ".induk=data_siswa.induk";
                adapter.SelectCommand = new SQLiteCommand(query, sqlite);
                commandBuilder = new SQLiteCommandBuilder(adapter);
                adapter.Fill(dataset, tablename);

            }
            catch (SQLiteException ex)
            {
                Console.WriteLine(ex);
            }
        }
        public static void MP_KD4(String target, String tablename)
        {
            try
            {
                string query = "SELECT distinct " + target + ".id, " + target + ".induk, data_siswa.nama, " + kdk + " FROM " + target + " inner join data_siswa on " + target + ".induk=data_siswa.induk";
                adapter.SelectCommand = new SQLiteCommand(query, sqlite);
                commandBuilder = new SQLiteCommandBuilder(adapter);
                adapter.Fill(dataset, tablename);

            }
            catch (SQLiteException ex)
            {
                Console.WriteLine(ex);
            }
        }
        public static void KD(String target, String tablename,int limit)
        {
            try
            {
                string query = "select id,kd,"+target+ " from kompetensi_dasar limit "+ limit;
                adapter.SelectCommand = new SQLiteCommand(query, sqlite);
                commandBuilder = new SQLiteCommandBuilder(adapter);
                adapter.Fill(dataset, tablename);

            }
            catch (SQLiteException ex)
            {
                Console.WriteLine(ex);
            }
        }
        public static void UpdateDB(SQLiteDataAdapter uAdapter,DataSet uDS,string uTable,String q)
        {
            try
            {
                string query = "SELECT * FROM " + q;
                uAdapter.SelectCommand = new SQLiteCommand(query, sqlite);
                commandBuilder = new SQLiteCommandBuilder(uAdapter);
                uAdapter.UpdateCommand = commandBuilder.GetUpdateCommand();
                uAdapter.Update(uDS, uTable);
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show("Error, " + ex);
            }
        }
        public static void UpdateDB2(SQLiteDataAdapter uAdapter, DataSet uDS, string uTable, String q)
        {            
            try
            {                
                string query = "SELECT id," + kd + "uts,uas FROM " + q;
                uAdapter.SelectCommand = new SQLiteCommand(query, sqlite);
                commandBuilder = new SQLiteCommandBuilder(uAdapter);
                uAdapter.UpdateCommand = commandBuilder.GetUpdateCommand();
                uAdapter.Update(uDS, uTable);
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show("Error, " + ex);
            }
        }
        public static void UpdateData3(SQLiteDataAdapter uAdapter, DataSet uDS, string uTable, String q)
        {
            try
            {
                string query = "SELECT id," + kd + "uts,uas FROM " + q;
                uAdapter.SelectCommand = new SQLiteCommand(query, sqlite);
                commandBuilder = new SQLiteCommandBuilder(uAdapter);
                uAdapter.UpdateCommand = commandBuilder.GetUpdateCommand();
                uAdapter.Update(uDS, uTable);
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show("Error, " + ex);
            }
        }
        public static void UpdateData4(SQLiteDataAdapter uAdapter, DataSet uDS, string uTable, String q)
        {
            try
            {
                string query = "SELECT id," + kdk + " FROM " + q;
                uAdapter.SelectCommand = new SQLiteCommand(query, sqlite);
                commandBuilder = new SQLiteCommandBuilder(uAdapter);
                uAdapter.UpdateCommand = commandBuilder.GetUpdateCommand();
                uAdapter.Update(uDS, uTable);
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show("Error, " + ex);
            }
        }
        public static void UpdateKD(SQLiteDataAdapter uAdapter, DataSet uDS, String q, string uTable)
        {
            try
            {
                string query = "SELECT id,kd," + q + " FROM kompetensi_dasar";
                uAdapter.SelectCommand = new SQLiteCommand(query, sqlite);
                commandBuilder = new SQLiteCommandBuilder(uAdapter);
                uAdapter.UpdateCommand = commandBuilder.GetUpdateCommand();
                uAdapter.Update(uDS, uTable);
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show("Error, " + ex);
            }
        }
    }
}
