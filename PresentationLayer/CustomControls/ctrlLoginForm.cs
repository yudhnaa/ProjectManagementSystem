using BusinessLayer;
using DTOLayer;
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
        public UserControl ctrlSignUp {get; set; }
        private bool isShowPassword;

        public ctrlLoginForm()
        {
            InitializeComponent();
            isShowPassword = false;
            tbPassword.PasswordChar = '*';
        }

        public ctrlLoginForm(UserControl ctrlSignUp)
        {
            InitializeComponent();
            this.ctrlSignUp = ctrlSignUp;
            isShowPassword = false;
            tbPassword.PasswordChar = '*';
        }

        private UserDTO loginUser(string userName, string password)
        {
            UserDTO user = new UserDTO
            {
                Username = userName,
                Password = password
            };

            UserServices userServices = new UserServices();
            UserDTO checkedUser = userServices.CheckLoginUser(user);

            return checkedUser;

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string userName = tbUsername.Text.Trim();
            string password = tbPassword.Text;

            UserDTO checkedUser = loginUser(userName, password);

            if (checkedUser != null)
            {
                MessageBox.Show("Login Successfully");
                Form frmHome = new frmHome(checkedUser);
                this.Hide();
                frmHome.Show();

            }
            else
            {
                DialogResult result = MessageBox.Show("Try Again?", "Login Failed", MessageBoxButtons.RetryCancel, MessageBoxIcon.Warning);
                if (result == DialogResult.Retry)
                {
                    tbUsername.Clear();
                    tbPassword.Clear();
                    tbUsername.Focus();
                }
            }

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

        private void btnSignUpp_Click(object sender, EventArgs e)
        {
            this.Hide();
            this.ctrlSignUp.Show();
        }
    }
}
