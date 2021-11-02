/*
 * Created by SharpDevelop.
 * User: Microsan84
 * Date: 2017-05-06
 * Time: 13:11
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace RoboRemoPC
{
    partial class MainForm
    {
        /// <summary>
        /// Designer variable used to keep track of non-visual components.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.DataGridView dgv;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripDropDownButton tsbtnfile;
        private System.Windows.Forms.ToolStripMenuItem tsmiFileOpen;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel tsslblItemCount;
        private System.Windows.Forms.DataGridView dgvUiItem;
        private System.Windows.Forms.Panel panelUiView;
        private System.Windows.Forms.ToolStripStatusLabel tsslblItemCountCaption;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripStatusLabel tsslblDebug;
        
        
        /// <summary>
        /// Disposes resources used by the form.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing) {
                if (components != null) {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }
        
        /// <summary>
        /// This method is required for Windows Forms designer support.
        /// Do not change the method contents inside the source code editor. The Forms designer might
        /// not be able to load this method if it was changed manually.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.dgv = new System.Windows.Forms.DataGridView();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbtnfile = new System.Windows.Forms.ToolStripDropDownButton();
            this.tsmiCreateNewUi = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiFileOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiSaveFile = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiFileSaveAs = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiRefUiItemsFileOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.panelUiView = new System.Windows.Forms.Panel();
            this.btnYon = new System.Windows.Forms.Button();
            this.btnBon = new System.Windows.Forms.Button();
            this.btnGon = new System.Windows.Forms.Button();
            this.btnRon = new System.Windows.Forms.Button();
            this.btnYoff = new System.Windows.Forms.Button();
            this.btnBoff = new System.Windows.Forms.Button();
            this.btnGoff = new System.Windows.Forms.Button();
            this.btnRoff = new System.Windows.Forms.Button();
            this.cmsEditor = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiAdd = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiRemoveUiItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiCopyUiItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiPasteUiItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dgvUiItem = new System.Windows.Forms.DataGridView();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tsslblItemCountCaption = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsslblItemCount = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsslblDebug = new System.Windows.Forms.ToolStripStatusLabel();
            this.customSlider22 = new CustomSlider2();
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.panelUiView.SuspendLayout();
            this.cmsEditor.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUiItem)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgv
            // 
            this.dgv.AllowUserToAddRows = false;
            this.dgv.AllowUserToDeleteRows = false;
            this.dgv.AllowUserToResizeRows = false;
            this.dgv.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.dgv.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgv.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgv.Location = new System.Drawing.Point(0, 32);
            this.dgv.Name = "dgv";
            this.dgv.RowHeadersVisible = false;
            this.dgv.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv.ShowCellToolTips = false;
            this.dgv.Size = new System.Drawing.Size(284, 536);
            this.dgv.TabIndex = 0;
            this.dgv.DataSourceChanged += new System.EventHandler(this.dgv_DataSourceChanged);
            this.dgv.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.dgv_CellBeginEdit);
            this.dgv.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_CellClick);
            this.dgv.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_CellValueChanged);
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbtnfile,
            this.toolStripSeparator1});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1088, 25);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsbtnfile
            // 
            this.tsbtnfile.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbtnfile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiCreateNewUi,
            this.toolStripMenuItem1,
            this.tsmiFileOpen,
            this.toolStripMenuItem2,
            this.tsmiSaveFile,
            this.toolStripMenuItem4,
            this.tsmiFileSaveAs,
            this.toolStripMenuItem3,
            this.tsmiRefUiItemsFileOpen});
            this.tsbtnfile.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnfile.Image")));
            this.tsbtnfile.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnfile.Name = "tsbtnfile";
            this.tsbtnfile.Size = new System.Drawing.Size(38, 22);
            this.tsbtnfile.Text = "&File";
            // 
            // tsmiCreateNewUi
            // 
            this.tsmiCreateNewUi.Name = "tsmiCreateNewUi";
            this.tsmiCreateNewUi.Size = new System.Drawing.Size(188, 22);
            this.tsmiCreateNewUi.Text = "&New";
            this.tsmiCreateNewUi.Click += new System.EventHandler(this.tsmiCreateNewUi_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(185, 6);
            // 
            // tsmiFileOpen
            // 
            this.tsmiFileOpen.Name = "tsmiFileOpen";
            this.tsmiFileOpen.Size = new System.Drawing.Size(188, 22);
            this.tsmiFileOpen.Text = "&Open";
            this.tsmiFileOpen.Click += new System.EventHandler(this.tsmiFileOpen_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(185, 6);
            // 
            // tsmiSaveFile
            // 
            this.tsmiSaveFile.Enabled = false;
            this.tsmiSaveFile.Name = "tsmiSaveFile";
            this.tsmiSaveFile.Size = new System.Drawing.Size(188, 22);
            this.tsmiSaveFile.Text = "Save";
            this.tsmiSaveFile.Click += new System.EventHandler(this.tsmiSaveFile_Click);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(185, 6);
            // 
            // tsmiFileSaveAs
            // 
            this.tsmiFileSaveAs.Name = "tsmiFileSaveAs";
            this.tsmiFileSaveAs.Size = new System.Drawing.Size(188, 22);
            this.tsmiFileSaveAs.Text = "Save &As";
            this.tsmiFileSaveAs.Click += new System.EventHandler(this.tsmiFileSaveAs_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(185, 6);
            // 
            // tsmiRefUiItemsFileOpen
            // 
            this.tsmiRefUiItemsFileOpen.Name = "tsmiRefUiItemsFileOpen";
            this.tsmiRefUiItemsFileOpen.Size = new System.Drawing.Size(188, 22);
            this.tsmiRefUiItemsFileOpen.Text = "Open UiItems Ref. file";
            this.tsmiRefUiItemsFileOpen.Click += new System.EventHandler(this.tsmiRefUiItemsFileOpen_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // panelUiView
            // 
            this.panelUiView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelUiView.BackColor = System.Drawing.SystemColors.Control;
            this.panelUiView.Controls.Add(this.btnYon);
            this.panelUiView.Controls.Add(this.btnBon);
            this.panelUiView.Controls.Add(this.btnGon);
            this.panelUiView.Controls.Add(this.btnRon);
            this.panelUiView.Controls.Add(this.btnYoff);
            this.panelUiView.Controls.Add(this.btnBoff);
            this.panelUiView.Controls.Add(this.btnGoff);
            this.panelUiView.Controls.Add(this.btnRoff);
            this.panelUiView.Location = new System.Drawing.Point(290, 32);
            this.panelUiView.Name = "panelUiView";
            this.panelUiView.Size = new System.Drawing.Size(347, 536);
            this.panelUiView.TabIndex = 0;
            // 
            // btnYon
            // 
            this.btnYon.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(252)))), ((int)(((byte)(19)))));
            this.btnYon.Location = new System.Drawing.Point(137, 300);
            this.btnYon.Name = "btnYon";
            this.btnYon.Size = new System.Drawing.Size(76, 74);
            this.btnYon.TabIndex = 7;
            this.btnYon.Text = "button5";
            this.btnYon.UseVisualStyleBackColor = false;
            // 
            // btnBon
            // 
            this.btnBon.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(89)))), ((int)(((byte)(89)))), ((int)(((byte)(255)))));
            this.btnBon.Location = new System.Drawing.Point(137, 220);
            this.btnBon.Name = "btnBon";
            this.btnBon.Size = new System.Drawing.Size(76, 74);
            this.btnBon.TabIndex = 6;
            this.btnBon.Text = "button6";
            this.btnBon.UseVisualStyleBackColor = false;
            // 
            // btnGon
            // 
            this.btnGon.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(255)))), ((int)(((byte)(18)))));
            this.btnGon.Location = new System.Drawing.Point(137, 140);
            this.btnGon.Name = "btnGon";
            this.btnGon.Size = new System.Drawing.Size(76, 74);
            this.btnGon.TabIndex = 5;
            this.btnGon.Text = "button7";
            this.btnGon.UseVisualStyleBackColor = false;
            // 
            // btnRon
            // 
            this.btnRon.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(89)))), ((int)(((byte)(89)))));
            this.btnRon.Location = new System.Drawing.Point(137, 60);
            this.btnRon.Name = "btnRon";
            this.btnRon.Size = new System.Drawing.Size(76, 74);
            this.btnRon.TabIndex = 4;
            this.btnRon.Text = "button8";
            this.btnRon.UseVisualStyleBackColor = false;
            // 
            // btnYoff
            // 
            this.btnYoff.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(106)))), ((int)(((byte)(105)))), ((int)(((byte)(0)))));
            this.btnYoff.Location = new System.Drawing.Point(55, 300);
            this.btnYoff.Name = "btnYoff";
            this.btnYoff.Size = new System.Drawing.Size(76, 74);
            this.btnYoff.TabIndex = 3;
            this.btnYoff.Text = "button4";
            this.btnYoff.UseVisualStyleBackColor = false;
            // 
            // btnBoff
            // 
            this.btnBoff.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(106)))));
            this.btnBoff.Location = new System.Drawing.Point(55, 220);
            this.btnBoff.Name = "btnBoff";
            this.btnBoff.Size = new System.Drawing.Size(76, 74);
            this.btnBoff.TabIndex = 2;
            this.btnBoff.Text = "button3";
            this.btnBoff.UseVisualStyleBackColor = false;
            // 
            // btnGoff
            // 
            this.btnGoff.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(0)))));
            this.btnGoff.Location = new System.Drawing.Point(55, 140);
            this.btnGoff.Name = "btnGoff";
            this.btnGoff.Size = new System.Drawing.Size(76, 74);
            this.btnGoff.TabIndex = 1;
            this.btnGoff.Text = "button2";
            this.btnGoff.UseVisualStyleBackColor = false;
            // 
            // btnRoff
            // 
            this.btnRoff.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(106)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnRoff.Location = new System.Drawing.Point(55, 60);
            this.btnRoff.Name = "btnRoff";
            this.btnRoff.Size = new System.Drawing.Size(76, 74);
            this.btnRoff.TabIndex = 0;
            this.btnRoff.Text = "button1";
            this.btnRoff.UseVisualStyleBackColor = false;
            // 
            // cmsEditor
            // 
            this.cmsEditor.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.cmsEditor.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiAdd,
            this.toolStripMenuItem5,
            this.tsmiRemoveUiItem,
            this.tsmiCopyUiItem,
            this.tsmiPasteUiItem});
            this.cmsEditor.Name = "cmsEditor";
            this.cmsEditor.Size = new System.Drawing.Size(118, 98);
            // 
            // tsmiAdd
            // 
            this.tsmiAdd.Name = "tsmiAdd";
            this.tsmiAdd.Size = new System.Drawing.Size(117, 22);
            this.tsmiAdd.Text = "Add";
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(114, 6);
            // 
            // tsmiRemoveUiItem
            // 
            this.tsmiRemoveUiItem.Name = "tsmiRemoveUiItem";
            this.tsmiRemoveUiItem.Size = new System.Drawing.Size(117, 22);
            this.tsmiRemoveUiItem.Text = "Remove";
            this.tsmiRemoveUiItem.Click += new System.EventHandler(this.tsmiRemoveUiItems_Click);
            // 
            // tsmiCopyUiItem
            // 
            this.tsmiCopyUiItem.Name = "tsmiCopyUiItem";
            this.tsmiCopyUiItem.Size = new System.Drawing.Size(117, 22);
            this.tsmiCopyUiItem.Text = "Copy";
            this.tsmiCopyUiItem.Click += new System.EventHandler(this.tsmiCopyUiItem_Click);
            // 
            // tsmiPasteUiItem
            // 
            this.tsmiPasteUiItem.Name = "tsmiPasteUiItem";
            this.tsmiPasteUiItem.Size = new System.Drawing.Size(117, 22);
            this.tsmiPasteUiItem.Text = "Paste";
            this.tsmiPasteUiItem.Click += new System.EventHandler(this.tsmiPasteUiItem_Click);
            // 
            // dgvUiItem
            // 
            this.dgvUiItem.AllowUserToAddRows = false;
            this.dgvUiItem.AllowUserToDeleteRows = false;
            this.dgvUiItem.AllowUserToResizeRows = false;
            this.dgvUiItem.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvUiItem.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvUiItem.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvUiItem.Location = new System.Drawing.Point(643, 32);
            this.dgvUiItem.MultiSelect = false;
            this.dgvUiItem.Name = "dgvUiItem";
            this.dgvUiItem.RowHeadersVisible = false;
            this.dgvUiItem.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvUiItem.ShowCellToolTips = false;
            this.dgvUiItem.Size = new System.Drawing.Size(227, 536);
            this.dgvUiItem.TabIndex = 2;
            this.dgvUiItem.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.dgvUiItem_CellBeginEdit);
            this.dgvUiItem.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvUiItem_CellClick);
            this.dgvUiItem.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvUiItem_CellValueChanged);
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsslblItemCountCaption,
            this.tsslblItemCount,
            this.toolStripStatusLabel2,
            this.tsslblDebug});
            this.statusStrip1.Location = new System.Drawing.Point(0, 566);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1088, 27);
            this.statusStrip1.TabIndex = 4;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tsslblItemCountCaption
            // 
            this.tsslblItemCountCaption.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsslblItemCountCaption.Name = "tsslblItemCountCaption";
            this.tsslblItemCountCaption.Size = new System.Drawing.Size(118, 22);
            this.tsslblItemCountCaption.Text = "Item Count:";
            // 
            // tsslblItemCount
            // 
            this.tsslblItemCount.Font = new System.Drawing.Font("Courier New", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsslblItemCount.Name = "tsslblItemCount";
            this.tsslblItemCount.Size = new System.Drawing.Size(21, 22);
            this.tsslblItemCount.Text = "0";
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(12, 22);
            this.toolStripStatusLabel2.Text = "-";
            // 
            // tsslblDebug
            // 
            this.tsslblDebug.Name = "tsslblDebug";
            this.tsslblDebug.Size = new System.Drawing.Size(57, 22);
            this.tsslblDebug.Text = "<debug>";
            // 
            // customSlider22
            // 
            this.customSlider22.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.customSlider22.BackColor = System.Drawing.Color.Yellow;
            this.customSlider22.ForeColor = System.Drawing.Color.Black;
            this.customSlider22.GraphicsInterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBilinear;
            this.customSlider22.GraphicsPixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
            this.customSlider22.GraphicsSmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            this.customSlider22.Location = new System.Drawing.Point(40, 15);
            this.customSlider22.Name = "customSlider22";
            this.customSlider22.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.customSlider22.Size = new System.Drawing.Size(75, 350);
            this.customSlider22.SliderBarColor = System.Drawing.Color.Red;
            this.customSlider22.TabIndex = 8;
            this.customSlider22.Text = "customSlider22";
            this.customSlider22.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            this.customSlider22.Value = 64;
            this.customSlider22.ValueMax = 100;
            this.customSlider22.ValueMin = 0;
            this.customSlider22.ValueSteps = 1;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.customSlider22);
            this.panel1.Location = new System.Drawing.Point(915, 32);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(161, 417);
            this.panel1.TabIndex = 9;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1088, 593);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panelUiView);
            this.Controls.Add(this.dgvUiItem);
            this.Controls.Add(this.dgv);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.toolStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "RoboRemoPC";
            this.Shown += new System.EventHandler(this.this_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.panelUiView.ResumeLayout(false);
            this.cmsEditor.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvUiItem)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.ToolStripMenuItem tsmiFileSaveAs;
        private System.Windows.Forms.ToolStripMenuItem tsmiRefUiItemsFileOpen;
        private System.Windows.Forms.ContextMenuStrip cmsEditor;
        private System.Windows.Forms.ToolStripMenuItem tsmiRemoveUiItem;
        private System.Windows.Forms.ToolStripMenuItem tsmiCopyUiItem;
        private System.Windows.Forms.ToolStripMenuItem tsmiPasteUiItem;
        private System.Windows.Forms.ToolStripMenuItem tsmiCreateNewUi;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem tsmiAdd;
        private System.Windows.Forms.ToolStripMenuItem tsmiSaveFile;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem4;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem5;
        private System.Windows.Forms.Button btnYon;
        private System.Windows.Forms.Button btnBon;
        private System.Windows.Forms.Button btnGon;
        private System.Windows.Forms.Button btnRon;
        private System.Windows.Forms.Button btnYoff;
        private System.Windows.Forms.Button btnBoff;
        private System.Windows.Forms.Button btnGoff;
        private System.Windows.Forms.Button btnRoff;
        private CustomSlider2 customSlider22;
        private System.Windows.Forms.Panel panel1;
    }
}
