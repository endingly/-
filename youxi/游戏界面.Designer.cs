namespace youxi
{
    partial class 游戏界面
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
            this.pbMainPalette = new System.Windows.Forms.PictureBox();
            this.pbNextPalette = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblLevel = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pbMainPalette)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbNextPalette)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblLevel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pbMainPalette
            // 
            this.pbMainPalette.Location = new System.Drawing.Point(12, 12);
            this.pbMainPalette.Name = "pbMainPalette";
            this.pbMainPalette.Size = new System.Drawing.Size(335, 546);
            this.pbMainPalette.TabIndex = 0;
            this.pbMainPalette.TabStop = false;
            this.pbMainPalette.Tag = "pbMainPalette";
            this.pbMainPalette.Paint += new System.Windows.Forms.PaintEventHandler(this.pbMainPalette_Paint);
            // 
            // pbNextPalette
            // 
            this.pbNextPalette.Location = new System.Drawing.Point(353, 12);
            this.pbNextPalette.Name = "pbNextPalette";
            this.pbNextPalette.Size = new System.Drawing.Size(177, 183);
            this.pbNextPalette.TabIndex = 1;
            this.pbNextPalette.TabStop = false;
            this.pbNextPalette.Tag = "pbNextPalette";
            this.pbNextPalette.Paint += new System.Windows.Forms.PaintEventHandler(this.pbNextPalette_Paint);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(354, 223);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 15);
            this.label1.TabIndex = 2;
            this.label1.Tag = "label1";
            this.label1.Text = "等级:";
            // 
            // lblLevel
            // 
            this.lblLevel.Location = new System.Drawing.Point(405, 201);
            this.lblLevel.Name = "lblLevel";
            this.lblLevel.Size = new System.Drawing.Size(100, 50);
            this.lblLevel.TabIndex = 3;
            this.lblLevel.TabStop = false;
            this.lblLevel.Tag = "lblLevel";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(354, 273);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 15);
            this.label2.TabIndex = 4;
            this.label2.Text = "分数：";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(405, 257);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(100, 44);
            this.pictureBox1.TabIndex = 5;
            this.pictureBox1.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(354, 428);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(82, 15);
            this.label3.TabIndex = 6;
            this.label3.Text = "游戏介绍：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(357, 447);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(98, 15);
            this.label4.TabIndex = 7;
            this.label4.Text = "F2：开始游戏";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(357, 462);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(106, 15);
            this.label5.TabIndex = 8;
            this.label5.Text = "F3：暂停/继续";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(357, 481);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(135, 15);
            this.label6.TabIndex = 9;
            this.label6.Text = "方向键：移动/旋转";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(360, 500);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(82, 15);
            this.label7.TabIndex = 10;
            this.label7.Text = "空格：直下";
            // 
            // 游戏界面
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(542, 570);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblLevel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pbNextPalette);
            this.Controls.Add(this.pbMainPalette);
            this.KeyPreview = true;
            this.Name = "游戏界面";
            this.Text = "游戏界面";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormMain_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.pbMainPalette)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbNextPalette)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblLevel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pbMainPalette;
        private System.Windows.Forms.PictureBox pbNextPalette;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox lblLevel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
    }
}