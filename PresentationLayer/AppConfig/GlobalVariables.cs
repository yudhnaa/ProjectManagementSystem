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
        public static Color CriticalPriorityColor { get;  } = Color.FromArgb(245, 12, 77);
        public static Color HighPriorityColor { get;  } = Color.FromArgb(246, 57, 110);
        public static Color MediumPriorityColor { get;  } = Color.FromArgb(255, 246, 133);
        public static Color LowPriorityColor { get;  } = Color.FromArgb(111, 151, 92);

        // Status Color
        public static Color NotStartedStatusColor { get;  } = Color.LightGray;
        public static Color InProgressStatusColor { get;  } = Color.Yellow;
        public static Color CompletedStatusColor { get;  } = Color.FromArgb(128, 255, 128);
        public static Color CancelledStatusColor { get;  } = Color.Red;

        // User Status Color
        public static Color ActiveStatusColor { get;  } = Color.FromArgb(128, 255, 128);
        public static Color InactiveStatusColor { get;  } = Color.Red;

        // Button Colors
        public static Color ButtonHoverFillColor { get;  } = Color.WhiteSmoke;
        public static Color ButtonPressedFillColor { get;  } = Color.WhiteSmoke;
        public static Color ButtonIdleFillColor { get;  } = Color.White;
        public static Color ButtonBorderColor { get;  } = Color.White;

        //Font color
        public static Color FontColorLightBackground { get;  } = Color.Black;
        public static Color FontColorLightBackground2 { get;  } = Color.DarkGray;

        public static Color FontColorDarkBackground { get;  } = Color.White;
        public static Color FontColorDarkBackground2 { get;  } = Color.WhiteSmoke;


        //Pagination
        public static int PageSize { get;  } = 10;
    }
}
