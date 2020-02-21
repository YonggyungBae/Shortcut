namespace Shortcut
{
    partial class FrmSplash
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
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.picTitle = new System.Windows.Forms.PictureBox();
            this.lblCnt = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.picTitle)).BeginInit();
            this.SuspendLayout();
            // 
            // progressBar
            // 
            this.progressBar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.progressBar.Location = new System.Drawing.Point(0, 165);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(774, 11);
            this.progressBar.Step = 1;
            this.progressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressBar.TabIndex = 0;
            // 
            // picTitle
            // 
            this.picTitle.BackgroundImage = global::Shortcut.Properties.Resources.Shortcut_Title;
            this.picTitle.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.picTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picTitle.Location = new System.Drawing.Point(0, 0);
            this.picTitle.Name = "picTitle";
            this.picTitle.Size = new System.Drawing.Size(774, 165);
            this.picTitle.TabIndex = 2;
            this.picTitle.TabStop = false;
            // 
            // lblCnt
            // 
            this.lblCnt.AutoSize = true;
            this.lblCnt.Location = new System.Drawing.Point(724, 150);
            this.lblCnt.Name = "lblCnt";
            this.lblCnt.Size = new System.Drawing.Size(38, 12);
            this.lblCnt.TabIndex = 3;
            this.lblCnt.Text = "Count";
            this.lblCnt.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // FrmSplash
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.ClientSize = new System.Drawing.Size(774, 176);
            this.Controls.Add(this.lblCnt);
            this.Controls.Add(this.picTitle);
            this.Controls.Add(this.progressBar);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmSplash";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Splash";
            this.Load += new System.EventHandler(this.FrmSplash_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picTitle)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.PictureBox picTitle;
        private System.Windows.Forms.Label lblCnt;
    }
}