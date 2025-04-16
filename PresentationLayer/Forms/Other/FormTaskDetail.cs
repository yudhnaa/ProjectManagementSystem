using BusinessLayer.Services;
using BusinessLayer;
using DTOLayer.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace PresentationLayer.Forms.Other
{
    public partial class FormTaskDetail : Form
    {
        private TaskDTO task;

        TaskPriorityServices taskPriorityServices;
        TaskStatusServices taskStatusServices;
        UserServices userServices;

        public FormTaskDetail(TaskDTO task)
        {
            InitializeComponent();

            this.task = task;
        }

        private void FormTaskDetail_Load(object sender, EventArgs e)
        {
            taskPriorityServices = new TaskPriorityServices();
            taskStatusServices = new TaskStatusServices();
            userServices = new UserServices();

            loadInfo();
        }

        private void loadInfo()
        {
            // Load priority and status information
            try
            {
                lbTaskProject.Text = String.Format("Project Id: {0} / Task Id: {1} - Task Code: {2} ", task.ProjectId, task.Id, task.Code);

                // Load user information
                lbOwner.Text = userServices.GetUserById(task.AssignedUserId).Username;
                lbCreatedBy.Text = userServices.GetUserById(task.CreatedBy).Username;
                lbDueDate.Text = task.DueDate?.ToString("dd/MM/yyyy");

                TaskPriorityDTO priority = taskPriorityServices.GetById(task.PriorityId);
                TaskStatusDTO status = taskStatusServices.GetById(task.StatusId);

                lbPriority._BackColor = Utils.Utils.GetPriorityColor(priority.Name);
                lbStatus._BackColor = Utils.Utils.GetStatusColor(status.Name);

                lbPriority.Text = priority.Name;
                lbStatus.Text = status.Name;
            }
            catch (SqlException ex)
            {
                MessageBox.Show(Text = "Error", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show(Text = "Error", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }
    }
}
