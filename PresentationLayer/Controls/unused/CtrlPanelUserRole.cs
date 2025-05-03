using BusinessLayer.Services;
using DTOLayer;
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
    public partial class CtrlPanelUserRole : UserControl
    {
        private UserRoleServices userRoleServices;
        public CtrlPanelUserRole()
        {
            InitializeComponent();
        }
       private void CtrlPanelUserRole_Load(object sender, EventArgs e)
        {
            InitControl();
            InitServices();
            LoadUserRoles();
        }
        private void InitControl()
        {
            dgvUserRole.AllowUserToAddRows = true;
            dgvUserRole.AllowUserToDeleteRows = false;
            dgvUserRole.EditMode = DataGridViewEditMode.EditProgrammatically;
        }
        private void InitServices()
        {
            // Initialize any services if needed
        }
        private void LoadUserRoles()
        {
            // Load user roles from the database
            UserRoleServices userRoleServices = new UserRoleServices();
            //List<UserRoleDTO> userRoles = userRoleServices.GetAllUserRoles();
            //if (userRoles == null || userRoles.Count == 0)
            //{
            //    MessageBox.Show("No user roles found.");
            //    return;
            //}
            //// Bind data to DataGridView
            //dgvUserRole.DataSource = userRoles;
        }
    }
}
