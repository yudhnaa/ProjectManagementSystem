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
using PresentationLayer.Controls.Project;
using PresentationLayer.AppContext;
using PresentationLayer.Config;

namespace PresentationLayer
{
    public partial class FormAdminHome : Form
    {
        private UserDTO user;

        ProjectServices projectServices;
        private TaskServices taskServices;
        private UserExtraInfoServices userServices;

        private CtrlPanelTaskAdmin ctrlPanelTaskAdmin;
        private CtrlPanelProjectAdmin ctrlPanelProjectAdmin;
        private CtrlPanelUserAdmin ctrlPanelUserAdmin;

        List<ProjectDTO> projects;
        List<TaskDTO> tasks;
        List<UserExtraInfoDTO> users;


        public FormAdminHome()
        {
            this.user = UserSession.Instance.User;

            InitializeComponent();

        }

        private void frmAdminHome_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;

            taskServices = new TaskServices();
            projectServices = new ProjectServices();
            userServices = new UserExtraInfoServices();

            lbUsername.Text = this.user.Username;
            lbUserRole.Text = UserSession.Instance.UserRole.Name;
            
        }


        private void loadProjects()
        {
            try
            {
                
                projects = projectServices.GetAllProjectsIncludeDeleteCancel();

                if (projects.Count == 0)
                {
                    MessageBox.Show("No projects found");
                    return;
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Database error: " + ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void loadTask()
        {
            try
            {
                tasks = taskServices.GetAllTaskIncludeAllStatus();
                if (tasks.Count == 0)
                {
                    MessageBox.Show("No tasks found");
                    return;
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Database error: " + ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void loadUsers()
        {
            try
            {
                users = userServices.GetAllUser(1, GlobalVariables.PageSize);

                if (users.Count == 0)
                {
                    MessageBox.Show("No users found");
                    return;
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Database error: " + ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void btnProject_Click(object sender, EventArgs e)
        {
            if (ctrlPanelProjectAdmin == null)
                ctrlPanelProjectAdmin = new CtrlPanelProjectAdmin();

            loadProjects();

            ctrlPanelProjectAdmin.projects = projects;

            panelCenterContent.Controls.Clear();
            panelCenterContent.Controls.Add(ctrlPanelProjectAdmin);
            ctrlPanelProjectAdmin.BringToFront();
        }

        private void btnTask_Click(object sender, EventArgs e)
        {
            if (ctrlPanelTaskAdmin == null)
                ctrlPanelTaskAdmin = new CtrlPanelTaskAdmin();

            loadTask();

            ctrlPanelTaskAdmin.tasks = tasks;

            panelCenterContent.Controls.Clear();
            panelCenterContent.Controls.Add(ctrlPanelTaskAdmin);
            ctrlPanelTaskAdmin.BringToFront();
        }

        private void btnUsers_Click(object sender, EventArgs e)
        {
            if (ctrlPanelUserAdmin == null)
                ctrlPanelUserAdmin = new CtrlPanelUserAdmin();

            loadUsers();

            ctrlPanelUserAdmin.users = users;
            panelCenterContent.Controls.Clear();
            panelCenterContent.Controls.Add(ctrlPanelUserAdmin);
            ctrlPanelUserAdmin.BringToFront();

        }

        private void bunifuButton2_Click(object sender, EventArgs e)
        {
            //panelCenterContent.Controls.Clear();
            //CtrlCreateProject ctrlCreateProject = new CtrlCreateProject(this.user);
            //ctrlCreateProject.BringToFront();
            //panelCenterContent.Controls.Add(ctrlCreateProject);
        }

        private void btnCreateUser_Click(object sender, EventArgs e)
        {
            //panelCenterContent.Controls.Clear();
            //ctrlCreateUser ctrlCreateUser = new ctrlCreateUser();
            //ctrlCreateUser.BringToFront();
            //panelCenterContent.Controls.Add(ctrlCreateUser);
        }

        private void btnCreateTask_Click(object sender, EventArgs e)
        {
            //panelCenterContent.Controls.Clear();
            //CtrlCreateTask ctrl_CreateTask = new CtrlCreateTask( 
            ////new TaskDTO
            ////{
            ////    Id = 1,
            ////    Code = "Test Task 01"
            ////}, 
            //new ProjectDTO
            //{
            //    Id = 1,
            //    Name = "Test Project 01"
            //});
            //ctrl_CreateTask.BringToFront();
            //panelCenterContent.Controls.Add(ctrl_CreateTask);
        }

        private void btnListTask_Click(object sender, EventArgs e)
        {
            //panelCenterContent.Controls.Clear();
            //TaskServices taskServices = new TaskServices();
            //List<TaskDTO> taskDTO = taskServices.GetTaskByProjectIdAndUserId(1,2);

            //Control.CtrlTask ctrl_Task = new Control.CtrlTask(taskDTO.First());
            //ctrl_Task.BringToFront();
            //panelCenterContent.Controls.Add(ctrl_Task);

        }
    }
}
