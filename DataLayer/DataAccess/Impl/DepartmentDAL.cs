using DataLayer.Domain;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.DataAccess
{
    public class DepartmentDAL : IDepartmentDAL
    {
        public bool CreateDepartment(Department department)
        {
            using (ProjectManagementSystemDBContext dbContext = new ProjectManagementSystemDBContext())
            {
                try
                {
                    dbContext.Departments.Add(department);
                    dbContext.SaveChanges();
                    return true;
                }
                catch (SqlException ex)
                {
                    // Handle SQL exceptions (e.g., log the error, rethrow, etc.)
                    throw new Exception("Database error occurred while adding department.", ex);
                }
                catch (Exception ex)
                {
                    // Handle other exceptions
                    throw new Exception("An error occurred while adding department.", ex);
                }
            }
        }
        public bool UpdateDepartment(Department department)
        {
            using (ProjectManagementSystemDBContext dbContext = new ProjectManagementSystemDBContext())
            {
                try
                {
                    var existingDepartment = dbContext.Departments.Find(department.Id);
                    if (existingDepartment != null)
                    {
                        existingDepartment.Name = department.Name;
                        existingDepartment.Description = department.Description;
                        existingDepartment.ManagerId = department.ManagerId;
                        existingDepartment.IsActive = department.IsActive;
                        existingDepartment.UpdatedDate = DateTime.Now;
                        dbContext.SaveChanges();
                        return true;
                    }
                    return false;
                }
                catch (SqlException ex)
                {
                    // Handle SQL exceptions (e.g., log the error, rethrow, etc.)
                    throw new Exception("Database error occurred while updating department.", ex);
                }
                catch (Exception ex)
                {
                    // Handle other exceptions
                    throw new Exception("An error occurred while updating department.", ex);
                }
            }
        }
        public List<Department> GetAllDepartments(string kw, bool isIncludeInActive)
        {
            using (var dbContext = new ProjectManagementSystemDBContext())
            {
                try
                {
                    IQueryable<Department> query = dbContext.Departments;

                    if (!string.IsNullOrEmpty(kw))
                    {
                        query = query.Where(d => d.Name.Contains(kw));
                    }

                    if (!isIncludeInActive)
                    {
                        query = query.Where(d => d.IsDeleted == false && d.IsActive == true);
                    }

                    return query.ToList();
                }
                catch (Exception ex)
                {
                    throw new Exception("An error occurred while retrieving departments.", ex);
                }
            }
        }

        public Department GetDepartmentById(int departmentId, bool isIncludeInActive)
        {
            using (var dbContext = new ProjectManagementSystemDBContext())
            {
                try
                {
                    var query = dbContext.Departments.AsQueryable();

                    if (!isIncludeInActive)
                    {
                        query = query.Where(d => d.IsActive == true && d.IsDeleted == false);
                    }

                    return query.FirstOrDefault(d => d.Id == departmentId);
                }
                catch (SqlException ex)
                {
                    throw new Exception("Database error occurred while retrieving the department.", ex);
                }
                catch (Exception ex)
                {
                    throw new Exception("An error occurred while retrieving the department.", ex);
                }
            }
        }

        public List<Department> GetDepartmentsByKw(string kw, bool isIncludeInActive)
        {
            using (var dbContext = new ProjectManagementSystemDBContext())
            {
                try
                {
                    var query = dbContext.Departments.AsQueryable();

                    if (!string.IsNullOrEmpty(kw))
                    {
                        query = query.Where(d => d.Name.Contains(kw));
                    }

                    if (!isIncludeInActive)
                    {
                        query = query.Where(d => d.IsActive == true && d.IsDeleted == false);
                    }

                    return query.ToList();
                }
                catch (Exception ex)
                {
                    throw new Exception("An error occurred while retrieving departments by keyword.", ex);
                }
            }
        }
    }
}
