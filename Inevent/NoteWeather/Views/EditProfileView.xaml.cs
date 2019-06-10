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
    /// Interaction logic for EditProfileView.xaml
    /// </summary>
    public partial class EditProfileView : UserControl
    {
        private Tag[] tags;
        public EditProfileView()
        {
            InitializeComponent();
            LoadTags();
        }

        public async void LoadTags()
        {
            tags = await Tags.GetAllTags();
            Tag[] usertags = await Tags.GetUserTags(Properties.Settings.Default.id);
            foreach(Tag tag in tags)
            {
                if (Array.Exists(usertags, element => element.Id == tag.Id))
                {
                    tag.IsChecked = true;
                }
                else
                {
                    tag.IsChecked = false;
                }
            }
            TagList.ItemsSource = tags;
        }



        private async void Submit_Click(object sender, RoutedEventArgs e)
        {
            int[] list = tags.Where(p => p.IsChecked == true).Select(p => p.Id).ToArray();
            await Users.ChangeUserTags(Properties.Settings.Default.id, list);
            Application.Current.MainWindow.Content = new HomeView();
        }
    }
}
