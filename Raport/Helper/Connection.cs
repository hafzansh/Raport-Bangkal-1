using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Text;
using System.Windows;

namespace Raport.Helper
{
    
    public class Connection
    {
        public static SQLiteDataAdapter adapter = new SQLiteDataAdapter();        
        public static DataTable table = new DataTable();
        public static DataSet dataset = new DataSet();
        public static SQLiteCommandBuilder commandBuilder;        
        private static string kd = "kdp1,kdp2,kdp3,kdp4,kdp5,";
        public static SQLiteConnection sqlite = new SQLiteConnection("Data Source=" + Constants.folderpath + Constants.dbName +".db" +";" + Constants.dbVersion);
        public static void setConnection()
        {
            try
            {
                Connection.DBConnection(Constants.dasis, Constants.dasis_title);
                Connection.DBConnection2(Constants.mtk, Constants.mtk_title);
                Connection.DBConnection2(Constants.pkn, Constants.pkn_title);
                Connection.KD3("pkn3", "kd_pkn3",Database.kd_pkn3);

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
        public static void DBConnection2(String target,String tablename)
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
        public static void KD3(String target, String tablename,int limit)
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
        public static void UpdatePKN(SQLiteDataAdapter uAdapter, DataSet uDS, string uTable, String q)
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
