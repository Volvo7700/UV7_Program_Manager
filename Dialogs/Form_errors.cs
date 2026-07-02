using System;
using System.Linq;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using UV7_Program_Manager.Config;

namespace UV7_Program_Manager.Dialogs
{
    public partial class Form_errors : Form
    {
        public Form_errors()
        {
            InitializeComponent();
        }

        private void Form_errors_Load(object sender, EventArgs e)
        {
            listBox_errors.Items.Clear();
            listBox_errors.Items.AddRange(CurrentConfig.ErrorList.ToArray());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Process.Start("explorer", Application.StartupPath + @"\Programs");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (listBox_errors.SelectedItems.Count > 0)
            {
                FileInfo f = new FileInfo(Application.StartupPath + @"\Programs" + listBox_errors.SelectedItem);
                Process.Start("explorer", f.DirectoryName);
            }
        }

        private void Form_errors_FormClosing(object sender, FormClosingEventArgs e)
        {
            CurrentConfig.ErrorsFormOpened = false;
        }
    }
}
