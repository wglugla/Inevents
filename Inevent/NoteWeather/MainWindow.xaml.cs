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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Net.Http;
using System.Net;
using System.IO;
using APIConnect;

namespace Inevent
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        public string Login { private get; set; }
        public string Password { private get; set; }

        public MainWindow()
        {
            InitializeComponent();
            ApiHelper.InitializeClient();
            ShowError("TEST");
            // ShowError(Properties.Settings.Default.accessToken);
        }



        private bool ValidateLogin()
        {
            if (Login.Length > 0)
            {
                return true;
            }
            return false;
        }

        private bool ValidatePassword()
        {
            if (Password.Length > 0 )
            {
                return true;
            }
            return false;
        }

        private void ShowError(string message)
        {
            loginErrorMessagebox.Text = message;
        }

        private void ClearError()
        {
            loginErrorMessagebox.Text = String.Empty;
        }

        private async void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            Login = loginBox.Text;
            Password = passwordBox.Text;

            if (!ValidateLogin())
            {
                ShowError("Wprowadź login");
            }
            else if (!ValidatePassword())
            {
                ShowError("Wprowadź hasło!");
            }
            else
            {
                ClearError();
                // Users users = new Users();
                // object result = await users.LoadUsers();
                // ShowError(result.ToString());
                Login login = new Login();
                if (await login.LoginUser(Login, Password))
                {
                    ShowError(Properties.Settings.Default.accessToken);
                }
                else
                {
                    ShowError("Nie udało się zalogować!");
                }
                
            }
        }
    }
}