using DTOLayer;
using DTOLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationLayer.AppContext
{
    public class UserSession
    {
        private static readonly UserSession _instance = new UserSession();
        public static UserSession Instance => _instance;

        public UserDTO User { get; private set; }
        public UserRoleDTO UserRole { get; private set; }

        private UserSession() { }

        public void SetUser(UserDTO user, UserRoleDTO userRole)
        {
            User = user;
            UserRole = userRole;
        }

        public void Logout()
        {
            User = null;
            UserRole = null;
        }

        public bool IsLoggedIn => User != null;
    }
}
