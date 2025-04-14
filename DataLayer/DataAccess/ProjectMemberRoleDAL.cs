using DataLayer.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class ProjectMemberRoleDAL
    {
        public List<ProjectMemberRole> GetAllProjectMemberRoles()
        {
            using (ProjectManagementSystemDBContext dbContext = new ProjectManagementSystemDBContext())
            {
                List<ProjectMemberRole> projectMemberRoles = dbContext.ProjectMemberRoles.ToList();

                return projectMemberRoles;
            }
        }
    }
}
