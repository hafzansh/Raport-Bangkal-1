using Raport.Helper;
using System;
using System.Collections.Generic;
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

namespace Raport.Pages
{
    /// <summary>
    /// Interaction logic for Sikap.xaml
    /// </summary>
    public partial class Sikap : Page
    {
        public Sikap()
        {
            InitializeComponent();
            Modal.Spinner(progress =>
            {
                progress.Report("Loading");
                Connection.dataset.Tables[Constants.sikap_title].Clear();
                progress.Report("Loading");
                Connection.SikapCon();
                progress.Report("Loading");
                this.Dispatcher.BeginInvoke((Action)delegate () {
                    sikapDG.ItemsSource = Connection.dataset.Tables[Constants.sikap_title].DefaultView;
                });
            }
           );
        }
        private void DataGrid_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = (e.Row.GetIndex() + 1).ToString();
        }
        private void sikapDG_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            foreach (Window window in Application.Current.Windows)
            {
                if (window.GetType() == typeof(Window1))
                {
                    (window as Window1).save.IsEnabled = true;
                }
            }
        }
    }
}
