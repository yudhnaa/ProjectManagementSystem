using Bunifu.UI.WinForms.BunifuButton;
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

namespace PresentationLayer
{
    public partial class frmHome: Form
    {
        private UserDTO user;
        private UserControl ucTask;
        private UserControl ucOverview;
        private UserControl ucGant;
        private UserControl ucHome;

        public frmHome(UserDTO user)
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            this.user = user;

            btnHome_Click(this, EventArgs.Empty);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            lbUsername.Text = this.user.Username;
            lbUserRole.Text = this.user.UserRole.Name;
        }

        private void bunifuLabel1_Click(object sender, EventArgs e)
        {

        }

        private void bunifuPictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void bunifuSeparator1_Click(object sender, EventArgs e)
        {

        }

        private void bunifuSeparator2_Click(object sender, EventArgs e)
        {

        }

        private void bunifuShadowPanel2_ControlAdded(object sender, ControlEventArgs e)
        {

        }

        private void bunifuShadowPanel5_ControlAdded(object sender, ControlEventArgs e)
        {

        }

        private void bunifuShadowPanel6_ControlAdded(object sender, ControlEventArgs e)
        {

        }

        private void frmHome_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            if (ucHome == null)
            {
                ucHome = new UC_SideBar.UC_Home();
                ucHome.Dock = DockStyle.Fill;
            }
            panelCenterContent.Controls.Clear();
            panelCenterContent.Controls.Add(ucHome);
        }

        private void btnTask_Click(object sender, EventArgs e)
        {
            if (ucTask == null)
            {
                ucTask = new UC_SideBar.UC_Task();
                ucTask.Dock = DockStyle.Fill;
            }
            panelCenterContent.Controls.Clear();
            panelCenterContent.Controls.Add(ucTask);
        }

        private void btnGant_Click(object sender, EventArgs e)
        {
            if (ucGant == null)
            {
                ucGant = new UC_SideBar.UC_Gant();
                ucGant.Dock = DockStyle.Fill;
            }
            panelCenterContent.Controls.Clear();
            panelCenterContent.Controls.Add(ucGant);
        }

        private void btnOverview_Click(object sender, EventArgs e)
        {
            if (ucOverview == null)
            {
                ucOverview = new UC_SideBar.UC_Overview();
                ucOverview.Dock = DockStyle.Fill;
            }
            panelCenterContent.Controls.Clear();
            panelCenterContent.Controls.Add(ucOverview);
            
        }
    }
}
