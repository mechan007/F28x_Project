using System.Windows.Forms;

namespace F28x_Project
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            menuStrip1 = new MenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem();
            exitToolStripMenuItem = new ToolStripMenuItem();
            settingsToolStripMenuItem = new ToolStripMenuItem();
            portToolStripMenuItem = new ToolStripMenuItem();
            portsToolStripComboBox = new ToolStripComboBox();
            languageToolStripMenuItem = new ToolStripMenuItem();
            languagesToolStripComboBox = new ToolStripComboBox();
            displaedVluesToolStripMenuItem = new ToolStripMenuItem();
            basicToolStripMenuItem = new ToolStripMenuItem();
            advancedToolStripMenuItem = new ToolStripMenuItem();
            graphToolStripMenuItem = new ToolStripMenuItem();
            scrollingToolStripMenuItem = new ToolStripMenuItem();
            continuousToolStripMenuItem = new ToolStripMenuItem();
            portToolStripText = new ToolStripTextBox();
            connectButtonToolStripMenuItem = new ToolStripMenuItem();
            statusStrip1 = new StatusStrip();
            modelToolStripStatusLabel = new ToolStripStatusLabel();
            toolStripStatusLabel2 = new ToolStripStatusLabel();
            serialNumberToolStripStatusLabel = new ToolStripStatusLabel();
            versionToolStripStatusLabel = new ToolStripStatusLabel();
            toolStripStatusLabel3 = new ToolStripStatusLabel();
            groupBox1 = new GroupBox();
            stateLabel = new Label();
            unitLabel = new Label();
            readingValueLabel = new Label();
            formsPlot1 = new ScottPlot.WinForms.FormsPlot();
            menuStrip1.SuspendLayout();
            statusStrip1.SuspendLayout();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem, settingsToolStripMenuItem, portToolStripText, connectButtonToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(384, 28);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { exitToolStripMenuItem });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new Size(44, 24);
            fileToolStripMenuItem.Text = "File";
            // 
            // exitToolStripMenuItem
            // 
            exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            exitToolStripMenuItem.Size = new Size(102, 24);
            exitToolStripMenuItem.Text = "Exit";
            // 
            // settingsToolStripMenuItem
            // 
            settingsToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { portToolStripMenuItem, languageToolStripMenuItem, displaedVluesToolStripMenuItem, graphToolStripMenuItem });
            settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            settingsToolStripMenuItem.Size = new Size(74, 24);
            settingsToolStripMenuItem.Text = "Settings";
            // 
            // portToolStripMenuItem
            // 
            portToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { portsToolStripComboBox });
            portToolStripMenuItem.Name = "portToolStripMenuItem";
            portToolStripMenuItem.Size = new Size(180, 24);
            portToolStripMenuItem.Text = "Port";
            // 
            // portsToolStripComboBox
            // 
            portsToolStripComboBox.Name = "portsToolStripComboBox";
            portsToolStripComboBox.Size = new Size(121, 28);
            // 
            // languageToolStripMenuItem
            // 
            languageToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { languagesToolStripComboBox });
            languageToolStripMenuItem.Name = "languageToolStripMenuItem";
            languageToolStripMenuItem.Size = new Size(180, 24);
            languageToolStripMenuItem.Text = "Language";
            // 
            // languagesToolStripComboBox
            // 
            languagesToolStripComboBox.Items.AddRange(new object[] { "English", "Česky", "Deutsch", "Polski" });
            languagesToolStripComboBox.Name = "languagesToolStripComboBox";
            languagesToolStripComboBox.Size = new Size(121, 28);
            // 
            // displaedVluesToolStripMenuItem
            // 
            displaedVluesToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { basicToolStripMenuItem, advancedToolStripMenuItem });
            displaedVluesToolStripMenuItem.Name = "displaedVluesToolStripMenuItem";
            displaedVluesToolStripMenuItem.Size = new Size(180, 24);
            displaedVluesToolStripMenuItem.Text = "Displaed vlues";
            // 
            // basicToolStripMenuItem
            // 
            basicToolStripMenuItem.Checked = true;
            basicToolStripMenuItem.CheckState = CheckState.Checked;
            basicToolStripMenuItem.Name = "basicToolStripMenuItem";
            basicToolStripMenuItem.Size = new Size(144, 24);
            basicToolStripMenuItem.Text = "Basic";
            // 
            // advancedToolStripMenuItem
            // 
            advancedToolStripMenuItem.Name = "advancedToolStripMenuItem";
            advancedToolStripMenuItem.Size = new Size(144, 24);
            advancedToolStripMenuItem.Text = "Advanced";
            // 
            // graphToolStripMenuItem
            // 
            graphToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { scrollingToolStripMenuItem, continuousToolStripMenuItem });
            graphToolStripMenuItem.Name = "graphToolStripMenuItem";
            graphToolStripMenuItem.Size = new Size(180, 24);
            graphToolStripMenuItem.Text = "Graph";
            // 
            // scrollingToolStripMenuItem
            // 
            scrollingToolStripMenuItem.Checked = true;
            scrollingToolStripMenuItem.CheckOnClick = true;
            scrollingToolStripMenuItem.CheckState = CheckState.Checked;
            scrollingToolStripMenuItem.Name = "scrollingToolStripMenuItem";
            scrollingToolStripMenuItem.Size = new Size(180, 24);
            scrollingToolStripMenuItem.Text = "Scrolling (60s)";
            // 
            // continuousToolStripMenuItem
            // 
            continuousToolStripMenuItem.CheckOnClick = true;
            continuousToolStripMenuItem.Name = "continuousToolStripMenuItem";
            continuousToolStripMenuItem.Size = new Size(180, 24);
            continuousToolStripMenuItem.Text = "Continuous";
            // 
            // portToolStripText
            // 
            portToolStripText.Alignment = ToolStripItemAlignment.Right;
            portToolStripText.BackColor = SystemColors.Window;
            portToolStripText.BorderStyle = BorderStyle.None;
            portToolStripText.Name = "portToolStripText";
            portToolStripText.ReadOnly = true;
            portToolStripText.Size = new Size(60, 24);
            portToolStripText.Text = "COM99";
            portToolStripText.TextBoxTextAlign = HorizontalAlignment.Center;
            // 
            // connectButtonToolStripMenuItem
            // 
            connectButtonToolStripMenuItem.Alignment = ToolStripItemAlignment.Right;
            connectButtonToolStripMenuItem.AutoSize = false;
            connectButtonToolStripMenuItem.BackColor = SystemColors.Control;
            connectButtonToolStripMenuItem.DisplayStyle = ToolStripItemDisplayStyle.Image;
            connectButtonToolStripMenuItem.Image = Properties.Resources.link;
            connectButtonToolStripMenuItem.ImageTransparentColor = Color.Transparent;
            connectButtonToolStripMenuItem.Name = "connectButtonToolStripMenuItem";
            connectButtonToolStripMenuItem.Size = new Size(24, 24);
            // 
            // statusStrip1
            // 
            statusStrip1.Items.AddRange(new ToolStripItem[] { modelToolStripStatusLabel, toolStripStatusLabel2, serialNumberToolStripStatusLabel, versionToolStripStatusLabel, toolStripStatusLabel3 });
            statusStrip1.Location = new Point(0, 462);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(384, 31);
            statusStrip1.TabIndex = 1;
            statusStrip1.Text = "statusStrip1";
            // 
            // modelToolStripStatusLabel
            // 
            modelToolStripStatusLabel.AutoSize = false;
            modelToolStripStatusLabel.Name = "modelToolStripStatusLabel";
            modelToolStripStatusLabel.Padding = new Padding(3, 0, 3, 0);
            modelToolStripStatusLabel.Size = new Size(81, 26);
            // 
            // toolStripStatusLabel2
            // 
            toolStripStatusLabel2.Margin = new Padding(50, 3, 0, 2);
            toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            toolStripStatusLabel2.Size = new Size(37, 26);
            toolStripStatusLabel2.Text = "S/N:";
            // 
            // serialNumberToolStripStatusLabel
            // 
            serialNumberToolStripStatusLabel.AutoSize = false;
            serialNumberToolStripStatusLabel.Name = "serialNumberToolStripStatusLabel";
            serialNumberToolStripStatusLabel.Padding = new Padding(3, 0, 3, 0);
            serialNumberToolStripStatusLabel.Size = new Size(89, 26);
            // 
            // versionToolStripStatusLabel
            // 
            versionToolStripStatusLabel.AutoSize = false;
            versionToolStripStatusLabel.Margin = new Padding(55, 3, 0, 3);
            versionToolStripStatusLabel.Name = "versionToolStripStatusLabel";
            versionToolStripStatusLabel.Padding = new Padding(3, 0, 3, 0);
            versionToolStripStatusLabel.Size = new Size(59, 25);
            // 
            // toolStripStatusLabel3
            // 
            toolStripStatusLabel3.Name = "toolStripStatusLabel3";
            toolStripStatusLabel3.Size = new Size(151, 20);
            toolStripStatusLabel3.Text = "toolStripStatusLabel3";
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(stateLabel);
            groupBox1.Controls.Add(unitLabel);
            groupBox1.Controls.Add(readingValueLabel);
            groupBox1.Location = new Point(12, 31);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(360, 199);
            groupBox1.TabIndex = 2;
            groupBox1.TabStop = false;
            // 
            // stateLabel
            // 
            stateLabel.Font = new Font("Century Gothic", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 238);
            stateLabel.Location = new Point(6, 173);
            stateLabel.Name = "stateLabel";
            stateLabel.Size = new Size(308, 23);
            stateLabel.TabIndex = 2;
            stateLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // unitLabel
            // 
            unitLabel.Font = new Font("Century Gothic", 12F, FontStyle.Bold, GraphicsUnit.Point, 238);
            unitLabel.Location = new Point(258, 43);
            unitLabel.Name = "unitLabel";
            unitLabel.Size = new Size(83, 18);
            unitLabel.TabIndex = 1;
            unitLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // readingValueLabel
            // 
            readingValueLabel.Font = new Font("Century Gothic", 48F, FontStyle.Bold, GraphicsUnit.Point, 0);
            readingValueLabel.Location = new Point(6, 23);
            readingValueLabel.Name = "readingValueLabel";
            readingValueLabel.Size = new Size(259, 77);
            readingValueLabel.TabIndex = 0;
            readingValueLabel.TextAlign = ContentAlignment.MiddleRight;
            // 
            // formsPlot1
            // 
            formsPlot1.Location = new Point(0, 236);
            formsPlot1.Name = "formsPlot1";
            formsPlot1.Size = new Size(384, 223);
            formsPlot1.TabIndex = 3;
            // 
            // Form1
            // 
            ClientSize = new Size(384, 493);
            Controls.Add(formsPlot1);
            Controls.Add(groupBox1);
            Controls.Add(statusStrip1);
            Controls.Add(menuStrip1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MainMenuStrip = menuStrip1;
            Name = "Form1";
            Text = "F28X Toolset";
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            groupBox1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private MenuStrip menuStrip1;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem exitToolStripMenuItem;
        private ToolStripMenuItem settingsToolStripMenuItem;
        private ToolStripMenuItem portToolStripMenuItem;
        private ToolStripComboBox toolStripComboBox1;
        private ToolStripMenuItem languageToolStripMenuItem;
        private ToolStripComboBox languagesToolStripComboBox;
        private ToolStripTextBox portToolStripText;
        private ToolStripComboBox portsToolStripComboBox;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel toolStripStatusLabel1;
        private ToolStripStatusLabel toolStripStatusLabel2;
        private ToolStripStatusLabel serialNumberToolStripStatusLabel;
        private ToolStripStatusLabel versionToolStripStatusLabel;
        private ToolStripStatusLabel modelToolStripStatusLabel;
        private ToolStripStatusLabel toolStripStatusLabel3;
        private ToolStripMenuItem connectButtonToolStripMenuItem;
        private GroupBox groupBox1;
        private Label readingValueLabel;
        private Label unitLabel;
        private Label stateLabel;
        private ToolStripMenuItem displaedVluesToolStripMenuItem;
        private ToolStripMenuItem basicToolStripMenuItem;
        private ToolStripMenuItem advancedToolStripMenuItem;
        private ScottPlot.WinForms.FormsPlot formsPlot1;
        private ToolStripMenuItem graphToolStripMenuItem;
        private ToolStripMenuItem scrollingToolStripMenuItem;
        private ToolStripMenuItem continuousToolStripMenuItem;
    }
}