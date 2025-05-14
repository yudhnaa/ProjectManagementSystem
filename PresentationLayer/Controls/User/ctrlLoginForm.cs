using BusinessLayer;
using BusinessLayer.Services;
using DTOLayer;
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
    public partial class ctrlLoginForm : UserControl
    {
        public ctrlSignUpForm ctrlSignUp { get; set; }
        public Form frmLogin { get; set; }

        private bool isPasswordVisible = false;

        public ctrlLoginForm() : this(null) { }

        public ctrlLoginForm(ctrlSignUpForm ctrlSignUp)
        {
            InitializeComponent();
            this.ctrlSignUp = ctrlSignUp;
            tbPassword.PasswordChar = '*';
        }

        private UserDTO AuthenticateUser(string username, string password)
        {
            var userServices = new UserServices();
            return userServices.CheckLoginUser(new UserDTO
            {
                Username = username,
                Password = password
            });
        }


        private bool ValidateInputs(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                MessageBox.Show("Username cannot be empty.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tbUsername.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Password cannot be empty.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tbPassword.Focus();
                return false;
            }

            return true;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = tbUsername.Text.Trim();
            string password = tbPassword.Text;

            if (!ValidateInputs(username, password))
                return;

            var user = AuthenticateUser(username, password);

            if (user != null)
            {
                var role = new UserRoleServices().GetUserRoleById(user.UserRoleId);
                UserSession.Instance.SetUser(user, role);

                Form homeForm = role.Name switch
                {
                    "Admin" => new FormAdminHome(),
                    "Employee" or "Manager" => new FormUserHome(),
                    _ => null
                };

                if (homeForm != null)
                {
                    ResetFields();
                    frmLogin.Hide();
                    FormContext.Instance.SetHomeForms(homeForm);
                    homeForm.Show();
                }
            }
            else
            {
                if (MessageBox.Show("Try Again?", "Login Failed", MessageBoxButtons.RetryCancel, MessageBoxIcon.Warning) == DialogResult.Retry)
                {
                    ResetFields();
                }
            }
        }

        private void ResetFields()
        {
            tbUsername.Clear();
            tbPassword.Clear();
            tbUsername.Focus();
        }

        private void btnShowPassword_Click(object sender, EventArgs e)
        {
            isPasswordVisible = !isPasswordVisible;
            tbPassword.PasswordChar = isPasswordVisible ? '\0' : '*';
            btnShowPassword.Image = isPasswordVisible ? Properties.Resources.open_eye : Properties.Resources.eye;
        }

        private void btnSignUpp_Click(object sender, EventArgs e)
        {
            this.Hide();
            ctrlSignUp?.Show();
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void bunifuLabel4_Click(object sender, EventArgs e)
        {

        }

        private void bunifuLabel3_Click(object sender, EventArgs e)
        {

        }

        private void tbPassword_TextChanged(object sender, EventArgs e)
        {

        }

        private void tbUsername_TextChanged(object sender, EventArgs e)
        {

        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }

}
