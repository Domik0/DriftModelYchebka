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
    /// Логика взаимодействия для TeamInfo.xaml
    /// </summary>
    public partial class TeamInfo : Page
    {
        Team TeamSelect;
        Team TeamClone;
        public TeamInfo(Team select)
        {
            InitializeComponent();
            this.TeamSelect = select;
            if (TeamSelect != null)
            {
                TeamClone = new Team
                {
                    Id = TeamSelect.Id,
                    Name = TeamSelect.Name,
                    ImageTeam = TeamSelect.ImageTeam,
                };

                if (select.Name == null)
                {
                    EditTeamButton.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
                }
                DataContext = TeamClone;
                //19.03.22
                if (App.User?.Role == 1 || App.User?.Role == 3)
                {
                    EditTeamButton.Visibility = Visibility.Hidden;
                }
                else
                {
                    EditTeamButton.Visibility = Visibility.Visible;
                }
            }
        }

        private void BackClick(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void EditTrackClick(object sender, RoutedEventArgs e)
        {
            TeamImage.Visibility = Visibility.Hidden;
            TeamImageBox.Visibility = Visibility.Visible;
            NameText.Visibility = Visibility.Hidden;
            NameBox.Visibility = Visibility.Visible;

            EditTeamButton.Visibility = Visibility.Hidden;
            SaveDelTeamButton.Visibility = Visibility.Visible;
        }

        private void SaveTrackClick(object sender, RoutedEventArgs e)
        {
            TeamImage.Visibility = Visibility.Visible;
            TeamImageBox.Visibility = Visibility.Hidden;
            NameText.Visibility = Visibility.Visible;
            NameBox.Visibility = Visibility.Hidden;

            EditTeamButton.Visibility = Visibility.Visible;
            SaveDelTeamButton.Visibility = Visibility.Hidden;

            if (TeamClone != null)
            {
                TeamSelect.Id = TeamClone.Id;
                TeamSelect.Name = TeamClone.Name;
                TeamSelect.ImageTeam = TeamClone.ImageTeam;
            }
            App.dbContext.SaveChanges();
        }

        private void DeletTrackClick(object sender, RoutedEventArgs e)
        {
            App.dbContext.teams.Remove(TeamSelect);
            App.dbContext.SaveChanges();
            NavigationService.GoBack();
        }
    }
}
