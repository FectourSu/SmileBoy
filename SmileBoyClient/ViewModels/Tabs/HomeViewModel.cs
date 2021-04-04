using LiveCharts;
using LiveCharts.Configurations;
using LiveCharts.Wpf;
using LiveCharts.Wpf.Charts.Base;
using System;
using System.Drawing;
using System.Linq;
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
                        1023,
                        2311,
                        1111,
                        2359,
                        3005,
                        4010,
                        3100,
                        2654,
                        2913,
                        3811,
                        4607,
                        3120,
                        2230,
                        3333,
                        4225,
                        4031,
                        4229,
                        4133,
                        5012,
                    },
                    LabelPoint = point => "$" + point.Y,
                    Fill = System.Windows.Media.Brushes.Transparent,
                },
            };
        }
        
    }
}
