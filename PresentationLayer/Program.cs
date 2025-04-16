using BusinessLayer;
using DataLayer.Domain;
using DTOLayer.Models;
using System;
using System.Collections.Generic;
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

            //Application.Run(new frmLogin());

            //Mock login user
            UserServices userServices = new UserServices();
            //UserDTO user = userServices.CheckLoginUser(new UserDTO { Username = "admin", Password = "1" });
            UserDTO user = userServices.CheckLoginUser(new UserDTO { Username = "awhite", Password = "1" });

            if (user.UserRoleId == 1)
            {
                Application.Run(new FormAdminHome(user));

            }
            else
            {
                Application.Run(new FormUserHome(user));

            }

        }
    }
}
