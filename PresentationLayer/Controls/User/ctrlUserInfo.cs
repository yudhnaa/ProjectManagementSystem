using BusinessLayer;
using BusinessLayer.Services;
using DTOLayer.Models;
using PresentationLayer.AppContext;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PresentationLayer.CustomControls
{
    public partial class ctrlUserInfo : UserControl
    {
        private UserDTO user;

        private bool isShowPassword;

        private UserServices userServices;

        public ctrlUserInfo()
        {
            this.user = UserSession.Instance.User;

            InitializeComponent();


        }

        private void ctrlUserInfo_Load(object sender, EventArgs e)
        {
            isShowPassword = false;
            tbPassword.PasswordChar = '*';

            this.Dock = DockStyle.Fill;

            setUserInfo();

            userServices = new UserServices();
        }

        private void setUserInfo()
        {
            //if (user.Avatar != null)
            //{
            //    picAvatar.Image = Image.FromFile(user.Avatar);
            //}

            tbUsername.Text = user.Username;
            tbFirstname.Text = user.FirstName;
            tbLastname.Text = user.LastName;
            tbAddress.Text = user.Address;
            tbEmail.Text = user.Email;
            tbPhone.Text = user.PhoneNumber;
            tbPassword.Text = user.Password;

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

        private void btCancel_Click(object sender, EventArgs e)
        {
            this.Dispose();
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

            try
            {
                userServices.UpdateUser(user);
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

        private void btCreate_Click(object sender, EventArgs e)
        {
            UpdateUserInfo();
        }
    }
}
