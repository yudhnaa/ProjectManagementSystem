using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.EnumObjects
{
    public enum ProjectMemberRoleEnum
    {
        Manager = 1, // Responsible for managing the project
        Developer = 2, // Responsible for developing project tasks
        Tester = 3, // Responsible for testing project deliverables
        Designer = 4 // Responsible for designing project assets
    }

    public static class ProjectMemberRoleEnumExtensions
    {
        public static ProjectMemberRoleEnum? FromId(int id)
        {
            if (System.Enum.IsDefined(typeof(ProjectMemberRoleEnum), id))
            {
                return (ProjectMemberRoleEnum)id;
            }
            return null;
        }

        public static int ToId(this ProjectMemberRoleEnum role)
        {
            return (int)role;
        }

        public static string ToString(this ProjectMemberRoleEnum role)
        {
            return role.ToString();
        }
        
        public static string ToString(this int roleId)
        {
            ProjectMemberRoleEnum? role = FromId(roleId);
            if (role.HasValue)
            {
                return role.Value.ToString();
            }
            return string.Empty;
        }
    }
}
