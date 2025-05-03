namespace PresentationLayer.UC_SideBar.UC_Project
{
    partial class CtrlListMyProjects
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
            this.bunifuCustomLabel1 = new Bunifu.Framework.UI.BunifuCustomLabel();
            this.listbxMyProjects = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // bunifuCustomLabel1
            // 
            this.bunifuCustomLabel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.bunifuCustomLabel1.Font = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bunifuCustomLabel1.Location = new System.Drawing.Point(0, 0);
            this.bunifuCustomLabel1.Name = "bunifuCustomLabel1";
            this.bunifuCustomLabel1.Size = new System.Drawing.Size(207, 26);
            this.bunifuCustomLabel1.TabIndex = 25;
            this.bunifuCustomLabel1.Text = "My Projects";
            this.bunifuCustomLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // listbxMyProjects
            // 
            this.listbxMyProjects.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listbxMyProjects.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listbxMyProjects.FormattingEnabled = true;
            this.listbxMyProjects.Location = new System.Drawing.Point(0, 26);
            this.listbxMyProjects.Name = "listbxMyProjects";
            this.listbxMyProjects.Size = new System.Drawing.Size(207, 161);
            this.listbxMyProjects.TabIndex = 26;
            this.listbxMyProjects.SelectedIndexChanged += new System.EventHandler(this.listbxMyProjects_SelectedIndexChanged);
            // 
            // CtrlListMyProjects
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.listbxMyProjects);
            this.Controls.Add(this.bunifuCustomLabel1);
            this.Name = "CtrlListMyProjects";
            this.Size = new System.Drawing.Size(207, 187);
            this.Load += new System.EventHandler(this.UC_MyProjects_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Bunifu.Framework.UI.BunifuCustomLabel bunifuCustomLabel1;
        private System.Windows.Forms.ListBox listbxMyProjects;
    }
}
