using BusinessLayer.Services;
using BusinessLayer;
using DataLayer.Domain;
using DTOLayer.Models;
using PresentationLayer.AppContext;
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
using DTOLayer;
using Bunifu.UI.WinForms.Helpers.Transitions;
using DataLayer.EnumObjects;

namespace PresentationLayer.Controls.SideBar.Admin
{
    public partial class CtrlPanelUserAdminNew : UserControl
    {
        private readonly UserDTO user;

        private List<UserDTO> users;

        private UserExtraInfoDTO currentUser;

        private readonly IUserRoleServices userRoleServices = new UserRoleServices();
        private readonly IDepartmentServices departmentServices = new DepartmentServices();
        private readonly IUserExtraInfoServices userExtraInfoServices = new UserExtraInfoServices();
        private readonly IUserServices userServices = new UserServices();

        private List<DepartmentDTO> departmentDTOs;
        private List<UserRoleDTO> userRoleDTOs;

        private bool isShowPassword;
        private bool isCreating = false;

        public CtrlPanelUserAdminNew()
        {
            this.user = UserSession.Instance.User;
            InitializeComponent();

        }

        private void CtrlPanelUserAdminNew_Load(object sender, EventArgs e)
        {
            if (user == null)
            {
                MessageBox.Show("Cannot authenticate user.");
                return;
            }

            InitConTrols();
            try
            {
                LoadDepartments();
                LoadUserRoles();
                LoadUsers();
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Database error: " + ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }

        }

        private void InitConTrols()
        {
            this.Dock = DockStyle.Fill;

            dgvItems.AutoGenerateColumns = false;
            dgvItems.ReadOnly = true;
            dgvItems.MultiSelect = false;
            dgvItems.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvItems.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvItems.CellFormatting += DgvItemsCellFormatting;

            cbRole.DisplayMember = "Name";
            cbRole.ValueMember = "Id";
            cbDepartment.DisplayMember = "Name";
            cbDepartment.ValueMember = "Id";

            isShowPassword = false;
            tbPassword.PasswordChar = '*';
        }

        
        private void LoadDepartments()
        {
            departmentDTOs = departmentServices.GetAllDepartments("");

            if (departmentDTOs != null)
            {
                cbDepartment.DataSource = departmentDTOs;
            }
        }

        private void LoadUserRoles()
        {
            userRoleDTOs = userRoleServices.GetAllUserRoles("");
            if (userRoleDTOs != null)
            {
                cbRole.DataSource = userRoleDTOs;
            }
        }

        private void LoadUsers()
        {
            users = userServices.GetAllUsersInlcudeInActive("");
            if (users != null)
            {
                dgvItems.DataSource = users;
                dgvItems.Columns.Clear();
                dgvItems.Columns.Add(new DataGridViewTextBoxColumn() { DataPropertyName = "Id", HeaderText = "Id", Width = 50, Name = "Id" });
                dgvItems.Columns.Add(new DataGridViewTextBoxColumn() { DataPropertyName = "Username", HeaderText = "Username", Width = 200 });
                dgvItems.Columns.Add(new DataGridViewTextBoxColumn() { DataPropertyName = "FirstName", HeaderText = "FirstName", Width = 100 });
                dgvItems.Columns.Add(new DataGridViewTextBoxColumn() { DataPropertyName = "UserRoleId", HeaderText = "UserRoleId", Name = "UserRoleId", Width = 100 });
                dgvItems.Columns.Add(new DataGridViewTextBoxColumn() { DataPropertyName = "Email", HeaderText = "Email", Width = 100 });

                dgvItems.Rows[0].Selected = true;
            }

        }

        private void DgvItemsCellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvItems.Columns[e.ColumnIndex].Name == "UserRoleId" && e.Value is int userRoleId)
            {
                e.Value = UserRoleEnumExtensions.ToString(userRoleId);
                e.FormattingApplied = true;
            }
        }

        private void SetUserData()
        {
            tbUsername.Text = currentUser.Username;
            tbPassword.Text = currentUser.Password;
            tbEmail.Text = currentUser.Email;
            tbFirstname.Text = currentUser.FirstName;
            tbLastname.Text = currentUser.LastName;
            tbPhone.Text = currentUser.PhoneNumber;
            tbAddress.Text = currentUser.Address;

            cbDepartment.SelectedValue = currentUser.DepartmentId;
            cbRole.SelectedValue = currentUser.UserRoleId;

            cbIsActive.Checked = currentUser.IsActive == true ? true : false;
        }

        
        private void dgvItems_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvItems.SelectedRows.Count == 1 && dgvItems.SelectedRows[0].Cells["Id"].Value != null)
            {
                DataGridViewRow item = dgvItems.SelectedRows[0];

                try
                {
                    currentUser = userExtraInfoServices.GetUserByIdInlcudeInActive((int)item.Cells["Id"].Value);
                    SetUserData();
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Database error occurred while retrieving project details.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred while retrieving project details.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }

        private bool ValidateUserInput()
        {
            bool isValid = true;
            string errorMessage = "";

            // Required fields validation
            if (string.IsNullOrWhiteSpace(tbFirstname.Text))
            {
                errorMessage += "First name is required.\n";
                isValid = false;
            }

            if (string.IsNullOrWhiteSpace(tbLastname.Text))
            {
                errorMessage += "Last name is required.\n";
                isValid = false;
            }

            if (string.IsNullOrWhiteSpace(tbEmail.Text))
            {
                errorMessage += "Email is required.\n";
                isValid = false;
            }
            else if (!IsValidEmail(tbEmail.Text.Trim()))
            {
                errorMessage += "Please enter a valid email address.\n";
                isValid = false;
            }

            if (string.IsNullOrWhiteSpace(tbPhone.Text))
            {
                errorMessage += "Phone number is required.\n";
                isValid = false;
            }

            if (string.IsNullOrWhiteSpace(tbPassword.Text))
            {
                errorMessage += "Password is required.\n";
                isValid = false;
            }

            // Display all validation errors at once
            if (!isValid)
            {
                MessageBox.Show(errorMessage, "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            return isValid;
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        private bool UpdateUserInfo()
        {
            // Validate user input first
            if (!ValidateUserInput())
            {
                return false;
            }

            // Update the user object with form data
            currentUser.FirstName = tbFirstname.Text.Trim();
            currentUser.LastName = tbLastname.Text.Trim();
            currentUser.Address = tbAddress.Text.Trim();
            currentUser.Email = tbEmail.Text.Trim();
            currentUser.PhoneNumber = tbPhone.Text.Trim();
            currentUser.Password = tbPassword.Text;
            currentUser.DepartmentId = (int?)cbDepartment.SelectedValue;
            currentUser.UserRoleId = (int)cbRole.SelectedValue;
            currentUser.IsActive = cbIsActive.Checked;

            try
            {
                var res = userExtraInfoServices.UpdateUser(currentUser);
                if (res)
                {
                    MessageBox.Show("User information updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return true;
                }
                else
                {
                    MessageBox.Show("Failed to update user information.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return false;
        }

        private bool CreateUser()
        {
            UserExtraInfoDTO newUser = new UserExtraInfoDTO()
            {
                // Update the user object with form data
                Username = tbUsername.Text.Trim(),
                FirstName = tbFirstname.Text.Trim(),
                LastName = tbLastname.Text.Trim(),
                Address = tbAddress.Text.Trim(),
                Email = tbEmail.Text.Trim(),
                PhoneNumber = tbPhone.Text.Trim(),
                Password = tbPassword.Text,
                DepartmentId = (int?)cbDepartment.SelectedValue,
                UserRoleId = (int)cbRole.SelectedValue,
                IsActive = cbIsActive.Checked

            };

            try
            {
                var res = userExtraInfoServices.Createuser(newUser);
                if (res != null)
                {
                    MessageBox.Show("User created successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (isCreating)
            {
                bool res = CreateUser();

                if (res)
                {
                    isCreating = false;
                    tbUsername.Enabled = false;
                    LoadUsers();
                }
                else
                {
                    MessageBox.Show("Failed to create user.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                bool res = UpdateUserInfo();

                if (res)
                {
                    isCreating = false;
                    LoadUsers();
                    tbUsername.Enabled = false;
                }
            }

        }

        private void ResetControl()
        {
            tbUsername.Text = string.Empty;
            tbPassword.Text = string.Empty;
            tbEmail.Text = string.Empty;
            tbFirstname.Text = string.Empty;
            tbLastname.Text = string.Empty;
            tbPhone.Text = string.Empty;
            tbAddress.Text = string.Empty;

            cbDepartment.SelectedIndex = -1; 
            cbRole.SelectedIndex = -1;

            cbIsActive.Checked = false;
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            if (isCreating)
            {
                isCreating = false;
                tbUsername.Enabled = false;
                ResetControl();
                LoadUsers();
            }
            else
            {
                dgvItems.Rows[0].Selected = true;
            }
        }

        private void splitContainer1_Paint(object sender, PaintEventArgs e)
        {
            SplitContainer s = sender as SplitContainer;
            if (s != null)
            {
                e.Graphics.FillRectangle(Brushes.LightGray, s.SplitterRectangle);
            }
        }

        private void LoadUser(string kw)
        {
            try
            {
                ủe
            }
            catch (SqlException ex)
            {
                ShowMessage("Database error occurred while retrieving projects.", "Error", MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                ShowMessage("An error occurred while retrieving projects.", "Error", MessageBoxIcon.Error);
            }
        }

       

        private void btCreate_Click(object sender, EventArgs e)
        {
            if (!isCreating)
            {
                isCreating = true;
                tbUsername.Enabled = true;
                ResetControl();
            }
        }

        private void btnShowPassword_Click(object sender, EventArgs e)
        {
            if (isShowPassword)
            {
                tbPassword.PasswordChar = '*';
                btnShowPassword.Text = "Show Password";
                isShowPassword = false;
            }
            else
            {
                tbPassword.PasswordChar = '\0';
                btnShowPassword.Text = "Hide Password";
                isShowPassword = true;
            }
        }

        private void tbSearch_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    string keyword = string.IsNullOrEmpty(tbSearch.Text.Trim()) ==  true ? "" : tbSearch.Text.Trim();
                    
                    users = userServices.GetAllUsersInlcudeInActive(keyword);

                    if (users == null || users.Count == 0)
                    {
                        MessageBox.Show("Không tìm thấy task nào với từ khóa này.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    dgvItems.DataSource = users;
                }
                catch (SqlException ex)
                {
                    MessageBox.Show($"Lỗi cơ sở dữ liệu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }
    }
}
    