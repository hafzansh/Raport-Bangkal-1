using Raport.Helper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for KDSettings.xaml
    /// </summary>
    public partial class KDSettings : Page
    {
        public KDSettings()
        {
            InitializeComponent();
            setValue();
        }
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^1-5]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void back_Click(object sender, RoutedEventArgs e)
        {
            if (Constants.isLanding)
            {
                foreach (Window window in Application.Current.Windows)
                {
                    if (window.GetType() == typeof(AppSettings))
                    {
                        (window as AppSettings).settings_frame.Navigate(new Uri("Pages/Landing/UserSettings.xaml", UriKind.RelativeOrAbsolute));
                    }
                }
            }
            else
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

        private void savenext_Click(object sender, RoutedEventArgs e)
        {
            if (Constants.isLanding)
            {
                if (validate())
                {
                    Database.db_name = Database.wali_kelas + "_" + Database.kelas + "_" + Database.semester + "_" + Database.tahun;
                    Database.CreateDB();
                    Constants.dbName = Database.db_name;
                    Window new_window = new Window1();
                    new_window.Show();
                    foreach (Window window in Application.Current.Windows)
                    {
                        if (window.GetType() == typeof(AppSettings))
                        {
                            (window as AppSettings).Hide();
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("it's working");
            }
            
        }
        private bool validate()
        {
            bool a = true;
            if (kd_agm3.Text == "")
            {
                MessageBox.Show("KD Agama (Pengetahuan) Belum Diisi!");
                kd_agm3.Focus();
                a = false;
            }
            else if (kd_agm4.Text == "")
            {
                MessageBox.Show("KD Agama (Keterampilan) Belum Diisi!");
                kd_agm4.Focus();
                a = false;
            }
            else if (kd_pkn3.Text == "")
            {
                MessageBox.Show("KD PKN (Pengetahuan) Belum Diisi!");
                kd_pkn3.Focus();
                a = false;
            }
            else if (kd_pkn4.Text == "")
            {
                MessageBox.Show("KD PKN (Keterampilan) Belum Diisi!");
                kd_pkn4.Focus();
                a = false;
            }
            else if (kd_bi3.Text == "")
            {
                MessageBox.Show("KD Bahasa Indonesia (Pengetahuan) Belum Diisi!");
                kd_bi3.Focus();
                a = false;
            }
            else if (kd_bi4.Text == "")
            {
                MessageBox.Show("KD Bahasa Indonesia (Keterampilan) Belum Diisi!");
                kd_bi4.Focus();
                a = false;
            }
            else if (kd_mtk3.Text == "")
            {
                MessageBox.Show("KD Matematika (Pengetahuan) Belum Diisi!");
                kd_mtk3.Focus();
                a = false;
            }
            else if (kd_mtk4.Text == "")
            {
                MessageBox.Show("KD Matematika (Keterampilan) Belum Diisi!");
                kd_mtk4.Focus();
                a = false;
            }
            else if (kd_ipa3.Text == "")
            {
                MessageBox.Show("KD IPA (Pengetahuan) Belum Diisi!");
                kd_ipa3.Focus();
                a = false;
            }
            else if (kd_ipa4.Text == "")
            {
                MessageBox.Show("KD IPA (Keterampilan) Belum Diisi!");
                kd_ipa4.Focus();
                a = false;
            }
            else if (kd_ips3.Text == "")
            {
                MessageBox.Show("KD IPS (Pengetahuan) Belum Diisi!");
                kd_ips3.Focus();
                a = false;
            }
            else if (kd_ips4.Text == "")
            {
                MessageBox.Show("KD IPS (Keterampilan) Belum Diisi!");
                kd_ips4.Focus();
                a = false;
            }
            else if (kd_sbdp3.Text == "")
            {
                MessageBox.Show("KD SBdP (Pengetahuan) Belum Diisi!");
                kd_sbdp3.Focus();
                a = false;
            }
            else if (kd_sbdp4.Text == "")
            {
                MessageBox.Show("KD SBdP (Keterampilan) Belum Diisi!");
                kd_sbdp4.Focus();
                a = false;
            }
            else if (kd_pjok3.Text == "")
            {
                MessageBox.Show("KD PJOK (Pengetahuan) Belum Diisi!");
                kd_pjok3.Focus();
                a = false;
            }
            else if (kd_pjok4.Text == "")
            {
                MessageBox.Show("KD PJOK (Keterampilan) Belum Diisi!");
                kd_pjok4.Focus();
                a = false;
            }
            else if (kd_bjr3.Text == "")
            {
                MessageBox.Show("KD Bahasa Banjar (Pengetahuan) Belum Diisi!");
                kd_bjr3.Focus();
                a = false;
            }
            else if (kd_bjr4.Text == "")
            {
                MessageBox.Show("KD Bahasa Banjar (Keterampilan) Belum Diisi!");
                kd_bjr4.Focus();
                a = false;
            }
            else if (kd_bing3.Text == "")
            {
                MessageBox.Show("KD Bahasa Inggris (Pengetahuan) Belum Diisi!");
                kd_bing3.Focus();
                a = false;
            }
            else if (kd_bing4.Text == "")
            {
                MessageBox.Show("KD Bahasa Inggris (Keterampilan) Belum Diisi!");
                kd_bing4.Focus();
                a = false;
            }
            else if (kd_bta3.Text == "")
            {
                MessageBox.Show("KD BTA (Pengetahuan) Belum Diisi!");
                kd_bta3.Focus();
                a = false;
            }
            else if (kd_bta4.Text == "")
            {
                MessageBox.Show("KD BTA (Keterampilan) Belum Diisi!");
                kd_bta4.Focus();
                a = false;
            }
            else
            {
                Database.kd_pkn3 = int.Parse(kd_pkn3.Text);
                Database.kd_pkn4 = int.Parse(kd_pkn4.Text);
                Database.kd_agm3 = int.Parse(kd_agm3.Text);
                Database.kd_agm4 = int.Parse(kd_agm4.Text);
                Database.kd_mtk3 = int.Parse(kd_mtk3.Text);
                Database.kd_mtk4 = int.Parse(kd_mtk4.Text);
                Database.kd_bi3 = int.Parse(kd_bi3.Text);
                Database.kd_bi4 = int.Parse(kd_bi4.Text);
                Database.kd_ipa3 = int.Parse(kd_ipa3.Text);
                Database.kd_ipa4 = int.Parse(kd_ipa4.Text);
                Database.kd_ips3 = int.Parse(kd_ips3.Text);
                Database.kd_ips4 = int.Parse(kd_ips4.Text);
                Database.kd_sbdp3 = int.Parse(kd_sbdp3.Text);
                Database.kd_sbdp4 = int.Parse(kd_sbdp4.Text);
                Database.kd_pjok3 = int.Parse(kd_pjok3.Text);
                Database.kd_pjok4 = int.Parse(kd_bta4.Text);
                Database.kd_bjr3 = int.Parse(kd_bjr3.Text);
                Database.kd_bjr4 = int.Parse(kd_bjr4.Text);
                Database.kd_bing3 = int.Parse(kd_bing3.Text);
                Database.kd_bing4 = int.Parse(kd_bing4.Text);
                Database.kd_bta3 = int.Parse(kd_bta3.Text);
                Database.kd_bta4 = int.Parse(kd_bta4.Text);
                a = true;
            }

            return a;
        }
        private void setValue()
        {            
            if (Constants.isLanding)
            {

            }
            else { 
            kd_pkn3.Text = Database.kd_pkn3.ToString();
            kd_pkn4.Text = Database.kd_pkn4.ToString();
            kd_agm3.Text = Database.kd_agm3.ToString();
            kd_agm4.Text = Database.kd_agm4.ToString();
            kd_mtk3.Text = Database.kd_mtk3.ToString();
            kd_mtk4.Text = Database.kd_mtk4.ToString();
            kd_bi3.Text = Database.kd_bi3.ToString();
            kd_bi4.Text = Database.kd_bi4.ToString();
            kd_ipa3.Text = Database.kd_ipa3.ToString();
            kd_ipa4.Text = Database.kd_ipa4.ToString();
            kd_ips3.Text = Database.kd_ips3.ToString();
            kd_ips4.Text = Database.kd_ips4.ToString();
            kd_sbdp3.Text = Database.kd_sbdp3.ToString();
            kd_sbdp4.Text = Database.kd_sbdp4.ToString();
            kd_pjok3.Text = Database.kd_pjok3.ToString();
            kd_pjok4.Text = Database.kd_pjok4.ToString();
            kd_bjr3.Text = Database.kd_bjr3.ToString();
            kd_bjr4.Text = Database.kd_bjr4.ToString();
            kd_bing3.Text = Database.kd_bing3.ToString();
            kd_bing4.Text = Database.kd_bing4.ToString();
            kd_bta3.Text = Database.kd_bta3.ToString();
            kd_bta4.Text = Database.kd_bta4.ToString();
            }
        }
    }
}
