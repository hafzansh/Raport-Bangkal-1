﻿using System;
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
        public static SQLiteConnection sqlite = new SQLiteConnection("Data Source=" + Constants.folderpath + Constants.dbName +".db" +";" + Constants.dbVersion);
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
                string query = "SELECT distinct " +target + ".id, " + target + ".induk, data_siswa.nama, " + target + ".kdp1," + target + ".kdp2, " + target + ".kdp3, " + target + ".kdp4, " + target + ".kdp5, " + target + ".uts, " + target + ".uas FROM " + target + " inner join data_siswa on " + target + ".induk=data_siswa.induk";
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
                string query = "SELECT id,kdp1,kdp2,kdp3,kdp4,kdp5,uts,uas FROM " + q;
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
