﻿using BusinessLayer.Services;
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
using System.Windows.Forms;

namespace PresentationLayer.Controls.SideBar.Admin
{
    public partial class CtrlPanelProjectStatus : UserControl
    {
        private ProjectStatusServices projectStatusServices;

        private List<ProjectStatusDTO> projectStatuses;

        private ProjectStatusDTO currentItem;

        public CtrlPanelProjectStatus()
        {
            InitializeComponent();
        }
        private void CtrlPanelProjectStatus_Load(object sender, EventArgs e)
        {
            InitControl();
            InitServices();
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

        private void InitServices()
        {
            projectStatusServices = new ProjectStatusServices();
        }

        private void LoadProjectStatuses()
        {
            //projectStatuses = projectStatusServices.GetAllProjectStatuses();
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
            if (string.IsNullOrWhiteSpace(tbName.Text))
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
                    Name = tbName.Text,
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
    }
}
