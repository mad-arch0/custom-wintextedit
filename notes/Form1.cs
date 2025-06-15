using System.Diagnostics;

namespace notes;

public partial class Form1 : Form
{
    public Form1()
    {
        InitializeComponent();
    }

    private void OpenFile()
    {
        if (!PromptSaveIfNeeded()) return;
        using (OpenFileDialog openFileDialog = new OpenFileDialog())
        {
            openFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                OpenFileByPath(openFileDialog.FileName);
            }
        }
    }

    private void SaveFile()
    {
        using (SaveFileDialog saveFileDialog = new SaveFileDialog())
        {
            saveFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                File.WriteAllText(saveFileDialog.FileName, richTextBox1.Text);
            }
        }
    }

    private string currentFolderPath = string.Empty;

    private void OpenFolder()
    {
        using (var folderDialog = new FolderBrowserDialog())
        {
            if (folderDialog.ShowDialog() == DialogResult.OK)
            {
                currentFolderPath = folderDialog.SelectedPath;
                PopulateTreeView(currentFolderPath);
            }
        }
    }

    private void PopulateTreeView(string folderPath)
    {
        treeView1.Nodes.Clear();
        var rootDirectoryInfo = new DirectoryInfo(folderPath);
        treeView1.Nodes.Add(CreateDirectoryNode(rootDirectoryInfo));
        treeView1.ExpandAll();
    }

    private TreeNode CreateDirectoryNode(DirectoryInfo directoryInfo)
    {
        var directoryNode = new TreeNode(directoryInfo.Name);
        foreach (var dir in directoryInfo.GetDirectories())
            directoryNode.Nodes.Add(CreateDirectoryNode(dir));
        foreach (var file in directoryInfo.GetFiles())
        {
            if (IsSupportedFile(file.Extension))
                directoryNode.Nodes.Add(new TreeNode(file.Name) { Tag = file.FullName });
        }
        return directoryNode;
    }

    private bool IsSupportedFile(string extension)
    {
        string[] supported = { ".txt", ".html", ".css", ".js", ".json", ".cs" };
        return supported.Contains(extension.ToLower());
    }

    private string? currentFilePath = null;
    private bool isTextChanged = false;

    private void richTextBox1_TextChanged(object sender, EventArgs e)
    {
        isTextChanged = true;
    }

    private bool PromptSaveIfNeeded()
    {
        if (isTextChanged)
        {
            var result = MessageBox.Show("Do you want to save changes to the current file?", "Save Changes", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                if (!string.IsNullOrEmpty(currentFilePath))
                {
                    File.WriteAllText(currentFilePath, richTextBox1.Text);
                    isTextChanged = false;
                    return true;
                }
                else
                {
                    SaveFile();
                    isTextChanged = false;
                    return true;
                }
            }
            else if (result == DialogResult.Cancel)
            {
                return false;
            }
        }
        return true;
    }

    private void OpenFileByPath(string filePath)
    {
        if (!PromptSaveIfNeeded()) return;
        richTextBox1.Text = File.ReadAllText(filePath);
        currentFilePath = filePath;
        isTextChanged = false;
    }

    private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
    {
        if (e.Node.Tag is string filePath && File.Exists(filePath))
        {
            OpenFileByPath(filePath);
        }
    }

    private void treeView1_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
    {
        if (e.Node.Tag is string filePath && File.Exists(filePath))
        {
            OpenFileByPath(filePath);
        }
    }

    private void AddMenu()
    {
        var menuStrip = new MenuStrip();
        var fileMenu = new ToolStripMenuItem("File");
        var openFileItem = new ToolStripMenuItem("Open File", null, (s, ev) => OpenFile());
        var openFolderItem = new ToolStripMenuItem("Open Folder", null, (s, ev) => OpenFolder());
        var saveItem = new ToolStripMenuItem("Save", null, (s, ev) => SaveFile());
        fileMenu.DropDownItems.Add(openFileItem);
        fileMenu.DropDownItems.Add(openFolderItem);
        fileMenu.DropDownItems.Add(saveItem);
        menuStrip.Items.Add(fileMenu);
        this.MainMenuStrip = menuStrip;
        this.Controls.Add(menuStrip);
        menuStrip.Dock = DockStyle.Top;
    }

    private void ApplyConfig()
    {
        string configPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "editorconfig.json");
        if (!File.Exists(configPath)) return;
        var json = File.ReadAllText(configPath);
        var config = System.Text.Json.JsonSerializer.Deserialize<EditorConfig>(json);
        if (config == null) return;

        if (!string.IsNullOrEmpty(config.FontFamily) && config.FontSize > 0)
        {
            try { richTextBox1.Font = new Font(config.FontFamily, config.FontSize); }
            catch { richTextBox1.Font = new Font(FontFamily.GenericMonospace, config.FontSize); }
        }
        if (!string.IsNullOrEmpty(config.BackColor))
            try { richTextBox1.BackColor = ColorTranslator.FromHtml(config.BackColor); } catch { }
        if (!string.IsNullOrEmpty(config.ForeColor))
            try { richTextBox1.ForeColor = ColorTranslator.FromHtml(config.ForeColor); } catch { }
        if (!string.IsNullOrEmpty(config.TreeBackColor))
            try { treeView1.BackColor = ColorTranslator.FromHtml(config.TreeBackColor); } catch { }
        if (!string.IsNullOrEmpty(config.TreeForeColor))
            try { treeView1.ForeColor = ColorTranslator.FromHtml(config.TreeForeColor); } catch { }
        if (!string.IsNullOrEmpty(config.MenuBackColor) && MainMenuStrip != null)
            try { MainMenuStrip.BackColor = ColorTranslator.FromHtml(config.MenuBackColor); } catch { }
        if (!string.IsNullOrEmpty(config.MenuForeColor) && MainMenuStrip != null)
            try { MainMenuStrip.ForeColor = ColorTranslator.FromHtml(config.MenuForeColor); } catch { }
    }

    private Process? terminalProcess;
    private void StartTerminal()
    {
        terminalProcess = new Process();
        terminalProcess.StartInfo.FileName = "cmd.exe";
        terminalProcess.StartInfo.UseShellExecute = false;
        terminalProcess.StartInfo.RedirectStandardInput = true;
        terminalProcess.StartInfo.RedirectStandardOutput = true;
        terminalProcess.StartInfo.RedirectStandardError = true;
        terminalProcess.StartInfo.CreateNoWindow = true;
        terminalProcess.OutputDataReceived += (s, e) => AppendTerminalText(e.Data);
        terminalProcess.ErrorDataReceived += (s, e) => AppendTerminalText(e.Data);
        terminalProcess.Start();
        terminalProcess.BeginOutputReadLine();
        terminalProcess.BeginErrorReadLine();
    }

    private void AppendTerminalText(string? text)
    {
        if (terminalTextBox.InvokeRequired)
        {
            terminalTextBox.Invoke(new Action<string?>(AppendTerminalText), text);
        }
        else if (!string.IsNullOrEmpty(text))
        {
            terminalTextBox.AppendText(text + "\r\n");
        }
    }

    private string terminalInputBuffer = string.Empty;

    private void terminalTextBox_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter)
        {
            if (!string.IsNullOrWhiteSpace(terminalInputBuffer) && terminalProcess != null)
            {
                terminalProcess.StandardInput.WriteLine(terminalInputBuffer);
                AppendTerminalText("> " + terminalInputBuffer);
                terminalInputBuffer = string.Empty;
            }
            e.Handled = true;
        }
        else if (e.KeyCode == Keys.Back)
        {
            if (terminalInputBuffer.Length > 0)
            {
                terminalInputBuffer = terminalInputBuffer.Substring(0, terminalInputBuffer.Length - 1);
            }
            e.Handled = true;
        }
        else if (e.KeyCode == Keys.V && e.Control)
        {
            var text = Clipboard.GetText();
            terminalInputBuffer += text;
            e.Handled = true;
        }
        else if (e.KeyCode >= Keys.Space && e.KeyCode <= Keys.Z)
        {
            terminalInputBuffer += e.Shift ? e.KeyCode.ToString() : e.KeyCode.ToString().ToLower();
            e.Handled = true;
        }
    }

    private List<string> terminalCommandHistory = new List<string>();
    private int terminalHistoryIndex = -1;

    private void terminalInputBox_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter)
        {
            string command = terminalInputBox.Text.Trim();
            if (!string.IsNullOrEmpty(command))
            {
                if (command.Equals("clear", StringComparison.OrdinalIgnoreCase))
                {
                    terminalTextBox.Clear();
                }
                else if (terminalProcess != null)
                {
                    terminalProcess.StandardInput.WriteLine(command);
                    AppendTerminalText("> " + command);
                }
                terminalCommandHistory.Add(command);
                terminalHistoryIndex = terminalCommandHistory.Count;
                terminalInputBox.Clear();
            }
            e.Handled = true;
        }
        else if (e.KeyCode == Keys.Up)
        {
            if (terminalCommandHistory.Count > 0 && terminalHistoryIndex > 0)
            {
                terminalHistoryIndex--;
                terminalInputBox.Text = terminalCommandHistory[terminalHistoryIndex];
                terminalInputBox.SelectionStart = terminalInputBox.Text.Length;
            }
            e.Handled = true;
        }
        else if (e.KeyCode == Keys.Down)
        {
            if (terminalCommandHistory.Count > 0 && terminalHistoryIndex < terminalCommandHistory.Count - 1)
            {
                terminalHistoryIndex++;
                terminalInputBox.Text = terminalCommandHistory[terminalHistoryIndex];
                terminalInputBox.SelectionStart = terminalInputBox.Text.Length;
            }
            else if (terminalHistoryIndex == terminalCommandHistory.Count - 1)
            {
                terminalHistoryIndex++;
                terminalInputBox.Clear();
            }
            e.Handled = true;
        }
    }

    private void terminalInputBox_KeyUp(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Tab)
        {
            string currentText = terminalInputBox.Text.Trim();
            if (!string.IsNullOrEmpty(currentText))
            {
                var matches = terminalCommandHistory.Where(cmd => cmd.StartsWith(currentText)).Distinct().ToList();
                if (matches.Count == 1)
                {
                    terminalInputBox.Text = matches[0];
                    terminalInputBox.SelectionStart = terminalInputBox.Text.Length;
                }
                else if (matches.Count > 1)
                {
                    AppendTerminalText(string.Join("    ", matches));
                }
            }
            e.Handled = true;
        }
    }

    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
        AddMenu();
        treeView1.NodeMouseDoubleClick += treeView1_NodeMouseDoubleClick;
        treeView1.AfterSelect += treeView1_AfterSelect;
        richTextBox1.TextChanged += richTextBox1_TextChanged;
        terminalTextBox.KeyDown -= terminalTextBox_KeyDown;
        terminalInputBox.KeyDown += terminalInputBox_KeyDown;
        StartTerminal();
        ApplyConfig();
    }

    private class EditorConfig
    {
        public string? FontFamily { get; set; }
        public float FontSize { get; set; }
        public string? BackColor { get; set; }
        public string? ForeColor { get; set; }
        public string? TreeBackColor { get; set; }
        public string? TreeForeColor { get; set; }
        public string? MenuBackColor { get; set; }
        public string? MenuForeColor { get; set; }
        public bool ShowScrollBars { get; set; } = true;
    }
}
