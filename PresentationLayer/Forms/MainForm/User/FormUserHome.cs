using Bunifu.UI.WinForms.BunifuButton;
using BusinessLayer;
using BusinessLayer.Services;
using DTOLayer;
using DTOLayer.Models;
using PresentationLayer.AppContext;
using PresentationLayer.Config;
using PresentationLayer.Controls.Project;
using PresentationLayer.Controls.SideBar;
using PresentationLayer.Controls.SideBar.User;
using PresentationLayer.CustomControls;
using PresentationLayer.UC_SideBar;
using PresentationLayer.UC_SideBar.UC_Project;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PresentationLayer
{
    public partial class FormUserHome: Form
    {
        private UserDTO user;

        private CtrlPanelTaskNew ucTask;
        private UserControl ucOverview;
        private UserControl ucGant;
        private CtrlPanelHomeUser ucHome;
        private CtrlPanelProjectNew ucProject;

        private CtrlListMyProjects ucMyProjects;

        private UserRoleServices roleServices;
        private ProjectServices projectServices;
        private TaskServices taskServices;

        private List<ProjectForListDTO> projects;
        private List<TaskDTO> tasks;


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

        private void InitControls()
        {
            this.WindowState = FormWindowState.Maximized;

            this.ucMyProjects = new UC_SideBar.UC_Project.CtrlListMyProjects();
            this.ucMyProjects.ProjectSelected += MyProjectsControl_ProjectSelected;

            lbUsername.Text = this.user.Username;
            lbUserRole.Text = UserSession.Instance.UserRole.Name;


            splitContainer1.Panel1.Controls.OfType<BunifuButton>()
                .ToList()
                .ForEach(btn => InitButton(btn));

            // Set the default button to btnHome
            btnHome_Click(btnHome, null);
        }

        private void InitServices()
        {
            roleServices = new UserRoleServices();
            this.projectServices = new ProjectServices();
            this.taskServices = new TaskServices();

            this.projects = new List<ProjectForListDTO>();
        }

        private void loadProjects()
        {
            try
            {
                projects = projectServices.GetProjectsForListByUserId(this.user.Id);
                ucMyProjects.projects = projects;

                splitContainer1.Panel2.Controls.Add(ucMyProjects);
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Database error: " + ex.Message);
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading projects: " + ex.Message);
                return;
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
            btn.onHoverState.BorderColor = GlobalVariables.ButtonBorderColor;
            btn.OnPressedState.ForeColor = GlobalVariables.FontColorLightBackground;

            btn.TextAlign = ContentAlignment.MiddleLeft;
            btn.Margin = new Padding(10, 0, 0, 0);
        }



        public FormUserHome()
        {
            this.user = UserSession.Instance.User;

            InitializeComponent();

            InitControls();
            InitServices();

            loadProjects();
        }

        private void MyProjectsControl_ProjectSelected(object sender, ProjectForListDTO selectedProject)
        {
            if (ucTask == null)
                ucTask = new CtrlPanelTaskNew();

            ucTask.CurrentProject = selectedProject;
            
        }

        private void frmHome_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            currentButton = sender as BunifuButton;

            if (ucHome == null)
            {
                ucHome = new CtrlPanelHomeUser();
                ucHome.Dock = DockStyle.Fill;
            }
            panelCenterContent.Controls.Clear();
            panelCenterContent.Controls.Add(ucHome);
        }

        private void btnTask_Click(object sender, EventArgs e)
        {
            currentButton = sender as BunifuButton;

            //if (ucTask == null)
            //{
            //    ucTask = new CtrlPanelTaskNew();
            //}

            //ucTask.currentProject = currentProject;

            panelCenterContent.Controls.Clear();
            panelCenterContent.Controls.Add(ucTask);
        }

        private void btnGant_Click(object sender, EventArgs e)
        {
            currentButton = sender as BunifuButton;

            if (ucGant == null)
            {
                ucGant = new UC_SideBar.CtrlPanelGant();
                ucGant.Dock = DockStyle.Fill;
            }
            panelCenterContent.Controls.Clear();
            panelCenterContent.Controls.Add(ucGant);
        }

        private void btnOverview_Click(object sender, EventArgs e)
        {
            currentButton = sender as BunifuButton;

            if (ucOverview == null)
            {
                ucOverview = new UC_SideBar.CtrlPanelOverview();
                ucOverview.Dock = DockStyle.Fill;
            }
            panelCenterContent.Controls.Clear();
            panelCenterContent.Controls.Add(ucOverview);
            
        }

        private void btnProjects_Click(object sender, EventArgs e)
        {
            currentButton = sender as BunifuButton;

            if (ucProject == null)
            {
                ucProject = new CtrlPanelProjectNew();
            }

            panelCenterContent.Controls.Clear();
            panelCenterContent.Controls.Add(ucProject);
        }

        private void btnAvatar_Click(object sender, EventArgs e)
        {
            currentButton = sender as BunifuButton;

            ctrlUserInfo ctrlUserInfo = new ctrlUserInfo();
            panelCenterContent.Controls.Clear();
            panelCenterContent.Controls.Add(ctrlUserInfo);
        }

        private void splitContainer1_Paint(object sender, PaintEventArgs e)
        {
            SplitContainer s = sender as SplitContainer;
            if (s != null)
            {
                Rectangle rect = s.SplitterRectangle;

                // Áp dụng padding phải 5px
                rect.Width -= 5;

                using (SolidBrush brush = new SolidBrush(Color.LightGray))
                {
                    e.Graphics.FillRectangle(brush, rect);
                }
            }
        }
    }
}
