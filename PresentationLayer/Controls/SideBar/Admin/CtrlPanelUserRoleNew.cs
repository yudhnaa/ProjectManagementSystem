using Bunifu.UI.WinForms.Helpers.Transitions;
using BusinessLayer;
using BusinessLayer.Services;
using DataLayer.Domain;
using DTOLayer;
using DTOLayer.Models;
using PresentationLayer.AppContext;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;
using System.Windows.Forms;
using System.Xml.Linq;

namespace PresentationLayer.Controls.SideBar.Admin
{
    public partial class CtrlPanelUserRoleNew : UserControl
    {
        private UserDTO user;

        private readonly IUserRoleServices userRoleServices = new UserRoleServices();

        private List<UserRoleDTO> userRolees;

        private UserRoleDTO currentItem;

        public CtrlPanelUserRoleNew()
        {
            this.user = UserSession.Instance.User;
            InitializeComponent();
        }
        private void CtrlPanelUserRoleNew_Load(object sender, EventArgs e)
        {
            InitControl();
            try
            {
                LoadUserRolees();
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Database error occurred while loading user roles: " + ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while loading user roles: " + ex.Message);
            }

        }

        private void InitControl()
        {
            this.Dock = DockStyle.Fill;
            this.DoubleBuffered = true;

            dgvItems.AutoGenerateColumns = false;
            dgvItems.ReadOnly = true;
            dgvItems.MultiSelect = false;
            dgvItems.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvItems.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            dgvItems.Columns.Clear();
            dgvItems.Columns.Add(new DataGridViewTextBoxColumn() { DataPropertyName = "Id", HeaderText = "Id", Name = "Id", Width = 50, ReadOnly = true });
            dgvItems.Columns.Add(new DataGridViewTextBoxColumn() { DataPropertyName = "Name", HeaderText = "Name", Name = "Name", Width = 100 });
            dgvItems.Columns.Add(new DataGridViewTextBoxColumn() { DataPropertyName = "Description", HeaderText = "Description", Name = "Description", Width = 200 });

        }

        

        private void LoadUserRolees()
        {
            userRolees = userRoleServices.GetAllUserRolesInlcudeInActive("");
            dgvItems.DataSource = userRolees;
        }

        private void SetSelectedItemData()
        {
            if (currentItem == null) return;

            tbId.Text = currentItem.Id.ToString();
            tbName.Text = currentItem.Name;
            tbDescription.Text = currentItem.Description;
        }

        private void dgvItems_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvItems.SelectedRows.Count > 0)
            {
                currentItem = dgvItems.SelectedRows[0].DataBoundItem as UserRoleDTO;
                SetSelectedItemData();
            }

        }

        private void ResetControls()
        {
            tbId.Text = "0";
            tbName.Text = string.Empty;
            tbDescription.Text = string.Empty;
        }

        private void btCreateProject_Click(object sender, EventArgs e)
        {
            dgvItems.Rows[0].Selected = false;
            ResetControls();
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(tbId.Text))
            {
                MessageBox.Show("User role name is required.");
                return false;
            }
            if (string.IsNullOrWhiteSpace(tbDescription.Text))
            {
                MessageBox.Show("User role description is required.");
                return false;
            }
            return true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateInput()) return;

            bool isRefresh = false;

            if (tbId.Text.Equals("0"))
            {
                UserRoleDTO newUserRole = new UserRoleDTO()
                {
                    Name = tbName.Text,
                    Description = tbDescription.Text,
                };

                isRefresh = userRoleServices.CreateUserRole(newUserRole);
            }
            else
            {
                currentItem.Name = tbName.Text;
                currentItem.Description = tbDescription.Text;

                isRefresh = userRoleServices.UpdateUserRole(currentItem);
            }

            if (isRefresh)
            {
                MessageBox.Show("User role saved successfully.");
                LoadUserRolees();
            }
            else
            {
                MessageBox.Show("Failed to save user role.");
            }
        }

        private void splitContainer1_Paint(object sender, PaintEventArgs e)
        {
            SplitContainer s = sender as SplitContainer;
            if (s != null)
            {
                e.Graphics.FillRectangle(Brushes.LightGray, s.SplitterRectangle);
            }
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            dgvItems.Rows[0].Selected = true;
        }

        private void tbSearch_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    string keyword = string.IsNullOrEmpty(tbSearch.Text.Trim()) == true ? "" : tbSearch.Text.Trim();

                    userRolees = userRoleServices.GetAllUserRolesInlcudeInActive(keyword);

                    if (userRolees == null || userRolees.Count == 0)
                    {
                        MessageBox.Show("Không tìm thấy task nào với từ khóa này.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    dgvItems.DataSource = userRolees;
                }
                catch (SqlException ex)
                {
                    MessageBox.Show($"Lỗi cơ sở dữ liệu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        protected override void OnVisibleChanged(EventArgs e)
        {
            base.OnVisibleChanged(e);

            if (this.Visible)
            {
                LoadUserRolees();
            }
        }
    }
}
