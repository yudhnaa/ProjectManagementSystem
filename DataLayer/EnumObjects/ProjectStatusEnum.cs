using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.EnumObjects
{
    public enum ProjectStatusEnum
    {
        NotStarted = 1,
        InProgress = 2,
        Completed = 3,
        Cancelled = 4
    }

    public static class ProjectStatusEnumExtensions
    {
        public static ProjectStatusEnum FromId(int id)
        {
            if (Enum.IsDefined(typeof(ProjectStatusEnum), id))
            {
                return (ProjectStatusEnum)id;
            }

            throw new ArgumentOutOfRangeException(nameof(id), id, null);

        }

        public static int ToId(this ProjectStatusEnum status)
        {
            return (int)status;
        }

        public static string ToString(this ProjectStatusEnum status)
        {
            switch (status)
            {
                case ProjectStatusEnum.NotStarted:
                    return "Not Started";
                case ProjectStatusEnum.InProgress:
                    return "In Progress";
                case ProjectStatusEnum.Completed:
                    return "Completed";
                case ProjectStatusEnum.Cancelled:
                    return "Cancelled";
                default:
                    throw new ArgumentOutOfRangeException(nameof(status), status, null);
            }
        }

        public static string ToString(this int statusId)
        {
            try
            {
                return ToString(FromId(statusId));
            }
            catch (ArgumentOutOfRangeException e)
            {
                throw e;
            }
        }
    }
}
