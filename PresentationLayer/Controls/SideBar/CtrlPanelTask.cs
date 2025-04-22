using BusinessLayer.Services;
using DTOLayer.Models;
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

        private void UC_Task_Load_1(object sender, EventArgs e)
        {
            this.Dock = DockStyle.Fill;
            //khởi tạo để gọi phương thức TaskStatusServices
            TaskStatusServices taskStatusServices = new TaskStatusServices();
            //gọi getTaskStatues để lấy trạng thái (task status) khi form được load lên
            List<TaskStatusDTO> TaskStatuses = taskStatusServices.getTaskStatuses();
            //hiển thị lên dropdown Status
            ddStatus.DataSource = TaskStatuses;
            //hiển thị tên của các trạng thái
            ddStatus.DisplayMember = "Name";
            //đánh id của các status
            ddStatus.ValueMember = "Id";

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

            }
            catch (Exception ex)
            {
                //trả ra thông báo lỗi không tìm thấy task khi search
                MessageBox.Show("Task not found "+ ex.Message);
            }
        }
    }
}
