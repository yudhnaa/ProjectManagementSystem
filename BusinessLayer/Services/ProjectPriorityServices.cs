using DataLayer.Domain;
using DTOLayer.Mappers;
using DTOLayer.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class ProjectPriorityServices
    {
        public List<ProjectPriorityDTO> GetAllProjectPriorities()
        {
            try
            {
                ProjectPriorityDAL projectPriorityDAL = new ProjectPriorityDAL();
                var projectPriorities = projectPriorityDAL.GetAllProjectPriorities();

                return projectPriorities.Select(p => p.ToDto()).ToList();

            } catch (SqlException ex)
            {
                // Handle SQL exceptions (e.g., log the error, rethrow, etc.)
                throw new Exception("Database error occurred while retrieving project priorities.", ex);
            }
            catch
            (Exception ex)
            {
                // Handle exceptions (e.g., log the error, rethrow, etc.)
                throw new Exception("An error occurred while retrieving project priorities.", ex);
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
