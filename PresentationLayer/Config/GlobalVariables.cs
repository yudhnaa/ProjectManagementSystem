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
        public static Color HighPriorityColor { get; set; } = Color.Red;
        public static Color MediumPriorityColor { get; set; } = Color.Yellow;
        public static Color LowPriorityColor { get; set; } = Color.FromArgb(128, 255, 128);

        // Status Color
        public static Color NotStartedStatusColor { get; set; } = Color.LightGray;
        public static Color InProgressStatusColor { get; set; } = Color.Yellow;
        public static Color CompletedStatusColor { get; set; } = Color.FromArgb(128, 255, 128);
        public static Color CancelledStatusColor { get; set; } = Color.Red;

        public static int PageSize { get; set; } = 20;

    }
}
