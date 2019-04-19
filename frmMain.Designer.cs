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
            this.GrpCmdBox = new System.Windows.Forms.GroupBox();
            this.GrpBackgroundBox = new System.Windows.Forms.GroupBox();
            this.GrpCmdTree = new System.Windows.Forms.GroupBox();
            this.contextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem_Add = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_Edit = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_Del = new System.Windows.Forms.ToolStripMenuItem();
            this.GrpCmdBox.SuspendLayout();
            this.GrpBackgroundBox.SuspendLayout();
            this.GrpCmdTree.SuspendLayout();
            this.contextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // TxtCmdBox
            // 
            this.TxtCmdBox.BackColor = System.Drawing.Color.Black;
            this.TxtCmdBox.Dock = System.Windows.Forms.DockStyle.Left;
            this.TxtCmdBox.Font = new System.Drawing.Font("LG스마트체2.0 SemiBold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.TxtCmdBox.ForeColor = System.Drawing.Color.White;
            this.TxtCmdBox.Location = new System.Drawing.Point(3, 17);
            this.TxtCmdBox.Name = "TxtCmdBox";
            this.TxtCmdBox.Size = new System.Drawing.Size(252, 29);
            this.TxtCmdBox.TabIndex = 0;
            // 
            // BtnRun
            // 
            this.BtnRun.Dock = System.Windows.Forms.DockStyle.Right;
            this.BtnRun.Location = new System.Drawing.Point(264, 17);
            this.BtnRun.MaximumSize = new System.Drawing.Size(75, 23);
            this.BtnRun.MinimumSize = new System.Drawing.Size(75, 23);
            this.BtnRun.Name = "BtnRun";
            this.BtnRun.Size = new System.Drawing.Size(75, 23);
            this.BtnRun.TabIndex = 1;
            this.BtnRun.Text = "&Run";
            this.BtnRun.UseVisualStyleBackColor = true;
            this.BtnRun.Click += new System.EventHandler(this.BtnRun_Click);
            // 
            // TreeView
            // 
            this.TreeView.AllowDrop = true;
            this.TreeView.BackColor = System.Drawing.Color.Black;
            this.TreeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TreeView.Font = new System.Drawing.Font("LG스마트체2.0 Bold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.TreeView.ForeColor = System.Drawing.Color.White;
            this.TreeView.HideSelection = false;
            this.TreeView.LabelEdit = true;
            this.TreeView.Location = new System.Drawing.Point(3, 17);
            this.TreeView.Name = "TreeView";
            this.TreeView.Size = new System.Drawing.Size(336, 399);
            this.TreeView.TabIndex = 0;
            this.TreeView.AfterExpand += new System.Windows.Forms.TreeViewEventHandler(this.TreeView_AfterExpand);
            this.TreeView.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.TreeView_ItemDrag);
            this.TreeView.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.TreeView_NodeMouseClick);
            this.TreeView.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.TreeView_NodeMouseDoubleClick);
            this.TreeView.DragDrop += new System.Windows.Forms.DragEventHandler(this.TreeView_DragDrop);
            this.TreeView.DragEnter += new System.Windows.Forms.DragEventHandler(this.TreeView_DragEnter);
            this.TreeView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TreeView_KeyDown);
            this.TreeView.MouseDown += new System.Windows.Forms.MouseEventHandler(this.TreeView_MouseDown);
            this.TreeView.MouseUp += new System.Windows.Forms.MouseEventHandler(this.TreeView_MouseUp);
            // 
            // GrpCmdBox
            // 
            this.GrpCmdBox.AutoSize = true;
            this.GrpCmdBox.Controls.Add(this.TxtCmdBox);
            this.GrpCmdBox.Controls.Add(this.BtnRun);
            this.GrpCmdBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.GrpCmdBox.Enabled = false;
            this.GrpCmdBox.Location = new System.Drawing.Point(3, 17);
            this.GrpCmdBox.Name = "GrpCmdBox";
            this.GrpCmdBox.Padding = new System.Windows.Forms.Padding(3, 3, 3, 35);
            this.GrpCmdBox.Size = new System.Drawing.Size(342, 52);
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
            this.GrpBackgroundBox.Name = "GrpBackgroundBox";
            this.GrpBackgroundBox.Size = new System.Drawing.Size(348, 491);
            this.GrpBackgroundBox.TabIndex = 0;
            this.GrpBackgroundBox.TabStop = false;
            // 
            // GrpCmdTree
            // 
            this.GrpCmdTree.BackColor = System.Drawing.SystemColors.Control;
            this.GrpCmdTree.Controls.Add(this.TreeView);
            this.GrpCmdTree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GrpCmdTree.Location = new System.Drawing.Point(3, 69);
            this.GrpCmdTree.Name = "GrpCmdTree";
            this.GrpCmdTree.Size = new System.Drawing.Size(342, 419);
            this.GrpCmdTree.TabIndex = 1;
            this.GrpCmdTree.TabStop = false;
            // 
            // contextMenu
            // 
            this.contextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem_Add,
            this.toolStripMenuItem_Edit,
            this.toolStripMenuItem_Del});
            this.contextMenu.Name = "ContextMenu_ToTreeView";
            this.contextMenu.Size = new System.Drawing.Size(109, 70);
            this.contextMenu.Opening += new System.ComponentModel.CancelEventHandler(this.ContextMenu_Opening);
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
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(348, 491);
            this.Controls.Add(this.GrpBackgroundBox);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "FrmMain";
            this.Text = "Shortcut";
            this.Load += new System.EventHandler(this.FrmMain_Load);
            this.GrpCmdBox.ResumeLayout(false);
            this.GrpCmdBox.PerformLayout();
            this.GrpBackgroundBox.ResumeLayout(false);
            this.GrpBackgroundBox.PerformLayout();
            this.GrpCmdTree.ResumeLayout(false);
            this.contextMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox TxtCmdBox;
        private System.Windows.Forms.Button BtnRun;
        private System.Windows.Forms.TreeView TreeView;
        private System.Windows.Forms.GroupBox GrpCmdBox;
        private System.Windows.Forms.GroupBox GrpBackgroundBox;
        private System.Windows.Forms.GroupBox GrpCmdTree;
        private System.Windows.Forms.ContextMenuStrip contextMenu;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_Add;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_Del;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_Edit;
    }
}