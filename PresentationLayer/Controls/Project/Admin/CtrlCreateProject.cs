using BusinessLayer;
using BusinessLayer.Services;
using DataLayer.Domain;
using DTOLayer;
using DTOLayer.Models;
using PresentationLayer.AppContext;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;

namespace PresentationLayer.CustomControls
{
    public partial class CtrlCreateProject : UserControl
    {
        public UserDTO user;

        private ProjectServices projectServices;
        private ProjectMemberRoleServices projectMemberRoleServices;
        private ProjectStatusServices projectStatusServices;
        private UserServices userServices;
        private ProjectMemberServices  projectMemberServices;
        private ProjectPriorityServices projectPriorityServices;

        private List<ProjectMemberRoleDTO> projectMemberRoles;
        private List<ProjectStatusDTO> projectStatuses;
        private List<ProjectPriorityDTO> projectPriorities;

        private Timer debounceTimer;

        public CtrlCreateProject(UserDTO user)
        {
            this.user = UserSession.Instance.User;

            InitializeComponent();

        }

        private void ctrlCreateProject_Load(object sender, System.EventArgs e)
        {
            this.Dock = DockStyle.Fill;

            projectServices = new ProjectServices();
            projectMemberRoleServices = new ProjectMemberRoleServices();
            projectStatusServices = new ProjectStatusServices();
            projectMemberServices = new ProjectMemberServices();
            projectPriorityServices = new ProjectPriorityServices();

            userServices = new UserServices();

            cbMember.DisplayMember = "Username";
            cbMember.ValueMember = "Id";
            cbRole.DisplayMember = "Name";
            cbRole.ValueMember = "Id";
            cbPriority.DisplayMember = "Name";
            cbPriority.ValueMember = "Id";
            cbStatus.DisplayMember = "Name";
            cbStatus.ValueMember = "Id";

            loadRole();
            loadStatus();
            loadPriority();

        }

        private void loadRole()
        {
            projectMemberRoles = projectMemberRoleServices.GetAllProjectMemberRoles("");

            cbRole.DataSource = projectMemberRoles;
        }

        private void loadStatus()
        {
            projectStatuses = projectStatusServices.GetAllProjectStatuses("");

            cbStatus.DataSource = projectStatuses;
        }

        private void loadPriority()
        {
            projectPriorities = projectPriorityServices.GetAllProjectPriorities("");

            cbPriority.DataSource = projectPriorities;
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


        private void btCreateProject_Click(object sender, System.EventArgs e)
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

        private void cbMember_TextChanged(object sender, System.EventArgs e)
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
                    int pageSize = 10;
                    List<UserDTO> users = userServices.GetAllUsers(kw);

                    // Set the data source of the combo box to the list of users
                    cbMember.DataSource = users;
                };
            }

            // Restart the timer on every text change
            debounceTimer.Stop();
            debounceTimer.Start();
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

        private void btCancelProject_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
