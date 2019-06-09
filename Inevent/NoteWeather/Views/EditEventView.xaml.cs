using Inevent.Models;
using Inevent.ViewModels;
using Newtonsoft.Json;
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
    /// Interaction logic for EditEventView.xaml
    /// </summary>
    public partial class EditEventView : UserControl
    {
        private Event[] currentEvent;
        private Tag[] tags;

        private int id;
        private int ownerId;
        private string title;
        private string place;
        private string eventDate;
        private string description;

        public EditEventView()
        {
            InitializeComponent();
            LoadTags();
            LoadData();
        }

        public async void LoadTags()
        {
            tags = await Tags.GetAllTags();
        }

        private async void LoadData()
        {
            try
            {
                currentEvent = await Events.LoadEvent(Properties.Settings.Default.currentEvent);
                TitleBox.Text = currentEvent[0].Title;
                PlaceBox.Text = currentEvent[0].Place;
                DateBox.Value = currentEvent[0].Date;
                DescriptionBox.Text = currentEvent[0].Description;
                foreach(string tagName in currentEvent[0].Tags)
                {
                    Tag newTag = tags.Where(p => p.Value == tagName).FirstOrDefault();
                    newTag.IsChecked = true;
                }

                TagList.ItemsSource = tags;
            }
            catch (Exception)
            {
                MessageBox.Show("Fail!");
            }
        }

        private async void ModifyEvent_click(object sender, RoutedEventArgs e)
        {
            id = currentEvent[0].Id;
            ownerId = Properties.Settings.Default.id;
            title = TitleBox.Text;
            place = PlaceBox.Text;
            DateTime? selectedDate = DateBox.Value;
            eventDate = selectedDate.Value.ToString("yyyy-MM-dd HH:mm", System.Globalization.CultureInfo.InvariantCulture);
            description = DescriptionBox.Text;
            try
            {
                await Events.UpdateEvent(id, ownerId, title, place, eventDate, description);
                int[] list = tags.Where(p => p.IsChecked == true).Select(p => p.Id).ToArray();
                await Events.ChangeEventTags(id, list);
            }
            catch (Exception ev)
            {
                MessageBox.Show(ev.Message);
            }
            Content = new DashboardModel();
        }
    }
}
