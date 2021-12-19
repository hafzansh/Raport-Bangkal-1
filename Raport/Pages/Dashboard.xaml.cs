using Raport.Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
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
            Constants.current1 = Database.kd_mtk3;
            InitializeComponent();            
            Connection.dataset.Tables[Constants.mtk_title].Clear();
            Connection.MP_KD3(Constants.mtk, Constants.mtk_title);            
            DG_mtk.ItemsSource = Connection.dataset.Tables[Constants.mtk_title].DefaultView;
            if (Database.kd_mtk3 == 1)
            {
                DG_mtk.Columns[7].IsReadOnly = DG_mtk.Columns[6].IsReadOnly = DG_mtk.Columns[5].IsReadOnly = DG_mtk.Columns[5].IsReadOnly = true;
            }else if (Database.kd_mtk3 == 2)
            {
                DG_mtk.Columns[7].IsReadOnly = DG_mtk.Columns[6].IsReadOnly = DG_mtk.Columns[5].IsReadOnly = true;
            }
            else if (Database.kd_mtk3 == 3)
            {
                DG_mtk.Columns[7].IsReadOnly = DG_mtk.Columns[6].IsReadOnly = true;
            }
            else if (Database.kd_mtk3 == 4)
            {
                DG_mtk.Columns[7].IsReadOnly = true;
            }
            else
            {

            }
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

        private void DG_mtk_PreviewKeyDown(object sender, KeyEventArgs e)
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
