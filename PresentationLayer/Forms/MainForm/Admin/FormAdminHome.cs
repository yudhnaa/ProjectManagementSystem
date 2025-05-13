using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Bunifu.UI.WinForms.BunifuButton;

using BusinessLayer.Services;
using DTOLayer;
using DTOLayer.Models;
using DataLayer.Domain;

using PresentationLayer.AppContext;
using PresentationLayer.Config;
using PresentationLayer.Utils;
using PresentationLayer.Controls;
using PresentationLayer.Controls.Project;
using PresentationLayer.Controls.SideBar.Admin;
using PresentationLayer.CustomControls;
using PresentationLayer.UC_SideBar;

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
            get => _currentButton;
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

        public FormAdminHome()
        {
            user = UserSession.Instance.User;
            InitializeComponent();

            InitFormSettings();
            InitControls();
            InitServices();
            btnProject_Click(btnProject, null);
        }

        private void InitFormSettings()
        {
            WindowState = FormWindowState.Normal;
            Size = new Size(1720, 880);
            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = true;
        }

        private void InitControls()
        {
            lbUsername.Text = user.Username;
            lbUserRole.Text = UserSession.Instance.UserRole.Name;

            foreach (var btn in panelLeft.Controls.OfType<BunifuButton>())
                InitButton(btn);
        }

        private void InitServices()
        {
            taskServices = new TaskServices();
            projectServices = new ProjectServices();
            userServices = new UserExtraInfoServices();
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

        private void LoadControl<T>(BunifuButton button, ref T control, Func<T> createControl) where T : UserControl
        {
            currentButton = button;

            if (control == null)
                control = createControl();

            panelCenterContent.Controls.Clear();
            panelCenterContent.Controls.Add(control);
            control.BringToFront();
        }

        private void SetButtonStyle(BunifuButton btn, Color fillColor, Color foreColor)
        {
            if (btn == null) return;
            btn.OnIdleState.FillColor = fillColor;
            btn.OnIdleState.ForeColor = foreColor;
            btn.Refresh();
        }

        private void btnProject_Click(object sender, EventArgs e) =>
            LoadControl(sender as BunifuButton, ref ctrlPanelProjectAdminNew, () => new CtrlPanelProjectAdminNew());

        private void btnTask_Click(object sender, EventArgs e) =>
            LoadControl(sender as BunifuButton, ref ctrlPanelTaskAdminNew, () => new CtrlPanelTaskAdminNew());

        private void btnUsers_Click(object sender, EventArgs e) =>
            LoadControl(sender as BunifuButton, ref ctrlPanelUserAdminNew, () => new CtrlPanelUserAdminNew());

        private void btnTaskStatusAdmin_Click(object sender, EventArgs e) =>
            LoadControl(sender as BunifuButton, ref ctrlPanelTaskStatusNew, () => new CtrlPanelTaskStatusNew());

        private void btnProjectStatusAdmin_Click(object sender, EventArgs e) =>
            LoadControl(sender as BunifuButton, ref ctrlPanelProjectStatus, () => new CtrlPanelProjectStatusNew());

        private void btnUserRoleAdmin_Click(object sender, EventArgs e) =>
            LoadControl(sender as BunifuButton, ref ctrlPanelUserRoleNew, () => new CtrlPanelUserRoleNew());

        private void btnTaskPriorityAdmin_Click(object sender, EventArgs e) =>
            LoadControl(sender as BunifuButton, ref ctrlPanelTaskPriorityNew, () => new CtrlPanelTaskPriorityNew());

        private void btnProjectPriorityAdmin_Click(object sender, EventArgs e) =>
            LoadControl(sender as BunifuButton, ref ctrlPanelProjectPriorityNew, () => new CtrlPanelProjectPriorityNew());

        private void btnDepartmentAdmin_Click(object sender, EventArgs e) =>
            LoadControl(sender as BunifuButton, ref ctrlPanelDepartmentAdminNew, () => new CtrlPanelDepartmentNew());

        private void panelLeft_Paint(object sender, PaintEventArgs e) => DrawRightBorder(sender, e);
        private void panel2_Paint(object sender, PaintEventArgs e) => DrawRightBorder(sender, e);

        private void DrawRightBorder(object sender, PaintEventArgs e)
        {
            if (sender is Panel panel)
            {
                using (Pen pen = new Pen(Color.Silver, 1))
                {
                    int x = panel.Width - 1;
                    e.Graphics.DrawLine(pen, x, 0, x, panel.Height);
                }
            }
        }

        private void FormAdminHome_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void panelCenterContent_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
