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
    public partial class CtrlPanelDepartment : UserControl
    {
        private DepartmentServices departmentServices;
        public CtrlPanelDepartment()
        {
            InitializeComponent();
        }
        private void CtrlPanelDepartment_Load(object sender, EventArgs e)
        {
            InitControl();
            InitServices();
            LoadDepartments();
        }
        private void InitControl()
        {
            dgvDepartment.AllowUserToAddRows = true;
            dgvDepartment.AllowUserToDeleteRows = false;
            dgvDepartment.EditMode = DataGridViewEditMode.EditProgrammatically;
        }
        private void InitServices()
        {
            departmentServices = new DepartmentServices();
        }
        private void LoadDepartments()
        {
            // Load departments from the database
            departmentServices = new DepartmentServices();
            //List<DepartmentDTO> departments = departmentServices.GetAllDepartments();
            //if (departments == null || departments.Count == 0)
            //{
            //    MessageBox.Show("No departments found.");
            //    return;
            //}
            //// Bind data to DataGridView
            //dgvDepartment.DataSource = departments;
        }
    }
}
