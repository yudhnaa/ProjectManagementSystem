using BusinessLayer;
using DTOLayer;
using PresentationLayer.CustomControls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Bunifu.UI.WinForms;
using System.Drawing.Drawing2D;

namespace PresentationLayer
{
    public partial class FormLogin : Form
    {
        public FormLogin()
        {
            InitializeComponent();
        }

        private void FrmLogin_Load(object sender, EventArgs e)
        {
            ctrlLoginForm1.ctrlSignUp = ctrlSignUpForm1;
            ctrlLoginForm1.frmLogin = this;
            ctrlSignUpForm1.ctrlLogin = ctrlLoginForm1;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }
    }
}
