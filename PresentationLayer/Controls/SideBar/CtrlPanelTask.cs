using BusinessLayer.Services;
using DataLayer.Domain;
using DTOLayer.Models;
using LiveCharts.Wpf.Components;
using PresentationLayer.AppContext;
using PresentationLayer.Config;
using PresentationLayer.Control;
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
    public partial class CtrlPanelTask : System.Windows.Forms.UserControl
    {
        private List<TaskDTO> _tasks;

        public List<TaskDTO> tasks
        {
            // biến dùng chung khai báo để lưu danh sách các task của 1 project
            get => _tasks;
            set
            {
                _tasks = value;
                RefreshTaskList(); // Refresh the ListBox whenever tasks are set
            }
        }
        private ProjectDTO _selectedProject;
        private TaskStatusServices taskStatusServices;
        private List<TaskStatusDTO> taskStatuses;

        public ProjectDTO selectedProject
        { 
            //tạo đối tượng để truy xuất project đang được chọn
            get => _selectedProject;
            set
            {
                _selectedProject = value;
              
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
               CtrlTask ctrlTask = new CtrlTask(task);

                tbGrid.RowCount++;
                tbGrid.Controls.Add(ctrlTask, 0, tbGrid.RowCount - 1);
                tbGrid.RowStyles.Add(new RowStyle(SizeType.AutoSize));
                tbGrid.SetColumnSpan(ctrlTask, 2);
            }
        }

        public CtrlPanelTask(ProjectDTO selectedProject)
        {
            InitializeComponent();
            //project alpha thì chỉ alpha thấy được những task của project alpha và không thấy task của người khác
            this.selectedProject = selectedProject;


        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void LoadTaskStatuses()
        {
            //loadTaskStatus(); --> Bo vao combobox
            taskStatuses = taskStatusServices.getTaskStatuses();

            // Tạo 1 đối tượng TaskStatusDTO giả để chứa cái status "All" (Không lưu xuống database)
            // cho tiện thêm vào dropdown
            TaskStatusDTO taskStatusAll = new TaskStatusDTO();
            taskStatusAll.Id = 0;
            taskStatusAll.Name = "All";

            taskStatuses.Add(taskStatusAll);
            ddStatus.DataSource = taskStatuses;
        }

        private void Style()
        {
            this.Dock = DockStyle.Fill;

            //hiển thị tên của các trạng thái
            ddStatus.DisplayMember = "Name";
            //đánh id của các status
            ddStatus.ValueMember = "Id";
        }

        private void InitService()
        {
            taskStatusServices = new TaskStatusServices();
        }
      
        private void UC_Task_Load_1(object sender, EventArgs e)
        {
            // Thường thì tách riêng ra thành cách hàm riêng như này sẽ dễ đọc code hơn, mỗi cái 1 nhiệm vụ
            InitService();
            Style();
            LoadTaskStatuses();
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void bunifuTextBox1_TextChanged(object sender, EventArgs e)
        {

        }


        private void btSearch_Click(object sender, EventArgs e)
        {
            try
            {
                //lấy từ khóa nhập vào search
                string KeyWordSearch = tbSearch.Text;

                // lấy id là trạng thái (status) của task đó
                int Id = (int)ddStatus.SelectedValue;

                //lấy userID được khai báo ở UserSession
                int userId = UserSession.Instance.User.Id;

                TaskServices taskServices = new TaskServices();
                /* taks được khai báo toàn cục 
                 * - lấy các biến keyword,  id (trạng thái -status), 
                 * PageSize được khai báo biến global về số task hiện trên list task
                 * userId và ProjectID là để phân biệt các project nào thì chỉ được xem các task của project đó */
                tasks = taskServices.GetTaskByKwAndStatus(KeyWordSearch, Id, GlobalVariables.PageSize, userId, selectedProject.Id);

                //load lại list Task
                RefreshTaskList();
            }
            catch (SqlException sqlEx)
            {
                // Log the SQL exception
                MessageBox.Show("Database error occurred while fetching tasks "+ sqlEx.Message);
                tasks = null;
                clearTaskList();

            }
            catch (Exception ex)
            {
                //trả ra thông báo lỗi không tìm thấy task khi search
                MessageBox.Show("Task not found "+ ex.Message);
                tasks = null;
                clearTaskList();
            }
        }
    }
}
