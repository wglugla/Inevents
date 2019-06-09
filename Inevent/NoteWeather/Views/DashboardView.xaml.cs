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
        public Event[] signedEvents { get; set; }
        public DashboardView()
        {
            InitializeComponent();
            LoadEvents();
            UserID = Properties.Settings.Default.id;
        }

        
        private bool ifSigned(int a, int[] b)
        {
            for (int i=0; i<b.Length; i++)
            {
                if (b[i] == a) return true;
            }
            return false;
        }


        private async void LoadEvents()
        {
            try
            {
                signedEvents = await Events.LoadSigned(Properties.Settings.Default.id);
                Event[] upcomingEvents = await Events.LoadEvents();
                int[] signedIds = signedEvents.Select(p => p.Id).ToArray();
                foreach (Event ev in upcomingEvents)
                {
                    ev.FormatedDate = ev.Date.ToString("dddd, dd MMMM yyyy");
                    ev.FormatedDay = ev.Date.ToString("dd");
                    ev.FormatedDayName = ev.Date.ToString("dddd").Substring(0, 3).ToUpper();

                }
                Upcoming.ItemsSource = upcomingEvents;
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        void JoinButton_click(object sender, EventArgs e)
        {
            Properties.Settings.Default.currentEvent = Convert.ToInt32((sender as Button).Tag);
            Content = new EventInfoModel();
        }

        void EventTile_click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            Properties.Settings.Default.currentEvent = Convert.ToInt32(btn.CommandParameter);
            Content = new EventInfoModel();
        }

        void DashboardButton_click(object sender, EventArgs e)
        {
            Content = new DashboardModel();
        }

        void Test_click(object sender, EventArgs e)
        {
            MessageBox.Show("JEST");
        }
    }
}
