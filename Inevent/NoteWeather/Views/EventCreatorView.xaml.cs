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
    /// Interaction logic for EventCreatorView.xaml
    /// </summary>
    public partial class EventCreatorView : UserControl
    {
        private int ownerId;
        private string title;
        private string place;
        private string eventDate;
        private string description;

        private Tag[] tags;

        public EventCreatorView()
        {
            InitializeComponent();
            LoadTags();
        }

        public async void LoadTags()
        {
            tags = await Tags.GetAllTags();
            TagList.ItemsSource = tags;
        }

        public async void AddTagsToEvent(int eventId, int[] tagsIds)
        {
            bool result = await Events.ChangeEventTags(eventId, tagsIds);
        }


        private async void CreateEvent_click(object sender, RoutedEventArgs e)
        {
            ownerId = Properties.Settings.Default.id;
            title = TitleBox.Text;
            place = PlaceBox.Text;
            DateTime? selectedDate = DateBox.Value;
            eventDate = selectedDate.Value.ToString("yyyy-MM-dd HH:mm", System.Globalization.CultureInfo.InvariantCulture);
            description = DescriptionBox.Text;
            int newEventId = 0;
            try
            {
                newEventId = await Events.CreateEvent(ownerId, title, place, eventDate, description);
                if (newEventId != 0)
                {
                    int[] list = tags.Where(p => p.IsChecked == true).Select(p => p.Id).ToArray();
                    AddTagsToEvent(newEventId, list);
                    Application.Current.MainWindow.Content = new HomeView();
                }
                else
                {
                    MessageBox.Show("FAIL!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }


        }
    }
}
