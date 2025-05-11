using DTOLayer.Models;

namespace DTOLayer.Mappers
{
    public static class TaskDependencyDTOMapper
    {
        public static TaskDependencyDTO ToDto(this DataLayer.Domain.TaskDependency taskdependency)
        {
            return new TaskDependencyDTO
            {
                Id = taskdependency.Id,
                TaskId = taskdependency.TaskId,
                DependsOnTaskId = taskdependency.DependsOnTaskId,
                DependencyType = taskdependency.DependencyType,
            };
        }

        public static DataLayer.Domain.TaskDependency ToTaskDependencyEntity(this TaskDependencyDTO model)
        {
            return new DataLayer.Domain.TaskDependency
            {
                Id = model.Id,
                TaskId = model.TaskId,
                DependsOnTaskId = model.DependsOnTaskId,
                DependencyType = model.DependencyType,
            };
        }

    }
}

