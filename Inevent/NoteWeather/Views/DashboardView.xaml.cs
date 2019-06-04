using Inevent.Elements;
using Inevent.Models;
using Inevent.ViewModels;
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
            LoadEvents();
            UserID = Properties.Settings.Default.id;
        }

        private async void LoadUserData()
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

        private async void LoadEvents()
        {
            Events events = new Events();
            try
            {
                Event[] upcomingEvents = await events.LoadEvents();
                this.Upcoming.ItemsSource = upcomingEvents;
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
                throw e;
            }
        }

        void JoinButton_click(object sender, EventArgs e)
        {
            MessageBox.Show((sender as Button).Tag.ToString());
        }

        void EventTile_click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            Properties.Settings.Default.currentEvent = Convert.ToInt32(btn.CommandParameter);
            Content = new EventInfoModel();
        }
    }
}
