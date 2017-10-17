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

namespace M2ICarsWPF
{
    /// <summary>
    /// Logique d'interaction pour AddDriver.xaml
    /// </summary>
    public partial class AddDriver : Window
    {
        public AddDriver()
        {
            InitializeComponent();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        public AddDriver(Window FenetrePrincipale) : this()
        {
            (DataContext as AddDriverViewModel).Wp = FenetrePrincipale;
        }
    }
}
