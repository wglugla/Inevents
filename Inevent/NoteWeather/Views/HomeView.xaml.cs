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
    /// Interaction logic for HomeView.xaml
    /// </summary>
    public partial class HomeView : UserControl
    {
        private Tag[] favourites;
        public HomeView()
        {
            InitializeComponent();
            LoadTags();
            DataContext = new DashboardModel();
        }

        public async void LoadTags()
        {
            try
            {
                Tag[] req = await Tags.GetAllTags();
                AllTags.ItemsSource = req;
                favourites = await Tags.GetUserTags(Properties.Settings.Default.id);
                if (favourites.Length > 0 )
                {
                    Favourites.ItemsSource = favourites;
                }
                else
                {
                    IfEmpty.Text = "Nie wybrałeś ulubionych tagów. Edytuj profil, aby to zrobić!";
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void Dashboard_click(object sender, EventArgs e)
        {
            Content = new HomeModel();
        }

        public void TagButton_click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            Properties.Settings.Default.currentTag = Convert.ToInt32(btn.Tag);
            DataContext = new EventsByTagModel();
            EventsByTagView x = new EventsByTagView();
            Control.DataContext = x;
            x.Refresh();
        }

        private void EditProfile_Click(object sender, RoutedEventArgs e)
        {
           DataContext = new EditProfileModel();
        }

        public void Refresh()
        {
            LoadTags();
        }

        private void CreateEvent_Click(object sender, RoutedEventArgs e)
        {
            DataContext = new EventCreatorModel();
        }

        private void Created_Click(object sender, RoutedEventArgs e)
        {
            DataContext = new CreatedEventsModel();
        }

        private void Signed_Click(object sender, RoutedEventArgs e)
        {
            DataContext = new SignedEventsModel();
        }
        private void Matched_Click(object sender, RoutedEventArgs e)
        {
            DataContext = new MatchedEventsModel();
        }
    }
}
