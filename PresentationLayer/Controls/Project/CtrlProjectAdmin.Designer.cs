namespace PresentationLayer.Controls.Project
{
    partial class CtrlProjectAdmin
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CtrlProjectAdmin));
            Bunifu.UI.WinForms.BunifuButton.BunifuIconButton.BorderEdges borderEdges2 = new Bunifu.UI.WinForms.BunifuButton.BunifuIconButton.BorderEdges();
            Bunifu.UI.WinForms.BunifuButton.BunifuIconButton.BorderEdges borderEdges1 = new Bunifu.UI.WinForms.BunifuButton.BunifuIconButton.BorderEdges();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.lbTitle = new System.Windows.Forms.Label();
            this.Status = new System.Windows.Forms.Label();
            this.tbDescription = new System.Windows.Forms.TextBox();
            this.pbIcon = new Bunifu.UI.WinForms.BunifuPictureBox();
            this.lbEndDate = new System.Windows.Forms.Label();
            this.lbTaskCount = new System.Windows.Forms.Label();
            this.btnEdit = new Bunifu.UI.WinForms.BunifuButton.BunifuIconButton();
            this.btnDelete = new Bunifu.UI.WinForms.BunifuButton.BunifuIconButton();
            this.lbStatus = new PresentationLayer.CustomControls.RoundedLabel();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbIcon)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 5;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.lbTitle, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.Status, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.tbDescription, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.pbIcon, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.lbStatus, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.lbEndDate, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.lbTaskCount, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.btnDelete, 3, 2);
            this.tableLayoutPanel1.Controls.Add(this.btnEdit, 3, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 44F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1031, 149);
            this.tableLayoutPanel1.TabIndex = 21;
            // 
            // lbTitle
            // 
            this.lbTitle.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lbTitle.AutoSize = true;
            this.lbTitle.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lbTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTitle.Location = new System.Drawing.Point(57, 6);
            this.lbTitle.Name = "lbTitle";
            this.lbTitle.Size = new System.Drawing.Size(94, 18);
            this.lbTitle.TabIndex = 23;
            this.lbTitle.Text = "Project 001";
            this.lbTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lbTitle.Click += new System.EventHandler(this.lbTitle_Click);
            // 
            // Status
            // 
            this.Status.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.Status.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Status.Location = new System.Drawing.Point(826, 0);
            this.Status.Name = "Status";
            this.Status.Size = new System.Drawing.Size(147, 31);
            this.Status.TabIndex = 19;
            this.Status.Text = "Status";
            this.Status.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tbDescription
            // 
            this.tbDescription.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tbDescription.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbDescription.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbDescription.Enabled = false;
            this.tbDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbDescription.Location = new System.Drawing.Point(64, 41);
            this.tbDescription.Margin = new System.Windows.Forms.Padding(10);
            this.tbDescription.Multiline = true;
            this.tbDescription.Name = "tbDescription";
            this.tbDescription.Size = new System.Drawing.Size(749, 53);
            this.tbDescription.TabIndex = 20;
            this.tbDescription.Text = resources.GetString("tbDescription.Text");
            // 
            // pbIcon
            // 
            this.pbIcon.AllowFocused = false;
            this.pbIcon.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pbIcon.AutoSizeHeight = true;
            this.pbIcon.BorderRadius = 25;
            this.pbIcon.Image = ((System.Drawing.Image)(resources.GetObject("pbIcon.Image")));
            this.pbIcon.IsCircle = true;
            this.pbIcon.Location = new System.Drawing.Point(2, 27);
            this.pbIcon.Margin = new System.Windows.Forms.Padding(2);
            this.pbIcon.Name = "pbIcon";
            this.tableLayoutPanel1.SetRowSpan(this.pbIcon, 2);
            this.pbIcon.Size = new System.Drawing.Size(50, 50);
            this.pbIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbIcon.TabIndex = 0;
            this.pbIcon.TabStop = false;
            this.pbIcon.Type = Bunifu.UI.WinForms.BunifuPictureBox.Types.Circle;
            // 
            // lbEndDate
            // 
            this.lbEndDate.AutoSize = true;
            this.lbEndDate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbEndDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbEndDate.ForeColor = System.Drawing.Color.IndianRed;
            this.lbEndDate.Location = new System.Drawing.Point(57, 104);
            this.lbEndDate.Name = "lbEndDate";
            this.lbEndDate.Size = new System.Drawing.Size(763, 45);
            this.lbEndDate.TabIndex = 22;
            this.lbEndDate.Text = "EndDate: 1/1/2024";
            this.lbEndDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbTaskCount
            // 
            this.lbTaskCount.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lbTaskCount.AutoSize = true;
            this.lbTaskCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTaskCount.ForeColor = System.Drawing.Color.IndianRed;
            this.lbTaskCount.Location = new System.Drawing.Point(856, 116);
            this.lbTaskCount.Name = "lbTaskCount";
            this.lbTaskCount.Size = new System.Drawing.Size(87, 20);
            this.lbTaskCount.TabIndex = 22;
            this.lbTaskCount.Text = "100 tasks";
            this.lbTaskCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnEdit
            // 
            this.btnEdit.AllowAnimations = true;
            this.btnEdit.AllowBorderColorChanges = true;
            this.btnEdit.AllowMouseEffects = true;
            this.btnEdit.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnEdit.AnimationSpeed = 200;
            this.btnEdit.BackColor = System.Drawing.Color.Transparent;
            this.btnEdit.BackgroundColor = System.Drawing.Color.Transparent;
            this.btnEdit.BorderColor = System.Drawing.Color.Transparent;
            this.btnEdit.BorderRadius = 1;
            this.btnEdit.BorderStyle = Bunifu.UI.WinForms.BunifuButton.BunifuIconButton.BorderStyles.Solid;
            this.btnEdit.BorderThickness = 1;
            this.btnEdit.ColorContrastOnClick = 30;
            this.btnEdit.ColorContrastOnHover = 30;
            this.btnEdit.Cursor = System.Windows.Forms.Cursors.Default;
            borderEdges2.BottomLeft = true;
            borderEdges2.BottomRight = true;
            borderEdges2.TopLeft = true;
            borderEdges2.TopRight = true;
            this.btnEdit.CustomizableEdges = borderEdges2;
            this.btnEdit.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnEdit.Image = global::PresentationLayer.Properties.Resources.edit;
            this.btnEdit.ImageMargin = new System.Windows.Forms.Padding(0);
            this.btnEdit.Location = new System.Drawing.Point(979, 47);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.RoundBorders = true;
            this.btnEdit.ShowBorders = true;
            this.btnEdit.Size = new System.Drawing.Size(40, 40);
            this.btnEdit.Style = Bunifu.UI.WinForms.BunifuButton.BunifuIconButton.ButtonStyles.Round;
            this.btnEdit.TabIndex = 24;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.AllowAnimations = true;
            this.btnDelete.AllowBorderColorChanges = true;
            this.btnDelete.AllowMouseEffects = true;
            this.btnDelete.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnDelete.AnimationSpeed = 200;
            this.btnDelete.BackColor = System.Drawing.Color.Transparent;
            this.btnDelete.BackgroundColor = System.Drawing.Color.Transparent;
            this.btnDelete.BorderColor = System.Drawing.Color.Transparent;
            this.btnDelete.BorderRadius = 1;
            this.btnDelete.BorderStyle = Bunifu.UI.WinForms.BunifuButton.BunifuIconButton.BorderStyles.Solid;
            this.btnDelete.BorderThickness = 1;
            this.btnDelete.ColorContrastOnClick = 30;
            this.btnDelete.ColorContrastOnHover = 30;
            this.btnDelete.Cursor = System.Windows.Forms.Cursors.Default;
            borderEdges1.BottomLeft = true;
            borderEdges1.BottomRight = true;
            borderEdges1.TopLeft = true;
            borderEdges1.TopRight = true;
            this.btnDelete.CustomizableEdges = borderEdges1;
            this.btnDelete.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnDelete.Image = global::PresentationLayer.Properties.Resources.trash;
            this.btnDelete.ImageMargin = new System.Windows.Forms.Padding(0);
            this.btnDelete.Location = new System.Drawing.Point(979, 107);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.RoundBorders = true;
            this.btnDelete.ShowBorders = true;
            this.btnDelete.Size = new System.Drawing.Size(40, 40);
            this.btnDelete.Style = Bunifu.UI.WinForms.BunifuButton.BunifuIconButton.ButtonStyles.Round;
            this.btnDelete.TabIndex = 24;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // lbStatus
            // 
            this.lbStatus._BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.lbStatus.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lbStatus.BackColor = System.Drawing.Color.White;
            this.lbStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbStatus.Location = new System.Drawing.Point(849, 56);
            this.lbStatus.Name = "lbStatus";
            this.lbStatus.Size = new System.Drawing.Size(100, 23);
            this.lbStatus.TabIndex = 21;
            this.lbStatus.Text = "Complete";
            // 
            // CtrlProjectAdmin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "CtrlProjectAdmin";
            this.Size = new System.Drawing.Size(1031, 149);
            this.Load += new System.EventHandler(this.CtrlProject_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbIcon)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private Bunifu.UI.WinForms.BunifuPictureBox pbIcon;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label Status;
        private System.Windows.Forms.TextBox tbDescription;
        private System.Windows.Forms.Label lbEndDate;
        private System.Windows.Forms.Label lbTaskCount;
        private System.Windows.Forms.Label lbTitle;
        private CustomControls.RoundedLabel lbStatus;
        private Bunifu.UI.WinForms.BunifuButton.BunifuIconButton btnEdit;
        private Bunifu.UI.WinForms.BunifuButton.BunifuIconButton btnDelete;
    }
}
