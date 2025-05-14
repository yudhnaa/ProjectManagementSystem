using Bunifu.Framework.UI;
using Bunifu.UI.WinForms;
using BusinessLayer;
using BusinessLayer.Services;
using BusinessLayer.Services.Ipml;
using C1.GanttView;
using C1.Win.Localization.Design;
using DataLayer.Domain;
using DataLayer.EnumObjects;
using DTOLayer;
using DTOLayer.Models;
using PresentationLayer.AppContext;
using PresentationLayer.Config;
using PresentationLayer.Controls.Others;
using PresentationLayer.CustomControls;
using PresentationLayer.Forms.MainForm.User;
using PresentationLayer.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Infrastructure;
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
    public partial class CtrlPanelTaskRequestHelp : UserControl
    {
        private readonly UserDTO user;
        private readonly TaskDTO parentTask;

        private List<TaskDTO> tasks = new List<TaskDTO>();

        private TaskDTO currentTask;

        private readonly ITaskServices taskServices = new TaskServices();
        private readonly ITaskHelpRequestServices taskHelpRequestServices = new TaskHelpRequestServices();

        private readonly IUserServices userServices = new UserServices();
        private readonly ITaskStatusServices taskStatusServices = new TaskStatusServices();
        private readonly ITaskPriorityServices taskPriorityServices = new TaskPriorityServices();
        private readonly ITaskCommentServices taskCommentServices = new TaskCommentServices();

        private List<TaskStatusDTO> taskStatusDTOs;
        private List<TaskCommentDTO> taskComments;
        private RoundedLabel lbStatus;



        public CtrlPanelTaskRequestHelp()
        {
            this.user = UserSession.Instance.User;
            InitializeComponent();
            InitConTrols();
            LoadTaskStatuses();
            LoadTasks();
        }

        private void CtrlPanelTaskAdminNew_Load(object sender, EventArgs e)
        {
        }

        private void InitConTrols()
        {
            lbStatus = new RoundedLabel();
            lbStatus._TextAlign = lbPriority._TextAlign;
            lbStatus.Anchor = lbPriority.Anchor;
            lbStatus.Margin = lbPriority.Margin;
            lbStatus.Padding = lbPriority.Padding;
            lbStatus.Size = lbPriority.Size;
            lbStatus.AutoSize = lbPriority.AutoSize;
            lbStatus.Font = lbPriority.Font;

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
            try
            {
                var taskHelpRequests = taskHelpRequestServices.GetTaskHelpRequestByUserId(user.Id);
                tasks.Clear();

                foreach (TaskHelpRequestDTO t in taskHelpRequests)
                {
                    tasks.Add(taskServices.GetTaskById(t.TaskId));
                }
                if (tasks?.Count > 0)
                {
                    dgvItems.DataSource = tasks;
                    dgvItems.Rows[0].Selected = true;
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
                lbTaskProject.Text = $"Task Id: {currentTask.Id} - Task Code: {currentTask.Code}";

                var assignedUser = userServices.GetUserById(currentTask.AssignedUserId);
                var createdByUser = userServices.GetUserById(currentTask.CreatedBy);

                lbOwner.Text = assignedUser?.Username ?? "Unknown";
                lbCreatedBy.Text = createdByUser?.Username ?? "Unknown";
                lbDueDate.Text = currentTask.DueDate?.ToString("dd/MM/yyyy");
                tbDescription.Text = currentTask.Description;

                lbStatus.Text = TaskStatusEnumExtensions.ToString(currentTask.StatusId);
                lbStatus._BackColor = Utils.Utils.GetStatusColor(lbStatus.Text);
                cbStatus.SelectedValue = currentTask.StatusId;

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
                if (currentTask == null)
                    return;

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
                currentTask = taskServices.GetTaskById((int)dgvItems.SelectedRows[0].Cells["Id"].Value);

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

        private void tbSearch_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    string keyword = tbSearch.Text.Trim();
                    keyword = string.IsNullOrEmpty(keyword) ? "" : keyword;

                    var filterTasks = tasks.Where(t => t.Name.Contains(keyword) || t.Code.Contains(keyword)).ToList();

                    if (filterTasks == null || filterTasks.Count == 0)
                    {
                        MessageBox.Show("Không tìm thấy task nào với từ khóa này.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    dgvItems.DataSource = filterTasks;
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

        private void panel2_SizeChanged(object sender, EventArgs e)
        {
            bunifuSeparator1.Width = panel2.Width;
        }

        private void btnSendComment_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(tbComment.Text))
                {
                    MessageBox.Show("Please enter a comment.");
                    return;
                }

                NotificationDTO noti = new NotificationDTO
                {
                    UserId = currentTask.AssignedUserId,
                    Title = GlobalVariables.CommentAddedTitle,
                    Message = string.Format(GlobalVariables.CommentAddedMSG, user.Username, currentTask.Name),
                    NotificationTypeId = (int)NotificationTypeEnum.CommentAdded,
                    IsRead = false,
                    CreatedDate = DateTime.Now
                };

                TaskCommentDTO taskComment = new TaskCommentDTO
                {
                    TaskId = currentTask.Id,
                    UserId = user.Id,
                    CommentText = tbComment.Text,
                    CreatedDate = DateTime.Now,
                };

                taskCommentServices.CreateTaskComment(taskComment, noti);
                tbComment.Clear();
                LoadTaskComments();
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

        protected override void OnVisibleChanged(EventArgs e)
        {
            base.OnVisibleChanged(e);

            if (this.Visible)
            {
                LoadTasks();
                LoadTaskComments();
                SetTaskComments();
            }
        }

    }
}
