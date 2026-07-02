using System;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Runtime.InteropServices;
using UV7_Program_Manager.CustomControls;
using UV7_Program_Manager.ShellTools;

namespace UV7_Program_Manager.Config
{
    public partial class Form_preferences : Form
    {
        public Form_preferences()
        {
            InitializeComponent();
            AddOnChangeHandlerToInputControls(this);
            openFileDialog.InitialDirectory = Application.StartupPath;
        }

        internal NativeToolStripRenderer ntsr1 = new NativeToolStripRenderer(new ToolbarTheme());
        internal NativeToolStripRenderer ntsr2 = new NativeToolStripRenderer(new ToolbarTheme());
        internal NativeToolStripRenderer ntsr3 = new NativeToolStripRenderer(new ToolbarTheme());
        internal NativeToolStripRenderer ntsr4 = new NativeToolStripRenderer(new ToolbarTheme());
        internal NativeToolStripRenderer ntsr5 = new NativeToolStripRenderer(new ToolbarTheme());

        #region UI_Form

        private void Form_preferences_Load(object sender, EventArgs e)
        {
            CurrentConfig.PreferencesFormOpened = true;
            button_apply.Enabled = false;
            LoadConfig();

            ntsr1.Theme = ToolbarTheme.Toolbar;
            ntsr2.Theme = ToolbarTheme.BrowserTabBar;
            ntsr3.Theme = ToolbarTheme.MediaToolbar;
            ntsr4.Theme = ToolbarTheme.CommunicationsToolbar;
            ntsr5.Theme = ToolbarTheme.Toolbar;

            menuStrip_pre1.Renderer = ntsr1;
            menuStrip_pre2.Renderer = ntsr2;
            menuStrip_pre3.Renderer = ntsr3;
            menuStrip_pre4.Renderer = ntsr4;
            menuStrip_pre5.Renderer = ntsr5;

            pictureBox1.Image = SystemIcons.Exclamation.ToBitmap();
            pictureBox2.Image = SystemIcons.Exclamation.ToBitmap();
            pictureBox3.Image = SystemIcons.Exclamation.ToBitmap();
            pictureBox4.Image = SystemIcons.Exclamation.ToBitmap();
        }

        private void Form_preferences_FormClosing(object sender, FormClosingEventArgs e)
        {
            CurrentConfig.PreferencesFormOpened = false;
        }

        public void InputControls_OnChange(object sender, EventArgs e)
        {
            button_apply.Enabled = true;
        }

        public void AddOnChangeHandlerToInputControls(Control ctrl)
        {
            foreach (Control subctrl in ctrl.Controls)
            {
                if (subctrl is Label)
                    ((Label)subctrl).TextChanged +=
                        new EventHandler(InputControls_OnChange);
                else if (subctrl is CheckBox)
                    ((CheckBox)subctrl).CheckedChanged +=
                        new EventHandler(InputControls_OnChange);
                else if (subctrl is RadioButton)
                    ((RadioButton)subctrl).CheckedChanged +=
                        new EventHandler(InputControls_OnChange);
                else if (subctrl is ComboBox)
                    ((ComboBox)subctrl).SelectedIndexChanged +=
                        new EventHandler(InputControls_OnChange);
                else
                {
                    if (subctrl.Controls.Count > 0)
                        this.AddOnChangeHandlerToInputControls(subctrl);
                }
            }
            button_changeColor.Click += new EventHandler(InputControls_OnChange);
            button_changeImage.Click += new EventHandler(InputControls_OnChange);
            button_changeTColor.Click += new EventHandler(InputControls_OnChange);
        }

        #endregion UI_Form

        #region Config

        private void LoadConfig()
        {
            // Page 1 : Application
            void SetAllControlsFont(Control.ControlCollection ctrls)
            {
                foreach (Control ctrl in ctrls)
                {
                    if (ctrl.Controls != null)
                        SetAllControlsFont(ctrl.Controls);

                    ctrl.Font = new Font(ApplicationConfig.Font, ctrl.Font.Style);
                }
            }
            SetAllControlsFont(this.Controls);

            if (ApplicationConfig.VisualStyle == 0) radioButton_vs0.Checked = true;
            else if (ApplicationConfig.VisualStyle == 1) radioButton_vs1.Checked = true;
            else if (ApplicationConfig.VisualStyle == 2) radioButton_vs2.Checked = true;
            else if (ApplicationConfig.VisualStyle == 3) radioButton_vs3.Checked = true;

            checkBox_desktopCompositing.Checked = ApplicationConfig.DesktopCompositing;

            if (ApplicationConfig.FontSelection == 0) radioButton_font0.Checked = true;
            else if (ApplicationConfig.FontSelection == 1) radioButton_font1.Checked = true;
            else if (ApplicationConfig.FontSelection == 2) radioButton_font2.Checked = true;
            else if (ApplicationConfig.FontSelection == 3) radioButton_font3.Checked = true;

            if (ApplicationConfig.MenuBarStyle == 0) radioButton_menu0.Checked = true;
            else if (ApplicationConfig.MenuBarStyle == 1) radioButton_menu1.Checked = true;
            else if (ApplicationConfig.MenuBarStyle == 2) radioButton_menu2.Checked = true;
            else if (ApplicationConfig.MenuBarStyle == 3) radioButton_menu3.Checked = true;

            checkBox_topMost.Checked = ApplicationConfig.TopMost;


            // Page 2 : Main Workspace
            if (ApplicationConfig.NewRenderMode)
            {
                radioButton_listView.Checked = true;
                panel_listViewOptions.Visible = true;
            }
            else
            {
                radioButton_mdi.Checked = true;
                panel_mdiOptions.Visible = true;
            }   
            
            panel_color.BackColor = ApplicationConfig.BackgroundColor;
            colorDialog.Color = ApplicationConfig.BackgroundColor;
            checkBox_showBackgroundImage.Checked = ApplicationConfig.BackgroundImage;
            comboBox_imageLayout.SelectedIndex = ApplicationConfig.BackgroundImageLayout;
            comboBox_startupLayout.SelectedIndex = ApplicationConfig.StartupLayout;
            checkBox_whiteFont.Checked = ApplicationConfig.WhiteFont;
            checkBox_programGroupsApplyColor.Checked = ApplicationConfig.ProgramGroupsApplyColor;
            checkBox_programGroupsTransparent.Checked = ApplicationConfig.ProgramGroupsTransparent;

            if (ApplicationConfig.Language) radioButton_langDE.Checked = true;
            else radioButton_langDefault.Checked = true;

            // Page 5 : Taskbar Icon
            checkBox_taskbarIcon.Checked = ApplicationConfig.TaskbarIcon;
            checkBox_taskbarIconShutdown.Checked = ApplicationConfig.TaskbarIconShutdown;
            panel_tColor.BackColor = ApplicationConfig.TaskbarIconColor;
            colorDialog_taskbarIcon.Color = ApplicationConfig.TaskbarIconColor;

            button_apply.Enabled = false;
        }

        private void SaveConfig()
        {
            // Page 1 : Application
            if (radioButton_vs0.Checked) ApplicationConfig.VisualStyle = 0;
            else if (radioButton_vs1.Checked) ApplicationConfig.VisualStyle = 1;
            else if (radioButton_vs2.Checked) ApplicationConfig.VisualStyle = 2;
            else if (radioButton_vs3.Checked) ApplicationConfig.VisualStyle = 3;

            ApplicationConfig.DesktopCompositing = checkBox_desktopCompositing.Checked;

            if (radioButton_font0.Checked) ApplicationConfig.FontSelection = 0;
            else if (radioButton_font1.Checked) ApplicationConfig.FontSelection = 1;
            else if (radioButton_font2.Checked) ApplicationConfig.FontSelection = 2;
            else if (radioButton_font3.Checked) ApplicationConfig.FontSelection = 3;

            if (radioButton_menu0.Checked) ApplicationConfig.MenuBarStyle = 0;
            else if (radioButton_menu1.Checked) ApplicationConfig.MenuBarStyle = 1;
            else if (radioButton_menu2.Checked) ApplicationConfig.MenuBarStyle = 2;
            else if (radioButton_menu3.Checked) ApplicationConfig.MenuBarStyle = 3;


            // Page 2 : Main Workspace
            ApplicationConfig.NewRenderMode = radioButton_listView.Checked;
            
            ApplicationConfig.BackgroundColor = panel_color.BackColor;
            ApplicationConfig.BackgroundImage = checkBox_showBackgroundImage.Checked;
            ApplicationConfig.BackgroundImageLayout = comboBox_imageLayout.SelectedIndex;
            ApplicationConfig.StartupLayout = comboBox_startupLayout.SelectedIndex;
            ApplicationConfig.WhiteFont = checkBox_whiteFont.Checked;
            ApplicationConfig.ProgramGroupsApplyColor = checkBox_programGroupsApplyColor.Checked;
            ApplicationConfig.ProgramGroupsTransparent = checkBox_programGroupsTransparent.Checked;
            ApplicationConfig.DrawProgBackground(ApplicationConfig.BackgroundColor);

            ApplicationConfig.Language = radioButton_langDE.Checked;
            

            ApplicationConfig.TopMost = checkBox_topMost.Checked;

            // Page 5 : Taskbar Icon
            ApplicationConfig.TaskbarIcon = checkBox_taskbarIcon.Checked;
            ApplicationConfig.TaskbarIconShutdown = checkBox_taskbarIconShutdown.Checked;
            ApplicationConfig.TaskbarIconColor = panel_tColor.BackColor;
        }

        private void CheckRenderModeChangesAndSave()
        {
            // If the render mode that is selected differs from the mode that is currently active,
            // require a restart
            if (radioButton_mdi.Checked == ApplicationConfig.NewRenderMode) // true: Modes differ --> Restart needed
            {
                DialogResult result = MessageBox.Show(Dialog.Config_ImmediateRestartRequired, Dialog.Config_ImmediateRestartRequiredTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                if (result == DialogResult.Yes)
                {
                    SaveConfig();
                    ApplicationConfig.SaveConfigFile();
                    Application.Restart();
                }
                else
                {
                    LoadConfig();
                }
            }
            else
            {
                SaveConfig();
                ApplicationConfig.SaveConfigFile();
            }
        }

        #endregion Config

        #region MainButtons

        private void Okay(object sender, EventArgs e)
        {
            CheckRenderModeChangesAndSave();
            this.Close();
        }

        private void Cancel(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Apply(object sender, EventArgs e)
        {
            CheckRenderModeChangesAndSave();
            button_apply.Enabled = false;
        }

        private void Reset(object sender, EventArgs e)
        {
            string msg = Dialog.Preferences_ResetConfirm;
            string title = Dialog.Preferences_ResetConfirmTitle;
            
            DialogResult result = MessageBox.Show(msg, title, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
            if (result == DialogResult.Yes)
            {
                ApplicationConfig.LoadDefaults();
                Application.Restart();
            }
        }

        private void Restart(object sender, EventArgs e)
        {
            Application.Restart();
        }

        #endregion MainButtons

        #region UI_Controls

        
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            button_changeImage.Enabled = checkBox_showBackgroundImage.Checked;
            comboBox_imageLayout.Enabled = checkBox_showBackgroundImage.Checked;
        }

        private void button_changeColor_Click(object sender, EventArgs e)
        {
            int ColorToInt(Color color)
            {
                return (color.R) | (color.G << 8) | (color.B << 16);
            }
            int[] customColors =
            {
                ColorToInt(SystemColors.AppWorkspace),
                ColorToInt(Color.FromArgb(13686764)),
                ColorToInt(SystemColors.ControlDarkDark),
                ColorToInt(SystemColors.Control),
                ColorToInt(SystemColors.Info),
                ColorToInt(SystemColors.Window)
            };
            colorDialog.CustomColors = customColors;
            DialogResult result = colorDialog.ShowDialog();
            if (result == DialogResult.OK) panel_color.BackColor = colorDialog.Color;
        }


        private void button_changeImage_Click(object sender, EventArgs e)
        {
            DialogResult result = openFileDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                FileInfo info = new FileInfo(openFileDialog.FileName);
                try
                {
                    File.Copy(openFileDialog.FileName, Application.StartupPath + @"\background", true);
                    ApplicationConfig.BackgroundImage = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, Dialog.Preferences_CopyErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void checkBox_showBackgroundImage_CheckedChanged(object sender, EventArgs e)
        {
            button_changeImage.Enabled = checkBox_showBackgroundImage.Checked;
            comboBox_imageLayout.Enabled = checkBox_showBackgroundImage.Checked;
        }

        #endregion UI_Controls

        private void button_addToUserStartup_Click(object sender, EventArgs e)
        {
            ShellLink link = new ShellLink();
            link.Target = Application.ExecutablePath;
            link.WorkingDirectory = Application.StartupPath;
            link.IconPath = Application.ExecutablePath;
            link.Description = "UV7 Program Manager";
            link.Save(Environment.GetFolderPath(Environment.SpecialFolder.Startup) + @"\UV7 Program Manager.lnk");
        }

        private void button_openUserStartup_Click(object sender, EventArgs e)
        {
            Process.Start("explorer", Environment.GetFolderPath(Environment.SpecialFolder.Startup));
        }

        [DllImport("shell32.dll")]
        static extern bool SHGetSpecialFolderPath(IntPtr hwndOwner,
            [Out] StringBuilder lpszPath, int nFolder, bool fCreate);
        const int CSIDL_COMMON_STARTMENU = 0x16;  // All Users\Start Menu
        const int CSIDL_COMMON_STARTUP = 0x18;  // All Users\Start Menu\Programs\Startup

        private void button_openGlobalStartup_Click(object sender, EventArgs e)
        {
            StringBuilder path = new StringBuilder(260);
            SHGetSpecialFolderPath(IntPtr.Zero, path, CSIDL_COMMON_STARTUP, false);
            string s = path.ToString();
            Process.Start("explorer", s);
        }

        private void button_pre1_Click(object sender, EventArgs e)
        {
            ApplicationConfig.BackgroundColor = SystemColors.AppWorkspace;
            ApplicationConfig.BackgroundImage = false;
            ApplicationConfig.MenuBarStyle = 0;
            ApplicationConfig.VisualStyle = 0;

            ApplicationConfig.SaveConfigFile();
            LoadConfig();
            button_apply.Enabled = false;
        }

        private void button_pre2_Click(object sender, EventArgs e)
        {
            ApplicationConfig.BackgroundColor = Color.FromArgb(198, 231, 255);
            ApplicationConfig.BackgroundImage = true;
            ApplicationConfig.BackgroundImageLayout = 3;
            ApplicationConfig.MenuBarStyle = 1;
            ApplicationConfig.VisualStyle = 0;
            try
            {
                File.Copy(Application.StartupPath + @"\back_aero-light.png", Application.StartupPath + @"\background", true);
                ApplicationConfig.BackgroundImage = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Dialog.Preferences_CopyErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            ApplicationConfig.SaveConfigFile();
            LoadConfig();
            button_apply.Enabled = false;
        }

        private void button_pre3_Click(object sender, EventArgs e)
        {
            ApplicationConfig.BackgroundColor = Color.FromArgb(75, 75, 75);
            ApplicationConfig.BackgroundImage = false;
            ApplicationConfig.MenuBarStyle = 2;
            ApplicationConfig.VisualStyle = 0;

            ApplicationConfig.SaveConfigFile();
            LoadConfig();
            button_apply.Enabled = false;
        }

        private void button_pre4_Click(object sender, EventArgs e)
        {
            ApplicationConfig.BackgroundColor = Color.FromArgb(0, 65, 130);
            ApplicationConfig.BackgroundImage = false;
            ApplicationConfig.MenuBarStyle = 3;
            ApplicationConfig.VisualStyle = 0;
            try
            {
                File.Copy(Application.StartupPath + @"\back_aero-dark.png", Application.StartupPath + @"\background", true);
                ApplicationConfig.BackgroundImage = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Dialog.Preferences_CopyErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            ApplicationConfig.SaveConfigFile();
            LoadConfig();
            button_apply.Enabled = false;
        }

        private void button_pre5_Click(object sender, EventArgs e)
        {
            ApplicationConfig.BackgroundColor = SystemColors.Info;
            ApplicationConfig.BackgroundImage = false;
            ApplicationConfig.MenuBarStyle = 0;
            ApplicationConfig.VisualStyle = 3;

            ApplicationConfig.SaveConfigFile();
            LoadConfig();
            button_apply.Enabled = false;
        }

        private void button_tColor_Click(object sender, EventArgs e)
        {
            DialogResult result = colorDialog_taskbarIcon.ShowDialog();
            if (result == DialogResult.OK) panel_tColor.BackColor = colorDialog_taskbarIcon.Color;
        }

        private void ChangeRenderMode(object sender, EventArgs e)
        {
            panel_mdiOptions.Visible = radioButton_mdi.Checked;
            panel_listViewOptions.Visible = radioButton_listView.Checked;
        }
    }
}
