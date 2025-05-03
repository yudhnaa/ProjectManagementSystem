using BusinessLayer.Services;
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
using DTOLayer.Models;
using PresentationLayer.UC_SideBar;
using PresentationLayer.CustomControls;
using DataLayer.Domain;
using PresentationLayer.Controls;
using System.Drawing.Design;
using System.Data.SqlClient;
using PresentationLayer.Controls.Project;
using PresentationLayer.AppContext;
using PresentationLayer.Config;
using PresentationLayer.Controls.SideBar.Admin;
using Bunifu.UI.WinForms.BunifuButton;
using PresentationLayer.Utils;

namespace PresentationLayer
{
    public partial class FormAdminHome : Form
    {
        private UserDTO user;

        private ProjectServices projectServices;
        private TaskServices taskServices;
        private UserExtraInfoServices userServices;

        private CtrlPanelProjectAdminNew ctrlPanelProjectAdminNew;
        private CtrlPanelTaskAdminNew ctrlPanelTaskAdminNew;
        private CtrlPanelUserAdminNew ctrlPanelUserAdminNew;

        private CtrlPanelTaskAdmin ctrlPanelTaskAdmin;
        private CtrlPanelProjectAdmin ctrlPanelProjectAdmin;
        private CtrlPanelUserAdmin ctrlPanelUserAdmin;
        private CtrlPanelProjectStatusNew ctrlPanelProjectStatus;
        private CtrlPanelTaskStatusNew ctrlPanelTaskStatusNew;
        private CtrlPanelProjectPriorityNew ctrlPanelProjectPriorityNew;
        private CtrlPanelTaskPriorityNew ctrlPanelTaskPriorityNew;
        private CtrlPanelUserRoleNew ctrlPanelUserRoleNew;
        private CtrlPanelDepartmentNew ctrlPanelDepartmentAdminNew;

        private BunifuButton _currentButton;
        private BunifuButton _previousButton;

        private BunifuButton currentButton
        {
            get
            {
                return _currentButton;
            }
            set
            {
                _previousButton = _currentButton ?? value;

                if (_previousButton != null)
                {
                    _previousButton.OnIdleState.FillColor = GlobalVariables.ButtonIdleFillColor;
                    _previousButton.OnIdleState.ForeColor = GlobalVariables.FontColorLightBackground2;
                    _previousButton.Refresh();
                }

                _currentButton = value;

                if (_currentButton != null)
                {
                    _currentButton.OnIdleState.FillColor = GlobalVariables.ButtonPressedFillColor;
                    _currentButton.OnIdleState.ForeColor = GlobalVariables.FontColorLightBackground;
                    _currentButton.Refresh();
                }
            }
        }

        private void InitButton(BunifuButton btn)
        {
            btn.CustomizableEdges.TopLeft = false;
            btn.CustomizableEdges.BottomLeft = false;

            btn.CustomizableEdges.BottomRight = true;
            btn.CustomizableEdges.TopRight = true;

            btn.OnIdleState.FillColor = GlobalVariables.ButtonIdleFillColor;
            btn.OnIdleState.BorderRadius = 45;
            btn.OnIdleState.BorderColor = GlobalVariables.ButtonBorderColor;
            btn.OnIdleState.ForeColor = GlobalVariables.FontColorLightBackground2;

            btn.onHoverState.FillColor = GlobalVariables.ButtonHoverFillColor;
            btn.onHoverState.BorderRadius = 45;
            btn.onHoverState.BorderColor = GlobalVariables.ButtonBorderColor;
            btn.onHoverState.ForeColor = GlobalVariables.FontColorLightBackground;

            btn.OnPressedState.FillColor = GlobalVariables.ButtonPressedFillColor;
            btn.OnPressedState.BorderRadius = 45;
            btn.OnPressedState.BorderColor = GlobalVariables.ButtonBorderColor;
            btn.OnPressedState.ForeColor = GlobalVariables.FontColorLightBackground;

            btn.TextAlign = ContentAlignment.MiddleLeft;
            btn.Margin = new Padding(10, 0, 0, 0);
        }

        private void InitControls()
        {
            btnProject_Click(btnProject, null);

            this.WindowState = FormWindowState.Maximized;

            lbUsername.Text = this.user.Username;
            lbUserRole.Text = UserSession.Instance.UserRole.Name;

            panelLeft.Controls.OfType<BunifuButton>()
                .ToList()
                .ForEach(btn => InitButton(btn));
        }

        private void InitServices()
        {
            taskServices = new TaskServices();
            projectServices = new ProjectServices();
            userServices = new UserExtraInfoServices();
        }

        public FormAdminHome()
        {
            this.user = UserSession.Instance.User;

            InitializeComponent();

            InitControls();
            InitServices();
        }

        private void btnProject_Click(object sender, EventArgs e)
        {
            currentButton = sender as BunifuButton;

            if (ctrlPanelProjectAdminNew == null)
                ctrlPanelProjectAdminNew = new CtrlPanelProjectAdminNew();

            panelCenterContent.Controls.Clear();
            panelCenterContent.Controls.Add(ctrlPanelProjectAdminNew);
            ctrlPanelProjectAdminNew.BringToFront();
        }

        private void btnTask_Click(object sender, EventArgs e)
        {
            currentButton = sender as BunifuButton;

            if (ctrlPanelTaskAdminNew == null)
                ctrlPanelTaskAdminNew = new CtrlPanelTaskAdminNew();

            panelCenterContent.Controls.Clear();
            panelCenterContent.Controls.Add(ctrlPanelTaskAdminNew);
            ctrlPanelTaskAdminNew.BringToFront();
        }

        private void btnUsers_Click(object sender, EventArgs e)
        {
            currentButton = sender as BunifuButton;

            if (ctrlPanelUserAdminNew == null)
                ctrlPanelUserAdminNew = new CtrlPanelUserAdminNew();

            panelCenterContent.Controls.Clear();
            panelCenterContent.Controls.Add(ctrlPanelUserAdminNew);
            ctrlPanelUserAdminNew.BringToFront();
        }

        private void btnTaskStatusAdmin_Click(object sender, EventArgs e)
        {
            currentButton = sender as BunifuButton;

            if (ctrlPanelTaskStatusNew == null)
                ctrlPanelTaskStatusNew = new CtrlPanelTaskStatusNew();

            panelCenterContent.Controls.Clear();
            panelCenterContent.Controls.Add(ctrlPanelTaskStatusNew);
            ctrlPanelTaskStatusNew.BringToFront();
        }

        private void btnProjectStatusAdmin_Click(object sender, EventArgs e)
        {
            currentButton = sender as BunifuButton;

            if (ctrlPanelProjectStatus == null)
                ctrlPanelProjectStatus = new CtrlPanelProjectStatusNew();

            panelCenterContent.Controls.Clear();
            panelCenterContent.Controls.Add(ctrlPanelProjectStatus);
            ctrlPanelProjectStatus.BringToFront();
        }

        private void btnUserRoleAdmin_Click(object sender, EventArgs e)
        {
            currentButton = sender as BunifuButton;

            if (ctrlPanelUserRoleNew == null)
                ctrlPanelUserRoleNew = new CtrlPanelUserRoleNew();
            panelCenterContent.Controls.Clear();
            panelCenterContent.Controls.Add(ctrlPanelUserRoleNew);
        }

        private void btnTaskPriorityAdmin_Click(object sender, EventArgs e)
        {
            currentButton = sender as BunifuButton;

            if (ctrlPanelTaskPriorityNew == null)
                ctrlPanelTaskPriorityNew = new CtrlPanelTaskPriorityNew();
            panelCenterContent.Controls.Clear();
            panelCenterContent.Controls.Add(ctrlPanelTaskPriorityNew);
        }

        private void btnProjectPriorityAdmin_Click(object sender, EventArgs e)
        {
            currentButton = sender as BunifuButton;

            if (ctrlPanelProjectPriorityNew == null)
                ctrlPanelProjectPriorityNew = new CtrlPanelProjectPriorityNew();
            panelCenterContent.Controls.Clear();
            panelCenterContent.Controls.Add(ctrlPanelProjectPriorityNew);
            ctrlPanelProjectPriorityNew.BringToFront();
        }

        private void btnDepartmentAdmin_Click(object sender, EventArgs e)
        {
            currentButton = sender as BunifuButton;

            if (ctrlPanelDepartmentAdminNew == null)
                ctrlPanelDepartmentAdminNew = new CtrlPanelDepartmentNew();
            panelCenterContent.Controls.Clear();
            panelCenterContent.Controls.Add(ctrlPanelDepartmentAdminNew);
            ctrlPanelDepartmentAdminNew.BringToFront();
        }

        private void bunifuSeparator1_Click(object sender, EventArgs e)
        {

        }

        private void panelLeft_Paint(object sender, PaintEventArgs e)
        {
            Panel panel = sender as Panel;
            if (panel != null)
            {
                using (Pen pen = new Pen(Color.LightGray, 1)) 
                {
                    int x = panel.Width - 1;
                    e.Graphics.DrawLine(pen, x, 0, x, panel.Height);
                }
            }
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            Panel panel = sender as Panel;
            if (panel != null)
            {
                using (Pen pen = new Pen(Color.LightGray, 1))
                {
                    int x = panel.Width - 1;
                    e.Graphics.DrawLine(pen, x, 0, x, panel.Height);
                }
            }
        }
    }
}