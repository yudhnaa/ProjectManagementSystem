using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DTOLayer;
using DTOLayer.Models;

namespace PresentationLayer.UC_SideBar.UC_Project
{
    public partial class UC_MyProjects : UserControl
    {
        // Define a delegate and event for selection change
        public delegate void ProjectSelectedEventHandler(object sender, ProjectDTO selectedProject);
        public event ProjectSelectedEventHandler ProjectSelected;

        public List<ProjectDTO> projects { get; set; }

        public ProjectDTO selectedItem { get; set; }

        public UC_MyProjects()
        {
            InitializeComponent();
            this.Dock = DockStyle.Bottom;

            listbxMyProjects.DisplayMember = "Name";
            listbxMyProjects.ValueMember = "Id";
        }

        private void UC_MyProjects_Load(object sender, EventArgs e)
        {
            if (projects == null || projects.Count == 0)
            {
                MessageBox.Show("No projects available.");
                return;
            }
            else
            {
                // Populate the ListBox with project names
                foreach (var project in projects)
                {
                    listbxMyProjects.Items.Add(project);
                }
            }

            listbxMyProjects.SelectedIndex = 0;
        }

        private void listbxMyProjects_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listbxMyProjects.SelectedItem != null)
            {
                selectedItem = (ProjectDTO) listbxMyProjects.SelectedItem;

                // Trigger the event and pass the selected project
                ProjectSelected?.Invoke(this, (ProjectDTO)selectedItem);
            }

        }
    }
}
