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
using System.Windows.Shapes;

namespace Raport.Helper.ModalUI
{
    /// <summary>
    /// Interaction logic for PasswordDialog.xaml
    /// </summary>
    public partial class PasswordDialog : Window
    {
        string db;
        string pass;
        public PasswordDialog(string param)
        {
            InitializeComponent();
            passHidden.Focus();
            db = param;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (passVisible.Visibility == Visibility.Visible)
            {
                passVisible.Visibility = Visibility.Hidden;                
                passHidden.Visibility = Visibility.Visible;                
                passCB.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#323B4B"));

            }
            else
            {
                passVisible.Text = passHidden.Password;
                passVisible.Visibility = Visibility.Visible;
                
                passHidden.Visibility = Visibility.Hidden;
                passCB.Foreground = new SolidColorBrush(Colors.Red);
            }
            
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Database.db_name = db;
            Constants.dbName = Database.db_name;
            
            string query = "SELECT password from app_settings";
            Connection.adapter.SelectCommand = new SQLiteCommand(query, Connection.sqlite);
            SQLiteConnection con = new SQLiteConnection("Data Source=" + Constants.folderpath + Constants.dbName + ".db" + ";Foreign Key Constraints=On;" + Constants.dbVersion);        
            SQLiteCommand cmd = new SQLiteCommand(query, con);
            try
            {
                con.Open();
                using (SQLiteDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        pass = (reader["password"].ToString());
                    }
                }
                if (pass == passHidden.Password)
                {
                    Constants.isLanding = false;
                    Database.pass = passHidden.Password;
                    Window window = new Window1();
                    window.WindowState = WindowState.Maximized;
                    window.Show();
                    foreach (Window current in Application.Current.Windows)
                    {
                        if (current.GetType() == typeof(AppSettings))
                        {
                            (current as AppSettings).Hide();
                        }
                    }
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Password Salah!");
                }
            }
            catch (Exception ex) {
                MessageBox.Show("Ada kesalahan!, "+ ex.ToString());
            }
            finally
            {
                con.Close();
            }
        }

    }
}
