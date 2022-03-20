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
    /// Логика взаимодействия для PilotInfo.xaml
    /// </summary>
    public partial class PilotInfo : Page
    {
        Pilot pilotSelect;
        Pilot pilotClone;

        public PilotInfo(Pilot select)
        {
            InitializeComponent();
            TeamComboBox.ItemsSource = App.dbContext.teams.ToList();
            this.pilotSelect = select;
            if (pilotSelect != null)
            {
                pilotClone = new Pilot
                {
                    Id = pilotSelect.Id,
                    TeamId = pilotSelect.TeamId,
                    FirstName = pilotSelect.FirstName,
                    LastName = pilotSelect.LastName,
                    Birthdate = pilotSelect.Birthdate,
                    Number = pilotSelect.Number,
                    City = pilotSelect.City,
                    ImagePilot = pilotSelect.ImagePilot,
                    Cars = pilotSelect.Cars,
                    LeaderPairArrivals = pilotSelect.LeaderPairArrivals,
                    HauntingPairArrivals = pilotSelect.HauntingPairArrivals,
                    Qualifications = pilotSelect.Qualifications,
                    Teams = pilotSelect.Teams
                };

                if (select.FirstName == null)
                {
                    EditPilotButton.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
                }
                DataContext = pilotClone;
                if(pilotClone.TeamId == null)
                {
                    pilotClone.TeamId = 0;
                    pilotClone.Teams = App.dbContext.teams.Where(t => t.Id == 0).First();
                }
                if(App.User?.Role == 3)
                {
                    WinCountLabel.Visibility = Visibility.Visible;
                    ArrivalCountLabel.Visibility = Visibility.Visible;
                }
            }
        }

        private void BackClick(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void EditPilotClick(object sender, RoutedEventArgs e)
        {
            EditPilotButton.Visibility = Visibility.Hidden;
            SavePilotButton.Visibility = Visibility.Visible;
            DeletPilot.Visibility = Visibility.Visible;

            FirstNameLabel.Visibility = Visibility.Hidden;
            TextBoxFirstNamePilot.Visibility = Visibility.Visible;
            LastNameLabel.Visibility = Visibility.Hidden;
            TextBoxLastNamePilot.Visibility = Visibility.Visible;
            ImagePilot.Visibility = Visibility.Hidden;
            TextBoxImagePilot.Visibility = Visibility.Visible;

            NameLabel.Visibility = Visibility.Hidden;
            TextBoxNamePilot.Visibility = Visibility.Visible;
            CityLabel.Visibility = Visibility.Hidden;
            TextBoxCityPilot.Visibility = Visibility.Visible;
            BirthdateLabel.Visibility = Visibility.Hidden;
            TextBoxBirthdatePilot.Visibility = Visibility.Visible;
            TeamLabel.Visibility = Visibility.Hidden;
            TeamComboBox.Visibility = Visibility.Visible;
        }

        private void SavePilotClick(object sender, RoutedEventArgs e)
        {
            EditPilotButton.Visibility = Visibility.Visible;
            SavePilotButton.Visibility = Visibility.Hidden;
            DeletPilot.Visibility = Visibility.Hidden;

            FirstNameLabel.Visibility = Visibility.Visible;
            TextBoxFirstNamePilot.Visibility = Visibility.Hidden;
            LastNameLabel.Visibility = Visibility.Visible;
            TextBoxLastNamePilot.Visibility = Visibility.Hidden;
            ImagePilot.Visibility = Visibility.Visible;
            TextBoxImagePilot.Visibility = Visibility.Hidden;

            NameLabel.Visibility = Visibility.Visible;
            TextBoxNamePilot.Visibility = Visibility.Hidden;
            CityLabel.Visibility = Visibility.Visible;
            TextBoxCityPilot.Visibility = Visibility.Hidden;
            BirthdateLabel.Visibility = Visibility.Visible;
            TextBoxBirthdatePilot.Visibility = Visibility.Hidden;
            TeamLabel.Visibility = Visibility.Visible;
            TeamComboBox.Visibility = Visibility.Hidden;

            Team t = TeamComboBox.SelectedItem as Team;
            pilotClone.TeamId = t.Id;
            pilotClone.Teams = t;

            if (pilotClone != null)
            {
                pilotSelect.Id = pilotClone.Id;
                pilotSelect.FirstName = pilotClone.FirstName;
                pilotSelect.LastName = pilotClone.LastName;
                pilotSelect.Birthdate = pilotClone.Birthdate;
                pilotSelect.Number = pilotClone.Number;
                pilotSelect.City = pilotClone.City;
                pilotSelect.ImagePilot = pilotClone.ImagePilot;
                pilotSelect.Cars = pilotClone.Cars;
                pilotSelect.TeamId = pilotClone.TeamId;
                pilotSelect.Teams = pilotClone.Teams;
            }
            App.dbContext.SaveChanges();
        }

        private void DeletPilotClick(object sender, RoutedEventArgs e)
        {
            App.dbContext.pilots.Remove(pilotSelect);
            App.dbContext.SaveChanges();
            NavigationService.GoBack();
        }
    }
}
