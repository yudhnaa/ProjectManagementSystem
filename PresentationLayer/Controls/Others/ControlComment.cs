using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PresentationLayer.Controls.Others
{
    public partial class ControlComment : UserControl
    {
        public ControlComment()
        {
            InitializeComponent();
        }

        private void ControlComment_SizeChanged(object sender, EventArgs e)
        {
            int cellWidth = tableLayoutPanel1.GetColumnWidths()[1];
            lbComment.MaximumSize = new Size(cellWidth, 0);
            lbComment.AutoSize = true;
        }

        public void SetComment(string comment, string username, string role)
        {
            lbUsername.Text = username;
            lbRole.Text = role;
            lbComment.Text = comment;

        }
    }
}
