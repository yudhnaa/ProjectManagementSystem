using Bunifu.Framework.UI;
using Bunifu.UI.WinForms;
using BusinessLayer;
using BusinessLayer.Services;
using BusinessLayer.Services.Ipml;
using DataLayer.Domain;
using DataLayer.EnumObjects;
using DTOLayer;
using DTOLayer.Models;
using PresentationLayer.AppContext;
using PresentationLayer.Controls.Others;
using PresentationLayer.CustomControls;
using PresentationLayer.Utils;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Menu;
using BunifuSeparator = Bunifu.UI.WinForms.BunifuSeparator;
using Task = System.Threading.Tasks.Task;

namespace PresentationLayer.Controls.SideBar.User
{
    public partial class CtrlPanelTaskNew : UserControl
    {
        private readonly UserDTO user;
        private TaskDTO parentTask;

        private ProjectForListDTO _currentProject;
        public ProjectForListDTO CurrentProject
        {
            get { return _currentProject; }
            set
            {
                _currentProject = value;
                if (_currentProject != null)
                {
                    LoadTasks();
                }
            }
        }

        private List<TaskForListDTO> tasks;

        private TaskDTO currentTask;

        private readonly ITaskServices taskServices = new TaskServices();
        private readonly IUserServices userServices = new UserServices();
        private readonly ITaskStatusServices taskStatusServices = new TaskStatusServices();
        private readonly ITaskPriorityServices taskPriorityServices = new TaskPriorityServices();
        private readonly ITaskCommentServices taskCommentServices = new TaskCommentServices();

        private List<TaskStatusDTO> taskStatusDTOs;
        private List<TaskCommentDTO> taskComments;
        private RoundedLabel lbStatus;

        public CtrlPanelTaskNew()
        {
            this.user = UserSession.Instance.User;
            InitializeComponent();
            InitConTrols();
        }

        private void CtrlPanelTaskAdminNew_Load(object sender, EventArgs e)
        {
            LoadTaskStatuses();
        }

        private void InitConTrols()
        {
            toggleMode.Visible = false;
            lbStatus = new RoundedLabel();
            lbStatus._TextAlign = lbPriority._TextAlign;
            lbStatus.Anchor = lbPriority.Anchor;
            lbStatus.Margin = lbPriority.Margin;
            lbStatus.Padding = lbPriority.Padding;
            lbStatus.Size = lbPriority.Size;
            lbStatus.AutoSize = lbPriority.AutoSize;
            lbStatus.Font = lbPriority.Font;

            btnUpdate.Visible = false;

            toggleMode.Checked = false;

            this.Dock = DockStyle.Fill;

            cbStatus.DisplayMember = "Name";
            cbStatus.ValueMember = "Id";

            dgvItems.AutoGenerateColumns = false;
            dgvItems.ReadOnly = true;
            dgvItems.MultiSelect = false;
            dgvItems.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvItems.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            dgvItems.Columns.Clear();
            dgvItems.Columns.Add(new DataGridViewTextBoxColumn() { DataPropertyName = "Id", HeaderText = "Id", Width = 50, Name = "Id" });
            dgvItems.Columns.Add(new DataGridViewTextBoxColumn() { DataPropertyName = "Code", HeaderText = "Code", Width = 200 });
            dgvItems.Columns.Add(new DataGridViewTextBoxColumn() { DataPropertyName = "Name", HeaderText = "Name", Width = 100 });
            dgvItems.Columns.Add(new DataGridViewTextBoxColumn()
            {
                DataPropertyName = "StatusId",
                HeaderText = "Status",
                Width = 100,
                Name = "Status",
                CellTemplate = new DataGridViewTextBoxCell()
            });
            dgvItems.Columns.Add(new DataGridViewTextBoxColumn() { DataPropertyName = "StartDate", HeaderText = "Start Date", Width = 100 });
            dgvItems.Columns.Add(new DataGridViewTextBoxColumn()
            {
                DataPropertyName = "CreatedBy",
                HeaderText = "Created By",
                Width = 100,
                Name = "CreatedBy",
                CellTemplate = new DataGridViewTextBoxCell()
            });
            dgvItems.CellFormatting += (s, e) =>
            {
                if (e.ColumnIndex == dgvItems.Columns["Status"].Index && e.Value != null)
                {
                    int statusId = (int)e.Value;
                    e.Value = ProjectStatusEnumExtensions.ToString(statusId);
                    e.FormattingApplied = true;
                }

                if (e.ColumnIndex == dgvItems.Columns["CreatedBy"].Index && e.Value != null)
                {
                    int userID = (int)e.Value;
                    e.Value = userServices.GetUserById(userID).Username;
                    e.FormattingApplied = true;
                }
            };
        }

        private void LoadTasks()
        {
            if (CurrentProject == null)
                return;

            try
            {
                tasks = taskServices.GetTaskForlistByProjectId(CurrentProject.Id);

                if (tasks?.Count > 0)
                {
                    dgvItems.DataSource = tasks;
                    dgvItems.Rows[0].Selected = true;
                }
                else
                {
                    MessageBox.Show("No tasks found.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                LogError("Error retrieving tasks", ex);
            }
        }

        private void LoadTaskStatuses()
        {
            try
            {
                taskStatusDTOs = taskStatusServices.GetAllTaskStatuses("");
                cbStatus.DataSource = taskStatusDTOs ?? new List<TaskStatusDTO>();
            }
            catch (Exception ex)
            {
                LogError("Error loading task statuses", ex);
            }
        }

        private void SetTaskInfo()
        {
            try
            {
                lbTaskProject.Text = $"Project Id: {CurrentProject.ProjectCode} / Task Id: {currentTask.Id} - Task Code: {currentTask.Code}";

                var assignedUser = userServices.GetUserById(currentTask.AssignedUserId);
                var createdByUser = userServices.GetUserById(currentTask.CreatedBy);

                lbOwner.Text = assignedUser?.Username ?? "Unknown";
                lbCreatedBy.Text = createdByUser?.Username ?? "Unknown";
                lbDueDate.Text = currentTask.DueDate?.ToString("dd/MM/yyyy");
                tbDescription.Text = currentTask.Description;

                lbStatus.Text = TaskStatusEnumExtensions.ToString(currentTask.StatusId);
                lbStatus._BackColor = Utils.Utils.GetStatusColor(lbStatus.Text);
                //cbStatus.SelectedIndex = currentTask.StatusId - 1;

                lbPriority.Text = TaskPriorityEnumExtensions.ToString(currentTask.PriorityId);
                lbPriority._BackColor = Utils.Utils.GetPriorityColor(lbPriority.Text);
            }
            catch (Exception ex)
            {
                LogError("Error setting task info", ex);
            }
        }

        private async void LoadTaskComments()
        {
            try
            {
                taskComments = await Task.Run(() => taskCommentServices.GetAllTaskCommentsById(currentTask.Id));

                panelComments.Controls
                    .OfType<Control>()
                    .Where(control => !(control is BunifuSeparator))
                    .ToList()
                    .ForEach(control => panelComments.Controls.Remove(control));

                if (taskComments?.Count > 0)
                {
                    SetTaskComments();
                }
            }
            catch (Exception ex)
            {
                LogError("Error retrieving task comments", ex);
            }
        }

        private void SetTaskComments()
        {
            if (taskComments == null || taskComments.Count == 0)
                return;

            foreach (var taskComment in taskComments)
            {
                var userComment = userServices.GetUserById(taskComment.UserId);
                if (userComment == null)
                    continue;

                var controlComment = new ControlComment
                {
                    Dock = DockStyle.Fill
                };
                controlComment.SetComment(taskComment.CommentText, userComment.Username, ProjectMemberRoleEnumExtensions.ToString(userComment.UserRoleId));
                panelComments.Controls.Add(controlComment);
            }
        }

        private void dgvItems_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvItems.SelectedRows.Count != 1 || dgvItems.SelectedRows[0].Cells["Id"].Value == null)
                return;

            try
            {
                toggleMode.Checked = false;

                currentTask = taskServices.GetTaskById((int)dgvItems.SelectedRows[0].Cells["Id"].Value);

                toggleMode.Visible = user.Id == currentTask.AssignedUserId;

                SetTaskInfo();
                LoadTaskComments();
            }
            catch (Exception ex)
            {
                LogError("Error retrieving project details", ex);
            }
        }

        private void LogError(string message, Exception ex)
        {
            // Centralized error logging
            MessageBox.Show($"{message}: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private bool ValidateInputs()
        {
            //if (String.IsNullOrEmpty(tbDescription.Text))
            //{
            //    MessageBox.Show("Please enter description.");
            //    return false;
            //}

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
                taskServices.UpdateTaskStatus(currentTask.Id, TaskStatusEnumExtensions.FromId((int)cbStatus.SelectedValue));
                MessageBox.Show("Task update successfully.");
                CurrentProject = _currentProject;
                toggleMode.Checked = false;
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

        private void tbSearch_KeyDown(object sender, KeyEventArgs e)
        {
            
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    string keyword = tbSearch.Text.Trim();
                    keyword = string.IsNullOrEmpty(keyword) ? "" : keyword;

                    tasks = taskServices.GetTasksForlistByProjectIdAndUserIdAndKw(CurrentProject.Id, user.Id, keyword);

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

        private void toggleMode_CheckedChanged(object sender, Bunifu.UI.WinForms.BunifuToggleSwitch.CheckedChangedEventArgs e)
        {
            bool isEditMode = toggleMode.Checked;

            // Get current control in cell (1,0)
            System.Windows.Forms.Control control = tableLayoutPanel1.GetControlFromPosition(1, 3);

            // Remove the current control from the table layout panel
            if (control != null)
                tableLayoutPanel1.Controls.Remove(control);

            if (isEditMode)
            {
                // Add a ComboBox to the table layout panel
                tableLayoutPanel1.Controls.Add(cbStatus, 1, 3);

                tbDescription.ReadOnly = true;
                lbComment.Visible = false;
                tbComment.Visible = false;
                btnUpdate.Visible = true;
                btnSendComment.Visible = false;
                panelComments.Visible = false;
            }
            else
            {
                tableLayoutPanel1.Controls.Add(lbStatus, 1, 3);
                tbDescription.ReadOnly = true;
                if (currentTask != null)
                    tbDescription.Text = currentTask.Description;
                lbComment.Visible = true;
                tbComment.Visible = true;
                btnUpdate.Visible = false;
                btnSendComment.Visible = true;
                panelComments.Visible = true;

            }
        }

        private void panel2_SizeChanged(object sender, EventArgs e)
        {
            bunifuSeparator1.Width = panel2.Width;
        }
    }
}
