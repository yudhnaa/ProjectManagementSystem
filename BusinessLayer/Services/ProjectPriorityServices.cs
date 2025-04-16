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

        public ProjectPriorityDTO GetById(int priorityId)
        {
            try
            {
                ProjectPriorityDAL projectPriorityDAL = new ProjectPriorityDAL();

                var projectPriority = projectPriorityDAL.GetById(priorityId);
                if (projectPriority != null)
                {
                    return projectPriority.ToDto();
                }
                else
                {
                    throw new Exception("Project priority not found.");
                }
            }
            catch (SqlException ex)
            {
                // Handle SQL exceptions (e.g., log the error, rethrow, etc.)
                throw new Exception("Database error occurred while retrieving project priority.", ex);
            }
            catch (Exception ex)
            {
                // Handle exceptions (e.g., log the error, rethrow, etc.)
                throw new Exception("An error occurred while retrieving project priority.", ex);
            }
        }
        
    }
}
