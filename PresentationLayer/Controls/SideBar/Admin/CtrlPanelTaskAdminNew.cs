using BusinessLayer;
using BusinessLayer.Services;
using C1.GanttView;
using C1.Win.Localization.Design;
using DTOLayer;
using DTOLayer.Models;
using PresentationLayer.AppContext;
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

namespace PresentationLayer.Controls.SideBar.Admin
{
    public partial class CtrlPanelTaskAdminNew : UserControl
    {
        private UserDTO user;
        private TaskDTO parentTask;
        private TaskServices taskServices;

        private List<TaskForListDTO> tasks;

        private TaskDTO currentTask;
        private ProjectForListDTO currentTaskProject;

        private UserServices userServices;
        private TaskStatusServices taskStatusServices;
        private TaskPriorityServices taskPriorityServices;
        private ProjectServices projectServices;

        private List<TaskStatusDTO> taskStatusDTOs;
        private List<TaskPriorityDTO> taskPriorityDTOs;
        private List<TaskDTO> taskDTOs;
        private ProjectForListDTO currentProject;

        private Timer debounceTimer;

        private string lastSearchComboBox = "";
        private System.Windows.Forms.ComboBox currentSearchBox = null;
        public CtrlPanelTaskAdminNew()
        {
            this.user = UserSession.Instance.User;
            InitializeComponent();

        }

        private void CtrlPanelTaskAdminNew_Load(object sender, EventArgs e)
        {
            InitConTrols();
            InitServices();
            LoadTaskStatuses();
            LoadTaskPriorities();
            LoadTasks();
        }

        private void InitConTrols()
        {
            this.Dock = DockStyle.Fill;

            dgvItems.AutoGenerateColumns = false;
            dgvItems.ReadOnly = true;
            dgvItems.MultiSelect = false;
            dgvItems.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvItems.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            CbUser.DisplayMember = "Username";
            CbUser.ValueMember = "Id";
            cbStatus.DisplayMember = "Name";
            cbStatus.ValueMember = "Id";
            cbPriority.DisplayMember = "Name";
            cbPriority.ValueMember = "Id";
            cbParentTask.DisplayMember = "Code";
            cbParentTask.ValueMember = "Id";
            cbProject.DisplayMember = "ProjectCode";
            cbProject.ValueMember = "Id";

            cbProject.Enabled = false;

            debounceTimer = new Timer();
            debounceTimer.Interval = 1000;
            debounceTimer.Tick += DebounceTimer_Tick;
        }

        private void InitServices()
        {
            // Initialize services here
            taskServices = new TaskServices();
            userServices = new UserServices();
            taskStatusServices = new TaskStatusServices();
            taskPriorityServices = new TaskPriorityServices();
            taskServices = new TaskServices();
            projectServices = new ProjectServices();


        }
        private void LoadTasks()
        {
            // Load data into controls here
            try
            {
                tasks = taskServices.GetAllTaskForListInlcudeInActive("");

                if (tasks != null && tasks.Count > 0)
                {
                    dgvItems.DataSource = tasks;
                    dgvItems.Columns.Clear();
                    dgvItems.Columns.Add(new DataGridViewTextBoxColumn() { DataPropertyName = "Id", HeaderText = "Id", Width = 50, Name = "Id" });
                    dgvItems.Columns.Add(new DataGridViewTextBoxColumn() { DataPropertyName = "Code", HeaderText = "Code", Width = 200 });
                    dgvItems.Columns.Add(new DataGridViewTextBoxColumn() { DataPropertyName = "Name", HeaderText = "Name", Width = 100 });
                    dgvItems.Columns.Add(new DataGridViewTextBoxColumn() { DataPropertyName = "StatusId", HeaderText = "Status Id", Width = 100 });
                    dgvItems.Columns.Add(new DataGridViewTextBoxColumn() { DataPropertyName = "StartDate", HeaderText = "Start Date", Width = 100 });
                    dgvItems.Columns.Add(new DataGridViewTextBoxColumn() { DataPropertyName = "CreatedBy", HeaderText = "Created By", Width = 100 });

                    dgvItems.Rows[0].Selected = true;

                }
                else
                {
                    MessageBox.Show("No users found.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
            catch (SqlException ex)
            {
                MessageBox.Show("Database error occurred while retrieving users.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while retrieving users.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void SetTaskInfo()
        {
            if (currentTask == null)
            {
                MessageBox.Show("Not Found Task");
                return;
            }

            tbTitle.Text = currentTask.Name;
            tbCode.Text = currentTask.Code;
            datepickerStart.Value = currentTask.StartDate ?? DateTime.Now;
            datepickerEnd.Value = currentTask.DueDate ?? DateTime.Now;
            tbEstimate.Text = ((int)currentTask.EstimatedHours).ToString();
            tbDescription.Text = currentTask.Description;

            cbStatus.SelectedValue = currentTask.StatusId;
            cbPriority.SelectedValue = currentTask.PriorityId;

            CbUser.DataSource = new List<UserDTO> { userServices.GetUserById(currentTask.AssignedUserId) };
            CbUser.SelectedValue = currentTask.AssignedUserId;

            var res = LoadTaskProject();
            if (res)
            {
                cbProject.DataSource = new List<ProjectForListDTO> { currentTaskProject };
                cbProject.SelectedValue = currentTask.ProjectId;
            }
            else
            {
                cbProject.DataSource = null;
                cbProject.Text = "No project found";
            }

            if (currentTask.ParentTaskId.HasValue && currentTask.ParentTaskId.Value != -1)
            {
                TaskDTO currentParentTask = taskServices.GetTaskById(currentTask.ParentTaskId.Value);
                if (currentParentTask != null)
                {
                    cbParentTask.DataSource = new List<TaskDTO> { currentParentTask };
                    cbParentTask.SelectedValue = currentTask.ParentTaskId;
                }
            }
        }

        private void LoadTaskStatuses()
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

        private void LoadTaskPriorities()
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

        private bool LoadTaskProject()
        {
            try
            {
                var res = projectServices.GetProjectForListById(currentTask.ProjectId);

                if (res != null)
                {
                    currentTaskProject = res;
                    return true;
                }
                return false;
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Database error occurred while retrieving task project.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading task project: " + ex.Message);
            }
            return false;
        }

        private void DebounceTimer_Tick(object sender, EventArgs e)
        {
            debounceTimer.Stop();

            try
            {

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
                    cbParentTask.SelectedIndex = -1;
                }
                //else if (currentSearchBox == cbProject)
                //{
                //    string kw = cbProject.Text;
                //    currentProjects = projectServices.GetProjectsForListByKw(kw, 10);

                //    if (currentProjects == null)
                //        return;

                //    cbProject.DataSource = currentProjects;
                //    cbProject.SelectedIndex = -1;
                //}
            }
            catch (SqlException ex)
            {

            }
            catch (Exception ex)
            {

            }
        }

        private void dgvItems_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvItems.SelectedRows.Count == 1 && dgvItems.SelectedRows[0].Cells["Id"].Value != null)
            {
                DataGridViewRow item = dgvItems.SelectedRows[0];

                try
                {
                    currentTask = taskServices.GetTaskById((int)item.Cells["Id"].Value);
                    SetTaskInfo();
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Database error occurred while retrieving project details.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred while retrieving project details.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }

        private void CbUser_KeyUp(object sender, KeyEventArgs e)
        {
            currentSearchBox = CbUser;
            debounceTimer.Stop();
            debounceTimer.Start();
        }

        private void cbParentTask_KeyUp(object sender, KeyEventArgs e)
        {
            currentSearchBox = cbParentTask;
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

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            // Validate inputs before proceeding
            if (!ValidateInputs())
            {
                return;
            }

            TaskDTO newTask = new TaskDTO
            {
                Id = currentTask.Id,
                Code = tbCode.Text,
                Name = tbTitle.Text,
                Description = tbDescription.Text,
                ProjectId = currentTask.ProjectId,
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
                ProjectId = (int)cbProject.SelectedValue,
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

        private void tbSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    taskServices = new TaskServices();
                    string keyword = tbSearch.Text.Trim();
                    int pageSize = 10;

                    if (string.IsNullOrEmpty(keyword))
                    {
                        tasks = taskServices.GetAllTaskForListInlcudeInActive("");
                    }
                    else
                    {
                        tasks = taskServices.GetAllTaskForListInlcudeInActive(keyword);
                    }

                    if (tasks == null || tasks.Count == 0)
                    {
                        MessageBox.Show("Không tìm thấy task nào với từ khóa này.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    dgvItems.DataSource = tasks;
                }
                catch (SqlException ex)
                {
                    MessageBox.Show($"Lỗi cơ sở dữ liệu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void splitContainer1_Paint(object sender, PaintEventArgs e)
        {
            SplitContainer s = sender as SplitContainer;
            if (s != null)
            {
                e.Graphics.FillRectangle(Brushes.LightGray, s.SplitterRectangle);
            }
        }
    }
}