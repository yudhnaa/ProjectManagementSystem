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
using System.Windows.Forms;

namespace PresentationLayer.Controls.SideBar.User
{
    public partial class CtrlPanelProjectNew : UserControl
    {
        private readonly UserDTO user;

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
        private Dictionary<int, UserDTO> userCache = new Dictionary<int, UserDTO>();

        public CtrlPanelProjectNew()
        {
            user = UserSession.Instance.User;
            InitializeComponent();
        }

        private void CtrlPanelTaskAdminNew_Load(object sender, EventArgs e)
        {
            InitControls();
            InitServices();
            LoadRoles();
            LoadProjects();
        }

        private void InitControls()
        {
            Dock = DockStyle.Fill;
            ConfigureDataGridView();
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

        private void ConfigureDataGridView()
        {
            dgvItems.AutoGenerateColumns = false;
            dgvItems.ReadOnly = true;
            dgvItems.MultiSelect = false;
            dgvItems.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvItems.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            dgvItems.Columns.Clear();
            dgvItems.Columns.AddRange(new DataGridViewColumn[]
            {
                new DataGridViewTextBoxColumn { DataPropertyName = "Id", HeaderText = "Id", Width = 50, Name = "Id" },
                new DataGridViewTextBoxColumn { DataPropertyName = "Name", HeaderText = "Project Name", Width = 200 },
                new DataGridViewTextBoxColumn { DataPropertyName = "ProjectCode", HeaderText = "Project Code", Width = 100 },
                new DataGridViewTextBoxColumn { DataPropertyName = "StartDate", HeaderText = "Start Date", Width = 100 },
                CreateEnumTextColumn("StatusId", "Status", ProjectStatusEnumExtensions.ToString),
                CreateEnumTextColumn("PriorityId", "Priority", ProjectPriorityEnumExtensions.ToString),
                new DataGridViewTextBoxColumn { DataPropertyName = "PercentComplete", HeaderText = "Percent Complete", Width = 100 },
                new DataGridViewTextBoxColumn { DataPropertyName = "IsDeleted", HeaderText = "Is Deleted", Width = 100 }
            });
        }

        private DataGridViewTextBoxColumn CreateEnumTextColumn(string propertyName, string headerText, Func<int, string> enumToString)
        {
            var column = new DataGridViewTextBoxColumn
            {
                DataPropertyName = propertyName,
                HeaderText = headerText,
                Name = headerText,
                Width = 100
            };

            dgvItems.CellFormatting += (s, e) =>
            {
                if (e.ColumnIndex == dgvItems.Columns[headerText].Index && e.Value is int intValue)
                {
                    e.Value = enumToString(intValue);
                    e.FormattingApplied = true;
                }
            };

            return column;
        }

        private void LoadProjects()
        {
            try
            {
                projects = projectServices.GetProjectsForListByUserId(user.Id);

                if (projects?.Any() == true)
                {
                    dgvItems.DataSource = projects;
                    dgvItems.Rows[0].Selected = true;
                }
                else
                {
                    MessageBox.Show("No projects found.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (SqlException)
            {
                MessageBox.Show("Database error occurred while retrieving projects.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception)
            {
                MessageBox.Show("An error occurred while retrieving projects.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadRoles()
        {
            projectMemberRoles = projectMemberRoleServices.GetAllProjectMemberRoles("");
        }

        private void LoadProjectDetail(int projectId)
        {
            try
            {
                currentProject = projectServices.GetProjectById(projectId);
                SetProjectInfo();
            }
            catch (SqlException)
            {
                MessageBox.Show("Database error occurred while retrieving project details.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception)
            {
                MessageBox.Show("An error occurred while retrieving project details.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetProjectInfo()
        {
            lbShortDescription.Text = $"Project: {currentProject.Name} / Code: {currentProject.ProjectCode}";
            lbName.Text = currentProject.Name;
            tbProjectCode.Text = currentProject.ProjectCode;
            tbProjectDesrciption.Text = currentProject.Description;
            datePickerStart.Value = currentProject.StartDate ?? DateTime.Now;
            datePickerEnd.Value = currentProject.EndDate ?? DateTime.Now;
            tbBudget.Text = currentProject.Budget.ToString();

            lbPriority.Text = TaskPriorityEnumExtensions.ToString(currentProject.PriorityId);
            lbPriority._BackColor = Utils.Utils.GetPriorityColor(lbPriority.Text);

            lbStatus.Text = TaskStatusEnumExtensions.ToString(currentProject.StatusId);
            lbStatus._BackColor = Utils.Utils.GetStatusColor(lbStatus.Text);

            currentProjectMembers = projectMemberServices.GetProjectMembersById(currentProject.Id);
            listviewMembers.BeginUpdate();
            listviewMembers.Items.Clear();

            foreach (var member in currentProjectMembers)
            {
                if (!userCache.TryGetValue(member.UserId, out var memberUser))
                {
                    memberUser = userServices.GetUserById(member.UserId);
                    if (memberUser != null) userCache[member.UserId] = memberUser;
                }

                var role = projectMemberRoles.FirstOrDefault(r => r.Id == member.RoleInProject);
                if (memberUser != null && role != null)
                {
                    listviewMembers.Items.Add(new ListViewItem(new[] { memberUser.Id.ToString(), memberUser.LastName, role.Name }));
                }
            }

            listviewMembers.EndUpdate();
            listviewMembers.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            listviewMembers.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
        }

        private void dgvItems_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvItems.SelectedRows.Count == 1 && dgvItems.SelectedRows[0].Cells["Id"].Value is int projectId)
            {
                LoadProjectDetail(projectId);
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
                MessageBox.Show("Please select a member to remove.");
            }
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

        private void SearchProjects(string keyword)
        {
            try
            {
                projects = projectServices.GetAllProjectsForList(keyword ?? "");
                dgvItems.DataSource = projects;
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"Database error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void splitContainer1_Paint(object sender, PaintEventArgs e)
        {
            if (sender is SplitContainer s)
            {
                e.Graphics.FillRectangle(Brushes.LightGray, s.SplitterRectangle);
            }
        }
    }
}
