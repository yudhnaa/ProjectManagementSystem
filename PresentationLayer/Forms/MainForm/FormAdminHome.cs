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
using System.Drawing.Design;
using System.Data.SqlClient;

namespace PresentationLayer
{
    public partial class FormAdminHome : Form
    {
        private UserDTO user;
        private UserRoleDTO loginUserRole;

        private UserRoleServices roleServices;

        private UC_SideBar.CtrlPanelTask ucTask;
        private UserControl ucOverview;
        private UserControl ucGant;
        private UserControl ucHome;




        public FormAdminHome(UserDTO user)
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
            roleServices = new UserRoleServices();

            //btnHome_Click(this, EventArgs.Empty);

            loadUserRole();

            lbUsername.Text = this.user.Username;
            lbUserRole.Text = loginUserRole.Name;
            //loadProjects();
        }

        private void loadUserRole()
        {
            try
            {
                loginUserRole = roleServices.GetUserRoleById(user.Id);
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Database error: " + ex.Message);
                this.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                this.Dispose();
            }
        }

        private void bunifuButton2_Click(object sender, EventArgs e)
        {
            panelCenterContent.Controls.Clear();
            CtrlCreateProject ctrlCreateProject = new CtrlCreateProject(this.user);
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
            CtrlCreateTask ctrl_CreateTask = new CtrlCreateTask(this.user, 
            //new TaskDTO
            //{
            //    Id = 1,
            //    Code = "Test Task 01"
            //}, 
            new ProjectDTO
            {
                Id = 1,
                Name = "Test Project 01"
            });
            ctrl_CreateTask.BringToFront();
            panelCenterContent.Controls.Add(ctrl_CreateTask);
        }

        private void btnListTask_Click(object sender, EventArgs e)
        {
            panelCenterContent.Controls.Clear();
            TaskServices taskServices = new TaskServices();
            List<TaskDTO> taskDTO = taskServices.GetTaskByProjectIdAndUserId(1,2);

            Control.CtrlTask ctrl_Task = new Control.CtrlTask(taskDTO.First());
            ctrl_Task.BringToFront();
            panelCenterContent.Controls.Add(ctrl_Task);

        }
    }
}
