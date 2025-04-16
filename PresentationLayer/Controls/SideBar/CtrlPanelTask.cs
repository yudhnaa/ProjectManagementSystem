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
using System.Web.UI;
using System.Windows.Forms;

namespace PresentationLayer.UC_SideBar
{
    public partial class CtrlPanelTask : System.Windows.Forms.UserControl
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
            foreach (TaskDTO task in tasks)
            {
               CtrlTask ctrlTask = new CtrlTask(task);

                tbGrid.RowCount++;
                tbGrid.Controls.Add(ctrlTask, 0, tbGrid.RowCount - 1);
                tbGrid.RowStyles.Add(new RowStyle(SizeType.AutoSize));
                tbGrid.SetColumnSpan(ctrlTask, 2);
            }
        }

        public CtrlPanelTask()
        {
            InitializeComponent();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void UC_Task_Load_1(object sender, EventArgs e)
        {
            this.Dock = DockStyle.Fill;
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
