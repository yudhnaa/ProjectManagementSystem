using DataLayer.Domain;
using DataLayer.EnumObjects;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.DataAccess
{
    public class ProjectPriorityDAL : IProjectPriorityDAL
    {
        public bool CreateProjectPriority(ProjectPriority projectPriority)
        {
            using (ProjectManagementSystemDBContext dbContext = new ProjectManagementSystemDBContext())
            {
                try
                {
                    dbContext.ProjectPriorities.Add(projectPriority);
                    dbContext.SaveChanges();
                    return true;
                }
                catch (SqlException ex)
                {
                    // Handle SQL exceptions (e.g., log the error, rethrow, etc.)
                    throw new Exception("Database error occurred while adding project priorities.", ex);
                }
                catch (Exception ex)
                {
                    // Handle other exceptions
                    throw new Exception("An error occurred while adding project priorities.", ex);
                }
            }
        }
        public bool UpdateProjectPriority(ProjectPriority projectPriority)
        {
            using (ProjectManagementSystemDBContext dbContext = new ProjectManagementSystemDBContext())
            {
                try
                {
                    var existingProjectPriority = dbContext.ProjectPriorities.Find(projectPriority.Id);
                    if (existingProjectPriority != null)
                    {
                        existingProjectPriority.Name = projectPriority.Name;
                        existingProjectPriority.Description = projectPriority.Description;
                        existingProjectPriority.UpdatedDate = DateTime.Now;
                        dbContext.SaveChanges();
                        return true;
                    }
                    return false;
                }
                catch (SqlException ex)
                {
                    // Handle SQL exceptions (e.g., log the error, rethrow, etc.)
                    throw new Exception("Database error occurred while updating project priorities.", ex);
                }
                catch (Exception ex)
                {
                    // Handle other exceptions
                    throw new Exception("An error occurred while updating project priorities.", ex);
                }
            }
        }

        public List<ProjectPriority> GetAllProjectPriorities(string kw, bool isIncludeInActive)
        {
            using (ProjectManagementSystemDBContext dbContext = new ProjectManagementSystemDBContext())
            {
                try
                {
                    var query = dbContext.ProjectPriorities.AsQueryable();

                    if (!string.IsNullOrEmpty(kw))
                    {
                        query = query.Where(p => p.Name.Contains(kw) || p.Description.Contains(kw));
                    }

                    if (!isIncludeInActive)
                    {
                        query = query.Where(p => p.IsActive == true && p.IsDeleted == false);
                    }

                    return query.ToList();
                }
                catch (SqlException ex)
                {
                    throw new Exception("Database error occurred while retrieving project priorities.", ex);
                }
                catch (Exception ex)
                {
                    throw new Exception("An error occurred while retrieving project priorities.", ex);
                }
            }
        }

        public ProjectPriority GetById(int priorityId, bool isIncludeInActive)
        {
            using (ProjectManagementSystemDBContext dbContext = new ProjectManagementSystemDBContext())
            {
                try
                {
                    var query = dbContext.ProjectPriorities.Where(p => p.Id == priorityId);

                    if (!isIncludeInActive)
                    {
                        query = query.Where(p => p.IsActive == true && p.IsDeleted == false);
                    }

                    return query.FirstOrDefault();
                }
                catch (SqlException ex)
                {
                    throw new Exception("Database error occurred while retrieving the project priority by ID.", ex);
                }
                catch (Exception ex)
                {
                    throw new Exception("An error occurred while retrieving the project priority by ID.", ex);
                }
            }
        }

        public ProjectPriorityEnum? GetPriorityEnumById(int priorityId)
        {
            return ProjectPriorityEnumExtensions.FromId(priorityId);
        }

        public int GetPriorityIdByEnum(ProjectPriorityEnum priorityEnum)
        {
            return priorityEnum.ToId();
        }
    }
}
