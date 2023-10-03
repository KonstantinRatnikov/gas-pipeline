using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using System.Collections.Generic;
using System.Windows;


namespace Газопровод
{
    /// <summary>
    /// Логика взаимодействия для Graph.xaml
    /// </summary>
    public partial class Graph : Window
    {
        PlotModel plotModel;

        public Graph()
        {
            InitializeComponent();
        }
        public Graph(string name, Point[] points, string nameX, string nameY)
        {
            InitializeComponent();

            plotModel = new PlotModel { Title = name };

            BuildGraph(points, nameX, nameY);
        }
        void BuildGraph(Point[] points, string nameX, string nameY)
        {

            var series = new LineSeries();
            var xAxis = new LinearAxis
            {
                Position = AxisPosition.Bottom,
                Title = nameX,
                AxisTitleDistance = 10
            };
            var yAxis = new LinearAxis
            {
                Position = AxisPosition.Left,
                Title = nameY,
                AxisTitleDistance = 10
            };
            // Устанавливаем стиль для сетки на оси X
            xAxis.MajorGridlineStyle = LineStyle.Solid;
            xAxis.MajorGridlineColor = OxyColor.FromRgb(200, 200, 200);

            // Устанавливаем стиль для сетки на оси Y
            yAxis.MajorGridlineStyle = LineStyle.Solid;
            yAxis.MajorGridlineColor = OxyColor.FromRgb(200, 200, 200);

            for (int i = 0; i < points.Length; i++)
            {
                series.Points.Add(new DataPoint(points[i].X, points[i].Y));
            }
            
            plotModel.Axes.Add(xAxis);
            plotModel.Axes.Add(yAxis);
            plotModel.Series.Add(series);
            plotView.Model = plotModel;
        }
    }
}
