using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data.SQLite;
using Raport.Helper;
using System.Globalization;
using System.Windows;

namespace Raport.Services
{
    public static class ReportCover
    {
        public static SQLiteDataAdapter adapter = new SQLiteDataAdapter();
        public static SQLiteCommandBuilder commandBuilder;
        public static void CreateReport()
        {
            try { 
            DataTable dt = new();
            dt.Clear();
            dt = Connection.dataset.Tables[Constants.dasis_title];
            dt.Columns.Add("kepsek", typeof(string));
            dt.Columns.Add("nip_kepsek", typeof(string));
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DateTime date = DateTime.Parse(dt.Rows[i]["tanggal_lahir"].ToString(), CultureInfo.CreateSpecificCulture("en-US"));
                dt.Rows[i]["tanggal_lahir"] = date.ToString("dd MMMM yyyy");
                dt.Rows[i]["kepsek"] = Database.kepala_sekolah;
                dt.Rows[i]["nip_kepsek"] = Database.nip_kepala_sekolah;
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

            File.WriteAllText(@"Templates\TempDataCover.csv", sb.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
