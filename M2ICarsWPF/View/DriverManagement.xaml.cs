using M2ICarsWPF.ViewModel;
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

        private void DeleteDriver(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Etes vous sûr de vouloir supprimer cette réservation ?", "Confirmation", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
            if (result == MessageBoxResult.OK)
            {
                (this.DataContext as DriverViewModel).DeleteDriver((Driver)DataDriver.SelectedItem);
            }
        }

        private void EditDriver(object sender, RoutedEventArgs e)
        {
            EditDriver w = new EditDriver(DataDriver.SelectedItem as Driver);
            w.Show();
        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            Home h = new Home();
            NavigationService.Navigate(h);
        }

        private void AddDriver(object sender, RoutedEventArgs e)
        {
            AddDriver w = new AddDriver();
            w.Show();
        }
    }
}
