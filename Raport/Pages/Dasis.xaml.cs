using Raport.Helper;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for Dasis.xaml
    /// </summary>
    public partial class Dasis : Page
    {

        public Dasis()
        {
            InitializeComponent();            
            Modal.Spinner(progress => 
            {
                progress.Report("Loading");
                Connection.dataset.Tables[Constants.dasis_title].Clear();
                progress.Report("Loading");
                Connection.DBConnection(Constants.dasis, Constants.dasis_title);
                progress.Report("Loading");
                this.Dispatcher.BeginInvoke((Action)delegate () {
                    dasisDG.ItemsSource = Connection.dataset.Tables[Constants.dasis_title].DefaultView;
                });
            }
            );
            
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

        private void dasisDG_PreviewKeyDown(object sender, KeyEventArgs e)
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

        private void dasisDG_PreparingCellForEdit(object sender, DataGridPreparingCellForEditEventArgs e)
        {
            
        }

    }
}
