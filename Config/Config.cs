using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Drawing;
using System.IO;
using UV7_Program_Manager.CustomControls;

namespace UV7_Program_Manager.Config
{
    public static partial class ApplicationConfig
    {
        private static int visualStyle;
        private static bool desktopCompositing;
        private static Font font;
        private static int fontSelection;
        private static bool whiteFont;
        private static int menuBarStyle;
        private static bool topMost;

        private static Color backgroundColor;
        private static bool backgroundImage;
        private static int backgroundImageLayout;
        private static bool programGroupsApplyColor;
        private static bool programGroupsTransparent;
        private static int startupLayout;

        private static bool language;

        private static bool desktopMode;
        private static int desktopScreen;

        private static bool taskbarIcon;
        private static Color taskbarIconColor;
        private static bool taskbarIconShutdown;

        private static bool newRenderMode;

        public static int VisualStyle
        {
            get
            {
                return visualStyle;
            }
            set
            {
                visualStyle = value;
                switch (value)
                {
                    case 0:
                        Application.VisualStyleState = System.Windows.Forms.VisualStyles.VisualStyleState.ClientAndNonClientAreasEnabled;
                        break;
                    case 1:
                        Application.VisualStyleState = System.Windows.Forms.VisualStyles.VisualStyleState.ClientAreaEnabled;
                        break;
                    case 2:
                        Application.VisualStyleState = System.Windows.Forms.VisualStyles.VisualStyleState.NonClientAreaEnabled;
                        break;
                    case 3:
                        Application.VisualStyleState = System.Windows.Forms.VisualStyles.VisualStyleState.NoneEnabled;
                        break;
                }
            }
        }

        public static bool DesktopCompositing
        {
            get
            {
                return desktopCompositing;
            }
            set
            {
                desktopCompositing = value;
            }
        }

        public static Font Font
        {
            get
            {
                return font;
            }
            set
            {
                if (value.Size < 6) font = new Font(value.FontFamily, 6);
                if (value.Size > 10) font = new Font(value.FontFamily, 10);

                font = value;
                foreach (Form f in Application.OpenForms.OfType<Form_main>())
                {
                    void SetAllControlsFont(Control.ControlCollection ctrls)
                    {
                        foreach (Control ctrl in ctrls)
                        {
                            if (ctrl is GradientPanel)
                            {

                            }
                            else
                            {
                                if (ctrl.Controls != null)
                                    SetAllControlsFont(ctrl.Controls);

                                ctrl.Font = new Font(ApplicationConfig.Font, ctrl.Font.Style);
                            }
                        }
                    }
                    SetAllControlsFont(f.Controls);
                    f.Invalidate();
                }
            }
        }

        public static int FontSelection
        {
            get
            {
                return fontSelection;
            }
            set
            {
                fontSelection = value;
                switch (value)
                {
                    case 0:
                        Font = new Font("Tahoma", 8);
                        break;
                    case 1:
                        Font = new Font("Microsoft Sans Serif", 8);
                        break;
                    case 2:
                        Font = new Font("Segoe UI", 8);
                        break;
                    case 3:
                        Font = new Font("Segoe UI", 9);
                        break;
                }
            }
        }

        public static bool WhiteFont
        {
            get
            {
                return whiteFont;
            }
            set
            {
                whiteFont = value;
                foreach (Form_prog f in Application.OpenForms.OfType<Form_prog>())
                {
                    f.LoadFontColor();
                }
            }
        }

        public static int MenuBarStyle
        {
            get
            {
                return menuBarStyle;
            }
            set
            {
                menuBarStyle = value;
                foreach (Form_main f in Application.OpenForms.OfType<Form_main>())
                {
                    f.ChangeMenuBarStyle(value);
                }
            }
        }

        public static bool TopMost
        {
            get
            {
                return topMost;
            }
            set
            {
                topMost = value;
                foreach (Form_main f in Application.OpenForms.OfType<Form_main>())
                {
                    f.TopMost = value;
                }
            }
        }

        public static Color BackgroundColor
        {
            get
            {
                return backgroundColor;
            }
            set
            {
                backgroundColor = value;
                foreach (Form_main f in Application.OpenForms.OfType<Form_main>())
                {
                    f.BackColor = value;
                }
                DrawProgBackground(value);
            }
        }

        public static bool BackgroundImage
        {
            get
            {
                return backgroundImage;
            }
            set
            {
                backgroundImage = value;

                foreach (Form_main f in Application.OpenForms.OfType<Form_main>())
                {
                    if (BackgroundImage)
                    {
                        try
                        {
                            if (File.Exists(Application.StartupPath + @"\background"))
                            {
                                using (FileStream fs = new FileStream(Application.StartupPath + @"\background", FileMode.Open))
                                {
                                    Image image = Image.FromStream(fs);
                                    f.BackgroundImage = image;

                                    DrawProgBackground(BackgroundColor);
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                        string msg = Dialog.Config_ImageLoadError;    
                        string title = Dialog.Config_ImageLoadErrorTitle;    
                        DialogResult result = MessageBox.Show(msg + ex.Message, title, MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2);
                        }
                    }
                    else f.BackgroundImage = null;
                }
            }
        }

        public static int BackgroundImageLayout
        {
            get
            {
                return backgroundImageLayout;
            }
            set
            {
                backgroundImageLayout = value;
                foreach (Form_main f in Application.OpenForms.OfType<Form_main>())
                {
                    if (value == 0) f.BackgroundImageLayout = ImageLayout.None;
                    else if (value == 1) f.BackgroundImageLayout = ImageLayout.Tile;
                    else if (value == 2) f.BackgroundImageLayout = ImageLayout.Center;
                    else if (value == 3) f.BackgroundImageLayout = ImageLayout.Stretch;
                    else if (value == 4) f.BackgroundImageLayout = ImageLayout.Zoom;
                }
            }
        }

        public static bool ProgramGroupsApplyColor
        {
            get
            {
                return programGroupsApplyColor;
            }
            set
            {
                programGroupsApplyColor = value;
            }
        }

        public static bool ProgramGroupsTransparent
        {
            get
            {
                return programGroupsTransparent;
            }
            set
            {
                programGroupsTransparent = value;
                ////public bool ProgramsLoadCompleted = false;
                //foreach (Form_prog f in Application.OpenForms.OfType<Form_prog>())
                //{
                //    if (value) f.BackColor = Color.Transparent;
                //    if (value) f.BackColor = Color.Transparent;
                //    foreach (NativeListView n in f.Controls.OfType<NativeListView>())
                //    {
                //        if (n.programGroupsLoaded)
                //        {
                //            n.TransparentBackground = value;
                            
                //        }
                //    }
                //}
            }
        }

        public static int StartupLayout
        {
            get
            {
                return startupLayout;
            }
            set
            {
                startupLayout = value;
            }
        }

        public static bool Language
        {
            get
            {
                return language;
            }
            set
            {
                language = value;
                if (value)
                    System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("de-DE");
                else
                    System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en");
            }
        }

        public static bool DesktopMode
        {
            get
            {
                return desktopMode;
            }
            set
            {
                desktopMode = value;
            }
        }

        public static int DesktopScreen
        {
            get
            {
                return desktopScreen;
            }
            set
            {
                desktopScreen = value;

            }
        }

        public static bool TaskbarIcon
        {
            get
            {
                return taskbarIcon;
            }
            set
            {
                taskbarIcon = value;
            }
        }

        public static Color TaskbarIconColor
        {
            get
            {
                return taskbarIconColor;
            }
            set
            {
                taskbarIconColor = value;
                if (UV7_Program_Manager.TaskbarIcon.ContextMenu != null)
                {
                    UV7_Program_Manager.TaskbarIcon.ContextMenu.Items[0].ForeColor = value;
                }
            }
        }

        public static bool TaskbarIconShutdown
        {
            get
            {
                return taskbarIconShutdown;
            }
            set
            {
                taskbarIconShutdown = value;
            }
        }

        public static bool NewRenderMode
        {
            get
            {
                return newRenderMode;
            }
            set
            {
                newRenderMode = value;
            }
        }
    }

    public static class CurrentConfig
    {
        public static bool PreferencesFormOpened = false;
        //public static bool AboutFormOpened = false;
        public static bool ErrorsFormOpened = false;
        public static List<string> ErrorList = new List<string>();
    }
}
