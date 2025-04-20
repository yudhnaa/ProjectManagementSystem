using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.Services;
using DataLayer;
using DataLayer.Domain;

namespace BusinessLayer
{
    public class UserDAL
    {
        public User CheckLoginUser(User user)
        {

            using (ProjectManagementSystemDBContext dbContext = new ProjectManagementSystemDBContext())
            {
                try
                {
                    var u = dbContext.Users.Where(us => us.Username == user.Username).FirstOrDefault();

                    if (u == null)
                        return null;
                    else
                    {
                        if (u.Password != u.Password)
                            return null;
                        else
                            return u;
                    }
                }
                catch (SqlException ex)
                {
                    throw ex;
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }
        }

        public List<User> GetUserByKw(string kw, int pageSize)
        {
            using (ProjectManagementSystemDBContext dbContext = new ProjectManagementSystemDBContext())
            {
                try
                {
                    var users = dbContext.Users
                    .Where(u => u.Username.Contains(kw) || u.FirstName.Contains(kw) || u.LastName.Contains(kw))
                    .Take(pageSize).ToList();

                    return users;
                }
                catch (SqlException ex)
                {
                    throw ex;
                }
                catch (Exception ex)
                {
                    throw ex;

                }
            }
        }

        public List<User> GetAllUsers(int page, int pageSize)
        {
            using (ProjectManagementSystemDBContext dbContext = new ProjectManagementSystemDBContext())
            {
                try
                {
                    var users = dbContext.Users
                        .OrderBy(u => u.Id)
                        .Skip((page - 1) * pageSize)
                        .Take(pageSize)
                        .ToList();

                    if (users == null)
                        return null;

                    return users;
                }
                catch (SqlException ex)
                {
                    throw ex;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public User createUser(User user)
        {
            using (ProjectManagementSystemDBContext dbContext = new ProjectManagementSystemDBContext())
            {
                try
                {
                    dbContext.Users.Add(user);
                    dbContext.SaveChanges();

                    return user;
                }
                catch (SqlException ex)
                {
                    throw ex;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public User UpdateUser(User user)
        {
            using (ProjectManagementSystemDBContext dbContext = new ProjectManagementSystemDBContext())
            {
                try
                {
                    var existingUser = dbContext.Users.Find(user.Id);
                    if (existingUser != null)
                    {
                        dbContext.Entry(existingUser).CurrentValues.SetValues(user);
                        dbContext.SaveChanges();
                    }
                    return user;
                }
                catch (SqlException ex)
                {
                    throw ex;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public User GetUserById(int userId)
        {
            using (ProjectManagementSystemDBContext dbContext = new ProjectManagementSystemDBContext())
            {
                try
                {
                    var user = dbContext.Users.Find(userId);
                    return user;
                }
                catch (SqlException ex)
                {
                    throw ex;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public List<User> GetAllUsers()
        {
            using (ProjectManagementSystemDBContext dbContext = new ProjectManagementSystemDBContext())
            {
                try
                {
                    var users = dbContext.Users.ToList();
                    if (users == null)
                        return null;

                    return users;
                }
                catch (SqlException ex)
                {
                    throw ex;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
    }
}
