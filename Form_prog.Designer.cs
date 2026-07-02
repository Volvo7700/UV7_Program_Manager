
namespace UV7_Program_Manager
{
    partial class Form_prog
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_prog));
            this.imageList_32 = new System.Windows.Forms.ImageList(this.components);
            this.nativeListView_programs = new UV7_Program_Manager.CustomControls.NativeListView();
            this.SuspendLayout();
            // 
            // imageList_32
            // 
            this.imageList_32.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList_32.ImageStream")));
            this.imageList_32.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList_32.Images.SetKeyName(0, "progman.ico");
            this.imageList_32.Images.SetKeyName(1, "progman.ico");
            this.imageList_32.Images.SetKeyName(2, "progman.ico");
            this.imageList_32.Images.SetKeyName(3, "progman.ico");
            this.imageList_32.Images.SetKeyName(4, "progman.ico");
            this.imageList_32.Images.SetKeyName(5, "progman.ico");
            this.imageList_32.Images.SetKeyName(6, "progman.ico");
            this.imageList_32.Images.SetKeyName(7, "progman.ico");
            this.imageList_32.Images.SetKeyName(8, "progman.ico");
            this.imageList_32.Images.SetKeyName(9, "progman.ico");
            this.imageList_32.Images.SetKeyName(10, "progman.ico");
            this.imageList_32.Images.SetKeyName(11, "progman.ico");
            this.imageList_32.Images.SetKeyName(12, "progman.ico");
            this.imageList_32.Images.SetKeyName(13, "progman.ico");
            this.imageList_32.Images.SetKeyName(14, "progman.ico");
            this.imageList_32.Images.SetKeyName(15, "progman.ico");
            // 
            // nativeListView_programs
            // 
            this.nativeListView_programs.Activation = System.Windows.Forms.ItemActivation.TwoClick;
            this.nativeListView_programs.BackColor = System.Drawing.SystemColors.Control;
            this.nativeListView_programs.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.nativeListView_programs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.nativeListView_programs.FullRowSelect = true;
            this.nativeListView_programs.HideSelection = false;
            this.nativeListView_programs.LargeImageList = this.imageList_32;
            this.nativeListView_programs.Location = new System.Drawing.Point(0, 0);
            this.nativeListView_programs.MultiSelect = false;
            this.nativeListView_programs.Name = "nativeListView_programs";
            this.nativeListView_programs.Size = new System.Drawing.Size(592, 230);
            this.nativeListView_programs.SmallImageList = this.imageList_32;
            this.nativeListView_programs.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.nativeListView_programs.TabIndex = 0;
            this.nativeListView_programs.TileSize = new System.Drawing.Size(170, 36);
            this.nativeListView_programs.TransparentBackground = false;
            this.nativeListView_programs.UseCompatibleStateImageBehavior = false;
            this.nativeListView_programs.DoubleClick += new System.EventHandler(this.nativeListView_programs_ItemActivate);
            this.nativeListView_programs.KeyDown += new System.Windows.Forms.KeyEventHandler(this.nativeListView_KeyDown);
            // 
            // Form_prog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(592, 230);
            this.Controls.Add(this.nativeListView_programs);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form_prog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Program Group";
            this.TransparencyKey = System.Drawing.SystemColors.Control;
            this.Load += new System.EventHandler(this.Form_prog_Load);
            this.ResizeEnd += new System.EventHandler(this.Form_prog_ResizeEnd);
            this.ResumeLayout(false);

        }

        #endregion

        private UV7_Program_Manager.CustomControls.NativeListView nativeListView_programs;
        private System.Windows.Forms.ImageList imageList_32;
    }
}