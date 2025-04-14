using DataLayer.Domain;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class ProjectPriorityDAL
    {
        public List<ProjectPriority> GetAllProjectPriorities()
        {
            using (ProjectManagementSystemDBContext dbContext = new ProjectManagementSystemDBContext())
            {
                try
                {
                    var projectPriorities = dbContext.ProjectPriorities.ToList();
                    return projectPriorities;
                }
                catch (SqlException ex)
                {
                    // Handle SQL exceptions (e.g., log the error, rethrow, etc.)
                    throw ex;
                }
                catch (Exception ex)
                {
                    // Handle exceptions (e.g., log the error, rethrow, etc.)
                    throw ex;
                }
            }
        }
        //public bool AddProjectPriority(ProjectPriorityDTO projectPriorityDTO)
        //{
        //    using (ProjectManagementSystemDBContext dbContext = new ProjectManagementSystemDBContext())
        //    {
        //        try
        //        {
        //            ProjectPriority projectPriority = projectPriorityDTO.ToProjectPriorityEntity();
        //            dbContext.ProjectPriorities.Add(projectPriority);
        //            dbContext.SaveChanges();
        //            return true;
        //        }
        //        catch (Exception ex)
        //        {
        //            return false;
        //        }
        //    }
        //}
        //public bool RemoveProjectPriority(int id)
        //{
        //    using (ProjectManagementSystemDBContext dbContext = new ProjectManagementSystemDBContext())
        //    {
        //        try
        //        {
        //            ProjectPriority projectPriority = dbContext.ProjectPriorities.FirstOrDefault(pp => pp.Id == id);
        //            if (projectPriority != null)
        //            {
        //                dbContext.ProjectPriorities.Remove(projectPriority);
        //                dbContext.SaveChanges();
        //                return true;
        //            }
        //            return false;
        //        }
        //        catch (Exception ex)
        //        {
        //            return false;
        //        }
        //    }
        //}
    }
}
