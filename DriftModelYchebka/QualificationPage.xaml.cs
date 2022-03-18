using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity;
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
    /// Логика взаимодействия для QualificationPage.xaml
    /// </summary>
    public partial class QualificationPage : Page
    {
        Stage stageSelect;
        Stage stageClone;
        public QualificationPage(Stage select)
        {
            InitializeComponent();
            stageSelect = select;
            stageClone = new Stage
            {
                Id = stageSelect.Id,
                Number = stageSelect.Number,
                Date = stageSelect.Date,
                TournamentId = stageSelect.TournamentId,
                TrackId = stageSelect.TrackId,
                Qualifications = stageSelect.Qualifications,
                PairArrivals = stageSelect.PairArrivals,
                Tournaments = stageSelect.Tournaments,
                Tracks = stageSelect.Tracks
            };

            //19.03.22
            if (App.User?.Role == 1 || App.User?.Role == 3)
            {
                DelSelectQualification.Visibility = Visibility.Hidden;
                AddSelectQualification.Visibility = Visibility.Hidden;
                EditSelectQualification.Visibility = Visibility.Hidden;
            }
            else
            {
                DelSelectQualification.Visibility = Visibility.Visible;
                AddSelectQualification.Visibility = Visibility.Visible;
                EditSelectQualification.Visibility = Visibility.Visible;
            }
        }

        private void BackClick(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void QualificationSaveClick(object sender, RoutedEventArgs e)
        {
            stageSelect.Qualifications = stageClone.Qualifications;
            App.dbContext.SaveChanges();
            MessageBox.Show("Сохранено", "", MessageBoxButton.OK);
            QualificationListView.ItemsSource = stageClone.Qualifications;
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(QualificationListView.ItemsSource);
            view.SortDescriptions.Add(new SortDescription("BestAttempt", ListSortDirection.Descending));
            int indexQualification = 1;
            foreach (Qualification item in view)
            {
                if (indexQualification == 1)
                    item.PointForQualification = 25;
                if (indexQualification == 2)
                    item.PointForQualification = 21;
                if (indexQualification == 3)
                    item.PointForQualification = 19;
                if (indexQualification == 4)
                    item.PointForQualification = 17;
                if (5 <= indexQualification && indexQualification <= 6)
                    item.PointForQualification = 12;
                if (7 <= indexQualification && indexQualification <= 8)
                    item.PointForQualification = 9;
                if (9 <= indexQualification && indexQualification <= 12)
                    item.PointForQualification = 6;
                if (13 <= indexQualification && indexQualification <= 16)
                    item.PointForQualification = 4;
                indexQualification++;
            }
        }

        private void EditSelectQualificationClick(object sender, RoutedEventArgs e)
        {
            Qualification q = QualificationListView.SelectedItem as Qualification;
            if(q != null)
            {
                MainPage mp = App.Current.MainWindow.Content as MainPage;
                App.dbContext.stages.Load();
                mp.mainFrame.NavigationService.Navigate(new SelectPilot(q));
            }
            else
            {
                MessageBox.Show("Нужно выбрать участника", "Предупреждение", MessageBoxButton.OK);
            }
        }

        private void DelSelectQualificationClick(object sender, RoutedEventArgs e)
        {
            Qualification q = QualificationListView.SelectedItem as Qualification;
            if (q != null)
            {
                stageClone.Qualifications.Remove(q);
                QualificationListView.ItemsSource = null;
                QualificationListView.ItemsSource = stageClone.Qualifications;
            }
            else
            {
                MessageBox.Show("Нужно выбрать участника", "Предупреждение", MessageBoxButton.OK);
            }
}

        private void AddSelectQualificationClick(object sender, RoutedEventArgs e)
        {
            stageClone.Qualifications.Add(new Qualification
            {
                StagesId = stageClone.Id
            });
            App.dbContext.Entry((stageClone.Qualifications.ToList()).Last()).Reference(s => s.Stages).Load();
            QualificationListView.ItemsSource = null;
            QualificationListView.ItemsSource = stageClone.Qualifications;
            QualificationListView.SelectedItem = null;
            QualificationListView.SelectedItem = stageClone.Qualifications.Last();
            EditSelectQualification.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            QualificationListView.ItemsSource = stageClone.Qualifications;
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(QualificationListView.ItemsSource);
            view.SortDescriptions.Add(new SortDescription("BestAttempt", ListSortDirection.Descending));
            int indexQualification = 1;
            foreach (Qualification item in view)
            {
                if (indexQualification == 1)
                    item.PointForQualification = 25;
                if (indexQualification == 2)
                    item.PointForQualification = 21;
                if (indexQualification == 3)
                    item.PointForQualification = 19;
                if (indexQualification == 4)
                    item.PointForQualification = 17;
                if (5 <= indexQualification && indexQualification <= 6)
                    item.PointForQualification = 12;
                if (7 <= indexQualification && indexQualification <= 8)
                    item.PointForQualification = 9;
                if (9 <= indexQualification && indexQualification <= 12)
                    item.PointForQualification = 6;
                if (13 <= indexQualification && indexQualification <= 16)
                    item.PointForQualification = 4;
                indexQualification++;
            }
        }
    }
}
