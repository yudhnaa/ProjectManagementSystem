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
        private bool isShowPassword;

        public ctrlSignUpForm()
        {
            InitializeComponent();
            isShowPassword = false;
            this.Hide();
            btnShowPassword.Image = Properties.Resources.eye;
            tbSignUpPassword.PasswordChar = '*';
        }

        public ctrlSignUpForm(UserControl ctrlLogin)
        {
            InitializeComponent();
            this.ctrlLogin = ctrlLogin;
            isShowPassword = false;
            this.Hide();
            btnShowPassword.Image = Properties.Resources.eye;
            tbSignUpPassword.PasswordChar = '*';
        }

        private void btnSignUpBack_Click(object sender, EventArgs e)
        {
            this.Hide();
            ctrlLogin.Show();
        }

        private void ctrlSignUp_Load(object sender, EventArgs e)
        {

        }

        private void btnShowPassword_Click(object sender, EventArgs e)
        {
            isShowPassword = !isShowPassword;

            if (isShowPassword == true)
            {

                btnShowPassword.Image = Properties.Resources.open_eye;
                tbSignUpPassword.PasswordChar = '\0';

            }
            else
            {
                btnShowPassword.Image = Properties.Resources.eye;
                tbSignUpPassword.PasswordChar = '*';
            }
        }

        private void btnSignUp_Click(object sender, EventArgs e)
        {
            UserDTO userDTO = new UserDTO
            {
                FirstName = tbSignUpFirstName.Text,
                LastName = tbSignUpLastName.Text,
                Username = tbSignUpUsername.Text,
                Password = tbSignUpPassword.Text,
                Email = tbSignUpEmail.Text
            };

            UserServices userServices = new UserServices();
            try
            {
                userServices.CreateUser(userDTO);
                MessageBox.Show("User created successfully!");
                this.Hide();
                ctrlLogin.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
    }
}
