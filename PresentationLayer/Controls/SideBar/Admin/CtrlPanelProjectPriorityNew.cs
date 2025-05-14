using BusinessLayer.Services;
using DataLayer.Domain;
using DTOLayer.Models;
using PresentationLayer.AppContext;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace PresentationLayer.Controls.SideBar.Admin
{
    public partial class CtrlPanelProjectPriorityNew : UserControl
    {
        private readonly UserDTO _user;
        private readonly IProjectPriorityServices projectPriorityServices = new ProjectPriorityServices();

        private List<ProjectPriorityDTO> _projectPriorities;
        private ProjectPriorityDTO _currentItem;

        public CtrlPanelProjectPriorityNew()
        {
            _user = UserSession.Instance.User;
            InitializeComponent();
        }

        private void CtrlPanelProjectPriorityNew_Load(object sender, EventArgs e)
        {
            try
            {
                InitializeControl();
                LoadProjectPriorities();
            }
            catch (SqlException ex)
            {
                ShowErrorMessage("Database error occurred while loading project priorities", ex);
            }
            catch (Exception ex)
            {
                ShowErrorMessage("An unexpected error occurred", ex);
            }
        }

        private void InitializeControl()
        {
            Dock = DockStyle.Fill;
            DoubleBuffered = true;

            ConfigureDataGridView();
        }

        private void ConfigureDataGridView()
        {
            dgvItems.AutoGenerateColumns = false;
            dgvItems.ReadOnly = true;
            dgvItems.MultiSelect = false;
            dgvItems.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvItems.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvItems.Columns.Clear();

            AddDataGridViewColumn("Id", "Id", 50);
            AddDataGridViewColumn("Name", "Name", 100);
            AddDataGridViewColumn("Description", "Description", 200);
        }

        private void AddDataGridViewColumn(string propertyName, string headerText, int width)
        {
            dgvItems.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = propertyName,
                HeaderText = headerText,
                Name = propertyName,
                Width = width,
                ReadOnly = true
            });
        }

        private void LoadProjectPriorities(string keyword = null)
        {
            try
            {
                _projectPriorities = projectPriorityServices.GetAllProjectPrioritiesInlcudeInActive(keyword);
                dgvItems.DataSource = _projectPriorities;
            }
            catch (Exception ex)
            {
                ShowErrorMessage("Error loading project priorities", ex);
            }
        }

        private void SetSelectedItemData()
        {
            if (_currentItem == null)
            {
                ClearForm();
                return;
            }

            tbId.Text = _currentItem.Id.ToString();
            tbName.Text = _currentItem.Name;
            tbDescription.Text = _currentItem.Description;
        }

        private void ClearForm()
        {
            tbId.Text = "0";
            tbName.Clear();
            tbDescription.Clear();
        }

        private void dgvItems_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvItems.SelectedRows.Count > 0 && dgvItems.SelectedRows[0].DataBoundItem is ProjectPriorityDTO selectedItem)
            {
                _currentItem = selectedItem;
                SetSelectedItemData();
            }
        }

        private void btCreateProject_Click(object sender, EventArgs e)
        {
            _currentItem = null;
            ClearForm();
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(tbName.Text))
            {
                ShowValidationError("Project priority name is required");
                return false;
            }

            if (string.IsNullOrWhiteSpace(tbDescription.Text))
            {
                ShowValidationError("Project priority description is required");
                return false;
            }

            return true;
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateInput()) return;

            try
            {
                bool success;

                if (tbId.Text == "0") // New item
                {
                    var newProjectPriority = new ProjectPriorityDTO
                    {
                        Name = tbName.Text.Trim(),
                        Description = tbDescription.Text.Trim()
                    };

                    success = projectPriorityServices.CreateProjectPriority(newProjectPriority);
                }
                else // Existing item
                {
                    if (_currentItem == null) return;

                    _currentItem.Name = tbName.Text.Trim();
                    _currentItem.Description = tbDescription.Text.Trim();

                    success = projectPriorityServices.UpdateProjectPriority(_currentItem);
                }

                if (success)
                {
                    MessageBox.Show("Project priority saved successfully.", "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadProjectPriorities();
                }
                else
                {
                    MessageBox.Show("Failed to save project priority.", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage("Error saving project priority", ex);
            }
        }

        private void tbSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    var keyword = string.IsNullOrWhiteSpace(tbSearch.Text) ? null : tbSearch.Text.Trim();
                    LoadProjectPriorities(keyword);
                }
                catch (Exception ex)
                {
                    ShowErrorMessage("Error searching project priorities", ex);
                }

                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void splitContainer1_Paint(object sender, PaintEventArgs e)
        {
            if (sender is SplitContainer splitContainer)
            {
                using (var brush = new SolidBrush(SystemColors.Control))
                {
                    e.Graphics.FillRectangle(brush, splitContainer.SplitterRectangle);
                }
            }
        }

        private void ShowErrorMessage(string message, Exception ex = null)
        {
            var fullMessage = ex == null ? message : $"{message}: {ex.Message}";
            MessageBox.Show(fullMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void ShowValidationError(string message)
        {
            MessageBox.Show(message, "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            ClearForm();
            LoadProjectPriorities();
        }

        protected override void OnVisibleChanged(EventArgs e)
        {
            base.OnVisibleChanged(e);

            if (this.Visible)
            {
                LoadProjectPriorities();
            }
        }
    }
}