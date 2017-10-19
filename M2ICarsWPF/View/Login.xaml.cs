using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
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
    /// Logique d'interaction pour Page1.xaml
    /// </summary>
    public partial class Page1 : Page
    {
        public Page1()
        {
            InitializeComponent();
        }

        public async void Login(object sender, RoutedEventArgs e)
        {
            bool loggedIn = await APIService.Instance.GetToken(username.Text,password.Password);
            
            if (loggedIn)
            {                
                Page2 nextPage = new Page2();
                NavigationService.Navigate(nextPage);
            }
        }
    }
}
