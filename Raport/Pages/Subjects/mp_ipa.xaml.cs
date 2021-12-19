using Raport.Helper;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Raport.Pages.Subjects
{
    public partial class mp_ipa : Page
    {
        public mp_ipa()
        {

            InitializeComponent();
            Constants.current1 = Database.kd_ipa3;
            Constants.current3 = Database.kd_ipa4;
            Constants.current2 = Database.kkm_ipa;            
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
            Connection.MP_KD3(Constants.ipa, Constants.ipa_title);
            Connection.MP_KD4(Constants.ipa, Constants.ipa_title2);
            Connection.KD("ipa3", "kd_ipa3", Database.kd_ipa3);
            Connection.KD("ipa4", "kd_ipa4", Database.kd_ipa4);
            data_kd.ItemsSource = Connection.dataset.Tables["kd_ipa3"].DefaultView;
            data.ItemsSource = Connection.dataset.Tables[Constants.ipa_title].DefaultView;
            data_kd2.ItemsSource = Connection.dataset.Tables["kd_ipa4"].DefaultView;
            data2.ItemsSource = Connection.dataset.Tables[Constants.ipa_title2].DefaultView;
        }
        private void clearTable()
        {
            Connection.dataset.Tables[Constants.ipa_title].Clear();
            Connection.dataset.Tables[Constants.ipa_title2].Clear();
            Connection.dataset.Tables["kd_ipa3"].Clear();
            Connection.dataset.Tables["kd_ipa4"].Clear();
        }
        private void validate()
        {
            ExpanderKD3.Title = "Aspek Pengetahuan (" + Constants.current1 + ")";
            ExpanderKD4.Title = "Aspek Keterampilan (" + Constants.current3 + ")";
            if (Database.kd_ipa3 == 1)
            {
                data.Columns[7].IsReadOnly = data.Columns[6].IsReadOnly = data.Columns[5].IsReadOnly = data.Columns[4].IsReadOnly = true;
                data.Columns[12].IsReadOnly = data.Columns[11].IsReadOnly = data.Columns[10].IsReadOnly = data.Columns[9].IsReadOnly = true;
            }
            else if (Database.kd_ipa3 == 2)
            {
                data.Columns[7].IsReadOnly = data.Columns[6].IsReadOnly = data.Columns[5].IsReadOnly = true;
                data.Columns[12].IsReadOnly = data.Columns[11].IsReadOnly = data.Columns[10].IsReadOnly = true;
            }
            else if (Database.kd_ipa3 == 3)
            {
                data.Columns[7].IsReadOnly = data.Columns[6].IsReadOnly = true;
                data.Columns[12].IsReadOnly = data.Columns[11].IsReadOnly = true;
            }
            else if (Database.kd_ipa3 == 4)
            {
                data.Columns[7].IsReadOnly = true;
                data.Columns[12].IsReadOnly = true;
            }

            if (Database.kd_ipa4 == 1)
            {
                data2.Columns[7].IsReadOnly = data2.Columns[6].IsReadOnly = data2.Columns[5].IsReadOnly = data2.Columns[4].IsReadOnly = true;
            }
            else if (Database.kd_ipa4 == 2)
            {
                data2.Columns[7].IsReadOnly = data2.Columns[6].IsReadOnly = data2.Columns[5].IsReadOnly = true;
            }
            else if (Database.kd_ipa4 == 3)
            {
                data2.Columns[7].IsReadOnly = data2.Columns[6].IsReadOnly = true;
            }
            else if (Database.kd_ipa4 == 4)
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
