using System;
using System.Windows.Forms;
using UV7_Program_Manager.Config;
using UV7_Program_Manager.Tools;

namespace UV7_Program_Manager
{
    public partial class Form_start : Form
    {
        public Form_start()
        {
            InitializeComponent();
            label_version.Text = VersionFormat.ReadableVersion;
        }

        private void Form_start_Shown(object sender, EventArgs e)
        {
            ApplicationConfig.LoadConfigFile();
            Form_main f = new Form_main();
            f.Show();
        }
    }
}
