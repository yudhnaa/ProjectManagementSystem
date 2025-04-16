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
            

        }

        private void ctrlSignUpForm1_Load(object sender, EventArgs e)
        {
            this.ctrlSignUpForm1.ctrlLogin = ctrlLoginForm1;
        }

        private void ctrlLoginForm1_Load(object sender, EventArgs e)
        {
            this.ctrlLoginForm1.ctrlSignUp = ctrlSignUpForm1;
            this.ctrlLoginForm1.frmLogin = this;
        }
    }
}
