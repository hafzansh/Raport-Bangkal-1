using Raport.Helper;
using System.Collections.Generic;
using System.Data;
using System.Text;
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
    public partial class mp_pkn : Page
    {        
        public mp_pkn()
        {

            InitializeComponent();
            Constants.current1 = Database.kd_pkn3;
            Connection.dataset.Tables[Constants.pkn_title].Clear();
            Connection.DBConnection2(Constants.pkn, Constants.pkn_title);
            Connection.dataset.Tables["kd_pkn3"].Clear();
            Connection.KD3("pkn3", "kd_pkn3", Database.kd_pkn3);
            data_kd.ItemsSource = Connection.dataset.Tables["kd_pkn3"].DefaultView;
            data.ItemsSource = Connection.dataset.Tables[Constants.pkn_title].DefaultView;
            if (Database.kd_pkn3 == 1)
            {
                data.Columns[7].IsReadOnly = data.Columns[6].IsReadOnly = data.Columns[5].IsReadOnly = data.Columns[4].IsReadOnly = true;
            }else if (Database.kd_pkn3 == 2)
            {
                data.Columns[7].IsReadOnly = data.Columns[6].IsReadOnly = data.Columns[5].IsReadOnly = true;
            }
            else if (Database.kd_pkn3 == 3)
            {
                data.Columns[7].IsReadOnly = data.Columns[6].IsReadOnly = true;
            }
            else if (Database.kd_pkn3 == 4)
            {
                data.Columns[7].IsReadOnly = true;
            }
            else
            {

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
