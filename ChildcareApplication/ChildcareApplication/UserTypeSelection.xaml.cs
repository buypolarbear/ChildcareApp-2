﻿using System;
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
using AdminTools;
using GuardianTools;

namespace ChildcareApplication {
    /// <summary>
    /// Interaction logic for UserSelection.xaml
    /// </summary>
    public partial class UserSelection : Window {
        public UserSelection() {
            InitializeComponent();
            this.MouseDown += WindowMouseDown;
        }

        private void btn_ParentUse_Click(object sender, RoutedEventArgs e) {
            GuardianCheckIn parentLogin = new GuardianCheckIn();
            parentLogin.Show();
            this.Close();
        }

        private void btn_AdminLogin_Click(object sender, RoutedEventArgs e) {
            AdminLogin adminLogin = new AdminLogin();
            adminLogin.Show();
            this.Close();
        }

        private void btn_Exit_Click(object sender, RoutedEventArgs e) {
            Application.Current.Shutdown();
        }

        private void WindowMouseDown(object sender, MouseButtonEventArgs e){
            if (e.ChangedButton == MouseButton.Left)
                DragMove();
        }
    }
}
