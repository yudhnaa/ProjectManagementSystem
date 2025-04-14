using DTOLayer.Models;
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
    public partial class ctrlTask : UserControl
    {
        private TaskDTO taskDTO;
        public ctrlTask(TaskDTO taskDTO)
        {
            InitializeComponent();

            this.taskDTO = taskDTO;
        }

        private void ctrl_Task_Load(object sender, EventArgs e)
        {
            lbTitle.Text = taskDTO.Name;
            lbAbstract.Text = String.Format("#{0} Opened {1} days ago by {2} in {3}", taskDTO.Code, taskDTO.CreatedDate, taskDTO.AssignedUserId, taskDTO.ProjectId);


        }

        private void bunifuLabel1_Click(object sender, EventArgs e)
        {

        }
    }
}
