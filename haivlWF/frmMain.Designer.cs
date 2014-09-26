namespace haivlWSCORE
{
    partial class frmMain
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel_center = new System.Windows.Forms.Panel();
            this.label_uploader = new System.Windows.Forms.Label();
            this.panel_center2 = new System.Windows.Forms.Panel();
            this.picture_main = new System.Windows.Forms.PictureBox();
            this.panel_bottom = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.label_shortcut2 = new System.Windows.Forms.Label();
            this.label_keyshortcut = new System.Windows.Forms.Label();
            this.panel_top = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label_title = new System.Windows.Forms.Label();
            this.label_isvideo = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel_center.SuspendLayout();
            this.panel_center2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picture_main)).BeginInit();
            this.panel_bottom.SuspendLayout();
            this.panel_top.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel_center);
            this.panel1.Controls.Add(this.panel_bottom);
            this.panel1.Controls.Add(this.panel_top);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(954, 549);
            this.panel1.TabIndex = 0;
            // 
            // panel_center
            // 
            this.panel_center.BackColor = System.Drawing.Color.Black;
            this.panel_center.Controls.Add(this.label_uploader);
            this.panel_center.Controls.Add(this.panel_center2);
            this.panel_center.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_center.Location = new System.Drawing.Point(0, 31);
            this.panel_center.Name = "panel_center";
            this.panel_center.Size = new System.Drawing.Size(954, 493);
            this.panel_center.TabIndex = 6;
            // 
            // label_uploader
            // 
            this.label_uploader.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label_uploader.BackColor = System.Drawing.Color.White;
            this.label_uploader.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_uploader.Location = new System.Drawing.Point(664, 467);
            this.label_uploader.Name = "label_uploader";
            this.label_uploader.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label_uploader.Size = new System.Drawing.Size(287, 23);
            this.label_uploader.TabIndex = 1;
            this.label_uploader.Text = "[uploader]";
            // 
            // panel_center2
            // 
            this.panel_center2.AutoScroll = true;
            this.panel_center2.BackColor = System.Drawing.Color.LightGray;
            this.panel_center2.Controls.Add(this.label_isvideo);
            this.panel_center2.Controls.Add(this.picture_main);
            this.panel_center2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_center2.Location = new System.Drawing.Point(0, 0);
            this.panel_center2.Name = "panel_center2";
            this.panel_center2.Size = new System.Drawing.Size(954, 493);
            this.panel_center2.TabIndex = 0;
            // 
            // picture_main
            // 
            this.picture_main.BackColor = System.Drawing.Color.DimGray;
            this.picture_main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picture_main.Image = global::haivlWSCORE.Properties.Resources._20_09_2024_github_graph_QuanLyTaiSan;
            this.picture_main.Location = new System.Drawing.Point(0, 0);
            this.picture_main.Name = "picture_main";
            this.picture_main.Size = new System.Drawing.Size(954, 493);
            this.picture_main.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picture_main.TabIndex = 0;
            this.picture_main.TabStop = false;
            this.picture_main.Click += new System.EventHandler(this.picture_main_Click);
            // 
            // panel_bottom
            // 
            this.panel_bottom.BackColor = System.Drawing.Color.White;
            this.panel_bottom.Controls.Add(this.label1);
            this.panel_bottom.Controls.Add(this.label_shortcut2);
            this.panel_bottom.Controls.Add(this.label_keyshortcut);
            this.panel_bottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel_bottom.Location = new System.Drawing.Point(0, 524);
            this.panel_bottom.Name = "panel_bottom";
            this.panel_bottom.Size = new System.Drawing.Size(954, 25);
            this.panel_bottom.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(810, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(140, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "Exit Fullscreen (ESC)";
            // 
            // label_shortcut2
            // 
            this.label_shortcut2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label_shortcut2.AutoSize = true;
            this.label_shortcut2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_shortcut2.Location = new System.Drawing.Point(387, 5);
            this.label_shortcut2.Name = "label_shortcut2";
            this.label_shortcut2.Size = new System.Drawing.Size(160, 17);
            this.label_shortcut2.TabIndex = 1;
            this.label_shortcut2.Text = "Trước (Left) - Kế (Right)";
            // 
            // label_keyshortcut
            // 
            this.label_keyshortcut.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label_keyshortcut.AutoSize = true;
            this.label_keyshortcut.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_keyshortcut.Location = new System.Drawing.Point(8, 3);
            this.label_keyshortcut.Name = "label_keyshortcut";
            this.label_keyshortcut.Size = new System.Drawing.Size(207, 17);
            this.label_keyshortcut.TabIndex = 0;
            this.label_keyshortcut.Text = "Mới (1) - Bình chọn (2) - Hot (3)";
            // 
            // panel_top
            // 
            this.panel_top.Controls.Add(this.panel3);
            this.panel_top.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel_top.Location = new System.Drawing.Point(0, 0);
            this.panel_top.Name = "panel_top";
            this.panel_top.Size = new System.Drawing.Size(954, 31);
            this.panel_top.TabIndex = 3;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.label_title);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(954, 28);
            this.panel3.TabIndex = 2;
            // 
            // label_title
            // 
            this.label_title.BackColor = System.Drawing.Color.White;
            this.label_title.Dock = System.Windows.Forms.DockStyle.Top;
            this.label_title.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_title.Location = new System.Drawing.Point(0, 0);
            this.label_title.Name = "label_title";
            this.label_title.Padding = new System.Windows.Forms.Padding(0, 2, 0, 0);
            this.label_title.Size = new System.Drawing.Size(954, 28);
            this.label_title.TabIndex = 0;
            this.label_title.Text = "[Title]";
            this.label_title.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label_isvideo
            // 
            this.label_isvideo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label_isvideo.BackColor = System.Drawing.Color.White;
            this.label_isvideo.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_isvideo.Location = new System.Drawing.Point(898, 439);
            this.label_isvideo.Name = "label_isvideo";
            this.label_isvideo.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label_isvideo.Size = new System.Drawing.Size(53, 23);
            this.label_isvideo.TabIndex = 3;
            this.label_isvideo.Text = "[video]";
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(954, 549);
            this.Controls.Add(this.panel1);
            this.KeyPreview = true;
            this.Name = "frmMain";
            this.Text = "haivl no ads for .NET 4.0 (beta 1, not support video)";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.panel1.ResumeLayout(false);
            this.panel_center.ResumeLayout(false);
            this.panel_center2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picture_main)).EndInit();
            this.panel_bottom.ResumeLayout(false);
            this.panel_bottom.PerformLayout();
            this.panel_top.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel_top;
        private System.Windows.Forms.Panel panel_bottom;
        private System.Windows.Forms.Panel panel_center;
        private System.Windows.Forms.Panel panel_center2;
        private System.Windows.Forms.PictureBox picture_main;
        private System.Windows.Forms.Label label_uploader;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label_title;
        private System.Windows.Forms.Label label_keyshortcut;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label_shortcut2;
        private System.Windows.Forms.Label label_isvideo;


    }
}

