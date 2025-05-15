using Bunifu.UI.WinForms.Helpers.Transitions;
using BusinessLayer.Services;
using DataLayer.Domain;
using DataLayer.EnumObjects;
using DTOLayer;
using DTOLayer.Models;
using PresentationLayer.AppContext;
using PresentationLayer.Config;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace PresentationLayer.Controls.SideBar.Admin
{
    public partial class CtrlPanelProjectMember : UserControl
    {
        private readonly UserDTO user;

        private readonly IProjectMemberServices projectMemberServices = new ProjectMemberServices();
        private readonly IUserServices userSerices = new UserServices();
        private readonly IProjectServices projectServices = new ProjectServices();

        private BindingList<ProjectMemberDTO> projectMemberList = new BindingList<ProjectMemberDTO>();

        Dictionary<int, UserDTO> userCache = new();
        Dictionary<int, ProjectForListDTO> projectCache = new();

        public CtrlPanelProjectMember()
        {
            user = UserSession.Instance.User;
            projectMemberList.AllowNew = false;

            InitializeComponent();
        }

        private void CtrlPanelDepartmentNew_Load(object sender, EventArgs e)
        {
            InitControl();
            try
            {
                LoadProjectMemberList();
            }
            catch (SqlException ex)
            {
                ShowError("Database error while loading departments", ex);
            }
            catch (Exception ex)
            {
                ShowError("Unexpected error while loading departments", ex);
            }
        }

        private void InitControl()
        {
            this.Dock = DockStyle.Fill;
            this.DoubleBuffered = true;

            dgvItems.AutoGenerateColumns = false;
            dgvItems.MultiSelect = false;
            dgvItems.AllowUserToAddRows = false;
            dgvItems.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvItems.Columns.Clear();

            dgvItems.Columns.AddRange(
                new DataGridViewTextBoxColumn() { DataPropertyName = "Id", HeaderText = "Id", Name = "Id", Width = 20, ReadOnly = true },
                new DataGridViewTextBoxColumn() { DataPropertyName = "ProjectId", HeaderText = "Project Name", Name = "ProjectId", Width = 30, ReadOnly = true },
                new DataGridViewTextBoxColumn() { DataPropertyName = "UserId", HeaderText = "User Name", Name = "UserId", Width = 20, ReadOnly = true },
                new DataGridViewTextBoxColumn() { DataPropertyName = "RoleInProject", HeaderText = "Role In Project", Name = "RoleInProject", Width = 10, ReadOnly = true },
                new DataGridViewCheckBoxColumn() { DataPropertyName = "IsConfirmed", HeaderText = "Is Confirmed", Name = "IsConfirmed", Width = 100 }
            );

            dgvItems.CellFormatting += DgvItemsCellFormatting;

        }

        private void DgvItemsCellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvItems.Columns[e.ColumnIndex].Name == "ProjectId" && e.Value is int projectId && projectId != 0)
            {
                if (!projectCache.ContainsKey(projectId))
                {
                    var curProject = projectServices.GetProjectForListByIdInlcudeInActive(projectId);
                    projectCache.Add(projectId, curProject);
                }

                e.Value = projectCache[projectId].Name;
                e.FormattingApplied = true;
            }

            if (dgvItems.Columns[e.ColumnIndex].Name == "UserId" && e.Value is int userId && userId != 0)
            {
                if (!userCache.ContainsKey(userId))
                {
                    var curUser = userSerices.GetUserById(userId);
                    userCache.Add(userId, curUser);
                }

                e.Value = userCache[userId].Username;
                e.FormattingApplied = true;
            }

            if (dgvItems.Columns[e.ColumnIndex].Name == "RoleInProject" && e.Value is int roleId)
            {
                e.Value = ProjectMemberRoleEnumExtensions.FromId(roleId).ToString();
                e.FormattingApplied = true;
            }
        }

        private void LoadProjectMemberList(string keyword = "")
        {
            projectMemberList = new BindingList<ProjectMemberDTO>(projectMemberServices.GetAllProjectMembers(""));
            dgvItems.DataSource = projectMemberList;
        }

        private void tbSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    string keyword = tbSearch.Text.Trim();
                    LoadProjectMemberList(string.IsNullOrEmpty(keyword) ? "" : keyword);
                }
                catch (SqlException ex)
                {
                    ShowError("Database error during search", ex);
                }
                catch (Exception ex)
                {
                    ShowError("Unexpected error during search", ex);
                }

                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void ShowError(string message, Exception ex)
        {
            MessageBox.Show($"{message}: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                bool res = projectMemberServices.UpdateProjectMember(projectMemberList.ToList());
                if (res)
                {
                    MessageBox.Show("Update successful");
                    LoadProjectMemberList();
                }
                else
                {
                    MessageBox.Show("Update failed");
                }
            }
            catch (SqlException ex)
            {
                ShowError("Database error during update", ex);
            }
            catch (Exception ex)
            {
                ShowError("Unexpected error during update", ex);
            }
        }

        private void bunifuIconButton1_Click(object sender, EventArgs e)
        {
            LoadProjectMemberList();
        }

        private void tbSearch_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    string kw = tbSearch.Text.ToString();
                    var projectMembers = projectMemberServices.GetAllProjectMembersIncludeInActive(kw);
                    if (projectMembers != null)
                    {
                        projectMemberList.Clear();
                        projectMembers.ForEach(m => projectMemberList.Add(m));
                    }
                }
                catch (SqlException ex)
                {
                    ShowError("Database error during search", ex);
                }
                catch (Exception ex)
                {
                    ShowError("Unexpected error during search", ex);
                }
            }
        }

        protected override void OnVisibleChanged(EventArgs e)
        {
            base.OnVisibleChanged(e);

            if (this.Visible)
            {
                LoadProjectMemberList();
            }
        }
    }
}
