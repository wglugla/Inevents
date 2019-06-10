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
    /// Interaction logic for CreatedEventsView.xaml
    /// </summary>
    public partial class CreatedEventsView : UserControl
    {
        public Event[] signedEvents { get; set; }
        public CreatedEventsView()
        {
            InitializeComponent();
            LoadEvents();
        }

        private async void LoadEvents()
        {
            try
            {
                signedEvents = await Users.GetCreatedEvents(Properties.Settings.Default.id);
                foreach (Event ev in signedEvents)
                {
                    ev.FormatedDate = ev.Date.ToString("dddd, dd MMMM yyyy HH:mm");
                    ev.FormatedDay = ev.Date.ToString("dd");
                    ev.FormatedDayName = ev.Date.ToString("dddd").Substring(0, 3).ToUpper();
                    TimeSpan t = ev.Date - DateTime.Now;
                    if (ev.Date > DateTime.Now)
                    {
                        ev.Countdown = string.Format("{0} dni, {1} godzin, {2} minut, {3} sekund", t.Days, t.Hours, t.Minutes, t.Seconds);
                    }
                    else
                    {
                        ev.Countdown = "Wydarzenie odbyło się";

                    }
                }
                if (signedEvents.Length > 0)
                {
                    Created.ItemsSource = signedEvents;
                }
                else
                {
                    IfEmpty.Text = "Nie utworzyłeś żadnego wydarzenia";
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private void DetailsButton_click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("DETAILS");
        }
    }
}
