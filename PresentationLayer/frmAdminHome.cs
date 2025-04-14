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
using PresentationLayer.Control;

namespace PresentationLayer
{
    public partial class frmAdminHome : Form
    {
        private User user;
        private UC_Task ucTask;
        private UserControl ucOverview;
        private UserControl ucGant;
        private UserControl ucHome;



        public frmAdminHome(User user)
        {
            InitializeComponent();

            this.user = user;
        }

        private void frmAdminHome_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;

            //ucTask = new UC_SideBar.UC_Task();

            //this.projects = new List<ProjectDTO>();

            //this.ucMyProjects = new UC_SideBar.UC_Project.UC_MyProjects();
            //this.ucMyProjects.ProjectSelected += MyProjectsControl_ProjectSelected;

            //this.projectServices = new ProjectServices();
            //this.taskServices = new TaskServices();

            //btnHome_Click(this, EventArgs.Empty);


            lbUsername.Text = this.user.Username;
            lbUserRole.Text = this.user.UserRole.Name;
            //loadProjects();
        }

        private void bunifuButton2_Click(object sender, EventArgs e)
        {
            panelCenterContent.Controls.Clear();
            ctrlCreateProject ctrlCreateProject = new ctrlCreateProject(this.user);
            ctrlCreateProject.BringToFront();
            panelCenterContent.Controls.Add(ctrlCreateProject);
        }

        private void btnCreateUser_Click(object sender, EventArgs e)
        {
            panelCenterContent.Controls.Clear();
            ctrlCreateUser ctrlCreateUser = new ctrlCreateUser();
            ctrlCreateUser.BringToFront();
            panelCenterContent.Controls.Add(ctrlCreateUser);
        }

        private void btnCreateTask_Click(object sender, EventArgs e)
        {
            panelCenterContent.Controls.Clear();
            ctrlCreateTask ctrl_CreateTask = new ctrlCreateTask(this.user, new TaskDTO
            {
                Id = 1,
                Code = "Test Task 01"
            }, new ProjectDTO
            {
                Id = 1,
                Name = "Test Project 01"
            });
            ctrl_CreateTask.BringToFront();
            panelCenterContent.Controls.Add(ctrl_CreateTask);
        }
    }
}
