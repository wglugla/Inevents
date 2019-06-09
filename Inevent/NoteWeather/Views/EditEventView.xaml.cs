using Inevent.Models;
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
                MessageBox.Show(currentEvent[0].Date.ToString());
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

        private void ModifyEvent_click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("MODIFY");
        }
    }
}
