using Bunifu.UI.WinForms.BunifuButton;
using BusinessLayer.Services;
using BusinessLayer.Services.Ipml;
using DataLayer.Domain;
using DataLayer.EnumObjects;
using DTOLayer.Models;
using PresentationLayer.AppContext;
using PresentationLayer.Controls.SideBar.User;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace PresentationLayer.Forms.MainForm.User
{
    public partial class FormNotification : Form
    {

        private readonly UserDTO user = UserSession.Instance.User;
        private readonly INotificationServices notificationServices = new NotificationServices();
        private readonly IProjectMemberServices projectMemberServices = new ProjectMemberServices();
        private readonly ITaskServices taskServices = new TaskServices();

        public delegate void OnAcceptEventHandler();
        public event OnAcceptEventHandler RefreshEvent;

        List<NotificationDTO> notifications = new();


        public FormNotification()
        {
            InitializeComponent();
            InitControls();
        }

        private void InitControls()
        {
            this.HorizontalScroll.Enabled = false;
            this.ShowInTaskbar = false;

        }

        private void SetNotifications()
        {
            if (notifications == null || notifications.Count == 0)
                return;

            foreach (var item in notifications)
            {
                CtrlNotification ctrlNotification = new CtrlNotification(item.Title, item.Message, (NotificationTypeEnum) item.NotificationTypeId);

                switch (item.NotificationTypeId)
                {
                    case (int)NotificationTypeEnum.ProjectInvitation:
                        ctrlNotification.AcceptClick += AcceptProjectInvite;
                        break;
                    case (int)NotificationTypeEnum.TaskHelpRequest:
                        ctrlNotification.AcceptClick += AcceptTaskHelpRequest;
                        break;
                    default:
                        break;
                }

                tableNotis.RowStyles.Add(new RowStyle(SizeType.AutoSize));

                tableNotis.Controls.Add(ctrlNotification);
            }
        }

        private void LoadNotification()
        {
            try
            {
                notifications = notificationServices.GetNotificationByUserId(user.Id);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AcceptTaskHelpRequest(object sender, EventArgs e)
        {
            
        }

        private void AcceptProjectInvite(object sender, EventArgs e)
        {
            try
            {
                var notification = (CtrlNotification) (sender);

                var res = projectMemberServices.ConfirmProjectMemberByNotification(user.Id, NotificationTypeEnum.ProjectInvitation, notification.Content);

                if (res)
                {
                    MessageBox.Show("You have accepted the project invitation.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    RefreshEvent();
                }
                else
                {
                    MessageBox.Show("Failed to accept the project invitation.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void FormNotification_Load(object sender, EventArgs e)
        {
            LoadNotification();

            SetNotifications();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            notifications.Clear();
            tableNotis.Controls.Clear();
            this.Close();
        }
    }
}
