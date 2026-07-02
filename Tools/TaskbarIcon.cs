using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using UV7_Program_Manager.Config;
using UV7_Program_Manager.Tools;

namespace UV7_Program_Manager
{
    public static class TaskbarIcon
    {
        static void HsvToRgb(double h, double S, double V, out int r, out int g, out int b)
        {
            double H = h;
            while (H < 0) { H += 360; };
            while (H >= 360) { H -= 360; };
            double R, G, B;
            if (V <= 0)
            { R = G = B = 0; }
            else if (S <= 0)
            {
                R = G = B = V;
            }
            else
            {
                double hf = H / 60.0;
                int i = (int)Math.Floor(hf);
                double f = hf - i;
                double pv = V * (1 - S);
                double qv = V * (1 - S * f);
                double tv = V * (1 - S * (1 - f));
                switch (i)
                {

                    // Red is the dominant color

                    case 0:
                        R = V;
                        G = tv;
                        B = pv;
                        break;

                    // Green is the dominant color

                    case 1:
                        R = qv;
                        G = V;
                        B = pv;
                        break;
                    case 2:
                        R = pv;
                        G = V;
                        B = tv;
                        break;

                    // Blue is the dominant color

                    case 3:
                        R = pv;
                        G = qv;
                        B = V;
                        break;
                    case 4:
                        R = tv;
                        G = pv;
                        B = V;
                        break;

                    // Red is the dominant color

                    case 5:
                        R = V;
                        G = pv;
                        B = qv;
                        break;

                    // Just in case we overshoot on our math by a little, we put these here. Since its a switch it won't slow us down at all to put these here.

                    case 6:
                        R = V;
                        G = tv;
                        B = pv;
                        break;
                    case -1:
                        R = V;
                        G = pv;
                        B = qv;
                        break;

                    // The color is not defined, we should throw an error.

                    default:
                        //LFATAL("i Value error in Pixel conversion, Value is %d", i);
                        R = G = B = V; // Just pretend its black/white
                        break;
                }
            }
            r = Clamp((int)(R * 255.0));
            g = Clamp((int)(G * 255.0));
            b = Clamp((int)(B * 255.0));
        }

        /// <summary>
        /// Clamp a value to 0-255
        /// </summary>
        static int Clamp(int i)
        {
            if (i < 0) return 0;
            if (i > 255) return 255;
            return i;
        }

        private class CustomRenderer : ToolStripProfessionalRenderer
        {
            public CustomRenderer() : base(new RedColors()) { }
        }

        private class RedColors : ProfessionalColorTable
        {
            public override Color MenuItemSelected
            {
                get 
                {
                    float H = ApplicationConfig.TaskbarIconColor.GetHue();
                    float S = ApplicationConfig.TaskbarIconColor.GetSaturation();
                    float V = ApplicationConfig.TaskbarIconColor.GetBrightness();
                    V *= (float)3;
                    if (V > 1) V = 1;
                    S /= (float)4.0;

                    int R, G, B;
                    HsvToRgb(H, S, V, out R, out G, out B);

                    Color c = Color.FromArgb(255, R, G, B);
                    return c;
                }
            }
            
            public override Color MenuItemBorder
            {
                get { return ApplicationConfig.TaskbarIconColor; }
            }
        }


        public static NotifyIcon Icon;

        public static ContextMenuStrip ContextMenu;
        
        public static void ShowQuickLaunchMenu(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ContextMenu.Show(Control.MousePosition.X, Control.MousePosition.Y);
            }
            
        }

        static ProgramGroup[] ProgramGroups;

        public static void Load(ProgramGroup[] programGroups)
        {
            if (Icon != null) Icon.Visible = false;

            ProgramGroups = programGroups;

            Icon = new NotifyIcon();
            NotifyIcon n = Icon;
            n.Icon = System.Drawing.Icon.FromHandle(Properties.Resources.Quick_Launch.GetHicon());
            ContextMenu = new ContextMenuStrip();
            ContextMenuStrip c = ContextMenu;
            c.Renderer = new CustomRenderer();

            n.MouseUp += new MouseEventHandler(ShowQuickLaunchMenu);
            n.Text = Dialog.TaskbarIcon_QuickLaunch;
            //n.ContextMenuStrip = new ContextMenuStrip();
            //n.ContextMenuStrip.Items.Add("UV7 Program Manager - Options");
            //n.ContextMenuStrip.Items[0].Font = new Font(n.ContextMenuStrip.Font, FontStyle.Bold);
            //n.ContextMenuStrip.Items.Add(new ToolStripSeparator());
            //n.ContextMenuStrip.Items.Add("Show");
            //n.ContextMenuStrip.Items.Add("Desktop Mode");
            //n.ContextMenuStrip.Items.Add(new ToolStripSeparator());
            //n.ContextMenuStrip.Items.Add("Exit Program");
            //n.ContextMenuStrip.Items.Add("Exit Windows");

            c.Items.Add(Dialog.TaskbarIcon_QuickLaunch);
            c.Items[0].Font = new Font(c.Items[0].Font, FontStyle.Bold);
            c.Items[0].ForeColor = ApplicationConfig.TaskbarIconColor;
            c.Items[0].Image = Properties.Resources.Progman_16;
            c.Items[0].MouseUp += new MouseEventHandler(ShowFormMain);
            c.Items.Add(new ToolStripSeparator());

            for (int i = 0; i < ProgramGroups.Length; i++)
            {
                DirectoryInfo dirInfo = new DirectoryInfo(ProgramGroups[i].Name);
                ToolStripMenuItem tm = new ToolStripMenuItem(dirInfo.Name);
                tm.Image = Properties.Resources.Prog_Folder;
                foreach (ProgramItem item in ProgramGroups[i])
                {
                    ToolStripMenuItem tp = new ToolStripMenuItem();
                    tp.Text = Path.GetFileNameWithoutExtension(item.ShortCutFile);
                    
                    tp.Image = item.ImageSmall;
                    tp.Click += new EventHandler(StartSelected);
                    tm.DropDownItems.Add(tp);
                }
                c.Items.Add(tm);
            }

            c.Items.Add(new ToolStripSeparator());

            if (ApplicationConfig.TaskbarIconShutdown)
            {
                ToolStripMenuItem shutdownItem = new ToolStripMenuItem();
                shutdownItem.Text = Dialog.TaskbarIcon_ShutDown;
                shutdownItem.Image = Properties.Resources.End_Session;
                shutdownItem.Click += new EventHandler(Shutdown);
                c.Items.Add(shutdownItem);
            }

            c.Items.Add(Dialog.TaskbarIcon_Cancel);

            n.Visible = true;
            //n.ShowBalloonTip(2000, "UV7 Program Manager", "... supports Quick Launch now!\nClick on the icon to see more!", ToolTipIcon.Info);
        }

        private static void StartSelected(object sender, EventArgs e)
        {
            if (ContextMenu.Items.Count > 2)
            {
                ToolStripMenuItem t = (ToolStripMenuItem)sender;
                ToolStripMenuItem p = (ToolStripMenuItem)t.OwnerItem;
                int iGroup = ContextMenu.Items.IndexOf(p) - 2; // Do not count Shutdown entry
                int iProgram = p.DropDownItems.IndexOf(t);
                try
                {
                    ProgramItem item = ProgramGroups[iGroup][iProgram];
                    ProcessStartInfo info = new ProcessStartInfo(item.Target, item.Arguments);
                    if (item.RunAsAdmin) info.Verb = "runas";
                    Process.Start(info);
                }
                catch (Win32Exception w)
                {
                    if (w.NativeErrorCode == 2 || w.NativeErrorCode == 3)
                    {
                        FileInfo f = new FileInfo(ProgramGroups[iGroup][iProgram].Target);

                        DialogResult result = MessageBox.Show(string.Format(Dialog.Prog_ShortcutFileNotFound, f.Name), Dialog.Prog_ShortcutFileNotFoundTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                        if (result == DialogResult.Yes)
                        {
                            try
                            {
                                File.Delete(ProgramGroups[iGroup][iProgram].ShortCutFile);
                                ProgramGroups[iGroup].RemoveAt(iProgram);
                                ToolStripMenuItem tm = (ToolStripMenuItem)ContextMenu.Items[iGroup];
                                tm.DropDownItems.RemoveAt(iProgram);
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(string.Format(Dialog.Prog_ShortcutDeleteError, ex.Message, ex.Data), Dialog.Prog_ShortcutDeleteErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + "\r\n\r\n" + ProgramGroups[iGroup][iProgram].Target, ex.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private static void Shutdown(object sender, EventArgs e)
        {
            Shell32.Shell shell = new Shell32.Shell();
            shell.ShutdownWindows();
        }

        private static void ShowFormMain(object sender, MouseEventArgs e)
        {
            foreach (Form_main f in Application.OpenForms.OfType<Form_main>())
            {
                f.BringToFront();
            }
        }
    }
}
