using BusinessLayer.Services;
using DTOLayer.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PresentationLayer.Controls.SideBar.Admin
{
    public partial class CtrlPanelProjectPriority : UserControl
    {
        private ProjectPriorityServices projectPriorityServices;
        public CtrlPanelProjectPriority()
        {
            InitializeComponent();
        }
        private void CtrlPanelProjectPriority_Load(object sender, EventArgs e)
        {
            InitControl();
            InitServices();
            LoadProjectPriorities();
        }
        private void InitControl()
        {
            dgvProjectPriority.AllowUserToAddRows = true;
            dgvProjectPriority.AllowUserToDeleteRows = false;
            dgvProjectPriority.EditMode = DataGridViewEditMode.EditProgrammatically;
        }
        private void InitServices()
        {
            projectPriorityServices = new ProjectPriorityServices();
        }
        private void LoadProjectPriorities()
        {
            // Load project priorities from the database
            projectPriorityServices = new ProjectPriorityServices();
            //List<ProjectPriorityDTO> projectPriorities = projectPriorityServices.GetAllProjectPriorities();
            //if (projectPriorities == null || projectPriorities.Count == 0)
            //{
            //    MessageBox.Show("No project priorities found.");
            //    return;
            //}
            //// Bind data to DataGridView
            //dgvProjectPriority.DataSource = projectPriorities;
        }
    }
}
