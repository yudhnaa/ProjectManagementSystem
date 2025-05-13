using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer;
using DataLayer.Domain;

namespace DataLayer.DataAccess
{
    public class UserDAL : IUserDAL
    {
        public User CheckLoginUser(User user)
        {
            if (user == null || string.IsNullOrEmpty(user.Username))
                return null;

            using (var dbContext = new ProjectManagementSystemDBContext())
            {
                try
                {
                    var u = dbContext.Users.FirstOrDefault(us => us.Username == user.Username);

                    if (u != null)
                        return u;

                    return null;
                }
                catch (Exception ex)
                {
                    // Don't re-throw exceptions with no additional info
                    throw;
                }
            }
        }

        public User CreateUser(User user)
        {
            if (user == null)
                return null;

            using (var dbContext = new ProjectManagementSystemDBContext())
            {
                try
                {
                    dbContext.Users.Add(user);
                    dbContext.SaveChanges();
                    return user;
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public List<User> GetAllUsers(string kw, bool isIncludeInActive)
        {
            using (var dbContext = new ProjectManagementSystemDBContext())
            {
                try
                {
                    var query = dbContext.Users.AsQueryable();

                    // Filter by IsDeleted status when isIncludeInActive is false
                    if (!isIncludeInActive)
                        query = query.Where(u => u.IsDeleted == false && u.IsActive == true);

                    // Apply keyword filter if provided
                    if (!string.IsNullOrEmpty(kw))
                        query = query.Where(u => u.Username.Contains(kw) ||
                                               u.FirstName.Contains(kw) ||
                                               u.LastName.Contains(kw));

                    return query.ToList();
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public List<User> GetProjectsByKeywordAndStatusAndDepartment(string keyword, int statusId, int departmentID, bool isIncludeInActive)
        {
            if (keyword == null)
                keyword = string.Empty;

            using (var dbContext = new ProjectManagementSystemDBContext())
            {
                try
                {
                    var query = dbContext.Users.AsQueryable();

                    // Apply filters
                    query = query.Where(p => p.Username.Contains(keyword) &&
                                         p.UserRoleId == statusId &&
                                         p.DepartmentId == departmentID);

                    if (!isIncludeInActive)
                        query = query.Where(p => p.IsDeleted == false && p.IsActive == true);

                    return query.ToList();
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public User GetUserById(int userId, bool isIncludeInActive)
        {
            using (var dbContext = new ProjectManagementSystemDBContext())
            {
                try
                {
                    var query = dbContext.Users.AsQueryable();

                    if (!isIncludeInActive)
                        query = query.Where(u => u.IsDeleted == false && u.IsActive == true);

                    return query.FirstOrDefault(u => u.Id == userId);
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public bool UpdateUser(User user)
        {
            if (user == null)
                return false;

            using (var dbContext = new ProjectManagementSystemDBContext())
            {
                try
                {
                    var existingUser = dbContext.Users.Find(user.Id);
                    if (existingUser == null)
                        return false;

                    existingUser.Username = user.Username;
                    existingUser.FirstName = user.FirstName;
                    existingUser.LastName = user.LastName;
                    existingUser.Email = user.Email;
                    existingUser.PhoneNumber = user.PhoneNumber;
                    existingUser.Address = user.Address;
                    existingUser.UserRoleId = user.UserRoleId;
                    existingUser.DepartmentId = user.DepartmentId;
                    existingUser.IsActive = user.IsActive;
                    existingUser.IsDeleted = user.IsDeleted ?? false;
                    existingUser.UpdatedDate = user.UpdatedDate;
                    existingUser.Password = user.Password;
                    //existingUser.UpdatedBy = user.UpdatedBy;


                    dbContext.SaveChanges();
                    return true;
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
    }
}
