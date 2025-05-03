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
    public partial class LabelWithImage : UserControl
    {

        public LabelWithImage()
        {
            InitializeComponent();
        }

        [Browsable(true)]
        public string TextLabel
        {
            get { return label1.Text; }
            set { label1.Text = value; }
        }

        [Browsable(true)]
        public Image Image
        {
            get { return pictureBox1.Image; }
            set
            {
                pictureBox1.Image = value;
            }
        }
    }
}
