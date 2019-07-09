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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Net.Http;
using System.Net;
using System.IO;
using APIConnect;
using Inevent.ViewModels;

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
            ErrorPopup.IsOpen = false;
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

        /// <summary>
        /// Metoda otwierajca popup w przypadku błędu logowania
        /// </summary>
        /// <param name="message"> Treść błędu </param>
        private void ShowError(string message)
        {
            ErrorPopup.IsOpen = true;
            ErrorPopupContent.Text = message;
        }
        
        /// <summary>
        /// Metoda zamykająca popup
        /// </summary>
        private void ClearError()
        {
            loginErrorMessagebox.Text = String.Empty;
        }

        private async void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            Login = loginBox.Text;
            Password = passwordBox.Password.ToString();
            
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
                Login login = new Login();
                bool success = await login.LoginUser(Login, Password);
                if (success == true)
                {
                    ApiHelper.AddTokenHeader(Properties.Settings.Default.accessToken);
                    Content = new HomeModel();
                }
                else
                {
                    ShowError("Nie udało się zalogować!");
                }
                
            }
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            Content = new RegisterModel();
        }

        private void ErrorPopupCloseButton_Click(object sender, RoutedEventArgs e)
        {
            ErrorPopup.IsOpen = false;
        }
    }
}
