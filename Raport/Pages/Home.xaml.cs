using Raport.Helper;
using Raport.Services;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace Raport.Pages
{
    /// <summary>
    /// Lógica de interacción para Home.xaml
    /// </summary>
    public partial class Home : Page
    {
        public Home()
        {
            InitializeComponent();
            dg_home.ItemsSource = Connection.dataset.Tables[Constants.dasis_title].DefaultView;
            string students = Connection.dataset.Tables[Constants.dasis_title].Rows.Count.ToString();
            studentsCount.Content = students + " Orang";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Report.CreateDasis(Connection.dataset.Tables[Constants.dasis_title]);
        }
        private void DataGrid_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = (e.Row.GetIndex() + 1).ToString();
        }
        private void Button2_Click(object sender, RoutedEventArgs e)
        {
            DataGrid datagrid = ((Button)sender).CommandParameter as DataGrid;            
            var selectedIndex = datagrid.SelectedIndex;
            string name = Connection.dataset.Tables[Constants.dasis_title].Rows[selectedIndex]["nama"].ToString();
            string msgtext = "Cetak "+name+"?";
            string txt = "Cetak Raport";
            MessageBoxButton button = MessageBoxButton.YesNo;
            MessageBoxResult result = MessageBox.Show(msgtext, txt, button);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    Report.createRaport(selectedIndex);
                    break;
                case MessageBoxResult.No:
                    
                    break;
            }
        }

        private void Button3_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(Environment.CurrentDirectory);
        }        
        private void Button4_Click(object sender, RoutedEventArgs e)
        {
            foreach (Window window in Application.Current.Windows)
            {
                if (window.GetType() == typeof(Window1))
                {
                    (window as Window1).btnDasis.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
                }
            }
        }

    }
}
