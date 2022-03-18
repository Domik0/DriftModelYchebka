using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
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
    /// Логика взаимодействия для PairArrivalsPage.xaml
    /// </summary>
    public partial class PairArrivalsPage : Page
    {
        Stage stageSelect;
        Stage stageClone;

        public PairArrivalsPage(Stage select)
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
        }

        private void BackClick(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void PairArrivalsSaveClick(object sender, RoutedEventArgs e)
        {
            stageSelect.PairArrivals = stageClone.PairArrivals;
            try
            {
                App.dbContext.SaveChanges();
                MessageBox.Show("Сохранено", "", MessageBoxButton.OK);
            }
            catch (System.Data.Entity.Core.EntityException)
            {
                MessageBox.Show("Ошибка при сохранении", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                Process.GetCurrentProcess().Kill();
            }
        }

        private void PageLoaded(object sender, RoutedEventArgs e)
        {
            for (int i = stageClone.PairArrivals.Where(p => p.Tour == 1).Count(); i < 8; i++)
            {
                stageClone.PairArrivals.Add(new PairArrivals
                {
                    StagesId = stageClone.Id,
                    Tour = 1
                });
            }
            for (int i = stageClone.PairArrivals.Where(p => p.Tour == 2).Count(); i < 4; i++)
            {
                stageClone.PairArrivals.Add(new PairArrivals
                {
                    StagesId = stageClone.Id,
                    Tour = 2
                });
            }
            for (int i = stageClone.PairArrivals.Where(p => p.Tour == 3).Count(); i < 2; i++)
            {
                stageClone.PairArrivals.Add(new PairArrivals
                {
                    StagesId = stageClone.Id,
                    Tour = 3
                });
            }
            if (stageClone.PairArrivals.Where(p => p.Tour == 4).Count() == 0)
            {
                stageClone.PairArrivals.Add(new PairArrivals
                {
                    StagesId = stageClone.Id,
                    Tour = 4
                });
            }
            List<PairArrivals> listTop8 = stageClone.PairArrivals.Where(p => p.Tour == 1).ToList();
            List<PairArrivals> listTop4 = stageClone.PairArrivals.Where(p => p.Tour == 2).ToList();
            List<PairArrivals> listTop2 = stageClone.PairArrivals.Where(p => p.Tour == 3).ToList();
            List<PairArrivals> listTop1 = stageClone.PairArrivals.Where(p => p.Tour == 4).ToList();

            int indexList = 0, indexPilot = 0;
            foreach (var item in (stageClone.Qualifications.ToList()).OrderBy(q => q.BestAttempt).Take(16))
            {
                if (indexPilot == 0)
                {
                    listTop8[indexList].FirstPilotId = item.PilotId;
                    listTop8[indexList].FirstPilot = item.Pilots;
                }
                else
                {
                    listTop8[indexList].SecondPilotId = item.PilotId;
                    listTop8[indexList].SecondPilot = item.Pilots;
                }
                indexPilot++;
                if (indexPilot == 2)
                {
                    indexPilot = 0;
                    indexList++;
                }
            }
            indexList = 0; indexPilot = 0;
            foreach (var item in listTop8)
            {
                if (item.FirstPilotWin != null)
                {
                    if (indexPilot == 0)
                    {
                        listTop4[indexList].FirstPilotId = item.FirstPilotWin == true ? item.FirstPilotId : item.FirstPilotWin == false ? item.SecondPilotId : null;
                        listTop4[indexList].FirstPilot = item.FirstPilotWin == true ? item.FirstPilot : item.FirstPilotWin == false ? item.SecondPilot : null;
                    }
                    else
                    {
                        listTop4[indexList].SecondPilotId = item.FirstPilotWin == true ? item.FirstPilotId : item.FirstPilotWin == false ? item.SecondPilotId : null;
                        listTop4[indexList].SecondPilot = item.FirstPilotWin == true ? item.FirstPilot : item.FirstPilotWin == false ? item.SecondPilot : null;
                    }
                }
                indexPilot++;
                if (indexPilot == 2)
                {
                    indexPilot = 0;
                    indexList++;
                }
            }
            indexList = 0; indexPilot = 0;
            foreach (var item in listTop4)
            {
                if (indexPilot == 0)
                {
                    listTop2[indexList].FirstPilotId = item.FirstPilotWin == true ? item.FirstPilotId : item.FirstPilotWin == false ? item.SecondPilotId : null;
                    listTop2[indexList].FirstPilot = item.FirstPilotWin == true ? item.FirstPilot : item.FirstPilotWin == false ? item.SecondPilot : null;
                }
                else
                {
                    listTop2[indexList].SecondPilotId = item.FirstPilotWin == true ? item.FirstPilotId : item.FirstPilotWin == false ? item.SecondPilotId : null;
                    listTop2[indexList].SecondPilot = item.FirstPilotWin == true ? item.FirstPilot : item.FirstPilotWin == false ? item.SecondPilot : null;
                }
                indexPilot++;
                if (indexPilot == 2)
                {
                    indexPilot = 0;
                    indexList++;
                }
            }
            indexList = 0; indexPilot = 0;
            foreach (var item in listTop2)
            {
                if (indexPilot == 0)
                {
                    listTop1[indexList].FirstPilotId = item.FirstPilotWin == true ? item.FirstPilotId : item.FirstPilotWin == false ? item.SecondPilotId : null;
                    listTop1[indexList].FirstPilot = item.FirstPilotWin == true ? item.FirstPilot : item.FirstPilotWin == false ? item.SecondPilot : null;
                }
                else
                {
                    listTop1[indexList].SecondPilotId = item.FirstPilotWin == true ? item.FirstPilotId : item.FirstPilotWin == false ? item.SecondPilotId : null;
                    listTop1[indexList].SecondPilot = item.FirstPilotWin == true ? item.FirstPilot : item.FirstPilotWin == false ? item.SecondPilot : null;
                }
                indexPilot++;
                if (indexPilot == 2)
                {
                    indexPilot = 0;
                    indexList++;
                }
            }

            Top81.DataContext = null;
            Top82.DataContext = null;
            Top83.DataContext = null;
            Top84.DataContext = null;
            Top85.DataContext = null;
            Top86.DataContext = null;
            Top87.DataContext = null;
            Top88.DataContext = null;

            Top41.DataContext = null;
            Top42.DataContext = null;
            Top43.DataContext = null;
            Top44.DataContext = null;

            Top21.DataContext = null;
            Top22.DataContext = null;

            Top1.DataContext = null;

            Top81.DataContext = listTop8[0];
            Top82.DataContext = listTop8[1];
            Top83.DataContext = listTop8[2];
            Top84.DataContext = listTop8[3];
            Top85.DataContext = listTop8[4];
            Top86.DataContext = listTop8[5];
            Top87.DataContext = listTop8[6];
            Top88.DataContext = listTop8[7];

            Top41.DataContext = listTop4[0];
            Top42.DataContext = listTop4[1];
            Top43.DataContext = listTop4[2];
            Top44.DataContext = listTop4[3];

            Top21.DataContext = listTop2[0];
            Top22.DataContext = listTop2[1];

            Top1.DataContext = listTop1[0];
        }
    }
}
