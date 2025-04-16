using BusinessLayer.Services;
using BusinessLayer;
using DataLayer.Domain;
using DTOLayer.Models;
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

namespace PresentationLayer
{
    public partial class FormProjectDetail : Form
    {
        public UserDTO user;
        private ProjectDTO projectDTO;

        private ProjectServices projectServices;
        private ProjectMemberRoleServices projectMemberRoleServices;
        private ProjectStatusServices projectStatusServices;
        private UserServices userServices;
        private ProjectMemberServices projectMemberServices;
        private ProjectPriorityServices projectPriorityServices;

        private List<ProjectMemberDTO> projectMembers;
        private List<ProjectMemberRoleDTO> projectMemberRoles;
        private ProjectStatusDTO projectStatusDTO;
        private ProjectPriorityDTO projectPriorityDTO;

        private Timer debounceTimer;

        public FormProjectDetail(ProjectDTO projectDTO)
        {
            InitializeComponent();

            this.projectDTO = projectDTO;
        }

        private void FormProjectDetail_Load(object sender, EventArgs e)
        {
            this.Dock = DockStyle.Fill;

            projectServices = new ProjectServices();
            projectMemberRoleServices = new ProjectMemberRoleServices();
            projectStatusServices = new ProjectStatusServices();
            projectMemberServices = new ProjectMemberServices();
            projectPriorityServices = new ProjectPriorityServices();

            userServices = new UserServices();

            cbPriority.DisplayMember = "Name";
            cbPriority.ValueMember = "Id";
            cbStatus.DisplayMember = "Name";
            cbStatus.ValueMember = "Id";

            loadProjectInfo();
            setProjectInfo();
        }

        private void loadProjectInfo()
        {
            try
            {
                projectMemberRoles = projectMemberRoleServices.GetAllProjectMemberRoles();
                projectStatusDTO = projectStatusServices.GetById(projectDTO.Id);
                projectPriorityDTO = projectPriorityServices.GetById(projectDTO.PriorityId);
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Database error: " + ex.Message);
                this.Dispose();
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                this.Dispose();
                return;
            }
        }

        private void setProjectInfo()
        {
            tbProjectName.Text = projectDTO.Name;
            tbProjectCode.Text = projectDTO.ProjectCode;
            tbProjectDesrciption.Text = projectDTO.Description;
            datePickerStart.Value = (DateTime)projectDTO.StartDate;
            datePickerEnd.Value = (DateTime)projectDTO.EndDate;
            tbBudget.Text = projectDTO.Budget.ToString();

            cbStatus.SelectedText = projectStatusDTO.Name;
            cbPriority.SelectedText = projectPriorityDTO.Name;

            projectMembers = projectMemberServices.GetProjectMembersById(projectDTO.Id);
            listviewMembers.Items.Clear();
            listviewMembers.BeginUpdate();
            foreach (ProjectMemberDTO member in projectMembers)
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

            cbStatus.SelectedValue = projectDTO.StatusId;
            cbPriority.SelectedValue = projectDTO.PriorityId;
        }
    }
}
