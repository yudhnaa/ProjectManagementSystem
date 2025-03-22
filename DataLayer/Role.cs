using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    class Role
    {
        private int roleID;
        private string roleName;
        private string description;
        private DateTime createdDate;
        private DateTime updatedDate;

        public Role()
        {
        }

        public Role(int roleID, string roleName, string description, DateTime createdDate, DateTime updatedDate)
        {
            this.roleID = roleID;
            this.roleName = roleName;
            this.description = description;
            this.createdDate = createdDate;
            this.updatedDate = updatedDate;
        }

        public int RoleID
        {
            get { return roleID; }
            set { roleID = value; }
        }

        public string RoleName
        {
            get { return roleName; }
            set { roleName = value; }
        }

        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        public DateTime CreatedDate
        {
            get { return createdDate; }
            set { createdDate = value; }
        }

        public DateTime UpdatedDate
        {
            get { return updatedDate; }
            set { updatedDate = value; }
        }
    }
}
