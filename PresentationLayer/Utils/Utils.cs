using PresentationLayer.Config;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationLayer.Utils
{
    public static class Utils
    {
        public static Color GetPriorityColor(String priorityName)
        {
            switch (priorityName)
            {
                case "High":
                    return GlobalVariables.HighPriorityColor;
                case "Medium":
                    return GlobalVariables.MediumPriorityColor;
                case "Low":
                    return GlobalVariables.LowPriorityColor;
                default:
                    return Color.White;
            }
        }

        public static Color GetStatusColor(String statusName)
        {
            switch (statusName)
            {
                case "Not Started":
                    return GlobalVariables.NotStartedStatusColor;
                case "In Progress":
                    return GlobalVariables.InProgressStatusColor;
                case "Completed":
                    return GlobalVariables.CompletedStatusColor;
                case "Cancelled":
                    return GlobalVariables.CancelledStatusColor;
                default:
                    return Color.White;
            }
        }
    }
}
