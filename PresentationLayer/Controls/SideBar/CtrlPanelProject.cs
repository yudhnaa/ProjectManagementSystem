using DTOLayer.Models;
using PresentationLayer.Control;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PresentationLayer.Controls.Project
{
    public partial class CtrlPanelProject : UserControl
    {
        private List<ProjectDTO> _projects;

        public List<ProjectDTO> projects
        {
            get => _projects;
            set
            {
                _projects = value;
                RefreshTaskList(); // Refresh the ListBox whenever tasks are set
            }
        }

        public CtrlPanelProject()
        {
            InitializeComponent();
        }

        private void clearTaskList()
        {
            // Xóa các control trong hàng 2 -> end
            for (int row = tbGrid.RowCount - 1; row >= 2; row--) // lùi dần để tránh lỗi khi xóa
            {
                for (int col = 0; col < tbGrid.ColumnCount; col++)
                {
                    var control = tbGrid.GetControlFromPosition(col, row);
                    if (control != null)
                    {
                        tbGrid.Controls.Remove(control);
                        control.Dispose();
                    }
                }

                // Xóa RowStyle tương ứng nếu có
                if (tbGrid.RowStyles.Count > row)
                {
                    tbGrid.RowStyles.RemoveAt(row);
                }
            }
        }

        private void RefreshTaskList()
        {
            // Clear existing controls in the table layout panel
            clearTaskList();
            foreach (ProjectDTO project in projects)
            {
                CtrlProject ctrlProject = new CtrlProject(project);

                tbGrid.RowCount++;
                tbGrid.Controls.Add(ctrlProject, 0, tbGrid.RowCount - 1);
                tbGrid.RowStyles.Add(new RowStyle(SizeType.AutoSize));
                tbGrid.SetColumnSpan(ctrlProject, 2);
            }
        }

        private void CtrlPanelProject_Load(object sender, EventArgs e)
        {
            this.Dock = DockStyle.Fill;
        }
    }
}
