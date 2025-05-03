using BusinessLayer.Services;
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
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace PresentationLayer.Controls.SideBar.Admin
{
    public partial class CtrlPanelTaskStatus : UserControl
    {
        private TaskStatusServices taskStatusServices;

        public CtrlPanelTaskStatus()
        {
            InitializeComponent();
        }

        private void CtrlPanelTaskStatus_Load(object sender, EventArgs e)
        { 
            InitControl();
            InitServices();
            //LoadTaskStatuses();
        }

        private void InitControl()
        {
            dgvTaskStatus.AllowUserToAddRows = true;
            dgvTaskStatus.AllowUserToDeleteRows = false;
            //dgvTaskStatus.EditMode = DataGridViewEditMode.EditProgrammatically;
        }

        private void InitServices()
        {
            taskStatusServices = new TaskStatusServices();
        }

        private void LoadTaskStatuses()
        {
            //Load du lieu len
            taskStatusServices = new TaskStatusServices();
            List<TaskStatusDTO> taskStatuses = taskStatusServices.GetAllTaskStatusesInlcudeInActive("");

            if (taskStatuses == null || taskStatuses.Count == 0)
            {
                MessageBox.Show("No currentTask status found.");
                return;
            }

            //Them vao combobox
            dgvTaskStatus.DataSource = taskStatuses;
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            //de ti ve coi cai them da, lamf tiep liet ke danh sach may cai kia di ok 
            // them status moi

        }

    }
}
