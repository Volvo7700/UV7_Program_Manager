using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace UV7_Program_Manager.Tools
{
    public static class ClearBitmap
    {
        public static Bitmap Generate32()
        {
            Bitmap bmp = new Bitmap(32, 32);
            using (Graphics gr = Graphics.FromImage(bmp))
            {
                gr.Clear(Color.FromKnownColor(KnownColor.Transparent));
            }
            return bmp;
        }

        public static Bitmap Generate16()
        {
            Bitmap bmp = new Bitmap(16, 16);
            using (Graphics gr = Graphics.FromImage(bmp))
            {
                gr.Clear(Color.FromKnownColor(KnownColor.Transparent));
            }
            return bmp;
        }
    }
}
