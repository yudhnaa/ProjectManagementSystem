using BusinessLayer.Services.Ipml;
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

namespace PresentationLayer.Forms.MainForm.User
{
    public partial class FormNotification : Form
    {

        private readonly UserDTO user = UserSession.Instance.User;
        private readonly INotificationServices notificationServices = new NotificationServices();

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
                CtrlNotification ctrlNotification = new CtrlNotification(item.Title, item.Message);

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

        private void AcceptTaskHelpRequest()
        {

        }

        private void AcceptProjectInvite()
        {

        }

        private void FormNotification_Load(object sender, EventArgs e)
        {
            LoadNotification();

            SetNotifications();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
