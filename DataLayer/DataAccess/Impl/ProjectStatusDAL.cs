using DataLayer.Domain;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.DataAccess
{
    public class ProjectStatusDAL : IProjectStatusDAL
    {
        private const string DatabaseErrorMessage = "Database error occurred while {0}.";
        private const string GenericErrorMessage = "An error occurred while {0}.";

        public bool CreateProjectStatus(ProjectStatus projectStatus)
        {
            using (var dbContext = new ProjectManagementSystemDBContext())
            {
                try
                {
                    dbContext.ProjectStatuses.Add(projectStatus);
                    return dbContext.SaveChanges() > 0;
                }
                catch (SqlException ex)
                {
                    throw new Exception(string.Format(DatabaseErrorMessage, "adding project status"), ex);
                }
                catch (Exception ex)
                {
                    throw new Exception(string.Format(GenericErrorMessage, "adding project status"), ex);
                }
            }
        }

        public List<ProjectStatus> GetAllProjectStatuses(string kw, bool isIncludeInActive)
        {
            using (var dbContext = new ProjectManagementSystemDBContext())
            {
                try
                {
                    return GetFilteredQuery(dbContext, kw, isIncludeInActive).ToList();
                }
                catch (SqlException ex)
                {
                    throw new Exception(string.Format(DatabaseErrorMessage, "retrieving project statuses"), ex);
                }
                catch (Exception ex)
                {
                    throw new Exception(string.Format(GenericErrorMessage, "retrieving project statuses"), ex);
                }
            }
        }

        public ProjectStatus GetById(int statusId, bool isIncludeInActive)
        {
            using (var dbContext = new ProjectManagementSystemDBContext())
            {
                try
                {
                    var query = dbContext.ProjectStatuses.Where(p => p.Id == statusId);

                    if (!isIncludeInActive)
                        query = query.Where(p => p.IsActive == true && p.IsDeleted == false);

                    return query.FirstOrDefault();
                }
                catch (SqlException ex)
                {
                    throw new Exception(string.Format(DatabaseErrorMessage, "retrieving project status"), ex);
                }
                catch (Exception ex)
                {
                    throw new Exception(string.Format(GenericErrorMessage, "retrieving project status"), ex);
                }
            }
        }

        public List<ProjectStatus> GetProjectStatusByName(string kw, bool isIncludeInActive)
        {
            if (string.IsNullOrEmpty(kw))
                return new List<ProjectStatus>();

            using (var dbContext = new ProjectManagementSystemDBContext())
            {
                try
                {
                    return GetFilteredQuery(dbContext, kw, isIncludeInActive).ToList();
                }
                catch (SqlException ex)
                {
                    throw new Exception(string.Format(DatabaseErrorMessage, "retrieving project status by name"), ex);
                }
                catch (Exception ex)
                {
                    throw new Exception(string.Format(GenericErrorMessage, "retrieving project status by name"), ex);
                }
            }
        }

        public List<ProjectStatus> GetProjectStatusByStatusId(int statusId, bool isIncludeInActive)
        {
            using (var dbContext = new ProjectManagementSystemDBContext())
            {
                try
                {
                    var query = dbContext.ProjectStatuses.Where(p => p.Id == statusId);

                    if (!isIncludeInActive)
                        query = query.Where(p => p.IsActive == true && p.IsDeleted == false);

                    return query.ToList();
                }
                catch (SqlException ex)
                {
                    throw new Exception(string.Format(DatabaseErrorMessage, "retrieving project status by ID"), ex);
                }
                catch (Exception ex)
                {
                    throw new Exception(string.Format(GenericErrorMessage, "retrieving project status by ID"), ex);
                }
            }
        }

        public bool UpdateProjectStatus(ProjectStatus projectStatus)
        {
            using (var dbContext = new ProjectManagementSystemDBContext())
            {
                try
                {
                    var existingProjectStatus = dbContext.ProjectStatuses.Find(projectStatus.Id);
                    if (existingProjectStatus == null)
                        return false;

                    existingProjectStatus.Name = projectStatus.Name;
                    existingProjectStatus.Description = projectStatus.Description;
                    existingProjectStatus.IsActive = projectStatus.IsActive;
                    existingProjectStatus.IsDeleted = !projectStatus.IsActive;
                    existingProjectStatus.UpdatedDate = DateTime.Now;

                    return dbContext.SaveChanges() > 0;
                }
                catch (SqlException ex)
                {
                    throw new Exception(string.Format(DatabaseErrorMessage, "updating project status"), ex);
                }
                catch (Exception ex)
                {
                    throw new Exception(string.Format(GenericErrorMessage, "updating project status"), ex);
                }
            }
        }

        private static IQueryable<ProjectStatus> GetFilteredQuery(ProjectManagementSystemDBContext dbContext, string kw, bool isIncludeInActive)
        {
            var query = dbContext.ProjectStatuses.AsQueryable();

            if (!isIncludeInActive)
                query = query.Where(p => p.IsActive == true && p.IsDeleted == false);

            if (!string.IsNullOrEmpty(kw))
                query = query.Where(p => p.Name.Contains(kw));

            return query;
        }
    }
}
