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
    public partial class CtrlPanelTaskAdmin : System.Windows.Forms.UserControl
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
            if (tasks == null)
                return;

            foreach (TaskDTO task in tasks)
            {
                CtrlTaskAdmin ctrlTaskAdmin = new CtrlTaskAdmin(task);

                tbGrid.RowCount++;
                tbGrid.Controls.Add(ctrlTaskAdmin, 0, tbGrid.RowCount - 1);
                tbGrid.RowStyles.Add(new RowStyle(SizeType.AutoSize));
                tbGrid.SetColumnSpan(ctrlTaskAdmin, 2);
            }

            // Add an empty row at the end
            tbGrid.RowCount++;
            tbGrid.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        }

        public CtrlPanelTaskAdmin()
        {
            InitializeComponent();
        }

        private void style()
        {
            this.Dock = DockStyle.Fill;

            tbGrid.Margin = new Padding(10, 0, 0, 10);
            if (tbGrid.ColumnCount > 0)
            {
                tbGrid.ColumnStyles.Clear();
                tbGrid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60F));
                tbGrid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40F));
            }

            tbSearch.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right;

            drpdwnStatus.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right;
            btnSearch.Anchor = AnchorStyles.Right | AnchorStyles.Top;
        }

        private void UC_Task_Load_1(object sender, EventArgs e)
        {
            style();
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
