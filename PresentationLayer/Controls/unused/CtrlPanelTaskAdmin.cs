using BusinessLayer.Services;
using DataLayer.Domain;
using DTOLayer.Models;
using PresentationLayer.Controls;
using PresentationLayer.Forms.Other;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;
using System.Windows.Forms;

namespace PresentationLayer.UC_SideBar
{
    public partial class CtrlPanelTaskAdmin : System.Windows.Forms.UserControl
    {
        private List<TaskDTO> _tasks;

        private TaskStatusServices taskStatusServices;
        private System.Threading.Tasks.TaskStatus taskStatus;

        public List<TaskDTO> tasks
        {
            get => _tasks;
            set
            {
                _tasks = value;
                RefreshTaskList(); // Refresh the ListBox whenever tasks are set
            }
        }

        private void clearTaskList()
        {
            // Xóa các control trong hàng 2 -> end
            for (int row = tbGrid.RowCount - 1; row >= 2; row--) // lùi dần để tránh lỗi khi xóa
            {
                for (int col = 0; col < tbGrid.ColumnCount; col++)
                {
                    var control = tbGrid.GetControlFromPosition(col, row);
                    if (control != null)
                    {
                        tbGrid.Controls.Remove(control);
                        control.Dispose();
                    }
                }

                // Xóa RowStyle tương ứng nếu có
                if (tbGrid.RowStyles.Count > row)
                {
                    tbGrid.RowStyles.RemoveAt(row);
                }
            }
        }

        private void RefreshTaskList()
        {
            // Clear existing controls in the table layout panel
            clearTaskList();
            if (tasks == null)
                return;

            foreach (TaskDTO task in tasks)
            {
                CtrlTaskAdmin ctrlTaskAdmin = new CtrlTaskAdmin(task);

                tbGrid.RowCount++;
                tbGrid.Controls.Add(ctrlTaskAdmin, 0, tbGrid.RowCount - 1);
                tbGrid.RowStyles.Add(new RowStyle(SizeType.AutoSize));
                tbGrid.SetColumnSpan(ctrlTaskAdmin, 2);
            }

            // Add an empty row at the end
            tbGrid.RowCount++;
            tbGrid.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        }

        public CtrlPanelTaskAdmin()
        {
            InitializeComponent();
        }

        private void style()
        {
            tbGrid.AutoScroll = true;
            this.Dock = DockStyle.Fill;

            tbGrid.Margin = new Padding(10, 0, 0, 10);
            if (tbGrid.ColumnCount > 0)
            {
                tbGrid.ColumnStyles.Clear();
                tbGrid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 55F));
                tbGrid.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            }

            tbSearch.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right;
            drpdwnStatus.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right;
            btnSearch.Anchor = AnchorStyles.Right | AnchorStyles.Top;
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void UC_Task_Load_1(object sender, EventArgs e)
        {
            try
            {
                taskStatusServices = new TaskStatusServices();
                List<TaskStatusDTO> taskStatuses = taskStatusServices.GetAllTaskStatuses("");

                taskStatuses.Insert(0, new TaskStatusDTO
                {
                    Id = -1,        // hoặc giá trị không nằm trong bảng thực tế
                    Name = "Show All"
                });

                if (taskStatuses == null || taskStatuses.Count == 0)
                {
                    MessageBox.Show("No currentTask status found.");
                    style();
                    return;
                }

                //Them vao combobox
                drpdwnStatus.DataSource = taskStatuses;
                drpdwnStatus.DisplayMember = "Name";
                drpdwnStatus.ValueMember = "Id";

                //Muc dau tien lam mac dinh
                if (drpdwnStatus.Items.Count > 0)
                {
                    drpdwnStatus.SelectedIndex = 0;
                }
                style();
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                style();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                style();
            }
        }


        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                TaskServices taskServices = new TaskServices();
                string keyword = tbSearch.Text.Trim();
                int selectedStatus = Convert.ToInt32(drpdwnStatus.SelectedValue);

                if (selectedStatus == -1)
                {
                    // 👉 Nếu chọn "Tất cả", lấy toàn bộ task (có thể kèm từ khóa nếu muốn)
                    tasks = taskServices.GetAllTaskInlcudeInActive(""); // hoặc SearchAllTaskAdmin(keyword)
                }
                else
                {
                    //tasks = taskServices.SearchTaskAdmin(keyword, selectedStatus);
                }

                if (tasks == null || tasks.Count == 0)
                {
                    MessageBox.Show("Không tìm thấy currentTask nào với từ khóa này.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                RefreshTaskList();
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"Lỗi cơ sở dữ liệu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            FormTaskCreate formTaskCreate = new FormTaskCreate( 
            //new TaskDTO
            //{
            //    Id = 1,
            //    Code = "Test Task 01"
            //}, 
            new ProjectDTO
            {
                Id = 1,
                Name = "Test Project 01"
            });
            formTaskCreate.ShowDialog();
        }
    }
}
