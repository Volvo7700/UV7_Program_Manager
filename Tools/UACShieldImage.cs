using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace UV7_Program_Manager.Tools
{
    public static class UACShieldImage
    {
        public static Bitmap AddUACShield(Bitmap bmp)
        {
            Icon shieldIcon = IconUtils.GetSystemIcon(IconUtils.SHSTOCKICONID.SIID_SHIELD, IconUtils.SHGSI.SHGSI_ICON | IconUtils.SHGSI.SHGSI_SMALLICON);
            if (shieldIcon != null)
                Graphics.FromImage(bmp).DrawIcon(shieldIcon, bmp.Width - 16, bmp.Height - 16);
            return bmp;
        }
    }
}
