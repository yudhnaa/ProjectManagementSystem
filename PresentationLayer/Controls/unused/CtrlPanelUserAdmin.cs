using BusinessLayer;
using BusinessLayer.Services;
using C1.Win.Localization.Design;
using DTOLayer;
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
    public partial class CtrlPanelUserAdmin : System.Windows.Forms.UserControl
    {
        private List<UserExtraInfoDTO> _users;
        private UserServices userServices;
        private readonly UserRoleServices userRoleServices;
        private readonly DepartmentServices departmentServices;
        public List<UserExtraInfoDTO> Users
        {
            get => _users;
            set
            {
                _users = value;
                RefreshUserList(); // Refresh the ListBox whenever tasks are set
            }
        }

        private void ClearTaskList()
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

        private void RefreshUserList()
        {
            // Clear existing controls in the table layout panel
            ClearTaskList();
            if (Users == null)
                return;

            foreach (UserExtraInfoDTO user in Users)
            {
               CtrlUserAdmin ctrlUser= new CtrlUserAdmin(user);

                tbGrid.RowCount++;
                tbGrid.Controls.Add(ctrlUser, 0, tbGrid.RowCount - 1);
                tbGrid.RowStyles.Add(new RowStyle(SizeType.AutoSize));
                tbGrid.SetColumnSpan(ctrlUser, 3);
            }

            // Add an empty row at the end
            tbGrid.RowCount++;
            tbGrid.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        }

        public CtrlPanelUserAdmin()
        {
            InitializeComponent();
        }

        private void Style()
        {
            tbGrid.AutoScroll = true;
            this.Dock = DockStyle.Fill;

            tbGrid.Margin = new Padding(10, 0, 0, 10);

            if (tbGrid.ColumnCount > 0)
            {
                tbGrid.ColumnStyles.Clear();
                tbGrid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40F));
                tbGrid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
                tbGrid.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            }

            tbSearch.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right;

            drpdwnRole.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right;

            drpdwnDepartment.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right;

            btnSearch.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            btnAdd.Anchor = AnchorStyles.Right | AnchorStyles.Top;
        }

        private void UC_Task_Load_1(object sender, EventArgs e)
        {
            try
            {
                UserRoleServices userServices = new UserRoleServices();
                //List<UserRoleDTO> users = userServices.GetAllUserRoles();
                
                //DepartmentServices departmentServices = new DepartmentServices(); 
                //List<DepartmentDTO> departments = departmentServices.GetAllDepartments();

                //if (users == null || users.Count == 0|| departments==null||departments.Count == 0)
                //{
                //    MessageBox.Show("No currentTask status found.");
                //    Style();
                //    return;
                //}

                //Them vao combobox
                //drpdwnRole.DataSource = users;
                drpdwnRole.DisplayMember = "Name";
                drpdwnRole.ValueMember = "Id";

                //drpdwnDepartment.DataSource = departments;
                drpdwnDepartment.DisplayMember = "Name";
                drpdwnDepartment.ValueMember = "Id";

                //Muc dau tien lam mac dinh
                if (drpdwnRole.Items.Count > 0)
                {
                    drpdwnRole.SelectedIndex = 0;
                }
                Style();
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                Style();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                Style();
            }
        }

        private void TableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                UserExtraInfoServices userServices = new UserExtraInfoServices();
                string keyword = tbSearch.Text.Trim();
                int selectedRole = Convert.ToInt32(drpdwnRole.SelectedValue);
                int selectedDepartment = Convert.ToInt32(drpdwnDepartment.SelectedValue);

                 //Users = userServices.SearchUsersAdmin(keyword, selectedRole, selectedDepartment);
                if (Users == null || Users.Count == 0)
                {
                    MessageBox.Show("Không tìm thấy người dùng nào với những lựa chọn này.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                else
                {
                    RefreshUserList();
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

        private void btnAdd_Click(object sender, EventArgs e)
        {
            FormUserCreate formUserCreate = new FormUserCreate();
            formUserCreate.ShowDialog();
        }
    }
}
