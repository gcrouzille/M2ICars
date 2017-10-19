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
using M2ICarsWPF.ViewModel;
using M2ICarsWPF.View;

namespace M2ICarsWPF
{
    /// <summary>
    /// Logique d'interaction pour Welcome.xaml
    /// </summary>
    public partial class Welcome : Window
    {
        public Welcome()
        {
            InitializeComponent();
        }

        private void AddUser_Click(object sender, RoutedEventArgs e)
        {
            AddUser a = new AddUser();
            a.Show();
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            User user = new User();
            string text = filtre.Text;
            Details details = new Details();
            

        }
            

        private void Detail_Click(object sender, RoutedEventArgs e)
        {

        }

       
    }
}
