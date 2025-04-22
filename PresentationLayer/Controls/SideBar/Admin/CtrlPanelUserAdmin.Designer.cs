namespace PresentationLayer.UC_SideBar
{
    partial class CtrlPanelUserAdmin
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
            Bunifu.UI.WinForms.BunifuTextBox.StateProperties stateProperties1 = new Bunifu.UI.WinForms.BunifuTextBox.StateProperties();
            Bunifu.UI.WinForms.BunifuTextBox.StateProperties stateProperties2 = new Bunifu.UI.WinForms.BunifuTextBox.StateProperties();
            Bunifu.UI.WinForms.BunifuTextBox.StateProperties stateProperties3 = new Bunifu.UI.WinForms.BunifuTextBox.StateProperties();
            Bunifu.UI.WinForms.BunifuTextBox.StateProperties stateProperties4 = new Bunifu.UI.WinForms.BunifuTextBox.StateProperties();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CtrlPanelUserAdmin));
            Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderEdges borderEdges1 = new Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderEdges();
            this.tbGrid = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.tbSearch = new Bunifu.UI.WinForms.BunifuTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnSearch = new Bunifu.UI.WinForms.BunifuButton.BunifuButton();
            this.drpdwnRole = new Bunifu.UI.WinForms.BunifuDropdown();
            this.label3 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.drpdwnDepartment = new Bunifu.UI.WinForms.BunifuDropdown();
            this.tbGrid.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbGrid
            // 
            this.tbGrid.ColumnCount = 3;
            this.tbGrid.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 62.79343F));
            this.tbGrid.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 37.20657F));
            this.tbGrid.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 476F));
            this.tbGrid.Controls.Add(this.panel2, 2, 1);
            this.tbGrid.Controls.Add(this.label1, 0, 0);
            this.tbGrid.Controls.Add(this.tbSearch, 0, 1);
            this.tbGrid.Controls.Add(this.label2, 1, 0);
            this.tbGrid.Controls.Add(this.panel1, 1, 1);
            this.tbGrid.Controls.Add(this.label3, 2, 0);
            this.tbGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbGrid.Location = new System.Drawing.Point(0, 0);
            this.tbGrid.Margin = new System.Windows.Forms.Padding(4);
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
            this.tbSearch.Padding = new System.Windows.Forms.Padding(4);
            this.tbSearch.PasswordChar = '\0';
            this.tbSearch.PlaceholderForeColor = System.Drawing.Color.Silver;
            this.tbSearch.PlaceholderText = "Enter text";
            this.tbSearch.ReadOnly = false;
            this.tbSearch.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.tbSearch.SelectedText = "";
            this.tbSearch.SelectionLength = 0;
            this.tbSearch.SelectionStart = 0;
            this.tbSearch.ShortcutsEnabled = true;
            this.tbSearch.Size = new System.Drawing.Size(509, 48);
            this.tbSearch.Style = Bunifu.UI.WinForms.BunifuTextBox._Style.Bunifu;
            this.tbSearch.TabIndex = 3;
            this.tbSearch.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.tbSearch.TextMarginBottom = 0;
            this.tbSearch.TextMarginLeft = 3;
            this.tbSearch.TextMarginTop = 1;
            this.tbSearch.TextPlaceholder = "Enter text";
            this.tbSearch.UseSystemPasswordChar = false;
            this.tbSearch.WordWrap = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(548, 12);
            this.label2.Margin = new System.Windows.Forms.Padding(13, 12, 13, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 25);
            this.label2.TabIndex = 4;
            this.label2.Text = "Role";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.drpdwnRole);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(539, 53);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(309, 65);
            this.panel1.TabIndex = 6;
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
            this.btnSearch.IconLeftAlign = System.Drawing.ContentAlignment.MiddleRight;
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
            this.btnSearch.Location = new System.Drawing.Point(302, 8);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(4);
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
            // drpdwnRole
            // 
            this.drpdwnRole.BackColor = System.Drawing.Color.Transparent;
            this.drpdwnRole.BackgroundColor = System.Drawing.Color.White;
            this.drpdwnRole.BorderColor = System.Drawing.Color.Silver;
            this.drpdwnRole.BorderRadius = 1;
            this.drpdwnRole.Color = System.Drawing.Color.Silver;
            this.drpdwnRole.Direction = Bunifu.UI.WinForms.BunifuDropdown.Directions.Down;
            this.drpdwnRole.DisabledBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.drpdwnRole.DisabledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.drpdwnRole.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.drpdwnRole.DisabledForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.drpdwnRole.DisabledIndicatorColor = System.Drawing.Color.DarkGray;
            this.drpdwnRole.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.drpdwnRole.DropdownBorderThickness = Bunifu.UI.WinForms.BunifuDropdown.BorderThickness.Thin;
            this.drpdwnRole.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.drpdwnRole.DropDownTextAlign = Bunifu.UI.WinForms.BunifuDropdown.TextAlign.Left;
            this.drpdwnRole.FillDropDown = true;
            this.drpdwnRole.FillIndicator = false;
            this.drpdwnRole.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.drpdwnRole.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.drpdwnRole.ForeColor = System.Drawing.Color.Black;
            this.drpdwnRole.FormattingEnabled = true;
            this.drpdwnRole.Icon = null;
            this.drpdwnRole.IndicatorAlignment = Bunifu.UI.WinForms.BunifuDropdown.Indicator.Right;
            this.drpdwnRole.IndicatorColor = System.Drawing.Color.DarkGray;
            this.drpdwnRole.IndicatorLocation = Bunifu.UI.WinForms.BunifuDropdown.Indicator.Right;
            this.drpdwnRole.IndicatorThickness = 2;
            this.drpdwnRole.IsDropdownOpened = false;
            this.drpdwnRole.ItemBackColor = System.Drawing.Color.White;
            this.drpdwnRole.ItemBorderColor = System.Drawing.Color.White;
            this.drpdwnRole.ItemForeColor = System.Drawing.Color.Black;
            this.drpdwnRole.ItemHeight = 26;
            this.drpdwnRole.ItemHighLightColor = System.Drawing.Color.DodgerBlue;
            this.drpdwnRole.ItemHighLightForeColor = System.Drawing.Color.White;
            this.drpdwnRole.ItemTopMargin = 3;
            this.drpdwnRole.Location = new System.Drawing.Point(9, 12);
            this.drpdwnRole.Margin = new System.Windows.Forms.Padding(13, 12, 13, 12);
            this.drpdwnRole.Name = "drpdwnRole";
            this.drpdwnRole.Size = new System.Drawing.Size(271, 32);
            this.drpdwnRole.TabIndex = 2;
            this.drpdwnRole.Text = null;
            this.drpdwnRole.TextAlignment = Bunifu.UI.WinForms.BunifuDropdown.TextAlign.Left;
            this.drpdwnRole.TextLeftMargin = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(865, 12);
            this.label3.Margin = new System.Windows.Forms.Padding(13, 12, 13, 12);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(123, 25);
            this.label3.TabIndex = 4;
            this.label3.Text = "Department";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.drpdwnDepartment);
            this.panel2.Controls.Add(this.btnSearch);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(856, 53);
            this.panel2.Margin = new System.Windows.Forms.Padding(4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(469, 65);
            this.panel2.TabIndex = 7;
            // 
            // drpdwnDepartment
            // 
            this.drpdwnDepartment.BackColor = System.Drawing.Color.Transparent;
            this.drpdwnDepartment.BackgroundColor = System.Drawing.Color.White;
            this.drpdwnDepartment.BorderColor = System.Drawing.Color.Silver;
            this.drpdwnDepartment.BorderRadius = 1;
            this.drpdwnDepartment.Color = System.Drawing.Color.Silver;
            this.drpdwnDepartment.Direction = Bunifu.UI.WinForms.BunifuDropdown.Directions.Down;
            this.drpdwnDepartment.DisabledBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.drpdwnDepartment.DisabledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.drpdwnDepartment.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.drpdwnDepartment.DisabledForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.drpdwnDepartment.DisabledIndicatorColor = System.Drawing.Color.DarkGray;
            this.drpdwnDepartment.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.drpdwnDepartment.DropdownBorderThickness = Bunifu.UI.WinForms.BunifuDropdown.BorderThickness.Thin;
            this.drpdwnDepartment.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.drpdwnDepartment.DropDownTextAlign = Bunifu.UI.WinForms.BunifuDropdown.TextAlign.Left;
            this.drpdwnDepartment.FillDropDown = true;
            this.drpdwnDepartment.FillIndicator = false;
            this.drpdwnDepartment.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.drpdwnDepartment.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.drpdwnDepartment.ForeColor = System.Drawing.Color.Black;
            this.drpdwnDepartment.FormattingEnabled = true;
            this.drpdwnDepartment.Icon = null;
            this.drpdwnDepartment.IndicatorAlignment = Bunifu.UI.WinForms.BunifuDropdown.Indicator.Right;
            this.drpdwnDepartment.IndicatorColor = System.Drawing.Color.DarkGray;
            this.drpdwnDepartment.IndicatorLocation = Bunifu.UI.WinForms.BunifuDropdown.Indicator.Right;
            this.drpdwnDepartment.IndicatorThickness = 2;
            this.drpdwnDepartment.IsDropdownOpened = false;
            this.drpdwnDepartment.ItemBackColor = System.Drawing.Color.White;
            this.drpdwnDepartment.ItemBorderColor = System.Drawing.Color.White;
            this.drpdwnDepartment.ItemForeColor = System.Drawing.Color.Black;
            this.drpdwnDepartment.ItemHeight = 26;
            this.drpdwnDepartment.ItemHighLightColor = System.Drawing.Color.DodgerBlue;
            this.drpdwnDepartment.ItemHighLightForeColor = System.Drawing.Color.White;
            this.drpdwnDepartment.ItemTopMargin = 3;
            this.drpdwnDepartment.Location = new System.Drawing.Point(9, 12);
            this.drpdwnDepartment.Margin = new System.Windows.Forms.Padding(13, 12, 13, 12);
            this.drpdwnDepartment.Name = "drpdwnDepartment";
            this.drpdwnDepartment.Size = new System.Drawing.Size(239, 32);
            this.drpdwnDepartment.TabIndex = 2;
            this.drpdwnDepartment.Text = null;
            this.drpdwnDepartment.TextAlignment = Bunifu.UI.WinForms.BunifuDropdown.TextAlign.Left;
            this.drpdwnDepartment.TextLeftMargin = 5;
            // 
            // CtrlPanelUserAdmin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.tbGrid);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "CtrlPanelUserAdmin";
            this.Size = new System.Drawing.Size(1329, 729);
            this.Load += new System.EventHandler(this.UC_Task_Load_1);
            this.tbGrid.ResumeLayout(false);
            this.tbGrid.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TableLayoutPanel tbGrid;
        private Bunifu.UI.WinForms.BunifuTextBox tbSearch;
        private Bunifu.UI.WinForms.BunifuDropdown drpdwnRole;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private Bunifu.UI.WinForms.BunifuButton.BunifuButton btnSearch;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel2;
        private Bunifu.UI.WinForms.BunifuDropdown drpdwnDepartment;
    }
}
