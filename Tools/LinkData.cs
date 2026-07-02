using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using UV7_Program_Manager.Config;
using UV7_Program_Manager.ShellTools;

namespace UV7_Program_Manager.Tools
{
    public struct ProgramItem
    {
        public string Arguments;
        public string Description;
        public WindowDisplayMode DisplayMode;
        public Bitmap ImageLarge;
        public Bitmap ImageSmall;
        public string Group;
        public string Name { get { return Path.GetFileNameWithoutExtension(ShortCutFile); } }
        public bool RunAsAdmin;
        public string ShortCutFile;
        public string Target;
        public bool WebShortcut;
        public string WorkingDirectory;
        
        public ProgramItem(string arguments, string description, WindowDisplayMode displayMode, string group,
            Bitmap imageLarge, Bitmap imageSmall, bool runAsAdmin, string shortCutFile, string target, bool webShortcut, string workingDirectory)
        {
            Arguments = arguments;
            Description = description;
            DisplayMode = displayMode;
            ImageLarge = imageLarge;
            ImageSmall = imageSmall;
            Group = group;
            RunAsAdmin = runAsAdmin;
            ShortCutFile = shortCutFile;
            Target = target;
            WebShortcut = webShortcut;
            WorkingDirectory = workingDirectory;
        }

        public ProgramItem(ShellLink link, string group, bool runAsAdmin)
        {
            Arguments = link.Arguments;
            Description = link.Description;
            DisplayMode = (WindowDisplayMode)((int)link.DisplayMode);
            if (link.LargeIcon == null)
                ImageLarge = ClearBitmap.Generate32();
            else ImageLarge = link.LargeIcon.ToBitmap();
            // Ensure null images are displayed as empty icons, avoid NullReferenceExceptions
            if (link.SmallIcon == null) ImageSmall = ClearBitmap.Generate16();
            else ImageSmall = link.SmallIcon.ToBitmap();

            Group = group;
            RunAsAdmin = runAsAdmin;
            ShortCutFile = link.ShortCutFile;
            Target = link.Target;
            WebShortcut = false;
            WorkingDirectory = link.WorkingDirectory;
        }
    }

    public class ProgramGroup : List<ProgramItem>
    {
        public string Name;
        public ProgramGroup(string name)
        {
            Name = name;
        }
    }

    public enum WindowDisplayMode
    {
        Normal,
        Minimized,
        Maximized
    }


    public static class LinkData
    {
        public static ProgramGroup[] LoadData(out int groups, out int programs, out int errors, out bool fatal, out string ex)
        {
            groups = 0;
            programs = 0;
            errors = 0;
            fatal = false;
            ex = "";

            ProgramGroup[] programGroups = null;

            try
            {
                var dirs = Directory.GetDirectories(Application.StartupPath + @"\Programs");
                programGroups = new ProgramGroup[dirs.Length];

                for (int i = 0; i < dirs.Length; i++)
                {
                    DirectoryInfo dirInfo = new DirectoryInfo(dirs[i]);

                    ProgramGroup currentGroup = new ProgramGroup(Path.GetFileName(dirs[i]));

                    string[] files = Directory.GetFiles(dirInfo.FullName);

                    foreach (string file in Directory.GetFiles(dirInfo.FullName))
                    {
                        FileInfo fi = new FileInfo(file);
                        if (fi.Extension.ToLower() == ".url")
                        {
                            try
                            {
                                string data = File.ReadAllText(fi.FullName);
                                int urlIndex = data.IndexOf("URL=");
                                int breakIndex = data.IndexOf("\r\n", urlIndex);
                                string url = data.Substring(urlIndex + 4, breakIndex - urlIndex - 4);

                                ProgramItem item = new ProgramItem(
                                    "",
                                    "",
                                    WindowDisplayMode.Normal,
                                    Path.GetFileName(dirs[i]),
                                    ClearBitmap.Generate32(),
                                    ClearBitmap.Generate16(),
                                    false,
                                    fi.Name,
                                    url,
                                    true,
                                    ""
                                    );
                                currentGroup.Add(item);

                                programs++;
                            }
                            catch
                            {
                                FileInfo fInfo = new FileInfo(file);
                                CurrentConfig.ErrorList.Add(@"\" + dirInfo.Name + @"\" + fInfo.Name);
                                errors++;
                            }
                        }
                        else if (fi.Extension.ToLower() == ".lnk")
                        {
                            try
                            {
                                ShellLink link = new ShellLink(file);
                                // Check if Shortcut needs to be run as Admin
                                bool runAsAdmin = false;
                                byte adminByte = 0;
                                using (BinaryReader reader = new BinaryReader(new FileStream(file, FileMode.Open)))
                                {
                                    reader.BaseStream.Seek(0x15, SeekOrigin.Begin);
                                    adminByte = reader.ReadByte();
                                    runAsAdmin = (adminByte & (1 << 5)) != 0; // Check for Bit 0x5 in Byte 0x15
                                    reader.Close();
                                }
                                ProgramItem item = new ProgramItem(link, Path.GetFileName(dirs[i]), runAsAdmin);
                                if (runAsAdmin)
                                    item.ImageLarge = UACShieldImage.AddUACShield(item.ImageLarge);
                                currentGroup.Add(item);
                                programs++;
                            }
                            catch
                            {
                                FileInfo fInfo = new FileInfo(file);
                                CurrentConfig.ErrorList.Add(@"\" + dirInfo.Name + @"\" + fInfo.Name);
                                errors++;
                            }
                        }
                    }
                    
                    programGroups[i] = currentGroup;
                    groups++;
                }
            }

            catch (Exception exc)
            {
                fatal = true;
                ex = exc.Message;
            }
            
            return programGroups;
        }
    }
}
