using Inevent.Models;
using Inevent.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
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
    /// Interaction logic for SignedEventsView.xaml
    /// </summary>
    public partial class SignedEventsView : UserControl
    {
        public Event[] signedEvents { get; private set; }
        public ObservableCollection<Event> observableEvents { get; private set; }

        public SignedEventsView()
        {
            InitializeComponent();
            LoadEvents();
        }

        private delegate void DataFormatter (Event ev);

        private void FormatDate(Event ev)
        {
            ev.FormatedDate = ev.Date.ToString("dddd, dd MMMM yyyy HH:mm");
        } 

        private void FormatDay(Event ev)
        {
            ev.FormatedDay = ev.Date.ToString("dd");
        }

        private void FormatDayName(Event ev)
        {
            ev.FormatedDayName = ev.Date.ToString("dddd").Substring(0, 3).ToUpper();
        }

        private async void LoadEvents()
        {
            try
            {
                signedEvents = await Events.LoadSigned(Properties.Settings.Default.id);
                DataFormatter formatDate = new DataFormatter(FormatDate);
                formatDate += FormatDay;
                formatDate += FormatDayName;
                foreach (Event ev in signedEvents)
                {
                    formatDate(ev);
                    TimeSpan t = ev.Date - DateTime.Now;
                    if (ev.Date > DateTime.Now)
                    {
                        ev.Countdown = string.Format("{0} dni, {1} godzin, {2} minut, {3} sekund", t.Days, t.Hours, t.Minutes, t.Seconds);
                    }
                    else
                    {
                        signedEvents = signedEvents.Where(val => val.Id != ev.Id).ToArray();

                    }
                }
                if (signedEvents.Length > 0)
                {
                    observableEvents = new ObservableCollection<Event>(signedEvents.OrderBy(p => p.Date));
                    Signed.ItemsSource = observableEvents;
                    Thread timeUpdater = new Thread(() =>
                    {
                        CancellationToken token = new CancellationToken();
                        for (; ; )
                        {
                            if (!token.IsCancellationRequested)
                            {
                                foreach (Event ev in observableEvents)
                                {
                                    TimeSpan t = ev.Date - DateTime.Now;
                                    if (ev.Date > DateTime.Now)
                                    {
                                        ev.Countdown = string.Format("{0} dni, {1} godzin, {2} minut, {3} sekund", t.Days, t.Hours, t.Minutes, t.Seconds);
                                    }
                                    else
                                    {
                                        ev.Countdown = "Wydarzenie odbyło się.";
                                    }
                                }
                                Task.Delay(1000, token).Wait(); // use await for async method

                            }
                            else
                            {
                                break; // end
                            }
                        }
                    });
                    timeUpdater.IsBackground = true;
                    timeUpdater.Start();
                }
                else
                {
                    IfEmpty.Text = "Nie bierzesz udziału w żadnym wydarzeniu";
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        void DetailsButton_click(object sender, EventArgs e)
        {
            Properties.Settings.Default.currentEvent = Convert.ToInt32((sender as Button).Tag);
            Content = new EventInfoModel();
        }
    }
}
