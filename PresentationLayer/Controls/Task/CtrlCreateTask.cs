using BusinessLayer.Services;
using BusinessLayer;
using DataLayer.Domain;
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
using C1.Win.C1GanttView;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace PresentationLayer.Control
{
    public partial class CtrlCreateTask : UserControl
    {
        public UserDTO user;
        private ProjectDTO project;
        public TaskDTO parentTask;

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

        public CtrlCreateTask(UserDTO user, TaskDTO parentTask , ProjectDTO project)
        {
            InitializeComponent();

            this.user = user;
            this.parentTask = parentTask;
            this.project = project;
        }

        public CtrlCreateTask(UserDTO user, ProjectDTO project)
        {
            InitializeComponent();

            this.user = user;
            this.project = project;
        }

        private void ctrl_CreateTasks_Load(object sender, EventArgs e)
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

            cbParentTask.Items.Add(parentTask);
            cbParentTask.SelectedIndex = 0;

            CbUser.AutoCompleteMode = AutoCompleteMode.Suggest;
            CbUser.AutoCompleteSource = AutoCompleteSource.ListItems;

            loadTaskStatuses();
            loadTaskPriorities();
        }

        private void DebounceTimer_Tick(object sender, EventArgs e)
        {
            debounceTimer.Stop();

            if (currentSearchBox == CbUser)
            {
                string searchText = CbUser.Text;
                int selectionStart = CbUser.SelectionStart;

                // Fetch users
                var users = userServices.GetUserByKw(searchText, 10);

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
                //string kw = cbParentTask.Text;
                //var tasks = taskServices.getTas(kw, 10); // <- Assuming this method exists
                //cbParentTask.DataSource = tasks;
            }
        }

        private void loadTaskStatuses()
        {
            try
            {
                taskStatusDTOs = taskStatusServices.getTaskStatuses();
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
                taskPriorityDTOs = taskPriorityServices.getTaskPriorities();
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
            //currentSearchBox = cbParentTask;
            //debounceTimer.Stop();
            //debounceTimer.Start();
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
                Code = tbCode.Text,
                Name = tbTitle.Text,
                Description = tbDescription.Text,
                ProjectId = project.Id,
                AssignedUserId = (int)CbUser.SelectedValue,
                StatusId = (int)cbStatus.SelectedValue,
                PriorityId = (int)cbPriority.SelectedValue,
                StartDate = datepickerStart.Value,
                DueDate = datepickerEnd.Value,
                EstimatedHours = int.Parse(tbEstimate.Text),
                ParentTaskId = parentTask == null ? -1: parentTask.Id,
                CreatedDate = DateTime.Now
            };

            try
            {
                taskServices.CreateTask(newTask, this.user.Id);
                MessageBox.Show("Task created successfully.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error creating newTask: " + ex.Message);
            }
        }

        private void btCancel_Click(object sender, EventArgs e)
        {

        }
    }
}
