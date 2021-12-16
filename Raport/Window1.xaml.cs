using Raport.Helper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Raport
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    
    public partial class Window1 : Window
    {
        long timeSaved = DateTime.UtcNow.Ticks;
        public static RoutedCommand Save = new RoutedCommand();
        public static RoutedCommand Toggle = new RoutedCommand();
        System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
        bool Home = true;
        bool dashboard, dasis = false;
        public Window1()
        {
            InitializeComponent();
            setConnection();
            btnHome.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
            Save.InputGestures.Add(new KeyGesture(Key.S, ModifierKeys.Control));
            Toggle.InputGestures.Add(new KeyGesture(Key.Tab, ModifierKeys.Control));
            
        }
        private void exitPrompt()
        {
            string msgtext = "Simpan data yang telah dirubah?";
            string txt = "Raport K13 SDN Bangkal 1";
            MessageBoxButton button = MessageBoxButton.YesNoCancel;
            MessageBoxResult result = MessageBox.Show(msgtext, txt, button);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    MessageBox.Show("dataDisimpan");
                    break;
                case MessageBoxResult.No:
                    Connection.sqlite.Close();
                    Constants.CloseApp();
                    break;
                case MessageBoxResult.Cancel:                    
                    break;
            }
        }        
        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            savedTime();
        }
        private void savedTime()
        {
            const int SECOND = 1;
            const int MINUTE = 60 * SECOND;
            const int HOUR = 60 * MINUTE;
            const int DAY = 24 * HOUR;
            const int MONTH = 30 * DAY;
            string a;
            var ts = new TimeSpan(DateTime.UtcNow.Ticks - (Int64)timeSaved);
            
            double delta = Math.Abs(ts.TotalSeconds);

            if (delta < 1 * MINUTE)

                a = ts.Seconds == 1 ? "beberapa detik yang lalu" : "beberapa detik yang lalu";

            else if (delta < 2 * MINUTE)
                a = "Semenit yang lalu";

            else if (delta < 45 * MINUTE)
                a = ts.Minutes + " menit yang lalu";

            else if (delta < 90 * MINUTE)
                a = "Satu jam yang lalu";

            else if (delta < 24 * HOUR)
                a = ts.Hours + " jam yang lalu";

            else if (delta < 48 * HOUR)
                a = "Kemarin";

            else if (delta < 30 * DAY)
                a = ts.Days + " Hari yang lalu";

            else if (delta < 12 * MONTH)
            {
                long months = Convert.ToInt64(Math.Floor((double)ts.Days / 30));
                a = months <= 1 ? "1 Bulan yang lalu" : months + " Bulan yang lalu";
            }
            else
            {
                long years = Convert.ToInt64(Math.Floor((double)ts.Days / 365));
                a = years <= 1 ? "1 Tahun yang lalu" : years + " Tahun yang lalu";
            }

            lastSaved.Content = "Terakhir disimpan, " + a;
        }
        private void SaveExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            if (save.IsEnabled) { 
            save.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
            }
        }
        private void ToggleExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            if (Tg_Btn.IsChecked == true)
            {
                Tg_Btn.IsChecked = false;
            }
            else
            {
                Tg_Btn.IsChecked = true;
            }

        }
        private void setConnection()
        {
            try
            {
                Connection.DBConnection(Constants.dasis, Constants.dasis_title);
                Connection.DBConnection2(Constants.mtk, Constants.mtk_title);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Koneksi Database error, " + ex);
            }
        }
        private void BG_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Tg_Btn.IsChecked = false;
        }

        // Start: MenuLeft PopupButton //
        private void btnHome_MouseEnter(object sender, MouseEventArgs e)
        {
            if (Tg_Btn.IsChecked == false)
            {
                Popup.PlacementTarget = btnHome;
                Popup.Placement = PlacementMode.Right;
                Popup.IsOpen = true;
                Header.PopupText.Text = "Home";
            }
        }

        private void btnHome_MouseLeave(object sender, MouseEventArgs e)
        {
            Popup.Visibility = Visibility.Collapsed;
            Popup.IsOpen = false;
        }
        private void btnDasis_MouseEnter(object sender, MouseEventArgs e)
        {
            if (Tg_Btn.IsChecked == false)
            {
                Popup.PlacementTarget = btnProducts;
                Popup.Placement = PlacementMode.Right;
                Popup.IsOpen = true;
                Header.PopupText.Text = "Data Siswa";
            }
        }

        private void btnDasis_MouseLeave(object sender, MouseEventArgs e)
        {
            Popup.Visibility = Visibility.Collapsed;
            Popup.IsOpen = false;
        }
        private void btnDashboard_MouseEnter(object sender, MouseEventArgs e)
        {
            if (Tg_Btn.IsChecked == false)
            {
                Popup.PlacementTarget = btnDashboard;
                Popup.Placement = PlacementMode.Right;
                Popup.IsOpen = true;
                Header.PopupText.Text = "Dashboard";
            }
        }

        private void btnDashboard_MouseLeave(object sender, MouseEventArgs e)
        {
            Popup.Visibility = Visibility.Collapsed;
            Popup.IsOpen = false;
        }

        private void btnProducts_MouseEnter(object sender, MouseEventArgs e)
        {
            if (Tg_Btn.IsChecked == false)
            {
                Popup.PlacementTarget = btnProducts;
                Popup.Placement = PlacementMode.Right;
                Popup.IsOpen = true;
                Header.PopupText.Text = "Products";
            }
        }

        private void btnProducts_MouseLeave(object sender, MouseEventArgs e)
        {
            Popup.Visibility = Visibility.Collapsed;
            Popup.IsOpen = false;
        }

        private void btnProductStock_MouseEnter(object sender, MouseEventArgs e)
        {
            if (Tg_Btn.IsChecked == false)
            {
                Popup.PlacementTarget = btnProductStock;
                Popup.Placement = PlacementMode.Right;
                Popup.IsOpen = true;
                Header.PopupText.Text = "Product Stock";
            }
        }

        private void btnProductStock_MouseLeave(object sender, MouseEventArgs e)
        {
            Popup.Visibility = Visibility.Collapsed;
            Popup.IsOpen = false;
        }

        private void btnOrderList_MouseEnter(object sender, MouseEventArgs e)
        {
            if (Tg_Btn.IsChecked == false)
            {
                Popup.PlacementTarget = btnOrderList;
                Popup.Placement = PlacementMode.Right;
                Popup.IsOpen = true;
                Header.PopupText.Text = "Order List";
            }
        }

        private void btnOrderList_MouseLeave(object sender, MouseEventArgs e)
        {
            Popup.Visibility = Visibility.Collapsed;
            Popup.IsOpen = false;
        }

        private void btnBilling_MouseEnter(object sender, MouseEventArgs e)
        {
            if (Tg_Btn.IsChecked == false)
            {
                Popup.PlacementTarget = btnBilling;
                Popup.Placement = PlacementMode.Right;
                Popup.IsOpen = true;
                Header.PopupText.Text = "Billing";
            }
        }

        private void btnBilling_MouseLeave(object sender, MouseEventArgs e)
        {
            Popup.Visibility = Visibility.Collapsed;
            Popup.IsOpen = false;
        }

        private void btnPointOfSale_MouseEnter(object sender, MouseEventArgs e)
        {
            if (Tg_Btn.IsChecked == false)
            {
                Popup.PlacementTarget = btnPointOfSale;
                Popup.Placement = PlacementMode.Right;
                Popup.IsOpen = true;
                Header.PopupText.Text = "Poin Of Sale";
            }
        }

        private void btnPointOfSale_MouseLeave(object sender, MouseEventArgs e)
        {
            Popup.Visibility = Visibility.Collapsed;
            Popup.IsOpen = false;
        }

        private void btnSecurity_MouseEnter(object sender, MouseEventArgs e)
        {
            if (Tg_Btn.IsChecked == false)
            {
                Popup.PlacementTarget = btnSecurity;
                Popup.Placement = PlacementMode.Right;
                Popup.IsOpen = true;
                Header.PopupText.Text = "Security";
            }
        }

        private void btnSecurity_MouseLeave(object sender, MouseEventArgs e)
        {
            Popup.Visibility = Visibility.Collapsed;
            Popup.IsOpen = false;
        }
        private void btnSetting_MouseEnter(object sender, MouseEventArgs e)
        {
            if (Tg_Btn.IsChecked == false)
            {
                Popup.PlacementTarget = btnSetting;
                Popup.Placement = PlacementMode.Right;
                Popup.IsOpen = true;
                Header.PopupText.Text = "Setting";
            }
        }

        private void btnSetting_MouseLeave(object sender, MouseEventArgs e)
        {
            Popup.Visibility = Visibility.Collapsed;
            Popup.IsOpen = false;
        }
        // End: MenuLeft PopupButton //

        // Start: Button Close | Restore | Minimize 
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            if (save.IsEnabled)
            {
                exitPrompt();
            }
            else
            {
                Connection.sqlite.Close();
                Constants.CloseApp();
            }
        }

        private void btnRestore_Click(object sender, RoutedEventArgs e)
        {
            if (WindowState == WindowState.Normal)
                WindowState = WindowState.Maximized;
            else
                WindowState = WindowState.Normal;
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }
        // End: Button Close | Restore | Minimize

        private void btnAction(String title,String uri)
        {
            fContainer.Navigate(new Uri(uri, UriKind.RelativeOrAbsolute));
            appTitle.Content = title;
            Home = dasis = dashboard = false;
        }

        private void btnHome_Click(object sender, RoutedEventArgs e)
        {
            btnAction("Home", "Pages/Home.xaml");            
            Home = true;            
        }

        private void btnDashboard_Click(object sender, RoutedEventArgs e)
        {
            btnAction("Dashboard", "Pages/Dashboard.xaml");
            dashboard = true;
        }
        private void btnDasis_Click(object sender, RoutedEventArgs e)
        {
            btnAction("Data Siswa", "Pages/Dasis.xaml");
            dasis = true;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (Home)
            {

            } else if (dasis)
            {
                Connection.UpdateDB(Connection.adapter, Connection.dataset, Constants.dasis_title, Constants.dasis);
                save.IsEnabled = false;
                reCount();
            }
            else if (dashboard)
            {
                
                Connection.UpdateDB(Connection.adapter, Connection.dataset, Constants.mtk_title, Constants.mtk);
                save.IsEnabled = false;
                reCount();
                
            }
        }

        public void reCount()
        {
            timeSaved = DateTime.UtcNow.Ticks;
            dispatcherTimer.Stop();
            savedTime();            
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 1, 0);
            dispatcherTimer.Start();
        }

        private void StackPanel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                if (WindowState == WindowState.Normal)
                    WindowState = WindowState.Maximized;
                else
                    WindowState = WindowState.Normal;
            }
        }
        private void home_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                DragMove();
        }        
    }
}
