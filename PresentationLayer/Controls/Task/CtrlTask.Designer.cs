namespace PresentationLayer.Control
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
            Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderEdges borderEdges1 = new Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderEdges();
            this.lbTitle = new Bunifu.UI.WinForms.BunifuLabel();
            this.pbIcon = new Bunifu.UI.WinForms.BunifuPictureBox();
            this.lbAbstract = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.lbDueda = new System.Windows.Forms.Label();
            this.btnUpdateTask = new Bunifu.UI.WinForms.BunifuButton.BunifuButton();
            this.lbStatus = new PresentationLayer.CustomControls.RoundedLabel();
            this.lbPriority = new PresentationLayer.CustomControls.RoundedLabel();
            this.lbDueDate = new PresentationLayer.CustomControls.RoundedLabel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
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
            this.tableLayoutPanel1.Controls.Add(this.lbTitle, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.pbIcon, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.lbAbstract, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.btnUpdateTask, 5, 1);
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
            // btnUpdateTask
            // 
            this.btnUpdateTask.AllowAnimations = true;
            this.btnUpdateTask.AllowMouseEffects = true;
            this.btnUpdateTask.AllowToggling = false;
            this.btnUpdateTask.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnUpdateTask.AnimationSpeed = 200;
            this.btnUpdateTask.AutoGenerateColors = false;
            this.btnUpdateTask.AutoRoundBorders = false;
            this.btnUpdateTask.AutoSizeLeftIcon = true;
            this.btnUpdateTask.AutoSizeRightIcon = true;
            this.btnUpdateTask.BackColor = System.Drawing.Color.Transparent;
            this.btnUpdateTask.BackColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(122)))), ((int)(((byte)(183)))));
            this.btnUpdateTask.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnUpdateTask.BackgroundImage")));
            this.btnUpdateTask.BorderStyle = Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderStyles.Solid;
            this.btnUpdateTask.ButtonText = "Update";
            this.btnUpdateTask.ButtonTextMarginLeft = 0;
            this.btnUpdateTask.ColorContrastOnClick = 45;
            this.btnUpdateTask.ColorContrastOnHover = 45;
            this.btnUpdateTask.Cursor = System.Windows.Forms.Cursors.Default;
            borderEdges1.BottomLeft = true;
            borderEdges1.BottomRight = true;
            borderEdges1.TopLeft = true;
            borderEdges1.TopRight = true;
            this.btnUpdateTask.CustomizableEdges = borderEdges1;
            this.btnUpdateTask.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnUpdateTask.DisabledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(191)))), ((int)(((byte)(191)))));
            this.btnUpdateTask.DisabledFillColor = System.Drawing.Color.Empty;
            this.btnUpdateTask.DisabledForecolor = System.Drawing.Color.Empty;
            this.btnUpdateTask.FocusState = Bunifu.UI.WinForms.BunifuButton.BunifuButton.ButtonStates.Pressed;
            this.btnUpdateTask.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnUpdateTask.ForeColor = System.Drawing.Color.White;
            this.btnUpdateTask.IconLeft = null;
            this.btnUpdateTask.IconLeftAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnUpdateTask.IconLeftCursor = System.Windows.Forms.Cursors.Default;
            this.btnUpdateTask.IconLeftPadding = new System.Windows.Forms.Padding(11, 3, 3, 3);
            this.btnUpdateTask.IconMarginLeft = 11;
            this.btnUpdateTask.IconPadding = 10;
            this.btnUpdateTask.IconRight = null;
            this.btnUpdateTask.IconRightAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnUpdateTask.IconRightCursor = System.Windows.Forms.Cursors.Default;
            this.btnUpdateTask.IconRightPadding = new System.Windows.Forms.Padding(3, 3, 7, 3);
            this.btnUpdateTask.IconSize = 25;
            this.btnUpdateTask.IdleBorderColor = System.Drawing.Color.Empty;
            this.btnUpdateTask.IdleBorderRadius = 0;
            this.btnUpdateTask.IdleBorderThickness = 0;
            this.btnUpdateTask.IdleFillColor = System.Drawing.Color.Empty;
            this.btnUpdateTask.IdleIconLeftImage = null;
            this.btnUpdateTask.IdleIconRightImage = null;
            this.btnUpdateTask.IndicateFocus = false;
            this.btnUpdateTask.Location = new System.Drawing.Point(1093, 68);
            this.btnUpdateTask.Name = "btnUpdateTask";
            this.btnUpdateTask.OnDisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(191)))), ((int)(((byte)(191)))));
            this.btnUpdateTask.OnDisabledState.BorderRadius = 20;
            this.btnUpdateTask.OnDisabledState.BorderStyle = Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderStyles.Solid;
            this.btnUpdateTask.OnDisabledState.BorderThickness = 1;
            this.btnUpdateTask.OnDisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.btnUpdateTask.OnDisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(160)))), ((int)(((byte)(168)))));
            this.btnUpdateTask.OnDisabledState.IconLeftImage = null;
            this.btnUpdateTask.OnDisabledState.IconRightImage = null;
            this.btnUpdateTask.onHoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(150)))), ((int)(((byte)(255)))));
            this.btnUpdateTask.onHoverState.BorderRadius = 20;
            this.btnUpdateTask.onHoverState.BorderStyle = Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderStyles.Solid;
            this.btnUpdateTask.onHoverState.BorderThickness = 1;
            this.btnUpdateTask.onHoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(150)))), ((int)(((byte)(255)))));
            this.btnUpdateTask.onHoverState.ForeColor = System.Drawing.Color.White;
            this.btnUpdateTask.onHoverState.IconLeftImage = null;
            this.btnUpdateTask.onHoverState.IconRightImage = null;
            this.btnUpdateTask.OnIdleState.BorderColor = System.Drawing.Color.DodgerBlue;
            this.btnUpdateTask.OnIdleState.BorderRadius = 20;
            this.btnUpdateTask.OnIdleState.BorderStyle = Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderStyles.Solid;
            this.btnUpdateTask.OnIdleState.BorderThickness = 1;
            this.btnUpdateTask.OnIdleState.FillColor = System.Drawing.Color.DodgerBlue;
            this.btnUpdateTask.OnIdleState.ForeColor = System.Drawing.Color.White;
            this.btnUpdateTask.OnIdleState.IconLeftImage = null;
            this.btnUpdateTask.OnIdleState.IconRightImage = null;
            this.btnUpdateTask.OnPressedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(96)))), ((int)(((byte)(144)))));
            this.btnUpdateTask.OnPressedState.BorderRadius = 20;
            this.btnUpdateTask.OnPressedState.BorderStyle = Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderStyles.Solid;
            this.btnUpdateTask.OnPressedState.BorderThickness = 1;
            this.btnUpdateTask.OnPressedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(96)))), ((int)(((byte)(144)))));
            this.btnUpdateTask.OnPressedState.ForeColor = System.Drawing.Color.White;
            this.btnUpdateTask.OnPressedState.IconLeftImage = null;
            this.btnUpdateTask.OnPressedState.IconRightImage = null;
            this.btnUpdateTask.Size = new System.Drawing.Size(118, 39);
            this.btnUpdateTask.TabIndex = 22;
            this.btnUpdateTask.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnUpdateTask.TextAlignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.btnUpdateTask.TextMarginLeft = 0;
            this.btnUpdateTask.TextPadding = new System.Windows.Forms.Padding(0);
            this.btnUpdateTask.UseDefaultRadiusAndThickness = true;
            this.btnUpdateTask.Click += new System.EventHandler(this.btnUpdateTask_Click);
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
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(742, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 18);
            this.label1.TabIndex = 19;
            this.label1.Text = "Status";
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
        private Bunifu.UI.WinForms.BunifuButton.BunifuButton btnUpdateTask;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}
