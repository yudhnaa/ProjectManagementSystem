using BusinessLayer;
using BusinessLayer.Services;
using DTOLayer;
using DTOLayer.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PresentationLayer.Forms.Other
{
    public partial class FormUserInfoEdit : Form
    {
        private UserExtraInfoDTO user;

        private UserRoleServices userRoleServices;
        private DepartmentServices departmentServices;
        private UserExtraInfoServices userExtraInfoServices;

        private List<DepartmentDTO> departmentDTOs;
        private List<UserRoleDTO> userRoleDTOs;

        private bool isShowPassword;

        public FormUserInfoEdit(UserExtraInfoDTO user)
        {
            this.user = user;

            InitializeComponent();
        }

        private void FormUserInfoEdit_Load(object sender, EventArgs e)
        {
            if (user == null)
            {
                MessageBox.Show("User not found");
                return;
            }

            userRoleServices = new UserRoleServices();
            departmentServices = new DepartmentServices();
            userExtraInfoServices = new UserExtraInfoServices();

            cbRole.DisplayMember = "Name";
            cbRole.ValueMember = "Id";
            cbDepartment.DisplayMember = "Name";
            cbDepartment.ValueMember = "Id";

            isShowPassword = false;
            tbPassword.PasswordChar = '*';

            loadDepartments();
            loadUserRoles();

            setUserData();
        }

        private void loadDepartments()
        {
            departmentDTOs = departmentServices.GetAllDepartments();

            if (departmentDTOs != null)
            {
                cbDepartment.DataSource = departmentDTOs;
            }
        }

        private void loadUserRoles()
        {
            userRoleDTOs = userRoleServices.getAllUserRoles();
            if (userRoleDTOs != null)
            {
                cbRole.DataSource = userRoleDTOs;
            }
        }

        private void setUserData()
        {
            tbUsername.Text = user.Username;
            tbPassword.Text = user.Password;
            tbEmail.Text = user.Email;
            tbFirstname.Text = user.FirstName;
            tbLastname.Text = user.LastName;
            tbPhone.Text = user.PhoneNumber;
            tbAddress.Text = user.Address;

            cbDepartment.SelectedValue = user.DepartmentId;
            cbRole.SelectedValue = user.UserRoleId;

            cbIsActive.Checked = user.IsActive == true ? true : false;
        }

        private void UpdateUserInfo()
        {
            // Validate user input first
            if (!ValidateUserInput())
            {
                return; // Stop the update process if validation fails
            }

            // Update the user object with form data
            user.FirstName = tbFirstname.Text.Trim();
            user.LastName = tbLastname.Text.Trim();
            user.Address = tbAddress.Text.Trim();
            user.Email = tbEmail.Text.Trim();
            user.PhoneNumber = tbPhone.Text.Trim();
            user.Password = tbPassword.Text;
            user.DepartmentId = (int?)cbDepartment.SelectedValue;
            user.UserRoleId = (int)cbRole.SelectedValue;
            user.IsActive = cbIsActive.Checked;

            try
            {
                userExtraInfoServices.UpdateUser(user);
                MessageBox.Show("User information updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void btUpdate_Click(object sender, EventArgs e)
        {
            UpdateUserInfo();
        }

        private void btnShowPassword_Click(object sender, EventArgs e)
        {

            isShowPassword = !isShowPassword;

            if (isShowPassword == true)
            {
                btnShowPassword.Image = Properties.Resources.open_eye;
                tbPassword.PasswordChar = '\0';

            }
            else
            {
                btnShowPassword.Image = Properties.Resources.eye;
                tbPassword.PasswordChar = '*';
            }
        }
    }
}
