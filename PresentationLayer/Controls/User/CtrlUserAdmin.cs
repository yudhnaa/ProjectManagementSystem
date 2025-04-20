using BusinessLayer;
using BusinessLayer.Services;
using DTOLayer;
using DTOLayer.Models;
using PresentationLayer.CustomControls;
using PresentationLayer.Forms.Other;
using PresentationLayer.Utils;
using RestSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace PresentationLayer.Control
{
    public partial class CtrlUserAdmin : UserControl
    {
        private UserExtraInfoDTO user;
        private UserRoleDTO userRoleDTO;
        private DepartmentDTO userDepartmentDTO;

        private UserRoleServices userRoleServices;
        private DepartmentServices departmentServices;

        public CtrlUserAdmin(UserExtraInfoDTO user)
        {
            this.user = user;

            InitializeComponent();

        }

        private void style()
        {
            this.Dock = DockStyle.Fill;
            this.Margin = new Padding(50, 0, 50, 10);

            tableLayoutPanel1.ColumnStyles.Clear();
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 80F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));

            lbUsername.Anchor = AnchorStyles.Left;
            lbName.Anchor = AnchorStyles.Left | AnchorStyles.Top;
        }

        private void ctrl_User_Load(object sender, EventArgs e)
        {

            if (user == null)
            {
                MessageBox.Show("Task not found");
                return;
            }

            style();

            userRoleServices = new UserRoleServices();
            departmentServices = new DepartmentServices();


            loadUserData();
            setUserData();

        }

        private void loadUserData()
        {
            try
            {
                userRoleDTO = userRoleServices.GetUserRoleById(user.UserRoleId);
                userDepartmentDTO = departmentServices.GetDepartmentById((int)user.DepartmentId);
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error loading user data: " + ex.Message);
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading user data: " + ex.Message);
                return;
            }
        }

        private void setUserData()
        {
            lbUsername.Text = user.Username;
            lbName.Text = String.Format("Full Name: {0} {1}", user.LastName, user.LastName);

            lbRoleName.Text = userRoleDTO.Name;
            lbDepartment.Text = userDepartmentDTO.Name;
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            FormUserInfoEdit formUserInfoEdit = new FormUserInfoEdit(user);
            formUserInfoEdit.ShowDialog();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {

        }
    }
}
