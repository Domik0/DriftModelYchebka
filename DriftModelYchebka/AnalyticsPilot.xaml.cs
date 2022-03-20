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
        int tmpSotr = 0;
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

        private void Sorted(object sender, MouseButtonEventArgs e)
        {

            switch (tmpSotr)
            {
                case 0:
                    PilotListView.ItemsSource = App.dbContext.pilots.ToList().OrderByDescending(p => p.WinRate);
                    tmpSotr = 1;
                    break;
                    
                case 1:
                    PilotListView.ItemsSource = App.dbContext.pilots.ToList().OrderBy(p => p.WinRate);
                    tmpSotr = 2;
                    break;

                default:
                    PilotListView.ItemsSource = App.dbContext.pilots.ToList();
                    tmpSotr = 0;
                    break;

            }
        }
    }
}
