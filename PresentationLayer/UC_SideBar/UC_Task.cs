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

namespace PresentationLayer.UC_SideBar
{
    public partial class UC_Task : UserControl
    {
        private List<TaskDTO> _tasks;
        public List<TaskDTO> tasks
        {
            get => _tasks;
            set
            {
                _tasks = value;
                RefreshTaskList(); // Refresh the ListBox whenever tasks are set
            }
        }

        private void RefreshTaskList()
        {
            listBox1.Items.Clear(); // Clear existing items
            if (_tasks != null && _tasks.Count > 0)
            {
                foreach (var task in _tasks)
                {
                    listBox1.Items.Add(task.Name); // Add task names to the ListBox
                }
            }
        }

        public UC_Task()
        {
            InitializeComponent();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void UC_Task_Load_1(object sender, EventArgs e)
        {
            this.Dock = DockStyle.Fill;

            listBox1.DisplayMember = "Name";
            listBox1.ValueMember = "Id";
        }
    }
}
