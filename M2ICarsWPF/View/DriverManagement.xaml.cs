using System;
using System.Collections.Generic;
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

namespace M2ICarsWPF.View
{
    /// <summary>
    /// Logique d'interaction pour DriverManagement.xaml
    /// </summary>
    public partial class DriverManagement : Page
    {
        public DriverManagement()
        {
            InitializeComponent();
            
        }

        private void AddDriver_Click(object sender, RoutedEventArgs e)
        {
            AddDriver a = new AddDriver();
            a.Show();
        }

        private void Retour_Click(object sender, RoutedEventArgs e)
        {
            Home home = new Home();
            NavigationService.Navigate(home);
        }
    }
}
