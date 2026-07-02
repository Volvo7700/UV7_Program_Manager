using System;
using System.Windows.Forms;
using UV7_Program_Manager.Tools;

namespace UV7_Program_Manager.Dialogs
{
    public partial class Form_about : Form
    {
        public Form_about()
        {
            InitializeComponent();
        }

        private void Form_about_Load(object sender, EventArgs e)
        {
            label_version.Text = VersionFormat.ReadableVersion + " Beta";
        }

        private void ShowCredits(object sender, EventArgs e)
        {
            Form_credits fc = new Form_credits();
            fc.ShowDialog(this);
        }
    }
}
