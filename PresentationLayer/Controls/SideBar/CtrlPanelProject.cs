using BusinessLayer;
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
using System.Windows.Forms;

namespace PresentationLayer.Controls.Project
{
    public partial class CtrlPanelProject : UserControl
    {
        private List<ProjectDTO> _projects;

        public List<ProjectDTO> projects
        {
            get => _projects;
            set
            {
                _projects = value;
                RefreshTaskList(); // Refresh the ListBox whenever tasks are set
            }
        }
        // Biến có thể dùng ở nhiều chỗ thì sẽ khai báo toàn cục cho tiện truy cập, mà nhớ để private,
        // khi nào có mục đích rõ ràng hãy mở public ra
        private ProjectStatusServices projectStatusServices;
        private List<taskStatusDTO> projectStatuses;

        public CtrlPanelProject()
        {
            InitializeComponent();
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
            if (projects != null)
            {
                foreach (ProjectDTO project in projects)
                {
                    CtrlProject ctrlProject = new CtrlProject(project);

                    tbGrid.RowCount++;
                    tbGrid.Controls.Add(ctrlProject, 0, tbGrid.RowCount - 1);
                    tbGrid.RowStyles.Add(new RowStyle(SizeType.AutoSize));
                    tbGrid.SetColumnSpan(ctrlProject, 2);
                }
            }
        }

        private void LoadProjectStatuses()
        {
            //loadProjectStatus(); --> Bo vao combobox
            projectStatuses = projectStatusServices.GetAllProjectStatuses();

            // Tạo 1 đối tượng ProjectStatusDTO giả để chứa cái status "All" (Không lưu xuống database)
            // cho tiện thêm vào dropdown
            taskStatusDTO projectStatusAll = new taskStatusDTO();
            projectStatusAll.Id = 0;
            projectStatusAll.Name = "All";

            projectStatuses.Add(projectStatusAll);

            ddProjectStatus.DataSource = projectStatuses;
        }

        private void Style()
        {
            this.Dock = DockStyle.Fill;

            ddProjectStatus.DisplayMember = "Name";
            ddProjectStatus.ValueMember = "Id";
        }

        private void InitService()
        {
            projectStatusServices = new ProjectStatusServices();
        }
        private void CtrlPanelProject_Load(object sender, EventArgs e)
        {
            // Thường thì tách riêng ra thành cách hàm riêng như này sẽ dễ đọc code hơn, mỗi cái 1 nhiệm vụ
            InitService();
            Style();
            LoadProjectStatuses();   
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            // Goi ProjectServices de lay du lieu

            //--> Hien thi danh sach len 
            try
            {
                //keyword là từ khóa nhập vào ô tìm kiếm 
                string KeyWordSearchProject = tbProjectSearch.Text;
                //lấy trạng thái của project
                int statusId = (int)ddProjectStatus.SelectedValue;

                ProjectServices projectServices = new ProjectServices();
                //trong getProjectByKwAndStatus thì gọi các biến kw, statusId, v(số project hiển thị trên list)
                projects = projectServices.GetProjectsByKwAndStatus(KeyWordSearchProject, statusId, GlobalVariables.PageSize);
            //--> khúc này nếu mà chọn status là All thì truyền vào statusId là "0"

                //load lại list hiển thị danh sách project
                RefreshTaskList();
            }
            catch (SqlException sqlEx)
            {
                // Log the SQL exception
                MessageBox.Show("Database error occurred while fetching project " + sqlEx.Message);
                projects = null;
                clearTaskList();

            }
            catch (Exception ex)
            {
                //Thông báo về lỗi không tìm thấy project avf thông điệp lỗi
                MessageBox.Show("Project not found " + ex.Message);
                projects = null;
                clearTaskList();
            }
        }


    }
}
