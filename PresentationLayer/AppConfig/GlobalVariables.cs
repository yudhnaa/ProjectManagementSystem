using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationLayer.Config
{
    public static class GlobalVariables
    {
        // Priority Color
        public static Color CriticalPriorityColor { get; set; } = Color.FromArgb(248, 187, 208);
        public static Color HighPriorityColor { get; set; } = Color.FromArgb(255, 224, 178);
        public static Color MediumPriorityColor { get; set; } = Color.FromArgb(255, 249, 196);
        public static Color LowPriorityColor { get; set; } = Color.FromArgb(213, 245, 227);

        // Status Color
        public static Color NotStartedStatusColor { get; set; } = Color.LightGray;
        public static Color InProgressStatusColor { get; set; } = Color.FromArgb(225, 190, 231);
        public static Color CompletedStatusColor { get; set; } = Color.FromArgb(200, 230, 201);
        public static Color CancelledStatusColor { get; set; } = Color.FromArgb(174, 223, 247);

        // User Status Color
        public static Color ActiveStatusColor { get;  } = Color.FromArgb(128, 255, 128);
        public static Color InactiveStatusColor { get;  } = Color.Red;

        // Button Colors
        public static Color ButtonHoverFillColor { get; } = Color.FromArgb(176, 196, 222);
        public static Color ButtonPressedFillColor { get; } = Color.FromArgb(30, 144, 255);
        public static Color ButtonIdleFillColor { get;  } = Color.White;
        public static Color ButtonBorderColor { get;  } = Color.White;

        //Font color
        //public static Color FontColorLightBackground { get; set; } = Color.White;//
        //public static Color FontColorLightBackground2 { get; set; } = Color.DarkGray;
        public static Color FontColorLightBackground { get;  } = Color.Black;
        public static Color FontColorLightBackground2 { get;  } = Color.DarkGray;

        public static Color FontColorDarkBackground { get;  } = Color.White;
        public static Color FontColorDarkBackground2 { get;  } = Color.WhiteSmoke;


        //Pagination
        public static int PageSize { get; set; } = 10;

        //button color change
        public static Color ButtonActiveFillColor = Color.FromArgb(0, 122, 204); // Xanh dương
        public static Color ButtonActiveBorderColor = Color.FromArgb(0, 100, 180);

        public static string TaskHelpRequestTitle = "Help Requested";
        public static string TaskHelpRequestMSG = "{0} has requested your help with task {1}";
        
        public static string ProjectInvitationTitle = "Project Invitation";
        public static string ProjectInvitationMSG = "You have been invited to join {0}";
        
        public static string TaskExpiringTitle = "Task Approaching Due Date";
        public static string TaskExpiringMSG = "Task {0} is due in {1} days";
        
        public static string TaskAssignmentTitle = "Help Requested";
        public static string TaskAssignmentMSG = "You have been assigned to task {0}";
        
        public static string CommentAddedTitle = "New Comment";
        public static string CommentAddedMSG = "{0} commented on task {1}";



    }
}
