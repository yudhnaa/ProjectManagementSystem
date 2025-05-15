using BusinessLayer;
using BusinessLayer.Services;
using DataLayer.Domain;
using DTOLayer.Models;
using PresentationLayer.AppContext;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PresentationLayer
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                Form loginForm = new FormLogin();
                if (!FormContext.Instance.HasLoginForms)
                {
                    FormContext.Instance.SetLoginForms(loginForm);
                }

                Application.Run(loginForm);

                //Mock login user

                //UserServices userServices = new UserServices();
                //UserRoleServices userRoleServices = new UserRoleServices();

                //UserDTO user = userServices.CheckLoginUser(
                //    new UserDTO
                //    {
                //        Username = "admin",
                //        //Username = "jsmith",
                //        Password = "1"
                //    });

                //Color.FromArgb(159, 179, 223);

                //UserSession.Instance.SetUser(user, userRoleServices.GetUserRoleById(user.UserRoleId));

                //Form FormHome;

                //if (user.UserRoleId == 1)
                //    FormHome = new FormAdminHome();
                //else
                //    FormHome = new FormUserHome();

                //FormContext.Instance.SetHomeForms(FormHome);

                //Application.Run(FormHome);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
    }
}
