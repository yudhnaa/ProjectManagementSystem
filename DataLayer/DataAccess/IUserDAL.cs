using DataLayer.Domain;
using System.Collections.Generic;

namespace DataLayer.DataAccess
{
    public interface IUserDAL
    {
        User CheckLoginUser(User user);
        User CreateUser(User user);
        List<User> GetAllUsers(string kw, bool isIncludeInActive);
        List<User> GetProjectsByKeywordAndStatusAndDepartment(string keyword, int statusId, int departmentID, bool isIncludeInActive);
        User GetUserById(int userId, bool isIncludeInActive);
        bool UpdateUser(User user);
    }
}