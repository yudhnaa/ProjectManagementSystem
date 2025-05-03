namespace PresentationLayer.Controls
{
    partial class CtrlTask
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CtrlTask));
            Bunifu.UI.WinForms.BunifuButton.BunifuIconButton.BorderEdges borderEdges1 = new Bunifu.UI.WinForms.BunifuButton.BunifuIconButton.BorderEdges();
            this.lbTitle = new Bunifu.UI.WinForms.BunifuLabel();
            this.pbIcon = new Bunifu.UI.WinForms.BunifuPictureBox();
            this.lbAbstract = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.lbDueDate = new PresentationLayer.CustomControls.RoundedLabel();
            this.lbPriority = new PresentationLayer.CustomControls.RoundedLabel();
            this.lbStatus = new PresentationLayer.CustomControls.RoundedLabel();
            this.label1 = new System.Windows.Forms.Label();
            this.lbDueda = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnEdit = new Bunifu.UI.WinForms.BunifuButton.BunifuIconButton();
            ((System.ComponentModel.ISupportInitialize)(this.pbIcon)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbTitle
            // 
            this.lbTitle.AllowParentOverrides = false;
            this.lbTitle.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lbTitle.AutoEllipsis = false;
            this.lbTitle.AutoSize = false;
            this.lbTitle.AutoSizeHeightOnly = true;
            this.lbTitle.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lbTitle.CursorType = System.Windows.Forms.Cursors.Hand;
            this.lbTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTitle.Location = new System.Drawing.Point(56, 19);
            this.lbTitle.Margin = new System.Windows.Forms.Padding(2);
            this.lbTitle.Name = "lbTitle";
            this.lbTitle.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lbTitle.Size = new System.Drawing.Size(649, 20);
            this.lbTitle.TabIndex = 1;
            this.lbTitle.Text = "Make an Automatic Payment System that enable the design";
            this.lbTitle.TextAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.lbTitle.TextFormat = Bunifu.UI.WinForms.BunifuLabel.TextFormattingOptions.Default;
            this.lbTitle.Click += new System.EventHandler(this.lbTitle_Click);
            // 
            // pbIcon
            // 
            this.pbIcon.AllowFocused = false;
            this.pbIcon.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pbIcon.AutoSizeHeight = true;
            this.pbIcon.BorderRadius = 25;
            this.pbIcon.Image = ((System.Drawing.Image)(resources.GetObject("pbIcon.Image")));
            this.pbIcon.IsCircle = true;
            this.pbIcon.Location = new System.Drawing.Point(2, 33);
            this.pbIcon.Margin = new System.Windows.Forms.Padding(2);
            this.pbIcon.Name = "pbIcon";
            this.tableLayoutPanel1.SetRowSpan(this.pbIcon, 2);
            this.pbIcon.Size = new System.Drawing.Size(50, 50);
            this.pbIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbIcon.TabIndex = 0;
            this.pbIcon.TabStop = false;
            this.pbIcon.Type = Bunifu.UI.WinForms.BunifuPictureBox.Types.Circle;
            // 
            // lbAbstract
            // 
            this.lbAbstract.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lbAbstract.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbAbstract.Location = new System.Drawing.Point(57, 76);
            this.lbAbstract.Name = "lbAbstract";
            this.lbAbstract.Size = new System.Drawing.Size(648, 23);
            this.lbAbstract.TabIndex = 19;
            this.lbAbstract.Text = "#402235 Opened 10 days ago by Yeah Gkod in #Project001";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 6;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.btnEdit, 5, 0);
            this.tableLayoutPanel1.Controls.Add(this.lbTitle, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.pbIcon, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.lbAbstract, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.lbDueDate, 4, 1);
            this.tableLayoutPanel1.Controls.Add(this.lbPriority, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.lbStatus, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.label1, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.lbDueda, 4, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 3, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1221, 117);
            this.tableLayoutPanel1.TabIndex = 20;
            // 
            // lbDueDate
            // 
            this.lbDueDate._BackColor = System.Drawing.Color.Empty;
            this.lbDueDate.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lbDueDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbDueDate.Location = new System.Drawing.Point(968, 70);
            this.lbDueDate.Margin = new System.Windows.Forms.Padding(10);
            this.lbDueDate.Name = "lbDueDate";
            this.lbDueDate.Padding = new System.Windows.Forms.Padding(5, 15, 5, 15);
            this.lbDueDate.Size = new System.Drawing.Size(105, 34);
            this.lbDueDate.TabIndex = 21;
            this.lbDueDate.Text = "roundedLabel2";
            // 
            // lbPriority
            // 
            this.lbPriority._BackColor = System.Drawing.Color.Empty;
            this.lbPriority.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lbPriority.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbPriority.Location = new System.Drawing.Point(843, 70);
            this.lbPriority.Margin = new System.Windows.Forms.Padding(10);
            this.lbPriority.Name = "lbPriority";
            this.lbPriority.Padding = new System.Windows.Forms.Padding(5, 15, 5, 15);
            this.lbPriority.Size = new System.Drawing.Size(105, 34);
            this.lbPriority.TabIndex = 21;
            this.lbPriority.Text = "roundedLabel2";
            // 
            // lbStatus
            // 
            this.lbStatus._BackColor = System.Drawing.Color.Empty;
            this.lbStatus.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lbStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbStatus.Location = new System.Drawing.Point(718, 70);
            this.lbStatus.Margin = new System.Windows.Forms.Padding(10);
            this.lbStatus.Name = "lbStatus";
            this.lbStatus.Padding = new System.Windows.Forms.Padding(5, 15, 5, 15);
            this.lbStatus.Size = new System.Drawing.Size(105, 34);
            this.lbStatus.TabIndex = 20;
            this.lbStatus.Text = "roundedLabel1";
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(742, 20);
            this.label1.Name = "lbComment";
            this.label1.Size = new System.Drawing.Size(56, 18);
            this.label1.TabIndex = 19;
            this.label1.Text = "Status";
            // 
            // lbDueda
            // 
            this.lbDueda.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lbDueda.AutoSize = true;
            this.lbDueda.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbDueda.Location = new System.Drawing.Point(981, 20);
            this.lbDueda.Name = "lbDueda";
            this.lbDueda.Size = new System.Drawing.Size(78, 18);
            this.lbDueda.TabIndex = 19;
            this.lbDueda.Text = "Due Date";
            this.lbDueda.Click += new System.EventHandler(this.lbDueda_Click);
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(864, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 18);
            this.label2.TabIndex = 19;
            this.label2.Text = "Priority";
            this.label2.Click += new System.EventHandler(this.lbDueda_Click);
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
            this.btnEdit.Image = ((System.Drawing.Image)(resources.GetObject("btnEdit.Image")));
            this.btnEdit.ImageMargin = new System.Windows.Forms.Padding(0);
            this.btnEdit.Location = new System.Drawing.Point(1132, 38);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.RoundBorders = true;
            this.tableLayoutPanel1.SetRowSpan(this.btnEdit, 2);
            this.btnEdit.ShowBorders = true;
            this.btnEdit.Size = new System.Drawing.Size(40, 40);
            this.btnEdit.Style = Bunifu.UI.WinForms.BunifuButton.BunifuIconButton.ButtonStyles.Round;
            this.btnEdit.TabIndex = 28;
            this.btnEdit.Click += new System.EventHandler(this.btnUpdateTask_Click);
            // 
            // CtrlTask
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "CtrlTask";
            this.Size = new System.Drawing.Size(1221, 117);
            this.Load += new System.EventHandler(this.ctrl_Task_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbIcon)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Bunifu.UI.WinForms.BunifuLabel lbTitle;
        private Bunifu.UI.WinForms.BunifuPictureBox pbIcon;
        private System.Windows.Forms.Label lbAbstract;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label lbDueda;
        private CustomControls.RoundedLabel lbStatus;
        private CustomControls.RoundedLabel lbPriority;
        private CustomControls.RoundedLabel lbDueDate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private Bunifu.UI.WinForms.BunifuButton.BunifuIconButton btnEdit;
    }
}
