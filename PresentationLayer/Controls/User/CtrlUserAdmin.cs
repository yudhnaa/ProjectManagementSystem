using BusinessLayer;
using BusinessLayer.Services;
using DTOLayer;
using DTOLayer.Models;
using PresentationLayer.CustomControls;
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

namespace PresentationLayer.Controls
{
    public partial class CtrlUserAdmin : UserControl
    {
        private UserExtraInfoDTO user;
        private UserRoleDTO userRoleDTO;
        private DepartmentDTO userDepartmentDTO;

        private UserServices userServices;
        private UserRoleServices userRoleServices;
        private DepartmentServices departmentServices;

        public CtrlUserAdmin(UserExtraInfoDTO user)
        {
            this.user = user;

            InitializeComponent();

        }

        private void Style()
        {
            this.Dock = DockStyle.Fill;
            this.Margin = new Padding(50, 0, 50, 10);

            tableLayoutPanel1.ColumnStyles.Clear();
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));

            lbUsername.Anchor = AnchorStyles.Left;
            lbName.Anchor = AnchorStyles.Left | AnchorStyles.Top;
        }

        private void InitServices()
        {
            userRoleServices = new UserRoleServices();
            departmentServices = new DepartmentServices();
            userServices = new UserServices();
        }

        private void ctrl_User_Load(object sender, EventArgs e)
        {

            if (user == null)
            {
                MessageBox.Show("Task not found");
                return;
            }

            Style();
            InitServices();
            LoadUserData();
            SetUserData();

        }

        private void LoadUserData()
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

        private void SetUserData()
        {
            lbUsername.Text = user.Username;
            lbName.Text = String.Format("Full Name: {0} {1}", user.LastName, user.LastName);

            lbRoleName.Text = userRoleDTO.Name;
            lbDepartment.Text = userDepartmentDTO.Name;

            lbStatus.Text = (bool)user.IsActive ? "Active" : "Inactive";
            lbStatus._BackColor = Utils.Utils.GetUserStatusColor(lbStatus.Text);
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            //FormUserInfoEdit formUserInfoEdit = new FormUserInfoEdit(user);
            //formUserInfoEdit.ShowDialog();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are you sure you want to delete this user?", "Delete User", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dialogResult == DialogResult.Yes)
            {
                try
                {
                    bool res = userServices.DeleteUserById(user.Id);
                    
                    if (!res)
                    {
                        MessageBox.Show("User not found or already deleted.");
                        return;
                    }

                    MessageBox.Show("User deleted successfully.");
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Error deleting user: " + ex.Message);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error deleting user: " + ex.Message);
                }
            }
        }
    }
}
