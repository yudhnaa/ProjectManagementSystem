using DataLayer.Domain;
using System.Collections.Generic;

namespace DataLayer.DataAccess
{
    public interface IUserRoleDAL
    {
        bool CreateUserRole(UserRole userRole);
        List<UserRole> GetAllUserRoles(string kw, bool isIncludeInActive);
        UserRole GetUserRoleById(int id, bool isIncludeInActive);
        bool UpdateUserRole(UserRole userRole);
    }
}