namespace Shortcut
{
    partial class Options
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Options));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage_General = new System.Windows.Forms.TabPage();
            this.grpMinimizeToTray = new System.Windows.Forms.GroupBox();
            this.chkMinimizeToTrayClickCloseButton = new System.Windows.Forms.CheckBox();
            this.chkMinimizeToTrayPressEsc = new System.Windows.Forms.CheckBox();
            this.chkMinimizeToTrayAfterRun = new System.Windows.Forms.CheckBox();
            this.chkShowInTaskBar = new System.Windows.Forms.CheckBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnApply = new System.Windows.Forms.Button();
            this.ToolTip_MinimizeToTrayAfterRun = new System.Windows.Forms.ToolTip(this.components);
            this.frmMainBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.chkOpenCmdWithSingleClick = new System.Windows.Forms.CheckBox();
            this.tabControl1.SuspendLayout();
            this.tabPage_General.SuspendLayout();
            this.grpMinimizeToTray.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.frmMainBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage_General);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(307, 211);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage_General
            // 
            this.tabPage_General.Controls.Add(this.grpMinimizeToTray);
            this.tabPage_General.Controls.Add(this.chkOpenCmdWithSingleClick);
            this.tabPage_General.Controls.Add(this.chkShowInTaskBar);
            this.tabPage_General.Location = new System.Drawing.Point(4, 22);
            this.tabPage_General.Name = "tabPage_General";
            this.tabPage_General.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_General.Size = new System.Drawing.Size(299, 185);
            this.tabPage_General.TabIndex = 0;
            this.tabPage_General.Text = "General";
            this.tabPage_General.UseVisualStyleBackColor = true;
            // 
            // grpMinimizeToTray
            // 
            this.grpMinimizeToTray.Controls.Add(this.chkMinimizeToTrayClickCloseButton);
            this.grpMinimizeToTray.Controls.Add(this.chkMinimizeToTrayPressEsc);
            this.grpMinimizeToTray.Controls.Add(this.chkMinimizeToTrayAfterRun);
            this.grpMinimizeToTray.Location = new System.Drawing.Point(6, 82);
            this.grpMinimizeToTray.Name = "grpMinimizeToTray";
            this.grpMinimizeToTray.Size = new System.Drawing.Size(287, 97);
            this.grpMinimizeToTray.TabIndex = 2;
            this.grpMinimizeToTray.TabStop = false;
            this.grpMinimizeToTray.Text = "Minimize to tray";
            // 
            // chkMinimizeToTrayClickCloseButton
            // 
            this.chkMinimizeToTrayClickCloseButton.AutoSize = true;
            this.chkMinimizeToTrayClickCloseButton.Location = new System.Drawing.Point(6, 64);
            this.chkMinimizeToTrayClickCloseButton.Name = "chkMinimizeToTrayClickCloseButton";
            this.chkMinimizeToTrayClickCloseButton.Size = new System.Drawing.Size(273, 16);
            this.chkMinimizeToTrayClickCloseButton.TabIndex = 2;
            this.chkMinimizeToTrayClickCloseButton.Text = "Minimize to tray when click close[X] button";
            this.chkMinimizeToTrayClickCloseButton.UseVisualStyleBackColor = true;
            // 
            // chkMinimizeToTrayPressEsc
            // 
            this.chkMinimizeToTrayPressEsc.AutoSize = true;
            this.chkMinimizeToTrayPressEsc.Location = new System.Drawing.Point(6, 42);
            this.chkMinimizeToTrayPressEsc.Name = "chkMinimizeToTrayPressEsc";
            this.chkMinimizeToTrayPressEsc.Size = new System.Drawing.Size(248, 16);
            this.chkMinimizeToTrayPressEsc.TabIndex = 2;
            this.chkMinimizeToTrayPressEsc.Text = "Minimize to tray when press \'Esc\' key ";
            this.chkMinimizeToTrayPressEsc.UseVisualStyleBackColor = true;
            // 
            // chkMinimizeToTrayAfterRun
            // 
            this.chkMinimizeToTrayAfterRun.AutoSize = true;
            this.chkMinimizeToTrayAfterRun.Location = new System.Drawing.Point(6, 20);
            this.chkMinimizeToTrayAfterRun.Name = "chkMinimizeToTrayAfterRun";
            this.chkMinimizeToTrayAfterRun.Size = new System.Drawing.Size(165, 16);
            this.chkMinimizeToTrayAfterRun.TabIndex = 1;
            this.chkMinimizeToTrayAfterRun.Text = "Minimize to tray after run";
            this.chkMinimizeToTrayAfterRun.UseVisualStyleBackColor = true;
            // 
            // chkShowInTaskBar
            // 
            this.chkShowInTaskBar.AutoSize = true;
            this.chkShowInTaskBar.Checked = true;
            this.chkShowInTaskBar.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkShowInTaskBar.Location = new System.Drawing.Point(6, 6);
            this.chkShowInTaskBar.Name = "chkShowInTaskBar";
            this.chkShowInTaskBar.Size = new System.Drawing.Size(115, 16);
            this.chkShowInTaskBar.TabIndex = 0;
            this.chkShowInTaskBar.Text = "Show in taskbar";
            this.chkShowInTaskBar.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(159, 229);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "Ca&ncel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOk
            // 
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOk.Location = new System.Drawing.Point(78, 229);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 5;
            this.btnOk.Text = "&OK";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.BtnOk_Click);
            // 
            // btnApply
            // 
            this.btnApply.Location = new System.Drawing.Point(240, 229);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(75, 23);
            this.btnApply.TabIndex = 7;
            this.btnApply.Text = "&Apply";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.BtnApply_Click);
            // 
            // ToolTip_MinimizeToTrayAfterRun
            // 
            this.ToolTip_MinimizeToTrayAfterRun.AutomaticDelay = 100;
            this.ToolTip_MinimizeToTrayAfterRun.AutoPopDelay = 3000;
            this.ToolTip_MinimizeToTrayAfterRun.InitialDelay = 100;
            this.ToolTip_MinimizeToTrayAfterRun.ReshowDelay = 20;
            // 
            // frmMainBindingSource
            // 
            this.frmMainBindingSource.DataSource = typeof(Shortcut.FrmMain);
            // 
            // chkOpenCmdWithSingleClick
            // 
            this.chkOpenCmdWithSingleClick.AutoSize = true;
            this.chkOpenCmdWithSingleClick.Checked = true;
            this.chkOpenCmdWithSingleClick.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkOpenCmdWithSingleClick.Location = new System.Drawing.Point(6, 28);
            this.chkOpenCmdWithSingleClick.Name = "chkOpenCmdWithSingleClick";
            this.chkOpenCmdWithSingleClick.Size = new System.Drawing.Size(203, 16);
            this.chkOpenCmdWithSingleClick.TabIndex = 0;
            this.chkOpenCmdWithSingleClick.Text = "Open a node with a single click";
            this.chkOpenCmdWithSingleClick.UseVisualStyleBackColor = true;
            // 
            // Options
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(324, 260);
            this.Controls.Add(this.btnApply);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Options";
            this.ShowInTaskbar = false;
            this.Text = "Options";
            this.tabControl1.ResumeLayout(false);
            this.tabPage_General.ResumeLayout(false);
            this.tabPage_General.PerformLayout();
            this.grpMinimizeToTray.ResumeLayout(false);
            this.grpMinimizeToTray.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.frmMainBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage_General;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.CheckBox chkMinimizeToTrayAfterRun;
        private System.Windows.Forms.CheckBox chkShowInTaskBar;
        private System.Windows.Forms.GroupBox grpMinimizeToTray;
        private System.Windows.Forms.CheckBox chkMinimizeToTrayPressEsc;
        private System.Windows.Forms.ToolTip ToolTip_MinimizeToTrayAfterRun;
        private System.Windows.Forms.CheckBox chkMinimizeToTrayClickCloseButton;
        private System.Windows.Forms.BindingSource frmMainBindingSource;
        private System.Windows.Forms.CheckBox chkOpenCmdWithSingleClick;
    }
}