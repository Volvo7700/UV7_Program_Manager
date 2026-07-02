using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using UV7_Program_Manager.Config;
using UV7_Program_Manager.CustomControls;
using UV7_Program_Manager.Dialogs;
using UV7_Program_Manager.Tools;

namespace UV7_Program_Manager
{
    public partial class Form_main : Form
    {
        public void UpdateRenderMode()
        {
            this.IsMdiContainer = !ApplicationConfig.NewRenderMode;
            folv_prog.Visible = ApplicationConfig.NewRenderMode;
        }
        
        private void LoadData()
        {
            this.Text = Dialog.Main_Load;
            toolStripStatusLabel_status.Text = Dialog.Main_StatusStrip_Load;

            Form frm = null;
            foreach (Form_errors f in Application.OpenForms.OfType<Form_errors>())
            {
                frm = f;
            }
            if (frm != null)
            {
                frm.Close();
                frm.Dispose();
            }

            CurrentConfig.ErrorList.Clear();
            toolStripStatusLabel_errors.LinkVisited = false;

            int groups = 0;
            int programs = 0;
            int errors = 0;
            bool fatal = false;
            string exception = "";

            ProgramGroup[] programGroups = LinkData.LoadData(out groups, out programs, out errors, out fatal, out exception);

            if (fatal == true)
            {
                DialogResult result = MessageBox.Show(Dialog.Main_MajorLoadError + exception, Dialog.Main_MajorLoadErrorTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                toolStripStatusLabel_status.Text = String.Format(Dialog.Main_StatusStrip_MajorErrors, errors);
                if (result == DialogResult.Yes) LoadData();
                else return;
            }

            // Decide whether to display one grouped ListView or MDI Windows
            UpdateRenderMode();
            if (ApplicationConfig.NewRenderMode)
            {
                folv_prog.AlwaysGroupByColumn = olvc_group;
                folv_prog.ShowGroups = true;
                
                // Convert the grouped programs to one list and send it to the ObjectListView
                ProgramGroup allProgramsList = new ProgramGroup("");
                foreach (ProgramGroup p in programGroups)
                {
                    allProgramsList.AddRange(p);
                }
                folv_prog.SetObjects(allProgramsList);
            }
            else
            {
                // Create a MDI Window for each group
                foreach (ProgramGroup p in programGroups)
                {
                    Form_prog form_prog = new Form_prog(p.Name, p, this);

                    foreach (NativeListView n in form_prog.Controls.OfType<NativeListView>())
                    {
                        if (ApplicationConfig.ProgramGroupsTransparent)
                        {
                            form_prog.BackColor = Color.Transparent;
                            n.TransparentBackground = true;
                        }
                        if (ApplicationConfig.ProgramGroupsApplyColor)
                        {
                            n.BackColor = ApplicationConfig.BackgroundColor;
                        }
                        n.programGroupsLoaded = true;
                    }
                    form_prog.Show();
                }
                toolStripStatusLabel_status.Text = Dialog.Main_StatusStrip_LoadFinished;
            }

            if (errors > 0)
            {
                if (errors == 1) toolStripStatusLabel_errors.Text = errors.ToString() + Dialog.Main_StatusStrip_Error;
                else toolStripStatusLabel_errors.Text = errors.ToString() + Dialog.Main_StatusStrip_Errors;

                toolStripStatusLabel_errors.Visible = true;
            }
            else
            {
                toolStripStatusLabel_errors.Visible = false;
            }

            toolStripStatusLabel_groups.Text = groups + Dialog.Main_StatusStrip_Groups;
            toolStripStatusLabel_programs.Text = programs + Dialog.Main_StatusStrip_Programs;


            if (ApplicationConfig.TaskbarIcon)
            {
                Bitmap shield16 = ClearBitmap.Generate16();
                shield16 = UACShieldImage.AddUACShield(shield16);
                foreach (ProgramGroup group in programGroups)
                {
                    for (int i = 0; i < group.Count; i++)
                    {
                        if (group[i].RunAsAdmin)
                        {
                            ProgramItem tmpItm = group[i];
                            tmpItm.ImageSmall = shield16;
                            group[i] = tmpItm;
                        }   
                    }
                }
                TaskbarIcon.Load(programGroups);
            }

            switch (ApplicationConfig.StartupLayout)
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

            if (ApplicationConfig.DesktopMode)
            {
                label_machineName.Text = Environment.MachineName;
            }
            else
            {
                this.Text = "UV7 Program Manager - " + Environment.MachineName + "\\" + Environment.UserName;
            }
        }
        
        private void OLV_StartSelected(object sender, EventArgs e)
        {
            if (folv_prog.SelectedObjects.Count > 0)
            {
                ProgramItem item = (ProgramItem)folv_prog.SelectedObject;
                try
                {
                    ProcessStartInfo info = new ProcessStartInfo(item.Target, item.Arguments);
                    if (item.RunAsAdmin) info.Verb = "runas";
                    Process.Start(info);
                }
                catch (Win32Exception w)
                {
                    // If target file does not exist, ask wheter to delete the link
                    if (w.NativeErrorCode == 2 || w.NativeErrorCode == 3)
                    {
                        FileInfo f = new FileInfo(item.Target);

                        DialogResult result = MessageBox.Show(string.Format(Dialog.Prog_ShortcutFileNotFound, f.Name), Dialog.Prog_ShortcutFileNotFoundTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                        if (result == DialogResult.Yes)
                        {
                            try
                            {
                                File.Delete(item.ShortCutFile);
                                folv_prog.Items.RemoveByKey(item.Name);
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
                    MessageBox.Show(ex.Message + "\r\n\r\n" + item.Target, ex.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
