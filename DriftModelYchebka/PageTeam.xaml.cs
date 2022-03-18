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
    /// Логика взаимодействия для PageTeam.xaml
    /// </summary>
    public partial class PageTeam : Page
    {
        public PageTeam()
        {
            InitializeComponent();
            TeamListView.ItemsSource = App.dbContext.teams.Local.ToList();
        }

        private void PageLoaded(object sender, RoutedEventArgs e)
        {
            TeamListView.ItemsSource = null;
            TeamListView.ItemsSource = App.dbContext.teams.ToList();
        }

        private void TeamAddClick(object sender, RoutedEventArgs e)
        {
            App.dbContext.teams.Add(new Team());
            TeamListView.ItemsSource = null;
            TeamListView.ItemsSource = App.dbContext.teams.Local;
            TeamListView.SelectedItem = null;
            TeamListView.SelectedItem = TeamListView.Items[TeamListView.Items.Count - 1];
        }

        private void TeamListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (TeamListView.SelectedItem != null)
            {
                Team t = TeamListView.SelectedItem as Team;
                NavigationService.Navigate(new TeamInfo(t));
                TeamListView.SelectedItem = null;
            }
        }
    }
}
