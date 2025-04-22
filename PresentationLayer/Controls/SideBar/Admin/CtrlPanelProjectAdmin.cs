using BusinessLayer.Services;
using BusinessLayer;
using DTOLayer.Models;
using PresentationLayer.AppContext;
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
using System.Windows.Forms.DataVisualization.Charting;
using C1.Framework;

namespace PresentationLayer.Controls.Project
{
    public partial class CtrlPanelProjectAdmin : UserControl
    {
        private UserDTO user;

        private List<ProjectDTO> _projects;

        private ProjectDTO project;

        private ProjectStatusServices projectStatusServices;
        private ProjectServices projectServices;

        public List<ProjectDTO> projects
        {
            get => _projects;
            set
            {
                _projects = value;
                RefreshProjectList(); // Refresh the ListBox whenever tasks are set
            }
        }

        public CtrlPanelProjectAdmin()
        {
            this.user = UserSession.Instance.User;

            InitializeComponent();

            this.Dock = DockStyle.Fill;
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

        private void RefreshProjectList()
        {
            // Clear existing controls in the table layout panel
            clearTaskList();
            foreach (ProjectDTO project in projects)
            {
                CtrlProjectAdmin ctrlProjectAdmin = new CtrlProjectAdmin(project);

                tbGrid.RowCount++;
                tbGrid.Controls.Add(ctrlProjectAdmin, 0, tbGrid.RowCount - 1);
                tbGrid.RowStyles.Add(new RowStyle(SizeType.AutoSize));
                tbGrid.SetColumnSpan(ctrlProjectAdmin, 2);
            }

            // Add an empty row at the end
            tbGrid.RowCount++;
            tbGrid.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        }

        private void style()
        {
            this.Dock = DockStyle.Fill;

            tbGrid.Margin = new Padding(10, 0, 0, 10);
            if (tbGrid.ColumnCount > 0)
            {
                tbGrid.ColumnStyles.Clear();
                tbGrid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60F));
                tbGrid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40F));
            }

            tbSearch.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right;

            drpdwnStatus.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right;
            btnSearch.Anchor = AnchorStyles.Right | AnchorStyles.Top;
        }

        private void CtrlPanelProject_Load(object sender, EventArgs e)
        {
            //Load project status --> Bo vao 
            try
            {
                projectStatusServices = new ProjectStatusServices();
                List<ProjectStatusDTO> projectStatuses = projectStatusServices.GetAllProjectStatuses();
                
                //thêm thuộc tính ShowAll
                projectStatuses.Insert(0, new ProjectStatusDTO
                {
                    Id = -1,        // hoặc giá trị không nằm trong bảng thực tế
                    Name = "Show All"
                });
                
                if (projectStatuses == null || projectStatuses.Count == 0)
                {
                    MessageBox.Show("No project status found.");
                    style();
                    return;
                }

                //Them vao combobox
                drpdwnStatus.DataSource = projectStatuses;
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
            //Goi ProjectServices de lay du lieu
            try
            {
                projectServices = new ProjectServices();
                int selectedStatus = Convert.ToInt32(drpdwnStatus.SelectedValue);

                if (selectedStatus == -1)
                {
                    // Nếu chọn "Tất cả", gọi GetAllProjectsAdmin
                    projects = projectServices.GetAllProjectsIncludeDeleteCancel();
                }
                else
                {
                    // Nếu chọn trạng thái cụ thể
                    string keyword = tbSearch.Text.Trim();
                    projects = projectServices.SearchProjectAdmin(keyword, selectedStatus);
                }
                if (projects == null || projects.Count == 0)
                {
                    MessageBox.Show("Không tìm thấy project nào với từ khóa này.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                RefreshProjectList();
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

        private void btnShowAll_Click(object sender, EventArgs e)
        {
            try
            {
                projectServices = new ProjectServices();

                // Lấy toàn bộ danh sách dự án
                projects = projectServices.GetAllProjectsIncludeDeleteCancel();

                if (projects == null || projects.Count == 0)
                {
                    MessageBox.Show("Không có dự án nào.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // Hiển thị danh sách dự án
                RefreshProjectList();

                // Xóa các điều kiện tìm kiếm trước đó
                tbSearch.Text = "";
                if (drpdwnStatus.Items.Count > 0)
                {
                    drpdwnStatus.SelectedIndex = 0;
                }
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
    }
}
