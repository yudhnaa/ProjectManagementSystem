using BusinessLayer.Services;
using BusinessLayer;
using DataLayer.Domain;
using DTOLayer.Models;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DTOLayer;

namespace PresentationLayer.CustomControls
{
    public partial class ctrlCreateUser : UserControl
    {
        public User user;

        private UserServices userServices;
        private DepartmentServices departmentServices;
        private UserRoleServices userRoleServices;
        //private UploadServices uploadServices;

        private List<DepartmentDTO> departmentDTOs;
        private List<UserRoleDTO> userRoleDTOs;

        private Timer debounceTimer;

        public ctrlCreateUser()
        {
            InitializeComponent();
        }

        private void ctrlCreateUser_Load(object sender, EventArgs e)
        {
            this.Dock = DockStyle.Fill;

            userServices = new UserServices();
            departmentServices = new DepartmentServices();
            userRoleServices = new UserRoleServices();
            
            cbRole.DisplayMember = "Name";
            cbRole.ValueMember = "Id";
            cbDepartment.DisplayMember = "Name";
            cbDepartment.ValueMember = "Id";

            loadDepartments();
            loadUserRoles();

        }

        public void loadDepartments()
        {
            departmentDTOs = departmentServices.GetAllDepartments();

            if (departmentDTOs != null)
            {
                cbDepartment.DataSource = departmentDTOs;
            }
        }

        public void loadUserRoles()
        {
            userRoleDTOs = userRoleServices.getAllUserRoles();
            if (userRoleDTOs != null)
            {
                cbRole.DataSource = userRoleDTOs;
            }
        }

        private bool ValidateInput()
        {
            bool isValid = true;
            string errorMessage = "";

            // Required fields validation
            if (string.IsNullOrWhiteSpace(tbUsername.Text))
            {
                errorMessage += "Username is required.\n";
                isValid = false;
            }

            if (string.IsNullOrWhiteSpace(tbPassword.Text))
            {
                errorMessage += "Password is required.\n";
                isValid = false;
            }
            else if (tbPassword.Text.Length < 6)
            {
                errorMessage += "Password must be at least 6 characters long.\n";
                isValid = false;
            }

            if (string.IsNullOrWhiteSpace(btEmail.Text))
            {
                errorMessage += "Email is required.\n";
                isValid = false;
            }
            else if (!IsValidEmail(btEmail.Text.Trim()))
            {
                errorMessage += "Please enter a valid email address.\n";
                isValid = false;
            }

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

            if (string.IsNullOrWhiteSpace(tbPhone.Text))
            {
                errorMessage += "Phone number is required.\n";
                isValid = false;
            }
            else if (!IsValidPhoneNumber(tbPhone.Text.Trim()))
            {
                errorMessage += "Please enter a valid phone number.\n";
                isValid = false;
            }

            if (cbRole.SelectedItem == null)
            {
                errorMessage += "User role is required.\n";
                isValid = false;
            }

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

        private bool IsValidPhoneNumber(string phoneNumber)
        {
            // Vietnamese phone number validation
            return System.Text.RegularExpressions.Regex.IsMatch(
                phoneNumber,
                @"^(03|05|07|08|09|01[2|6|8|9])([0-9]{8})\b$");
        }

        private void ClearForm()
        {
            // Clear all input fields
            tbUsername.Text = string.Empty;
            tbPassword.Text = string.Empty;
            btEmail.Text = string.Empty;
            tbFirstname.Text = string.Empty;
            tbLastname.Text = string.Empty;
            tbPhone.Text = string.Empty;
            tbAddress.Text = string.Empty;

            // Reset selections
            if (cbRole.Items.Count > 0)
                cbRole.SelectedIndex = 0;

            if (cbDepartment.Items.Count > 0)
                cbDepartment.SelectedIndex = 0;

            cbIsActive.Checked = true;
        }

        private void btCreate_Click(object sender, EventArgs e)
        {
            if (ValidateInput())
            {
                try
                {
                    // Create user DTO
                    UserDTO userDTO = new UserDTO
                    {
                        Username = tbUsername.Text.Trim(),
                        Password = tbPassword.Text, // Consider hashing the password
                        Email = btEmail.Text.Trim(),
                        FirstName = tbFirstname.Text.Trim(),
                        LastName = tbLastname.Text.Trim(),
                        PhoneNumber = tbPhone.Text.Trim(),
                        Address = tbAddress.Text.Trim(),
                        //Avatar = 
                        UserRoleId = ((UserRoleDTO) cbRole.SelectedItem).Id,
                        CreatedDate = DateTime.Now,
                    };

                    //Call service to create user
                   var result = userServices.createUser(userDTO);

                    if (result != null)
                    {
                       
                        MessageBox.Show("User created successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Clear form fields after successful creation
                        ClearForm();
                    }
                    else
                    {
                        MessageBox.Show("Failed to create user. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void cbIsActive_CheckedChanged(object sender, Bunifu.UI.WinForms.BunifuCheckBox.CheckedChangedEventArgs e)
        {

        }

        private void bunifuLabel15_Click(object sender, EventArgs e)
        {

        }
    }
}
