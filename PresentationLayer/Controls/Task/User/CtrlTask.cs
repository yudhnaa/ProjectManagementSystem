using BusinessLayer.Services;
using DTOLayer.Models;
using PresentationLayer.Forms.Other;
using PresentationLayer.Utils;
using RestSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PresentationLayer.Control
{
    public partial class CtrlTask : UserControl
    {
        private TaskDTO taskDTO;

        private TaskStatusServices taskStatusServices;
        private TaskPriorityServices taskPriorityServices;

        private TaskStatusDTO taskStatusDTO;
        private TaskPriorityDTO taskPriorityDTO;

        public CtrlTask(TaskDTO taskDTO)
        {
            this.taskDTO = taskDTO;

            InitializeComponent();

        }

        private void style()
        {
            this.Dock = DockStyle.Fill;
            this.Margin = new Padding(50, 0, 50, 10);

            tableLayoutPanel1.ColumnStyles.Clear();
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 65F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 5F));

            lbTitle.Anchor = AnchorStyles.Left;
            lbAbstract.Anchor = AnchorStyles.Left | AnchorStyles.Top;
        }

        private void ctrl_Task_Load(object sender, EventArgs e)
        {

            if (taskDTO == null)
            {
                MessageBox.Show("Task not found");
                return;
            }

            style();

            taskStatusServices = new TaskStatusServices();
            taskPriorityServices = new TaskPriorityServices();

            loadTaskData();
            setTaskData();

        }

        private void loadTaskData()
        {
            try
            {
                taskStatusServices = new TaskStatusServices();
                taskPriorityServices = new TaskPriorityServices();

                taskStatusDTO = taskStatusServices.GetById(taskDTO.StatusId);
                taskPriorityDTO = taskPriorityServices.GetById(taskDTO.PriorityId);
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error loading task data: " + ex.Message);
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading task data: " + ex.Message);
                return;
            }
        }

        private void setTaskData()
        {
            lbTitle.Text = taskDTO.Name;
            lbAbstract.Text = String.Format("#{0} Opened {1} days ago by {2} in {3}", taskDTO.Code, taskDTO.CreatedDate, taskDTO.AssignedUserId, taskDTO.ProjectId);
            lbStatus.Text = taskStatusDTO.Name;
            lbStatus._BackColor = Utils.Utils.GetStatusColor(taskStatusDTO.Name);

            lbPriority.Text = taskPriorityDTO.Name;
            lbPriority._BackColor = Utils.Utils.GetPriorityColor(taskPriorityDTO.Name);

            lbDueDate.Text = taskDTO.DueDate?.Date.ToString("dd/MM/yyyy");
            lbDueDate._BackColor = Color.Green;
        }

        private void lbTitle_Click(object sender, EventArgs e)
        {
            FormTaskDetail formTaskDetail = new FormTaskDetail(this.taskDTO);
            formTaskDetail.ShowDialog();
        }

        private void btnUpdateTask_Click(object sender, EventArgs e)
        {
            FormTaskUpdate formTaskUpdate = new FormTaskUpdate(this.taskDTO);
            formTaskUpdate.ShowDialog();
        }

        private void lbDueda_Click(object sender, EventArgs e)
        {

        }
    }
}
