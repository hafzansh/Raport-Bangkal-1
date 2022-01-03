using Raport.Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Raport
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    /// 
    
    public partial class Window1 : Window
    {
        long timeSaved = DateTime.UtcNow.Ticks;
        public static RoutedCommand Save = new RoutedCommand();
        public static RoutedCommand Toggle = new RoutedCommand();
        System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
        bool Home = true;
        bool absen,sikap,pkn,agm,mtk,bi,ipa,ips,pjok,sbdp,bjr,bing,bta,dashboard, dasis = false;
        private static TaskScheduler GetSyncronizationContent() =>
     SynchronizationContext.Current != null ?
          TaskScheduler.FromCurrentSynchronizationContext() :
          TaskScheduler.Current;
        public Window1()
        {
            InitializeComponent();
            this.Opacity = 0.5;            
            this.Effect = new BlurEffect();
            Modal.Spinner(progress =>
            {
                loadValue();
                progress.Report("Loading");
                this.Dispatcher.BeginInvoke((Action)delegate () {
                    setting.Content = Database.kelas + ", " + Database.semester + ", " + Database.tahun;
                    progress.Report("Loading");
                    guru.Content = Database.wali_kelas;
                    progress.Report("Loading");
                });                
                Connection.setConnection();
                progress.Report("Loading");
            });
            this.Opacity = 1;
            this.Effect = null;
            btnHome.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
            Save.InputGestures.Add(new KeyGesture(Key.S, ModifierKeys.Control));
            Toggle.InputGestures.Add(new KeyGesture(Key.Tab, ModifierKeys.Control));
            
        }
        private void loadValue()
        {
            Connection.sqlite.Open();
            string query = "select * from app_settings";
            SQLiteCommand command = new SQLiteCommand(query, Connection.sqlite);
            SQLiteDataReader dr = command.ExecuteReader();
            while (dr.Read())
            {
                Database.wali_kelas = (string)dr["wali_kelas"];
                Database.nip_wali_kelas = (string)dr["nip_wali_kelas"];
                Database.kepala_sekolah = (string)dr["kepala_sekolah"];
                Database.nip_kepala_sekolah = (string)dr["nip_kepala_sekolah"];
                Database.semester = (string)dr["semester"];
                Database.tahun = (string)dr["tahun"];
                Database.kelas = (string)dr["kelas"];
                Database.kd_agm3 = (int)dr["kd_agm3"];
                Database.kd_agm4 = (int)dr["kd_agm4"];
                Database.kd_pkn3 = (int)dr["kd_pkn3"];
                Database.kd_pkn4 = (int)dr["kd_pkn4"];
                Database.kd_bi3 = (int)dr["kd_bi3"];
                Database.kd_bi4 = (int)dr["kd_bi4"];
                Database.kd_mtk3 = (int)dr["kd_mtk3"];
                Database.kd_mtk4 = (int)dr["kd_mtk4"];
                Database.kd_ipa3 = (int)dr["kd_ipa3"];
                Database.kd_ipa4 = (int)dr["kd_ipa4"];
                Database.kd_ips3 = (int)dr["kd_ips3"];
                Database.kd_ips4 = (int)dr["kd_ips4"];
                Database.kd_sbdp3 = (int)dr["kd_sbdp3"];
                Database.kd_sbdp4 = (int)dr["kd_sbdp4"];
                Database.kd_pjok3 = (int)dr["kd_pjok3"];
                Database.kd_pjok4 = (int)dr["kd_pjok4"];
                Database.kd_bjr3 = (int)dr["kd_bjr3"];
                Database.kd_bjr4 = (int)dr["kd_bjr4"];
                Database.kd_bing3 = (int)dr["kd_bing3"];
                Database.kd_bing4 = (int)dr["kd_bing4"];
                Database.kd_bta3 = (int)dr["kd_bta3"];
                Database.kd_bta4 = (int)dr["kd_bta4"];
                Database.kkm_agm = (int)dr["kkm_agm"];
                Database.kkm_pkn = (int)dr["kkm_pkn"];
                Database.kkm_bi = (int)dr["kkm_bi"];
                Database.kkm_mtk = (int)dr["kkm_mtk"];
                Database.kkm_ipa = (int)dr["kkm_ipa"];
                Database.kkm_ips = (int)dr["kkm_ips"];
                Database.kkm_sbdp = (int)dr["kkm_sbdp"];
                Database.kkm_pjok = (int)dr["kkm_pjok"];
                Database.kkm_bjr = (int)dr["kkm_bjr"];
                Database.kkm_bing = (int)dr["kkm_bing"];
                Database.kkm_bta = (int)dr["kkm_bta"];
            }
            Connection.sqlite.Close();
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
        private void BG_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Tg_Btn.IsChecked = false;
        }
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
        private void btnAction(String title,String uri)
        {
            fContainer.Navigate(new Uri(uri, UriKind.RelativeOrAbsolute));
            appTitle.Content = title;
            absen = sikap = pkn = agm = mtk = bi = ipa = ips = pjok = sbdp = bjr = bing = bta = Home = dasis = dashboard = false;
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
            else if (pkn)
            {
                UpdateData(Connection.adapter, Connection.dataset, Constants.pkn_title, Constants.pkn_title2, Constants.pkn, "pkn3", "kd_pkn3", "pkn4", "kd_pkn4");
                save.IsEnabled = false;
                reCount();

            }
            else if (agm)
            {
                UpdateData(Connection.adapter, Connection.dataset, Constants.agm_title, Constants.agm_title2, Constants.agm, "agm3", "kd_agm3", "agm4", "kd_agm4");
                save.IsEnabled = false;
                reCount();

            }
            else if (bi)
            {
                UpdateData(Connection.adapter, Connection.dataset, Constants.bi_title, Constants.bi_title2, Constants.bi, "bi3", "kd_bi3", "bi4", "kd_bi4");
                save.IsEnabled = false;
                reCount();

            }
            else if (mtk)
            {
                UpdateData(Connection.adapter, Connection.dataset, Constants.mtk_title, Constants.mtk_title2, Constants.mtk, "mtk3", "kd_mtk3", "mtk4", "kd_mtk4");
                save.IsEnabled = false;
                reCount();

            }
            else if (ipa)
            {
                UpdateData(Connection.adapter, Connection.dataset, Constants.ipa_title, Constants.ipa_title2, Constants.ipa, "ipa3", "kd_ipa3", "ipa4", "kd_ipa4");
                save.IsEnabled = false;
                reCount();

            }
            else if (ips)
            {
                UpdateData(Connection.adapter, Connection.dataset, Constants.ips_title, Constants.ips_title2, Constants.ips, "ips3", "kd_ips3", "ips4", "kd_ips4");
                save.IsEnabled = false;
                reCount();

            }
            else if (sbdp)
            {
                UpdateData(Connection.adapter, Connection.dataset, Constants.sbdp_title, Constants.sbdp_title2, Constants.sbdp, "sbdp3", "kd_sbdp3", "sbdp4", "kd_sbdp4");
                save.IsEnabled = false;
                reCount();

            }
            else if (pjok)
            {
                UpdateData(Connection.adapter, Connection.dataset, Constants.pjok_title, Constants.pjok_title2, Constants.pjok, "pjok3", "kd_pjok3", "pjok4", "kd_pjok4");
                save.IsEnabled = false;
                reCount();

            }
            else if (bjr)
            {
                UpdateData(Connection.adapter, Connection.dataset, Constants.bjr_title, Constants.bjr_title2, Constants.bjr, "bjr3", "kd_bjr3", "bjr4", "kd_bjr4");
                save.IsEnabled = false;
                reCount();

            }
            else if (bing)
            {
                UpdateData(Connection.adapter, Connection.dataset, Constants.bing_title, Constants.bing_title2, Constants.bing, "bing3", "kd_bing3", "bing4", "kd_bing4");
                save.IsEnabled = false;
                reCount();

            }
            else if (bta)
            {
                UpdateData(Connection.adapter, Connection.dataset, Constants.bta_title, Constants.bta_title2, Constants.bta, "bta3", "kd_bta3", "bta4", "kd_bta4");
                save.IsEnabled = false;
                reCount();

            }
            else if (absen)
            {
                Connection.UpdateAbsen();
                save.IsEnabled = false;
                reCount();

            }
            else if (sikap)
            {
                Connection.UpdateSikap();
                save.IsEnabled = false;
                reCount();

            }
        }
        private void UpdateData(SQLiteDataAdapter adapter, DataSet dataset, string title3,string title4,string table,string kd3,string kdtable3,string kd4,string kdtable4)
        {
            Connection.UpdateData3(adapter, dataset, title3, table);
            Connection.UpdateData4(adapter, dataset, title4, table);
            Connection.UpdateKD(adapter, dataset, kd3, kdtable3);
            Connection.UpdateKD(adapter, dataset, kd4, kdtable4);
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
        private void btnDasis_MouseEnter(object sender, MouseEventArgs e)
        {
            if (Tg_Btn.IsChecked == false)
            {
                Popup.PlacementTarget = btnDasis;
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
        private void btnDasis_Click(object sender, RoutedEventArgs e)
        {
            if (save.IsEnabled)
            {
                save.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
            }
            btnAction("Data Siswa", "Pages/Dasis.xaml");
            dasis = true;
        }
        private void btnPkn_MouseEnter(object sender, MouseEventArgs e)
        {
            if (Tg_Btn.IsChecked == false)
            {
                Popup.PlacementTarget = btnPkn;
                Popup.Placement = PlacementMode.Right;
                Popup.IsOpen = true;
                Header.PopupText.Text = Constants.pkn_title;
            }
        }
        private void btnPkn_MouseLeave(object sender, MouseEventArgs e)
        {
            Popup.Visibility = Visibility.Collapsed;
            Popup.IsOpen = false;
        }
        private void btnPkn_Click(object sender, RoutedEventArgs e)
        {
            if (save.IsEnabled)
            {
                save.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
            }
            btnAction(Constants.pkn_title + " (" + Database.kkm_pkn + ")", "Pages/Subjects/mp_pkn.xaml");
            pkn = true;
        }
        private void btnagm_MouseEnter(object sender, MouseEventArgs e)
        {
            if (Tg_Btn.IsChecked == false)
            {
                Popup.PlacementTarget = btnagm;
                Popup.Placement = PlacementMode.Right;
                Popup.IsOpen = true;
                Header.PopupText.Text = Constants.agm_title;
            }
        }
        private void btnagm_MouseLeave(object sender, MouseEventArgs e)
        {
            Popup.Visibility = Visibility.Collapsed;
            Popup.IsOpen = false;
        }
        private void btnagm_Click(object sender, RoutedEventArgs e)
        {
            if (save.IsEnabled)
            {
                save.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
            }
            btnAction(Constants.agm_title + " (" + Database.kkm_agm + ")", "Pages/Subjects/mp_agm.xaml");
            agm = true;
        }
        private void btnbi_MouseEnter(object sender, MouseEventArgs e)
        {
            if (Tg_Btn.IsChecked == false)
            {
                Popup.PlacementTarget = btnbi;
                Popup.Placement = PlacementMode.Right;
                Popup.IsOpen = true;
                Header.PopupText.Text = Constants.bi_title;
            }
        }

        private void btnbi_MouseLeave(object sender, MouseEventArgs e)
        {
            Popup.Visibility = Visibility.Collapsed;
            Popup.IsOpen = false;
        }
        private void btnbi_Click(object sender, RoutedEventArgs e)
        {
            if (save.IsEnabled)
            {
                save.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
            }
            btnAction(Constants.bi_title + " (" + Database.kkm_bi + ")", "Pages/Subjects/mp_bi.xaml");
            bi = true;
        }
        private void btnmtk_MouseEnter(object sender, MouseEventArgs e)
        {
            if (Tg_Btn.IsChecked == false)
            {
                Popup.PlacementTarget = btnmtk;
                Popup.Placement = PlacementMode.Right;
                Popup.IsOpen = true;
                Header.PopupText.Text = Constants.mtk_title;
            }
        }

        private void btnmtk_MouseLeave(object sender, MouseEventArgs e)
        {
            Popup.Visibility = Visibility.Collapsed;
            Popup.IsOpen = false;
        }
        private void btnmtk_Click(object sender, RoutedEventArgs e)
        {
            if (save.IsEnabled)
            {
                save.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
            }
            btnAction(Constants.mtk_title + " (" + Database.kkm_mtk + ")", "Pages/Subjects/mp_mtk.xaml");
            mtk = true;
        }
        private void btnipa_MouseEnter(object sender, MouseEventArgs e)
        {
            if (Tg_Btn.IsChecked == false)
            {
                Popup.PlacementTarget = btnipa;
                Popup.Placement = PlacementMode.Right;
                Popup.IsOpen = true;
                Header.PopupText.Text = Constants.ipa_title;
            }
        }

        private void btnipa_MouseLeave(object sender, MouseEventArgs e)
        {
            Popup.Visibility = Visibility.Collapsed;
            Popup.IsOpen = false;
        }
        private void btnipa_Click(object sender, RoutedEventArgs e)
        {
            if (save.IsEnabled)
            {
                save.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
            }
            btnAction(Constants.ipa_title + " (" + Database.kkm_ipa + ")", "Pages/Subjects/mp_ipa.xaml");
            ipa = true;
        }
        private void btnips_MouseEnter(object sender, MouseEventArgs e)
        {
            if (Tg_Btn.IsChecked == false)
            {
                Popup.PlacementTarget = btnips;
                Popup.Placement = PlacementMode.Right;
                Popup.IsOpen = true;
                Header.PopupText.Text = Constants.ips_title;
            }
        }

        private void btnips_MouseLeave(object sender, MouseEventArgs e)
        {
            Popup.Visibility = Visibility.Collapsed;
            Popup.IsOpen = false;
        }
        private void btnips_Click(object sender, RoutedEventArgs e)
        {
            if (save.IsEnabled)
            {
                save.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
            }
            btnAction(Constants.ips_title + " (" + Database.kkm_ips + ")", "Pages/Subjects/mp_ips.xaml");
            ips = true;
        }
        private void btnsbdp_MouseEnter(object sender, MouseEventArgs e)
        {
            if (Tg_Btn.IsChecked == false)
            {
                Popup.PlacementTarget = btnsbdp;
                Popup.Placement = PlacementMode.Right;
                Popup.IsOpen = true;
                Header.PopupText.Text = Constants.sbdp_title;
            }
        }

        private void btnsbdp_MouseLeave(object sender, MouseEventArgs e)
        {
            Popup.Visibility = Visibility.Collapsed;
            Popup.IsOpen = false;
        }
        private void btnsbdp_Click(object sender, RoutedEventArgs e)
        {
            if (save.IsEnabled)
            {
                save.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
            }
            btnAction(Constants.sbdp_title + " (" + Database.kkm_sbdp + ")", "Pages/Subjects/mp_sbdp.xaml");
            sbdp = true;
        }
        private void btnpjok_MouseEnter(object sender, MouseEventArgs e)
        {
            if (Tg_Btn.IsChecked == false)
            {
                Popup.PlacementTarget = btnpjok;
                Popup.Placement = PlacementMode.Right;
                Popup.IsOpen = true;
                Header.PopupText.Text = Constants.pjok_title;
            }
        }

        private void btnpjok_MouseLeave(object sender, MouseEventArgs e)
        {
            Popup.Visibility = Visibility.Collapsed;
            Popup.IsOpen = false;
        }
        private void btnpjok_Click(object sender, RoutedEventArgs e)
        {
            if (save.IsEnabled)
            {
                save.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
            }
            btnAction(Constants.pjok_title + " (" + Database.kkm_pjok + ")", "Pages/Subjects/mp_pjok.xaml");
            pjok = true;
        }
        private void btnbjr_MouseEnter(object sender, MouseEventArgs e)
        {
            if (Tg_Btn.IsChecked == false)
            {
                Popup.PlacementTarget = btnbjr;
                Popup.Placement = PlacementMode.Right;
                Popup.IsOpen = true;
                Header.PopupText.Text = Constants.bjr_title;
            }
        }

        private void btnbjr_MouseLeave(object sender, MouseEventArgs e)
        {
            Popup.Visibility = Visibility.Collapsed;
            Popup.IsOpen = false;
        }
        private void btnbjr_Click(object sender, RoutedEventArgs e)
        {
            if (save.IsEnabled)
            {
                save.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
            }
            btnAction(Constants.bjr_title + " (" + Database.kkm_bjr + ")", "Pages/Subjects/mp_bjr.xaml");
            bjr = true;
        }
        private void btnbing_MouseEnter(object sender, MouseEventArgs e)
        {
            if (Tg_Btn.IsChecked == false)
            {
                Popup.PlacementTarget = btnbing;
                Popup.Placement = PlacementMode.Right;
                Popup.IsOpen = true;
                Header.PopupText.Text = Constants.bing_title;
            }
        }

        private void btnbing_MouseLeave(object sender, MouseEventArgs e)
        {
            Popup.Visibility = Visibility.Collapsed;
            Popup.IsOpen = false;
        }
        private void btnbing_Click(object sender, RoutedEventArgs e)
        {
            if (save.IsEnabled)
            {
                save.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
            }
            btnAction(Constants.bing_title + " (" + Database.kkm_bing + ")", "Pages/Subjects/mp_bing.xaml");
            bing = true;
        }
        private void btnbta_MouseEnter(object sender, MouseEventArgs e)
        {
            if (Tg_Btn.IsChecked == false)
            {
                Popup.PlacementTarget = btnbta;
                Popup.Placement = PlacementMode.Right;
                Popup.IsOpen = true;
                Header.PopupText.Text = Constants.bta_title;
            }
        }

        private void btnbta_MouseLeave(object sender, MouseEventArgs e)
        {
            Popup.Visibility = Visibility.Collapsed;
            Popup.IsOpen = false;
        }
        private void btnbta_Click(object sender, RoutedEventArgs e)
        {
            if (save.IsEnabled)
            {
                save.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
            }
            btnAction(Constants.bta_title + " (" + Database.kkm_bta + ")", "Pages/Subjects/mp_bta.xaml");
            bta = true;
        }
        private void btnAbsen_MouseEnter(object sender, MouseEventArgs e)
        {
            if (Tg_Btn.IsChecked == false)
            {
                Popup.PlacementTarget = btnAbsen;
                Popup.Placement = PlacementMode.Right;
                Popup.IsOpen = true;
                Header.PopupText.Text = Constants.absen_title;
            }
        }

        private void btnAbsen_MouseLeave(object sender, MouseEventArgs e)
        {
            Popup.Visibility = Visibility.Collapsed;
            Popup.IsOpen = false;
        }
        private void btnAbsen_Click(object sender, RoutedEventArgs e)
        {
            if (save.IsEnabled)
            {
                save.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
            }
            btnAction(Constants.absen_title, "Pages/Absen.xaml");
            absen = true;
        }
        private void btnSikap_MouseEnter(object sender, MouseEventArgs e)
        {
            if (Tg_Btn.IsChecked == false)
            {
                Popup.PlacementTarget = btnSikap;
                Popup.Placement = PlacementMode.Right;
                Popup.IsOpen = true;
                Header.PopupText.Text = Constants.sikap_title;
            }
        }

        private void btnSikap_MouseLeave(object sender, MouseEventArgs e)
        {
            Popup.Visibility = Visibility.Collapsed;
            Popup.IsOpen = false;
        }
        private void btnSikap_Click(object sender, RoutedEventArgs e)
        {
            if (save.IsEnabled)
            {
                save.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
            }
            btnAction(Constants.sikap_title, "Pages/Sikap.xaml");
            sikap = true;
        }
    }
}
