using Raport.Helper;
using Raport.Helper.ModalUI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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

namespace Raport.Pages.Landing
{
    /// <summary>
    /// Interaction logic for WelcomeScreen.xaml
    /// </summary>
    public partial class WelcomeScreen : Page
    {
        public static FileInfo[] Files;
        public static int idx = 0;
        public WelcomeScreen()
        {
            InitializeComponent();
            populate();
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(listFiles.ItemsSource);
            view.SortDescriptions.Add(new System.ComponentModel.SortDescription("LastSaved", System.ComponentModel.ListSortDirection.Descending));
            view.Filter = UserFilter;
        }

        private void btBrowse_Click(object sender, RoutedEventArgs e)
        {
            foreach (Window window in Application.Current.Windows)
            {
                if (window.GetType() == typeof(AppSettings))
                {
                    (window as AppSettings).settings_frame.Navigate(new Uri("Pages/Landing/UserSettings.xaml", UriKind.RelativeOrAbsolute));
                }
            }
        }
        public void populate()
        {
            string[] filePaths = Directory.GetFiles(Constants.folderpath, "*.db",
                                         SearchOption.TopDirectoryOnly);
            DirectoryInfo d = new DirectoryInfo(Constants.folderpath); //Assuming Test is your Folder

            Files = d.GetFiles("*.db");
            List<FileName> items = new List<FileName>();
            for (int i = 0; i < filePaths.Length; i++)
            {
                items.Add(new FileName() { Name = StringChange.GetUntilOrEmpty(Files[i].Name.ToString()), Time = Files[i].CreationTime.ToString("dd/MM/yy hh:mm"), Header = (kelas(Files[i].Name) + semester(Files[i].Name)),Details = Path.GetFileNameWithoutExtension(Files[i].FullName), LastSaved = Files[i].LastWriteTime.ToString("dd/MM/yy hh:mm") });
            }
            listFiles.ItemsSource = items;
        }
        private string semester(string target)
        {
            string[] kls = { "Ganjil", "Genap" };
            if (target.Contains(kls[0]))
            {
                target = " - Semester " + kls[0];
            }
            else if (target.Contains(kls[1]))
            {
                target = " - Semester " + kls[1];
            }
            return target;
        }
        private bool UserFilter(object item)
        {
            if (String.IsNullOrEmpty(txtFilter.Text))
                return true;
            else
                return ((item as FileName).Header.IndexOf(txtFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0);
        }

        private void txtFilter_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(listFiles.ItemsSource).Refresh();
        }
        private string kelas(string target)
        {
            string[] kls = { "I (Satu)", "II (Dua)", "III (Tiga)", "IV (Empat)", "V (Lima)", "VI (Enam)" };
            if (target.Contains(kls[0]))
            {
                target = kls[0];
            }
            else if (target.Contains(kls[1]))
            {
                target = kls[1];
            }
            else if (target.Contains(kls[2]))
            {
                target = kls[2];
            }
            else if (target.Contains(kls[3]))
            {
                target = kls[3];
            }
            else if (target.Contains(kls[4]))
            {
                target = kls[4];
            }
            else if (target.Contains(kls[5]))
            {
                target = kls[5];
            }
            return "Kelas " + target;
        }
        public class FileName
        {
            public string Name { get; set; }

            public string Time { get; set; }
            public string Header { get; set; }
            public string Details { get; set; }
            public string LastSaved { get; set; }
        }


        private void openFolder(object sender, RoutedEventArgs e)
        {
            bool exists = Directory.Exists(Constants.folderpath);

            if (!exists)
                Directory.CreateDirectory(Constants.folderpath);

            Process.Start("explorer.exe", Constants.folderpath);
            
        }

        private void listFiles_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            FileName raport = (FileName)listFiles.SelectedItems[0];            
            PasswordDialog pd = new PasswordDialog(raport.Details);
            pd.ShowDialog();

        }
    }
}
