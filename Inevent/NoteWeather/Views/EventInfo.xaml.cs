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
        public EventInfo()
        {
            InitializeComponent();
            Load();
        }

        public async void Load()
        {
            try
            {

            signedId = await Events.LoadSignedId(Properties.Settings.Default.id);
            Event[] current = await Events.LoadEvent(Properties.Settings.Default.currentEvent);
            Info.ItemsSource = current;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }

        void DashboardButton_click(object sender, EventArgs e)
        {
            Content = new DashboardModel();
        }
    }

    
}
