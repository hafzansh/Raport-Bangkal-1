using Raport.Helper;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Raport.Pages.Subjects
{
    public partial class mp_ips : Page
    {
        public mp_ips()
        {

            InitializeComponent();
            Constants.current1 = Database.kd_ips3;
            Constants.current3 = Database.kd_ips4;
            Constants.current2 = Database.kkm_ips;            
            validate();
            try
            {
                clearTable();
                fillConnect();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error" + ex);
            }



        }
        private void fillConnect()
        {
            Connection.MP_KD3(Constants.ips, Constants.ips_title);
            Connection.MP_KD4(Constants.ips, Constants.ips_title2);
            Connection.KD("ips3", "kd_ips3", Database.kd_ips3);
            Connection.KD("ips4", "kd_ips4", Database.kd_ips4);
            data_kd.ItemsSource = Connection.dataset.Tables["kd_ips3"].DefaultView;
            data.ItemsSource = Connection.dataset.Tables[Constants.ips_title].DefaultView;
            data_kd2.ItemsSource = Connection.dataset.Tables["kd_ips4"].DefaultView;
            data2.ItemsSource = Connection.dataset.Tables[Constants.ips_title2].DefaultView;
        }
        private void clearTable()
        {
            Connection.dataset.Tables[Constants.ips_title].Clear();
            Connection.dataset.Tables[Constants.ips_title2].Clear();
            Connection.dataset.Tables["kd_ips3"].Clear();
            Connection.dataset.Tables["kd_ips4"].Clear();
        }
        private void validate()
        {
            ExpanderKD3.Title = "Aspek Pengetahuan (" + Constants.current1 + ")";
            ExpanderKD4.Title = "Aspek Keterampilan (" + Constants.current3 + ")";
            if (Database.kd_ips3 == 1)
            {
                data.Columns[7].IsReadOnly = data.Columns[6].IsReadOnly = data.Columns[5].IsReadOnly = data.Columns[4].IsReadOnly = true;
                data.Columns[12].IsReadOnly = data.Columns[11].IsReadOnly = data.Columns[10].IsReadOnly = data.Columns[9].IsReadOnly = true;
            }
            else if (Database.kd_ips3 == 2)
            {
                data.Columns[7].IsReadOnly = data.Columns[6].IsReadOnly = data.Columns[5].IsReadOnly = true;
                data.Columns[12].IsReadOnly = data.Columns[11].IsReadOnly = data.Columns[10].IsReadOnly = true;
            }
            else if (Database.kd_ips3 == 3)
            {
                data.Columns[7].IsReadOnly = data.Columns[6].IsReadOnly = true;
                data.Columns[12].IsReadOnly = data.Columns[11].IsReadOnly = true;
            }
            else if (Database.kd_ips3 == 4)
            {
                data.Columns[7].IsReadOnly = true;
                data.Columns[12].IsReadOnly = true;
            }

            if (Database.kd_ips4 == 1)
            {
                data2.Columns[7].IsReadOnly = data2.Columns[6].IsReadOnly = data2.Columns[5].IsReadOnly = data2.Columns[4].IsReadOnly = true;
            }
            else if (Database.kd_ips4 == 2)
            {
                data2.Columns[7].IsReadOnly = data2.Columns[6].IsReadOnly = data2.Columns[5].IsReadOnly = true;
            }
            else if (Database.kd_ips4 == 3)
            {
                data2.Columns[7].IsReadOnly = data2.Columns[6].IsReadOnly = true;
            }
            else if (Database.kd_ips4 == 4)
            {
                data2.Columns[7].IsReadOnly = true;
            }
        }
        private void data_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            foreach (Window window in Application.Current.Windows)
            {
                if (window.GetType() == typeof(Window1))
                {
                    (window as Window1).save.IsEnabled = true;
                }
            }

        }

        private void data_PreviewKeyDown(object sender, KeyEventArgs e)
        {            
            UIElement uiElement = e.OriginalSource as UIElement;
            switch (e.Key)
            {
                case Key.Left:
                    e.Handled = true;                    
                    _ = uiElement.MoveFocus(new TraversalRequest(FocusNavigationDirection.Left));
                    break;
                case Key.Right:
                    e.Handled = true;
                    _ = uiElement.MoveFocus(new TraversalRequest(FocusNavigationDirection.Right));
                    break;
                case Key.Up:
                    e.Handled = true;
                    _ = uiElement.MoveFocus(new TraversalRequest(FocusNavigationDirection.Up));
                    break;
                case Key.Down:
                    e.Handled = true;
                    _ = uiElement.MoveFocus(new TraversalRequest(FocusNavigationDirection.Down));
                    break;
            }
        }
    }
}
