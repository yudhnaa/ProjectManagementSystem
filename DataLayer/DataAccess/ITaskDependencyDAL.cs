using DataLayer.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.DataAccess
{
    public interface ITaskDependencyDAL
    {
        List<TaskDependency> GetDependentTaskIds(int taskId, bool isIncludeInActive);
    }
}
