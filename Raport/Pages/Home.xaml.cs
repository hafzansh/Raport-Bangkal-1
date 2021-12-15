using Raport.Helper;
using Raport.Services;
using System.Windows;
using System.Windows.Controls;

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
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Report.CreateDasis(Connection.dataset.Tables[Constants.dasis_title]);
        }
    }
}
