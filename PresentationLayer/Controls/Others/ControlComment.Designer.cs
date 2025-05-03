namespace PresentationLayer.Controls.Others
{
    partial class ControlComment
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
            Bunifu.UI.WinForms.BunifuButton.BunifuIconButton.BorderEdges borderEdges2 = new Bunifu.UI.WinForms.BunifuButton.BunifuIconButton.BorderEdges();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ControlComment));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.btnAvatar = new Bunifu.UI.WinForms.BunifuButton.BunifuIconButton();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.lbUsername = new Bunifu.Framework.UI.BunifuCustomLabel();
            this.lbRole = new PresentationLayer.CustomControls.RoundedLabel();
            this.lbComment = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 99.0099F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 0.990099F));
            this.tableLayoutPanel1.Controls.Add(this.btnAvatar, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.lbComment, 1, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 99.0099F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 0.990099F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(535, 124);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // btnAvatar
            // 
            this.btnAvatar.AllowAnimations = true;
            this.btnAvatar.AllowBorderColorChanges = true;
            this.btnAvatar.AllowMouseEffects = true;
            this.btnAvatar.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnAvatar.AnimationSpeed = 200;
            this.btnAvatar.BackColor = System.Drawing.Color.Transparent;
            this.btnAvatar.BackgroundColor = System.Drawing.Color.DodgerBlue;
            this.btnAvatar.BorderColor = System.Drawing.Color.DodgerBlue;
            this.btnAvatar.BorderRadius = 1;
            this.btnAvatar.BorderStyle = Bunifu.UI.WinForms.BunifuButton.BunifuIconButton.BorderStyles.Solid;
            this.btnAvatar.BorderThickness = 1;
            this.btnAvatar.ColorContrastOnClick = 30;
            this.btnAvatar.ColorContrastOnHover = 30;
            this.btnAvatar.Cursor = System.Windows.Forms.Cursors.Default;
            borderEdges2.BottomLeft = true;
            borderEdges2.BottomRight = true;
            borderEdges2.TopLeft = true;
            borderEdges2.TopRight = true;
            this.btnAvatar.CustomizableEdges = borderEdges2;
            this.btnAvatar.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnAvatar.Image = ((System.Drawing.Image)(resources.GetObject("btnAvatar.Image")));
            this.btnAvatar.ImageMargin = new System.Windows.Forms.Padding(0);
            this.btnAvatar.Location = new System.Drawing.Point(3, 3);
            this.btnAvatar.Name = "btnAvatar";
            this.btnAvatar.RoundBorders = true;
            this.btnAvatar.ShowBorders = true;
            this.btnAvatar.Size = new System.Drawing.Size(40, 40);
            this.btnAvatar.Style = Bunifu.UI.WinForms.BunifuButton.BunifuIconButton.ButtonStyles.Round;
            this.btnAvatar.TabIndex = 25;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.flowLayoutPanel1.AutoSize = true;
            this.flowLayoutPanel1.Controls.Add(this.lbUsername);
            this.flowLayoutPanel1.Controls.Add(this.lbRole);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(46, 1);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(206, 43);
            this.flowLayoutPanel1.TabIndex = 29;
            // 
            // lbUsername
            // 
            this.lbUsername.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lbUsername.AutoSize = true;
            this.lbUsername.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbUsername.Location = new System.Drawing.Point(10, 9);
            this.lbUsername.Margin = new System.Windows.Forms.Padding(10, 5, 5, 5);
            this.lbUsername.Name = "lbUsername";
            this.lbUsername.Size = new System.Drawing.Size(117, 25);
            this.lbUsername.TabIndex = 28;
            this.lbUsername.Text = "@username";
            this.lbUsername.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbRole
            // 
            this.lbRole._BackColor = System.Drawing.Color.Gray;
            this.lbRole._TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbRole.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lbRole.AutoSize = true;
            this.lbRole.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.lbRole.Location = new System.Drawing.Point(137, 10);
            this.lbRole.Margin = new System.Windows.Forms.Padding(5, 10, 10, 10);
            this.lbRole.Name = "lbRole";
            this.lbRole.Padding = new System.Windows.Forms.Padding(5);
            this.lbRole.Size = new System.Drawing.Size(59, 23);
            this.lbRole.TabIndex = 29;
            this.lbRole.Text = "Manager";
            this.lbRole.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbComment
            // 
            this.lbComment.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbComment.Location = new System.Drawing.Point(49, 46);
            this.lbComment.MaximumSize = new System.Drawing.Size(500, 0);
            this.lbComment.Name = "lbComment";
            this.lbComment.Padding = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.lbComment.Size = new System.Drawing.Size(478, 77);
            this.lbComment.TabIndex = 30;
            this.lbComment.Text = resources.GetString("lbComment.Text");
            // 
            // ControlComment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "ControlComment";
            this.Size = new System.Drawing.Size(535, 124);
            this.SizeChanged += new System.EventHandler(this.ControlComment_SizeChanged);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private Bunifu.UI.WinForms.BunifuButton.BunifuIconButton btnAvatar;
        private Bunifu.Framework.UI.BunifuCustomLabel lbUsername;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private CustomControls.RoundedLabel lbRole;
        private System.Windows.Forms.Label lbComment;
    }
}
