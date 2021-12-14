using System;
using System.Data;
using System.Data.SQLite;
using System.Windows;
using System.IO;
using Raport.Services;
using Raport.Helper;
using System.Timers;

namespace Raport
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        long lastSaved = DateTime.UtcNow.Ticks;

        public MainWindow()
        {            
            InitializeComponent();
            Connection.DBConnection(Constants.dasis, Constants.dasis_title);
            dgr.ItemsSource = Connection.dataset.Tables[Constants.dasis_title].DefaultView;
            timer.Content = lastSaved;
        }
        

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Connection.table = Connection.dataset.Tables[0];
            Report.CreateDasis(Connection.table);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Connection.UpdateDB(Connection.adapter, Connection.dataset, Constants.dasis_title,Constants.dasis);
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            const int SECOND = 1;
            const int MINUTE = 60 * SECOND;
            const int HOUR = 60 * MINUTE;
            const int DAY = 24 * HOUR;
            const int MONTH = 30 * DAY;
            string a;
            var ts = new TimeSpan(DateTime.UtcNow.Ticks - (Int64)lastSaved);
            lastSaved = DateTime.UtcNow.Ticks;
            double delta = Math.Abs(ts.TotalSeconds);

            if (delta < 1 * MINUTE)

                a= ts.Seconds == 1 ? "1 detik yang lalu" : ts.Seconds + " detik yang lalu";

            else if (delta < 2 * MINUTE)
                a= "Semenit yang lalu";

            else if (delta < 45 * MINUTE)
                a= ts.Minutes + " menit yang lalu";

            else if (delta < 90 * MINUTE)
                a= "Satu jam yang lalu";

            else if (delta < 24 * HOUR)
                a= ts.Hours + " jam yang lalu";

            else if (delta < 48 * HOUR)
                a= "Kemarin";

            else if (delta < 30 * DAY)
                a= ts.Days + " Hari yang lalu";

            else if (delta < 12 * MONTH)
            {
                long months = Convert.ToInt64(Math.Floor((double)ts.Days / 30));
                a= months <= 1 ? "1 Bulan yang lalu" : months + " Bulan yang lalu";
            }
            else
            {
                long years = Convert.ToInt64(Math.Floor((double)ts.Days / 365));
                a= years <= 1 ? "1 Tahun yang lalu" : years + " Tahun yang lalu";
            }

            timer.Content = a;
        }


    }
}
