using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace UV7_Program_Manager.CustomControls
{
    // Panel with Gradient Background
    public class GradientPanel : Panel
    {
        public GradientPanel()
        {

        }

        private Color color1 = Color.DarkSlateGray;
        private Color color2 = Color.DarkOrange;
        private float angle;

        public Color Color1
        {
            get
            {
                return color1;
            }
            set
            {
                color1 = value;
                this.Invalidate();
            }
        }

        public Color Color2
        {
            get
            {
                return color2;
            }
            set
            {
                color2 = value;
                this.Invalidate();
            }
        }

        public float Angle
        {
            get
            {
                return angle;
            }
            set
            {
                angle = value;
                this.Invalidate();
            }
        }
        protected override void OnPaintBackground(PaintEventArgs e)
        {
            base.OnPaintBackground(e);
            LinearGradientBrush lgb = new LinearGradientBrush(e.ClipRectangle, Color1, Color2, Angle);
            e.Graphics.FillRectangle(lgb, e.ClipRectangle);
        }
    }
}
