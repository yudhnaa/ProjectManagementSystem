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
    public partial class FormTaskUpdateAdmin : Form
    {
        private TaskDTO task;

        private UserServices userServices;
        private TaskStatusServices taskStatusServices;
        private TaskPriorityServices taskPriorityServices;
        private TaskServices taskServices;

        private List<TaskStatusDTO> taskStatusDTOs;
        private List<TaskPriorityDTO> taskPriorityDTOs;
        private List<TaskDTO> taskDTOs;

        private Timer debounceTimer;

        private string lastSearchComboBox = "";
        private System.Windows.Forms.ComboBox currentSearchBox = null;

        public FormTaskUpdateAdmin(TaskDTO task)
        {
            InitializeComponent();

            this.task = task;
        }

        private void FormTaskUpdate_Load(object sender, EventArgs e)
        {
            userServices = new UserServices();
            taskStatusServices = new TaskStatusServices();
            taskPriorityServices = new TaskPriorityServices();
            taskServices = new TaskServices();

            debounceTimer = new Timer();
            debounceTimer.Interval = 1000;
            debounceTimer.Tick += DebounceTimer_Tick;

            CbUser.DisplayMember = "Username";
            CbUser.ValueMember = "Id";
            cbStatus.DisplayMember = "Name";
            cbStatus.ValueMember = "Id";
            cbPriority.DisplayMember = "Name";
            cbPriority.ValueMember = "Id";
            cbParentTask.DisplayMember = "Code";
            cbParentTask.ValueMember = "Id";

            

            CbUser.AutoCompleteMode = AutoCompleteMode.Suggest;
            CbUser.AutoCompleteSource = AutoCompleteSource.ListItems;

            loadTaskStatuses();
            loadTaskPriorities();
            setTaskInfo();
        }

        private void DebounceTimer_Tick(object sender, EventArgs e)
        {
            debounceTimer.Stop();

            if (currentSearchBox == CbUser)
            {
                string searchText = CbUser.Text;
                int selectionStart = CbUser.SelectionStart;

                // Fetch users
                var users = userServices.GetAllUsers(searchText);

                // Update DataSource
                CbUser.BeginUpdate();
                CbUser.DataSource = users;
                CbUser.SelectedIndex = -1;
                CbUser.EndUpdate();

                // Restore text and caret position
                CbUser.Text = searchText;
                CbUser.SelectionStart = selectionStart;
            }
            else if (currentSearchBox == cbParentTask)
            {
                string kw = cbParentTask.Text;
                var tasks = taskServices.GetAllTask(kw);

                if (tasks == null)
                {
                    MessageBox.Show("No projects found.");
                    return;
                }
                cbParentTask.DataSource = tasks;
            }
        }

        private void setTaskInfo()
        {
            if (task == null)
                MessageBox.Show("Not Found Task");
            
            tbTitle.Text = task.Name;
            tbCode.Text = task.Code;
            datepickerStart.Value = task.StartDate ?? DateTime.Now;
            datepickerEnd.Value = task.DueDate ?? DateTime.Now;
            tbEstimate.Text = ((int)task.EstimatedHours).ToString();
            tbDescription.Text = task.Description;

            cbStatus.SelectedValue = task.StatusId;
            cbPriority.SelectedValue = task.PriorityId;

            CbUser.DataSource = new List<UserDTO> { userServices.GetUserById(task.AssignedUserId) };
            CbUser.SelectedValue = task.AssignedUserId;

            if (task.ParentTaskId.HasValue && task.ParentTaskId.Value != -1)
            {
                TaskDTO currentParentTask = taskServices.GetTaskById(task.ParentTaskId.Value);
                if (currentParentTask != null)
                {
                    cbParentTask.DataSource = new List<TaskDTO> { currentParentTask };
                    cbParentTask.SelectedValue = task.ParentTaskId;
                }
            }
        }

        private void loadTaskStatuses()
        {
            try
            {
                taskStatusDTOs = taskStatusServices.GetAllTaskStatuses("");
                if (taskStatusDTOs != null)
                {
                    cbStatus.DataSource = taskStatusDTOs;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading newTask statuses: " + ex.Message);
            }
        }

        private void loadTaskPriorities()
        {
            try
            {
                taskPriorityDTOs = taskPriorityServices.GetAllTaskPriorities("");
                if (taskPriorityDTOs != null)
                {
                    cbPriority.DataSource = taskPriorityDTOs;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading newTask priorities: " + ex.Message);
            }
        }

        private void cbParentTask_TextChanged(object sender, EventArgs e)
        {
            currentSearchBox = cbParentTask;
            debounceTimer.Stop();
            debounceTimer.Start();
        }

        private void CbUser_TextChanged(object sender, EventArgs e)
        {
            currentSearchBox = CbUser;
            debounceTimer.Stop();
            debounceTimer.Start();
        }

        private bool ValidateInputs()
        {
            // Check if required fields are empty
            if (string.IsNullOrWhiteSpace(tbCode.Text))
            {
                MessageBox.Show("Task Code is required.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(tbTitle.Text))
            {
                MessageBox.Show("Task Title is required.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(tbEstimate.Text) || !int.TryParse(tbEstimate.Text, out _))
            {
                MessageBox.Show("Estimated Hours must be a valid number.");
                return false;
            }

            // Validate date fields
            if (datepickerStart.Value > datepickerEnd.Value)
            {
                MessageBox.Show("Start Date cannot be later than Due Date.");
                return false;
            }

            // Validate dropdown selections
            if (CbUser.SelectedValue == null)
            {
                MessageBox.Show("Please select an Assigned User.");
                return false;
            }

            if (cbStatus.SelectedValue == null)
            {
                MessageBox.Show("Please select a Task Status.");
                return false;
            }

            if (cbPriority.SelectedValue == null)
            {
                MessageBox.Show("Please select a Task Priority.");
                return false;
            }

            return true;
        }

        private void btCreate_Click(object sender, EventArgs e)
        {
            // Validate inputs before proceeding
            if (!ValidateInputs())
            {
                return;
            }

            TaskDTO newTask = new TaskDTO
            {
                Id = task.Id,
                Code = tbCode.Text,
                Name = tbTitle.Text,
                Description = tbDescription.Text,
                ProjectId = task.ProjectId,
                AssignedUserId = (int)CbUser.SelectedValue,
                StatusId = (int)cbStatus.SelectedValue,
                PriorityId = (int)cbPriority.SelectedValue,
                StartDate = datepickerStart.Value,
                DueDate = datepickerEnd.Value,
                EstimatedHours = int.Parse(tbEstimate.Text),
                ParentTaskId = cbParentTask.Text == "" ? 0 : (int)cbParentTask.SelectedValue,
                UpdatedDate = DateTime.Now,
            };

            try
            {
                taskServices.UpdateTask(newTask);
                MessageBox.Show("Task update successfully.");
            }
            catch (SqlException sqlEx)
            {
                MessageBox.Show("Database error: " + sqlEx.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void btCancel_Click(object sender, EventArgs e)
        {

        }
    }
}
