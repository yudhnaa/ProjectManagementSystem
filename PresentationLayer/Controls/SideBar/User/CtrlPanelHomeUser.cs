using BusinessLayer.Services;
using DTOLayer.Models;
using PresentationLayer.AppContext;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace PresentationLayer.Controls.SideBar
{
    public partial class CtrlPanelHomeUser : UserControl
    {

        private readonly UserDTO user;
        private readonly IUserServices userServices = new UserServices();
        private readonly ITaskServices taskServices = new TaskServices();
        public CtrlPanelHomeUser()
        {
            user = UserSession.Instance.User;
            InitializeComponent();
        }

        protected override void OnVisibleChanged(EventArgs e)
        {
            base.OnVisibleChanged(e);

            if (this.Visible)
            {
                LoadAllCharts();
            }
        }

        private void CtrlPanelHomeUser_Load(object sender, EventArgs e)
        {
            InitControl();
        }

        public void LoadAllCharts()
        {
            SetDataChartTasksPerProject(user.Id);
            SetDataChartStatusOfTask(user.Id);
            SetDataChartCompletedOfDay(user.Id);
        }
        private void InitControl()
        {
            this.Dock = DockStyle.Fill;
            
            int marginH = (int)(tableLayoutPanel1.Width / 4 * 0.2);
            int marginV = (int)(tableLayoutPanel1.Height / 4 * 0.2);

            chart2.Margin = new Padding(marginH, marginV, marginH, marginV);
            chart3.Margin = new Padding(marginH, marginV, marginH, marginV);
            chartCompletedTasks.Margin = new Padding(marginH, marginV, marginH, marginV);
        }

        private void ResetChartData(Chart chart)
        {
            chart.Titles.Clear();
            chart.Series.Clear();
        }

        //Column Chart
        private void SetDataChartTasksPerProject(int userId)
        {
            var taskInProject = new TaskServices();
            var data = taskInProject.CountTaskByProjectAndUserId(userId);
            // Clear existing series
            ResetChartData(chart2);
            // Create a new series
            Series series = new Series
            {
                Name = "Tasks per Project",
                ChartType = SeriesChartType.Column,
                Color = Color.CornflowerBlue,
                BorderWidth = 1,
                YValueType = ChartValueType.Int32
            };
            
            // Generate sample data
            foreach(var item in data)
            {
                int pointIndex = series.Points.AddXY(item.Key, item.Value);
                var point = series.Points[pointIndex];
                point.Label = item.Value.ToString();
            }

            // Add series to chart
            chart2.Series.Add(series);
            
            // Set chart titles (optional)
            chart2.Titles.Add("Task Count per Project");
            chart2.ChartAreas[0].AxisX.Title = "Project";
            chart2.ChartAreas[0].AxisY.Title = "Task";
            chart2.ChartAreas[0].AxisY.MajorGrid.LineWidth = 1;
            
        }

        //Pie Chart
        private void SetDataChartStatusOfTask(int userId)
        {
            var taskServices = new TaskServices();
            var data = taskServices.CountTaskByStatusAndUserId(userId);
            // Calculate total count for percentage calculation
            int total = data.Sum(item => item.Value);

            // Clear existing series
            ResetChartData(chart3);

            // Create a new series
            Series series = new Series
            {
                ChartType = SeriesChartType.Doughnut,
                IsValueShownAsLabel = true
            };
            series["DoughnutRadius"] = "40";

            // Generate sample data
            foreach (var item in data)
            {
                int pointIndex = series.Points.AddXY(item.Key, item.Value);
                double percentage = total == 0 ? 0 : ((double)item.Value / total) * 100;
                // Format label thành phần trăm nếu > 0
                if (percentage > 0)
                {
                    series.Points[pointIndex].Label = $"{percentage:F1}%";
                }
                else
                {
                    // Ẩn label nếu 0%
                    series.Points[pointIndex].Label = "";
                }
                // Luôn hiện chú thích trạng thái
                series.Points[pointIndex].LegendText = item.Key;
            }

            series.LabelFormat = "#";
            // Add series to chart
            chart3.Series.Add(series);

            // Set chart titles (optional)
            chart3.Titles.Add("Task Counts by Status");
            chart3.ChartAreas[0].AxisX.Title = "X Axis";
            chart3.ChartAreas[0].AxisY.Title = "Y Axis";
        }

        //Line Chart
        private void SetDataChartCompletedOfDay(int userId)
        {
            var taskServices = new TaskServices();
            var data = taskServices.GetCompletedTaskByDate(userId);
            // Clear existing series
            ResetChartData(chartCompletedTasks);

            Series series1 = new Series
            {
                Name = "Task Completed",
                ChartType = SeriesChartType.Spline,
                BorderWidth = 2,
                Color = Color.Blue,
                XValueType = ChartValueType.Date,
                YValueType = ChartValueType.Int32,
            };

            foreach (var item in data)
            {
                series1.Points.AddXY(item.Key, item.Value);
            }
            chartCompletedTasks.Series.Add(series1);

            // SetDataChartCompletedOfDay chart titles (optional)
            chartCompletedTasks.Titles.Add("Task Completed per Day");
            chartCompletedTasks.ChartAreas[0].AxisX.Title = "Day, Month, Year";
            chartCompletedTasks.ChartAreas[0].AxisY.Title = "Number of Task";
        }


        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {
            //TableLayoutPanel panel = sender as TableLayoutPanel;
            //if (panel == null) return;

            //Pen pen = new Pen(Color.Gray, 1); // Màu và độ dày của đường chia

            //// Tính vị trí dòng (horizontal lines)
            //int y = 0;
            //for (int row = 0; row < panel.RowCount - 1; row++)
            //{
            //    y += panel.GetRowHeights()[row];
            //    e.Graphics.DrawLine(pen, 0, y, panel.Width, y); // Vẽ đường ngang
            //}

            //// Tính vị trí cột (vertical lines)
            //int x = 0;
            //for (int col = 0; col < panel.ColumnCount - 1; col++)
            //{
            //    x += panel.GetColumnWidths()[col];
            //    e.Graphics.DrawLine(pen, x, 0, x, panel.Height); // Vẽ đường dọc
            //}
        }
    }
}
