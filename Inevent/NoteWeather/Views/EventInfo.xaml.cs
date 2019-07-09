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
    /// Interaction logic for EventInfo.xaml
    /// </summary>
    public partial class EventInfo : UserControl
    {
        private List<int> signedId = new List<int>();
        private bool signed = false;
        private int currentEventId;
        public EventInfo()
        {
            InitializeComponent();
            Load();
        }

        public bool CheckIfOwner(int ownerId)
        {
            return ownerId == Properties.Settings.Default.id;
        }

        public async void Load()
        {
            try
            {
                signedId = await Events.LoadSignedId(Properties.Settings.Default.id);
                Event[] current = await Events.LoadEvent(Properties.Settings.Default.currentEvent);
                if (current[0].Tags.Length <= 0)
                {
                    IfEmpty.Text = "Brak tagów przypisanych do tego wydarzenia.";
                }
                Info.ItemsSource = current;
                int[] ids = signedId.ToArray();
                if (ids.Contains(current[0].Id))
                {
                    signed = true;
                    SignedToggler.Content = "Opuść wydarzenie";
                }
                else
                {
                    signed = false;
                    SignedToggler.Content = "Weź udział";
                }
                if (CheckIfOwner(current[0].OwnerId))
                {
                    currentEventId = current[0].Id;
                    Button eventEditButton = new Button()
                    {
                        Content = "Edytuj wydarzenie",
                        Width = 120,
                        Margin = new Thickness(10, 0, 0, 0),
                        Style = (Style)FindResource("eventInfoButton")

                    };
                    Button eventDeleteButton = new Button()
                    {
                        Content = "Usuń wydarzenie",
                        Width = 120,
                        Margin = new Thickness(10, 0, 0, 0),
                        Style = (Style)FindResource("eventInfoButton")
                    };
                    eventEditButton.Click += eventEditButton_click;
                    eventDeleteButton.Click += eventDeleteButton_click;

                    ButtonsStackPanel.Children.Add(eventEditButton);
                    ButtonsStackPanel.Children.Add(eventDeleteButton);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }

        public void eventEditButton_click(object sender, EventArgs e)
        {
            Content = new EditEventModel();
        }
        public async void eventDeleteButton_click(object sender, EventArgs e)
        {
            bool success = await Events.DeleteEvent(currentEventId);
            if (success)
            {
                Content = new DashboardModel();
            }
            else
            {
                MessageBox.Show("Fail");
            }
        }


        public void DashboardButton_click(object sender, EventArgs e)
        {
            Content = new DashboardModel();
        }

        public async void SignedToggler_click(object sender, EventArgs e)
        {
            bool success;
            try
            {
                if (signed == true)
                {
                    success = await Events.RemoveMember(Properties.Settings.Default.currentEvent, Properties.Settings.Default.id);
                }
                else
                {
                    success = await Events.AddMember(Properties.Settings.Default.currentEvent, Properties.Settings.Default.id);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            Content = new DashboardModel();
        }
    }

    
}
