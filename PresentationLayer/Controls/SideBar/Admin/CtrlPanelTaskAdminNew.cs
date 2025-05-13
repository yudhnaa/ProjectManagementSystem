using BusinessLayer;
using BusinessLayer.Services;
using C1.GanttView;
using C1.Win.Localization.Design;
using DataLayer.EnumObjects;
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
        private readonly ITaskServices taskServices = new TaskServices();

        private List<TaskForListDTO> tasks;

        private TaskDTO currentTask;
        private ProjectForListDTO currentTaskProject = new ProjectForListDTO();
        private bool isCreating = false;

        private readonly IUserServices userServices = new UserServices();
        private readonly ITaskStatusServices taskStatusServices = new TaskStatusServices();
        private readonly ITaskPriorityServices taskPriorityServices = new TaskPriorityServices();
        private readonly IProjectServices projectServices = new ProjectServices();

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
            LoadTaskStatuses();
            LoadTaskPriorities();
            LoadTasks();
        }

        private void InitConTrols()
        {
            this.Dock = DockStyle.Fill;

            dgvItems.AutoGenerateColumns = false;
            dgvItems.ReadOnly = true;
            dgvItems.AllowUserToAddRows = false;
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

            debounceTimer = new Timer();
            debounceTimer.Interval = 1000;
            debounceTimer.Tick += DebounceTimer_Tick;

            dgvItems.CellFormatting += DgvItemsCellFormatting;

        }


        private void LoadTasks()
        {
            try
            {
                tasks = taskServices.GetAllTaskForListInlcudeInActive("") ?? new List<TaskForListDTO>();

                dgvItems.DataSource = null;
                dgvItems.Columns.Clear();

                if (tasks.Count > 0)
                {
                    // Manual column setup
                    dgvItems.AutoGenerateColumns = false;

                    dgvItems.Columns.Add(new DataGridViewTextBoxColumn() { DataPropertyName = "Id", HeaderText = "Id", Width = 50, Name = "Id" });
                    dgvItems.Columns.Add(new DataGridViewTextBoxColumn() { DataPropertyName = "Code", HeaderText = "Code", Width = 200 });
                    dgvItems.Columns.Add(new DataGridViewTextBoxColumn() { DataPropertyName = "Name", HeaderText = "Name", Width = 100 });
                    dgvItems.Columns.Add(new DataGridViewTextBoxColumn() { DataPropertyName = "StatusId", HeaderText = "Status", Width = 100, Name = "StatusId" });
                    dgvItems.Columns.Add(new DataGridViewTextBoxColumn() { DataPropertyName = "StartDate", HeaderText = "Start Date", Width = 100 });
                    dgvItems.Columns.Add(new DataGridViewTextBoxColumn() { DataPropertyName = "CreatedBy", HeaderText = "Created By", Width = 100, Name = "CreatedBy" });

                    dgvItems.DataSource = new BindingList<TaskForListDTO>(tasks);

                    dgvItems.Rows[0].Selected = true;
                }
                else
                {
                    MessageBox.Show("No tasks found.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (SqlException)
            {
                MessageBox.Show("Database error occurred while retrieving tasks.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception)
            {
                MessageBox.Show("An error occurred while retrieving tasks.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DgvItemsCellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvItems.Columns[e.ColumnIndex].Name == "StatusId" && e.Value is int statusId)
            {
                e.Value = TaskStatusEnumExtensions.ToString(statusId);
                e.FormattingApplied = true;
            }

            if (dgvItems.Columns[e.ColumnIndex].Name == "CreatedBy" && e.Value is int userID)
            {
                e.Value = userServices.GetUserById(userID).Username;
                e.FormattingApplied = true;
            }
        }

        private void DgvItems_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvItems.Columns[e.ColumnIndex].Name == "StatusId" && e.Value is int statusId)
            {
                e.Value = TaskStatusEnumExtensions.ToString(statusId);
                e.FormattingApplied = true;
            }

            if (dgvItems.Columns[e.ColumnIndex].Name == "CreatedBy" && e.Value is int userID)
            {
                e.Value = userServices.GetUserById(userID).Username;
                e.FormattingApplied = true;
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

                if (currentSearchBox == CbUser && cbProject.SelectedValue != null)
                {
                    string searchText = CbUser.Text;
                    int selectionStart = CbUser.SelectionStart;

                    // Fetch users
                    var users = userServices.GetUsersInProject(searchText, (int)cbProject.SelectedValue);

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
                    if (cbProject.SelectedValue != null)
                    {
                        string kw = cbParentTask.Text;
                        var tasks = taskServices.GetTaskForlistByProjectId((int)cbProject.SelectedValue);
                        cbParentTask.DisplayMember = "Code";
                        cbParentTask.ValueMember = "Id";
                        cbParentTask.DataSource = tasks;
                        cbParentTask.SelectedIndex = -1;
                    }
                   
                }
                else if (currentSearchBox == cbProject)
                {
                    string kw = cbProject.Text;
                    List<ProjectForListDTO> projectDTOs = projectServices.GetAllProjectsForList(kw);

                    if (projectDTOs == null)
                        return;

                    cbProject.DataSource = projectDTOs;
                    cbProject.SelectedIndex = -1;
                }
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
            isCreating = false;
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

        private void cbProject_KeyUp(object sender, KeyEventArgs e)
        {
            currentSearchBox = cbProject;
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
            if (datepickerStart.Value >= datepickerEnd.Value)
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

            try
            {
                if (isCreating)
                {
                    bool res = CreateTask();
                    if (res)
                    {
                        isCreating = false;
                        LoadTasks();
                    }
                }
                else
                {
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

                    taskServices.UpdateTask(newTask);
                    LoadTasks();
                    MessageBox.Show("Task update successfully.");
                }

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

        private bool CreateTask()
        {
            // Validate inputs before proceeding
            if (!ValidateInputs())
            {
                return false;
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
                ParentTaskId = (int?)cbParentTask.SelectedValue,
                CreatedDate = DateTime.Now
            };

            try
            {
                var res = taskServices.CreateTask(newTask, this.user.Id);
                if (res)
                {
                    MessageBox.Show("Task created successfully.");
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error creating new Task: " + ex.Message);
            }
            return false;
        }

        private void ResetInput()
        {
            tbCode.Clear();
            tbTitle.Clear();
            tbDescription.Clear();
            tbEstimate.Clear();

            cbProject.SelectedIndex = -1;
            CbUser.SelectedIndex = -1;
            cbStatus.SelectedIndex = -1;
            cbPriority.SelectedIndex = -1;
            cbParentTask.DataSource = null;

            datepickerStart.Value = DateTime.Today;
            datepickerEnd.Value = DateTime.Today;
        }

        private void btCreate_Click(object sender, EventArgs e)
        {
            if (isCreating == false)
            {
                isCreating = true;
                ResetInput();
            }
        }

        private void tbSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
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

        private void btCancel_Click(object sender, EventArgs e)
        {
            LoadTasks();
        }

       
    }
}