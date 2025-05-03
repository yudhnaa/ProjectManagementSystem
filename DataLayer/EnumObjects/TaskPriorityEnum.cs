using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.EnumObjects
{
    public enum TaskPriorityEnum
    {
        Critical = 1,
        High = 2,
        Medium = 3,
        Low = 4
    }

    public static class TaskPriorityEnumExtensions
    {
        public static TaskPriorityEnum? FromId(int id)
        {
            if (Enum.IsDefined(typeof(TaskPriorityEnum), id))
            {
                return (TaskPriorityEnum)id;
            }
            return null;
        }

        public static int ToId(this TaskPriorityEnum priority)
        {
            return (int)priority;
        }

        public static string ToString(this TaskPriorityEnum priority)
        {
            switch (priority)
            {
                case TaskPriorityEnum.Critical:
                    return "Critical";
                case TaskPriorityEnum.High:
                    return "High";
                case TaskPriorityEnum.Medium:
                    return "Medium";
                case TaskPriorityEnum.Low:
                    return "Low";
                default:
                    throw new ArgumentOutOfRangeException(nameof(priority), priority, null);
            }
        }

        public static string ToString(this int priorityId)
        {
            try
            {
                return ToString(FromId(priorityId).Value);
            }
            catch (ArgumentOutOfRangeException e)
            {
                throw e;
            }
        }
    }
}
