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
using System.Windows.Shapes;

namespace M2ICarsWPF
{
    /// <summary>
    /// Logique d'interaction pour AddUser.xaml
    /// </summary>
    public partial class AddUser 
    {
        public AddUser()
        {
            InitializeComponent();
        }

        private void Cancel (object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void SaveUser(object sender, RoutedEventArgs e)
        {
            (this.DataContext as AddUserViewModel).AddNewUser();
            this.Close();
        }
    }
}
