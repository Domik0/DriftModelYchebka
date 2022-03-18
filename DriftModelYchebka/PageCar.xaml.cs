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
    /// Логика взаимодействия для PageCar.xaml
    /// </summary>
    public partial class PageCar : Page
    {
        public PageCar()
        {
            InitializeComponent();
            CarListView.ItemsSource = App.dbContext.cars.Local.ToList();
        }

        private void PageLoaded(object sender, RoutedEventArgs e)
        {
            CarListView.ItemsSource = null;
            CarListView.ItemsSource = App.dbContext.cars.ToList();
        }

        private void CarSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CarListView.SelectedItem != null)
            {
                Car c = CarListView.SelectedItem as Car;
                NavigationService.Navigate(new CarInfo(c));
                CarListView.SelectedItem = null;
            }
        }

        private void CarAddClick(object sender, RoutedEventArgs e)
        {
            App.dbContext.cars.Add(new Car());
            CarListView.ItemsSource = null;
            CarListView.ItemsSource = App.dbContext.cars.Local;
            CarListView.SelectedItem = null;
            CarListView.SelectedItem = CarListView.Items[CarListView.Items.Count - 1];
        }
    }
}
