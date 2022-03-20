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
    /// Логика взаимодействия для MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
            if(App.User?.Role == 3)
            {
                ButtonTrack.Visibility = Visibility.Hidden;
                ButtonCar.Visibility = Visibility.Hidden;
                ButtonTeam.Visibility = Visibility.Hidden;
            }
        }

        private void ButtonTournament(object sender, RoutedEventArgs e)
        {
            mainFrame.NavigationService.Navigate(new PageTournament());
        }

        private void ButtomPilot(object sender, RoutedEventArgs e)
        {
            if (App.User?.Role == 3)
            {
                mainFrame.NavigationService.Navigate(new AnalyticsPilot());
            }
            else
            {
                mainFrame.NavigationService.Navigate(new PagePilot());
            }
        }

        private void TrackClick(object sender, RoutedEventArgs e)
        {
            mainFrame.NavigationService.Navigate(new PageTrack());
        }

        private void CarClick(object sender, RoutedEventArgs e)
        {
            mainFrame.NavigationService.Navigate(new PageCar());
        }

        private void TeamClick(object sender, RoutedEventArgs e)
        {
            mainFrame.NavigationService.Navigate(new PageTeam());
        }
    }
}
