using DataLayer.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class ProjectStatusDAL
    {
        public List<ProjectStatus> GetAllProjectStatuses()
        {
            using (ProjectManagementSystemDBContext dbContext = new ProjectManagementSystemDBContext())
            {
                List<ProjectStatus> projectStatuses = dbContext.ProjectStatuses.ToList();

                return projectStatuses;
            }
        }
    }
}
