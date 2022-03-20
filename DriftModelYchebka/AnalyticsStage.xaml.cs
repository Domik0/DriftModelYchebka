using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DriftModelYchebka
{
    /// <summary>
    /// Interaction logic for AnalyticsStage.xaml
    /// </summary>
    public partial class AnalyticsStage : Page
    {
        Stage stageSelect;
        public AnalyticsStage(Stage select)
        {
            InitializeComponent();
            stageSelect = select;
            QualificationListView.ItemsSource = stageSelect.Qualifications;
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(QualificationListView.ItemsSource);
            view.SortDescriptions.Add(new SortDescription("WinRate", ListSortDirection.Descending));
        }

        private void BackClick(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
