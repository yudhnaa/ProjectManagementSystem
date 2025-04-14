using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PresentationLayer.Control
{
    public partial class ctrl_Task : UserControl
    {
        public ctrl_Task()
        {
            InitializeComponent();
        }

        public class RoundedPanel : Panel
        {
            private int _borderRadius = 30;

            [Category("Appearance")]
            public int BorderRadius
            {
                get { return _borderRadius; }
                set { _borderRadius = value; this.Invalidate(); }
            }

            protected override void OnPaint(PaintEventArgs e)
            {
                base.OnPaint(e);
                // Code vẽ bo góc ở đây


            }
        }

        private void ctrl_Task_Load(object sender, EventArgs e)
        {

        }
    }
}
