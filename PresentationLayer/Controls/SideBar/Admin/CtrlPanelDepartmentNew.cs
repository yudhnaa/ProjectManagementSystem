using Bunifu.UI.WinForms.Helpers.Transitions;
using BusinessLayer.Services;
using DataLayer.Domain;
using DTOLayer;
using DTOLayer.Models;
using PresentationLayer.AppContext;
using PresentationLayer.Config;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace PresentationLayer.Controls.SideBar.Admin
{
    public partial class CtrlPanelDepartmentNew : UserControl
    {
        private readonly UserDTO user;

        private readonly IDepartmentServices departmentServices = new DepartmentServices();
        private readonly IUserServices userSerices = new UserServices();

        private List<DepartmentDTO> departmentes;
        private DepartmentDTO currentItem;

        Dictionary<int, UserDTO> userCache = new();
        BindingList<UserDTO> managerUser = new();

        public CtrlPanelDepartmentNew()
        {
            InitializeComponent();
            user = UserSession.Instance.User;
        }

        private void CtrlPanelDepartmentNew_Load(object sender, EventArgs e)
        {
            InitControl();
            try
            {
                LoadDepartments();
            }
            catch (SqlException ex)
            {
                ShowError("Database error while loading departments", ex);
            }
            catch (Exception ex)
            {
                ShowError("Unexpected error while loading departments", ex);
            }
        }

        private void InitControl()
        {
            this.Dock = DockStyle.Fill;
            this.DoubleBuffered = true;

            cbManagerUser.DisplayMember = "Username";
            cbManagerUser.ValueMember = "Id";
            cbManagerUser.DataSource = managerUser;

            dgvItems.AutoGenerateColumns = false;
            dgvItems.ReadOnly = true;
            dgvItems.MultiSelect = false;
            dgvItems.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvItems.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvItems.Columns.Clear();

            dgvItems.Columns.AddRange(
                new DataGridViewTextBoxColumn() { DataPropertyName = "Id", HeaderText = "Id", Name = "Id", Width = 20, ReadOnly = true },
                new DataGridViewTextBoxColumn() { DataPropertyName = "Name", HeaderText = "Name", Name = "Name", Width = 30 },
                new DataGridViewTextBoxColumn() { DataPropertyName = "ManagerId", HeaderText = "Manager Id", Name = "ManagerId", Width = 20, ReadOnly = true },
                new DataGridViewTextBoxColumn() { DataPropertyName = "IsActive", HeaderText = "Active", Name = "IsActive", Width = 10, ReadOnly = true },
                new DataGridViewTextBoxColumn() { DataPropertyName = "Description", HeaderText = "Description", Name = "Description", Width = 100 }
            );
        }

        private void LoadDepartments(string keyword = "")
        {
            
            departmentes = departmentServices.GetAllDepartmentsInlcudeInactive(keyword);
            dgvItems.DataSource = departmentes;
            
        }

        private void SetSelectedItemData()
        {
            
            if (currentItem == null)
            {
                managerUser.Clear();
                return;
            }

            tbId.Text = currentItem.Id.ToString();
            tbName.Text = currentItem.Name;
            tbDescription.Text = currentItem.Description;
            cbIsActive.Checked = currentItem.IsActive ?? false;

            if (userCache.ContainsKey(currentItem.ManagerId ?? 0))
            {
                managerUser.Add(userCache[currentItem.ManagerId ?? 0]);
                cbManagerUser.SelectedItem = userCache[currentItem.ManagerId ?? 0];
            }
            else
            {
                if (currentItem.ManagerId == null)
                {
                    managerUser.Clear();
                    return;
                }

                try
                {
                    var manager = userSerices.GetUserById(currentItem.ManagerId ?? 0);
                    if (manager != null)
                    {
                        userCache[currentItem.ManagerId ?? 0] = manager;
                        managerUser.Add(manager);
                        cbManagerUser.SelectedItem = userCache[currentItem.ManagerId ?? 0];
                    }
                }
                catch (SqlException ex)
                {
                    ShowError("Database error while loading manager", ex);
                }
                catch (Exception ex)
                {
                    ShowError("Unexpected error while loading manager", ex);
                }
            }
        }

        private void dgvItems_SelectionChanged(object sender, EventArgs e)
        {
            
            if (dgvItems.SelectedRows.Count > 0 && currentItem != dgvItems.SelectedRows[0].DataBoundItem as DepartmentDTO)
            {
                currentItem = dgvItems.SelectedRows[0].DataBoundItem as DepartmentDTO;
                SetSelectedItemData();
            }
        }

        private void btCreateProject_Click(object sender, EventArgs e)
        {
            tbId.Text = "0";
            tbName.Clear();
            tbDescription.Clear();
            managerUser.Clear();
            cbIsActive.Checked = false;
            
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(tbName.Text))
            {
                MessageBox.Show("Department name is required.");
                return false;
            }
            if (string.IsNullOrWhiteSpace(tbDescription.Text))
            {
                MessageBox.Show("Department description is required.");
                return false;
            }
            if (cbManagerUser.SelectedValue == null)
            {
                MessageBox.Show("Manager ID is required.");
                return false;
            }
            return true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateInput()) return;

            bool isSuccess = false;
            if (tbId.Text == "0")
            {
                if (((UserDTO)cbManagerUser.SelectedItem) != null)
                {
                    var newDepartment = new DepartmentDTO
                    {
                        Name = tbName.Text,
                        Description = tbDescription.Text,
                        ManagerId = ((UserDTO)cbManagerUser.SelectedItem)?.Id,
                        IsActive = cbIsActive.Checked
                    };
                    isSuccess = departmentServices.CreateDepartment(newDepartment);
                }
            }
            else
            {
                currentItem.Name = tbName.Text;
                currentItem.Description = tbDescription.Text;
                currentItem.ManagerId = ((UserDTO)cbManagerUser.SelectedItem)?.Id ?? currentItem.ManagerId;
                currentItem.IsActive = cbIsActive.Checked;
                isSuccess = departmentServices.UpdateDepartment(currentItem);
            }

            MessageBox.Show(isSuccess ? "Department saved successfully." : "Failed to save department.");
            if (isSuccess) LoadDepartments();
        }

        private void tbSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    string keyword = tbSearch.Text.Trim();
                    LoadDepartments(string.IsNullOrEmpty(keyword) ? "" : keyword);
                }
                catch (SqlException ex)
                {
                    ShowError("Database error during search", ex);
                }
                catch (Exception ex)
                {
                    ShowError("Unexpected error during search", ex);
                }

                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void splitContainer1_Paint(object sender, PaintEventArgs e)
        {
            if (sender is SplitContainer s)
                e.Graphics.FillRectangle(Brushes.LightGray, s.SplitterRectangle);
        }

        private void ShowError(string message, Exception ex)
        {
            MessageBox.Show($"{message}: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void cbManagerUser_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    string keyword = cbManagerUser.Text.Trim();
                    var users = userSerices.GetAllUsers(keyword);

                    managerUser.Clear();
                    foreach (var user in users)
                    {
                        if (!userCache.ContainsKey(user.Id))
                        {
                            userCache[user.Id] = user;
                        }
                        managerUser.Add(user);
                    }

                    cbManagerUser.DisplayMember = "Username";
                    cbManagerUser.ValueMember = "Id";
                    cbManagerUser.SelectedIndex = -1;
                }
                catch (SqlException ex)
                {
                    ShowError("Database error while loading managerUsers", ex);
                }
                catch (Exception ex)
                {
                    ShowError("Unexpected error while loading managerUsers", ex);
                }
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }
        
         private void LoadLocation(int x, int y)
        {
            dgvItems.Location = new Point(x, y); // Thay x, y bằng giá trị bạn muốn
            dgvItems.Left = x; // Hoặc chỉnh riêng lẻ
            dgvItems.Top = y;
        }
        
        private void dgvItems_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void splitContainer1_SplitterMoved(object sender, SplitterEventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
