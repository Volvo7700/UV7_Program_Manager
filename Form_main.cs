using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Reflection;
using System.Diagnostics;
using System.Runtime.InteropServices;
using UV7_Program_Manager.Config;
using UV7_Program_Manager.CustomControls;
using UV7_Program_Manager.Dialogs;
using UV7_Program_Manager.ShellTools;

namespace UV7_Program_Manager
{
    public partial class Form_main : Form
    {
        #region Form Methods

        #region DWM and Constructor
        [Flags]
        public enum DwmWindowAttribute : uint
        {
            DWMWA_NCRENDERING_ENABLED = 1,
            DWMWA_NCRENDERING_POLICY,
            DWMWA_TRANSITIONS_FORCEDISABLED,
            DWMWA_ALLOW_NCPAINT,
            DWMWA_CAPTION_BUTTON_BOUNDS,
            DWMWA_NONCLIENT_RTL_LAYOUT,
            DWMWA_FORCE_ICONIC_REPRESENTATION,
            DWMWA_FLIP3D_POLICY,
            DWMWA_EXTENDED_FRAME_BOUNDS,
            DWMWA_HAS_ICONIC_BITMAP,
            DWMWA_DISALLOW_PEEK,
            DWMWA_EXCLUDED_FROM_PEEK,
            DWMWA_CLOAK,
            DWMWA_CLOAKED,
            DWMWA_FREEZE_REPRESENTATION,
            DWMWA_LAST
        }

        [DllImport("dwmapi.dll", PreserveSig = true)]
        public static extern int DwmSetWindowAttribute(IntPtr hwnd, DwmWindowAttribute attr, ref int attrValue, int attrSize);

        public Form_main()
        {
            InitializeComponent();

            int value = 0x01;
            

            if (ApplicationConfig.DesktopCompositing)
            {
                if (Environment.OSVersion.Version.Major > 5)
                {
                    DwmSetWindowAttribute(Handle, DwmWindowAttribute.DWMWA_NCRENDERING_POLICY, ref value, Marshal.SizeOf(typeof(int)));
                }
            }
            else
            {
                DwmSetWindowAttribute(Handle, DwmWindowAttribute.DWMWA_NCRENDERING_ENABLED, ref value, Marshal.SizeOf(typeof(int)));
            }
            
            toolStripStatusLabel_status.Text = Dialog.Main_StatusStrip_Load;

            MDIClientSupport.SetBevel(this, false);

            this.Resize += delegate { this.Refresh(); };

            foreach (Control ctl in this.Controls)
            {    // Find the MDI client window
                if (ctl is MdiClient)
                {
                    // Hackorama to avoid flicker:
                    var dblBuf = ctl.GetType().GetProperty("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance);
                    dblBuf.SetValue(ctl, true, null);
                    break;
                }
            }
            this.SetStyle(ControlStyles.UserPaint |
                     ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.OptimizedDoubleBuffer, true);
        }

        #endregion DWM and Constructor

        #region Desktop Mode Native Methods

        private bool _enableOverride;
        internal class NativeMethods
        {
            public const int WM_WINDOWPOSCHANGING = 0x46;
            public const int WM_WINDOWPOSCHANGED = 0x47;
            public const int GWL_HWNDPARENT = -8;
            public const int SW_SHOW = 1;


            [Flags()]
            public enum SetWindowPosFlags
            {
                SWP_NOSIZE = 0x1,
                SWP_NOMOVE = 0x2,
                SWP_NOZORDER = 0x4,
                SWP_NOREDRAW = 0x8,
                SWP_NOACTIVATE = 0x10,
                SWP_FRAMECHANGED = 0x20,
                SWP_DRAWFRAME = SWP_FRAMECHANGED,
                SWP_SHOWWINDOW = 0x40,
                SWP_HIDEWINDOW = 0x80,
                SWP_NOCOPYBITS = 0x100,
                SWP_NOOWNERZORDER = 0x200,
                SWP_NOREPOSITION = SWP_NOOWNERZORDER,
                SWP_NOSENDCHANGING = 0x400,
                SWP_DEFERERASE = 0x2000,
                SWP_ASYNCWINDOWPOS = 0x4000,
            }

            public enum WindowZOrder
            {
                HWND_TOP = 0,
                HWND_BOTTOM = 1,
                HWND_TOPMOST = -1,
                HWND_NOTOPMOST = -2,
            }

            [StructLayout(LayoutKind.Sequential)]
            public struct WINDOWPOS
            {
                public IntPtr hWnd;
                public IntPtr hwndInsertAfter;
                public int x;
                public int y;
                public int cx;
                public int cy;
                public SetWindowPosFlags flags;

                // Returns the WINDOWPOS structure pointed to by the lParam parameter
                // of a WM_WINDOWPOSCHANGING or WM_WINDOWPOSCHANGED message.
                public static WINDOWPOS FromMessage(Message msg)
                {
                    // Marshal the lParam parameter to an WINDOWPOS structure,
                    // and return the new structure
                    return (WINDOWPOS)Marshal.PtrToStructure(msg.LParam, typeof(WINDOWPOS));
                }

                // Replaces the original WINDOWPOS structure pointed to by the lParam
                // parameter of a WM_WINDOWPOSCHANGING or WM_WINDOWPSCHANGING message
                // with this one, so that the native window will be able to see any
                // changes that we have made to its values.
                public void UpdateMessage(Message msg)
                {
                    // Marshal this updated structure back to lParam so the native
                    // window can respond to our changes.
                    // The old structure that it points to should be deleted, too.
                    Marshal.StructureToPtr(this, msg.LParam, true);
                }
            }
        }


        public static class HWND
        {
            public static readonly IntPtr
            NOTOPMOST = new IntPtr(-2),
            BROADCAST = new IntPtr(0xffff),
            TOPMOST = new IntPtr(-1),
            TOP = new IntPtr(0),
            BOTTOM = new IntPtr(1);
        }


        public static class SWP
        {
            public static readonly int
            NOSIZE = 0x0001,
            NOMOVE = 0x0002,
            NOZORDER = 0x0004,
            NOREDRAW = 0x0008,
            NOACTIVATE = 0x0010,
            DRAWFRAME = 0x0020,
            FRAMECHANGED = 0x0020,
            SHOWWINDOW = 0x0040,
            HIDEWINDOW = 0x0080,
            NOCOPYBITS = 0x0100,
            NOOWNERZORDER = 0x0200,
            NOREPOSITION = 0x0200,
            NOSENDCHANGING = 0x0400,
            DEFERERASE = 0x2000,
            ASYNCWINDOWPOS = 0x4000;
        }


        [DllImport("user32.dll", SetLastError = true)]
        static extern int SetWindowLong(IntPtr hWnd, int nIndex, IntPtr dwNewLong);

        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr FindWindow(string lpWindowClass, string lpWindowName);

        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr FindWindowEx(IntPtr parentHandle, IntPtr childAfter, string className, string windowTitle);

        [DllImport("user32.dll")]
        public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, int uFlags);

        [DllImport("user32.dll")]
        private static extern int ShowWindow(IntPtr hwnd, int command);

        protected override void WndProc(ref Message m)
        {
            if (_enableOverride)
            {
                if (m.Msg == NativeMethods.WM_WINDOWPOSCHANGING)
                {
                    // Extract the WINDOWPOS structure corresponding to this message
                    NativeMethods.WINDOWPOS wndPos = NativeMethods.WINDOWPOS.FromMessage(m);

                    wndPos.flags = wndPos.flags | NativeMethods.SetWindowPosFlags.SWP_NOZORDER;
                    wndPos.UpdateMessage(m);
                }
            }
            base.WndProc(ref m);
        }

        private void timer_desktop_Tick(object sender, EventArgs e)
        {
            SetWindowPos(Handle, HWND.BOTTOM, 0, 0, 0, 0, SWP.SHOWWINDOW | SWP.NOMOVE | SWP.NOOWNERZORDER | SWP.NOSIZE | SWP.NOACTIVATE);

            //IntPtr task = FindWindow("Shell_TrayWnd", "");
            //ShowWindow(task, NativeMethods.SW_SHOW);

            _enableOverride = true;
            string h = String.Format("{0:00}", DateTime.Now.Hour);
            string m = String.Format("{0:00}", DateTime.Now.Minute);
            label_time.Text = h + ":" + m;
        }

        private void ShowPowerOptions(object sender, EventArgs e)
        {
            Shell32.Shell shell = new Shell32.Shell();
            shell.ShutdownWindows();
        }

        private void ChangeScreen(object sender, EventArgs e)
        {
            contextMenu_changeScreen.MenuItems.Clear();
            Screen[] screens = Screen.AllScreens;
            for (int i = 0; i < screens.Length; i++)
            {
                Screen s = screens[i];
                String w = String.Format("{0:0000}", s.Bounds.Width);
                String h = String.Format("{0:0000}", s.Bounds.Height);
                String primary = "  ";
                if (s.Primary) primary += "[Primary]";
                contextMenu_changeScreen.MenuItems.Add(String.Format("#{0}  {1}x{2}  {3}{4}", i, w, h, s.DeviceFriendlyName(), primary), ChangeScreenContextMenu_Click);
            }
            contextMenu_changeScreen.Show(dropDownButton_changeScreen, new Point(0, 0 + dropDownButton_changeScreen.Height));
        }

        private void ChangeScreenContextMenu_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < contextMenu_changeScreen.MenuItems.Count; i++)
            {
                if (sender == contextMenu_changeScreen.MenuItems[i])
                {
                    ApplicationConfig.DesktopScreen = i;
                    this.WindowState = FormWindowState.Normal;
                    this.Location = Screen.AllScreens[i].WorkingArea.Location;
                    this.WindowState = FormWindowState.Maximized;
                    this.Size = Screen.AllScreens[i].WorkingArea.Size;
                }
            }
        }

        #endregion Desktop Mode Native Methods


        private NativeToolStripRenderer ntsr = new NativeToolStripRenderer(new ToolbarTheme());
        FormWindowState lastWindowState;
        int lastLayout;

        private void Form_main_Load(object sender, EventArgs e)
        {
            Console.WriteLine($"Classic Theme: {WinShell.IsClassicThemeActive()}");
            ntsr.Theme = ToolbarTheme.Toolbar;
            menuStrip_main.Renderer = ntsr;
            
            if (ApplicationConfig.DesktopMode)
            {
                menuStrip_main.Padding = new Padding(menuStrip_main.Padding.Left + 2, menuStrip_main.Padding.Top, menuStrip_main.Padding.Right, menuStrip_main.Padding.Bottom);
                label_machineName.Text = Dialog.Main_StatusStrip_Load;
                label_userName.Text = Environment.UserName;
                string h = String.Format("{0:00}", DateTime.Now.Hour);
                string m = String.Format("{0:00}", DateTime.Now.Minute);
                label_time.Text = h + ":" + m;

                gradientPanel_desktop.Visible = true;
                this.FormBorderStyle = FormBorderStyle.Sizable;
                
                try
                {
                    this.Size = Screen.AllScreens[ApplicationConfig.DesktopScreen].WorkingArea.Size;
                    this.WindowState = FormWindowState.Normal; 
                    this.Location = Screen.AllScreens[ApplicationConfig.DesktopScreen].WorkingArea.Location;
                }
                catch
                {
                    this.Size = Screen.AllScreens[0].WorkingArea.Size;
                    this.WindowState = FormWindowState.Normal; 
                    this.Location = Screen.AllScreens[0].WorkingArea.Location;
                }
                
                this.WindowState = FormWindowState.Maximized;
                this.MaximumSize = this.Size;
                this.FormBorderStyle = FormBorderStyle.None;

                
                IntPtr hprog = FindWindowEx(
                    FindWindowEx(
                        FindWindow("Progman", "Program Manager"),
                        IntPtr.Zero, "SHELLDLL_DefView", ""
                    ),
                    IntPtr.Zero, "SysListView32", "FolderView"
                );

                SetWindowLong(this.Handle, NativeMethods.GWL_HWNDPARENT, hprog);

                label_userName.Font = new Font("Verdana", 12);
                label_machineName.Font = new Font("Verdana", 12, FontStyle.Bold);
                label_time.Font = new Font("Verdana", 12);

                timer_desktop.Start();

                ApplicationConfig.BackgroundColor = ApplicationConfig.BackgroundColor;
                ApplicationConfig.BackgroundImageLayout = ApplicationConfig.BackgroundImageLayout;
                ApplicationConfig.BackgroundImage = ApplicationConfig.BackgroundImage;
                ApplicationConfig.Font = ApplicationConfig.Font;
                ApplicationConfig.MenuBarStyle = ApplicationConfig.MenuBarStyle;
            }
            
            else
            {
                if (Properties.Settings.Default.Maximized)
                {
                    Location = Properties.Settings.Default.Location;
                    WindowState = FormWindowState.Maximized;
                    Size = Properties.Settings.Default.Size;
                }
                else if (Properties.Settings.Default.Minimized)
                {
                    Location = Properties.Settings.Default.Location;
                    WindowState = FormWindowState.Minimized;
                    Size = Properties.Settings.Default.Size;
                }
                else
                {
                    Location = Properties.Settings.Default.Location;
                    Size = Properties.Settings.Default.Size;
                }
                lastWindowState = this.WindowState;
                lastLayout = ApplicationConfig.StartupLayout;

                ApplicationConfig.BackgroundColor = ApplicationConfig.BackgroundColor;
                ApplicationConfig.BackgroundImageLayout = ApplicationConfig.BackgroundImageLayout;
                ApplicationConfig.BackgroundImage = ApplicationConfig.BackgroundImage;
                ApplicationConfig.Font = ApplicationConfig.Font;
                ApplicationConfig.MenuBarStyle = ApplicationConfig.MenuBarStyle;
                ApplicationConfig.TopMost = ApplicationConfig.TopMost;
            }
        }

        private void Form_main_Shown(object sender, EventArgs e)
        {
            LoadData();
            this.Text = "UV7 Program Manager - " + Environment.MachineName + "\\" + Environment.UserName;
        }

        private void Form_main_FormClosing(object sender, FormClosingEventArgs e)
        {
            TaskbarIcon.Icon.Visible = false;
            
            if (!ApplicationConfig.DesktopMode)
            {
                if (WindowState == FormWindowState.Maximized)
                {
                    Properties.Settings.Default.Location = RestoreBounds.Location;
                    Properties.Settings.Default.Size = RestoreBounds.Size;
                    Properties.Settings.Default.Maximized = true;
                    Properties.Settings.Default.Minimized = false;
                }
                else if (WindowState == FormWindowState.Normal)
                {
                    Properties.Settings.Default.Location = Location;
                    Properties.Settings.Default.Size = Size;
                    Properties.Settings.Default.Maximized = false;
                    Properties.Settings.Default.Minimized = false;
                }
                else
                {
                    Properties.Settings.Default.Location = RestoreBounds.Location;
                    Properties.Settings.Default.Size = RestoreBounds.Size;
                    Properties.Settings.Default.Maximized = false;
                    Properties.Settings.Default.Minimized = true;
                }
                Properties.Settings.Default.Save();
            }
        }

        private void Form_main_BackColorChanged(object sender, EventArgs e)
        {
            foreach (MdiClient m in this.Controls.OfType<MdiClient>())
            {
                m.BackColor = this.BackColor;
            }
        }

        private void Form_main_ResizeEnd(object sender, EventArgs e)
        {
            switch (lastLayout)
            {
                case 0:
                    ManualLayoutCascade();
                    break;
                case 1:
                    foreach (Form f in this.MdiChildren.Reverse())
                        f.BringToFront();
                    LayoutMdi(MdiLayout.TileVertical);
                    break;
                case 2:
                    foreach (Form f in this.MdiChildren.Reverse())
                        f.BringToFront();
                    LayoutMdi(MdiLayout.TileHorizontal);
                    break;
            }

            if (ApplicationConfig.ProgramGroupsTransparent)
            {
                foreach (Form_prog fp in Application.OpenForms.OfType<Form_prog>())
                {
                    foreach (NativeListView n in fp.Controls.OfType<NativeListView>())
                    {
                        n.DrawTransparentBackground();
                    }
                }
            }
        }
        private void Form_main_Resize(object sender, EventArgs e)
        {
            if (lastWindowState != this.WindowState)
            {
                lastWindowState = this.WindowState;

                switch (lastLayout)
                {
                    case 0:
                        ManualLayoutCascade();
                        break;
                    case 1:
                        foreach (Form f in this.MdiChildren.Reverse())
                            f.BringToFront();
                        LayoutMdi(MdiLayout.TileVertical);
                        break;
                    case 2:
                        foreach (Form f in this.MdiChildren.Reverse())
                            f.BringToFront();
                        LayoutMdi(MdiLayout.TileHorizontal);
                        break;
                }
            }
        }

        #endregion Form Methods

        #region Public Methods

        public void ChangeMenuBarStyle(int style)
        {
            switch (style)
            {
                case 0:
                    ntsr.Theme = ToolbarTheme.Toolbar;
                    break;
                case 1:
                    ntsr.Theme = ToolbarTheme.BrowserTabBar;
                    break;
                case 2:
                    ntsr.Theme = ToolbarTheme.MediaToolbar;
                    break;
                case 3:
                    ntsr.Theme = ToolbarTheme.CommunicationsToolbar;
                    break;
            }
            menuStrip_main.Invalidate();
        }

        #endregion Public Methods

        #region Custom Methods

        private void ManualLayoutCascade()
        {
            int indent = 0;
            foreach (Form_prog fp in this.MdiChildren.OfType<Form_prog>())
            {
                fp.Location = new Point(indent, indent);
                indent += SystemInformation.CaptionHeight + SystemInformation.FixedFrameBorderSize.Height;
            }
        }

        #endregion Custom Methods

        #region MenuStrip

        // File
        private void StartProgram(object sender, EventArgs e)
        {
            foreach (Form_prog f in this.MdiChildren.OfType<Form_prog>())
            {
                if (ActiveMdiChild == f) f.StartSelected();
            }
        }

        private void Run(object sender, EventArgs e)
        {
            Shell32.Shell shell = new Shell32.Shell();
            shell.FileRun();
        }

        private void ReloadEntries(object sender, EventArgs e)
        {
            foreach (Form_prog f in this.MdiChildren.OfType<Form_prog>())
            {
                f.Close();
                f.Dispose();
            }
            LoadData();
        }

        private void Exit(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ExitWindows(object sender, EventArgs e)
        {
            Shell32.Shell shell = new Shell32.Shell();
            shell.ShutdownWindows();
        }


        // Options
        private void Preferences(object sender, EventArgs e)
        {
            if (!CurrentConfig.PreferencesFormOpened)
            {
                Form_preferences fp = new Form_preferences();
                int posX = (this.Width - fp.Width) / 2;
                int posY = (this.Height - fp.Height) / 2;
                
                if (ApplicationConfig.NewRenderMode)
                {
                    posX += this.Location.X;
                    posY += this.Location.Y;
                    fp.Location = new Point(posX, posY);
                    fp.ShowDialog();
                }
                else
                {
                    fp.MdiParent = this;
                    fp.Location = new Point(posX, posY);
                    fp.Show();
                }    
            }
            else
            {
                foreach (Form_preferences f in this.MdiChildren.OfType<Form_preferences>())
                {
                    f.Activate();
                }
            }
        }

        private void OpenUsersStartMenuDirectory(object sender, EventArgs e)
        {
            Process.Start("explorer", WinShell.UsersStartMenuDir);
        }

        private void OpenSystemStartMenuDirectory(object sender, EventArgs e)
        {
            Process.Start("explorer", WinShell.SystemStartMenuDir);
        }

        private void OpenProgramsDirectory(object sender, EventArgs e)
        {
            Process.Start("explorer", Application.StartupPath + @"\Programs");
        }

        private void ResetMainWindowSize(object sender, EventArgs e)
        {
            this.Size = new Size(880, 560);
        }

        // Windows

        private void MdiRefreshLayout(object sender, EventArgs e)
        {
            switch (lastLayout)
            {
                case 0:
                    ManualLayoutCascade();
                    break;
                case 1:
                    foreach (Form f in this.MdiChildren.Reverse())
                        f.BringToFront();
                    LayoutMdi(MdiLayout.TileVertical);
                    break;
                case 2:
                    foreach (Form f in this.MdiChildren.Reverse())
                        f.BringToFront();
                    LayoutMdi(MdiLayout.TileHorizontal);
                    break;
            }
        }

        private void MdiCascade(object sender, EventArgs e)
        {
            ManualLayoutCascade();
            lastLayout = 0;
        }

        private void MdiTileVertical(object sender, EventArgs e)
        {
            foreach (Form f in this.MdiChildren.Reverse())
                f.BringToFront();
            LayoutMdi(MdiLayout.TileVertical);
            lastLayout = 1;
        }

        private void MdiTileHorizontal(object sender, EventArgs e)
        {
            foreach (Form f in this.MdiChildren.Reverse())
                f.BringToFront();
            LayoutMdi(MdiLayout.TileHorizontal);
            lastLayout = 2;
        }

        private void MdiArrangeIons(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.ArrangeIcons);
        }

        private void MdiMinimizeAll(object sender, EventArgs e)
        {
            foreach (Form_prog f in this.MdiChildren.OfType<Form_prog>())
            {
                if (f.WindowState != FormWindowState.Minimized) f.WindowState = FormWindowState.Minimized;
            }
        }

        private void MdiRestoreAll(object sender, EventArgs e)
        {
            foreach (Form_prog f in this.MdiChildren.OfType<Form_prog>())
            {
                if (f.WindowState != FormWindowState.Normal) f.WindowState = FormWindowState.Normal;
            }
        }

        private void MdiRestoreOriginalSize(object sender, EventArgs e)
        {
            foreach (Form_prog f in this.MdiChildren.OfType<Form_prog>())
            {
                f.Size = new Size(600, 260);
            }
        }

        // Help
        private void About(object sender, EventArgs e)
        {
            Form_about fa = new Form_about();
            int posX = this.Left + (this.Width / 2) - (fa.Width - 2);
            int posY = this.Top + (this.Height / 2) - (fa.Height - 2);
            fa.Location = new Point(posX, posY);
            fa.SetDesktopLocation(posX, posY);
            fa.TopMost = this.TopMost;
            //fa.Parent = this;
            fa.ShowDialog();
        }

        private int SecretCountdown = 2;

        private void Secret(object sender, EventArgs e)
        {
            if (SecretCountdown > 0)
            {
                SecretCountdown--;
                helpToolStripMenuItem.ShowDropDown();
            }
            else if (SecretCountdown == 0) MessageBox.Show("Congratulations! You have found an easter egg!\n[Insert easter egg here]", "Easter Egg", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // Desktop Mode
        private void DesktopMode(object sender, EventArgs e)
        {
            ApplicationConfig.DesktopMode = !ApplicationConfig.DesktopMode;
            Application.Restart();
        }

        #endregion MenuStrip

        #region StatusStrip

        private void statusStrip1_FontChanged(object sender, EventArgs e)
        {
            toolStripStatusLabel_errors.Font = new Font(statusStrip1.Font, toolStripStatusLabel_errors.Font.Style);
        }

        private void toolStripStatusLabel_errors_MouseEnter(object sender, EventArgs e)
        {
            toolStripStatusLabel_errors.ForeColor = Color.Red;
        }

        private void toolStripStatusLabel_errors_Click(object sender, EventArgs e)
        {
            if (!CurrentConfig.ErrorsFormOpened)
            {
                toolStripStatusLabel_errors.LinkVisited = true;

                Form_errors fe = new Form_errors();
                int posX = (this.Width / 2) - (fe.Width / 2);
                int posY = (this.Height / 2) - (fe.Height / 2);
                fe.Location = new Point(posX, posY);
                
                if (!ApplicationConfig.NewRenderMode)
                    fe.MdiParent = this;
                fe.Show();
                CurrentConfig.ErrorsFormOpened = true;
            }
            else
            {
                foreach (Form_errors f in this.MdiChildren.OfType<Form_errors>())
                {
                    f.Activate();
                }
            }
        }

        #endregion StatusStrip
    }
}
