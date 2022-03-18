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
    /// Логика взаимодействия для TrackInfo.xaml
    /// </summary>
    public partial class TrackInfo : Page
    {
        Tracks _tracksesSelect;
        Tracks _tracksesClone;

        public TrackInfo(Tracks select)
        {
            InitializeComponent();
            this._tracksesSelect = select;
            if (_tracksesSelect != null)
            {
                _tracksesClone = new Tracks
                {
                    Id = _tracksesSelect.Id,
                    Name = _tracksesSelect.Name,
                    City = _tracksesSelect.City,
                    ImageTrack = _tracksesSelect.ImageTrack
                };

                if (select.Name == null)
                {
                    EditTrackButton.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
                }
                DataContext = _tracksesClone;
                //19.03.22
                if (App.User?.Role == 1 || App.User?.Role == 3)
                {
                    EditTrackButton.Visibility = Visibility.Hidden;
                }
                else
                {
                    EditTrackButton.Visibility = Visibility.Visible;
                }
            }
        }

        private void BackClick(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void EditTrackClick(object sender, RoutedEventArgs e)
        {
            EditTrackButton.Visibility = Visibility.Hidden;
            SaveTrackButton.Visibility = Visibility.Visible;
            DeletTrack.Visibility = Visibility.Visible;

            ImageTrack.Visibility = Visibility.Hidden;
            TextBoxImageTrack.Visibility = Visibility.Visible;

            NameLabel.Visibility = Visibility.Hidden;
            CityLabel.Visibility = Visibility.Hidden;
            TextBoxNameTrak.Visibility = Visibility.Visible;
            TextBoxCityTrack.Visibility = Visibility.Visible;
        }

        private void SaveTrackClick(object sender, RoutedEventArgs e)
        {
            EditTrackButton.Visibility = Visibility.Visible;
            SaveTrackButton.Visibility = Visibility.Hidden;
            DeletTrack.Visibility = Visibility.Hidden;

            ImageTrack.Visibility = Visibility.Visible;
            TextBoxImageTrack.Visibility = Visibility.Hidden;

            NameLabel.Visibility = Visibility.Visible;
            CityLabel.Visibility = Visibility.Visible;
            TextBoxNameTrak.Visibility = Visibility.Hidden;
            TextBoxCityTrack.Visibility = Visibility.Hidden;


            if (_tracksesClone != null)
            {
                _tracksesSelect.Id = _tracksesClone.Id;
                _tracksesSelect.Name = _tracksesClone.Name;
                _tracksesSelect.City = _tracksesClone.City;
                _tracksesSelect.ImageTrack = _tracksesClone.ImageTrack;
            }
            App.dbContext.SaveChanges();
        }

        private void DeletTrackClick(object sender, RoutedEventArgs e)
        {
            App.dbContext.tracks.Remove(_tracksesSelect);
            App.dbContext.SaveChanges();
            NavigationService.GoBack();
        }
    }
}
