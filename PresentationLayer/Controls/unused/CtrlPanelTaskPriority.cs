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
    public partial class CtrlPanelTaskPriority : UserControl
    {
        private TaskPriorityServices taskPriorityServices;
        public CtrlPanelTaskPriority()
        {
            InitializeComponent();
        }
        private void CtrlPanelTaskPriority_Load(object sender, EventArgs e)
        {
            InitControl();
            InitServices();
            LoadTaskPriorities();
        }
        private void InitControl()
        {
            dgvTaskPriority.AllowUserToAddRows = true;
            dgvTaskPriority.AllowUserToDeleteRows = false;
            dgvTaskPriority.EditMode = DataGridViewEditMode.EditProgrammatically;
        }
        private void InitServices()
        {
            taskPriorityServices = new TaskPriorityServices();
        }
        private void LoadTaskPriorities()
        {
            // Load task priorities from the database
            taskPriorityServices = new TaskPriorityServices();
            List<TaskPriorityDTO> taskPriorities = taskPriorityServices.GetAllTaskPrioritiesInlcudeInActive("");
            if (taskPriorities == null || taskPriorities.Count == 0)
            {
                MessageBox.Show("No currentTask priorities found.");
                return;
            }
            // Bind data to DataGridView
            dgvTaskPriority.DataSource = taskPriorities;
        }
    }
}
