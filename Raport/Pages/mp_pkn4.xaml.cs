using Raport.Helper;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Raport.Pages
{
    public partial class mp_pkn4 : Page
    {
        private static TaskScheduler GetSyncronizationContent() =>
     SynchronizationContext.Current != null ?
          TaskScheduler.FromCurrentSynchronizationContext() :
          TaskScheduler.Current;
        public mp_pkn4()
        {

            InitializeComponent();
            Constants.current1 = Database.kd_pkn3;
            Constants.current3 = Database.kd_pkn4;
            Constants.current2 = Database.kkm_pkn;
            Connection.dataset.Tables[Constants.pkn_title2].Clear();
            Connection.MP_KD4(Constants.pkn, Constants.pkn_title2);
            Connection.dataset.Tables["kd_pkn4"].Clear();
            Connection.KD("pkn4", "kd_pkn4", Database.kd_pkn4);
            data_kd.ItemsSource = Connection.dataset.Tables["kd_pkn4"].DefaultView;            
            data.ItemsSource = Connection.dataset.Tables[Constants.pkn_title2].DefaultView;            
            //if (Database.kd_pkn3 == 1)
            //{
            //    data.Columns[7].IsReadOnly = data.Columns[6].IsReadOnly = data.Columns[5].IsReadOnly = data.Columns[4].IsReadOnly = true;
            //}
            //else if (Database.kd_pkn3 == 2)
            //{
            //    data.Columns[7].IsReadOnly = data.Columns[6].IsReadOnly = data.Columns[5].IsReadOnly = true;
            //}
            //else if (Database.kd_pkn3 == 3)
            //{
            //    data.Columns[7].IsReadOnly = data.Columns[6].IsReadOnly = true;
            //}
            //else if (Database.kd_pkn3 == 4)
            //{
            //    data.Columns[7].IsReadOnly = true;
            //}
            //else
            //{

            //}
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
