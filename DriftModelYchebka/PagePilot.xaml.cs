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
using DriftModelYchebka;

namespace Drift
{
    /// <summary>
    /// Логика взаимодействия для PagePilot.xaml
    /// </summary>
    public partial class PagePilot : Page
    {
        public PagePilot()
        {
            InitializeComponent();

            //19.03.22
            if (App.User?.Role == 1 || App.User?.Role == 3)
            {
                addPilotButton.Visibility = Visibility.Hidden;
            }
            else
            {
                addPilotButton.Visibility = Visibility.Visible;
            }
        }

        private void PageLoaded(object sender, RoutedEventArgs e)
        {
            PilotListView.ItemsSource = null;
            PilotListView.ItemsSource = App.dbContext.pilots.Local.ToList();
        }

        private void PilotAddClick(object sender, RoutedEventArgs e)
        {
            App.dbContext.pilots.Add(new Pilot());
            PilotListView.ItemsSource = null;
            PilotListView.ItemsSource = App.dbContext.pilots.Local;
            PilotListView.SelectedItem = null;
            PilotListView.SelectedItem = PilotListView.Items[PilotListView.Items.Count - 1];
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
