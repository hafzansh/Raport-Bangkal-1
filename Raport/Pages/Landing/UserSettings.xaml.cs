using Raport.Helper;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
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
                page_title.Content = "Buat Raport Baru";
            }
            else
            {
                savenext.Content = "Simpan";
                page_title.Content = "Edit Pengaturan Raport";
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
                if (validate())
                {
                    foreach (Window window in Application.Current.Windows)
                    {
                        if (window.GetType() == typeof(Window1))
                        {
                            (window as Window1).fContainer.Navigate(new Uri("Pages/Landing/KDSettings.xaml", UriKind.RelativeOrAbsolute));
                            (window as Window1).guru.Content = Database.wali_kelas;
                            (window as Window1).setting.Content = Database.kelas + ", " + Database.semester + ", " + Database.tahun;
                            try {
                                SQLiteCommand command = Connection.sqlite.CreateCommand();
                                command.CommandText = "UPDATE app_settings SET wali_kelas = @wali, " +
                                    "nip_wali_kelas = @nip_wali,kepala_sekolah = @kepsek," +
                                    "nip_kepala_sekolah = @nip_kepsek,semester = @smt," +
                                    "tahun = @tahun,kelas = @kelas where id=1";
                                command.Parameters.AddWithValue("@wali", Database.wali_kelas);
                                command.Parameters.AddWithValue("@nip_wali", Database.nip_wali_kelas);
                                command.Parameters.AddWithValue("@kepsek", Database.kepala_sekolah);
                                command.Parameters.AddWithValue("@nip_kepsek", Database.nip_kepala_sekolah);
                                command.Parameters.AddWithValue("@smt", Database.semester);
                                command.Parameters.AddWithValue("@tahun", Database.tahun);
                                command.Parameters.AddWithValue("@kelas", Database.kelas);
                                Connection.sqlite.Open();
                                command.ExecuteNonQuery();
                                Connection.sqlite.Close();

                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("pog" + ex);
                            }
                            

                            

                        }
                    }
                }
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
