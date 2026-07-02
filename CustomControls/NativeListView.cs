using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace UV7_Program_Manager.CustomControls
{
    // ListView with Explorer Theme and partial Transparency Support
    public class NativeListView : ListView
    {
        [DllImport("uxtheme.dll", CharSet = CharSet.Unicode)]
        public extern static Int32 SetWindowTheme(IntPtr hWnd, String textSubAppName, String textSubIdList);

        protected override void CreateHandle()
        {
            base.CreateHandle();
            SetWindowTheme(this.Handle, "explorer", null);
        }
        
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int SetWindowLongPtr(IntPtr hwnd, int index, int newStyle);
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)] 
        public static extern int SendMessage(IntPtr hWnd, int wMsg, IntPtr wParam, IntPtr lParam);

        public const int GWL_EXSTYLE = -20;
        public const int LVS_EX_TRANSPARENTBKGND = 0x00400000;
        public const int VM_SETEXTENDEDLISTVIEWSTYLE = 0x1036;
        public const int WM_PRINTCLIENT = 0x0318;

        public bool programGroupsLoaded = false;

        private bool transparentBackground = false;

        public bool TransparentBackground
        {
            get
            {
                return transparentBackground;
            }
            set
            {
                transparentBackground = value;
                //this.Invalidate();
            }
        }

        //public override Color BackColor
        //{
        //    get
        //    {
        //        return Color.DarkBlue;
        //    }
        //    set
        //    {

        //    }
        //}
        
        public NativeListView()
        {
            SetWindowLongPtr(this.Handle, GWL_EXSTYLE, LVS_EX_TRANSPARENTBKGND);
            SendMessage(this.Handle, VM_SETEXTENDEDLISTVIEWSTYLE, new IntPtr(LVS_EX_TRANSPARENTBKGND), IntPtr.Zero);
            if (this.Parent != null)
                SendMessage(this.Parent.Handle, WM_PRINTCLIENT, IntPtr.Zero, IntPtr.Zero);
        }

        //public NativeListView()
        //{
        //    this.SetStyle(System.Windows.Forms.ControlStyles.SupportsTransparentBackColor, true);
        //    this.BackColor = Color.Transparent;
        //}

        private bool backgroundDrawing = false;

        public void DrawTransparentBackground()
        {
            //try
            //{
            //    backgroundDrawing = true;
            //    this.Hide();
            //    Bitmap bmp = new Bitmap(this.Parent.Width, this.Parent.Height);

            //    this.Parent.DrawToBitmap(bmp, new Rectangle(0, 0, this.Parent.Width, this.Parent.Height));

            //    Bitmap source = new Bitmap(this.Width, this.Height);
            //    Graphics g = Graphics.FromImage(source);

            //    // Do not draw specific borders if window is beyond screen on that side
            //    int x, y;

            //    if (this.Parent.Location.X < 0)
            //        x = 0;
            //    else
            //        x = SystemInformation.FrameBorderSize.Width;

            //    if (this.Parent.Location.Y < 0)
            //        y = 0;
            //    else
            //        y = SystemInformation.CaptionHeight + SystemInformation.FrameBorderSize.Height;

            //    g.DrawImage(bmp, 0, 0, new Rectangle(x, y, this.Width, this.Height), GraphicsUnit.Pixel);
            //    this.BackgroundImage = source;
            //    this.Show();
            //    backgroundDrawing = false;
            //}
            //catch
            //{

            //}
        }

        //protected override void WndProc(ref Message m)
        //{
        //    switch (m.Msg)
        //    {
        //        case 0x0F: // WM_PAINT
        //            this.HandlePaint(ref m);
        //            break;
        //        default:
        //            base.WndProc(ref m);
        //            break;
        //    }
        //}

        //protected virtual void HandlePaint(ref Message m)
        //{
        //    base.WndProc(ref m);
        //    Graphics g = this.CreateGraphics();
        //    Control c = this.Parent.Parent;
        //    Bitmap bmp = new Bitmap(c.Width, c.Height);
        //    c.DrawToBitmap(bmp, c.ClientRectangle);
        //    Bitmap source = new Bitmap(g.ClipRectangle.Width, pevent.ClipRectangle.Height);
        //    pevent.Graphics.DrawImage(source, 0, 0, pevent.ClipRectangle, GraphicsUnit.Pixel);
        //    this.BackgroundImage = source;
        //}

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            this.Invalidate();
        }

        protected override void OnInvalidated(InvalidateEventArgs e)
        {
            base.OnInvalidated(e);
            if (!backgroundDrawing)
            {
                if (TransparentBackground)
                {
                    DrawTransparentBackground();
                }
                else
                {
                    this.BackgroundImage = null;
                }
            }
        }
    }
}
