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
    public partial class CtrlListMyProjects : UserControl
    {
        // Define a delegate and event for selection change
        public delegate void ProjectSelectedEventHandler(object sender, ProjectForListDTO selectedProject);
        public event ProjectSelectedEventHandler ProjectSelected;


        private List<ProjectForListDTO> _projects;

        public List<ProjectForListDTO> Projects
        {
            get => _projects;
            set
            {
                _projects = value;
                UC_MyProjects_Load(this, EventArgs.Empty);
            }
        }

        public ProjectForListDTO selectedItem { get; set; }

        public CtrlListMyProjects()
        {
            InitializeComponent();
            this.Dock = DockStyle.Bottom;

            listbxMyProjects.DisplayMember = "Name";
            listbxMyProjects.ValueMember = "Id";
        }

        private void UC_MyProjects_Load(object sender, EventArgs e)
        {
            if (_projects != null && _projects.Count != 0)
            {
                this.Dock = DockStyle.Fill;
                listbxMyProjects.Items.Clear();
                this.BringToFront();

                // Populate the ListBox with project names
                foreach (var project in _projects)
                {
                    listbxMyProjects.Items.Add(project);
                }

                listbxMyProjects.SelectedIndex = 0;
            }
        }

        private void listbxMyProjects_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listbxMyProjects.SelectedItem != null)
            {
                selectedItem = (ProjectForListDTO) listbxMyProjects.SelectedItem;

                // Trigger the event and pass the selected project
                ProjectSelected?.Invoke(this, (ProjectForListDTO)selectedItem);
            }
        }
    }
}
