using Bunifu.UI.WinForms.BunifuButton;
using BusinessLayer;
using BusinessLayer.Services;
using DTOLayer;
using DTOLayer.Models;
using PresentationLayer.CustomControls;
using PresentationLayer.UC_SideBar;
using PresentationLayer.UC_SideBar.UC_Project;
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
    public partial class frmUserHome: Form
    {
        private UserDTO user;
        private UC_Task ucTask;
        private UserControl ucOverview;
        private UserControl ucGant;
        private UserControl ucHome;

        private UC_MyProjects ucMyProjects;

        private ProjectServices projectServices;
        private TaskServices taskServices;

        private List<ProjectDTO> projects;
        private List<TaskDTO> tasks;

        public frmUserHome(UserDTO user)
        {
            InitializeComponent();
            this.user = user;
        }

        private void frmHome_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;

            ucTask = new UC_SideBar.UC_Task();

            this.projects = new List<ProjectDTO>();

            this.ucMyProjects = new UC_SideBar.UC_Project.UC_MyProjects();
            this.ucMyProjects.ProjectSelected += MyProjectsControl_ProjectSelected;

            this.projectServices = new ProjectServices();
            this.taskServices = new TaskServices();

            btnHome_Click(this, EventArgs.Empty);


            lbUsername.Text = this.user.Username;
            lbUserRole.Text = this.user.UserRoleId.ToString();
            loadProjects();
        }

        private void loadTasks()
        {
            if (ucMyProjects.selectedItem != null)
            {
                this.tasks = taskServices.GetTaskByProjectIdAndUserId(ucMyProjects.selectedItem.Id, user.Id);
                ucTask.tasks = this.tasks;
            }
            
        }

        private void MyProjectsControl_ProjectSelected(object sender, ProjectDTO selectedProject)
        {
            loadTasks();
        }

        private void loadProjects()
        {
            projects = projectServices.GetProjectsByUserId(this.user.Id);

            if (ucMyProjects is UC_SideBar.UC_Project.UC_MyProjects myProjectsControl)
            {
                myProjectsControl.projects = projects;
            }

            panelMyProjects.Controls.Add(ucMyProjects);
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
            panelCenterContent.Controls.Clear();
            panelCenterContent.Controls.Add(ucTask);

            loadTasks();
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
