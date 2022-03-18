using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public partial class PageTournament : Page
    {
        ObservableCollection<Tournament> tournaments;
        public PageTournament()
        {
            InitializeComponent();
            tournaments = App.dbContext.tournaments.Local;
            TournamentListView.ItemsSource = tournaments;
        }

        private void SelectTournamentClick(object sender, RoutedEventArgs e)
        {
            Tournament t = TournamentListView.SelectedItem as Tournament;
            TournamentInfo.Navigate(new TournamentInfo(t));
        }

        private void TournamentAddClick(object sender, RoutedEventArgs e)
        {
            tournaments.Add(new Tournament());
            TournamentListView.ItemsSource = null;
            TournamentListView.ItemsSource = tournaments;
            TournamentListView.SelectedItem = null;
            TournamentListView.SelectedItem = TournamentListView.Items[TournamentListView.Items.Count - 1];
        }
    }
}
