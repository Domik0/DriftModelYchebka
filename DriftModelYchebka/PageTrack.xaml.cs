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
    /// Логика взаимодействия для PageTrack.xaml
    /// </summary>
    public partial class PageTrack : Page
    {
        public PageTrack()
        {
            InitializeComponent();
            TrackListView.ItemsSource = App.dbContext.tracks.ToList();

            //19.03.22
            if (App.User?.Role == 1 || App.User?.Role == 3)
            {
                addTrackButton.Visibility = Visibility.Hidden;
            }
            else
            {
                addTrackButton.Visibility = Visibility.Visible;
            }
        }

        private void PageLoaded(object sender, RoutedEventArgs e)
        {
            TrackListView.ItemsSource = null;
            TrackListView.ItemsSource = App.dbContext.tracks.Local.ToList();
        }

        private void TrackAddClick(object sender, RoutedEventArgs e)
        {
            App.dbContext.tracks.Add(new Tracks());
            TrackListView.ItemsSource = null;
            TrackListView.ItemsSource = App.dbContext.tracks.Local;
            TrackListView.SelectedItem = null;
            TrackListView.SelectedItem = TrackListView.Items[TrackListView.Items.Count - 1];
        }

        private void TrackListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (TrackListView.SelectedItem != null)
            {
                Tracks t = TrackListView.SelectedItem as Tracks;
                NavigationService.Navigate(new TrackInfo(t));
                TrackListView.SelectedItem = null;
            }
        }
    }
}
