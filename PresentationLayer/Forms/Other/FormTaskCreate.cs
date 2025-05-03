using BusinessLayer.Services;
using BusinessLayer;
using DTOLayer.Models;
using PresentationLayer.AppContext;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PresentationLayer.Config;

namespace PresentationLayer.Forms.Other
{
    public partial class FormTaskCreate : Form
    {
        public UserDTO user;
        public TaskDTO parentTask;

        private UserServices userServices;
        private TaskServices taskServices;
        private ProjectServices projectServices;
        private TaskStatusServices taskStatusServices;
        private TaskPriorityServices taskPriorityServices;

        private List<TaskStatusDTO> taskStatusDTOs;
        private List<TaskPriorityDTO> taskPriorityDTOs;
        private List<TaskDTO> taskDTOs;

        private Timer debounceTimer;

        private string lastSearchComboBox = "";
        private System.Windows.Forms.ComboBox currentSearchBox = null;

        public FormTaskCreate(TaskDTO parentTask, ProjectDTO project)
        {
            this.user = UserSession.Instance.User;
            this.parentTask = parentTask;

            InitializeComponent();
        }

        public FormTaskCreate(ProjectDTO project)
        {
            this.user = UserSession.Instance.User;

            InitializeComponent();
        }

        private void FormCreateTasks_Load(object sender, EventArgs e)
        {
            userServices = new UserServices();
            projectServices = new ProjectServices();
            taskServices = new TaskServices();
            taskStatusServices = new TaskStatusServices();
            taskPriorityServices = new TaskPriorityServices();

            debounceTimer = new Timer();
            debounceTimer.Interval = 1000;
            debounceTimer.Tick += DebounceTimer_Tick;

            CbUser.DisplayMember = "Username";
            CbUser.ValueMember = "Id";
            cbStatus.DisplayMember = "Name";
            cbStatus.ValueMember = "Id";
            cbPriority.DisplayMember = "Name";
            cbPriority.ValueMember = "Id";
            cbParentTask.DisplayMember = "Name";
            cbParentTask.ValueMember = "Id";
            cbProject.DisplayMember = "Name";
            cbProject.ValueMember = "Id";

            if (parentTask != null)
            {
                cbParentTask.Items.Add(parentTask);
                cbParentTask.SelectedIndex = 0;
            }

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
                string kw = CbUser.Text;

                // Fetch users
                var users = userServices.GetAllUsers(kw);

                // Update DataSource
                CbUser.DataSource = users;
                CbUser.SelectedIndex = -1;

                // Restore text and caret position
                CbUser.Text = kw;
            }
            else if (currentSearchBox == cbParentTask)
            {
                string kw = cbParentTask.Text;
                var tasks = taskServices.GetAllTask(kw);

                cbParentTask.DataSource = tasks;
                cbParentTask.SelectedIndex = -1;

                // Restore text and caret position
                cbParentTask.Text = kw;

            } else if (currentSearchBox == cbProject)
            {
                string kw = cbProject.Text;
                var projects = projectServices.GetAllProjects(kw);

                cbProject.DataSource = projects;
                cbProject.SelectedIndex = -1;

                // Restore text and caret position
                cbProject.Text = kw;
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

            if (cbProject.SelectedValue == null)
            {
                MessageBox.Show("Please select a Project.");
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
                ProjectId = (int) cbProject.SelectedValue,
                AssignedUserId = (int)CbUser.SelectedValue,
                StatusId = (int)cbStatus.SelectedValue,
                PriorityId = (int)cbPriority.SelectedValue,
                StartDate = datepickerStart.Value,
                DueDate = datepickerEnd.Value,
                EstimatedHours = int.Parse(tbEstimate.Text),
                ParentTaskId = parentTask == null ? -1 : parentTask.Id,
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
            this.Dispose();
        }

        private void CbUser_KeyUp(object sender, KeyEventArgs e)
        {
            currentSearchBox = CbUser;
            debounceTimer.Stop();
            debounceTimer.Start();
        }

        private void cbProject_KeyUp(object sender, KeyEventArgs e)
        {
            currentSearchBox = cbProject;
            debounceTimer.Stop();
            debounceTimer.Start();
        }

        private void cbParentTask_KeyUp(object sender, KeyEventArgs e)
        {
            currentSearchBox = cbParentTask;
            debounceTimer.Stop();
            debounceTimer.Start();
        }
    }
}
