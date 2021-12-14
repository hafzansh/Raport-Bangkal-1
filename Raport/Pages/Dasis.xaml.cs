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

namespace Raport.Pages
{
    /// <summary>
    /// Interaction logic for Dasis.xaml
    /// </summary>
    public partial class Dasis : Page
    {
        public Dasis()
        {
            InitializeComponent();
            //Connection.DBConnection(Constants.dasis, Constants.dasis_title);
            dasisDG.ItemsSource = Connection.dataset.Tables[Constants.dasis_title].DefaultView;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Connection.UpdateDB(Connection.adapter, Connection.dataset, Constants.dasis_title,Constants.dasis);
        }

        private void dasisDG_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
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
