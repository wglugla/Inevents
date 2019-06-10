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
                TimeSpan t = ev.Date - DateTime.Now;
                if (ev.Date > DateTime.Now)
                {
                    ev.Countdown = string.Format("{0} dni, {1} godzin, {2} minut, {3} sekund", t.Days, t.Hours, t.Minutes, t.Seconds);
                }
                else
                {
                    events = events.Where(val => val.Id != ev.Id).ToArray();
                    ev.Countdown = "Wydarzenie odbyło się.";
                }
            }
            if (events.Length > 0)
            {
                EventsList.ItemsSource = events;
            }
            else
            {
                IfEmpty.Text = "Brak nadchodzących wydarzeń";
            }
            
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
