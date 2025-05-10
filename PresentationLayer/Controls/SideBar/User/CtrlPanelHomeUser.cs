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
        private readonly IUserServices userServices= new UserServices();
        private readonly ITaskServices taskServices= new TaskServices();
        public CtrlPanelHomeUser()
        {
            user = UserSession.Instance.User;
            InitializeComponent();
            
        }

        private void CtrlPanelHomeUser_Load(object sender, EventArgs e)
        {
            InitControl();
        }

        private void InitControl()
        {
            this.Dock = DockStyle.Fill;
            
            GenerateChartData2();
            GenerateChartData3();
            SetDataChartCompletedOfDay(user.Id);

            int marginH = (int)(tableLayoutPanel1.Width / 4 * 0.4);
            int marginV = (int)(tableLayoutPanel1.Height / 4 * 0.4);


            chart2.Margin = new Padding(marginH, marginV, marginH, marginV);
            chart3.Margin = new Padding(marginH, marginV, marginH, marginV);
            chartCompletedTasks.Margin = new Padding(marginH, marginV, marginH, marginV);

        }

        private void GenerateChartData2()
        {
            // Clear existing series
            chart2.Series.Clear();

            // Create a new series
            Series series = new Series("Sample Data");
            series.ChartType = SeriesChartType.Column;

            // Generate sample data
            Random rnd = new Random();
            for (int i = 1; i <= 10; i++)
            {
                int yValue = rnd.Next(10, 100); // Random value between 10 and 100
                series.Points.AddXY(i, yValue);
            }

            // Add series to chart
            chart2.Series.Add(series);

            // SetDataChartCompletedOfDay chart titles (optional)
            chart2.Titles.Add("Sample Chart");
            chart2.ChartAreas[0].AxisX.Title = "X Axis";
            chart2.ChartAreas[0].AxisY.Title = "Y Axis";
        }

        private void GenerateChartData3()
        {
            // Clear existing series
            chart3.Series.Clear();

            // Create a new series
            Series series = new Series("Sample Data");
            series.ChartType = SeriesChartType.Doughnut;

            // Generate sample data
            Random rnd = new Random();
            for (int i = 1; i <= 10; i++)
            {
                int yValue = rnd.Next(10, 100); // Random value between 10 and 100
                series.Points.AddXY(i, yValue);
            }

            // Add series to chart
            chart3.Series.Add(series);

            // SetDataChartCompletedOfDay chart titles (optional)
            chart3.Titles.Add("Sample Chart");
            chart3.ChartAreas[0].AxisX.Title = "X Axis";
            chart3.ChartAreas[0].AxisY.Title = "Y Axis";
        }

        private void SetDataChartCompletedOfDay(int userId)
        {
            var taskServices = new TaskServices();
            var data = taskServices.GetCompletedTaskByDate(userId);
            // Clear existing series
            chartCompletedTasks.Series.Clear();

            Series series1 = new Series
            {
                Name = "Số Task đã hoàn thành",
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
            chartCompletedTasks.Titles.Add("Task Hoàn thành theo ngày");
            chartCompletedTasks.ChartAreas[0].AxisX.Title = "Ngày";
            chartCompletedTasks.ChartAreas[0].AxisY.Title = "Số Task hoàn thành";
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
