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
    /// Логика взаимодействия для CarInfo.xaml
    /// </summary>
    public partial class CarInfo : Page
    {
        Car carSelect;
        Car carClone;

        public CarInfo(Car select)
        {
            InitializeComponent();
            PilotComboBox.ItemsSource = App.dbContext.pilots.ToList();
            this.carSelect = select;
            if (carSelect != null)
            {
                carClone = new Car
                {
                    Id = carSelect.Id,
                    PilotId = carSelect.PilotId,
                    Pilots = carSelect.Pilots,
                    Model = carSelect.Model,
                    Engine = carSelect.Engine,
                    Power = carSelect.Power,
                    WheelWidth = carSelect.WheelWidth,
                    ImageCar = carSelect.ImageCar
                };

                if (select.Model == null)
                {
                    EditCarButton.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
                }
                DataContext = carClone;
            }
        }

        private void BackClick(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void EditTrackClick(object sender, RoutedEventArgs e)
        {
            ModelCarText.Visibility = Visibility.Hidden;
            ModelCarBox.Visibility = Visibility.Visible;
            EngineText.Visibility = Visibility.Hidden;
            EngineBox.Visibility = Visibility.Visible;

            EditCarButton.Visibility = Visibility.Hidden;
            SaveDelCarButton.Visibility = Visibility.Visible;
        }

        private void SaveTrackClick(object sender, RoutedEventArgs e)
        {
            ModelCarText.Visibility = Visibility.Visible;
            ModelCarBox.Visibility = Visibility.Hidden;
            EngineText.Visibility = Visibility.Visible;
            EngineBox.Visibility = Visibility.Hidden;

            EditCarButton.Visibility = Visibility.Visible;
            SaveDelCarButton.Visibility = Visibility.Hidden;

            if (carClone.PilotId != null)
            {
                Pilot p = PilotComboBox.SelectedItem as Pilot;
                carClone.PilotId = p.Id;
                carClone.Pilots = p;
            }

            if (carClone != null)
            {
                carSelect.Id = carClone.Id;
                carSelect.PilotId = carClone.PilotId;
                carSelect.Pilots = carClone.Pilots;
                carSelect.Model = carClone.Model;
                carSelect.Engine = carClone.Engine;
                carSelect.Power = carClone.Power;
                carSelect.WheelWidth = carClone.WheelWidth;
                carSelect.ImageCar = carClone.ImageCar;
            }
            App.dbContext.SaveChanges();
        }

        private void DeletTrackClick(object sender, RoutedEventArgs e)
        {
            App.dbContext.cars.Remove(carSelect);
            App.dbContext.SaveChanges();
            NavigationService.GoBack();
        }
    }
}
