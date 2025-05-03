using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.EnumObjects
{
    public enum ProjectPriorityEnum
    {
        High = 1,
        Medium = 2,
        Low = 3
    }

    public static class ProjectPriorityEnumExtensions
    {
        public static ProjectPriorityEnum? FromId(int id)
        {
            if (Enum.IsDefined(typeof(ProjectPriorityEnum), id))
            {
                return (ProjectPriorityEnum)id;
            }
            return null;
        }

        public static int ToId(this ProjectPriorityEnum priority)
        {
            return (int)priority;
        }

        public static string ToString(this ProjectPriorityEnum priority)
        {
            switch (priority)
            {
                case ProjectPriorityEnum.High:
                    return "High";
                case ProjectPriorityEnum.Medium:
                    return "Medium";
                case ProjectPriorityEnum.Low:
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
