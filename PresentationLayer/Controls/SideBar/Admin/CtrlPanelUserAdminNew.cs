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

namespace PresentationLayer.Controls.SideBar.Admin
{
    public partial class CtrlPanelUserAdminNew : UserControl
    {
        private UserDTO user;

        private List<UserDTO> users;

        private UserExtraInfoDTO currentUser;

        private readonly IUserRoleServices userRoleServices = new UserRoleServices();
        private readonly IDepartmentServices departmentServices = new DepartmentServices();
        private readonly IUserExtraInfoServices userExtraInfoServices = new UserExtraInfoServices();
        private readonly IUserServices userServices = new UserServices();

        private List<DepartmentDTO> departmentDTOs;
        private List<UserRoleDTO> userRoleDTOs;

        private bool isShowPassword;
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
                dgvItems.Columns.Add(new DataGridViewTextBoxColumn() { DataPropertyName = "UserRoleId", HeaderText = "UserRoleId", Width = 100 });
                dgvItems.Columns.Add(new DataGridViewTextBoxColumn() { DataPropertyName = "Email", HeaderText = "Email", Width = 100 });

                dgvItems.Rows[0].Selected = true;
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

        private void UpdateUserInfo()
        {
            // Validate user input first
            if (!ValidateUserInput())
            {
                return;
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
                    MessageBox.Show("User information updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    MessageBox.Show("Failed to update user information.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btUpdate_Click(object sender, EventArgs e)
        {
            UpdateUserInfo();
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pbAvatar_Click(object sender, EventArgs e)
        {

        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void splitContainer1_Paint(object sender, PaintEventArgs e)
        {
            SplitContainer s = sender as SplitContainer;
            if (s != null)
            {
                e.Graphics.FillRectangle(Brushes.LightGray, s.SplitterRectangle);
            }
        }
    }
}
    