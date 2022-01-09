using Raport.Helper;
using Raport.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
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
using System.IO;

namespace Raport.Pages
{
    /// <summary>
    /// Interaction logic for Laporan.xaml
    /// </summary>
    public partial class Laporan : Page
    {
        string[] mapel =
            {
            "Agama","Pendidikan Kewarganegaraan","Bahasa Indonesia","Matematika","Ilmu Pengetahuan Alam",
            "Ilmu Pengetahuan Sosial","Seni Budaya dan Pengetahuan", "Pendidikan Jasmani, Olahraga dan Kesehatan",
            "Bahasa Banjar","Bahasa Inggris","Baca Tulis Al-Qur'an"
            };
        public Laporan()
        {
            InitializeComponent();
            AddTable();
        }
        private void DataGrid_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = (e.Row.GetIndex() + 1).ToString();
        }
        private void AddTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Mata Pelajaran", typeof(string));
            for (int i = 0; i < mapel.Length; i++)
            {
                dt.Rows.Add(mapel[i]);
            }
            dg_home.ItemsSource = dt.DefaultView;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DataGrid datagrid = ((Button)sender).CommandParameter as DataGrid;
            var selectedIndex = datagrid.SelectedIndex;
            switch (selectedIndex) {
                case 0:
                    Report3("kd_agm3", Constants.agm_title, Constants.agm_title, Database.kkm_agm, Database.kd_agm3);
                    break;
                case 1:
                    Report3("kd_pkn3", Constants.pkn_title, Constants.pkn_title, Database.kkm_pkn, Database.kd_pkn3);
                    break;
                case 2:
                    Report3("kd_bi3", Constants.bi_title, Constants.bi_title, Database.kkm_bi, Database.kd_bi3);
                    break;
                case 3:
                    Report3("kd_mtk3", Constants.mtk_title, Constants.mtk_title, Database.kkm_mtk, Database.kd_mtk3);
                    break;
                case 4:
                    Report3("kd_ipa3", Constants.ipa_title, Constants.ipa_title, Database.kkm_ipa, Database.kd_ipa3);
                    break;
                case 5:
                    Report3("kd_ips3", Constants.ips_title, Constants.ips_title, Database.kkm_ips, Database.kd_ips3);
                    break;
                case 6:
                    Report3("kd_sbdp3", Constants.sbdp_title, Constants.sbdp_title, Database.kkm_sbdp, Database.kd_sbdp3);
                    break;
                case 7:
                    Report3("kd_pjok3", Constants.pjok_title, Constants.pjok_title, Database.kkm_pjok, Database.kd_pjok3);
                    break;
                case 8:
                    Report3("kd_bjr3", Constants.bjr_title, Constants.bjr_title, Database.kkm_bjr, Database.kd_bjr3);
                    break;
                case 9:
                    Report3("kd_bing3", Constants.bing_title, Constants.bing_title, Database.kkm_bing, Database.kd_bing3);
                    break;
                case 10:
                    Report3("kd_bta3", Constants.bta_title, Constants.bta_title, Database.kkm_bta, Database.kd_bta3);
                    break;
            }
        }

        private void Report3(string kd,string nilai,string title,double kkm,int bagi)
        {
            Modal.Spinner(progress =>
            {
                progress.Report("Loading");
                ReportKD3.CreateReport(Connection.dataset.Tables[kd], Connection.dataset.Tables[nilai], title, Database.semester, Database.kelas, Database.tahun, Database.wali_kelas, kkm, bagi);
            });
        }
        private void Report4(string kd, string nilai, string title, double kkm,int bagi)
        {
            Modal.Spinner(progress =>
            {
                progress.Report("Loading");
                ReportKD4.CreateReport(Connection.dataset.Tables[kd], Connection.dataset.Tables[nilai], title, Database.semester, Database.kelas, Database.tahun, Database.wali_kelas, kkm, bagi);
            });
        }
        private void Button_Click2(object sender, RoutedEventArgs e)
        {
            DataGrid datagrid = ((Button)sender).CommandParameter as DataGrid;
            var selectedIndex = datagrid.SelectedIndex;
            switch (selectedIndex)
            {
                case 0:
                    Report4("kd_agm4", Constants.agm_title2, Constants.agm_title, Database.kkm_agm, Database.kd_agm4);
                    break;
                case 1:
                    Report4("kd_pkn4", Constants.pkn_title2, Constants.pkn_title, Database.kkm_pkn, Database.kd_pkn4);
                    break;
                case 2:
                    Report4("kd_bi4", Constants.bi_title2, Constants.bi_title, Database.kkm_bi, Database.kd_bi4);
                    break;
                case 3:
                    Report4("kd_mtk4", Constants.mtk_title2, Constants.mtk_title, Database.kkm_mtk, Database.kd_mtk4);
                    break;
                case 4:
                    Report4("kd_ipa4", Constants.ipa_title2, Constants.ipa_title, Database.kkm_ipa, Database.kd_ipa4);
                    break;
                case 5:
                    Report4("kd_ips4", Constants.ips_title2, Constants.ips_title, Database.kkm_ips, Database.kd_ips4);
                    break;
                case 6:
                    Report4("kd_sbdp4", Constants.sbdp_title2, Constants.sbdp_title, Database.kkm_sbdp, Database.kd_sbdp4);
                    break;
                case 7:
                    Report4("kd_pjok4", Constants.pjok_title2, Constants.pjok_title, Database.kkm_pjok, Database.kd_pjok4);
                    break;
                case 8:
                    Report4("kd_bjr4", Constants.bjr_title2, Constants.bjr_title, Database.kkm_bjr, Database.kd_bjr4);
                    break;
                case 9:
                    Report4("kd_bing4", Constants.bing_title2, Constants.bing_title, Database.kkm_bing, Database.kd_bing4);
                    break;
                case 10:
                    Report4("kd_bta4", Constants.bta_title2, Constants.bta_title, Database.kkm_bta, Database.kd_bta4);
                    break;
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Modal.Spinner(progress =>
            {
                progress.Report("Loading");
                Report.CreateDasis(Connection.dataset.Tables[Constants.dasis_title]);
            });
            
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            try
            {
                Modal.Spinner(progress =>
                {
                    progress.Report("Loading");
                    NilaiRaport.print();
                    progress.Report("Loading");
                    string strDataFilename = Environment.CurrentDirectory + @"\Templates\TempData.csv";
                    Object strWordFilename = Environment.CurrentDirectory + @"\Templates\Raport.docx";
                    Microsoft.Office.Interop.Word.Application _wordApp = new Microsoft.Office.Interop.Word.Application();
                    Microsoft.Office.Interop.Word.Document oDoc = _wordApp.Documents.Add(strWordFilename);
                    progress.Report("Loading");
                    _wordApp.Visible = true;
                    oDoc.MailMerge.MainDocumentType = Microsoft.Office.Interop.Word.WdMailMergeMainDocType.wdFormLetters;
                    oDoc.MailMerge.OpenDataSource(strDataFilename);                    
                    oDoc.MailMerge.Execute(true);                    
                });
            }catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            
        }
        private void Button_rekap3(object sender, RoutedEventArgs e)
        {
            try
            {
                Modal.Spinner(progress =>
                {
                    progress.Report("Loading");
                    RekapNilai.print(true);
                    
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }
        private void Button_rekap4(object sender, RoutedEventArgs e)
        {
            try
            {
                Modal.Spinner(progress =>
                {
                    progress.Report("Loading");
                    RekapNilai.print(false);

                });
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            Modal.Spinner(progress =>
            {
                progress.Report("Loading");
                ReportAbsen.CreateReport();
            });
        }
        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            Modal.Spinner(progress =>
            {
                progress.Report("Loading");
                ReportSikap.CreateReport();
            });
        }
        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            Modal.Spinner(progress =>
            {
                progress.Report("Loading");
                ReportCover.CreateReport();
                progress.Report("Loading");
                string strDataFilename = Environment.CurrentDirectory + @"\Templates\TempDataCover.csv";
                Object strWordFilename = Environment.CurrentDirectory + @"\Templates\RaportCover.docx";
                Microsoft.Office.Interop.Word.Application _wordApp = new Microsoft.Office.Interop.Word.Application();
                Microsoft.Office.Interop.Word.Document oDoc = _wordApp.Documents.Add(strWordFilename);
                progress.Report("Loading");
                _wordApp.Visible = true;
                oDoc.MailMerge.MainDocumentType = Microsoft.Office.Interop.Word.WdMailMergeMainDocType.wdFormLetters;
                oDoc.MailMerge.OpenDataSource(strDataFilename);
                oDoc.MailMerge.Execute(true);
            });
        }
    }
}
