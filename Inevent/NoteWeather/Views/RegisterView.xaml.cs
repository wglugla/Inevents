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
using APIConnect;
using Inevent.ViewModels;

namespace Inevent.Views
{
    /// <summary>
    /// Interaction logic for RegisterView.xaml
    /// </summary>
    public partial class RegisterView : UserControl
    {
        public RegisterView()
        {
            InitializeComponent();
        }

        private async void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameBox.Text;
            string name = NameBox.Text;
            string surname = SurnameBox.Text;
            string password = PasswordBox.Password.ToString();

            Login l = new Login();
            try
            {
                bool result = await l.RegisterUser(username, name, surname, password);
                if (result)
                {
                    bool success = await l.LoginUser(username, password);
                    if (success == true)
                    {
                        ApiHelper.AddTokenHeader(Properties.Settings.Default.accessToken);
                        Content = new HomeModel();
                    }
                    else
                    {
                        MessageBox.Show("Nie udało się zarejestrować!");
                    }
                }
                else
                {
                    MessageBox.Show(result.ToString());
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
