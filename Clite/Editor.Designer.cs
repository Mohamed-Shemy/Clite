namespace Clite
{
    partial class Editor
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Editor));
            this.btn_exit = new System.Windows.Forms.Button();
            this.btn_set = new System.Windows.Forms.Button();
            this.btn_max = new System.Windows.Forms.Button();
            this.btn_min = new System.Windows.Forms.Button();
            this.ToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.btn_Compile = new System.Windows.Forms.Button();
            this.btn_save = new System.Windows.Forms.Button();
            this.btn_load = new System.Windows.Forms.Button();
            this.btn_lists = new System.Windows.Forms.Button();
            this.logo = new System.Windows.Forms.Label();
            this.ParentContainer = new System.Windows.Forms.SplitContainer();
            this.edit = new FastColoredTextBoxNS.FastColoredTextBox();
            this.Tabs = new iTalk.iTalk_TabControl();
            this.tp_errorlist = new System.Windows.Forms.TabPage();
            this.error_list = new System.Windows.Forms.ListView();
            this.ch_number = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ch_type = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ch_file = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ch_line = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ch_cln = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ch_dec = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tp_output = new System.Windows.Forms.TabPage();
            this.txt_out = new System.Windows.Forms.RichTextBox();
            this.ListIcons = new System.Windows.Forms.ImageList(this.components);
            this.lbl_stats = new System.Windows.Forms.Label();
            this.lbl_editor = new System.Windows.Forms.Label();
            this.btn_build = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.ParentContainer)).BeginInit();
            this.ParentContainer.Panel1.SuspendLayout();
            this.ParentContainer.Panel2.SuspendLayout();
            this.ParentContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.edit)).BeginInit();
            this.Tabs.SuspendLayout();
            this.tp_errorlist.SuspendLayout();
            this.tp_output.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_exit
            // 
            this.btn_exit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_exit.FlatAppearance.BorderSize = 0;
            this.btn_exit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_exit.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_exit.ForeColor = System.Drawing.Color.White;
            this.btn_exit.Image = ((System.Drawing.Image)(resources.GetObject("btn_exit.Image")));
            this.btn_exit.Location = new System.Drawing.Point(866, 12);
            this.btn_exit.Name = "btn_exit";
            this.btn_exit.Size = new System.Drawing.Size(32, 35);
            this.btn_exit.TabIndex = 5;
            this.btn_exit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.ToolTip.SetToolTip(this.btn_exit, "Exit");
            this.btn_exit.UseVisualStyleBackColor = true;
            this.btn_exit.Click += new System.EventHandler(this.ExitClick);
            // 
            // btn_set
            // 
            this.btn_set.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_set.FlatAppearance.BorderSize = 0;
            this.btn_set.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_set.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_set.ForeColor = System.Drawing.Color.White;
            this.btn_set.Image = ((System.Drawing.Image)(resources.GetObject("btn_set.Image")));
            this.btn_set.Location = new System.Drawing.Point(718, 12);
            this.btn_set.Name = "btn_set";
            this.btn_set.Size = new System.Drawing.Size(32, 35);
            this.btn_set.TabIndex = 6;
            this.btn_set.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.ToolTip.SetToolTip(this.btn_set, "Settings");
            this.btn_set.UseVisualStyleBackColor = true;
            // 
            // btn_max
            // 
            this.btn_max.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_max.FlatAppearance.BorderSize = 0;
            this.btn_max.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_max.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_max.ForeColor = System.Drawing.Color.White;
            this.btn_max.Image = ((System.Drawing.Image)(resources.GetObject("btn_max.Image")));
            this.btn_max.Location = new System.Drawing.Point(828, 12);
            this.btn_max.Name = "btn_max";
            this.btn_max.Size = new System.Drawing.Size(32, 35);
            this.btn_max.TabIndex = 7;
            this.btn_max.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.ToolTip.SetToolTip(this.btn_max, "Max");
            this.btn_max.UseVisualStyleBackColor = true;
            this.btn_max.Click += new System.EventHandler(this.MaxWindowClick);
            // 
            // btn_min
            // 
            this.btn_min.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_min.FlatAppearance.BorderSize = 0;
            this.btn_min.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_min.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_min.ForeColor = System.Drawing.Color.White;
            this.btn_min.Image = ((System.Drawing.Image)(resources.GetObject("btn_min.Image")));
            this.btn_min.Location = new System.Drawing.Point(790, 12);
            this.btn_min.Name = "btn_min";
            this.btn_min.Size = new System.Drawing.Size(32, 35);
            this.btn_min.TabIndex = 7;
            this.btn_min.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.ToolTip.SetToolTip(this.btn_min, "Min");
            this.btn_min.UseVisualStyleBackColor = true;
            this.btn_min.Click += new System.EventHandler(this.MinWindowClick);
            // 
            // ToolTip
            // 
            this.ToolTip.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.ToolTip.ForeColor = System.Drawing.Color.White;
            this.ToolTip.IsBalloon = true;
            this.ToolTip.OwnerDraw = true;
            this.ToolTip.StripAmpersands = true;
            // 
            // btn_Compile
            // 
            this.btn_Compile.FlatAppearance.BorderSize = 0;
            this.btn_Compile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Compile.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Compile.ForeColor = System.Drawing.Color.White;
            this.btn_Compile.Image = ((System.Drawing.Image)(resources.GetObject("btn_Compile.Image")));
            this.btn_Compile.Location = new System.Drawing.Point(166, 12);
            this.btn_Compile.Name = "btn_Compile";
            this.btn_Compile.Size = new System.Drawing.Size(32, 35);
            this.btn_Compile.TabIndex = 6;
            this.btn_Compile.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.ToolTip.SetToolTip(this.btn_Compile, "Compile");
            this.btn_Compile.UseVisualStyleBackColor = true;
            this.btn_Compile.Click += new System.EventHandler(this.CompileClick);
            // 
            // btn_save
            // 
            this.btn_save.FlatAppearance.BorderSize = 0;
            this.btn_save.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_save.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_save.ForeColor = System.Drawing.Color.White;
            this.btn_save.Image = ((System.Drawing.Image)(resources.GetObject("btn_save.Image")));
            this.btn_save.Location = new System.Drawing.Point(128, 12);
            this.btn_save.Name = "btn_save";
            this.btn_save.Size = new System.Drawing.Size(32, 35);
            this.btn_save.TabIndex = 6;
            this.btn_save.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.ToolTip.SetToolTip(this.btn_save, "Save");
            this.btn_save.UseVisualStyleBackColor = true;
            this.btn_save.Click += new System.EventHandler(this.SaveCodeClick);
            // 
            // btn_load
            // 
            this.btn_load.FlatAppearance.BorderSize = 0;
            this.btn_load.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_load.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_load.ForeColor = System.Drawing.Color.White;
            this.btn_load.Image = ((System.Drawing.Image)(resources.GetObject("btn_load.Image")));
            this.btn_load.Location = new System.Drawing.Point(90, 12);
            this.btn_load.Name = "btn_load";
            this.btn_load.Size = new System.Drawing.Size(32, 35);
            this.btn_load.TabIndex = 6;
            this.btn_load.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.ToolTip.SetToolTip(this.btn_load, "Load Code File");
            this.btn_load.UseVisualStyleBackColor = true;
            this.btn_load.Click += new System.EventHandler(this.LoadCodeClick);
            // 
            // btn_lists
            // 
            this.btn_lists.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btn_lists.FlatAppearance.BorderSize = 0;
            this.btn_lists.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_lists.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_lists.ForeColor = System.Drawing.Color.White;
            this.btn_lists.Image = ((System.Drawing.Image)(resources.GetObject("btn_lists.Image")));
            this.btn_lists.Location = new System.Drawing.Point(12, 479);
            this.btn_lists.Name = "btn_lists";
            this.btn_lists.Size = new System.Drawing.Size(32, 11);
            this.btn_lists.TabIndex = 10;
            this.btn_lists.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.ToolTip.SetToolTip(this.btn_lists, "Monitor");
            this.btn_lists.UseVisualStyleBackColor = true;
            this.btn_lists.Click += new System.EventHandler(this.CollapsErrorListClick);
            // 
            // logo
            // 
            this.logo.Image = ((System.Drawing.Image)(resources.GetObject("logo.Image")));
            this.logo.Location = new System.Drawing.Point(0, 0);
            this.logo.Name = "logo";
            this.logo.Size = new System.Drawing.Size(62, 58);
            this.logo.TabIndex = 12;
            this.ToolTip.SetToolTip(this.logo, "C-Lite Compiler");
            this.logo.DoubleClick += new System.EventHandler(this.MaxWindowClick);
            this.logo.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MoveMouseDown);
            this.logo.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MoveMouse);
            this.logo.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MoveMouseUp);
            // 
            // ParentContainer
            // 
            this.ParentContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ParentContainer.Location = new System.Drawing.Point(12, 61);
            this.ParentContainer.Name = "ParentContainer";
            this.ParentContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // ParentContainer.Panel1
            // 
            this.ParentContainer.Panel1.Controls.Add(this.edit);
            // 
            // ParentContainer.Panel2
            // 
            this.ParentContainer.Panel2.Controls.Add(this.Tabs);
            this.ParentContainer.Size = new System.Drawing.Size(886, 406);
            this.ParentContainer.SplitterDistance = 264;
            this.ParentContainer.SplitterWidth = 10;
            this.ParentContainer.TabIndex = 9;
            // 
            // edit
            // 
            this.edit.AutoCompleteBrackets = true;
            this.edit.AutoCompleteBracketsList = new char[] {
        '(',
        ')',
        '{',
        '}',
        '[',
        ']',
        '\"',
        '\"',
        '\'',
        '\''};
            this.edit.AutoIndentCharsPatterns = "\r\n^\\s*[\\w\\.]+(\\s\\w+)?\\s*(?<range>=)\\s*(?<range>[^;]+);\r\n^\\s*(case|default)\\s*[^:]" +
    "*(?<range>:)\\s*(?<range>[^;]+);\r\n";
            this.edit.AutoScrollMinSize = new System.Drawing.Size(31, 18);
            this.edit.BackBrush = null;
            this.edit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.edit.BookmarkColor = System.Drawing.Color.Blue;
            this.edit.BracketsHighlightStrategy = FastColoredTextBoxNS.BracketsHighlightStrategy.Strategy2;
            this.edit.CaretColor = System.Drawing.Color.Brown;
            this.edit.CharHeight = 18;
            this.edit.CharWidth = 10;
            this.edit.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.edit.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.edit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.edit.Font = new System.Drawing.Font("Courier New", 12F);
            this.edit.ForeColor = System.Drawing.Color.White;
            this.edit.IndentBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.edit.IsReplaceMode = false;
            this.edit.Language = FastColoredTextBoxNS.Language.CSharp;
            this.edit.LeftBracket = '(';
            this.edit.LeftBracket2 = '{';
            this.edit.Location = new System.Drawing.Point(0, 0);
            this.edit.Name = "edit";
            this.edit.Paddings = new System.Windows.Forms.Padding(0);
            this.edit.RightBracket = ')';
            this.edit.RightBracket2 = '}';
            this.edit.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.edit.ServiceColors = ((FastColoredTextBoxNS.ServiceColors)(resources.GetObject("edit.ServiceColors")));
            this.edit.ShowFoldingLines = true;
            this.edit.ShowScrollBars = false;
            this.edit.Size = new System.Drawing.Size(886, 264);
            this.edit.TabIndex = 13;
            this.edit.TextAreaBorderColor = System.Drawing.Color.White;
            this.edit.ToolTip = this.ToolTip;
            this.edit.Zoom = 100;
            // 
            // Tabs
            // 
            this.Tabs.Alignment = System.Windows.Forms.TabAlignment.Left;
            this.Tabs.Controls.Add(this.tp_errorlist);
            this.Tabs.Controls.Add(this.tp_output);
            this.Tabs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Tabs.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
            this.Tabs.ImageList = this.ListIcons;
            this.Tabs.ItemSize = new System.Drawing.Size(40, 110);
            this.Tabs.Location = new System.Drawing.Point(0, 0);
            this.Tabs.Multiline = true;
            this.Tabs.Name = "Tabs";
            this.Tabs.Padding = new System.Drawing.Point(0, 0);
            this.Tabs.SelectedIndex = 0;
            this.Tabs.Size = new System.Drawing.Size(886, 132);
            this.Tabs.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.Tabs.TabIndex = 13;
            // 
            // tp_errorlist
            // 
            this.tp_errorlist.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(246)))), ((int)(((byte)(246)))));
            this.tp_errorlist.Controls.Add(this.error_list);
            this.tp_errorlist.Location = new System.Drawing.Point(114, 4);
            this.tp_errorlist.Margin = new System.Windows.Forms.Padding(0);
            this.tp_errorlist.Name = "tp_errorlist";
            this.tp_errorlist.Padding = new System.Windows.Forms.Padding(3);
            this.tp_errorlist.Size = new System.Drawing.Size(768, 124);
            this.tp_errorlist.TabIndex = 0;
            this.tp_errorlist.Text = "Error List";
            // 
            // error_list
            // 
            this.error_list.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.error_list.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ch_number,
            this.ch_type,
            this.ch_file,
            this.ch_line,
            this.ch_cln,
            this.ch_dec});
            this.error_list.Dock = System.Windows.Forms.DockStyle.Fill;
            this.error_list.Font = new System.Drawing.Font("Century Gothic", 10F);
            this.error_list.ForeColor = System.Drawing.Color.White;
            this.error_list.FullRowSelect = true;
            this.error_list.GridLines = true;
            this.error_list.Location = new System.Drawing.Point(3, 3);
            this.error_list.Name = "error_list";
            this.error_list.ShowItemToolTips = true;
            this.error_list.Size = new System.Drawing.Size(762, 118);
            this.error_list.TabIndex = 4;
            this.error_list.UseCompatibleStateImageBehavior = false;
            this.error_list.View = System.Windows.Forms.View.Details;
            this.error_list.DoubleClick += new System.EventHandler(this.GoToErrorLineDClick);
            // 
            // ch_number
            // 
            this.ch_number.Text = "#";
            this.ch_number.Width = 29;
            // 
            // ch_type
            // 
            this.ch_type.Text = "Type";
            this.ch_type.Width = 123;
            // 
            // ch_file
            // 
            this.ch_file.Text = "File";
            this.ch_file.Width = 139;
            // 
            // ch_line
            // 
            this.ch_line.Text = "Line";
            this.ch_line.Width = 45;
            // 
            // ch_cln
            // 
            this.ch_cln.Text = "CN";
            this.ch_cln.Width = 46;
            // 
            // ch_dec
            // 
            this.ch_dec.Text = "Description";
            this.ch_dec.Width = 348;
            // 
            // tp_output
            // 
            this.tp_output.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(246)))), ((int)(((byte)(246)))));
            this.tp_output.Controls.Add(this.txt_out);
            this.tp_output.Location = new System.Drawing.Point(114, 4);
            this.tp_output.Margin = new System.Windows.Forms.Padding(0);
            this.tp_output.Name = "tp_output";
            this.tp_output.Padding = new System.Windows.Forms.Padding(3);
            this.tp_output.Size = new System.Drawing.Size(768, 124);
            this.tp_output.TabIndex = 1;
            this.tp_output.Text = "Output";
            // 
            // txt_out
            // 
            this.txt_out.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.txt_out.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txt_out.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txt_out.Font = new System.Drawing.Font("Century Gothic", 12F);
            this.txt_out.ForeColor = System.Drawing.Color.White;
            this.txt_out.Location = new System.Drawing.Point(3, 3);
            this.txt_out.Name = "txt_out";
            this.txt_out.ReadOnly = true;
            this.txt_out.Size = new System.Drawing.Size(762, 118);
            this.txt_out.TabIndex = 10;
            this.txt_out.Text = "";
            // 
            // ListIcons
            // 
            this.ListIcons.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ListIcons.ImageStream")));
            this.ListIcons.TransparentColor = System.Drawing.Color.Transparent;
            this.ListIcons.Images.SetKeyName(0, "Error_pink_20px.png");
            this.ListIcons.Images.SetKeyName(1, "Monitor_pink_20px.png");
            this.ListIcons.Images.SetKeyName(2, "Close Window_pink_32px.png");
            this.ListIcons.Images.SetKeyName(3, "Close Square.png");
            // 
            // lbl_stats
            // 
            this.lbl_stats.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lbl_stats.Font = new System.Drawing.Font("Century Gothic", 10F);
            this.lbl_stats.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(23)))), ((int)(((byte)(120)))));
            this.lbl_stats.Location = new System.Drawing.Point(69, 474);
            this.lbl_stats.Name = "lbl_stats";
            this.lbl_stats.Size = new System.Drawing.Size(268, 26);
            this.lbl_stats.TabIndex = 11;
            this.lbl_stats.Tag = "";
            // 
            // lbl_editor
            // 
            this.lbl_editor.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_editor.Font = new System.Drawing.Font("Century Gothic", 10F);
            this.lbl_editor.ForeColor = System.Drawing.Color.DodgerBlue;
            this.lbl_editor.Location = new System.Drawing.Point(396, 473);
            this.lbl_editor.Name = "lbl_editor";
            this.lbl_editor.Size = new System.Drawing.Size(502, 26);
            this.lbl_editor.TabIndex = 11;
            this.lbl_editor.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btn_build
            // 
            this.btn_build.FlatAppearance.BorderSize = 0;
            this.btn_build.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_build.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_build.ForeColor = System.Drawing.Color.White;
            this.btn_build.Image = ((System.Drawing.Image)(resources.GetObject("btn_build.Image")));
            this.btn_build.Location = new System.Drawing.Point(228, 12);
            this.btn_build.Name = "btn_build";
            this.btn_build.Size = new System.Drawing.Size(32, 35);
            this.btn_build.TabIndex = 6;
            this.btn_build.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.ToolTip.SetToolTip(this.btn_build, "Build");
            this.btn_build.UseVisualStyleBackColor = true;
            this.btn_build.Click += new System.EventHandler(this.BuildClick);
            // 
            // Editor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.ClientSize = new System.Drawing.Size(910, 500);
            this.Controls.Add(this.logo);
            this.Controls.Add(this.lbl_editor);
            this.Controls.Add(this.lbl_stats);
            this.Controls.Add(this.btn_lists);
            this.Controls.Add(this.ParentContainer);
            this.Controls.Add(this.btn_exit);
            this.Controls.Add(this.btn_load);
            this.Controls.Add(this.btn_save);
            this.Controls.Add(this.btn_build);
            this.Controls.Add(this.btn_Compile);
            this.Controls.Add(this.btn_set);
            this.Controls.Add(this.btn_min);
            this.Controls.Add(this.btn_max);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(910, 500);
            this.Name = "Editor";
            this.Text = "C-Lite Editor";
            this.Load += new System.EventHandler(this.Editor_Load);
            this.DoubleClick += new System.EventHandler(this.MaxWindowClick);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MoveMouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MoveMouse);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MoveMouseUp);
            this.ParentContainer.Panel1.ResumeLayout(false);
            this.ParentContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ParentContainer)).EndInit();
            this.ParentContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.edit)).EndInit();
            this.Tabs.ResumeLayout(false);
            this.tp_errorlist.ResumeLayout(false);
            this.tp_output.ResumeLayout(false);
            this.ResumeLayout(false);

        }
        #endregion

        private System.Windows.Forms.Button btn_exit;
        private System.Windows.Forms.Button btn_set;
        private System.Windows.Forms.Button btn_max;
        private System.Windows.Forms.Button btn_min;
        private System.Windows.Forms.ToolTip ToolTip;
        private System.Windows.Forms.Button btn_Compile;
        private System.Windows.Forms.Button btn_save;
        private System.Windows.Forms.Button btn_load;
        private System.Windows.Forms.SplitContainer ParentContainer;
        private System.Windows.Forms.Button btn_lists;
        private System.Windows.Forms.Label lbl_stats;
        private System.Windows.Forms.Label logo;
        private iTalk.iTalk_TabControl Tabs;
        private System.Windows.Forms.TabPage tp_errorlist;
        private System.Windows.Forms.ListView error_list;
        private System.Windows.Forms.ColumnHeader ch_number;
        private System.Windows.Forms.ColumnHeader ch_type;
        private System.Windows.Forms.ColumnHeader ch_file;
        private System.Windows.Forms.ColumnHeader ch_line;
        private System.Windows.Forms.ColumnHeader ch_cln;
        private System.Windows.Forms.ColumnHeader ch_dec;
        private System.Windows.Forms.TabPage tp_output;
        private System.Windows.Forms.RichTextBox txt_out;
        private System.Windows.Forms.ImageList ListIcons;
        private System.Windows.Forms.Label lbl_editor;
        private FastColoredTextBoxNS.FastColoredTextBox edit;
        private System.Windows.Forms.Button btn_build;
    }
}