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

            //Application.Run(new frmUserHome(new UserDTO
            //{
            //    Id = 2,
            //    Username = "user1",
            //    UserRole = new DTOLayer.UserRoleDTO { Name = "Admin" }
            //}));

            Application.Run(new frmAdminHome(new User
            {
                Id = 1,
                Username = "Admin",
                UserRole = new UserRole { Id = 1, Name = "Admin" }
            }));



        }
    }
}
