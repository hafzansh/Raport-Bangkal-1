using Raport.Helper;
using System;
using System.Collections.Generic;
using System.Text;
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
    /// Interaction logic for UserSettings.xaml
    /// </summary>
    public partial class UserSettings : Page
    {
        public UserSettings()
        {
            InitializeComponent();
            checkValue();
            if (Constants.isLanding)
            {
                savenext.Content = "Selanjutnya";
            }
            else
            {
                savenext.Content = "Simpan";
            }
        }

        private void savenext_Click(object sender, RoutedEventArgs e)
        {

            if (Constants.isLanding)
            {
                if (validate())
                {
                    foreach (Window window in Application.Current.Windows)
                    {
                        if (window.GetType() == typeof(AppSettings))
                        {
                            (window as AppSettings).settings_frame.Navigate(new Uri("Pages/Landing/KDSettings.xaml", UriKind.RelativeOrAbsolute));
                        }
                    }
                }
                
            }
            else
            {
                
            }
        }
        private bool validate()
        {
            bool a = true;
            if (wali_kelas.Text == "")
            {
                MessageBox.Show("Isi Wali kelas!");
                wali_kelas.Focus();
                a = false;
            }else if (nip_wali_kelas.Text == "")
            {
                MessageBox.Show("Isi NIP Wali kelas!");
                nip_wali_kelas.Focus();
                a = false;
            }
            else if (kepala_sekolah.Text == "")
            {
                MessageBox.Show("Isi Kepala Sekolah!");
                kepala_sekolah.Focus();
                a = false;
            }
            else if (nip_kepala_sekolah.Text == "")
            {
                MessageBox.Show("Isi NIP Kepala Sekolah!");
                nip_kepala_sekolah.Focus();
                a = false;
            }
            else
            {
                Database.wali_kelas = wali_kelas.Text;
                Database.nip_wali_kelas = nip_wali_kelas.Text;
                Database.kepala_sekolah = kepala_sekolah.Text;
                Database.nip_kepala_sekolah = nip_kepala_sekolah.Text;
                Database.tahun = tahun.Text;
                Database.semester = semester.Text;
                Database.kelas = kelas.Text;
                a = true;
            }
            return a;
        }

        private void checkValue()
        {
            if (Database.wali_kelas != null)
            {
                wali_kelas.Text = Database.wali_kelas;
            }
            if (Database.nip_wali_kelas != null)
            {
                nip_wali_kelas.Text = Database.nip_wali_kelas;
            }
            if (Database.kepala_sekolah != null)
            {
                kepala_sekolah.Text = Database.kepala_sekolah;
            }
            if (Database.nip_kepala_sekolah != null)
            {
                nip_kepala_sekolah.Text = Database.nip_kepala_sekolah;
            }
            if (Database.semester != null)
            {
                semester.Text = Database.semester;
            }
            if (Database.tahun != null)
            {
                tahun.Text = Database.tahun;
            }
            if (Database.kelas != null)
            {
                kelas.Text = Database.kelas;
            }
        }
    }
}
