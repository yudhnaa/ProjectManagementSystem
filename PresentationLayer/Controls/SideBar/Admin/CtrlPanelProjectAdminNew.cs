using BusinessLayer;
using BusinessLayer.Services;
using DataLayer.Domain;
using DataLayer.EnumObjects;
using DTOLayer.Models;
using PresentationLayer.AppContext;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace PresentationLayer.Controls.SideBar.Admin
{
    public partial class CtrlPanelProjectAdminNew : UserControl
    {
        // Services
        private readonly IProjectServices _projectServices = new ProjectServices();
        private readonly IProjectMemberServices _projectMemberServices = new ProjectMemberServices();
        private readonly IUserServices _userServices = new UserServices();
        private readonly IProjectMemberRoleServices _projectMemberRoleServices = new ProjectMemberRoleServices();
        private readonly IProjectStatusServices _projectStatusServices = new ProjectStatusServices();
        private readonly IProjectPriorityServices _projectPriorityServices = new ProjectPriorityServices();

        // Data
        private List<ProjectForListDTO> _projects;
        private ProjectDTO _currentProject;
        private List<ProjectMemberDTO> _currentProjectMembers;
        private List<ProjectMemberRoleDTO> _projectMemberRoles;
        private List<ProjectStatusDTO> _projectStatuses;
        private List<ProjectPriorityDTO> _projectPriorities;

        // UI
        private readonly Timer _debounceTimer;
        private readonly UserDTO _user;

        private bool isCreating = false;

        public CtrlPanelProjectAdminNew()
        {
            _user = UserSession.Instance.User;

            // Initialize debounce timer
            _debounceTimer = new Timer { Interval = 1000 };
            _debounceTimer.Tick += DebounceTimer_Tick;

            InitializeComponent();
            InitializeControls();
        }

        private void InitializeControls()
        {
            Dock = DockStyle.Fill;

            // Configure DataGridView
            dgvItems.AutoGenerateColumns = false;
            dgvItems.ReadOnly = true;
            dgvItems.MultiSelect = false;
            dgvItems.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvItems.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            // Configure combo boxes
            cbMember.DisplayMember = "Username";
            cbMember.ValueMember = "Id";
            cbRole.DisplayMember = "Name";
            cbRole.ValueMember = "Id";
            cbPriority.DisplayMember = "Name";
            cbPriority.ValueMember = "Id";
            cbStatus.DisplayMember = "Name";
            cbStatus.ValueMember = "Id";

            // Configure columns
            ConfigureDataGridViewColumns();
        }

        private void ConfigureDataGridViewColumns()
        {
            dgvItems.Columns.Clear();

            dgvItems.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Id",
                HeaderText = "Id",
                Width = 50,
                Name = "Id"
            });

            dgvItems.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Name",
                HeaderText = "Project Name",
                Width = 200
            });

            dgvItems.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "ProjectCode",
                HeaderText = "Project Code",
                Width = 100
            });

            dgvItems.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "StartDate",
                HeaderText = "Start Date",
                Width = 100
            });

            dgvItems.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "StatusId",
                HeaderText = "Status",
                Width = 100,
                Name = "Status"
            });

            dgvItems.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "PriorityId",
                HeaderText = "Priority",
                Width = 100,
                Name = "Priority"
            });

            dgvItems.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "PercentComplete",
                HeaderText = "Percent Complete",
                Width = 100
            });

            dgvItems.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "IsDeleted",
                HeaderText = "Is Deleted",
                Width = 100
            });

            dgvItems.CellFormatting += DgvItems_CellFormatting;
        }

        private void CtrlPanelTaskAdminNew_Load(object sender, EventArgs e)
        {
            LoadInitialData();
        }

        private void DgvItems_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == dgvItems.Columns["Status"].Index && e.Value != null)
            {
                e.Value = ProjectStatusEnumExtensions.ToString((int)e.Value);
                e.FormattingApplied = true;
            }

            if (e.ColumnIndex == dgvItems.Columns["Priority"].Index && e.Value != null)
            {
                e.Value = ProjectPriorityEnumExtensions.ToString((int)e.Value);
                e.FormattingApplied = true;
            }
        }

        private void dgvItems_SelectionChanged(object sender, EventArgs e)
        {
            isCreating = false;
            if (dgvItems.SelectedRows.Count == 1 && dgvItems.SelectedRows[0].Cells["Id"].Value != null)
            {
                var item = dgvItems.SelectedRows[0];
                LoadProjectDetails((int)item.Cells["Id"].Value);
            }
        }

        private void btnRemoveMember_Click(object sender, EventArgs e)
        {
            if (listviewMembers.SelectedItems.Count > 0)
            {
                listviewMembers.Items.Remove(listviewMembers.SelectedItems[0]);
            }
            else
            {
                ShowMessage("Please select a member to remove.");
            }
        }

        private void btnAddMember_Click(object sender, EventArgs e)
        {
            if (cbMember.SelectedItem == null || cbRole.SelectedItem == null)
            {
                ShowMessage("Please select a user and a role.");
                return;
            }

            var selectedUser = (UserDTO)cbMember.SelectedItem;

            if (listviewMembers.Items.Cast<ListViewItem>()
                .Any(item => item.SubItems[0].Text == selectedUser.Id.ToString()))
            {
                ShowMessage("User already exists in the list.");
                return;
            }

            var selectedRole = (ProjectMemberRoleDTO)cbRole.SelectedItem;

            listviewMembers.Items.Add(new ListViewItem(new[]
            {
                selectedUser.Id.ToString(),
                selectedUser.LastName,
                selectedRole.Name
            }));
        }

        private void cbMember_KeyUp(object sender, KeyEventArgs e)
        {
            _debounceTimer.Stop();
            _debounceTimer.Start();
        }

        private void DebounceTimer_Tick(object sender, EventArgs e)
        {
            _debounceTimer.Stop();

            var keyword = cbMember.Text;
            var users = _userServices.GetAllUsers(keyword);

            if (users != null)
            {
                cbMember.DataSource = users;
                cbMember.SelectedIndex = -1;
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (!ValidateProjectInput()) return;

            try
            {
                if (isCreating)
                {
                    var project = CreateProjectDTOFromForm();
                    var newProject = _projectServices.CreateProject(project);

                    if (newProject != null)
                    {
                        AddProjectMembers(newProject.Id);
                        ClearForm();
                        ShowMessage("Project created successfully.", "Success");
                        LoadProjects("");

                        isCreating = false;
                    }
                }
                else
                {
                    var project = CreateProjectDTOFromForm();
                    var result = _projectServices.UpdateProject(project);

                    if (result)
                    {
                        UpdateProjectMembers(project.Id);
                        ClearForm();
                        ShowMessage("Project updated successfully.", "Success");
                        LoadProjects("");
                    }
                }

            }
            catch (SqlException ex)
            {
                ShowMessage($"Database error: {ex.Message}", "Error", MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                ShowMessage($"An error occurred: {ex.Message}", "Error", MessageBoxIcon.Error);
            }
        }

        private void btCreateProject_Click(object sender, EventArgs e)
        {
            if (!isCreating)
            {
                isCreating = true;
                ClearFields();
            }

        }

        private void ClearFields()
        {
            tbProjectName.Clear();
            tbProjectCode.Clear();
            tbProjectDesrciption.Clear();
            tbBudget.Clear();
            tbPercentComplete.Clear();
            datePickerStart.Value = DateTime.Today;
            datePickerEnd.Value = DateTime.Today.AddDays(30);
            cbStatus.SelectedIndex = -1;
            cbPriority.SelectedIndex = -1;
            cbRole.SelectedIndex = -1;
            listviewMembers.Items.Clear();
        }

        private void tbSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SearchProjects(tbSearch.Text.Trim());
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

        private void LoadInitialData()
        {
            try
            {
                _projectMemberRoles = _projectMemberRoleServices.GetAllProjectMemberRoles("");
                _projectStatuses = _projectStatusServices.GetAllProjectStatuses("");
                _projectPriorities = _projectPriorityServices.GetAllProjectPriorities("");

                cbRole.DataSource = _projectMemberRoles;
                cbStatus.DataSource = _projectStatuses;
                cbPriority.DataSource = _projectPriorities;

                LoadProjects("");
            }
            catch (Exception ex)
            {
                ShowMessage($"Error loading initial data: {ex.Message}", "Error", MessageBoxIcon.Error);
            }
        }

        private void LoadProjects(string keyword)
        {
            try
            {
                _projects = _projectServices.GetAllProjectsForListInlcudeInActive(keyword);

                if (_projects?.Count > 0)
                {
                    dgvItems.DataSource = _projects;
                    dgvItems.Rows[0].Selected = true;
                }
                else
                {
                    ShowMessage("No projects found.", "Information", MessageBoxIcon.Information);
                }
            }
            catch (SqlException ex)
            {
                ShowMessage("Database error occurred while retrieving projects.", "Error", MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                ShowMessage("An error occurred while retrieving projects.", "Error", MessageBoxIcon.Error);
            }
        }

        private void LoadProjectDetails(int projectId)
        {
            try
            {
                _currentProject = _projectServices.GetProjectById(projectId);
                _currentProjectMembers = _projectMemberServices.GetProjectMembersByProjectIdInlcudeInActive(projectId);

                SetProjectInfo();
            }
            catch (SqlException ex)
            {
                ShowMessage("Database error occurred while retrieving project details.", "Error", MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                ShowMessage("An error occurred while retrieving project details.", "Error", MessageBoxIcon.Error);
            }
        }

        private void SetProjectInfo()
        {
            if (_currentProject == null) return;

            tbProjectName.Text = _currentProject.Name;
            tbProjectCode.Text = _currentProject.ProjectCode;
            tbProjectDesrciption.Text = _currentProject.Description;
            tbBudget.Text = _currentProject.Budget.ToString();
            tbPercentComplete.Text = _currentProject.PercentComplete.ToString();
            datePickerStart.Value = _currentProject.StartDate ?? DateTime.Today;
            datePickerEnd.Value = _currentProject.EndDate ?? DateTime.Today.AddDays(30);
            cbStatus.SelectedValue = _currentProject.StatusId;
            cbPriority.SelectedValue = _currentProject.PriorityId;

            LoadProjectMembers();
        }

        private void LoadProjectMembers()
        {
            listviewMembers.BeginUpdate();
            listviewMembers.Items.Clear();
            


            // add manager
            var user = _userServices.GetUserById(_currentProject.ManagerId);
            var role = _projectMemberRoles.FirstOrDefault(r => r.Id == ProjectMemberRoleEnum.Manager.ToId());

            listviewMembers.Items.Add(new ListViewItem(new[] {
                        user.Id.ToString(),
                        user.LastName,
                        role.Name,
                        true.ToString(),
            }));

            foreach (var member in _currentProjectMembers)
            {
                user = _userServices.GetUserById(member.UserId);
                role = _projectMemberRoles.FirstOrDefault(r => r.Id == member.RoleInProject);

                if (user != null && role != null)
                {
                    listviewMembers.Items.Add(new ListViewItem(new[] {
                        user.Id.ToString(),
                        user.LastName,
                        role.Name,
                        member.IsConfirmed.ToString(),
                    }));
                }
            }

            listviewMembers.EndUpdate();
            int totalWidth = listviewMembers.ClientSize.Width;

            listviewMembers.Columns[0].Width = (int)(totalWidth * 0.3);
            listviewMembers.Columns[1].Width = (int)(totalWidth * 0.4);
            listviewMembers.Columns[2].Width = (int)(totalWidth * 0.3);

            //listviewMembers.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            //listviewMembers.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
        }

        private void SearchProjects(string keyword)
        {
            try
            {
                LoadProjects(string.IsNullOrEmpty(keyword) ? "" : keyword);
            }
            catch (SqlException ex)
            {
                ShowMessage($"Database error: {ex.Message}", "Error", MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                ShowMessage($"Error: {ex.Message}", "Error", MessageBoxIcon.Error);
            }
        }

        private bool ValidateProjectInput()
        {
            if (string.IsNullOrWhiteSpace(tbProjectName.Text))
            {
                ShowMessage("Project name is required.", "Validation Error", MessageBoxIcon.Warning);
                tbProjectName.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(tbProjectCode.Text))
            {
                ShowMessage("Project code is required.", "Validation Error", MessageBoxIcon.Warning);
                tbProjectCode.Focus();
                return false;
            }

            if (!decimal.TryParse(tbBudget.Text, out decimal budget) || budget <= 0)
            {
                ShowMessage("Please enter a valid budget amount.", "Validation Error", MessageBoxIcon.Warning);
                tbBudget.Focus();
                return false;
            }

            if (!double.TryParse(tbPercentComplete.Text, out double percent) || percent < 0 || percent > 100)
            {
                ShowMessage("Please enter a valid percent.", "Validation Error", MessageBoxIcon.Warning);
                tbBudget.Focus();
                return false;
            }

            if (datePickerEnd.Value <= datePickerStart.Value)
            {
                ShowMessage("End date must be after the start date.", "Validation Error", MessageBoxIcon.Warning);
                datePickerEnd.Focus();
                return false;
            }

            if (cbStatus.SelectedValue == null)
            {
                ShowMessage("Please select a project status.", "Validation Error", MessageBoxIcon.Warning);
                cbStatus.Focus();
                return false;
            }

            if (cbPriority.SelectedValue == null)
            {
                ShowMessage("Please select a project priority.", "Validation Error", MessageBoxIcon.Warning);
                cbPriority.Focus();
                return false;
            }

            if (!listviewMembers.Items.Cast<ListViewItem>().Any(item => item.SubItems[2].Text == "Manager"))
            {
                ShowMessage("Please add a team member with Manager role.", "Validation Error", MessageBoxIcon.Warning);
                return false;
            }

            if (listviewMembers.Items.Count == 0)
            {
                ShowMessage("Please add at least one team member to the project.", "Validation Error", MessageBoxIcon.Warning);
                return false;
            }

            int managerCount = listviewMembers.Items
                .Cast<ListViewItem>()
                .Count(item => item.SubItems[2].Text == "Manager");

            if (managerCount > 1)
            {
                ShowMessage("Project can have only 1 Manager.", "Validation Error", MessageBoxIcon.Warning);
                return false;
            }


            return true;
        }

        private ProjectDTO CreateProjectDTOFromForm()
        {
            var managerId = listviewMembers.Items.Cast<ListViewItem>()
                .First(item => item.SubItems[2].Text == "Manager").SubItems[0].Text;

            return new ProjectDTO
            {
                Id = _currentProject?.Id ?? 0,
                Name = tbProjectName.Text,
                ProjectCode = tbProjectCode.Text,
                Description = tbProjectDesrciption.Text,
                StartDate = datePickerStart.Value,
                EndDate = datePickerEnd.Value,
                Budget = decimal.Parse(tbBudget.Text),
                StatusId = (int)cbStatus.SelectedValue,
                ManagerId = int.Parse(managerId),
                PriorityId = (int)cbPriority.SelectedValue,
                CreatedBy = _user.Id,
                PercentComplete = decimal.Parse(tbPercentComplete.Text)
            };
        }

        private void UpdateProjectMembers(int projectId)
        {
            var userIdsNotInListView = _currentProjectMembers
                .Where(m => !listviewMembers.Items.Cast<ListViewItem>()
                    .Any(item => int.Parse(item.SubItems[0].Text) == m.UserId))
                .Select(m => m.UserId)
                .ToArray();

            foreach (ListViewItem item in listviewMembers.Items)
            {
                if (item.SubItems[2].Text == "Manager") continue;

                var projectMember = new ProjectMemberDTO
                {
                    UserId = int.Parse(item.SubItems[0].Text),
                    RoleInProject = _projectMemberRoles.First(r => r.Name == item.SubItems[2].Text).Id,
                    ProjectId = projectId,
                    CreatedDate = DateTime.Today,
                    IsConfirmed = bool.TryParse(item.SubItems[2].Text, out bool res) && res
                };

                if (!_projectMemberServices.UpdateProjectMember(projectMember))
                {
                    ShowMessage("Failed to update project member.");
                    return;
                }
            }

            foreach (var userId in userIdsNotInListView)
            {
                if (!_projectMemberServices.RemoveProjectMember(projectId, userId))
                {
                    ShowMessage("Failed to remove project member.");
                    return;
                }
            }
        }

        private void AddProjectMembers(int projectId)
        {
            foreach (ListViewItem item in listviewMembers.Items)
            {
                if (item.SubItems[2].Text == "Manager") continue;

                var projectMember = new ProjectMemberDTO
                {
                    UserId = int.Parse(item.SubItems[0].Text),
                    RoleInProject = _projectMemberRoles.First(r => r.Name == item.SubItems[2].Text).Id,
                    ProjectId = projectId,
                    CreatedDate = DateTime.Today,
                };

                if (!_projectMemberServices.CreateMemberToProject(projectMember))
                {
                    ShowMessage("Member is already in this project");
                }
            }
        }

        private void ClearForm()
        {
            tbProjectName.Clear();
            tbProjectCode.Clear();
            tbBudget.Clear();
            tbProjectDesrciption.Clear();
            cbMember.SelectedItem = null;
            cbRole.SelectedItem = null;
            listviewMembers.Items.Clear();
        }

        private void ShowMessage(string message, string title = "", MessageBoxIcon icon = MessageBoxIcon.Information)
        {
            MessageBox.Show(message, title, MessageBoxButtons.OK, icon);
        }

        private void dgvItems_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btCancelProject_Click(object sender, EventArgs e)
        {
            LoadProjects("");
        }
    }
}