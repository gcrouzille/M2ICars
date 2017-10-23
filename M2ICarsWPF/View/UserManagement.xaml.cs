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
    /// Logique d'interaction pour UserManagement.xaml
    /// </summary>
    public partial class UserManagement : Page
    {
        public UserManagement()
        {
            InitializeComponent();
        }

        private void AddUser(object sender, RoutedEventArgs e)
        {
            AddUser a = new AddUser();
            a.Show();
        }

        private void Retour_Click(object sender, RoutedEventArgs e)
        {
            Home home = new Home();
            NavigationService.Navigate(home);
        }

        private void EditUser(object sender, RoutedEventArgs e)
        {
            EditUser w = new EditUser(datagridClient.SelectedItem as User);
            w.Show();
        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            Home h = new Home();
            NavigationService.Navigate(h);
        }

        private void DeleteUser(object sender, RoutedEventArgs e)
        {
           
                MessageBoxResult result = MessageBox.Show("Etes vous sûr de vouloir supprimer cette réservation ?", "Confirmation", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
                if (result == MessageBoxResult.OK)
                {
                    (this.DataContext as UserViewModel).DeleteUser((User)datagridClient.SelectedItem);
                }
            }
        
    }
}
