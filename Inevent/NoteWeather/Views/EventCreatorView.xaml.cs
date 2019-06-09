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

        public EventCreatorView()
        {
            InitializeComponent();
        }

        private async void CreateEvent_click(object sender, RoutedEventArgs e)
        {
            ownerId = Properties.Settings.Default.id;
            title = TitleBox.Text;
            place = PlaceBox.Text;
            DateTime? selectedDate = DateBox.Value;
            eventDate = selectedDate.Value.ToString("yyyy-MM-dd HH:mm", System.Globalization.CultureInfo.InvariantCulture);
            description = DescriptionBox.Text;
<<<<<<< HEAD
            int newEventId = 0;
            try
            {
                newEventId = await Events.CreateEvent(ownerId, title, place, eventDate, description);
                if (newEventId != 0)
                {


=======
            bool success = false;
            try
            {
                success = await Events.CreateEvent(ownerId, title, place, eventDate, description);
                if (success == true)
                {
>>>>>>> 36d3bc9c2767e2630d25128d85115978ba198327
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
