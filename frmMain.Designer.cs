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
            this.NotifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuNotifyIcon = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem_NotifyIcon_Open = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_NotifyIcon_Config = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_NotifyIcon_Exit = new System.Windows.Forms.ToolStripMenuItem();
            this.tmrNodeOver = new System.Windows.Forms.Timer(this.components);
            this.toolMainTool = new System.Windows.Forms.ToolStrip();
            this.toolItem_Add = new System.Windows.Forms.ToolStripButton();
            this.toolItem_Remove = new System.Windows.Forms.ToolStripButton();
            this.toolItem_Edit = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.GrpBackgroundBox = new System.Windows.Forms.GroupBox();
            this.GrpCmdTree = new System.Windows.Forms.GroupBox();
            this.TreeView = new System.Windows.Forms.TreeView();
            this.GrpCmdBox = new System.Windows.Forms.GroupBox();
            this.TxtCmdBox = new System.Windows.Forms.TextBox();
            this.BtnRun = new System.Windows.Forms.Button();
            this.contextMenuNotifyIcon.SuspendLayout();
            this.toolMainTool.SuspendLayout();
            this.GrpBackgroundBox.SuspendLayout();
            this.GrpCmdTree.SuspendLayout();
            this.GrpCmdBox.SuspendLayout();
            this.SuspendLayout();
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
            this.tmrNodeOver.Interval = 1000;
            this.tmrNodeOver.Tick += new System.EventHandler(this.tmrNodeOver_Tick);
            // 
            // toolMainTool
            // 
            this.toolMainTool.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolMainTool.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolItem_Add,
            this.toolItem_Remove,
            this.toolItem_Edit,
            this.toolStripSeparator1});
            this.toolMainTool.Location = new System.Drawing.Point(0, 0);
            this.toolMainTool.Name = "toolMainTool";
            this.toolMainTool.Size = new System.Drawing.Size(384, 25);
            this.toolMainTool.TabIndex = 1;
            this.toolMainTool.Text = "Tools";
            // 
            // toolItem_Add
            // 
            this.toolItem_Add.Image = ((System.Drawing.Image)(resources.GetObject("toolItem_Add.Image")));
            this.toolItem_Add.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolItem_Add.Name = "toolItem_Add";
            this.toolItem_Add.Size = new System.Drawing.Size(49, 22);
            this.toolItem_Add.Text = "&Add";
            this.toolItem_Add.Click += new System.EventHandler(this.ToolItem_Add_Click);
            // 
            // toolItem_Remove
            // 
            this.toolItem_Remove.Image = ((System.Drawing.Image)(resources.GetObject("toolItem_Remove.Image")));
            this.toolItem_Remove.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolItem_Remove.Name = "toolItem_Remove";
            this.toolItem_Remove.Size = new System.Drawing.Size(70, 22);
            this.toolItem_Remove.Text = "&Remove";
            this.toolItem_Remove.ToolTipText = "Remove";
            this.toolItem_Remove.Click += new System.EventHandler(this.ToolItem_Remove_Click);
            // 
            // toolItem_Edit
            // 
            this.toolItem_Edit.Image = ((System.Drawing.Image)(resources.GetObject("toolItem_Edit.Image")));
            this.toolItem_Edit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolItem_Edit.Name = "toolItem_Edit";
            this.toolItem_Edit.Size = new System.Drawing.Size(47, 22);
            this.toolItem_Edit.Text = "&Edit";
            this.toolItem_Edit.Click += new System.EventHandler(this.ToolItem_Edit_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // GrpBackgroundBox
            // 
            this.GrpBackgroundBox.Controls.Add(this.GrpCmdTree);
            this.GrpBackgroundBox.Controls.Add(this.GrpCmdBox);
            this.GrpBackgroundBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GrpBackgroundBox.Location = new System.Drawing.Point(0, 25);
            this.GrpBackgroundBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.GrpBackgroundBox.Name = "GrpBackgroundBox";
            this.GrpBackgroundBox.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.GrpBackgroundBox.Size = new System.Drawing.Size(384, 437);
            this.GrpBackgroundBox.TabIndex = 2;
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
            this.GrpCmdTree.Size = new System.Drawing.Size(378, 349);
            this.GrpCmdTree.TabIndex = 1;
            this.GrpCmdTree.TabStop = false;
            // 
            // TreeView
            // 
            this.TreeView.AllowDrop = true;
            this.TreeView.BackColor = System.Drawing.Color.Black;
            this.TreeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TreeView.Font = new System.Drawing.Font("Malgun Gothic", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.TreeView.ForeColor = System.Drawing.Color.White;
            this.TreeView.HideSelection = false;
            this.TreeView.Location = new System.Drawing.Point(3, 20);
            this.TreeView.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.TreeView.Name = "TreeView";
            this.TreeView.Size = new System.Drawing.Size(372, 325);
            this.TreeView.TabIndex = 0;
            this.TreeView.BeforeCollapse += new System.Windows.Forms.TreeViewCancelEventHandler(this.TreeView_BeforeCollapse);
            this.TreeView.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.TreeView_BeforeExpand);
            this.TreeView.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.TreeView_ItemDrag);
            this.TreeView.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.TreeView_NodeMouseClick);
            this.TreeView.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.TreeView_NodeMouseDoubleClick);
            this.TreeView.DragDrop += new System.Windows.Forms.DragEventHandler(this.TreeView_DragDrop);
            this.TreeView.DragEnter += new System.Windows.Forms.DragEventHandler(this.TreeView_DragEnter);
            this.TreeView.DragOver += new System.Windows.Forms.DragEventHandler(this.TreeView_DragOver);
            this.TreeView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TreeView_KeyDown);
            this.TreeView.MouseDown += new System.Windows.Forms.MouseEventHandler(this.TreeView_MouseDown);
            // 
            // GrpCmdBox
            // 
            this.GrpCmdBox.AutoSize = true;
            this.GrpCmdBox.Controls.Add(this.TxtCmdBox);
            this.GrpCmdBox.Controls.Add(this.BtnRun);
            this.GrpCmdBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.GrpCmdBox.Location = new System.Drawing.Point(3, 20);
            this.GrpCmdBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.GrpCmdBox.Name = "GrpCmdBox";
            this.GrpCmdBox.Padding = new System.Windows.Forms.Padding(3, 4, 3, 44);
            this.GrpCmdBox.Size = new System.Drawing.Size(378, 64);
            this.GrpCmdBox.TabIndex = 0;
            this.GrpCmdBox.TabStop = false;
            this.GrpCmdBox.Text = "Command";
            this.GrpCmdBox.Visible = false;
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
            this.BtnRun.Location = new System.Drawing.Point(300, 20);
            this.BtnRun.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.BtnRun.MaximumSize = new System.Drawing.Size(75, 29);
            this.BtnRun.MinimumSize = new System.Drawing.Size(75, 29);
            this.BtnRun.Name = "BtnRun";
            this.BtnRun.Size = new System.Drawing.Size(75, 29);
            this.BtnRun.TabIndex = 1;
            this.BtnRun.Text = "&Run";
            this.BtnRun.UseVisualStyleBackColor = true;
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 462);
            this.Controls.Add(this.GrpBackgroundBox);
            this.Controls.Add(this.toolMainTool);
            this.Font = new System.Drawing.Font("Malgun Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MinimizeBox = false;
            this.Name = "FrmMain";
            this.Text = "Shortcut";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmMain_FormClosing);
            this.Load += new System.EventHandler(this.FrmMain_Load);
            this.SizeChanged += new System.EventHandler(this.FrmMain_SizeChanged);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmMain_KeyDown);
            this.contextMenuNotifyIcon.ResumeLayout(false);
            this.toolMainTool.ResumeLayout(false);
            this.toolMainTool.PerformLayout();
            this.GrpBackgroundBox.ResumeLayout(false);
            this.GrpBackgroundBox.PerformLayout();
            this.GrpCmdTree.ResumeLayout(false);
            this.GrpCmdBox.ResumeLayout(false);
            this.GrpCmdBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.NotifyIcon NotifyIcon;
        private System.Windows.Forms.ContextMenuStrip contextMenuNotifyIcon;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_NotifyIcon_Open;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_NotifyIcon_Exit;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_NotifyIcon_Config;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.Timer tmrNodeOver;
        private System.Windows.Forms.ToolStrip toolMainTool;
        private System.Windows.Forms.GroupBox GrpBackgroundBox;
        private System.Windows.Forms.GroupBox GrpCmdTree;
        private System.Windows.Forms.TreeView TreeView;
        private System.Windows.Forms.GroupBox GrpCmdBox;
        private System.Windows.Forms.TextBox TxtCmdBox;
        private System.Windows.Forms.Button BtnRun;
        private System.Windows.Forms.ToolStripButton toolItem_Add;
        private System.Windows.Forms.ToolStripButton toolItem_Remove;
        private System.Windows.Forms.ToolStripButton toolItem_Edit;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    }
}