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
    /// Interaction logic for EventsByTagView.xaml
    /// </summary>
    public partial class EventsByTagView : UserControl
    {
        public EventsByTagView()
        {
            InitializeComponent();
            LoadEvents();
        }

        public async void LoadEvents()
        {
            Event[] events = await Events.FindEventByTag(Properties.Settings.Default.currentTag);
            foreach (Event ev in events)
            {
                ev.FormatedDate = DateTime.Now.ToString("dddd, dd MMMM yyyy");
                ev.FormatedDay = DateTime.Now.ToString("dd");
                ev.FormatedDayName = DateTime.Now.ToString("dddd").Substring(0, 3).ToUpper();

            }
            EventsList.ItemsSource = events;
        }

        void JoinButton_click(object sender, EventArgs e)
        {
            Properties.Settings.Default.currentEvent = Convert.ToInt32((sender as Button).Tag);
            Content = new EventInfoModel();
        }

        public void Refresh()
        {
            LoadEvents();
        }
    }
}
