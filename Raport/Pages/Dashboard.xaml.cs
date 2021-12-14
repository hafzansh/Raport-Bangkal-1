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
    /// Lógica de interacción para Dashboard.xaml
    /// </summary>
    public partial class Dashboard : Page
    {
        public Dashboard()
        {
            InitializeComponent();
            //Connection.DBConnection(Constants.mtk, Constants.mtk_title);
            DG_mtk.ItemsSource = Connection.dataset.Tables[Constants.mtk_title].DefaultView;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Connection.UpdateDB(Connection.adapter, Connection.dataset, Constants.mtk_title,Constants.mtk);
        }

        private void DG_mtk_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
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
