﻿using M2ICarsWPF.ViewModel;
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
    /// Logique d'interaction pour Page2.xaml
    /// </summary>
    public partial class ReservationManagement : Page
    {
        public ReservationManagement()
        {
            InitializeComponent();
            
        }

        private void DataGrid_RowDetailsVisibilityChanged(object sender, DataGridRowDetailsEventArgs e)
        {
            if (e.Row.DetailsVisibility == Visibility.Visible)
            { 
                User user = Manager.Instance.Users.First(u => u.UserId == (e.Row.Item as Reservation).ClientId);
                Driver driver = Manager.Instance.Drivers.First(d => d.DriverId == (e.Row.Item as Reservation).ReservationDriverId);
                (this.DataContext as ReservationViewModel).SetClient(user);
                (this.DataContext as ReservationViewModel).SetDriver(driver);
            }
        }

        private void DeleteReservation(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Etes vous sûr de vouloir supprimer cette réservation ?", "Confirmation", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
            if (result == MessageBoxResult.OK)
            {
                (this.DataContext as ReservationViewModel).DeleteReservation((Reservation)myDataGrid.SelectedItem);
            }
        }

        private void EditReservation(object sender, RoutedEventArgs e)
        {
            EditReservation w = new EditReservation(myDataGrid.SelectedItem as Reservation);
            w.Show();
        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            Home h = new Home();
            NavigationService.Navigate(h);
        }

        private void AddReservation(object sender, RoutedEventArgs e)
        {
            AddReservation w = new AddReservation();
            w.Show();
        }
    }
}
