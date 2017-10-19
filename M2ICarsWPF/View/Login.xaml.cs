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
    public partial class Login : Page
    {
        public Login()
        {
            InitializeComponent();
        }

        public async void LoginRequest(object sender, RoutedEventArgs e)
        {
            bool loggedIn = await APIService.Instance.GetToken(username.Text,password.Password);
            
            if (loggedIn)
            {
                Manager.Instance.Initialize();
                Home nextPage = new Home();
                NavigationService.Navigate(nextPage);
            }
        }
    }
}
