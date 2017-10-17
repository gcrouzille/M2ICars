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

namespace M2ICarsWPF
{
    /// <summary>
    /// Logique d'interaction pour Accueil.xaml
    /// </summary>
    public partial class Accueil : Window
    {
        public Accueil()
        {
            InitializeComponent();
        }

        private void User_Click(object sender, RoutedEventArgs e)
        {
            User f = new User();
            f.Show();
        }
        private void Driver_Click(object sender, RoutedEventArgs e)
        {
            DriverWindow f = new DriverWindow();
            f.Show();
            
            
        }
        private void Reservation_Click(object sender, RoutedEventArgs e)
        {
            Reservation f = new Reservation();
            f.Show();
        }
        private void Tarif_Click(object sender, RoutedEventArgs e)
        {
            Tarif f = new Tarif();
            f.Show();
        }

    }
}
