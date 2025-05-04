using Bunifu.UI.WinForms.Helpers.Transitions;
using BusinessLayer;
using BusinessLayer.Services;
using DataLayer.Domain;
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
    public partial class CtrlPanelProjectStatusNew : UserControl
    {
        private readonly UserDTO user;

        private readonly ProjectStatusServices projectStatusServices = new();

        private List<ProjectStatusDTO> projectStatuses;

        private ProjectStatusDTO currentItem;

        public CtrlPanelProjectStatusNew()
        {
            this.user = UserSession.Instance.User;
            InitializeComponent();
        }
        private void CtrlPanelProjectStatus_Load(object sender, EventArgs e)
        {
            InitControl();
            try
            {
                LoadProjectStatuses();
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Database error occurred while loading project statuses: " + ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while loading project statuses: " + ex.Message);
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
        private void LoadProjectStatuses()
        {
            projectStatuses = projectStatusServices.GetAllProjectStatusesInlcudeInActive("");
            dgvItems.DataSource = projectStatuses;
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
                currentItem = dgvItems.SelectedRows[0].DataBoundItem as ProjectStatusDTO;
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
                ProjectStatusDTO newProjectStatus = new ProjectStatusDTO()
                {
                    Name = tbId.Text,
                    Description = tbDescription.Text,
                };

                isRefresh = projectStatusServices.CreateProjectStatus(newProjectStatus);
            }
            else
            {
                currentItem.Name = tbName.Text;
                currentItem.Description = tbDescription.Text;

                isRefresh = projectStatusServices.UpdateProjectStatus(currentItem);
            }

            if (isRefresh)
            {
                MessageBox.Show("Project status saved successfully.");
                LoadProjectStatuses();
            }
            else
            {
                MessageBox.Show("Failed to save project status.");
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
