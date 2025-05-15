using DataLayer.EnumObjects;
using DTOLayer.Models;
using PresentationLayer.AppContext;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PresentationLayer.Controls.SideBar.User
{
    public partial class CtrlNotification : UserControl
    {
        private readonly UserDTO user = UserSession.Instance.User;
        private readonly NotificationTypeEnum type;

        [Category("Action")]
        [Description("Occur when user click Accept button.")]
        public event EventHandler AcceptClick;

        public CtrlNotification()
        {
            InitializeComponent();
        }



        public CtrlNotification(string title, string content, NotificationTypeEnum type)
        {
            InitializeComponent();

            Title = title;
            Content = content;
            this.type = type;

            switch (this.type)
            {
                case NotificationTypeEnum.ProjectInvitation:
                    btnAccept.Visible = true;
                    break;
                case NotificationTypeEnum.TaskAssignment:
                    btnAccept.Visible = false;
                    break;
                case NotificationTypeEnum.TaskHelpRequest:
                    btnAccept.Visible = false;
                    break;
                case NotificationTypeEnum.CommentAdded:
                    btnAccept.Visible = false;
                    break;
                case NotificationTypeEnum.TaskExpiring:
                    btnAccept.Visible = false;
                    break;
                default:
                    btnAccept.Visible = false;
                    break;
            }

        }
        
        private void btnAccept_Click(object sender, EventArgs e)
        {
            AcceptClick?.Invoke(this, e);
        }

        [Browsable(true)]
        [Category("Custom Props")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Description("The text of notification title")]
        public string Title
        {
            get => lbTitle.Text;
            set
            {
                lbTitle.Text = value;
                Invalidate(); 
            }
        }

        [Browsable(true)]
        [Category("Custom Props")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Description("The text of notification content")]
        public string Content
        {
            get => lbContent.Text;
            set 
            {
                lbContent.Text = value;
                Invalidate();
            }
        }
    }
}
