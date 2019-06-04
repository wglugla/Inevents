using Inevent.Models;
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

namespace Inevent.Views
{
    /// <summary>
    /// Interaction logic for DashboardView.xaml
    /// </summary>
    public partial class DashboardView : UserControl
    {
        public int UserID { get; set; }
        public DashboardView()
        {
            InitializeComponent();
            LoadUserData();
            UserID = Properties.Settings.Default.id;
        }

        private async Task LoadUserData()
        {
            Users user = new Users();
            try
            {
                User req = new User();
                req = await user.LoadUser();
                Username.Content = "Hello, " + req.Username + "!";
            }
            catch (Exception e)
            {
                Username.Content = "Error" + e;
            }
        }
    }
}
