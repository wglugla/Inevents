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
    /// Interaction logic for EditProfileView.xaml
    /// </summary>
    public partial class EditProfileView : UserControl
    {
        public EditProfileView()
        {
            InitializeComponent();
            LoadTags();
        }

        public async void LoadTags()
        {
            Tag[] tags = await Tags.GetAllTags();
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
    }
}
