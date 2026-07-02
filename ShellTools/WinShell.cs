using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;

namespace UV7_Program_Manager.ShellTools
{
    public static class WinShell
    {
        [DllImport("shell32.dll")]
        private static extern bool SHGetSpecialFolderPath(IntPtr hwndOwner,
            [Out] StringBuilder lpszPath, int nFolder, bool fCreate);
        private const int CSIDL_STARTMENU = 0xB;  // All Users\Start Menu
        private const int CSIDL_COMMON_STARTMENU = 0x16;  // All Users\Start Menu
        private const int CSIDL_COMMON_STARTUP = 0x18;  // All Users\Start Menu\Programs\Startup

        public static string UsersStartMenuDir
        {
            get
            {
                StringBuilder path = new StringBuilder(260);
                SHGetSpecialFolderPath(IntPtr.Zero, path, CSIDL_STARTMENU, false);
                return path.ToString();
            }
        }
        
        public static string SystemStartMenuDir
        {
            get
            {
                StringBuilder path = new StringBuilder(260);
                SHGetSpecialFolderPath(IntPtr.Zero, path, CSIDL_COMMON_STARTMENU, false);
                return path.ToString();
            }
        }

        [DllImport("uxtheme.dll")] private static extern int GetThemeAppProperties();

        public static bool IsClassicThemeActive()
        {
            // Detect if classic theme is active
            return GetThemeAppProperties() == 0;
        }
    }
}
