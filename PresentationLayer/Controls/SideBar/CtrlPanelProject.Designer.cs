namespace PresentationLayer.Controls.Project
{
    partial class CtrlPanelProject
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CtrlPanelProject));
            Bunifu.UI.WinForms.BunifuTextBox.StateProperties stateProperties1 = new Bunifu.UI.WinForms.BunifuTextBox.StateProperties();
            Bunifu.UI.WinForms.BunifuTextBox.StateProperties stateProperties2 = new Bunifu.UI.WinForms.BunifuTextBox.StateProperties();
            Bunifu.UI.WinForms.BunifuTextBox.StateProperties stateProperties3 = new Bunifu.UI.WinForms.BunifuTextBox.StateProperties();
            Bunifu.UI.WinForms.BunifuTextBox.StateProperties stateProperties4 = new Bunifu.UI.WinForms.BunifuTextBox.StateProperties();
            Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderEdges borderEdges1 = new Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderEdges();
            this.tbGrid = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.tbProjectSearch = new Bunifu.UI.WinForms.BunifuTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.ddProjectStatus = new Bunifu.UI.WinForms.BunifuDropdown();
            this.btnSearch = new Bunifu.UI.WinForms.BunifuButton.BunifuButton();
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
            this.tbGrid.Controls.Add(this.tbProjectSearch, 0, 1);
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
            this.tbGrid.Size = new System.Drawing.Size(1325, 694);
            this.tbGrid.TabIndex = 2;
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
            // tbProjectSearch
            // 
            this.tbProjectSearch.AcceptsReturn = false;
            this.tbProjectSearch.AcceptsTab = false;
            this.tbProjectSearch.AnimationSpeed = 200;
            this.tbProjectSearch.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.tbProjectSearch.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.tbProjectSearch.AutoSizeHeight = true;
            this.tbProjectSearch.BackColor = System.Drawing.Color.Transparent;
            this.tbProjectSearch.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tbProjectSearch.BackgroundImage")));
            this.tbProjectSearch.BorderColorActive = System.Drawing.Color.DodgerBlue;
            this.tbProjectSearch.BorderColorDisabled = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.tbProjectSearch.BorderColorHover = System.Drawing.Color.FromArgb(((int)(((byte)(105)))), ((int)(((byte)(181)))), ((int)(((byte)(255)))));
            this.tbProjectSearch.BorderColorIdle = System.Drawing.Color.Silver;
            this.tbProjectSearch.BorderRadius = 1;
            this.tbProjectSearch.BorderThickness = 1;
            this.tbProjectSearch.CharacterCase = Bunifu.UI.WinForms.BunifuTextBox.CharacterCases.Normal;
            this.tbProjectSearch.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.tbProjectSearch.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.tbProjectSearch.DefaultFont = new System.Drawing.Font("Segoe UI", 9.25F);
            this.tbProjectSearch.DefaultText = "";
            this.tbProjectSearch.FillColor = System.Drawing.Color.White;
            this.tbProjectSearch.HideSelection = true;
            this.tbProjectSearch.IconLeft = null;
            this.tbProjectSearch.IconLeftCursor = System.Windows.Forms.Cursors.IBeam;
            this.tbProjectSearch.IconPadding = 10;
            this.tbProjectSearch.IconRight = null;
            this.tbProjectSearch.IconRightCursor = System.Windows.Forms.Cursors.IBeam;
            this.tbProjectSearch.Lines = new string[0];
            this.tbProjectSearch.Location = new System.Drawing.Point(13, 61);
            this.tbProjectSearch.Margin = new System.Windows.Forms.Padding(13, 12, 13, 12);
            this.tbProjectSearch.MaxLength = 32767;
            this.tbProjectSearch.MinimumSize = new System.Drawing.Size(1, 1);
            this.tbProjectSearch.Modified = false;
            this.tbProjectSearch.Multiline = false;
            this.tbProjectSearch.Name = "tbProjectSearch";
            stateProperties1.BorderColor = System.Drawing.Color.DodgerBlue;
            stateProperties1.FillColor = System.Drawing.Color.Empty;
            stateProperties1.ForeColor = System.Drawing.Color.Empty;
            stateProperties1.PlaceholderForeColor = System.Drawing.Color.Empty;
            this.tbProjectSearch.OnActiveState = stateProperties1;
            stateProperties2.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            stateProperties2.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            stateProperties2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            stateProperties2.PlaceholderForeColor = System.Drawing.Color.DarkGray;
            this.tbProjectSearch.OnDisabledState = stateProperties2;
            stateProperties3.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(105)))), ((int)(((byte)(181)))), ((int)(((byte)(255)))));
            stateProperties3.FillColor = System.Drawing.Color.Empty;
            stateProperties3.ForeColor = System.Drawing.Color.Empty;
            stateProperties3.PlaceholderForeColor = System.Drawing.Color.Empty;
            this.tbProjectSearch.OnHoverState = stateProperties3;
            stateProperties4.BorderColor = System.Drawing.Color.Silver;
            stateProperties4.FillColor = System.Drawing.Color.White;
            stateProperties4.ForeColor = System.Drawing.Color.Empty;
            stateProperties4.PlaceholderForeColor = System.Drawing.Color.Empty;
            this.tbProjectSearch.OnIdleState = stateProperties4;
            this.tbProjectSearch.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tbProjectSearch.PasswordChar = '\0';
            this.tbProjectSearch.PlaceholderForeColor = System.Drawing.Color.Silver;
            this.tbProjectSearch.PlaceholderText = "Enter text";
            this.tbProjectSearch.ReadOnly = false;
            this.tbProjectSearch.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.tbProjectSearch.SelectedText = "";
            this.tbProjectSearch.SelectionLength = 0;
            this.tbProjectSearch.SelectionStart = 0;
            this.tbProjectSearch.ShortcutsEnabled = true;
            this.tbProjectSearch.Size = new System.Drawing.Size(773, 48);
            this.tbProjectSearch.Style = Bunifu.UI.WinForms.BunifuTextBox._Style.Bunifu;
            this.tbProjectSearch.TabIndex = 3;
            this.tbProjectSearch.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.tbProjectSearch.TextMarginBottom = 0;
            this.tbProjectSearch.TextMarginLeft = 3;
            this.tbProjectSearch.TextMarginTop = 1;
            this.tbProjectSearch.TextPlaceholder = "Enter text";
            this.tbProjectSearch.UseSystemPasswordChar = false;
            this.tbProjectSearch.WordWrap = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(812, 12);
            this.label2.Margin = new System.Windows.Forms.Padding(13, 12, 13, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(147, 25);
            this.label2.TabIndex = 4;
            this.label2.Text = "Project Status";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.ddProjectStatus);
            this.flowLayoutPanel1.Controls.Add(this.btnSearch);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(803, 53);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(521, 69);
            this.flowLayoutPanel1.TabIndex = 5;
            // 
            // ddProjectStatus
            // 
            this.ddProjectStatus.BackColor = System.Drawing.Color.Transparent;
            this.ddProjectStatus.BackgroundColor = System.Drawing.Color.White;
            this.ddProjectStatus.BorderColor = System.Drawing.Color.Silver;
            this.ddProjectStatus.BorderRadius = 1;
            this.ddProjectStatus.Color = System.Drawing.Color.Silver;
            this.ddProjectStatus.Direction = Bunifu.UI.WinForms.BunifuDropdown.Directions.Down;
            this.ddProjectStatus.DisabledBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.ddProjectStatus.DisabledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.ddProjectStatus.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.ddProjectStatus.DisabledForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.ddProjectStatus.DisabledIndicatorColor = System.Drawing.Color.DarkGray;
            this.ddProjectStatus.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.ddProjectStatus.DropdownBorderThickness = Bunifu.UI.WinForms.BunifuDropdown.BorderThickness.Thin;
            this.ddProjectStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddProjectStatus.DropDownTextAlign = Bunifu.UI.WinForms.BunifuDropdown.TextAlign.Left;
            this.ddProjectStatus.FillDropDown = true;
            this.ddProjectStatus.FillIndicator = false;
            this.ddProjectStatus.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ddProjectStatus.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.ddProjectStatus.ForeColor = System.Drawing.Color.Black;
            this.ddProjectStatus.FormattingEnabled = true;
            this.ddProjectStatus.Icon = null;
            this.ddProjectStatus.IndicatorAlignment = Bunifu.UI.WinForms.BunifuDropdown.Indicator.Right;
            this.ddProjectStatus.IndicatorColor = System.Drawing.Color.DarkGray;
            this.ddProjectStatus.IndicatorLocation = Bunifu.UI.WinForms.BunifuDropdown.Indicator.Right;
            this.ddProjectStatus.IndicatorThickness = 2;
            this.ddProjectStatus.IsDropdownOpened = false;
            this.ddProjectStatus.ItemBackColor = System.Drawing.Color.White;
            this.ddProjectStatus.ItemBorderColor = System.Drawing.Color.White;
            this.ddProjectStatus.ItemForeColor = System.Drawing.Color.Black;
            this.ddProjectStatus.ItemHeight = 26;
            this.ddProjectStatus.ItemHighLightColor = System.Drawing.Color.DodgerBlue;
            this.ddProjectStatus.ItemHighLightForeColor = System.Drawing.Color.White;
            this.ddProjectStatus.ItemTopMargin = 3;
            this.ddProjectStatus.Location = new System.Drawing.Point(13, 12);
            this.ddProjectStatus.Margin = new System.Windows.Forms.Padding(13, 12, 13, 12);
            this.ddProjectStatus.Name = "ddProjectStatus";
            this.ddProjectStatus.Size = new System.Drawing.Size(331, 32);
            this.ddProjectStatus.TabIndex = 2;
            this.ddProjectStatus.Text = null;
            this.ddProjectStatus.TextAlignment = Bunifu.UI.WinForms.BunifuDropdown.TextAlign.Left;
            this.ddProjectStatus.TextLeftMargin = 5;
            // 
            // btnSearch
            // 
            this.btnSearch.AllowAnimations = true;
            this.btnSearch.AllowMouseEffects = true;
            this.btnSearch.AllowToggling = false;
            this.btnSearch.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnSearch.AnimationSpeed = 200;
            this.btnSearch.AutoGenerateColors = false;
            this.btnSearch.AutoRoundBorders = false;
            this.btnSearch.AutoSizeLeftIcon = true;
            this.btnSearch.AutoSizeRightIcon = true;
            this.btnSearch.BackColor = System.Drawing.Color.Transparent;
            this.btnSearch.BackColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(122)))), ((int)(((byte)(183)))));
            this.btnSearch.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSearch.BackgroundImage")));
            this.btnSearch.BorderStyle = Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderStyles.Solid;
            this.btnSearch.ButtonText = "Search";
            this.btnSearch.ButtonTextMarginLeft = 0;
            this.btnSearch.ColorContrastOnClick = 45;
            this.btnSearch.ColorContrastOnHover = 45;
            this.btnSearch.Cursor = System.Windows.Forms.Cursors.Default;
            borderEdges1.BottomLeft = true;
            borderEdges1.BottomRight = true;
            borderEdges1.TopLeft = true;
            borderEdges1.TopRight = true;
            this.btnSearch.CustomizableEdges = borderEdges1;
            this.btnSearch.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnSearch.DisabledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(191)))), ((int)(((byte)(191)))));
            this.btnSearch.DisabledFillColor = System.Drawing.Color.Empty;
            this.btnSearch.DisabledForecolor = System.Drawing.Color.Empty;
            this.btnSearch.FocusState = Bunifu.UI.WinForms.BunifuButton.BunifuButton.ButtonStates.Pressed;
            this.btnSearch.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnSearch.ForeColor = System.Drawing.Color.White;
            this.btnSearch.IconLeft = null;
            this.btnSearch.IconLeftAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSearch.IconLeftCursor = System.Windows.Forms.Cursors.Default;
            this.btnSearch.IconLeftPadding = new System.Windows.Forms.Padding(11, 3, 3, 3);
            this.btnSearch.IconMarginLeft = 11;
            this.btnSearch.IconPadding = 10;
            this.btnSearch.IconRight = null;
            this.btnSearch.IconRightAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSearch.IconRightCursor = System.Windows.Forms.Cursors.Default;
            this.btnSearch.IconRightPadding = new System.Windows.Forms.Padding(3, 3, 7, 3);
            this.btnSearch.IconSize = 25;
            this.btnSearch.IdleBorderColor = System.Drawing.Color.Empty;
            this.btnSearch.IdleBorderRadius = 0;
            this.btnSearch.IdleBorderThickness = 0;
            this.btnSearch.IdleFillColor = System.Drawing.Color.Empty;
            this.btnSearch.IdleIconLeftImage = null;
            this.btnSearch.IdleIconRightImage = null;
            this.btnSearch.IndicateFocus = false;
            this.btnSearch.Location = new System.Drawing.Point(361, 4);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.OnDisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(191)))), ((int)(((byte)(191)))));
            this.btnSearch.OnDisabledState.BorderRadius = 20;
            this.btnSearch.OnDisabledState.BorderStyle = Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderStyles.Solid;
            this.btnSearch.OnDisabledState.BorderThickness = 1;
            this.btnSearch.OnDisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.btnSearch.OnDisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(160)))), ((int)(((byte)(168)))));
            this.btnSearch.OnDisabledState.IconLeftImage = null;
            this.btnSearch.OnDisabledState.IconRightImage = null;
            this.btnSearch.onHoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(150)))), ((int)(((byte)(255)))));
            this.btnSearch.onHoverState.BorderRadius = 20;
            this.btnSearch.onHoverState.BorderStyle = Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderStyles.Solid;
            this.btnSearch.onHoverState.BorderThickness = 1;
            this.btnSearch.onHoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(150)))), ((int)(((byte)(255)))));
            this.btnSearch.onHoverState.ForeColor = System.Drawing.Color.White;
            this.btnSearch.onHoverState.IconLeftImage = null;
            this.btnSearch.onHoverState.IconRightImage = null;
            this.btnSearch.OnIdleState.BorderColor = System.Drawing.Color.DodgerBlue;
            this.btnSearch.OnIdleState.BorderRadius = 20;
            this.btnSearch.OnIdleState.BorderStyle = Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderStyles.Solid;
            this.btnSearch.OnIdleState.BorderThickness = 1;
            this.btnSearch.OnIdleState.FillColor = System.Drawing.Color.DodgerBlue;
            this.btnSearch.OnIdleState.ForeColor = System.Drawing.Color.White;
            this.btnSearch.OnIdleState.IconLeftImage = null;
            this.btnSearch.OnIdleState.IconRightImage = null;
            this.btnSearch.OnPressedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(96)))), ((int)(((byte)(144)))));
            this.btnSearch.OnPressedState.BorderRadius = 20;
            this.btnSearch.OnPressedState.BorderStyle = Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderStyles.Solid;
            this.btnSearch.OnPressedState.BorderThickness = 1;
            this.btnSearch.OnPressedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(96)))), ((int)(((byte)(144)))));
            this.btnSearch.OnPressedState.ForeColor = System.Drawing.Color.White;
            this.btnSearch.OnPressedState.IconLeftImage = null;
            this.btnSearch.OnPressedState.IconRightImage = null;
            this.btnSearch.Size = new System.Drawing.Size(136, 48);
            this.btnSearch.TabIndex = 3;
            this.btnSearch.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnSearch.TextAlignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.btnSearch.TextMarginLeft = 0;
            this.btnSearch.TextPadding = new System.Windows.Forms.Padding(0);
            this.btnSearch.UseDefaultRadiusAndThickness = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // CtrlPanelProject
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.tbGrid);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "CtrlPanelProject";
            this.Size = new System.Drawing.Size(1325, 694);
            this.Load += new System.EventHandler(this.CtrlPanelProject_Load);
            this.tbGrid.ResumeLayout(false);
            this.tbGrid.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tbGrid;
        private System.Windows.Forms.Label label1;
        private Bunifu.UI.WinForms.BunifuTextBox tbProjectSearch;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private Bunifu.UI.WinForms.BunifuDropdown ddProjectStatus;
        private Bunifu.UI.WinForms.BunifuButton.BunifuButton btnSearch;
    }
}
