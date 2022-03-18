using System;
using System.Collections.Generic;
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
    /// Логика взаимодействия для StageInfo.xaml
    /// </summary>
    public partial class StageInfo : Page
    {
        Stage stageSelect;
        Stage stageClone;

        public StageInfo(Stage select)
        {
            InitializeComponent();
            this.stageSelect = select;

            if (select != null)
            {
                if (select.Date == null)
                {
                    stageClone = stageSelect;
                    EditStageButton.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
                }
                else
                {
                    stageClone = new Stage
                    {
                        Id = stageSelect.Id,
                        Number = stageSelect.Number,
                        Date = stageSelect.Date,
                        TournamentId = stageSelect.TournamentId,
                        TrackId = stageSelect.TrackId,
                        PairArrivals = stageSelect.PairArrivals,
                        Qualifications = stageSelect.Qualifications,
                        Tournaments = stageSelect.Tournaments,
                        Tracks = stageSelect.Tracks
                    };
                }
                DataContext = stageClone;
                TracksComboBox.ItemsSource = App.dbContext.tracks.ToList();
            }
        }

        private void QualificationShowClick(object sender, RoutedEventArgs e)
        {
            Stage s = DataContext as Stage;
            MainPage mp = App.Current.MainWindow.Content as MainPage;
            mp.mainFrame.NavigationService.Navigate(new QualificationPage(s));
        }

        private void PairArrivalsShowClick(object sender, RoutedEventArgs e)
        {
            Stage s = DataContext as Stage;
            MainPage mp = App.Current.MainWindow.Content as MainPage;
            mp.mainFrame.NavigationService.Navigate(new PairArrivalsPage(s));
        }

        private void EditStageClick(object sender, RoutedEventArgs e)
        {
            EditStageButton.Visibility = Visibility.Hidden;
            SaveStageButton.Visibility = Visibility.Visible;
            TrackLabel.Visibility = Visibility.Hidden;
            TracksComboBox.Visibility = Visibility.Visible;
            CityLabel.Visibility = Visibility.Hidden;
            DateLabel.Visibility = Visibility.Hidden;
            StageImage.Visibility = Visibility.Hidden;
            TextBoxDateTeam.Visibility = Visibility.Visible;
        }

        private void SaveStageClick(object sender, RoutedEventArgs e)
        {
            EditStageButton.Visibility = Visibility.Visible;
            SaveStageButton.Visibility = Visibility.Hidden;
            TrackLabel.Visibility = Visibility.Visible;
            TracksComboBox.Visibility = Visibility.Hidden;
            CityLabel.Visibility = Visibility.Visible;
            DateLabel.Visibility = Visibility.Visible;
            StageImage.Visibility = Visibility.Visible;
            TextBoxDateTeam.Visibility = Visibility.Hidden;

            stageSelect.Tracks = stageClone.Tracks;
            stageSelect.Date = stageClone.Date;
            App.dbContext.SaveChanges();
        }

        private void TracksComboBoxSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(TracksComboBox.SelectedItem != null)
            {
                stageClone.Tracks = TracksComboBox.SelectedItem as Tracks;
                stageClone.TrackId = (TracksComboBox.SelectedItem as Tracks).Id;
                DataContext = null;
                DataContext = stageClone;
            }
        }
    }
}
