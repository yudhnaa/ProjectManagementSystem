using Bunifu.UI.WinForms.BunifuButton;
using BusinessLayer;
using BusinessLayer.Services;
using DTOLayer;
using DTOLayer.Models;
using PresentationLayer.AppContext;
using PresentationLayer.Controls.Project;
using PresentationLayer.Controls.SideBar;
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

        private CtrlPanelTask ucTask;
        private UserControl ucOverview;
        private UserControl ucGant;
        private CtrlPanelHomeUser ucHome;
        private CtrlPanelProject ucProject;

        private CtrlListMyProjects ucMyProjects;

        private UserRoleServices roleServices;
        private ProjectServices projectServices;
        private TaskServices taskServices;

        private List<ProjectDTO> projects;
        private List<TaskDTO> tasks;

        public FormUserHome()
        {
            this.user = UserSession.Instance.User;

            InitializeComponent();
        }

        private void frmHome_Load(object sender, EventArgs e)
        {
            roleServices = new UserRoleServices();

            this.WindowState = FormWindowState.Maximized;

            this.projects = new List<ProjectDTO>();

            this.ucMyProjects = new UC_SideBar.UC_Project.CtrlListMyProjects();
            this.ucMyProjects.ProjectSelected += MyProjectsControl_ProjectSelected;

            this.projectServices = new ProjectServices();
            this.taskServices = new TaskServices();

            //btnHome_Click(this, EventArgs.Empty);


            lbUsername.Text = this.user.Username;
            lbUserRole.Text = UserSession.Instance.UserRole.Name;
            loadProjects();
        }

        private void loadTasks()
        {
            if (ucMyProjects.selectedItem != null)
            {
                this.tasks = taskServices.GetTaskByProjectIdAndUserId(ucMyProjects.selectedItem.Id, user.Id);
            }
            
        }

        private void MyProjectsControl_ProjectSelected(object sender, ProjectDTO selectedProject)
        {
           loadTasks();
            if(ucMyProjects.selectedItem!= null && ucTask != null)
                ucTask.selectedProject = ucMyProjects.selectedItem;
        }

        private void loadProjects()
        {
            try
            {
                projects = projectServices.GetProjectsByUserId(this.user.Id);
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

        private void frmHome_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
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
            if (ucTask == null)
            {
                ucTask = new UC_SideBar.CtrlPanelTask(ucMyProjects.selectedItem);
            }

            panelCenterContent.Controls.Clear();
            panelCenterContent.Controls.Add(ucTask);

            loadTasks();

            ucTask.tasks = this.tasks;
        }

        private void btnGant_Click(object sender, EventArgs e)
        {
           

        }

        private void btnOverview_Click(object sender, EventArgs e)
        {
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
            if (ucProject == null)
            {
                ucProject = new CtrlPanelProject();
            }

            panelCenterContent.Controls.Clear();
            panelCenterContent.Controls.Add(ucProject);

            ucProject.projects = this.projects;
        }

        private void btnAvatar_Click(object sender, EventArgs e)
        {
            ctrlUserInfo ctrlUserInfo = new ctrlUserInfo();
            panelCenterContent.Controls.Clear();
            panelCenterContent.Controls.Add(ctrlUserInfo);
        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
