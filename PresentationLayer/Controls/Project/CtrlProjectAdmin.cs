using BusinessLayer.Services;
using DTOLayer.Models;
using PresentationLayer.AppContext;
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

namespace PresentationLayer.Controls.Project
{
    public partial class CtrlProjectAdmin : UserControl
    {
        private UserDTO user;
        private ProjectDTO project;

        private ProjectStatusServices projectStatusServices;
        private TaskServices taskServices;

        public CtrlProjectAdmin(ProjectDTO project)
        {
            this.project = project;
            this.user = UserSession.Instance.User;

            InitializeComponent();

        }

        private void CtrlProject_Load(object sender, EventArgs e)
        {
            if (project == null || user == null)
            {
                MessageBox.Show( "Project or user is null");
                this.Dispose();
                
                return;
            }

            projectStatusServices = new ProjectStatusServices();
            taskServices = new TaskServices();

            try
            {
                var projectStatus = projectStatusServices.GetById(project.StatusId);
                var tasksCount = taskServices.CountTaskByProjectId(project.Id);

                lbTitle.Text = project.Name;
                tbDescription.Text = project.Description;
                lbStatus.Text = projectStatus.Name;
                lbStatus._BackColor = Utils.Utils.GetStatusColor(projectStatus.Name);

                lbEndDate.Text = project.EndDate?.ToString("dd/MM/yyyy");
                lbTaskCount.Text = String.Format("Task count: {0}", tasksCount);
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

        private void lbTitle_Click(object sender, EventArgs e)
        {
            FormProjectDetail formProjectDetail = new FormProjectDetail(project);
            formProjectDetail.ShowDialog();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            FormProjectUpdate formProjectUpdate = new FormProjectUpdate(project);
            formProjectUpdate.ShowDialog();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            
        }
    }
}
