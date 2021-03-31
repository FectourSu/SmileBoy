using LiveCharts;
using LiveCharts.Wpf;
using LiveCharts.Wpf.Charts.Base;
using System.Drawing;
using System.Windows;

namespace SmileBoyClient.ViewModels
{
    class HomeViewModel
    {
        public SeriesCollection Series { get; set; }
        public HomeViewModel()
        {
            Series = new SeriesCollection
            {
                new LineSeries
                {
                    Values = new ChartValues<double>
                    {
                        1,
                        5,
                        7,
                        8,
                        5,
                        2,
                        8,
                        6,
                        2
                    },
                    Fill = System.Windows.Media.Brushes.Transparent,
                },
            };
        }
    }
}
