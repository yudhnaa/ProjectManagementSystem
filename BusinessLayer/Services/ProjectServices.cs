using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Domain;
using DTOLayer;
using DTOLayer.Mappers;
using DTOLayer.Models;


namespace BusinessLayer
{
    public class ProjectServices
    {

        public List<ProjectDTO> GetProjectsByUserId(int userId)
        {
            try
            {
                ProjectDAL projectDAL = new ProjectDAL();
                var projects = projectDAL.GetProjectsByUserId(userId);

                return projects.Select(p => p.ToDto()).ToList();
            }
            catch (SqlException ex)
            {
                // Handle SQL exceptions (e.g., log the error, rethrow, etc.)
                throw new Exception("Database error occurred while retrieving projects.", ex);
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                throw new Exception("An error occurred while retrieving projects.", ex);

            }
        }

        public ProjectDTO CreateProject(ProjectDTO projectDTO)
        {
            try
            {
                ProjectDAL projectDAL = new ProjectDAL();

                Project project = projectDTO.ToProjectEntity();

                project.PercentComplete = 0;
                project.IsDeleted = false;

                Project res = projectDAL.CreateProject(project);

                return res.ToDto();
            }
            catch (SqlException ex)
            {
                // Handle SQL exceptions (e.g., log the error, rethrow, etc.)
                throw new Exception("Database error occurred while creating project.", ex);
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                throw new Exception("An error occurred while creating project.", ex);



            }
        }

        public bool UpdateProject(ProjectDTO project)
        {
            try
            {
                ProjectDAL projectDAL = new ProjectDAL();
                Project existingProject = projectDAL.GetProjectById(project.Id);
                if (existingProject == null)
                    throw new Exception("Project not found.");

                existingProject.Name = project.Name;
                existingProject.ProjectCode = project.ProjectCode;
                existingProject.Description = project.Description;
                existingProject.StartDate = project.StartDate;
                existingProject.EndDate = project.EndDate;
                existingProject.Budget = project.Budget;
                existingProject.StatusId = project.StatusId;
                existingProject.ManagerId = project.ManagerId;
                existingProject.PriorityId = project.PriorityId;
                existingProject.PercentComplete = project.PercentComplete;
                existingProject.UpdatedDate = DateTime.Now;

                var res = projectDAL.UpdateProject(existingProject);
                return res;
            }
            catch (SqlException ex)
            {
                // Handle SQL exceptions (e.g., log the error, rethrow, etc.)
                throw new Exception("Database error occurred while updating project.", ex);
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                throw new Exception("An error occurred while updating project.", ex);
            }
        }

        public List<ProjectDTO> GetAllProjectsIncludeDeleteCancel()
        {
            try
            {
                ProjectDAL projectDAL = new ProjectDAL();

                var projects = projectDAL.GetAllProjectsIncludeDeleteCancel();

                return projects.Select(p => p.ToDto()).ToList();
            }
            catch (SqlException ex)
            {
                // Handle SQL exceptions (e.g., log the error, rethrow, etc.)
                throw new Exception("Database error occurred while retrieving all projects.", ex);
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                throw new Exception("An error occurred while retrieving all projects.", ex);
            }
        }

        // ham lay danh sach Project kem keyword + status
            //-->  Goi xuong DAL de truy van du lieu
            //Xu ly kq va tra ve cho Controller <--
    }
}
