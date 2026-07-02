using System;
using System.Drawing;
using System.Windows.Forms;

namespace UV7_Program_Manager.CustomControls
{
    // Custom Implementation of DropDownButton
    public class DropDownButton : Button
    {
        private int dropDownArrowSize = 8;

        public int DropDownArrowSize
        {
            get
            {
                return dropDownArrowSize;
            }
            set
            {
                dropDownArrowSize = value;
                this.Invalidate();
            }
        }

        protected override void OnPaint(PaintEventArgs pevent)
        {
            base.OnPaint(pevent);
            Rectangle r = new Rectangle(this.Width - this.Height, 0, this.Height, this.Height);
            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Center;
            sf.LineAlignment = StringAlignment.Center;
            pevent.Graphics.DrawString("u", new Font("Marlett", DropDownArrowSize), SystemBrushes.ControlText, r, sf);
        }

        protected override void OnClick(EventArgs e)
        {
            Point P = new Point(this.Location.X, this.Location.Y + this.Height);
            if (this.ContextMenu != null)
                this.ContextMenu.Show(this, PointToScreen(P));
            base.OnClick(e);
        }
    }
}
