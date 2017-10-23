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
    /// Logique d'interaction pour Home.xaml
    /// </summary>
    public partial class Home : Page
    {
        public Home()
        {
            InitializeComponent();
        }

        private void ManageReservation(object sender, RoutedEventArgs e)
        {            
            ReservationManagement nextPage = new ReservationManagement();
            NavigationService.Navigate(nextPage);
        }

        private void ManageUser(object sender, RoutedEventArgs e)
        {
            UserManagement nextPage = new UserManagement();
            NavigationService.Navigate(nextPage);
        }

        private void ManageDriver(object sender, RoutedEventArgs e)
        {
            DriverManagement nextPage = new DriverManagement();
            NavigationService.Navigate(nextPage);
        }
    }
}
