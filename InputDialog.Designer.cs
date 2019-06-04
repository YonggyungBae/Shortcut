namespace Shortcut
{
    partial class FrmInputDialog
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
            this.grpCmd = new System.Windows.Forms.GroupBox();
            this.txtCmd = new System.Windows.Forms.TextBox();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.btnFile = new System.Windows.Forms.Button();
            this.btnFolder = new System.Windows.Forms.Button();
            this.grpRun = new System.Windows.Forms.GroupBox();
            this.cboArguments = new System.Windows.Forms.TextBox();
            this.cboPath = new System.Windows.Forms.TextBox();
            this.btnClear = new System.Windows.Forms.Button();
            this.lblArguments = new System.Windows.Forms.Label();
            this.lblPath = new System.Windows.Forms.Label();
            this.chkRun = new System.Windows.Forms.CheckBox();
            this.grpCmd.SuspendLayout();
            this.grpRun.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpCmd
            // 
            this.grpCmd.Controls.Add(this.txtCmd);
            this.grpCmd.Location = new System.Drawing.Point(12, 12);
            this.grpCmd.Name = "grpCmd";
            this.grpCmd.Size = new System.Drawing.Size(353, 48);
            this.grpCmd.TabIndex = 0;
            this.grpCmd.TabStop = false;
            this.grpCmd.Text = "&Command";
            // 
            // txtCmd
            // 
            this.txtCmd.Location = new System.Drawing.Point(6, 17);
            this.txtCmd.Name = "txtCmd";
            this.txtCmd.Size = new System.Drawing.Size(341, 21);
            this.txtCmd.TabIndex = 0;
            // 
            // btnOk
            // 
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOk.Location = new System.Drawing.Point(12, 178);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 3;
            this.btnOk.Text = "&OK";
            this.btnOk.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(93, 178);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "Ca&ncel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // openFileDialog
            // 
            this.openFileDialog.Title = "Open";
            // 
            // btnFile
            // 
            this.btnFile.Location = new System.Drawing.Point(110, 20);
            this.btnFile.Name = "btnFile";
            this.btnFile.Size = new System.Drawing.Size(75, 23);
            this.btnFile.TabIndex = 3;
            this.btnFile.Text = "&File";
            this.btnFile.UseVisualStyleBackColor = true;
            this.btnFile.Click += new System.EventHandler(this.BtnFile_Click);
            // 
            // btnFolder
            // 
            this.btnFolder.Location = new System.Drawing.Point(191, 20);
            this.btnFolder.Name = "btnFolder";
            this.btnFolder.Size = new System.Drawing.Size(75, 23);
            this.btnFolder.TabIndex = 4;
            this.btnFolder.Text = "Fol&der";
            this.btnFolder.UseVisualStyleBackColor = true;
            this.btnFolder.Click += new System.EventHandler(this.BtnFolder_Click);
            // 
            // grpRun
            // 
            this.grpRun.Controls.Add(this.cboArguments);
            this.grpRun.Controls.Add(this.cboPath);
            this.grpRun.Controls.Add(this.btnClear);
            this.grpRun.Controls.Add(this.lblArguments);
            this.grpRun.Controls.Add(this.lblPath);
            this.grpRun.Controls.Add(this.btnFolder);
            this.grpRun.Controls.Add(this.btnFile);
            this.grpRun.Enabled = false;
            this.grpRun.Location = new System.Drawing.Point(12, 66);
            this.grpRun.Name = "grpRun";
            this.grpRun.Size = new System.Drawing.Size(353, 106);
            this.grpRun.TabIndex = 2;
            this.grpRun.TabStop = false;
            // 
            // cboArguments
            // 
            this.cboArguments.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cboArguments.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.cboArguments.Location = new System.Drawing.Point(86, 76);
            this.cboArguments.Name = "cboArguments";
            this.cboArguments.Size = new System.Drawing.Size(261, 21);
            this.cboArguments.TabIndex = 9;
            // 
            // cboPath
            // 
            this.cboPath.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cboPath.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.cboPath.Location = new System.Drawing.Point(50, 49);
            this.cboPath.Name = "cboPath";
            this.cboPath.Size = new System.Drawing.Size(297, 21);
            this.cboPath.TabIndex = 7;
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(272, 20);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 23);
            this.btnClear.TabIndex = 5;
            this.btnClear.Text = "Cl&ear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // lblArguments
            // 
            this.lblArguments.AutoSize = true;
            this.lblArguments.Location = new System.Drawing.Point(6, 79);
            this.lblArguments.Name = "lblArguments";
            this.lblArguments.Size = new System.Drawing.Size(74, 12);
            this.lblArguments.TabIndex = 8;
            this.lblArguments.Text = "&Arguments :";
            // 
            // lblPath
            // 
            this.lblPath.AutoSize = true;
            this.lblPath.Location = new System.Drawing.Point(6, 52);
            this.lblPath.Name = "lblPath";
            this.lblPath.Size = new System.Drawing.Size(38, 12);
            this.lblPath.TabIndex = 6;
            this.lblPath.Text = "&Path :";
            // 
            // chkRun
            // 
            this.chkRun.AutoSize = true;
            this.chkRun.Location = new System.Drawing.Point(10, 67);
            this.chkRun.Name = "chkRun";
            this.chkRun.Size = new System.Drawing.Size(46, 16);
            this.chkRun.TabIndex = 1;
            this.chkRun.Text = "&Run";
            this.chkRun.UseVisualStyleBackColor = true;
            this.chkRun.CheckedChanged += new System.EventHandler(this.chkRun_CheckedChanged);
            // 
            // FrmInputDialog
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(378, 211);
            this.Controls.Add(this.chkRun);
            this.Controls.Add(this.grpRun);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.grpCmd);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmInputDialog";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Add / Edit Command";
            this.grpCmd.ResumeLayout(false);
            this.grpCmd.PerformLayout();
            this.grpRun.ResumeLayout(false);
            this.grpRun.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox grpCmd;
        private System.Windows.Forms.TextBox txtCmd;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.Button btnFile;
        private System.Windows.Forms.Button btnFolder;
        private System.Windows.Forms.GroupBox grpRun;
        private System.Windows.Forms.Label lblPath;
        private System.Windows.Forms.Label lblArguments;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.CheckBox chkRun;
        private System.Windows.Forms.TextBox cboPath;
        private System.Windows.Forms.TextBox cboArguments;
    }
}