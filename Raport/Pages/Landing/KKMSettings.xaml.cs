using Raport.Helper;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Raport.Pages.Landing
{
    /// <summary>
    /// Interaction logic for KKMSettings.xaml
    /// </summary>
    public partial class KKMSettings : Page
    {
        public KKMSettings()
        {
            InitializeComponent();
            setValue();
        }
        private void setValue()
        {
            agama.Text = Database.kkm_agm.ToString();
            pkn.Text = Database.kkm_pkn.ToString();
            bi.Text = Database.kkm_bi.ToString();
            mtk.Text = Database.kkm_mtk.ToString();
            ipa.Text = Database.kkm_ipa.ToString();
            ips.Text = Database.kkm_ips.ToString();
            sbdp.Text = Database.kkm_sbdp.ToString();
            pjok.Text = Database.kkm_pjok.ToString();
            bjr.Text = Database.kkm_bjr.ToString();
            bing.Text = Database.kkm_bing.ToString();
            bta.Text = Database.kkm_bta.ToString();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {            
            Database.kkm_agm = int.Parse(agama.Text);
            Database.kkm_pkn = int.Parse(pkn.Text);
            Database.kkm_bi = int.Parse(bi.Text);
            Database.kkm_mtk = int.Parse(mtk.Text);
            Database.kkm_ipa = int.Parse(ipa.Text);
            Database.kkm_ips = int.Parse(ips.Text);
            Database.kkm_sbdp = int.Parse(sbdp.Text);
            Database.kkm_pjok = int.Parse(pjok.Text);
            Database.kkm_bjr = int.Parse(bjr.Text);
            Database.kkm_bing = int.Parse(bing.Text);
            Database.kkm_bta = int.Parse(bta.Text);
            try
            {
                SQLiteCommand command = Connection.sqlite.CreateCommand();
                command.CommandText = "UPDATE app_settings SET " +
                    "kkm_agm = @1, " +
                    "kkm_pkn = @3," +
                    "kkm_bi = @5," +
                    "kkm_mtk = @7," +
                    "kkm_ipa = @9," +
                    "kkm_ips = @11," +
                    "kkm_sbdp = @1," +
                    "kkm_pjok = @15," +
                    "kkm_bjr = @17," +
                    "kkm_bing = @19," +
                    "kkm_bta = @21" +
                    " where id=1";
                command.Parameters.AddWithValue("@1", Database.kkm_agm);
                command.Parameters.AddWithValue("@3", Database.kkm_pkn);
                command.Parameters.AddWithValue("@5", Database.kkm_bi);
                command.Parameters.AddWithValue("@7", Database.kkm_mtk);
                command.Parameters.AddWithValue("@9", Database.kkm_ipa);
                command.Parameters.AddWithValue("@11", Database.kkm_ips);
                command.Parameters.AddWithValue("@1", Database.kkm_sbdp);
                command.Parameters.AddWithValue("@15", Database.kkm_pjok);
                command.Parameters.AddWithValue("@17", Database.kkm_bjr);
                command.Parameters.AddWithValue("@19", Database.kkm_bing);
                command.Parameters.AddWithValue("@21", Database.kkm_bta);
                Connection.sqlite.Open();
                command.ExecuteNonQuery();
                Connection.sqlite.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Ada Kesalahan! " + ex.ToString());
            }
        }
        private void Button_Back(object sender, RoutedEventArgs e)
        {
            foreach (Window window in Application.Current.Windows)
            {
                if (window.GetType() == typeof(Window1))
                {
                    (window as Window1).fContainer.Navigate(new Uri("Pages/Home.xaml", UriKind.RelativeOrAbsolute));
                }
            }
        }
        private void Button_KD(object sender, RoutedEventArgs e)
        {
            foreach (Window window in Application.Current.Windows)
            {
                if (window.GetType() == typeof(Window1))
                {
                    (window as Window1).fContainer.Navigate(new Uri("Pages/Landing/KDSettings.xaml", UriKind.RelativeOrAbsolute));
                }
            }
        }
        private void Button_User(object sender, RoutedEventArgs e)
        {
            foreach (Window window in Application.Current.Windows)
            {
                if (window.GetType() == typeof(Window1))
                {
                    (window as Window1).fContainer.Navigate(new Uri("Pages/Landing/UserSettings.xaml", UriKind.RelativeOrAbsolute));
                }
            }
        }
    }
}
