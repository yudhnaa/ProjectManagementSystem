using DataLayer.DataAccess;
using System;

namespace DataLayer.EnumObjects
{
    public enum UserRoleEnum
    {
        Admin = 1,
        Manager = 2,
        Employee = 3
    }

    public static class UserRoleEnumExtensions
    {
        public static UserRoleEnum? FromId(int id)
        {
            if (Enum.IsDefined(typeof(UserRoleEnum), id))
            {
                return (UserRoleEnum)id;
            }
            return null;
        }

        public static int ToId(this UserRoleEnum role)
        {
            return (int)role;
        }

        public static string ToString(this UserRoleEnum role)
        {
            return role.ToString();
        }

        public static string ToString(this int roleId)
        {
            UserRoleEnum? role = FromId(roleId);
            if (role.HasValue)
            {
                return role.Value.ToString();
            }
            return "Not Config";
        }
    }
}
