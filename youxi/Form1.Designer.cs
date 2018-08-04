namespace youxi
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.buttonRepaly = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.buttonLoad = new System.Windows.Forms.Button();
            this.trackBarReviewSpeed = new System.Windows.Forms.TrackBar();
            this.buttonReview = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.文件ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.repalyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.repalyExtendedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reviewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.设置选项ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.风格ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.style1ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.style2ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.style3ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.游戏帮助ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.操作键ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.关于ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.progressBarReview = new System.Windows.Forms.ProgressBar();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.imageList2 = new System.Windows.Forms.ImageList(this.components);
            this.imageList3 = new System.Windows.Forms.ImageList(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.trackBarReviewSpeed)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonRepaly
            // 
            this.buttonRepaly.Location = new System.Drawing.Point(353, 325);
            this.buttonRepaly.Name = "buttonRepaly";
            this.buttonRepaly.Size = new System.Drawing.Size(75, 23);
            this.buttonRepaly.TabIndex = 0;
            this.buttonRepaly.Text = "重新开始";
            this.buttonRepaly.UseVisualStyleBackColor = true;
            this.buttonRepaly.Click += new System.EventHandler(this.buttonReplay_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(353, 368);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(75, 23);
            this.buttonSave.TabIndex = 1;
            this.buttonSave.Text = "保存";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // buttonLoad
            // 
            this.buttonLoad.Location = new System.Drawing.Point(353, 406);
            this.buttonLoad.Name = "buttonLoad";
            this.buttonLoad.Size = new System.Drawing.Size(75, 23);
            this.buttonLoad.TabIndex = 2;
            this.buttonLoad.Text = "载入";
            this.buttonLoad.UseVisualStyleBackColor = true;
            this.buttonLoad.Click += new System.EventHandler(this.buttonLoad_Click);
            // 
            // trackBarReviewSpeed
            // 
            this.trackBarReviewSpeed.Location = new System.Drawing.Point(353, 458);
            this.trackBarReviewSpeed.Maximum = 15;
            this.trackBarReviewSpeed.Minimum = 1;
            this.trackBarReviewSpeed.Name = "trackBarReviewSpeed";
            this.trackBarReviewSpeed.Size = new System.Drawing.Size(104, 56);
            this.trackBarReviewSpeed.TabIndex = 3;
            this.trackBarReviewSpeed.Value = 1;
            this.trackBarReviewSpeed.Scroll += new System.EventHandler(this.trackBar1_Scroll);
            // 
            // buttonReview
            // 
            this.buttonReview.Location = new System.Drawing.Point(353, 520);
            this.buttonReview.Name = "buttonReview";
            this.buttonReview.Size = new System.Drawing.Size(75, 23);
            this.buttonReview.TabIndex = 4;
            this.buttonReview.Text = "Re&view";
            this.buttonReview.UseVisualStyleBackColor = true;
            this.buttonReview.Click += new System.EventHandler(this.buttonReview_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.文件ToolStripMenuItem,
            this.设置选项ToolStripMenuItem,
            this.游戏帮助ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(487, 28);
            this.menuStrip1.TabIndex = 5;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 文件ToolStripMenuItem
            // 
            this.文件ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.repalyToolStripMenuItem,
            this.repalyExtendedToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.loadToolStripMenuItem,
            this.reviewToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.文件ToolStripMenuItem.Name = "文件ToolStripMenuItem";
            this.文件ToolStripMenuItem.Size = new System.Drawing.Size(51, 24);
            this.文件ToolStripMenuItem.Text = "文件";
            // 
            // repalyToolStripMenuItem
            // 
            this.repalyToolStripMenuItem.Name = "repalyToolStripMenuItem";
            this.repalyToolStripMenuItem.Size = new System.Drawing.Size(212, 26);
            this.repalyToolStripMenuItem.Text = "重新&开始";
            this.repalyToolStripMenuItem.Click += new System.EventHandler(this.repalyToolStripMenuItem_Click);
            // 
            // repalyExtendedToolStripMenuItem
            // 
            this.repalyExtendedToolStripMenuItem.Name = "repalyExtendedToolStripMenuItem";
            this.repalyExtendedToolStripMenuItem.Size = new System.Drawing.Size(212, 26);
            this.repalyExtendedToolStripMenuItem.Text = "Repaly(Extended)";
            this.repalyExtendedToolStripMenuItem.Click += new System.EventHandler(this.repalyExtendedToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(212, 26);
            this.saveToolStripMenuItem.Text = "&Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolstripMenuItem_Click);
            // 
            // loadToolStripMenuItem
            // 
            this.loadToolStripMenuItem.Name = "loadToolStripMenuItem";
            this.loadToolStripMenuItem.Size = new System.Drawing.Size(212, 26);
            this.loadToolStripMenuItem.Text = "&Load";
            this.loadToolStripMenuItem.Click += new System.EventHandler(this.loadToolStripMenuItem_Click);
            // 
            // reviewToolStripMenuItem
            // 
            this.reviewToolStripMenuItem.Name = "reviewToolStripMenuItem";
            this.reviewToolStripMenuItem.Size = new System.Drawing.Size(212, 26);
            this.reviewToolStripMenuItem.Text = "Re&view";
            this.reviewToolStripMenuItem.Click += new System.EventHandler(this.reviewToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(212, 26);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exiToolStripMenuItem_Click);
            // 
            // 设置选项ToolStripMenuItem
            // 
            this.设置选项ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.风格ToolStripMenuItem});
            this.设置选项ToolStripMenuItem.Name = "设置选项ToolStripMenuItem";
            this.设置选项ToolStripMenuItem.Size = new System.Drawing.Size(81, 24);
            this.设置选项ToolStripMenuItem.Text = "设置选项";
            // 
            // 风格ToolStripMenuItem
            // 
            this.风格ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.style1ToolStripMenuItem,
            this.style2ToolStripMenuItem,
            this.style3ToolStripMenuItem});
            this.风格ToolStripMenuItem.Name = "风格ToolStripMenuItem";
            this.风格ToolStripMenuItem.Size = new System.Drawing.Size(114, 26);
            this.风格ToolStripMenuItem.Text = "风格";
            // 
            // style1ToolStripMenuItem
            // 
            this.style1ToolStripMenuItem.Name = "style1ToolStripMenuItem";
            this.style1ToolStripMenuItem.Size = new System.Drawing.Size(123, 26);
            this.style1ToolStripMenuItem.Text = "风格1";
            this.style1ToolStripMenuItem.Click += new System.EventHandler(this.style1ToolStripMenuItem_Click);
            // 
            // style2ToolStripMenuItem
            // 
            this.style2ToolStripMenuItem.Name = "style2ToolStripMenuItem";
            this.style2ToolStripMenuItem.Size = new System.Drawing.Size(123, 26);
            this.style2ToolStripMenuItem.Text = "风格2";
            this.style2ToolStripMenuItem.Click += new System.EventHandler(this.style2ToolStripMenuItem_Click);
            // 
            // style3ToolStripMenuItem
            // 
            this.style3ToolStripMenuItem.Name = "style3ToolStripMenuItem";
            this.style3ToolStripMenuItem.Size = new System.Drawing.Size(123, 26);
            this.style3ToolStripMenuItem.Text = "风格3";
            this.style3ToolStripMenuItem.Click += new System.EventHandler(this.style3ToolStripMenuItem_Click);
            // 
            // 游戏帮助ToolStripMenuItem
            // 
            this.游戏帮助ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.操作键ToolStripMenuItem,
            this.关于ToolStripMenuItem});
            this.游戏帮助ToolStripMenuItem.Name = "游戏帮助ToolStripMenuItem";
            this.游戏帮助ToolStripMenuItem.Size = new System.Drawing.Size(81, 24);
            this.游戏帮助ToolStripMenuItem.Text = "游戏帮助";
            // 
            // 操作键ToolStripMenuItem
            // 
            this.操作键ToolStripMenuItem.Name = "操作键ToolStripMenuItem";
            this.操作键ToolStripMenuItem.Size = new System.Drawing.Size(216, 26);
            this.操作键ToolStripMenuItem.Text = "操作键";
            // 
            // 关于ToolStripMenuItem
            // 
            this.关于ToolStripMenuItem.Name = "关于ToolStripMenuItem";
            this.关于ToolStripMenuItem.Size = new System.Drawing.Size(216, 26);
            this.关于ToolStripMenuItem.Text = "关于";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // progressBarReview
            // 
            this.progressBarReview.Location = new System.Drawing.Point(0, 550);
            this.progressBarReview.Name = "progressBarReview";
            this.progressBarReview.Size = new System.Drawing.Size(100, 23);
            this.progressBarReview.TabIndex = 6;
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // imageList2
            // 
            this.imageList2.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList2.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList2.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // imageList3
            // 
            this.imageList3.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList3.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList3.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(487, 570);
            this.Controls.Add(this.progressBarReview);
            this.Controls.Add(this.buttonReview);
            this.Controls.Add(this.trackBarReviewSpeed);
            this.Controls.Add(this.buttonLoad);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.buttonRepaly);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.trackBarReviewSpeed)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonRepaly;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Button buttonLoad;
        private System.Windows.Forms.TrackBar trackBarReviewSpeed;
        private System.Windows.Forms.Button buttonReview;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 文件ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem repalyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem repalyExtendedToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 设置选项ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 游戏帮助ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 操作键ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 关于ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reviewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 风格ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem style1ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem style2ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem style3ToolStripMenuItem;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.ProgressBar progressBarReview;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ImageList imageList2;
        private System.Windows.Forms.ImageList imageList3;
    }
}

