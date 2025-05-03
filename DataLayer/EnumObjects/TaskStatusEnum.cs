using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.EnumObjects
{
    public enum TaskStatusEnum
    {
        NotStarted = 1,
        InProgress = 2,
        Completed = 3,
        Cancelled = 4
    }

    public static class TaskStatusEnumExtensions
    {
        public static TaskStatusEnum? FromId(int id)
        {
            if (Enum.IsDefined(typeof(TaskStatusEnum), id))
            {
                return (TaskStatusEnum)id;
            }
            return null;
        }

        public static int ToId(this TaskStatusEnum? status)
        {
            return (int)status;
        }

        public static string ToString(this TaskStatusEnum status)
        {
            switch (status)
            {
                case TaskStatusEnum.NotStarted:
                    return "Not Started";
                case TaskStatusEnum.InProgress:
                    return "In Progress";
                case TaskStatusEnum.Completed:
                    return "Completed";
                case TaskStatusEnum.Cancelled:
                    return "Cancelled";
                default:
                    throw new ArgumentOutOfRangeException(nameof(status), status, null);
            }
        }

        public static string ToString(this int statusId)
        {
            try
            {
                return ToString(FromId(statusId).Value);
            }
            catch (ArgumentOutOfRangeException e)
            {
                throw e;
            }
        }
    }
}
