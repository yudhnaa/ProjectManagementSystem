namespace PresentationLayer.Control
{
    partial class CtrlUserAdmin
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CtrlUserAdmin));
            Bunifu.UI.WinForms.BunifuButton.BunifuIconButton.BorderEdges borderEdges1 = new Bunifu.UI.WinForms.BunifuButton.BunifuIconButton.BorderEdges();
            Bunifu.UI.WinForms.BunifuButton.BunifuIconButton.BorderEdges borderEdges2 = new Bunifu.UI.WinForms.BunifuButton.BunifuIconButton.BorderEdges();
            this.lbUsername = new Bunifu.UI.WinForms.BunifuLabel();
            this.lbName = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.pbIcon = new Bunifu.UI.WinForms.BunifuPictureBox();
            this.lbDepartment = new PresentationLayer.CustomControls.RoundedLabel();
            this.lbRoleName = new PresentationLayer.CustomControls.RoundedLabel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnEdit = new Bunifu.UI.WinForms.BunifuButton.BunifuIconButton();
            this.btnDelete = new Bunifu.UI.WinForms.BunifuButton.BunifuIconButton();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbIcon)).BeginInit();
            this.SuspendLayout();
            // 
            // lbUsername
            // 
            this.lbUsername.AllowParentOverrides = false;
            this.lbUsername.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lbUsername.AutoEllipsis = false;
            this.lbUsername.AutoSize = false;
            this.lbUsername.AutoSizeHeightOnly = true;
            this.lbUsername.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lbUsername.CursorType = System.Windows.Forms.Cursors.Hand;
            this.lbUsername.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbUsername.Location = new System.Drawing.Point(56, 9);
            this.lbUsername.Margin = new System.Windows.Forms.Padding(2);
            this.lbUsername.Name = "lbUsername";
            this.lbUsername.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lbUsername.Size = new System.Drawing.Size(556, 20);
            this.lbUsername.TabIndex = 1;
            this.lbUsername.Text = "Username: Awhite";
            this.lbUsername.TextAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.lbUsername.TextFormat = Bunifu.UI.WinForms.BunifuLabel.TextFormattingOptions.Default;
            // 
            // lbName
            // 
            this.lbName.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lbName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbName.Location = new System.Drawing.Point(57, 52);
            this.lbName.Name = "lbName";
            this.lbName.Size = new System.Drawing.Size(555, 14);
            this.lbName.TabIndex = 19;
            this.lbName.Text = "Name: ABC Awhite";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 5;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Controls.Add(this.pbIcon, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.lbName, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.lbDepartment, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.lbRoleName, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.label1, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnEdit, 4, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnDelete, 4, 1);
            this.tableLayoutPanel1.Controls.Add(this.lbUsername, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(881, 83);
            this.tableLayoutPanel1.TabIndex = 20;
            // 
            // pbIcon
            // 
            this.pbIcon.AllowFocused = false;
            this.pbIcon.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pbIcon.AutoSizeHeight = true;
            this.pbIcon.BorderRadius = 25;
            this.pbIcon.Image = global::PresentationLayer.Properties.Resources.user;
            this.pbIcon.IsCircle = true;
            this.pbIcon.Location = new System.Drawing.Point(2, 15);
            this.pbIcon.Margin = new System.Windows.Forms.Padding(2);
            this.pbIcon.Name = "pbIcon";
            this.tableLayoutPanel1.SetRowSpan(this.pbIcon, 2);
            this.pbIcon.Size = new System.Drawing.Size(50, 50);
            this.pbIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbIcon.TabIndex = 0;
            this.pbIcon.TabStop = false;
            this.pbIcon.Type = Bunifu.UI.WinForms.BunifuPictureBox.Types.Circle;
            // 
            // lbDepartment
            // 
            this.lbDepartment._BackColor = System.Drawing.Color.Empty;
            this.lbDepartment.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lbDepartment.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbDepartment.Location = new System.Drawing.Point(729, 50);
            this.lbDepartment.Margin = new System.Windows.Forms.Padding(10);
            this.lbDepartment.Name = "lbDepartment";
            this.lbDepartment.Padding = new System.Windows.Forms.Padding(5, 15, 5, 15);
            this.lbDepartment.Size = new System.Drawing.Size(84, 18);
            this.lbDepartment.TabIndex = 21;
            this.lbDepartment.Text = "Deparment A";
            // 
            // lbRoleName
            // 
            this.lbRoleName._BackColor = System.Drawing.Color.Empty;
            this.lbRoleName.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lbRoleName.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbRoleName.Location = new System.Drawing.Point(625, 49);
            this.lbRoleName.Margin = new System.Windows.Forms.Padding(10);
            this.lbRoleName.Name = "lbRoleName";
            this.lbRoleName.Padding = new System.Windows.Forms.Padding(5, 15, 5, 15);
            this.lbRoleName.Size = new System.Drawing.Size(84, 21);
            this.lbRoleName.TabIndex = 20;
            this.lbRoleName.Text = "Admin";
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(645, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 18);
            this.label1.TabIndex = 19;
            this.label1.Text = "Role";
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(723, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(95, 18);
            this.label2.TabIndex = 19;
            this.label2.Text = "Department";
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
            this.btnEdit.Cursor = System.Windows.Forms.Cursors.Hand;
            borderEdges1.BottomLeft = true;
            borderEdges1.BottomRight = true;
            borderEdges1.TopLeft = true;
            borderEdges1.TopRight = true;
            this.btnEdit.CustomizableEdges = borderEdges1;
            this.btnEdit.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnEdit.Image = global::PresentationLayer.Properties.Resources.edit;
            this.btnEdit.ImageMargin = new System.Windows.Forms.Padding(0);
            this.btnEdit.Location = new System.Drawing.Point(835, 3);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.RoundBorders = true;
            this.btnEdit.ShowBorders = true;
            this.btnEdit.Size = new System.Drawing.Size(33, 33);
            this.btnEdit.Style = Bunifu.UI.WinForms.BunifuButton.BunifuIconButton.ButtonStyles.Round;
            this.btnEdit.TabIndex = 26;
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
            this.btnDelete.Cursor = System.Windows.Forms.Cursors.Hand;
            borderEdges2.BottomLeft = true;
            borderEdges2.BottomRight = true;
            borderEdges2.TopLeft = true;
            borderEdges2.TopRight = true;
            this.btnDelete.CustomizableEdges = borderEdges2;
            this.btnDelete.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnDelete.Image = global::PresentationLayer.Properties.Resources.trash;
            this.btnDelete.ImageMargin = new System.Windows.Forms.Padding(0);
            this.btnDelete.Location = new System.Drawing.Point(835, 42);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.RoundBorders = true;
            this.btnDelete.ShowBorders = true;
            this.btnDelete.Size = new System.Drawing.Size(34, 34);
            this.btnDelete.Style = Bunifu.UI.WinForms.BunifuButton.BunifuIconButton.ButtonStyles.Round;
            this.btnDelete.TabIndex = 25;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // CtrlUserAdmin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "CtrlUserAdmin";
            this.Size = new System.Drawing.Size(881, 83);
            this.Load += new System.EventHandler(this.ctrl_User_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbIcon)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Bunifu.UI.WinForms.BunifuLabel lbUsername;
        private Bunifu.UI.WinForms.BunifuPictureBox pbIcon;
        private System.Windows.Forms.Label lbName;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private CustomControls.RoundedLabel lbRoleName;
        private CustomControls.RoundedLabel lbDepartment;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private Bunifu.UI.WinForms.BunifuButton.BunifuIconButton btnDelete;
        private Bunifu.UI.WinForms.BunifuButton.BunifuIconButton btnEdit;
    }
}
