namespace MIPSAssembler_Winform
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.文件FToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip_Open = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip_Save = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip_Saveelse = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStrip_Exit = new System.Windows.Forms.ToolStripMenuItem();
            this.编辑EToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip_Add = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip_Cut = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip_Paste = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStrip_Undo = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip_Redo = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStrip_SelectAll = new System.Windows.Forms.ToolStripMenuItem();
            this.汇编AToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.帮助HToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip_About = new System.Windows.Forms.ToolStripMenuItem();
            this.editor = new System.Windows.Forms.RichTextBox();
            this.linenum = new System.Windows.Forms.RichTextBox();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.文件FToolStripMenuItem,
            this.编辑EToolStripMenuItem,
            this.汇编AToolStripMenuItem,
            this.帮助HToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 25);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            this.menuStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.menuStrip1_ItemClicked);
            // 
            // 文件FToolStripMenuItem
            // 
            this.文件FToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStrip_Open,
            this.toolStrip_Save,
            this.toolStrip_Saveelse,
            this.toolStripMenuItem1,
            this.toolStrip_Exit});
            this.文件FToolStripMenuItem.Name = "文件FToolStripMenuItem";
            this.文件FToolStripMenuItem.Size = new System.Drawing.Size(58, 21);
            this.文件FToolStripMenuItem.Text = "文件(&F)";
            // 
            // toolStrip_Open
            // 
            this.toolStrip_Open.Name = "toolStrip_Open";
            this.toolStrip_Open.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.toolStrip_Open.Size = new System.Drawing.Size(206, 22);
            this.toolStrip_Open.Text = "打开(&O)";
            this.toolStrip_Open.Click += new System.EventHandler(this.toolStrip_Open_Click);
            // 
            // toolStrip_Save
            // 
            this.toolStrip_Save.Name = "toolStrip_Save";
            this.toolStrip_Save.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.toolStrip_Save.Size = new System.Drawing.Size(206, 22);
            this.toolStrip_Save.Text = "保存(&S)";
            this.toolStrip_Save.Click += new System.EventHandler(this.toolStrip_Save_Click);
            // 
            // toolStrip_Saveelse
            // 
            this.toolStrip_Saveelse.Name = "toolStrip_Saveelse";
            this.toolStrip_Saveelse.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.S)));
            this.toolStrip_Saveelse.Size = new System.Drawing.Size(206, 22);
            this.toolStrip_Saveelse.Text = "另存为(&A)";
            this.toolStrip_Saveelse.Click += new System.EventHandler(this.toolStrip_Saveelse_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(203, 6);
            // 
            // toolStrip_Exit
            // 
            this.toolStrip_Exit.Name = "toolStrip_Exit";
            this.toolStrip_Exit.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.toolStrip_Exit.Size = new System.Drawing.Size(206, 22);
            this.toolStrip_Exit.Text = "退出(&E)";
            this.toolStrip_Exit.Click += new System.EventHandler(this.toolStrip_Exit_Click);
            // 
            // 编辑EToolStripMenuItem
            // 
            this.编辑EToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStrip_Add,
            this.toolStrip_Cut,
            this.toolStrip_Paste,
            this.toolStripMenuItem2,
            this.toolStrip_Undo,
            this.toolStrip_Redo,
            this.toolStripMenuItem3,
            this.toolStrip_SelectAll});
            this.编辑EToolStripMenuItem.Name = "编辑EToolStripMenuItem";
            this.编辑EToolStripMenuItem.Size = new System.Drawing.Size(59, 21);
            this.编辑EToolStripMenuItem.Text = "编辑(&E)";
            // 
            // toolStrip_Add
            // 
            this.toolStrip_Add.Name = "toolStrip_Add";
            this.toolStrip_Add.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.toolStrip_Add.Size = new System.Drawing.Size(161, 22);
            this.toolStrip_Add.Text = "复制(&C)";
            this.toolStrip_Add.Click += new System.EventHandler(this.toolStrip_Add_Click);
            // 
            // toolStrip_Cut
            // 
            this.toolStrip_Cut.Name = "toolStrip_Cut";
            this.toolStrip_Cut.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
            this.toolStrip_Cut.Size = new System.Drawing.Size(161, 22);
            this.toolStrip_Cut.Text = "剪切(&X)";
            this.toolStrip_Cut.Click += new System.EventHandler(this.toolStrip_Cut_Click);
            // 
            // toolStrip_Paste
            // 
            this.toolStrip_Paste.Name = "toolStrip_Paste";
            this.toolStrip_Paste.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
            this.toolStrip_Paste.Size = new System.Drawing.Size(161, 22);
            this.toolStrip_Paste.Text = "粘贴(&V)";
            this.toolStrip_Paste.Click += new System.EventHandler(this.toolStrip_Paste_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(158, 6);
            // 
            // toolStrip_Undo
            // 
            this.toolStrip_Undo.Name = "toolStrip_Undo";
            this.toolStrip_Undo.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Z)));
            this.toolStrip_Undo.Size = new System.Drawing.Size(161, 22);
            this.toolStrip_Undo.Text = "撤销(&Z)";
            this.toolStrip_Undo.Click += new System.EventHandler(this.toolStrip_Undo_Click);
            // 
            // toolStrip_Redo
            // 
            this.toolStrip_Redo.Name = "toolStrip_Redo";
            this.toolStrip_Redo.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Y)));
            this.toolStrip_Redo.Size = new System.Drawing.Size(161, 22);
            this.toolStrip_Redo.Text = "重做(&Y)";
            this.toolStrip_Redo.Click += new System.EventHandler(this.toolStrip_Redo_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(158, 6);
            // 
            // toolStrip_SelectAll
            // 
            this.toolStrip_SelectAll.Name = "toolStrip_SelectAll";
            this.toolStrip_SelectAll.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)));
            this.toolStrip_SelectAll.Size = new System.Drawing.Size(161, 22);
            this.toolStrip_SelectAll.Text = "全选(&A)";
            this.toolStrip_SelectAll.Click += new System.EventHandler(this.toolStrip_SelectAll_Click);
            // 
            // 汇编AToolStripMenuItem
            // 
            this.汇编AToolStripMenuItem.Name = "汇编AToolStripMenuItem";
            this.汇编AToolStripMenuItem.Size = new System.Drawing.Size(60, 21);
            this.汇编AToolStripMenuItem.Text = "汇编(&A)";
            // 
            // 帮助HToolStripMenuItem
            // 
            this.帮助HToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStrip_About});
            this.帮助HToolStripMenuItem.Name = "帮助HToolStripMenuItem";
            this.帮助HToolStripMenuItem.Size = new System.Drawing.Size(61, 21);
            this.帮助HToolStripMenuItem.Text = "帮助(&H)";
            // 
            // toolStrip_About
            // 
            this.toolStrip_About.Name = "toolStrip_About";
            this.toolStrip_About.ShortcutKeys = System.Windows.Forms.Keys.F1;
            this.toolStrip_About.Size = new System.Drawing.Size(180, 22);
            this.toolStrip_About.Text = "关于(&A)";
            this.toolStrip_About.Click += new System.EventHandler(this.toolStrip_About_Click);
            // 
            // editor
            // 
            this.editor.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.editor.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.editor.Location = new System.Drawing.Point(27, 26);
            this.editor.Name = "editor";
            this.editor.Size = new System.Drawing.Size(768, 422);
            this.editor.TabIndex = 1;
            this.editor.Text = "";
            this.editor.TextChanged += new System.EventHandler(this.editor_TextChanged);
            // 
            // linenum
            // 
            this.linenum.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.linenum.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linenum.ForeColor = System.Drawing.Color.DodgerBlue;
            this.linenum.Location = new System.Drawing.Point(4, 26);
            this.linenum.Name = "linenum";
            this.linenum.Size = new System.Drawing.Size(23, 423);
            this.linenum.TabIndex = 2;
            this.linenum.Text = "";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.linenum);
            this.Controls.Add(this.editor);
            this.Controls.Add(this.menuStrip1);
            this.Name = "Form1";
            this.Text = "MyMIPS";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 文件FToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStrip_Open;
        private System.Windows.Forms.ToolStripMenuItem 编辑EToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 汇编AToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 帮助HToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStrip_Save;
        private System.Windows.Forms.ToolStripMenuItem toolStrip_Saveelse;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem toolStrip_Exit;
        private System.Windows.Forms.ToolStripMenuItem toolStrip_Add;
        private System.Windows.Forms.ToolStripMenuItem toolStrip_About;
        private System.Windows.Forms.RichTextBox editor;
        private System.Windows.Forms.RichTextBox linenum;
        private System.Windows.Forms.ToolStripMenuItem toolStrip_Cut;
        private System.Windows.Forms.ToolStripMenuItem toolStrip_Paste;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem toolStrip_SelectAll;
        private System.Windows.Forms.ToolStripMenuItem toolStrip_Undo;
        private System.Windows.Forms.ToolStripMenuItem toolStrip_Redo;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
    }
}

