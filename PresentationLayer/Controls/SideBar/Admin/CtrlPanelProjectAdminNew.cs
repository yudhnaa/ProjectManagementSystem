using Bunifu.UI.WinForms.Helpers.Transitions;
using BusinessLayer;
using BusinessLayer.Services;
using DataLayer.Domain;
using DataLayer.EnumObjects;
using DTOLayer.Models;
using PresentationLayer.AppContext;
using PresentationLayer.Config;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;
using System.Windows.Forms;

namespace PresentationLayer.Controls.SideBar.Admin
{
    public partial class CtrlPanelProjectAdminNew : UserControl
    {
        private UserDTO user;

        private ProjectServices projectServices;
        private ProjectMemberServices projectMemberServices;
        private UserServices userServices;
        private ProjectMemberRoleServices projectMemberRoleServices;
        private ProjectStatusServices projectStatusServices;
        private ProjectPriorityServices projectPriorityServices;

        private List<ProjectForListDTO> projects;

        private ProjectDTO currentProject;
        private List<ProjectMemberDTO> currentProjectMembers;

        private List<ProjectMemberRoleDTO> projectMemberRoles;
        private List<ProjectStatusDTO> projectStatuses;
        private List<ProjectPriorityDTO> projectPriorities;

        private Timer debounceTimer;

        public CtrlPanelProjectAdminNew()
        {
            this.user = UserSession.Instance.User;

            InitializeComponent();
        }

        private void InitControls()
        {
            this.Dock = DockStyle.Fill;

            dgvItems.AutoGenerateColumns = false;
            dgvItems.ReadOnly = true;
            dgvItems.MultiSelect = false;
            dgvItems.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvItems.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            cbMember.DisplayMember = "Username";
            cbMember.ValueMember = "Id";
            cbRole.DisplayMember = "Name";
            cbRole.ValueMember = "Id";
            cbPriority.DisplayMember = "Name";
            cbPriority.ValueMember = "Id";
            cbStatus.DisplayMember = "Name";
            cbStatus.ValueMember = "Id";

            dgvItems.Columns.Clear();
            dgvItems.Columns.Add(new DataGridViewTextBoxColumn() { DataPropertyName = "Id", HeaderText = "Id", Width = 50, Name = "Id" });
            dgvItems.Columns.Add(new DataGridViewTextBoxColumn() { DataPropertyName = "Name", HeaderText = "Project Name", Width = 200 });
            dgvItems.Columns.Add(new DataGridViewTextBoxColumn() { DataPropertyName = "ProjectCode", HeaderText = "Project Code", Width = 100 });
            dgvItems.Columns.Add(new DataGridViewTextBoxColumn() { DataPropertyName = "StartDate", HeaderText = "Start Date", Width = 100 });
            dgvItems.Columns.Add(new DataGridViewTextBoxColumn()
            {
                DataPropertyName = "StatusId",
                HeaderText = "Status",
                Width = 100,
                Name = "Status",
                CellTemplate = new DataGridViewTextBoxCell()
            });
            dgvItems.Columns.Add(new DataGridViewTextBoxColumn()
            {
                DataPropertyName = "PriorityId",
                HeaderText = "Priority Id",
                Width = 100,
                Name = "Priority",
                CellTemplate = new DataGridViewTextBoxCell()
            });
            dgvItems.Columns.Add(new DataGridViewTextBoxColumn() { DataPropertyName = "PercentComplete", HeaderText = "Percent Complete", Width = 100 });
            dgvItems.Columns.Add(new DataGridViewTextBoxColumn() { DataPropertyName = "IsDeleted", HeaderText = "Is Deleted", Width = 100 });
            dgvItems.CellFormatting += (s, e) =>
            {
                if (e.ColumnIndex == dgvItems.Columns["Status"].Index && e.Value != null)
                {
                    int statusId = (int)e.Value;
                    e.Value = ProjectStatusEnumExtensions.ToString(statusId);
                    e.FormattingApplied = true;
                }

                if (e.ColumnIndex == dgvItems.Columns["Priority"].Index && e.Value != null)
                {
                    int statusId = (int)e.Value;
                    e.Value = ProjectPriorityEnumExtensions.ToString(statusId);
                    e.FormattingApplied = true;
                }
            };
        }

        private void InitServices()
        {
            projectServices = new ProjectServices();
            projectMemberServices = new ProjectMemberServices();
            userServices = new UserServices();

            projectMemberRoleServices = new ProjectMemberRoleServices();
            projectStatusServices = new ProjectStatusServices();
            projectPriorityServices = new ProjectPriorityServices();
        }

        private void LoadProjects()
        {
            try
            {
                projects = projectServices.GetAllProjectsForListInlcudeInActive("");

                if (projects != null && projects.Count > 0)
                {
                    dgvItems.DataSource = projects;

                    dgvItems.Rows[0].Selected = true;
                }
                else
                    MessageBox.Show("No projects found.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (SqlException ex)
            {
                MessageBox.Show("Database error occurred while retrieving projects.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            } 
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while retrieving projects.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadRole()
        {
            projectMemberRoles = projectMemberRoleServices.GetAllProjectMemberRoles("");
            cbRole.DataSource = projectMemberRoles;
        }

        private void LoadStatus()
        {
            projectStatuses = projectStatusServices.GetAllProjectStatuses("");
            cbStatus.DataSource = projectStatuses;
        }

        private void LoadPriority()
        {
            projectPriorities = projectPriorityServices.GetAllProjectPriorities("");

            cbPriority.DataSource = projectPriorities;
        }

        private void SetProjectInfo()
        {
            tbProjectName.Text = currentProject.Name;
            tbProjectCode.Text = currentProject.ProjectCode;
            tbProjectDesrciption.Text = currentProject.Description;
            datePickerStart.Value = (DateTime)currentProject.StartDate;
            datePickerEnd.Value = (DateTime)currentProject.EndDate;
            tbBudget.Text = currentProject.Budget.ToString();
            cbStatus.SelectedValue = currentProject.StatusId;
            cbPriority.SelectedValue = currentProject.PriorityId;

            currentProjectMembers = projectMemberServices.GetProjectMembersById(currentProject.Id);
            listviewMembers.Items.Clear();
            listviewMembers.BeginUpdate();
            foreach (ProjectMemberDTO member in currentProjectMembers)
            {
                UserDTO user = userServices.GetUserById(member.UserId);
                ProjectMemberRoleDTO role = projectMemberRoles.FirstOrDefault(r => r.Id == member.RoleInProject);
                if (user != null && role != null)
                {
                    listviewMembers.Items.Add(new ListViewItem(new string[] { user.Id.ToString(), user.LastName, role.Name }));
                }
            }
            listviewMembers.EndUpdate();
            listviewMembers.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            listviewMembers.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);

            cbStatus.SelectedValue = currentProject.StatusId;
            cbPriority.SelectedValue = currentProject.PriorityId;
        }

        private void CtrlPanelTaskAdminNew_Load(object sender, EventArgs e)
        {
            InitControls();
            InitServices();
            LoadRole();
            LoadStatus();
            LoadPriority();
            LoadProjects();
        }

        private void dgvItems_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvItems.SelectedRows.Count == 1 && dgvItems.SelectedRows[0].Cells["Id"].Value != null)
            {
                DataGridViewRow item = dgvItems.SelectedRows[0];

                try
                {
                    currentProject = projectServices.GetProjectById((int)item.Cells["Id"].Value);
                    SetProjectInfo();
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

        private void btnRemoveMember_Click(object sender, System.EventArgs e)
        {
            if (listviewMembers.SelectedItems.Count > 0)
            {
                // Remove the selected item from the ListView
                listviewMembers.Items.Remove(listviewMembers.SelectedItems[0]);
            }
            else
            {
                MessageBox.Show("Please select a member to remove.");
            }

        }

        private void btnAddMember_Click(object sender, System.EventArgs e)
        {
            if (cbMember.SelectedItem == null || cbRole.SelectedItem == null)
            {
                MessageBox.Show("Please select a user and a role.");
                return;
            }

            // Get the selected user from the combo box
            UserDTO selectedUser = (UserDTO)cbMember.SelectedItem;

            if (listviewMembers.Items.Cast<ListViewItem>().Any(item => item.SubItems[0].Text == selectedUser.Id.ToString()))
            {
                MessageBox.Show("User already exists in the list.");
                return;
            }

            // Get the selected role from the combo box
            ProjectMemberRoleDTO selectedRole = (ProjectMemberRoleDTO)cbRole.SelectedItem;

            listviewMembers.Items.Add(new ListViewItem(new string[]
            {
                selectedUser.Id.ToString(),
                selectedUser.LastName,
                selectedRole.Name
            }));
        }

        private void cbMember_KeyUp(object sender, KeyEventArgs e)
        {
            // Initialize the debounce timer if it hasn't been created yet
            if (debounceTimer == null)
            {
                debounceTimer = new Timer();
                debounceTimer.Interval = 1000;
                debounceTimer.Tick += (s, args) =>
                {
                    debounceTimer.Stop(); // Stop the timer to prevent repeated execution

                    // Fetch the user from the database
                    string kw = cbMember.Text;
                    List<UserDTO> users = userServices.GetAllUsers(kw);

                    // Set the data source of the combo box to the list of users
                    if (users != null)
                    {
                        cbMember.DataSource = users;
                        cbMember.SelectedIndex = -1;
                    }

                };
            }

            // Restart the timer on every text change
            debounceTimer.Stop();
            debounceTimer.Start();
        }

        private bool ValidateProjectInput()
        {
            // Validate project name
            if (string.IsNullOrWhiteSpace(tbProjectName.Text))
            {
                MessageBox.Show("Project name is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tbProjectName.Focus();
                return false;
            }

            // Validate project code
            if (string.IsNullOrWhiteSpace(tbProjectCode.Text))
            {
                MessageBox.Show("Project code is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tbProjectCode.Focus();
                return false;
            }

            // Validate budget
            if (string.IsNullOrWhiteSpace(tbBudget.Text) || !decimal.TryParse(tbBudget.Text, out decimal budget) || budget <= 0)
            {
                MessageBox.Show("Please enter a valid budget amount.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tbBudget.Focus();
                return false;
            }

            // Validate date range
            if (datePickerEnd.Value <= datePickerStart.Value)
            {
                MessageBox.Show("End date must be after the start date.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                datePickerEnd.Focus();
                return false;
            }

            // Validate status selection
            if (cbStatus.SelectedValue == null)
            {
                MessageBox.Show("Please select a project status.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cbStatus.Focus();
                return false;
            }

            // Validate priority selection
            if (cbPriority.SelectedValue == null)
            {
                MessageBox.Show("Please select a project priority.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cbPriority.Focus();
                return false;
            }

            // Validate that at least one Manager exists in team members
            string managerId = listviewMembers.Items.Cast<ListViewItem>()
                .FirstOrDefault(item => item.SubItems[2].Text == "Manager")?.SubItems[0].Text;

            if (string.IsNullOrEmpty(managerId))
            {
                MessageBox.Show("Please add a team member with Manager role.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // Validate that at least one team member exists
            if (listviewMembers.Items.Count == 0)
            {
                MessageBox.Show("Please add at least one team member to the project.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private int[] GetUserIdsNotInListView()
        {
            var listViewUserIds = listviewMembers.Items
                .Cast<ListViewItem>()
                .Select(item => int.Parse(item.SubItems[0].Text))
                .ToArray();

            var userIdsNotInListView = currentProjectMembers
                .Where(m => !listViewUserIds.Contains(m.UserId))
                .Select(m => m.UserId)
                .ToArray();

            return userIdsNotInListView;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {

            // Validate input before proceeding
            if (!ValidateProjectInput())
                return;

            string managerId = listviewMembers.Items.Cast<ListViewItem>()
                .FirstOrDefault(item => item.SubItems[2].Text == "Manager")?.SubItems[0].Text;

            ProjectDTO project = new ProjectDTO
            {
                Id = currentProject.Id,
                Name = tbProjectName.Text,
                ProjectCode = tbProjectCode.Text,
                Description = tbProjectDesrciption.Text,
                StartDate = datePickerStart.Value,
                EndDate = datePickerEnd.Value,
                Budget = decimal.Parse(tbBudget.Text),
                StatusId = int.Parse(cbStatus.SelectedValue.ToString()),
                ManagerId = int.Parse(managerId),
                PriorityId = int.Parse(cbPriority.SelectedValue.ToString()),
                CreatedBy = user.Id
            };

            try
            {
                var res = projectServices.UpdateProject(project);
                if (res == true)
                {
                    var userIdsNotInListView = GetUserIdsNotInListView();

                    foreach (ListViewItem item in listviewMembers.Items)
                    {
                        if (item.SubItems[2].Text == "Manager")
                            continue;

                        // Create a new project member
                        ProjectMemberDTO projectMemberDTO = new ProjectMemberDTO
                        {
                            UserId = int.Parse(item.SubItems[0].Text),
                            RoleInProject = projectMemberRoles.First(r => r.Name == item.SubItems[2].Text).Id,
                            ProjectId = currentProject.Id,
                            CreatedDate = System.DateTime.Today,
                        };

                        // Add project member to the database
                        var updateRes = projectMemberServices.UpdateProjectMember(projectMemberDTO, userIdsNotInListView);
                        if (updateRes == false)
                        {
                            MessageBox.Show("Failed to update project member.");
                            return;
                        }
                    }
                }

                // Clear the form fields
                tbProjectName.Clear();
                tbProjectCode.Clear();
                tbBudget.Clear();
                tbProjectDesrciption.Clear();
                cbMember.SelectedItem = null;
                cbRole.SelectedItem = null;

                //Readload Project List
                MessageBox.Show("Remember Reload Project List");

            }
            catch (SqlException ex)
            {
                MessageBox.Show($"An unexpected error occurred: {ex.Message}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An unexpected error occurred: {ex.Message}");
            }
        }

        private void splitContainer1_SplitterMoved(object sender, SplitterEventArgs e)
        {

        }

        private void btCreateProject_Click(object sender, EventArgs e)
        {
            // Validate input before proceeding
            if (!ValidateProjectInput())
                return;

            string managerId = listviewMembers.Items.Cast<ListViewItem>()
                .FirstOrDefault(item => item.SubItems[2].Text == "Manager")?.SubItems[0].Text;

            ProjectDTO project = new ProjectDTO
            {
                Name = tbProjectName.Text,
                ProjectCode = tbProjectCode.Text,
                Description = tbProjectDesrciption.Text,
                StartDate = datePickerStart.Value,
                EndDate = datePickerEnd.Value,
                Budget = decimal.Parse(tbBudget.Text),
                StatusId = int.Parse(cbStatus.SelectedValue.ToString()),
                ManagerId = int.Parse(managerId),
                PriorityId = int.Parse(cbPriority.SelectedValue.ToString()),
                //PercentComplete = 0, --> do this on create service
                CreatedBy = user.Id,
                CreatedDate = System.DateTime.Today,
            };

            try
            {
                ProjectDTO newProj = projectServices.CreateProject(project);

                foreach (ListViewItem item in listviewMembers.Items)
                {
                    if (item.SubItems[2].Text == "Manager")
                        continue;

                    // Create a new project member
                    ProjectMemberDTO projectMemberDTO = new ProjectMemberDTO
                    {
                        UserId = int.Parse(item.SubItems[0].Text),
                        RoleInProject = projectMemberRoles.First(r => r.Name == item.SubItems[2].Text).Id,
                        ProjectId = newProj.Id,
                        CreatedDate = System.DateTime.Today,
                    };

                    // Add project member to the database
                    bool isAddedToProject = projectMemberServices.CreateMemberToProject(projectMemberDTO);
                    if (!isAddedToProject)
                    {
                        MessageBox.Show("Member is already in this project");
                    }
                }

                // Clear the form fields
                tbProjectName.Clear();
                tbProjectCode.Clear();
                tbBudget.Clear();
                tbProjectDesrciption.Clear();
                cbMember.SelectedItem = null;
                cbRole.SelectedItem = null;
                listviewMembers.Items.Clear();
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"An unexpected error occurred: {ex.Message}");
            }
        }

        private void btCancelProject_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void tbSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    string keyword = tbSearch.Text.Trim();
                    keyword = string.IsNullOrEmpty(keyword) ? "" : keyword;

                    int pageSize = GlobalVariables.PageSize;
                    projects = projectServices.GetAllProjectsForListInlcudeInActive(keyword);

                    dgvItems.DataSource = projects;
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
