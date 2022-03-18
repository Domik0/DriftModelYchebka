using System;
using System.Collections.Generic;
using System.Data.Entity;
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
    /// Логика взаимодействия для SelectPilot.xaml
    /// </summary>
    public partial class SelectPilot : Page
    {
        Qualification qualificationSelect;

        public SelectPilot(Qualification select)
        {
            InitializeComponent();
            qualificationSelect = select;
            List<Pilot> qualificationPilot = new List<Pilot>();
            foreach(var item in qualificationSelect.Stages.Qualifications)
            {
                qualificationPilot.Add(item.Pilots);
            }
            qualificationPilot.Remove(qualificationSelect.Pilots);
            PilotComboBox.ItemsSource = (App.dbContext.pilots.ToList()).Except(qualificationPilot);
            if (qualificationSelect.Pilots != null)
            {
                PilotComboBox.SelectedItem = PilotComboBox.Items[PilotComboBox.Items.IndexOf(qualificationSelect.Pilots)];
            }
        }

        private void ButtonBackClick(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void ButtonSaveClick(object sender, RoutedEventArgs e)
        {
            if((PilotComboBox.SelectedItem as Pilot) != qualificationSelect.Pilots)
            {
                qualificationSelect.Pilots = PilotComboBox.SelectedItem as Pilot;
                MessageBox.Show("Сохранено", "", MessageBoxButton.OK);
            }
        }
    }
}
