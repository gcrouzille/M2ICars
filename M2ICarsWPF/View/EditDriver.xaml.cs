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
    /// Logique d'interaction pour EditDriver.xaml
    /// </summary>
    public partial class EditDriver : Window
    {
        
            public EditDriver(Driver driver)
            {
                InitializeComponent();
                (this.DataContext as AddDriverViewModel).Initialize(driver);
            }


            private void SaveDriver(object sender, RoutedEventArgs e)
            {


                (this.DataContext as AddDriverViewModel).SaveDriver();
                this.Close();


            }

            private void Cancel(object sender, RoutedEventArgs e)
            {
                this.Close();
            }
        }
    }

