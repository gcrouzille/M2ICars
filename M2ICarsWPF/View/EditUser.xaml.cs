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
    /// Logique d'interaction pour EditUser.xaml
    /// </summary>
    public partial class EditUser : Window
    {
        public EditUser(User user)
        {
            InitializeComponent();
            
            (this.DataContext as AddUserViewModel).Initialize(user);
        }


        private void SaveUser(object sender, RoutedEventArgs e)
        {
           
            
                (this.DataContext as AddUserViewModel).SaveUser();
                this.Close();
            
           
        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
