using DataLayer.Domain;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class DepartmentDAL
    {
        public List<Department> GetAllDepartments()
        {
            using (var dbContext = new ProjectManagementSystemDBContext())
            {
                try
                {
                    var departments = dbContext.Departments.ToList();

                    return departments;
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

        public List<Department> getDepartmentsByKw(string kw)
        {
            using (var dbContext = new ProjectManagementSystemDBContext())
            {
                try
                {
                    var departments = dbContext.Departments
                        .Where(d => d.Name.Contains(kw))
                        .ToList();
                    return departments;
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
