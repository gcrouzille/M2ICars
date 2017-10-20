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
using System.Windows.Shapes;

namespace M2ICarsWPF.View
{
    /// <summary>
    /// Logique d'interaction pour EditReservation.xaml
    /// </summary>
    public partial class EditReservation : Window
    {
        public EditReservation(Reservation r)
        {           
            InitializeComponent();
            StatutCB.ItemsSource = Enum.GetValues(typeof(Reservation.statut));
            ClientCB.ItemsSource = Manager.Instance.Users;
            DriverCB.ItemsSource = Manager.Instance.Drivers;
            (this.DataContext as AddReservationViewModel).Initialize(r);
        }

        private void SaveReservation(object sender, RoutedEventArgs e)
        {
            if ((this.DataContext as AddReservationViewModel).CheckForm())
            {
                (this.DataContext as AddReservationViewModel).SaveReservation();
                this.Close();
            }
            else
                MessageBox.Show("Vous devez remplir tous les champs");
        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
