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
        public static Color CriticalPriorityColor { get; set; } = Color.FromArgb(245, 12, 77);
        public static Color HighPriorityColor { get; set; } = Color.FromArgb(246, 57, 110);
        public static Color MediumPriorityColor { get; set; } = Color.FromArgb(255, 246, 133);
        public static Color LowPriorityColor { get; set; } = Color.FromArgb(111, 151, 92);

        // Status Color
        public static Color NotStartedStatusColor { get; set; } = Color.LightGray;
        public static Color InProgressStatusColor { get; set; } = Color.Yellow;
        public static Color CompletedStatusColor { get; set; } = Color.FromArgb(128, 255, 128);
        public static Color CancelledStatusColor { get; set; } = Color.Red;

        // User Status Color
        public static Color ActiveStatusColor { get; set; } = Color.FromArgb(128, 255, 128);
        public static Color InactiveStatusColor { get; set; } = Color.Red;

        // Button Colors
        public static Color ButtonHoverFillColor { get; set; } = Color.FromArgb(176, 196, 222);
        public static Color ButtonPressedFillColor { get; set; } = Color.FromArgb(30, 144, 255);


        public static Color ButtonIdleFillColor { get; set; } = Color.White;
        public static Color ButtonBorderColor { get; set; } = Color.White;//

        //Font color
        public static Color FontColorLightBackground { get; set; } = Color.White;//
        public static Color FontColorLightBackground2 { get; set; } = Color.DarkGray;

        public static Color FontColorDarkBackground { get; set; } = Color.White;
        public static Color FontColorDarkBackground2 { get; set; } = Color.WhiteSmoke;


        //Pagination
        public static int PageSize { get; set; } = 10;

        //button color change
        public static Color ButtonActiveFillColor = Color.FromArgb(0, 122, 204); // Xanh dương
        public static Color ButtonActiveBorderColor = Color.FromArgb(0, 100, 180);
       


    }
}
