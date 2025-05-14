using BusinessLayer.Services;
using BusinessLayer.Services.Ipml;
using DataLayer.Domain;
using DataLayer.EnumObjects;
using DTOLayer.Models;
using PresentationLayer.AppContext;
using PresentationLayer.Config;
using PresentationLayer.Controls.SideBar.User;
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

namespace PresentationLayer.Forms.MainForm.User
{
    public partial class FormRequestHelp : Form
    {

        private readonly UserDTO user = UserSession.Instance.User;
        private TaskDTO _task;

        public TaskDTO task
        {
            get => _task;
            set
            {
                _task = value;
                LoadUsers();
                SetUsers();
            }
        }

        private readonly IUserServices userServices = new UserServices();
        private readonly ITaskHelpRequestServices taskHelpRequestServices = new TaskHelpRequestServices();

        private BindingList<UserDTO> users = new();

        public FormRequestHelp(TaskDTO task)
        {

            InitializeComponent();
            InitControls();

            this.task = task;

        }

        private void InitControls()
        {
            this.HorizontalScroll.Enabled = false;
            this.ShowInTaskbar = false;
            
            cbUsers.DisplayMember = "Username";
            cbUsers.ValueMember = "Id";
        }

        private void FormNotification_Load(object sender, EventArgs e)
        {
            if (task == null)
                return;

            else
            {
                LoadUsers();
                SetUsers();
            }
                
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void LoadUsers()
        {
            try
            {
                users = new BindingList<UserDTO>(
                    userServices.GetUsersInProject("", task.ProjectId)
                                .Where(u => user.Id != u.Id)
                                .ToList()
                );
            }
            catch (SqlException sqlEx)
            {
                MessageBox.Show("Error loading users: " + sqlEx.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An unexpected error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetUsers()
        {
            if (users == null || users.Count == 0)
                return;

            cbUsers.DataSource = users;
        }

        private void btnSent_Click(object sender, EventArgs e)
        {
            if (cbUsers.SelectedValue == null)
                return;
            try
            {
                var noti = new NotificationDTO
                {
                    UserId = (int)cbUsers.SelectedValue,
                    Title = GlobalVariables.TaskHelpRequestTitle,
                    Message = string.Format(GlobalVariables.TaskHelpRequestMSG, cbUsers.SelectedText, task.Name),
                    NotificationTypeId = (int)NotificationTypeEnum.TaskHelpRequest,
                    IsRead = false,
                    CreatedDate = DateTime.Now
                };

                var taskHelpRequest = new TaskHelpRequestDTO
                {
                    TaskId = task.Id,
                    RequestedBy = user.Id,
                    RequestedTo = (int)cbUsers.SelectedValue,
                    RequestMessage = string.Format(GlobalVariables.TaskHelpRequestMSG, cbUsers.SelectedText, task.Name),
                    IsResolved = false,
                    ResolvedDate = null,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = null
                };

                var res = taskHelpRequestServices.CreateTaskHelpRequest(taskHelpRequest, noti);

                if (res)
                    MessageBox.Show("Request sent successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Fail to request" + ex.Message, "Fail", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Fail to request" + ex.Message, "Fail", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
