using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using UV7_Program_Manager.ShellTools;
using UV7_Program_Manager.Tools;

namespace UV7_Program_Manager
{
    public partial class Form_prog : Form
    {
        ProgramGroup programs;
        
        public Form_prog(string text, ProgramGroup group, Form mdiParent)
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            
            this.Text = text;
            this.MdiParent = mdiParent;

            programs = group;
            List<ListViewItem> items = new List<ListViewItem>();
            foreach (ProgramItem item in group)
            {
                imageList_32.Images.Add(item.ImageLarge);
                ListViewItem lvitem = new ListViewItem(Path.GetFileNameWithoutExtension(item.ShortCutFile), imageList_32.Images.Count - 1);
                items.Add(lvitem);
            }
            nativeListView_programs.Items.AddRange(items.ToArray());
        }

        public override Color BackColor
        {
            get
            {
                return Color.Transparent;
            }
            set
            {

            }
        }

        private void Form_prog_Load(object sender, EventArgs e)
        {
            void SetAllControlsFont(Control.ControlCollection ctrls)
            {
                foreach (Control ctrl in ctrls)
                {
                    if (ctrl.Controls != null)
                        SetAllControlsFont(ctrl.Controls);

                    ctrl.Font = new Font(Config.ApplicationConfig.Font, ctrl.Font.Style);
                }
            }
            SetAllControlsFont(this.Controls);
            LoadFontColor();
        }
        
        private void Form_prog_ResizeEnd(object sender, EventArgs e)
        {
            if (nativeListView_programs.TransparentBackground)
                nativeListView_programs.DrawTransparentBackground();
        }

        public void LoadFontColor()
        {
            if (Config.ApplicationConfig.WhiteFont)
                nativeListView_programs.ForeColor = Color.White;
            else 
                nativeListView_programs.ForeColor = Color.Black;
        }

        private void nativeListView_programs_ItemActivate(object sender, EventArgs e)
        {
            StartSelected();
        }

        public void StartSelected()
        {
            if (nativeListView_programs.SelectedItems.Count > 0)
            {
                try
                {
                    ProgramItem item = programs[nativeListView_programs.Items.IndexOf(nativeListView_programs.SelectedItems[0])];
                    
                    ProcessStartInfo info = new ProcessStartInfo(item.Target, item.Arguments);
                    if (item.RunAsAdmin)
                        info.Verb = "runas";
                    Process.Start(info);
                }
                catch (Win32Exception w)
                {
                    // If target file does not exist, ask wheter to delete the link
                    if (w.NativeErrorCode == 2 || w.NativeErrorCode == 3)
                    {
                        FileInfo f = new FileInfo(programs[nativeListView_programs.Items.IndexOf(nativeListView_programs.SelectedItems[0])].Target);

                        DialogResult result = MessageBox.Show(string.Format(Dialog.Prog_ShortcutFileNotFound, f.Name), Dialog.Prog_ShortcutFileNotFoundTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                        if (result == DialogResult.Yes)
                        {
                            try
                            {
                                File.Delete(programs[nativeListView_programs.Items.IndexOf(nativeListView_programs.SelectedItems[0])].ShortCutFile);
                                programs.RemoveAt(nativeListView_programs.Items.IndexOf(nativeListView_programs.SelectedItems[0]));
                                nativeListView_programs.Items.RemoveAt(nativeListView_programs.Items.IndexOf(nativeListView_programs.SelectedItems[0]));
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
                    MessageBox.Show(ex.Message + "\r\n\r\n" + programs[nativeListView_programs.Items.IndexOf(nativeListView_programs.SelectedItems[0])].Target, ex.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void nativeListView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                StartSelected();
            }
        }
    }
}
