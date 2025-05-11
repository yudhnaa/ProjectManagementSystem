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
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace PresentationLayer.Controls.SideBar
{
    public partial class CtrlPanelHomeUser : UserControl
    {

        private readonly UserDTO user;
        private readonly IUserServices userServices = new UserServices();
        private readonly ITaskServices taskServices = new TaskServices();
        private readonly Chart chart = new Chart();
        public CtrlPanelHomeUser()
        {
            user = UserSession.Instance.User;
            InitializeComponent();
        }


        private void CtrlPanelHomeUser_Load(object sender, EventArgs e)
        {
            InitControl();
            //LoadAllCharts();
        }

        public void LoadAllCharts()
        {
            SetDataChartTasksPerProject(user.Id);
            SetDataChartStatusOfTask(user.Id);
            GenerateChartData4();
        }
        private void InitControl()
        {
            this.Dock = DockStyle.Fill;
            

            int marginH = (int)(tableLayoutPanel1.Width / 4 * 0.2);
            int marginV = (int)(tableLayoutPanel1.Height / 4 * 0.2);


            chart2.Margin = new Padding(marginH, marginV, marginH, marginV);
            chart3.Margin = new Padding(marginH, marginV, marginH, marginV);
            chart4.Margin = new Padding(marginH, marginV, marginH, marginV);

        }

        private void ResetChartData(Chart chart)
        {
            chart.Titles.Clear();
            chart.Series.Clear();
        }

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
                series.Points.AddXY(item.Key, item.Value);
            }

            // Add series to chart
            chart2.Series.Add(series);

            // Set chart titles (optional)
            chart2.Titles.Add("Task Count per Project");
            chart2.ChartAreas[0].AxisX.Title = "Project";
            chart2.ChartAreas[0].AxisY.Title = "Task";
            chart2.ChartAreas[0].AxisY.MajorGrid.LineWidth = 1;
        }

        private void SetDataChartStatusOfTask(int userId)
        {
            var taskServices = new TaskServices();
            var data = taskServices.CountTaskByStatusAndUserId(userId);

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
                 series.Points.AddXY(item.Key, item.Value);
            }
            series.LabelFormat = "#";
            // Add series to chart
            chart3.Series.Add(series);

            // Set chart titles (optional)
            chart3.Titles.Add("Task Counts by Status");
            chart3.ChartAreas[0].AxisX.Title = "X Axis";
            chart3.ChartAreas[0].AxisY.Title = "Y Axis";
        }

        private void GenerateChartData4()
        {
            // Clear existing series
            ResetChartData(chart4);

            // Create a new series
            Series series = new Series("Sample Data");
            series.ChartType = SeriesChartType.Spline;

            // Generate sample data
            Random rnd = new Random();
            for (int i = 1; i <= 10; i++)
            {
                int yValue = rnd.Next(10, 100); // Random value between 10 and 100
                series.Points.AddXY(i, yValue);
            }

            // Add series to chart
            chart4.Series.Add(series);

            // Set chart titles (optional)
            chart4.Titles.Add("Sample Chart");
            chart4.ChartAreas[0].AxisX.Title = "X Axis";
            chart4.ChartAreas[0].AxisY.Title = "Y Axis";
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {
            TableLayoutPanel panel = sender as TableLayoutPanel;
            if (panel == null) return;

            Pen pen = new Pen(Color.Gray, 1); // Màu và độ dày của đường chia

            // Tính vị trí dòng (horizontal lines)
            int y = 0;
            for (int row = 0; row < panel.RowCount - 1; row++)
            {
                y += panel.GetRowHeights()[row];
                e.Graphics.DrawLine(pen, 0, y, panel.Width, y); // Vẽ đường ngang
            }

            // Tính vị trí cột (vertical lines)
            int x = 0;
            for (int col = 0; col < panel.ColumnCount - 1; col++)
            {
                x += panel.GetColumnWidths()[col];
                e.Graphics.DrawLine(pen, x, 0, x, panel.Height); // Vẽ đường dọc
            }
        }
    }
}
