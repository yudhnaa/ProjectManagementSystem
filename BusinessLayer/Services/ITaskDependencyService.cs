using DTOLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public interface ITaskDependencyService
    {
        List<TaskDependencyDTO> GetDependentByTaskIds(int taskId);
        List<TaskDependencyDTO> GetDependentByTaskIdsIncludeInActive(int taskId);
    }
}
