using BusinessLayer;
using BusinessLayer.Services;
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

namespace PresentationLayer.CustomControls
{
    public partial class ctrlSignUpForm : UserControl
    {
        public UserControl ctrlLogin { get; set; }
        private bool isPasswordVisible = false;

        public ctrlSignUpForm() : this(null) { }

        public ctrlSignUpForm(UserControl ctrlLogin)
        {
            InitializeComponent();
            this.ctrlLogin = ctrlLogin;
            tbSignUpPassword.PasswordChar = '*';
            btnShowPassword.Image = Properties.Resources.eye;
            this.Hide();
        }

        private void btnSignUpBack_Click(object sender, EventArgs e)
        {
            this.Hide();
            ctrlLogin?.Show();
        }

        private void btnShowPassword_Click(object sender, EventArgs e)
        {
            isPasswordVisible = !isPasswordVisible;
            tbSignUpPassword.PasswordChar = isPasswordVisible ? '\0' : '*';
            btnShowPassword.Image = isPasswordVisible ? Properties.Resources.open_eye : Properties.Resources.eye;
        }

        private void btnSignUp_Click(object sender, EventArgs e)
        {
            var userDTO = new UserDTO
            {
                FirstName = tbSignUpFirstName.Text,
                LastName = tbSignUpLastName.Text,
                Username = tbSignUpUsername.Text,
                Password = tbSignUpPassword.Text,
                Email = tbSignUpEmail.Text
            };

            var userServices = new UserServices();
            try
            {
                userServices.CreateUser(userDTO);
                MessageBox.Show("User created successfully!");
                this.Hide();
                ctrlLogin?.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
    }
}
