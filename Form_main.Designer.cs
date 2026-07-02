
namespace UV7_Program_Manager
{
    partial class Form_main
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_main));
            this.menuStrip_main = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.startProgramToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reloadEntriesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.runToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitWindowstoolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.preferencesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.openProgramsDirectoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openUsersStartMenuDirectoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openSystemStartMenuDirectoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.resetMainWindowSizeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.windowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.refreshLayoutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.cascadeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tileVerticalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tileHorizontalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
            this.arrangeIconsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.restoreAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.minimizeAlltoolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.restoreOriginalSizeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator_secret = new System.Windows.Forms.ToolStripSeparator();
            this.documentationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_desktopMode = new System.Windows.Forms.ToolStripMenuItem();
            this.closeAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel_status = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel_groups = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel_programs = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel_errors = new System.Windows.Forms.ToolStripStatusLabel();
            this.timer_desktop = new System.Windows.Forms.Timer(this.components);
            this.contextMenu_changeScreen = new System.Windows.Forms.ContextMenu();
            this.folv_prog = new BrightIdeasSoftware.FastObjectListView();
            this.olvc_name = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvc_group = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvc_description = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.gradientPanel_desktop = new UV7_Program_Manager.CustomControls.GradientPanel();
            this.dropDownButton_changeScreen = new UV7_Program_Manager.CustomControls.DropDownButton();
            this.dropDownButton_powerOptions = new UV7_Program_Manager.CustomControls.DropDownButton();
            this.label_machineName = new System.Windows.Forms.Label();
            this.label_userName = new System.Windows.Forms.Label();
            this.label_time = new System.Windows.Forms.Label();
            this.menuStrip_main.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.folv_prog)).BeginInit();
            this.gradientPanel_desktop.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip_main
            // 
            resources.ApplyResources(this.menuStrip_main, "menuStrip_main");
            this.menuStrip_main.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.optionsToolStripMenuItem,
            this.windowToolStripMenuItem,
            this.helpToolStripMenuItem,
            this.toolStripMenuItem_desktopMode});
            this.menuStrip_main.MdiWindowListItem = this.windowToolStripMenuItem;
            this.menuStrip_main.Name = "menuStrip_main";
            // 
            // fileToolStripMenuItem
            // 
            resources.ApplyResources(this.fileToolStripMenuItem, "fileToolStripMenuItem");
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.startProgramToolStripMenuItem,
            this.reloadEntriesToolStripMenuItem,
            this.toolStripSeparator1,
            this.runToolStripMenuItem,
            this.toolStripSeparator3,
            this.exitToolStripMenuItem,
            this.exitWindowstoolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Padding = new System.Windows.Forms.Padding(2, 0, 1, 0);
            // 
            // startProgramToolStripMenuItem
            // 
            resources.ApplyResources(this.startProgramToolStripMenuItem, "startProgramToolStripMenuItem");
            this.startProgramToolStripMenuItem.Name = "startProgramToolStripMenuItem";
            this.startProgramToolStripMenuItem.Click += new System.EventHandler(this.StartProgram);
            // 
            // reloadEntriesToolStripMenuItem
            // 
            resources.ApplyResources(this.reloadEntriesToolStripMenuItem, "reloadEntriesToolStripMenuItem");
            this.reloadEntriesToolStripMenuItem.Name = "reloadEntriesToolStripMenuItem";
            this.reloadEntriesToolStripMenuItem.Click += new System.EventHandler(this.ReloadEntries);
            // 
            // toolStripSeparator1
            // 
            resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            // 
            // runToolStripMenuItem
            // 
            resources.ApplyResources(this.runToolStripMenuItem, "runToolStripMenuItem");
            this.runToolStripMenuItem.Name = "runToolStripMenuItem";
            this.runToolStripMenuItem.Click += new System.EventHandler(this.Run);
            // 
            // toolStripSeparator3
            // 
            resources.ApplyResources(this.toolStripSeparator3, "toolStripSeparator3");
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            // 
            // exitToolStripMenuItem
            // 
            resources.ApplyResources(this.exitToolStripMenuItem, "exitToolStripMenuItem");
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.Exit);
            // 
            // exitWindowstoolStripMenuItem
            // 
            resources.ApplyResources(this.exitWindowstoolStripMenuItem, "exitWindowstoolStripMenuItem");
            this.exitWindowstoolStripMenuItem.Name = "exitWindowstoolStripMenuItem";
            this.exitWindowstoolStripMenuItem.Click += new System.EventHandler(this.ExitWindows);
            // 
            // optionsToolStripMenuItem
            // 
            resources.ApplyResources(this.optionsToolStripMenuItem, "optionsToolStripMenuItem");
            this.optionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.preferencesToolStripMenuItem,
            this.toolStripSeparator6,
            this.openProgramsDirectoryToolStripMenuItem,
            this.openUsersStartMenuDirectoryToolStripMenuItem,
            this.openSystemStartMenuDirectoryToolStripMenuItem,
            this.toolStripSeparator7,
            this.resetMainWindowSizeToolStripMenuItem});
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Padding = new System.Windows.Forms.Padding(2, 0, 1, 0);
            // 
            // preferencesToolStripMenuItem
            // 
            resources.ApplyResources(this.preferencesToolStripMenuItem, "preferencesToolStripMenuItem");
            this.preferencesToolStripMenuItem.Name = "preferencesToolStripMenuItem";
            this.preferencesToolStripMenuItem.Click += new System.EventHandler(this.Preferences);
            // 
            // toolStripSeparator6
            // 
            resources.ApplyResources(this.toolStripSeparator6, "toolStripSeparator6");
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            // 
            // openProgramsDirectoryToolStripMenuItem
            // 
            resources.ApplyResources(this.openProgramsDirectoryToolStripMenuItem, "openProgramsDirectoryToolStripMenuItem");
            this.openProgramsDirectoryToolStripMenuItem.Name = "openProgramsDirectoryToolStripMenuItem";
            this.openProgramsDirectoryToolStripMenuItem.Click += new System.EventHandler(this.OpenProgramsDirectory);
            // 
            // openUsersStartMenuDirectoryToolStripMenuItem
            // 
            resources.ApplyResources(this.openUsersStartMenuDirectoryToolStripMenuItem, "openUsersStartMenuDirectoryToolStripMenuItem");
            this.openUsersStartMenuDirectoryToolStripMenuItem.Name = "openUsersStartMenuDirectoryToolStripMenuItem";
            this.openUsersStartMenuDirectoryToolStripMenuItem.Click += new System.EventHandler(this.OpenUsersStartMenuDirectory);
            // 
            // openSystemStartMenuDirectoryToolStripMenuItem
            // 
            resources.ApplyResources(this.openSystemStartMenuDirectoryToolStripMenuItem, "openSystemStartMenuDirectoryToolStripMenuItem");
            this.openSystemStartMenuDirectoryToolStripMenuItem.Name = "openSystemStartMenuDirectoryToolStripMenuItem";
            this.openSystemStartMenuDirectoryToolStripMenuItem.Click += new System.EventHandler(this.OpenSystemStartMenuDirectory);
            // 
            // toolStripSeparator7
            // 
            resources.ApplyResources(this.toolStripSeparator7, "toolStripSeparator7");
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            // 
            // resetMainWindowSizeToolStripMenuItem
            // 
            resources.ApplyResources(this.resetMainWindowSizeToolStripMenuItem, "resetMainWindowSizeToolStripMenuItem");
            this.resetMainWindowSizeToolStripMenuItem.Name = "resetMainWindowSizeToolStripMenuItem";
            this.resetMainWindowSizeToolStripMenuItem.Click += new System.EventHandler(this.ResetMainWindowSize);
            // 
            // windowToolStripMenuItem
            // 
            resources.ApplyResources(this.windowToolStripMenuItem, "windowToolStripMenuItem");
            this.windowToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.refreshLayoutToolStripMenuItem,
            this.toolStripSeparator8,
            this.cascadeToolStripMenuItem,
            this.tileVerticalToolStripMenuItem,
            this.tileHorizontalToolStripMenuItem,
            this.toolStripSeparator9,
            this.arrangeIconsToolStripMenuItem,
            this.toolStripSeparator2,
            this.restoreAllToolStripMenuItem,
            this.minimizeAlltoolStripMenuItem,
            this.toolStripSeparator4,
            this.restoreOriginalSizeToolStripMenuItem,
            this.toolStripSeparator5});
            this.windowToolStripMenuItem.Name = "windowToolStripMenuItem";
            this.windowToolStripMenuItem.Padding = new System.Windows.Forms.Padding(2, 0, 1, 0);
            // 
            // refreshLayoutToolStripMenuItem
            // 
            resources.ApplyResources(this.refreshLayoutToolStripMenuItem, "refreshLayoutToolStripMenuItem");
            this.refreshLayoutToolStripMenuItem.Name = "refreshLayoutToolStripMenuItem";
            this.refreshLayoutToolStripMenuItem.Click += new System.EventHandler(this.MdiRefreshLayout);
            // 
            // toolStripSeparator8
            // 
            resources.ApplyResources(this.toolStripSeparator8, "toolStripSeparator8");
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            // 
            // cascadeToolStripMenuItem
            // 
            resources.ApplyResources(this.cascadeToolStripMenuItem, "cascadeToolStripMenuItem");
            this.cascadeToolStripMenuItem.Name = "cascadeToolStripMenuItem";
            this.cascadeToolStripMenuItem.Click += new System.EventHandler(this.MdiCascade);
            // 
            // tileVerticalToolStripMenuItem
            // 
            resources.ApplyResources(this.tileVerticalToolStripMenuItem, "tileVerticalToolStripMenuItem");
            this.tileVerticalToolStripMenuItem.Name = "tileVerticalToolStripMenuItem";
            this.tileVerticalToolStripMenuItem.Click += new System.EventHandler(this.MdiTileVertical);
            // 
            // tileHorizontalToolStripMenuItem
            // 
            resources.ApplyResources(this.tileHorizontalToolStripMenuItem, "tileHorizontalToolStripMenuItem");
            this.tileHorizontalToolStripMenuItem.Name = "tileHorizontalToolStripMenuItem";
            this.tileHorizontalToolStripMenuItem.Click += new System.EventHandler(this.MdiTileHorizontal);
            // 
            // toolStripSeparator9
            // 
            resources.ApplyResources(this.toolStripSeparator9, "toolStripSeparator9");
            this.toolStripSeparator9.Name = "toolStripSeparator9";
            // 
            // arrangeIconsToolStripMenuItem
            // 
            resources.ApplyResources(this.arrangeIconsToolStripMenuItem, "arrangeIconsToolStripMenuItem");
            this.arrangeIconsToolStripMenuItem.Name = "arrangeIconsToolStripMenuItem";
            this.arrangeIconsToolStripMenuItem.Click += new System.EventHandler(this.MdiArrangeIons);
            // 
            // toolStripSeparator2
            // 
            resources.ApplyResources(this.toolStripSeparator2, "toolStripSeparator2");
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            // 
            // restoreAllToolStripMenuItem
            // 
            resources.ApplyResources(this.restoreAllToolStripMenuItem, "restoreAllToolStripMenuItem");
            this.restoreAllToolStripMenuItem.Name = "restoreAllToolStripMenuItem";
            this.restoreAllToolStripMenuItem.Click += new System.EventHandler(this.MdiRestoreAll);
            // 
            // minimizeAlltoolStripMenuItem
            // 
            resources.ApplyResources(this.minimizeAlltoolStripMenuItem, "minimizeAlltoolStripMenuItem");
            this.minimizeAlltoolStripMenuItem.Name = "minimizeAlltoolStripMenuItem";
            this.minimizeAlltoolStripMenuItem.Click += new System.EventHandler(this.MdiMinimizeAll);
            // 
            // toolStripSeparator4
            // 
            resources.ApplyResources(this.toolStripSeparator4, "toolStripSeparator4");
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            // 
            // restoreOriginalSizeToolStripMenuItem
            // 
            resources.ApplyResources(this.restoreOriginalSizeToolStripMenuItem, "restoreOriginalSizeToolStripMenuItem");
            this.restoreOriginalSizeToolStripMenuItem.Name = "restoreOriginalSizeToolStripMenuItem";
            this.restoreOriginalSizeToolStripMenuItem.Click += new System.EventHandler(this.MdiRestoreOriginalSize);
            // 
            // toolStripSeparator5
            // 
            resources.ApplyResources(this.toolStripSeparator5, "toolStripSeparator5");
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            // 
            // helpToolStripMenuItem
            // 
            resources.ApplyResources(this.helpToolStripMenuItem, "helpToolStripMenuItem");
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem,
            this.toolStripSeparator_secret,
            this.documentationToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Padding = new System.Windows.Forms.Padding(2, 0, 1, 0);
            // 
            // aboutToolStripMenuItem
            // 
            resources.ApplyResources(this.aboutToolStripMenuItem, "aboutToolStripMenuItem");
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.About);
            // 
            // toolStripSeparator_secret
            // 
            resources.ApplyResources(this.toolStripSeparator_secret, "toolStripSeparator_secret");
            this.toolStripSeparator_secret.Name = "toolStripSeparator_secret";
            this.toolStripSeparator_secret.Click += new System.EventHandler(this.Secret);
            // 
            // documentationToolStripMenuItem
            // 
            resources.ApplyResources(this.documentationToolStripMenuItem, "documentationToolStripMenuItem");
            this.documentationToolStripMenuItem.Name = "documentationToolStripMenuItem";
            // 
            // toolStripMenuItem_desktopMode
            // 
            resources.ApplyResources(this.toolStripMenuItem_desktopMode, "toolStripMenuItem_desktopMode");
            this.toolStripMenuItem_desktopMode.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripMenuItem_desktopMode.Name = "toolStripMenuItem_desktopMode";
            this.toolStripMenuItem_desktopMode.Click += new System.EventHandler(this.DesktopMode);
            // 
            // closeAllToolStripMenuItem
            // 
            resources.ApplyResources(this.closeAllToolStripMenuItem, "closeAllToolStripMenuItem");
            this.closeAllToolStripMenuItem.Name = "closeAllToolStripMenuItem";
            // 
            // statusStrip1
            // 
            resources.ApplyResources(this.statusStrip1, "statusStrip1");
            this.statusStrip1.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel_status,
            this.toolStripStatusLabel_groups,
            this.toolStripStatusLabel_programs,
            this.toolStripStatusLabel_errors});
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.FontChanged += new System.EventHandler(this.statusStrip1_FontChanged);
            // 
            // toolStripStatusLabel_status
            // 
            resources.ApplyResources(this.toolStripStatusLabel_status, "toolStripStatusLabel_status");
            this.toolStripStatusLabel_status.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.toolStripStatusLabel_status.BorderStyle = System.Windows.Forms.Border3DStyle.Etched;
            this.toolStripStatusLabel_status.Margin = new System.Windows.Forms.Padding(0);
            this.toolStripStatusLabel_status.Name = "toolStripStatusLabel_status";
            // 
            // toolStripStatusLabel_groups
            // 
            resources.ApplyResources(this.toolStripStatusLabel_groups, "toolStripStatusLabel_groups");
            this.toolStripStatusLabel_groups.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.toolStripStatusLabel_groups.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.toolStripStatusLabel_groups.BorderStyle = System.Windows.Forms.Border3DStyle.Etched;
            this.toolStripStatusLabel_groups.Margin = new System.Windows.Forms.Padding(0);
            this.toolStripStatusLabel_groups.Name = "toolStripStatusLabel_groups";
            // 
            // toolStripStatusLabel_programs
            // 
            resources.ApplyResources(this.toolStripStatusLabel_programs, "toolStripStatusLabel_programs");
            this.toolStripStatusLabel_programs.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.toolStripStatusLabel_programs.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.toolStripStatusLabel_programs.BorderStyle = System.Windows.Forms.Border3DStyle.Etched;
            this.toolStripStatusLabel_programs.Margin = new System.Windows.Forms.Padding(0);
            this.toolStripStatusLabel_programs.Name = "toolStripStatusLabel_programs";
            // 
            // toolStripStatusLabel_errors
            // 
            resources.ApplyResources(this.toolStripStatusLabel_errors, "toolStripStatusLabel_errors");
            this.toolStripStatusLabel_errors.ActiveLinkColor = System.Drawing.Color.DarkOrange;
            this.toolStripStatusLabel_errors.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.toolStripStatusLabel_errors.BorderStyle = System.Windows.Forms.Border3DStyle.Etched;
            this.toolStripStatusLabel_errors.Image = global::UV7_Program_Manager.Properties.Resources.Warning_16;
            this.toolStripStatusLabel_errors.IsLink = true;
            this.toolStripStatusLabel_errors.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.toolStripStatusLabel_errors.LinkColor = System.Drawing.Color.Red;
            this.toolStripStatusLabel_errors.Margin = new System.Windows.Forms.Padding(0);
            this.toolStripStatusLabel_errors.Name = "toolStripStatusLabel_errors";
            this.toolStripStatusLabel_errors.VisitedLinkColor = System.Drawing.Color.DarkRed;
            this.toolStripStatusLabel_errors.Click += new System.EventHandler(this.toolStripStatusLabel_errors_Click);
            this.toolStripStatusLabel_errors.MouseEnter += new System.EventHandler(this.toolStripStatusLabel_errors_MouseEnter);
            // 
            // timer_desktop
            // 
            this.timer_desktop.Interval = 50;
            this.timer_desktop.Tick += new System.EventHandler(this.timer_desktop_Tick);
            // 
            // contextMenu_changeScreen
            // 
            resources.ApplyResources(this.contextMenu_changeScreen, "contextMenu_changeScreen");
            // 
            // folv_prog
            // 
            resources.ApplyResources(this.folv_prog, "folv_prog");
            this.folv_prog.AllColumns.Add(this.olvc_name);
            this.folv_prog.AllColumns.Add(this.olvc_group);
            this.folv_prog.AllColumns.Add(this.olvc_description);
            this.folv_prog.CellEditUseWholeCell = false;
            this.folv_prog.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvc_name,
            this.olvc_group,
            this.olvc_description});
            this.folv_prog.Cursor = System.Windows.Forms.Cursors.Default;
            this.folv_prog.FullRowSelect = true;
            this.folv_prog.HideSelection = false;
            this.folv_prog.MultiSelect = false;
            this.folv_prog.Name = "folv_prog";
            this.folv_prog.OverlayText.Text = resources.GetString("resource.Text");
            this.folv_prog.ShowGroups = false;
            this.folv_prog.UseCompatibleStateImageBehavior = false;
            this.folv_prog.UseExplorerTheme = true;
            this.folv_prog.View = System.Windows.Forms.View.LargeIcon;
            this.folv_prog.VirtualMode = true;
            this.folv_prog.ItemActivate += new System.EventHandler(this.OLV_StartSelected);
            // 
            // olvc_name
            // 
            this.olvc_name.AspectName = "Name";
            this.olvc_name.Groupable = false;
            resources.ApplyResources(this.olvc_name, "olvc_name");
            // 
            // olvc_group
            // 
            this.olvc_group.AspectName = "Group";
            resources.ApplyResources(this.olvc_group, "olvc_group");
            // 
            // olvc_description
            // 
            this.olvc_description.AspectName = "Description";
            this.olvc_description.FillsFreeSpace = true;
            this.olvc_description.Groupable = false;
            resources.ApplyResources(this.olvc_description, "olvc_description");
            // 
            // gradientPanel_desktop
            // 
            resources.ApplyResources(this.gradientPanel_desktop, "gradientPanel_desktop");
            this.gradientPanel_desktop.Angle = 90F;
            this.gradientPanel_desktop.Color1 = System.Drawing.Color.Empty;
            this.gradientPanel_desktop.Color2 = System.Drawing.SystemColors.Control;
            this.gradientPanel_desktop.Controls.Add(this.dropDownButton_changeScreen);
            this.gradientPanel_desktop.Controls.Add(this.dropDownButton_powerOptions);
            this.gradientPanel_desktop.Controls.Add(this.label_machineName);
            this.gradientPanel_desktop.Controls.Add(this.label_userName);
            this.gradientPanel_desktop.Controls.Add(this.label_time);
            this.gradientPanel_desktop.Name = "gradientPanel_desktop";
            // 
            // dropDownButton_changeScreen
            // 
            resources.ApplyResources(this.dropDownButton_changeScreen, "dropDownButton_changeScreen");
            this.dropDownButton_changeScreen.BackColor = System.Drawing.Color.Transparent;
            this.dropDownButton_changeScreen.DropDownArrowSize = 13;
            this.dropDownButton_changeScreen.FlatAppearance.BorderSize = 0;
            this.dropDownButton_changeScreen.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.ControlLight;
            this.dropDownButton_changeScreen.FlatAppearance.MouseOverBackColor = System.Drawing.Color.WhiteSmoke;
            this.dropDownButton_changeScreen.Name = "dropDownButton_changeScreen";
            this.dropDownButton_changeScreen.TabStop = false;
            this.dropDownButton_changeScreen.UseVisualStyleBackColor = false;
            this.dropDownButton_changeScreen.Click += new System.EventHandler(this.ChangeScreen);
            // 
            // dropDownButton_powerOptions
            // 
            resources.ApplyResources(this.dropDownButton_powerOptions, "dropDownButton_powerOptions");
            this.dropDownButton_powerOptions.BackColor = System.Drawing.Color.Transparent;
            this.dropDownButton_powerOptions.DropDownArrowSize = 13;
            this.dropDownButton_powerOptions.FlatAppearance.BorderSize = 0;
            this.dropDownButton_powerOptions.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.ControlLight;
            this.dropDownButton_powerOptions.FlatAppearance.MouseOverBackColor = System.Drawing.Color.WhiteSmoke;
            this.dropDownButton_powerOptions.Name = "dropDownButton_powerOptions";
            this.dropDownButton_powerOptions.TabStop = false;
            this.dropDownButton_powerOptions.UseVisualStyleBackColor = false;
            this.dropDownButton_powerOptions.Click += new System.EventHandler(this.ShowPowerOptions);
            // 
            // label_machineName
            // 
            resources.ApplyResources(this.label_machineName, "label_machineName");
            this.label_machineName.BackColor = System.Drawing.Color.Transparent;
            this.label_machineName.Name = "label_machineName";
            // 
            // label_userName
            // 
            resources.ApplyResources(this.label_userName, "label_userName");
            this.label_userName.BackColor = System.Drawing.Color.Transparent;
            this.label_userName.Name = "label_userName";
            // 
            // label_time
            // 
            resources.ApplyResources(this.label_time, "label_time");
            this.label_time.BackColor = System.Drawing.Color.Transparent;
            this.label_time.Name = "label_time";
            // 
            // Form_main
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.folv_prog);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip_main);
            this.Controls.Add(this.gradientPanel_desktop);
            this.DoubleBuffered = true;
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip_main;
            this.Name = "Form_main";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form_main_FormClosing);
            this.Load += new System.EventHandler(this.Form_main_Load);
            this.Shown += new System.EventHandler(this.Form_main_Shown);
            this.ResizeEnd += new System.EventHandler(this.Form_main_ResizeEnd);
            this.BackColorChanged += new System.EventHandler(this.Form_main_BackColorChanged);
            this.Resize += new System.EventHandler(this.Form_main_Resize);
            this.menuStrip_main.ResumeLayout(false);
            this.menuStrip_main.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.folv_prog)).EndInit();
            this.gradientPanel_desktop.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.MenuStrip menuStrip_main;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem windowToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem preferencesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem startProgramToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reloadEntriesToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem closeAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cascadeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tileVerticalToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tileHorizontalToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem arrangeIconsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem runToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel_status;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel_groups;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel_programs;
        private System.Windows.Forms.ToolStripMenuItem minimizeAlltoolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem restoreAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem restoreOriginalSizeToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel_errors;
        private System.Windows.Forms.ToolStripMenuItem resetMainWindowSizeToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripMenuItem openProgramsDirectoryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openUsersStartMenuDirectoryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openSystemStartMenuDirectoryToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator_secret;
        private System.Windows.Forms.ToolStripMenuItem documentationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitWindowstoolStripMenuItem;
        private UV7_Program_Manager.CustomControls.GradientPanel gradientPanel_desktop;
        private System.Windows.Forms.Label label_machineName;
        private System.Windows.Forms.Label label_userName;
        private System.Windows.Forms.Label label_time;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_desktopMode;
        private System.Windows.Forms.Timer timer_desktop;
        private UV7_Program_Manager.CustomControls.DropDownButton dropDownButton_powerOptions;
        private UV7_Program_Manager.CustomControls.DropDownButton dropDownButton_changeScreen;
        private System.Windows.Forms.ContextMenu contextMenu_changeScreen;
        private BrightIdeasSoftware.FastObjectListView folv_prog;
        private BrightIdeasSoftware.OLVColumn olvc_group;
        private BrightIdeasSoftware.OLVColumn olvc_name;
        private BrightIdeasSoftware.OLVColumn olvc_description;
        private System.Windows.Forms.ToolStripMenuItem refreshLayoutToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator9;
    }
}

