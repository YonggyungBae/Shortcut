namespace Shortcut
{
    partial class FrmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            this.TxtCmdBox = new System.Windows.Forms.TextBox();
            this.BtnRun = new System.Windows.Forms.Button();
            this.TreeView = new System.Windows.Forms.TreeView();
            this.contextMenuTreeView = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem_Add = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_Edit = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_Del = new System.Windows.Forms.ToolStripMenuItem();
            this.GrpCmdBox = new System.Windows.Forms.GroupBox();
            this.GrpBackgroundBox = new System.Windows.Forms.GroupBox();
            this.GrpCmdTree = new System.Windows.Forms.GroupBox();
            this.NotifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuNotifyIcon = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem_NotifyIcon_Open = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_NotifyIcon_Config = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_NotifyIcon_Exit = new System.Windows.Forms.ToolStripMenuItem();
            this.tmrNodeOver = new System.Windows.Forms.Timer(this.components);
            this.contextMenuTreeView.SuspendLayout();
            this.GrpCmdBox.SuspendLayout();
            this.GrpBackgroundBox.SuspendLayout();
            this.GrpCmdTree.SuspendLayout();
            this.contextMenuNotifyIcon.SuspendLayout();
            this.SuspendLayout();
            // 
            // TxtCmdBox
            // 
            this.TxtCmdBox.BackColor = System.Drawing.Color.Black;
            this.TxtCmdBox.Dock = System.Windows.Forms.DockStyle.Left;
            this.TxtCmdBox.Font = new System.Drawing.Font("Malgun Gothic", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.TxtCmdBox.ForeColor = System.Drawing.Color.White;
            this.TxtCmdBox.Location = new System.Drawing.Point(3, 20);
            this.TxtCmdBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.TxtCmdBox.Name = "TxtCmdBox";
            this.TxtCmdBox.Size = new System.Drawing.Size(252, 33);
            this.TxtCmdBox.TabIndex = 0;
            // 
            // BtnRun
            // 
            this.BtnRun.Dock = System.Windows.Forms.DockStyle.Right;
            this.BtnRun.Location = new System.Drawing.Point(259, 20);
            this.BtnRun.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.BtnRun.MaximumSize = new System.Drawing.Size(75, 29);
            this.BtnRun.MinimumSize = new System.Drawing.Size(75, 29);
            this.BtnRun.Name = "BtnRun";
            this.BtnRun.Size = new System.Drawing.Size(75, 29);
            this.BtnRun.TabIndex = 1;
            this.BtnRun.Text = "&Run";
            this.BtnRun.UseVisualStyleBackColor = true;
            this.BtnRun.Click += new System.EventHandler(this.BtnRun_Click);
            // 
            // TreeView
            // 
            this.TreeView.AllowDrop = true;
            this.TreeView.BackColor = System.Drawing.Color.Black;
            this.TreeView.ContextMenuStrip = this.contextMenuTreeView;
            this.TreeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TreeView.Font = new System.Drawing.Font("Malgun Gothic", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.TreeView.ForeColor = System.Drawing.Color.White;
            this.TreeView.HideSelection = false;
            this.TreeView.Location = new System.Drawing.Point(3, 20);
            this.TreeView.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.TreeView.Name = "TreeView";
            this.TreeView.Size = new System.Drawing.Size(331, 384);
            this.TreeView.TabIndex = 0;
            this.TreeView.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.TreeView_ItemDrag);
            this.TreeView.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.TreeView_NodeMouseDoubleClick);
            this.TreeView.DragDrop += new System.Windows.Forms.DragEventHandler(this.TreeView_DragDrop);
            this.TreeView.DragEnter += new System.Windows.Forms.DragEventHandler(this.TreeView_DragEnter);
            this.TreeView.DragOver += new System.Windows.Forms.DragEventHandler(this.TreeView_DragOver);
            this.TreeView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TreeView_KeyDown);
            this.TreeView.MouseClick += new System.Windows.Forms.MouseEventHandler(this.TreeView_MouseClick);
            this.TreeView.MouseDown += new System.Windows.Forms.MouseEventHandler(this.TreeView_MouseDown);
            // 
            // contextMenuTreeView
            // 
            this.contextMenuTreeView.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem_Add,
            this.toolStripMenuItem_Edit,
            this.toolStripMenuItem_Del});
            this.contextMenuTreeView.Name = "ContextMenu_ToTreeView";
            this.contextMenuTreeView.Size = new System.Drawing.Size(109, 70);
            this.contextMenuTreeView.Opening += new System.ComponentModel.CancelEventHandler(this.ContextMenu_Opening);
            // 
            // toolStripMenuItem_Add
            // 
            this.toolStripMenuItem_Add.Name = "toolStripMenuItem_Add";
            this.toolStripMenuItem_Add.Size = new System.Drawing.Size(108, 22);
            this.toolStripMenuItem_Add.Text = "&Add";
            this.toolStripMenuItem_Add.Click += new System.EventHandler(this.ToolStripMenuItem_Add_Click);
            // 
            // toolStripMenuItem_Edit
            // 
            this.toolStripMenuItem_Edit.Name = "toolStripMenuItem_Edit";
            this.toolStripMenuItem_Edit.Size = new System.Drawing.Size(108, 22);
            this.toolStripMenuItem_Edit.Text = "&Edit";
            this.toolStripMenuItem_Edit.Click += new System.EventHandler(this.ToolStripMenuItem_Edit_Click);
            // 
            // toolStripMenuItem_Del
            // 
            this.toolStripMenuItem_Del.Name = "toolStripMenuItem_Del";
            this.toolStripMenuItem_Del.Size = new System.Drawing.Size(108, 22);
            this.toolStripMenuItem_Del.Text = "&Delete";
            this.toolStripMenuItem_Del.Click += new System.EventHandler(this.ToolStripMenuItem_Del_Click);
            // 
            // GrpCmdBox
            // 
            this.GrpCmdBox.AutoSize = true;
            this.GrpCmdBox.Controls.Add(this.TxtCmdBox);
            this.GrpCmdBox.Controls.Add(this.BtnRun);
            this.GrpCmdBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.GrpCmdBox.Enabled = false;
            this.GrpCmdBox.Location = new System.Drawing.Point(3, 20);
            this.GrpCmdBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.GrpCmdBox.Name = "GrpCmdBox";
            this.GrpCmdBox.Padding = new System.Windows.Forms.Padding(3, 4, 3, 44);
            this.GrpCmdBox.Size = new System.Drawing.Size(337, 64);
            this.GrpCmdBox.TabIndex = 0;
            this.GrpCmdBox.TabStop = false;
            this.GrpCmdBox.Text = "Command";
            this.GrpCmdBox.Visible = false;
            // 
            // GrpBackgroundBox
            // 
            this.GrpBackgroundBox.Controls.Add(this.GrpCmdTree);
            this.GrpBackgroundBox.Controls.Add(this.GrpCmdBox);
            this.GrpBackgroundBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GrpBackgroundBox.Location = new System.Drawing.Point(0, 0);
            this.GrpBackgroundBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.GrpBackgroundBox.Name = "GrpBackgroundBox";
            this.GrpBackgroundBox.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.GrpBackgroundBox.Size = new System.Drawing.Size(343, 496);
            this.GrpBackgroundBox.TabIndex = 0;
            this.GrpBackgroundBox.TabStop = false;
            // 
            // GrpCmdTree
            // 
            this.GrpCmdTree.BackColor = System.Drawing.SystemColors.Control;
            this.GrpCmdTree.Controls.Add(this.TreeView);
            this.GrpCmdTree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GrpCmdTree.Location = new System.Drawing.Point(3, 84);
            this.GrpCmdTree.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.GrpCmdTree.Name = "GrpCmdTree";
            this.GrpCmdTree.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.GrpCmdTree.Size = new System.Drawing.Size(337, 408);
            this.GrpCmdTree.TabIndex = 1;
            this.GrpCmdTree.TabStop = false;
            // 
            // NotifyIcon
            // 
            this.NotifyIcon.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.NotifyIcon.ContextMenuStrip = this.contextMenuNotifyIcon;
            this.NotifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("NotifyIcon.Icon")));
            this.NotifyIcon.Text = "Shortcut.\r\nPress \"Win + Y\" keys to open Shortcut";
            this.NotifyIcon.Visible = true;
            this.NotifyIcon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.NotifyIcon_MouseDoubleClick);
            // 
            // contextMenuNotifyIcon
            // 
            this.contextMenuNotifyIcon.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem_NotifyIcon_Open,
            this.toolStripMenuItem_NotifyIcon_Config,
            this.aboutToolStripMenuItem,
            this.toolStripMenuItem_NotifyIcon_Exit});
            this.contextMenuNotifyIcon.Name = "contextMenuNotifyIcon";
            this.contextMenuNotifyIcon.Size = new System.Drawing.Size(112, 92);
            // 
            // toolStripMenuItem_NotifyIcon_Open
            // 
            this.toolStripMenuItem_NotifyIcon_Open.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMenuItem_NotifyIcon_Open.Image")));
            this.toolStripMenuItem_NotifyIcon_Open.Name = "toolStripMenuItem_NotifyIcon_Open";
            this.toolStripMenuItem_NotifyIcon_Open.Size = new System.Drawing.Size(111, 22);
            this.toolStripMenuItem_NotifyIcon_Open.Text = "&Open";
            this.toolStripMenuItem_NotifyIcon_Open.Click += new System.EventHandler(this.ToolStripMenuItem_NotifyIcon_Open_Click);
            // 
            // toolStripMenuItem_NotifyIcon_Config
            // 
            this.toolStripMenuItem_NotifyIcon_Config.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMenuItem_NotifyIcon_Config.Image")));
            this.toolStripMenuItem_NotifyIcon_Config.Name = "toolStripMenuItem_NotifyIcon_Config";
            this.toolStripMenuItem_NotifyIcon_Config.Size = new System.Drawing.Size(111, 22);
            this.toolStripMenuItem_NotifyIcon_Config.Text = "Opt&ion";
            this.toolStripMenuItem_NotifyIcon_Config.Click += new System.EventHandler(this.ToolStripMenuItem_NotifyIcon_Config_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("aboutToolStripMenuItem.Image")));
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(111, 22);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.AboutToolStripMenuItem_Click);
            // 
            // toolStripMenuItem_NotifyIcon_Exit
            // 
            this.toolStripMenuItem_NotifyIcon_Exit.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMenuItem_NotifyIcon_Exit.Image")));
            this.toolStripMenuItem_NotifyIcon_Exit.Name = "toolStripMenuItem_NotifyIcon_Exit";
            this.toolStripMenuItem_NotifyIcon_Exit.Size = new System.Drawing.Size(111, 22);
            this.toolStripMenuItem_NotifyIcon_Exit.Text = "&Exit";
            this.toolStripMenuItem_NotifyIcon_Exit.Click += new System.EventHandler(this.ToolStripMenuItem_NotifyIcon_Exit_Click);
            // 
            // tmrNodeOver
            // 
            this.tmrNodeOver.Tick += new System.EventHandler(this.tmrNodeOver_Tick);
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(343, 496);
            this.Controls.Add(this.GrpBackgroundBox);
            this.Font = new System.Drawing.Font("Malgun Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "FrmMain";
            this.Text = "Shortcut";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmMain_FormClosing);
            this.Load += new System.EventHandler(this.FrmMain_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmMain_KeyDown);
            this.contextMenuTreeView.ResumeLayout(false);
            this.GrpCmdBox.ResumeLayout(false);
            this.GrpCmdBox.PerformLayout();
            this.GrpBackgroundBox.ResumeLayout(false);
            this.GrpBackgroundBox.PerformLayout();
            this.GrpCmdTree.ResumeLayout(false);
            this.contextMenuNotifyIcon.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox TxtCmdBox;
        private System.Windows.Forms.Button BtnRun;
        private System.Windows.Forms.TreeView TreeView;
        private System.Windows.Forms.GroupBox GrpCmdBox;
        private System.Windows.Forms.GroupBox GrpBackgroundBox;
        private System.Windows.Forms.GroupBox GrpCmdTree;
        private System.Windows.Forms.ContextMenuStrip contextMenuTreeView;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_Add;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_Del;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_Edit;
        private System.Windows.Forms.NotifyIcon NotifyIcon;
        private System.Windows.Forms.ContextMenuStrip contextMenuNotifyIcon;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_NotifyIcon_Open;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_NotifyIcon_Exit;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_NotifyIcon_Config;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.Timer tmrNodeOver;
    }
}