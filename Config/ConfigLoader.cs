using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using UV7_Program_Manager.CustomControls;

namespace UV7_Program_Manager.Config
{
    public static partial class ApplicationConfig
    {
        public static void DrawProgBackground(Color value)
        {
            if (ProgramGroupsApplyColor)
            {
                foreach (Form_prog f in Application.OpenForms.OfType<Form_prog>())
                {
                    if (ProgramGroupsTransparent)
                    {
                        f.BackColor = Color.Transparent;
                        foreach (NativeListView n in f.Controls.OfType<NativeListView>())
                        {
                            n.BackColor = value;
                            n.TransparentBackground = true;
                        }
                    }
                    else
                    {
                        f.BackColor = value;
                        foreach (NativeListView n in f.Controls.OfType<NativeListView>())
                        {
                            n.BackColor = value;
                            n.TransparentBackground = false;
                        }
                    }
                }
            }
            else if (!ProgramGroupsApplyColor)
            {
                foreach (Form_prog f in Application.OpenForms.OfType<Form_prog>())
                {
                    if (ProgramGroupsTransparent)
                    {
                        f.BackColor = Color.Transparent;
                        foreach (NativeListView n in f.Controls.OfType<NativeListView>())
                        {
                            n.BackColor = SystemColors.Control;
                            n.TransparentBackground = true;
                        }
                    }
                    else
                    {
                        f.BackColor = SystemColors.Control;
                        foreach (NativeListView n in f.Controls.OfType<NativeListView>())
                        {
                            n.BackColor = SystemColors.Control;
                            n.TransparentBackground = false;
                        }
                    }
                }
            }
        }

        public static void LoadConfigFile()
        {
            // Load default values
            LoadDefaults();

            // Load config if existent
            if (File.Exists(Application.StartupPath + @"\" + Assembly.GetExecutingAssembly().GetName().Name + @".ini"))
            {
                // Define the entries that need to be loaded
                string[] E = { "VisualStyle", "DesktopCompositing", "FontSelection", "WhiteFont", "MenuBarStyle", "TopMost", "BackgroundColor", 
                    "BackgroundImage", "BackgroundImageLayout", "ProgramGroupsApplyColor", "ProgramGroupsTransparent", "StartupLayout",
                    "DesktopMode", "DesktopScreen", "TaskbarIcon", "TaskbarIconColor", "TaskbarIconShutdown", "NewRenderMode", "Language" };
                // for each entry, set the current entry to the current entry
                string CE = E[0];
                var ConfigFile = new IniFile();

                List<KeyValuePair<string, Exception>> errors = new List<KeyValuePair<string, Exception>>();

                // Try to read all entries
                try
                {
                    VisualStyle = Int32.Parse(ConfigFile.Read("VisualStyle"));
                }
                catch (Exception ex)
                {
                    // If an error occurs, add the entry that could not be loaded and corresponding exception to the errors list
                    errors.Add(new KeyValuePair<string, Exception>(CE, ex));
                }

                try
                {
                    CE = E[1];
                    DesktopCompositing = bool.Parse(ConfigFile.Read("DesktopCompositing"));
                }
                catch (Exception ex)
                {
                    errors.Add(new KeyValuePair<string, Exception>(CE, ex));
                }

                try
                {
                    CE = E[2];
                    FontSelection = Int32.Parse(ConfigFile.Read("FontSelection"));
                }
                catch (Exception ex)
                {
                    errors.Add(new KeyValuePair<string, Exception>(CE, ex));
                }

                try
                {
                    CE = E[3];
                    WhiteFont = bool.Parse(ConfigFile.Read("WhiteFont"));
                }
                catch (Exception ex)
                {
                    errors.Add(new KeyValuePair<string, Exception>(CE, ex));
                }

                try
                {
                    CE = E[4];
                    MenuBarStyle = Int32.Parse(ConfigFile.Read("MenuBarStyle"));
                }
                catch (Exception ex)
                {
                    errors.Add(new KeyValuePair<string, Exception>(CE, ex));
                }

                try
                {
                    CE = E[5];
                    TopMost = bool.Parse(ConfigFile.Read("TopMost"));
                }
                catch (Exception ex)
                {
                    errors.Add(new KeyValuePair<string, Exception>(CE, ex));
                }

                try
                {
                    CE = E[6];
                    BackgroundColor = Color.FromArgb(Int32.Parse(ConfigFile.Read("BackgroundColor")));
                }
                catch (Exception ex)
                {
                    errors.Add(new KeyValuePair<string, Exception>(CE, ex));
                }

                try
                {
                    CE = E[7];
                    BackgroundImage = bool.Parse(ConfigFile.Read("BackgroundImage"));
                }
                catch (Exception ex)
                {
                    errors.Add(new KeyValuePair<string, Exception>(CE, ex));
                }

                try
                {
                    CE = E[8];
                    BackgroundImageLayout = Int32.Parse(ConfigFile.Read("BackgroundImageLayout"));
                }
                catch (Exception ex)
                {
                    errors.Add(new KeyValuePair<string, Exception>(CE, ex));
                }

                try
                {
                    CE = E[9];
                    ProgramGroupsApplyColor = bool.Parse(ConfigFile.Read("ProgramGroupsApplyColor"));
                }
                catch (Exception ex)
                {
                    errors.Add(new KeyValuePair<string, Exception>(CE, ex));
                }

                try
                {
                    CE = E[10];
                    ProgramGroupsTransparent = bool.Parse(ConfigFile.Read("ProgramGroupsTransparent"));
                }
                catch (Exception ex)
                {
                    errors.Add(new KeyValuePair<string, Exception>(CE, ex));
                }

                try
                {
                    CE = E[11];
                    StartupLayout = Int32.Parse(ConfigFile.Read("StartupLayout"));
                }
                catch (Exception ex)
                {
                    errors.Add(new KeyValuePair<string, Exception>(CE, ex));
                }
                
                try
                {
                    CE = E[12];
                    DesktopMode = bool.Parse(ConfigFile.Read("DesktopMode"));
                }
                catch (Exception ex)
                {
                    errors.Add(new KeyValuePair<string, Exception>(CE, ex));
                }

                try
                {
                    CE = E[13];
                    DesktopScreen = Int32.Parse(ConfigFile.Read("DesktopScreen"));
                }
                catch (Exception ex)
                {
                    errors.Add(new KeyValuePair<string, Exception>(CE, ex));
                }
                
                try
                {
                    CE = E[14];
                    TaskbarIcon = bool.Parse(ConfigFile.Read("TaskbarIcon"));
                }
                catch (Exception ex)
                {
                    errors.Add(new KeyValuePair<string, Exception>(CE, ex));
                }

                try
                {
                    CE = E[15];
                    TaskbarIconColor = Color.FromArgb(Int32.Parse(ConfigFile.Read("TaskbarIconColor")));
                }
                catch (Exception ex)
                {
                    errors.Add(new KeyValuePair<string, Exception>(CE, ex));
                }
                
                try
                {
                    CE = E[16];
                    TaskbarIconShutdown = bool.Parse(ConfigFile.Read("TaskbarIconShutdown"));
                }
                catch (Exception ex)
                {
                    errors.Add(new KeyValuePair<string, Exception>(CE, ex));
                }

                try
                {
                    CE = E[17];
                    NewRenderMode = bool.Parse(ConfigFile.Read("NewRenderMode"));
                }
                catch (Exception ex)
                {
                    errors.Add(new KeyValuePair<string, Exception>(CE, ex));
                }

                try
                {
                    CE = E[18];
                    Language = bool.Parse(ConfigFile.Read("Language"));
                }
                catch (Exception ex)
                {
                    errors.Add(new KeyValuePair<string, Exception>(CE, ex));
                }

                // Check if errors occured
                if (errors.Count > 0)
                {
                    // If so, inform the user...
                    string msg = Dialog.Config_LoadError;
                    string title = Dialog.Config_LoadErrorTitle;

                    string errors_msg = $"{errors[0].Key}{Environment.NewLine}{errors[0].Value.Message}";
                    for (int i = 1; i < errors.Count; i++)
                    {
                        errors_msg += $"{Environment.NewLine}{Environment.NewLine}" +
                            $"{errors[i].Key}{Environment.NewLine}{errors[i].Value.Message}";
                    }

                    DialogResult result = MessageBox.Show(String.Format(msg, errors.Count, errors_msg), title, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);

                    // ... and ask whether ...
                    if (result == DialogResult.Yes)
                    {
                        // ... to replace faulty entries with defaults
                        // in this case nothing needs to be done here as they
                        // hadn't been overwritten in the first place
                    }
                    else if (result == DialogResult.No)
                    {
                        // ... to overwrite all values with defaults again
                        LoadDefaults();
                    }
                    else
                    {
                        // ... or to exit the application
                        Environment.Exit(0);
                    }
                }
            }
        }

        public static void LoadDefaults()
        {
            VisualStyle = 0;
            DesktopCompositing = false;

            //Font = new Font("Tahoma", 8);
            FontSelection = 0;
            WhiteFont = false;

            MenuBarStyle = 0;

            TopMost = false;

            BackgroundColor = SystemColors.AppWorkspace;
            BackgroundImage = false;
            BackgroundImageLayout = 0;

            ProgramGroupsApplyColor = false;
            ProgramGroupsTransparent = false;

            StartupLayout = 0;

            DesktopMode = false;
            DesktopScreen = 0;

            TaskbarIcon = true;
            TaskbarIconColor = Color.Red;
            TaskbarIconShutdown = true;

            NewRenderMode = false;

            Language = true;
        }

        public static void SaveConfigFile()
        {
            bool finished = false;
            string[] E = { "VisualStyle", "DesktopCompositing", "FontSelection", "WhiteFont", "MenuBarStyle", "TopMost", "BackgroundColor", 
                "BackgroundImage", "BackgroundImageLayout", "ProgramGroupsApplyColor", "ProgramGroupsTransparent", "StartupLayout", 
                "DesktopMode", "DesktopScreen", "TaskbarIcon", "TaskbarIconColor", "TaskbarIconShutdown", "NewRenderMode", "Language" };
            string CE = E[0];
            void Save()
            {
                var ConfigFile = new IniFile(Assembly.GetExecutingAssembly().GetName().Name + @".ini");
                ConfigFile.Write("VisualStyle", VisualStyle.ToString());
                CE = E[1];
                ConfigFile.Write("DesktopCompositing", DesktopCompositing.ToString());
                CE = E[2];
                ConfigFile.Write("FontSelection", FontSelection.ToString());
                CE = E[3];
                ConfigFile.Write("WhiteFont", WhiteFont.ToString());
                CE = E[4];
                ConfigFile.Write("MenuBarStyle", MenuBarStyle.ToString());
                CE = E[5];
                ConfigFile.Write("TopMost", TopMost.ToString());
                CE = E[6];
                ConfigFile.Write("BackgroundColor", BackgroundColor.ToArgb().ToString());
                CE = E[7];
                ConfigFile.Write("BackgroundImage", BackgroundImage.ToString());
                CE = E[8];
                ConfigFile.Write("BackgroundImageLayout", BackgroundImageLayout.ToString());
                CE = E[9];
                ConfigFile.Write("ProgramGroupsApplyColor", ProgramGroupsApplyColor.ToString());
                CE = E[10];
                ConfigFile.Write("ProgramGroupsTransparent", ProgramGroupsTransparent.ToString());
                CE = E[11];
                ConfigFile.Write("StartupLayout", StartupLayout.ToString());
                CE = E[12];
                ConfigFile.Write("DesktopMode", DesktopMode.ToString());
                CE = E[13];
                ConfigFile.Write("DesktopScreen", DesktopScreen.ToString());
                CE = E[14];
                ConfigFile.Write("TaskbarIcon", TaskbarIcon.ToString());
                CE = E[15];
                ConfigFile.Write("TaskbarIconColor", TaskbarIconColor.ToArgb().ToString());
                CE = E[16];
                ConfigFile.Write("TaskbarIconShutdown", TaskbarIconShutdown.ToString());
                CE = E[17];
                ConfigFile.Write("NewRenderMode", NewRenderMode.ToString());
                CE = E[18];
                ConfigFile.Write("Language", Language.ToString());
                finished = true;
            }
            while (!finished)
            {
                try
                {
                    Save();
                }
                catch (Exception ex)
                {
                    DialogResult result = MessageBox.Show(String.Format(Dialog.Config_SaveError, CE, ex.Message), Dialog.Config_SaveErrorTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                    if (result == DialogResult.No) finished = true;
                }
            }
        }
    }
}
