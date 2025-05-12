using Bunifu.UI.WinForms.BunifuButton;
using BusinessLayer.Services;
using DTOLayer;
using DTOLayer.Models;
using PresentationLayer.AppContext;
using PresentationLayer.Config;
using PresentationLayer.Controls.SideBar;
using PresentationLayer.Controls.SideBar.User;
using PresentationLayer.CustomControls;
using PresentationLayer.UC_SideBar;
using PresentationLayer.UC_SideBar.UC_Project;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace PresentationLayer
{
    public partial class FormUserHome : Form
    {
        private readonly UserDTO user = UserSession.Instance.User;

        private CtrlPanelTaskNew ucTask;
        private CtrlPanelHomeUser ucHome;
        private CtrlPanelProjectNew ucProject;
        private CtrlPanelGant ucGant;
        private CtrlListMyProjects ucMyProjects;

        // target-typed new expressions. c# >= 9.0
        private readonly IUserRoleServices roleServices = new UserRoleServices();
        private readonly IProjectServices projectServices = new ProjectServices();
        private readonly ITaskServices taskServices = new TaskServices();

        private List<ProjectForListDTO> projects = new();

        private BunifuButton _currentButton;
        private BunifuButton _previousButton;
        

        private BunifuButton currentButton
        {
            get => _currentButton;
            set
            {
                _previousButton = _currentButton ?? value;

                SetButtonStyle(_previousButton, GlobalVariables.ButtonIdleFillColor, GlobalVariables.FontColorLightBackground2);
                _currentButton = value;
                SetButtonStyle(_currentButton, GlobalVariables.ButtonPressedFillColor, GlobalVariables.FontColorLightBackground);
            }
        }

        public FormUserHome()
        {
            InitializeComponent();
            InitControls();
            LoadProjects();
        }

        private void InitControls()
        {
            //WindowState = FormWindowState.Maximized;  
            WindowState = FormWindowState.Normal;
            Size = new Size(1720, 880);
            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = true;

            ucMyProjects = new CtrlListMyProjects();
            ucMyProjects.ProjectSelected += MyProjectsControl_ProjectSelected;

            lbUsername.Text = user.Username;
            lbUserRole.Text = UserSession.Instance.UserRole.Name;

            foreach (var btn in splitContainer1.Panel1.Controls.OfType<BunifuButton>())
                InitButton(btn);

            btnHome_Click(btnHome, null); // Default screen  
        }

        private void LoadProjects()
        {
            try
            {
                projects = projectServices.GetProjectsForListByUserId(user.Id);
                ucMyProjects.projects = projects;
                splitContainer1.Panel2.Controls.Add(ucMyProjects);
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"Database error: {ex.Message}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading projects: {ex.Message}");
            }
        }

        private void InitButton(BunifuButton btn)
        {
            btn.CustomizableEdges = new Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderEdges
            {
                BottomLeft = false,
                BottomRight = true,
                TopLeft = false,
                TopRight = true
            };

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

        private void SetButtonStyle(BunifuButton btn, Color fillColor, Color foreColor)
        {
            if (btn == null) return;
            btn.OnIdleState.FillColor = fillColor;
            btn.OnIdleState.ForeColor = foreColor;
            btn.Refresh();
        }

        private void MyProjectsControl_ProjectSelected(object sender, ProjectForListDTO selectedProject)
        {
            ucTask ??= new CtrlPanelTaskNew();
            ucTask.CurrentProject = selectedProject;

            ucGant ??= new CtrlPanelGant();
            ucGant.CurrentProject = selectedProject;
        }

        private void frmHome_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void LoadControl(UserControl control)
        {
            panelCenterContent.Controls.Clear();
            panelCenterContent.Controls.Add(control);
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            currentButton = sender as BunifuButton;
            ucHome ??= new CtrlPanelHomeUser { Dock = DockStyle.Fill };
            LoadControl(ucHome);
            ucHome.LoadAllCharts();
        }
        private void btnTask_Click(object sender, EventArgs e)
        {
            currentButton = sender as BunifuButton;
            ucTask ??= new CtrlPanelTaskNew { Dock = DockStyle.Fill };
            LoadControl(ucTask);
        }

        private void btnGant_Click(object sender, EventArgs e)
        {
            currentButton = sender as BunifuButton;
            ucGant ??= new CtrlPanelGant { Dock = DockStyle.Fill };
            LoadControl(ucGant);
        }

        private void btnProjects_Click(object sender, EventArgs e)
        {
            currentButton = sender as BunifuButton;
            ucProject ??= new CtrlPanelProjectNew { Dock = DockStyle.Fill };
            LoadControl(ucProject);
        }

        private void btnAvatar_Click(object sender, EventArgs e)
        {
            currentButton = sender as BunifuButton;
            LoadControl(new ctrlUserInfo { Dock = DockStyle.Fill });
        }

        private void splitContainer1_Paint(object sender, PaintEventArgs e)
        {
            if (sender is SplitContainer s)
            {
                Rectangle rect = s.SplitterRectangle;
                rect.Width -= 5;

                using SolidBrush brush = new(Color.LightGray);
                e.Graphics.FillRectangle(brush, rect);
            }
        }
    }
}
