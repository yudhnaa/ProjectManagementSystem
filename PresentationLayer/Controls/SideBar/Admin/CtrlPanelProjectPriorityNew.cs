using Bunifu.UI.WinForms.Helpers.Transitions;
using BusinessLayer;
using BusinessLayer.Services;
using C1.Win.Localization.Design;
using DataLayer.Domain;
using DTOLayer.Models;
using PresentationLayer.AppContext;
using PresentationLayer.Config;
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
    public partial class CtrlPanelProjectPriorityNew : UserControl
    {
        private UserDTO user;

        private ProjectPriorityServices projectPriorityServices;

        private List<ProjectPriorityDTO> projectPriorities;

        private ProjectPriorityDTO currentItem;

        public CtrlPanelProjectPriorityNew()
        {
            this.user = UserSession.Instance.User;
            InitializeComponent();
        }
        private void CtrlPanelProjectPriorityNew_Load(object sender, EventArgs e)
        {
            InitControl();
            InitServices();
            try
            {
                LoadProjectPriorities();
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Database error occurred while loading project priority: " + ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while loading project priority: " + ex.Message);
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

        private void InitServices()
        {
            projectPriorityServices = new ProjectPriorityServices();
        }

        private void LoadProjectPriorities()
        {
            projectPriorities = projectPriorityServices.GetAllProjectPrioritiesInlcudeInActive("");
            dgvItems.DataSource = projectPriorities;
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
                currentItem = dgvItems.SelectedRows[0].DataBoundItem as ProjectPriorityDTO;
                SetSelectedItemData();
            }

        }

        private void btCreateProject_Click(object sender, EventArgs e)
        {
            tbId.Text = "0";
            tbName.Text = string.Empty;
            tbDescription.Text = string.Empty;
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(tbId.Text))
            {
                MessageBox.Show("Project status name is required.");
                return false;
            }
            if (string.IsNullOrWhiteSpace(tbDescription.Text))
            {
                MessageBox.Show("Project status description is required.");
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
                ProjectPriorityDTO newProjectPriority = new ProjectPriorityDTO()
                {
                    Name = tbId.Text,
                    Description = tbDescription.Text,
                };

                isRefresh = projectPriorityServices.CreateProjectPriority(newProjectPriority);
            }
            else
            {
                currentItem.Name = tbName.Text;
                currentItem.Description = tbDescription.Text;

                isRefresh = projectPriorityServices.UpdateProjectPriority(currentItem);
            }

            if (isRefresh)
            {
                MessageBox.Show("Project status saved successfully.");
                LoadProjectPriorities();
            }
            else
            {
                MessageBox.Show("Failed to save project status.");
            }
        }

        private void tbSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    string keyword = tbSearch.Text.Trim();
                    keyword = string.IsNullOrEmpty(keyword) ? null : keyword;
                    int pageSize = GlobalVariables.PageSize;

                    projectPriorities = projectPriorityServices.GetAllProjectPrioritiesInlcudeInActive(keyword);

                    dgvItems.DataSource = projectPriorities;
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

        private void splitContainer1_Paint(object sender, PaintEventArgs e)
        {
            SplitContainer s = sender as SplitContainer;
            if (s != null)
            {
                e.Graphics.FillRectangle(Brushes.LightGray, s.SplitterRectangle);
            }
        }
    }
}
