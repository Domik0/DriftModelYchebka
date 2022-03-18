using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
    /// Логика взаимодействия для TournamentInfo.xaml
    /// </summary>
    public partial class TournamentInfo : Page
    {
        Tournament tournamentSelect;
        Tournament tournamentClone;

        public TournamentInfo(Tournament select)
        {
            InitializeComponent();
            this.tournamentSelect = select;
            if(select != null)
            {
                if (select.Name == null)
                {
                    tournamentClone = tournamentSelect;
                    EditTournamentButton.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
                }
                else
                {
                    tournamentClone = new Tournament
                    {
                        Id = select.Id,
                        Name = select.Name,
                        ImageTournaments = select.ImageTournaments,
                        Stages = select.Stages
                    };
                    StagesComboBox.ItemsSource = tournamentClone.Stages;
                    if(tournamentClone.Stages.Count != 0)
                    {
                        StagesComboBox.SelectedIndex = 0;
                    }
                }
                DataContext = tournamentClone;
            }
        }

        private void StageSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Stage s = StagesComboBox.SelectedItem as Stage;
            StageInfo.Navigate(new StageInfo(s));
        }

        private void EditClick(object sender, RoutedEventArgs e)
        {
            EditTournamentButton.Visibility = Visibility.Hidden;
            SaveTournamentButton.Visibility = Visibility.Visible;
            DeletTournament.Visibility = Visibility.Visible;

            NameLabel.Visibility = Visibility.Hidden;
            TextBoxNameTournament.Visibility = Visibility.Visible;
            RemoveStage.Visibility = Visibility.Visible;
            AddStage.Visibility = Visibility.Visible;
        }

        private void SaveTeamClick(object sender, RoutedEventArgs e)
        {
            EditTournamentButton.Visibility = Visibility.Visible;
            SaveTournamentButton.Visibility = Visibility.Hidden;
            DeletTournament.Visibility = Visibility.Hidden;

            NameLabel.Visibility = Visibility.Visible;
            TextBoxNameTournament.Visibility = Visibility.Hidden;
            RemoveStage.Visibility = Visibility.Hidden;
            AddStage.Visibility = Visibility.Hidden;

            tournamentSelect.Name = tournamentClone.Name;
            tournamentSelect.Stages = tournamentClone.Stages;
            App.dbContext.SaveChanges();

            PageTournament p = (App.Current.MainWindow.Content as MainPage).mainFrame.Content as PageTournament;//очень плохо
            p.TournamentListView.Items.Refresh();
        }

        private void AddStageClick(object sender, RoutedEventArgs e)
        {
            tournamentClone.Stages.Add(new Stage
            {
                TournamentId = tournamentClone.Id,
                Number = tournamentClone.Stages.Count() == 0 ? 1 : tournamentClone.Stages.Last().Number + 1
            });
            StagesComboBox.ItemsSource = null;
            StagesComboBox.ItemsSource = tournamentClone.Stages;
            StagesComboBox.SelectedItem = null;
            StagesComboBox.SelectedItem = StagesComboBox.Items[StagesComboBox.Items.Count - 1];
        }

        private void RemoveStageClick(object sender, RoutedEventArgs e)
        {
            tournamentClone.Stages.Remove(StagesComboBox.SelectedItem as Stage);
            StagesComboBox.ItemsSource = null;
            StagesComboBox.ItemsSource = tournamentClone.Stages;
        }

        private void DeletTournamentClick(object sender, RoutedEventArgs e)
        {
            if(MessageBox.Show("Точно хотете удалить турнир?", "Подтверждение", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                App.dbContext.tournaments.Remove(tournamentSelect);
                App.dbContext.SaveChanges();
            }
            PageTournament p = (App.Current.MainWindow.Content as MainPage).mainFrame.Content as PageTournament;
            p.TournamentInfo.Content = null;
        }
    }
}
