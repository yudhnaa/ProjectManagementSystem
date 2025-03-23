using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PresentationLayer
{
    public partial class frmLogin : Form
    {
        private bool isShowPassword = false;
        public frmLogin()
        {
            InitializeComponent();
        }

        private void FrmLogin_Load(object sender, EventArgs e)
        {
            isShowPassword = false;
            tbPassword.PasswordChar = '*';
            panelSignUp.Hide();

        }

        private void bunifuTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private bool loginUser(string userName, string password)
        {
            if (userName.Equals("Admin") && password.Equals("1"))
            {
                return true;
            }
            else if (1 == 2)
            { 
                return true;
            }
            return false;
            
        }

        private void bunifuButton1_Click(object sender, EventArgs e)
        {
            string userName = tbUsername.Text.Trim();
            string password = tbPassword.Text;

            bool isLoginSuccess = loginUser(userName, password);
            
            if (isLoginSuccess)
            {
                MessageBox.Show("Login Successfully");
                Form frmHome = new frmHome();
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

        private void bunifuLabel1_Click(object sender, EventArgs e)
        {

        }

        private void btnShowPassword_Click(object sender, EventArgs e)
        {
            isShowPassword = !isShowPassword;

            if (isShowPassword == true)
            {
                btnShowPassword.Image = Properties.Resources.open_eye;
                tbPassword.PasswordChar ='\0';

            } else
            {
                btnShowPassword.Image = Properties.Resources.eye;
                tbPassword.PasswordChar = '*';
            }
        }

        private void bunifuButton3_Click(object sender, EventArgs e)
        {

        }

        private void bunifuLabel4_Click(object sender, EventArgs e)
        {

        }

        private void btnSignUpp_Click(object sender, EventArgs e)
        {
            panelSignIn.Hide();
            panelSignUp.Show();
        }

        private void btnSignUpBack_Click(object sender, EventArgs e)
        {
            panelSignUp.Hide();
            panelSignIn.Show();
        }
    }
}
