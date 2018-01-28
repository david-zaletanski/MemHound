namespace MemHound
{
    partial class frmMain
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
            this.outputTextBox = new System.Windows.Forms.RichTextBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openProcessToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.queryVirtualMemoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.readMemoryAddressToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pointerFinderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.baseAddressToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.currentProcessTxtBox = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.button2 = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.button3 = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.button5 = new System.Windows.Forms.Button();
            this.writeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editSelectedValueToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // outputTextBox
            // 
            this.outputTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.outputTextBox.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.outputTextBox.Location = new System.Drawing.Point(11, 327);
            this.outputTextBox.Name = "outputTextBox";
            this.outputTextBox.Size = new System.Drawing.Size(571, 125);
            this.outputTextBox.TabIndex = 0;
            this.outputTextBox.Text = "";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.toolsToolStripMenuItem,
            this.writeToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(595, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openProcessToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openProcessToolStripMenuItem
            // 
            this.openProcessToolStripMenuItem.Name = "openProcessToolStripMenuItem";
            this.openProcessToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.openProcessToolStripMenuItem.Text = "Open Process";
            this.openProcessToolStripMenuItem.Click += new System.EventHandler(this.openProcessToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.queryVirtualMemoryToolStripMenuItem,
            this.readMemoryAddressToolStripMenuItem,
            this.pointerFinderToolStripMenuItem,
            this.baseAddressToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.toolsToolStripMenuItem.Text = "Tools";
            // 
            // queryVirtualMemoryToolStripMenuItem
            // 
            this.queryVirtualMemoryToolStripMenuItem.Name = "queryVirtualMemoryToolStripMenuItem";
            this.queryVirtualMemoryToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
            this.queryVirtualMemoryToolStripMenuItem.Text = "Query Virtual Memory";
            this.queryVirtualMemoryToolStripMenuItem.Click += new System.EventHandler(this.queryVirtualMemoryToolStripMenuItem_Click);
            // 
            // readMemoryAddressToolStripMenuItem
            // 
            this.readMemoryAddressToolStripMenuItem.Name = "readMemoryAddressToolStripMenuItem";
            this.readMemoryAddressToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
            this.readMemoryAddressToolStripMenuItem.Text = "Read Memory Address";
            this.readMemoryAddressToolStripMenuItem.Click += new System.EventHandler(this.readMemoryAddressToolStripMenuItem_Click);
            // 
            // pointerFinderToolStripMenuItem
            // 
            this.pointerFinderToolStripMenuItem.Name = "pointerFinderToolStripMenuItem";
            this.pointerFinderToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
            this.pointerFinderToolStripMenuItem.Text = "Pointer Finder";
            this.pointerFinderToolStripMenuItem.Click += new System.EventHandler(this.pointerFinderToolStripMenuItem_Click);
            // 
            // baseAddressToolStripMenuItem
            // 
            this.baseAddressToolStripMenuItem.Name = "baseAddressToolStripMenuItem";
            this.baseAddressToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
            this.baseAddressToolStripMenuItem.Text = "Base Address";
            this.baseAddressToolStripMenuItem.Click += new System.EventHandler(this.baseAddressToolStripMenuItem_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Current Process:";
            // 
            // currentProcessTxtBox
            // 
            this.currentProcessTxtBox.Location = new System.Drawing.Point(103, 30);
            this.currentProcessTxtBox.Name = "currentProcessTxtBox";
            this.currentProcessTxtBox.ReadOnly = true;
            this.currentProcessTxtBox.Size = new System.Drawing.Size(139, 20);
            this.currentProcessTxtBox.TabIndex = 3;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(248, 29);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(50, 21);
            this.button1.TabIndex = 4;
            this.button1.Text = "Close";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 60);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Search For:";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(81, 57);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(139, 20);
            this.textBox1.TabIndex = 7;
            this.textBox1.Text = "42";
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "Int16",
            "Int32",
            "UInt32",
            "Int64",
            "UInt64",
            "Float",
            "Double"});
            this.comboBox1.Location = new System.Drawing.Point(226, 57);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(112, 21);
            this.comboBox1.TabIndex = 8;
            this.comboBox1.Text = "Int32";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(358, 55);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(71, 22);
            this.button2.TabIndex = 9;
            this.button2.Text = "Initial Scan";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 93);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(571, 228);
            this.dataGridView1.TabIndex = 15;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(435, 55);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(71, 22);
            this.button3.TabIndex = 16;
            this.button3.Text = "Update Scan";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(434, 29);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(148, 20);
            this.progressBar1.TabIndex = 17;
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(511, 55);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(71, 22);
            this.button5.TabIndex = 24;
            this.button5.Text = "Refresh";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // writeToolStripMenuItem
            // 
            this.writeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editSelectedValueToolStripMenuItem});
            this.writeToolStripMenuItem.Name = "writeToolStripMenuItem";
            this.writeToolStripMenuItem.Size = new System.Drawing.Size(47, 20);
            this.writeToolStripMenuItem.Text = "Write";
            // 
            // editSelectedValueToolStripMenuItem
            // 
            this.editSelectedValueToolStripMenuItem.Name = "editSelectedValueToolStripMenuItem";
            this.editSelectedValueToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.editSelectedValueToolStripMenuItem.Text = "Edit Selected Value";
            this.editSelectedValueToolStripMenuItem.Click += new System.EventHandler(this.editSelectedValueToolStripMenuItem_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(595, 461);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.currentProcessTxtBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.outputTextBox);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "frmMain";
            this.Text = ".: MemHound :.";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox outputTextBox;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openProcessToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox currentProcessTxtBox;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem queryVirtualMemoryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem readMemoryAddressToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pointerFinderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem baseAddressToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem writeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editSelectedValueToolStripMenuItem;
    }
}

