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
            this.bunifuCustomLabel1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.bunifuCustomLabel1.Name = "bunifuCustomLabel1";
            this.bunifuCustomLabel1.Size = new System.Drawing.Size(276, 32);
            this.bunifuCustomLabel1.TabIndex = 25;
            this.bunifuCustomLabel1.Text = "MY PROJECT";
            this.bunifuCustomLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // listbxMyProjects
            // 
            this.listbxMyProjects.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listbxMyProjects.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listbxMyProjects.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listbxMyProjects.FormattingEnabled = true;
            this.listbxMyProjects.ItemHeight = 20;
            this.listbxMyProjects.Location = new System.Drawing.Point(0, 32);
            this.listbxMyProjects.Margin = new System.Windows.Forms.Padding(4);
            this.listbxMyProjects.Name = "listbxMyProjects";
            this.listbxMyProjects.Size = new System.Drawing.Size(276, 198);
            this.listbxMyProjects.TabIndex = 26;
            this.listbxMyProjects.SelectedIndexChanged += new System.EventHandler(this.listbxMyProjects_SelectedIndexChanged);
            // 
            // CtrlListMyProjects
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.listbxMyProjects);
            this.Controls.Add(this.bunifuCustomLabel1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "CtrlListMyProjects";
            this.Size = new System.Drawing.Size(276, 230);
            this.Load += new System.EventHandler(this.UC_MyProjects_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Bunifu.Framework.UI.BunifuCustomLabel bunifuCustomLabel1;
        private System.Windows.Forms.ListBox listbxMyProjects;
    }
}
