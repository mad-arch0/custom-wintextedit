namespace notes;

partial class Form1
{
    private System.ComponentModel.IContainer components = null;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        this.components = new System.ComponentModel.Container();
        this.splitContainer1 = new System.Windows.Forms.SplitContainer();
        this.treeView1 = new System.Windows.Forms.TreeView();
        this.richTextBox1 = new System.Windows.Forms.RichTextBox();
        this.splitContainerTerminal = new System.Windows.Forms.SplitContainer();
        this.terminalTextBox = new System.Windows.Forms.TextBox();
        this.terminalInputBox = new System.Windows.Forms.TextBox();
        ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
        this.splitContainer1.Panel1.SuspendLayout();
        this.splitContainer1.Panel2.SuspendLayout();
        this.splitContainer1.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)(this.splitContainerTerminal)).BeginInit();
        this.splitContainerTerminal.Panel1.SuspendLayout();
        this.splitContainerTerminal.Panel2.SuspendLayout();
        this.splitContainerTerminal.SuspendLayout();
        this.SuspendLayout();
        this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
        this.splitContainer1.Location = new System.Drawing.Point(0, 0);
        this.splitContainer1.Name = "splitContainer1";
        this.splitContainer1.Panel1.Controls.Add(this.treeView1);
        this.splitContainer1.Panel2.Controls.Add(this.richTextBox1);
        this.splitContainer1.Size = new System.Drawing.Size(800, 450);
        this.splitContainer1.SplitterDistance = 200;
        this.splitContainer1.TabIndex = 0;
        this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
        this.treeView1.Location = new System.Drawing.Point(0, 0);
        this.treeView1.Name = "treeView1";
        this.treeView1.Size = new System.Drawing.Size(200, 450);
        this.treeView1.TabIndex = 0;
        this.richTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
        this.richTextBox1.Location = new System.Drawing.Point(0, 0);
        this.richTextBox1.Name = "richTextBox1";
        this.richTextBox1.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Both;
        this.richTextBox1.WordWrap = false;
        this.richTextBox1.Multiline = true;
        this.richTextBox1.Size = new System.Drawing.Size(596, 450);
        this.richTextBox1.TabIndex = 0;
        this.richTextBox1.Text = "";
        this.splitContainerTerminal.Dock = System.Windows.Forms.DockStyle.Fill;
        this.splitContainerTerminal.Orientation = System.Windows.Forms.Orientation.Horizontal;
        this.splitContainerTerminal.Name = "splitContainerTerminal";
        this.splitContainerTerminal.Panel1.Controls.Add(this.splitContainer1);
        this.splitContainerTerminal.Panel2.Controls.Clear();
        this.splitContainerTerminal.Panel2.Controls.Add(this.terminalInputBox);
        this.splitContainerTerminal.Panel2.Controls.Add(this.terminalTextBox);
        this.splitContainerTerminal.Size = new System.Drawing.Size(800, 450);
        this.splitContainerTerminal.SplitterDistance = 350;
        this.splitContainerTerminal.TabIndex = 0;
        this.terminalInputBox.Dock = System.Windows.Forms.DockStyle.Bottom;
        this.terminalInputBox.Multiline = false;
        this.terminalInputBox.BackColor = System.Drawing.Color.FromArgb(30, 30, 30);
        this.terminalInputBox.ForeColor = System.Drawing.Color.FromArgb(204, 204, 204);
        this.terminalInputBox.Font = new System.Drawing.Font("Cascadia Mono", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        this.terminalInputBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
        this.terminalInputBox.Name = "terminalInputBox";
        this.terminalInputBox.Size = new System.Drawing.Size(800, 24);
        this.terminalInputBox.TabIndex = 2;
        this.terminalTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
        this.terminalTextBox.Multiline = true;
        this.terminalTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
        this.terminalTextBox.BackColor = System.Drawing.Color.FromArgb(30, 30, 30);
        this.terminalTextBox.ForeColor = System.Drawing.Color.FromArgb(204, 204, 204);
        this.terminalTextBox.Font = new System.Drawing.Font("Cascadia Mono", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        this.terminalTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
        this.terminalTextBox.Name = "terminalTextBox";
        this.terminalTextBox.ReadOnly = true;
        this.terminalTextBox.ShortcutsEnabled = false;
        this.terminalTextBox.Size = new System.Drawing.Size(800, 100);
        this.terminalTextBox.TabIndex = 1;
        this.terminalTextBox.Text = "Terminal ready...\r\n";
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(800, 450);
        this.Controls.Add(this.splitContainerTerminal);
        this.Name = "Form1";
        this.Text = "Text Editor";
        this.splitContainer1.Panel1.ResumeLayout(false);
        this.splitContainer1.Panel2.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
        this.splitContainer1.ResumeLayout(false);
        this.splitContainerTerminal.Panel1.ResumeLayout(false);
        this.splitContainerTerminal.Panel2.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)(this.splitContainerTerminal)).EndInit();
        this.splitContainerTerminal.ResumeLayout(false);
        this.ResumeLayout(false);
    }

    private System.Windows.Forms.SplitContainer splitContainer1;
    private System.Windows.Forms.TreeView treeView1;
    private System.Windows.Forms.RichTextBox richTextBox1;
    private System.Windows.Forms.SplitContainer splitContainerTerminal;
    private System.Windows.Forms.TextBox terminalTextBox;
    private System.Windows.Forms.TextBox terminalInputBox;
}
