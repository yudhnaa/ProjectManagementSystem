using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PresentationLayer.Controls.Others
{
    public class ReadOnlyCombobox : ComboBox
    {
        private bool isReadOnly = false;
        private int lockedIndex = 0;

        public bool IsReadOnly
        {
            get => isReadOnly;
            set
            {
                isReadOnly = value;
                if (isReadOnly)
                    lockedIndex = this.SelectedIndex;
            }
        }

        // Các mã Windows Message cần chặn
        private const int WM_LBUTTONDOWN = 0x0201;
        private const int CB_SHOWDROPDOWN = 0x14F;

        protected override void WndProc(ref Message m)
        {
            if (isReadOnly)
            {
                if (m.Msg == WM_LBUTTONDOWN || m.Msg == CB_SHOWDROPDOWN)
                {
                    // Ngăn không cho dropdown được hiển thị
                    return;
                }
            }
            base.WndProc(ref m);
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (isReadOnly)
            {
                e.SuppressKeyPress = true;
            }
            else
            {
                base.OnKeyDown(e);
            }
        }

        protected override void OnSelectionChangeCommitted(EventArgs e)
        {
            if (!isReadOnly)
            {
                base.OnSelectionChangeCommitted(e);
            }
            else
            {
                this.SelectedIndex = lockedIndex;
            }
        }
    }
}
