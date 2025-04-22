namespace PresentationLayer.UC_SideBar
{
    partial class CtrlPanelTask
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CtrlPanelTask));
            Bunifu.UI.WinForms.BunifuTextBox.StateProperties stateProperties1 = new Bunifu.UI.WinForms.BunifuTextBox.StateProperties();
            Bunifu.UI.WinForms.BunifuTextBox.StateProperties stateProperties2 = new Bunifu.UI.WinForms.BunifuTextBox.StateProperties();
            Bunifu.UI.WinForms.BunifuTextBox.StateProperties stateProperties3 = new Bunifu.UI.WinForms.BunifuTextBox.StateProperties();
            Bunifu.UI.WinForms.BunifuTextBox.StateProperties stateProperties4 = new Bunifu.UI.WinForms.BunifuTextBox.StateProperties();
            Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderEdges borderEdges1 = new Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderEdges();
            this.tbGrid = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.tbSearch = new Bunifu.UI.WinForms.BunifuTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.ddStatus = new Bunifu.UI.WinForms.BunifuDropdown();
            this.btSearch = new Bunifu.UI.WinForms.BunifuButton.BunifuButton();
            this.tbGrid.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbGrid
            // 
            this.tbGrid.ColumnCount = 2;
            this.tbGrid.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tbGrid.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tbGrid.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            this.tbGrid.Controls.Add(this.label1, 0, 0);
            this.tbGrid.Controls.Add(this.tbSearch, 0, 1);
            this.tbGrid.Controls.Add(this.label2, 1, 0);
            this.tbGrid.Controls.Add(this.flowLayoutPanel1, 1, 1);
            this.tbGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbGrid.Location = new System.Drawing.Point(0, 0);
            this.tbGrid.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tbGrid.Name = "tbGrid";
            this.tbGrid.RowCount = 3;
            this.tbGrid.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tbGrid.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tbGrid.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tbGrid.Size = new System.Drawing.Size(1329, 729);
            this.tbGrid.TabIndex = 1;
            this.tbGrid.Paint += new System.Windows.Forms.PaintEventHandler(this.tableLayoutPanel1_Paint);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(13, 12);
            this.label1.Margin = new System.Windows.Forms.Padding(13, 12, 13, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 25);
            this.label1.TabIndex = 4;
            this.label1.Text = "Search";
            // 
            // tbSearch
            // 
            this.tbSearch.AcceptsReturn = false;
            this.tbSearch.AcceptsTab = false;
            this.tbSearch.AnimationSpeed = 200;
            this.tbSearch.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.tbSearch.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.tbSearch.AutoSizeHeight = true;
            this.tbSearch.BackColor = System.Drawing.Color.Transparent;
            this.tbSearch.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tbSearch.BackgroundImage")));
            this.tbSearch.BorderColorActive = System.Drawing.Color.DodgerBlue;
            this.tbSearch.BorderColorDisabled = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.tbSearch.BorderColorHover = System.Drawing.Color.FromArgb(((int)(((byte)(105)))), ((int)(((byte)(181)))), ((int)(((byte)(255)))));
            this.tbSearch.BorderColorIdle = System.Drawing.Color.Silver;
            this.tbSearch.BorderRadius = 1;
            this.tbSearch.BorderThickness = 1;
            this.tbSearch.CharacterCase = Bunifu.UI.WinForms.BunifuTextBox.CharacterCases.Normal;
            this.tbSearch.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.tbSearch.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.tbSearch.DefaultFont = new System.Drawing.Font("Segoe UI", 9.25F);
            this.tbSearch.DefaultText = "";
            this.tbSearch.FillColor = System.Drawing.Color.White;
            this.tbSearch.HideSelection = true;
            this.tbSearch.IconLeft = null;
            this.tbSearch.IconLeftCursor = System.Windows.Forms.Cursors.IBeam;
            this.tbSearch.IconPadding = 10;
            this.tbSearch.IconRight = null;
            this.tbSearch.IconRightCursor = System.Windows.Forms.Cursors.IBeam;
            this.tbSearch.Lines = new string[0];
            this.tbSearch.Location = new System.Drawing.Point(13, 61);
            this.tbSearch.Margin = new System.Windows.Forms.Padding(13, 12, 13, 12);
            this.tbSearch.MaxLength = 32767;
            this.tbSearch.MinimumSize = new System.Drawing.Size(1, 1);
            this.tbSearch.Modified = false;
            this.tbSearch.Multiline = false;
            this.tbSearch.Name = "tbSearch";
            stateProperties1.BorderColor = System.Drawing.Color.DodgerBlue;
            stateProperties1.FillColor = System.Drawing.Color.Empty;
            stateProperties1.ForeColor = System.Drawing.Color.Empty;
            stateProperties1.PlaceholderForeColor = System.Drawing.Color.Empty;
            this.tbSearch.OnActiveState = stateProperties1;
            stateProperties2.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            stateProperties2.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            stateProperties2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            stateProperties2.PlaceholderForeColor = System.Drawing.Color.DarkGray;
            this.tbSearch.OnDisabledState = stateProperties2;
            stateProperties3.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(105)))), ((int)(((byte)(181)))), ((int)(((byte)(255)))));
            stateProperties3.FillColor = System.Drawing.Color.Empty;
            stateProperties3.ForeColor = System.Drawing.Color.Empty;
            stateProperties3.PlaceholderForeColor = System.Drawing.Color.Empty;
            this.tbSearch.OnHoverState = stateProperties3;
            stateProperties4.BorderColor = System.Drawing.Color.Silver;
            stateProperties4.FillColor = System.Drawing.Color.White;
            stateProperties4.ForeColor = System.Drawing.Color.Empty;
            stateProperties4.PlaceholderForeColor = System.Drawing.Color.Empty;
            this.tbSearch.OnIdleState = stateProperties4;
            this.tbSearch.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tbSearch.PasswordChar = '\0';
            this.tbSearch.PlaceholderForeColor = System.Drawing.Color.Silver;
            this.tbSearch.PlaceholderText = "Enter text";
            this.tbSearch.ReadOnly = false;
            this.tbSearch.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.tbSearch.SelectedText = "";
            this.tbSearch.SelectionLength = 0;
            this.tbSearch.SelectionStart = 0;
            this.tbSearch.ShortcutsEnabled = true;
            this.tbSearch.Size = new System.Drawing.Size(773, 48);
            this.tbSearch.Style = Bunifu.UI.WinForms.BunifuTextBox._Style.Bunifu;
            this.tbSearch.TabIndex = 3;
            this.tbSearch.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.tbSearch.TextMarginBottom = 0;
            this.tbSearch.TextMarginLeft = 3;
            this.tbSearch.TextMarginTop = 1;
            this.tbSearch.TextPlaceholder = "Enter text";
            this.tbSearch.UseSystemPasswordChar = false;
            this.tbSearch.WordWrap = true;
            this.tbSearch.TextChanged += new System.EventHandler(this.bunifuTextBox1_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(812, 12);
            this.label2.Margin = new System.Windows.Forms.Padding(13, 12, 13, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(128, 25);
            this.label2.TabIndex = 4;
            this.label2.Text = "Task Status";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.ddStatus);
            this.flowLayoutPanel1.Controls.Add(this.btSearch);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(803, 53);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(522, 69);
            this.flowLayoutPanel1.TabIndex = 5;
            // 
            // ddStatus
            // 
            this.ddStatus.BackColor = System.Drawing.Color.Transparent;
            this.ddStatus.BackgroundColor = System.Drawing.Color.White;
            this.ddStatus.BorderColor = System.Drawing.Color.Silver;
            this.ddStatus.BorderRadius = 1;
            this.ddStatus.Color = System.Drawing.Color.Silver;
            this.ddStatus.Direction = Bunifu.UI.WinForms.BunifuDropdown.Directions.Down;
            this.ddStatus.DisabledBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.ddStatus.DisabledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.ddStatus.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.ddStatus.DisabledForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.ddStatus.DisabledIndicatorColor = System.Drawing.Color.DarkGray;
            this.ddStatus.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.ddStatus.DropdownBorderThickness = Bunifu.UI.WinForms.BunifuDropdown.BorderThickness.Thin;
            this.ddStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddStatus.DropDownTextAlign = Bunifu.UI.WinForms.BunifuDropdown.TextAlign.Left;
            this.ddStatus.FillDropDown = true;
            this.ddStatus.FillIndicator = false;
            this.ddStatus.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ddStatus.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.ddStatus.ForeColor = System.Drawing.Color.Black;
            this.ddStatus.FormattingEnabled = true;
            this.ddStatus.Icon = null;
            this.ddStatus.IndicatorAlignment = Bunifu.UI.WinForms.BunifuDropdown.Indicator.Right;
            this.ddStatus.IndicatorColor = System.Drawing.Color.DarkGray;
            this.ddStatus.IndicatorLocation = Bunifu.UI.WinForms.BunifuDropdown.Indicator.Right;
            this.ddStatus.IndicatorThickness = 2;
            this.ddStatus.IsDropdownOpened = false;
            this.ddStatus.ItemBackColor = System.Drawing.Color.White;
            this.ddStatus.ItemBorderColor = System.Drawing.Color.White;
            this.ddStatus.ItemForeColor = System.Drawing.Color.Black;
            this.ddStatus.ItemHeight = 26;
            this.ddStatus.ItemHighLightColor = System.Drawing.Color.DodgerBlue;
            this.ddStatus.ItemHighLightForeColor = System.Drawing.Color.White;
            this.ddStatus.ItemTopMargin = 3;
            this.ddStatus.Location = new System.Drawing.Point(13, 12);
            this.ddStatus.Margin = new System.Windows.Forms.Padding(13, 12, 13, 12);
            this.ddStatus.Name = "ddStatus";
            this.ddStatus.Size = new System.Drawing.Size(331, 32);
            this.ddStatus.TabIndex = 2;
            this.ddStatus.Text = null;
            this.ddStatus.TextAlignment = Bunifu.UI.WinForms.BunifuDropdown.TextAlign.Left;
            this.ddStatus.TextLeftMargin = 5;
            // 
            // btSearch
            // 
            this.btSearch.AllowAnimations = true;
            this.btSearch.AllowMouseEffects = true;
            this.btSearch.AllowToggling = false;
            this.btSearch.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btSearch.AnimationSpeed = 200;
            this.btSearch.AutoGenerateColors = false;
            this.btSearch.AutoRoundBorders = false;
            this.btSearch.AutoSizeLeftIcon = true;
            this.btSearch.AutoSizeRightIcon = true;
            this.btSearch.BackColor = System.Drawing.Color.Transparent;
            this.btSearch.BackColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(122)))), ((int)(((byte)(183)))));
            this.btSearch.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btSearch.BackgroundImage")));
            this.btSearch.BorderStyle = Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderStyles.Solid;
            this.btSearch.ButtonText = "Search";
            this.btSearch.ButtonTextMarginLeft = 0;
            this.btSearch.ColorContrastOnClick = 45;
            this.btSearch.ColorContrastOnHover = 45;
            this.btSearch.Cursor = System.Windows.Forms.Cursors.Default;
            borderEdges1.BottomLeft = true;
            borderEdges1.BottomRight = true;
            borderEdges1.TopLeft = true;
            borderEdges1.TopRight = true;
            this.btSearch.CustomizableEdges = borderEdges1;
            this.btSearch.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btSearch.DisabledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(191)))), ((int)(((byte)(191)))));
            this.btSearch.DisabledFillColor = System.Drawing.Color.Empty;
            this.btSearch.DisabledForecolor = System.Drawing.Color.Empty;
            this.btSearch.FocusState = Bunifu.UI.WinForms.BunifuButton.BunifuButton.ButtonStates.Pressed;
            this.btSearch.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btSearch.ForeColor = System.Drawing.Color.White;
            this.btSearch.IconLeft = null;
            this.btSearch.IconLeftAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btSearch.IconLeftCursor = System.Windows.Forms.Cursors.Default;
            this.btSearch.IconLeftPadding = new System.Windows.Forms.Padding(11, 3, 3, 3);
            this.btSearch.IconMarginLeft = 11;
            this.btSearch.IconPadding = 10;
            this.btSearch.IconRight = null;
            this.btSearch.IconRightAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btSearch.IconRightCursor = System.Windows.Forms.Cursors.Default;
            this.btSearch.IconRightPadding = new System.Windows.Forms.Padding(3, 3, 7, 3);
            this.btSearch.IconSize = 25;
            this.btSearch.IdleBorderColor = System.Drawing.Color.Empty;
            this.btSearch.IdleBorderRadius = 0;
            this.btSearch.IdleBorderThickness = 0;
            this.btSearch.IdleFillColor = System.Drawing.Color.Empty;
            this.btSearch.IdleIconLeftImage = null;
            this.btSearch.IdleIconRightImage = null;
            this.btSearch.IndicateFocus = false;
            this.btSearch.Location = new System.Drawing.Point(361, 4);
            this.btSearch.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btSearch.Name = "btSearch";
            this.btSearch.OnDisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(191)))), ((int)(((byte)(191)))));
            this.btSearch.OnDisabledState.BorderRadius = 20;
            this.btSearch.OnDisabledState.BorderStyle = Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderStyles.Solid;
            this.btSearch.OnDisabledState.BorderThickness = 1;
            this.btSearch.OnDisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.btSearch.OnDisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(160)))), ((int)(((byte)(168)))));
            this.btSearch.OnDisabledState.IconLeftImage = null;
            this.btSearch.OnDisabledState.IconRightImage = null;
            this.btSearch.onHoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(150)))), ((int)(((byte)(255)))));
            this.btSearch.onHoverState.BorderRadius = 20;
            this.btSearch.onHoverState.BorderStyle = Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderStyles.Solid;
            this.btSearch.onHoverState.BorderThickness = 1;
            this.btSearch.onHoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(150)))), ((int)(((byte)(255)))));
            this.btSearch.onHoverState.ForeColor = System.Drawing.Color.White;
            this.btSearch.onHoverState.IconLeftImage = null;
            this.btSearch.onHoverState.IconRightImage = null;
            this.btSearch.OnIdleState.BorderColor = System.Drawing.Color.DodgerBlue;
            this.btSearch.OnIdleState.BorderRadius = 20;
            this.btSearch.OnIdleState.BorderStyle = Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderStyles.Solid;
            this.btSearch.OnIdleState.BorderThickness = 1;
            this.btSearch.OnIdleState.FillColor = System.Drawing.Color.DodgerBlue;
            this.btSearch.OnIdleState.ForeColor = System.Drawing.Color.White;
            this.btSearch.OnIdleState.IconLeftImage = null;
            this.btSearch.OnIdleState.IconRightImage = null;
            this.btSearch.OnPressedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(96)))), ((int)(((byte)(144)))));
            this.btSearch.OnPressedState.BorderRadius = 20;
            this.btSearch.OnPressedState.BorderStyle = Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderStyles.Solid;
            this.btSearch.OnPressedState.BorderThickness = 1;
            this.btSearch.OnPressedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(96)))), ((int)(((byte)(144)))));
            this.btSearch.OnPressedState.ForeColor = System.Drawing.Color.White;
            this.btSearch.OnPressedState.IconLeftImage = null;
            this.btSearch.OnPressedState.IconRightImage = null;
            this.btSearch.Size = new System.Drawing.Size(136, 48);
            this.btSearch.TabIndex = 3;
            this.btSearch.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btSearch.TextAlignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.btSearch.TextMarginLeft = 0;
            this.btSearch.TextPadding = new System.Windows.Forms.Padding(0);
            this.btSearch.UseDefaultRadiusAndThickness = true;
            this.btSearch.Click += new System.EventHandler(this.btSearch_Click);
            // 
            // CtrlPanelTask
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.tbGrid);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "CtrlPanelTask";
            this.Size = new System.Drawing.Size(1329, 729);
            this.Load += new System.EventHandler(this.UC_Task_Load_1);
            this.tbGrid.ResumeLayout(false);
            this.tbGrid.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TableLayoutPanel tbGrid;
        private Bunifu.UI.WinForms.BunifuTextBox tbSearch;
        private Bunifu.UI.WinForms.BunifuDropdown ddStatus;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private Bunifu.UI.WinForms.BunifuButton.BunifuButton btSearch;
    }
}
