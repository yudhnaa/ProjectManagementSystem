namespace PresentationLayer.UC_SideBar.UC_Project
{
    partial class UC_MyProjects
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.listbxMyProjects = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // listbxMyProjects
            // 
            this.listbxMyProjects.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listbxMyProjects.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listbxMyProjects.FormattingEnabled = true;
            this.listbxMyProjects.Location = new System.Drawing.Point(0, 0);
            this.listbxMyProjects.Name = "listbxMyProjects";
            this.listbxMyProjects.Size = new System.Drawing.Size(207, 226);
            this.listbxMyProjects.TabIndex = 2;
            this.listbxMyProjects.SelectedIndexChanged += new System.EventHandler(this.listbxMyProjects_SelectedIndexChanged);
            // 
            // UC_MyProjects
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.listbxMyProjects);
            this.Name = "UC_MyProjects";
            this.Size = new System.Drawing.Size(207, 226);
            this.Load += new System.EventHandler(this.UC_MyProjects_Load);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ListBox listbxMyProjects;
    }
}
