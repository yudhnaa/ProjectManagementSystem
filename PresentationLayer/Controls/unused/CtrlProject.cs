using BusinessLayer.Services;
using DTOLayer.Models;
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
    public partial class CtrlProject : UserControl
    {
        private ProjectDTO project;

        private ProjectStatusServices projectStatusServices;
        private TaskServices taskServices;

        public CtrlProject(ProjectDTO project)
        {
            InitializeComponent();

            this.project = project;
        }

         private void style()
        {
            this.Dock = DockStyle.Fill;
            this.Margin = new Padding(50, 0, 50, 10);

            tableLayoutPanel1.ColumnStyles.Clear();
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));

            lbTitle.Anchor = AnchorStyles.Left;
            lbTitle.Margin = new Padding(10, 10, 10, 10);
            lbTitle.Padding = new Padding(0, 0, 0, 0);

            lbDescription.Anchor = AnchorStyles.Left | AnchorStyles.Top;
            lbDescription.Margin = new Padding(13, 0, 0, 10);

        }

        private void CtrlProject_Load(object sender, EventArgs e)
        {
            if (project == null)
            {
                MessageBox.Show("Project is null");
                this.Dispose();
                
                return;
            }

            style();

            projectStatusServices = new ProjectStatusServices();
            taskServices = new TaskServices();

            try
            {
                var projectStatus = projectStatusServices.GetById(project.StatusId);
                var tasksCount = taskServices.CountTaskByProjectId(project.Id);

                lbTitle.Text = project.Name;
                lbDescription.Text = project.Description;
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
    }
}
