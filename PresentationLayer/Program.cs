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
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //Application.Run(new FormLogin());

            //Mock login user

            UserServices userServices = new UserServices();
            UserRoleServices userRoleServices = new UserRoleServices();

            UserDTO user = userServices.CheckLoginUser(
                new UserDTO
                {
                    //Username = "admin",
                    Username = "awhite",
                    Password = "hashed_password_789"
                });

            Color.FromArgb(159, 179, 223);

            UserSession.Instance.SetUser(user, userRoleServices.GetUserRoleById(user.UserRoleId));

            if (user.UserRoleId == 1)
                Application.Run(new FormAdminHome());

            else
                Application.Run(new FormUserHome());
        }
    }
}
