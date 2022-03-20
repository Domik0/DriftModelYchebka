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
using Drift;

namespace DriftModelYchebka
{
    /// <summary>
    /// Interaction logic for AnalyticsPilot.xaml
    /// </summary>
    public partial class AnalyticsPilot : Page
    {
        public AnalyticsPilot()
        {
            InitializeComponent();
        }

        private void PageLoaded(object sender, RoutedEventArgs e)
        {
            PilotListView.ItemsSource = null;
            PilotListView.ItemsSource = App.dbContext.pilots.Local.ToList();
        }

        private void PilotListViewSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (PilotListView.SelectedItem != null)
            {
                Pilot p = PilotListView.SelectedItem as Pilot;
                NavigationService.Navigate(new PilotInfo(p));
                PilotListView.SelectedItem = null;
            }
        }
    }
}
