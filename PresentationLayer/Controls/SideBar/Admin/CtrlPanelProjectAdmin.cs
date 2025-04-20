using DTOLayer.Models;
using PresentationLayer.AppContext;
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
    public partial class CtrlPanelProjectAdmin : UserControl
    {
        private UserDTO user;

        private List<ProjectDTO> _projects;

        public List<ProjectDTO> projects
        {
            get => _projects;
            set
            {
                _projects = value;
                RefreshProjectList(); // Refresh the ListBox whenever tasks are set
            }
        }

        public CtrlPanelProjectAdmin()
        {
            this.user = UserSession.Instance.User;

            InitializeComponent();

            this.Dock = DockStyle.Fill;
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

        private void RefreshProjectList()
        {
            // Clear existing controls in the table layout panel
            clearTaskList();
            foreach (ProjectDTO project in projects)
            {
                CtrlProjectAdmin ctrlProjectAdmin = new CtrlProjectAdmin(project);

                tbGrid.RowCount++;
                tbGrid.Controls.Add(ctrlProjectAdmin, 0, tbGrid.RowCount - 1);
                tbGrid.RowStyles.Add(new RowStyle(SizeType.AutoSize));
                tbGrid.SetColumnSpan(ctrlProjectAdmin, 2);
            }

            // Add an empty row at the end
            tbGrid.RowCount++;
            tbGrid.RowStyles.Add(new RowStyle(SizeType.AutoSize));
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

        private void CtrlPanelProject_Load(object sender, EventArgs e)
        {
            style();
        }
    }
}
