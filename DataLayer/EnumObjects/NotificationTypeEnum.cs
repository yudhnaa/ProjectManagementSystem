using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.EnumObjects
{
    public enum NotificationTypeEnum
    {
        TaskHelpRequest = 1,
        ProjectInvitation = 2,
        TaskExpiring = 3,
        TaskAssignment = 4,
        CommentAdded = 5
    }
}
