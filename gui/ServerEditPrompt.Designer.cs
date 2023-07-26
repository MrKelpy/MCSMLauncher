using System.ComponentModel;

namespace MCSMLauncher.gui
{
    partial class ServerEditPrompt
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ServerEditPrompt));
            label1 = new System.Windows.Forms.Label();
            TextBoxServerName = new System.Windows.Forms.TextBox();
            label2 = new System.Windows.Forms.Label();
            textBox1 = new System.Windows.Forms.TextBox();
            label3 = new System.Windows.Forms.Label();
            textBox2 = new System.Windows.Forms.TextBox();
            textBox5 = new System.Windows.Forms.TextBox();
            label7 = new System.Windows.Forms.Label();
            textBox6 = new System.Windows.Forms.TextBox();
            ButtonFolderBrowsing = new System.Windows.Forms.Button();
            ButtonFolderBrowsing2 = new System.Windows.Forms.Button();
            CheckBoxCracked = new System.Windows.Forms.CheckBox();
            checkBox2 = new System.Windows.Forms.CheckBox();
            label5 = new System.Windows.Forms.Label();
            label8 = new System.Windows.Forms.Label();
            ComboBoxGamemode = new System.Windows.Forms.ComboBox();
            comboBox1 = new System.Windows.Forms.ComboBox();
            checkBox4 = new System.Windows.Forms.CheckBox();
            checkBox5 = new System.Windows.Forms.CheckBox();
            checkBox7 = new System.Windows.Forms.CheckBox();
            checkBox6 = new System.Windows.Forms.CheckBox();
            numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            label9 = new System.Windows.Forms.Label();
            textBox4 = new System.Windows.Forms.TextBox();
            label4 = new System.Windows.Forms.Label();
            label10 = new System.Windows.Forms.Label();
            textBox7 = new System.Windows.Forms.TextBox();
            numericUpDown3 = new System.Windows.Forms.NumericUpDown();
            ButtonOpenServerFolder = new System.Windows.Forms.Button();
            ButtonSave = new System.Windows.Forms.Button();
            ButtonDeleteServer = new System.Windows.Forms.Button();
            ButtonFolderBrowsing3 = new System.Windows.Forms.Button();
            label11 = new System.Windows.Forms.Label();
            textBox3 = new System.Windows.Forms.TextBox();
            FolderBrowser = new System.Windows.Forms.FolderBrowserDialog();
            label6 = new System.Windows.Forms.Label();
            checkBox3 = new System.Windows.Forms.CheckBox();
            NumericSpawnProtection = new System.Windows.Forms.NumericUpDown();
            CheckBoxSpawnProtection = new System.Windows.Forms.CheckBox();
            checkBox1 = new System.Windows.Forms.CheckBox();
            mkeq = new System.Windows.Forms.Label();
            ToolTip = new System.Windows.Forms.ToolTip(components);
            checkBox8 = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(numericUpDown1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(numericUpDown3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(NumericSpawnProtection)).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.BackColor = System.Drawing.Color.Transparent;
            label1.Location = new System.Drawing.Point(14, 20);
            label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(138, 38);
            label1.TabIndex = 3;
            label1.Text = "Server Name";
            label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TextBoxServerName
            // 
            TextBoxServerName.Location = new System.Drawing.Point(14, 58);
            TextBoxServerName.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            TextBoxServerName.Name = "TextBoxServerName";
            TextBoxServerName.Size = new System.Drawing.Size(138, 26);
            TextBoxServerName.TabIndex = 2;
            TextBoxServerName.Tag = "";
            // 
            // label2
            // 
            label2.BackColor = System.Drawing.Color.Transparent;
            label2.Location = new System.Drawing.Point(177, 20);
            label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(138, 38);
            label2.TabIndex = 5;
            label2.Text = "Allocated RAM";
            label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBox1
            // 
            textBox1.Location = new System.Drawing.Point(177, 58);
            textBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            textBox1.Name = "textBox1";
            textBox1.Size = new System.Drawing.Size(138, 26);
            textBox1.TabIndex = 4;
            textBox1.Tag = "ram";
            // 
            // label3
            // 
            label3.BackColor = System.Drawing.Color.Transparent;
            label3.Location = new System.Drawing.Point(14, 106);
            label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(138, 38);
            label3.TabIndex = 7;
            label3.Text = "Server Base Port";
            label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBox2
            // 
            textBox2.Location = new System.Drawing.Point(14, 145);
            textBox2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            textBox2.Name = "textBox2";
            textBox2.Size = new System.Drawing.Size(138, 26);
            textBox2.TabIndex = 6;
            textBox2.Tag = "baseport";
            // 
            // textBox5
            // 
            textBox5.Location = new System.Drawing.Point(14, 337);
            textBox5.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            textBox5.Name = "textBox5";
            textBox5.Size = new System.Drawing.Size(253, 26);
            textBox5.TabIndex = 10;
            textBox5.Tag = "serverbackupspath";
            // 
            // label7
            // 
            label7.BackColor = System.Drawing.Color.Transparent;
            label7.Location = new System.Drawing.Point(14, 385);
            label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label7.Name = "label7";
            label7.Size = new System.Drawing.Size(254, 38);
            label7.TabIndex = 13;
            label7.Text = "Playerdata Backups Path";
            label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBox6
            // 
            textBox6.Location = new System.Drawing.Point(14, 428);
            textBox6.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            textBox6.Name = "textBox6";
            textBox6.Size = new System.Drawing.Size(253, 26);
            textBox6.TabIndex = 12;
            textBox6.Tag = "playerdatabackupspath";
            // 
            // ButtonFolderBrowsing
            // 
            ButtonFolderBrowsing.Location = new System.Drawing.Point(273, 331);
            ButtonFolderBrowsing.Name = "ButtonFolderBrowsing";
            ButtonFolderBrowsing.Size = new System.Drawing.Size(42, 43);
            ButtonFolderBrowsing.TabIndex = 15;
            ButtonFolderBrowsing.Tag = "serverbackupspath";
            ButtonFolderBrowsing.UseVisualStyleBackColor = true;
            ButtonFolderBrowsing.Click += new System.EventHandler(ButtonFolderBrowsing_Click);
            // 
            // ButtonFolderBrowsing2
            // 
            ButtonFolderBrowsing2.Location = new System.Drawing.Point(273, 422);
            ButtonFolderBrowsing2.Name = "ButtonFolderBrowsing2";
            ButtonFolderBrowsing2.Size = new System.Drawing.Size(42, 43);
            ButtonFolderBrowsing2.TabIndex = 16;
            ButtonFolderBrowsing2.Tag = "playerdatabackupspath";
            ButtonFolderBrowsing2.UseVisualStyleBackColor = true;
            ButtonFolderBrowsing2.Click += new System.EventHandler(ButtonFolderBrowsing_Click);
            // 
            // CheckBoxCracked
            // 
            CheckBoxCracked.Location = new System.Drawing.Point(342, 52);
            CheckBoxCracked.Name = "CheckBoxCracked";
            CheckBoxCracked.Size = new System.Drawing.Size(105, 45);
            CheckBoxCracked.TabIndex = 17;
            CheckBoxCracked.Tag = "online-mode";
            CheckBoxCracked.Text = "Cracked";
            CheckBoxCracked.UseVisualStyleBackColor = true;
            // 
            // checkBox2
            // 
            checkBox2.Location = new System.Drawing.Point(342, 88);
            checkBox2.Name = "checkBox2";
            checkBox2.Size = new System.Drawing.Size(105, 45);
            checkBox2.TabIndex = 18;
            checkBox2.Tag = "pvp";
            checkBox2.Text = "PVP";
            checkBox2.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            label5.BackColor = System.Drawing.Color.Transparent;
            label5.Location = new System.Drawing.Point(344, 294);
            label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label5.Name = "label5";
            label5.Size = new System.Drawing.Size(128, 38);
            label5.TabIndex = 21;
            label5.Text = "Gamemode";
            label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label8
            // 
            label8.BackColor = System.Drawing.Color.Transparent;
            label8.Location = new System.Drawing.Point(502, 294);
            label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label8.Name = "label8";
            label8.Size = new System.Drawing.Size(128, 38);
            label8.TabIndex = 23;
            label8.Text = "Difficulty";
            label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ComboBoxGamemode
            // 
            ComboBoxGamemode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            ComboBoxGamemode.FormattingEnabled = true;
            ComboBoxGamemode.Items.AddRange(new object[] { "Creative", "Survival" });
            ComboBoxGamemode.Location = new System.Drawing.Point(344, 337);
            ComboBoxGamemode.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            ComboBoxGamemode.Name = "ComboBoxGamemode";
            ComboBoxGamemode.Size = new System.Drawing.Size(127, 28);
            ComboBoxGamemode.TabIndex = 24;
            ComboBoxGamemode.Tag = "gamemode";
            // 
            // comboBox1
            // 
            comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            comboBox1.FormattingEnabled = true;
            comboBox1.Items.AddRange(new object[] { "Hard", "Normal", "Easy" });
            comboBox1.Location = new System.Drawing.Point(502, 337);
            comboBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new System.Drawing.Size(127, 28);
            comboBox1.TabIndex = 25;
            comboBox1.Tag = "difficulty";
            // 
            // checkBox4
            // 
            checkBox4.Location = new System.Drawing.Point(450, 52);
            checkBox4.Name = "checkBox4";
            checkBox4.Size = new System.Drawing.Size(162, 45);
            checkBox4.TabIndex = 26;
            checkBox4.Tag = "enable-command-block";
            checkBox4.Text = "Command Blocks";
            checkBox4.UseVisualStyleBackColor = true;
            // 
            // checkBox5
            // 
            checkBox5.Location = new System.Drawing.Point(450, 88);
            checkBox5.Name = "checkBox5";
            checkBox5.Size = new System.Drawing.Size(160, 45);
            checkBox5.TabIndex = 27;
            checkBox5.Tag = "allow-flight";
            checkBox5.Text = "Allow Flight";
            checkBox5.UseVisualStyleBackColor = true;
            // 
            // checkBox7
            // 
            checkBox7.Location = new System.Drawing.Point(342, 125);
            checkBox7.Name = "checkBox7";
            checkBox7.Size = new System.Drawing.Size(160, 45);
            checkBox7.TabIndex = 30;
            checkBox7.Tag = "hardcore";
            checkBox7.Text = "Hardcore";
            checkBox7.UseVisualStyleBackColor = true;
            // 
            // checkBox6
            // 
            checkBox6.Location = new System.Drawing.Point(118, 94);
            checkBox6.Name = "checkBox6";
            checkBox6.Size = new System.Drawing.Size(161, 44);
            checkBox6.TabIndex = 30;
            checkBox6.Text = "Spawn Protection";
            checkBox6.UseVisualStyleBackColor = true;
            // 
            // numericUpDown1
            // 
            numericUpDown1.Location = new System.Drawing.Point(118, 144);
            numericUpDown1.Name = "numericUpDown1";
            numericUpDown1.Size = new System.Drawing.Size(162, 26);
            numericUpDown1.TabIndex = 31;
            numericUpDown1.Visible = false;
            // 
            // label9
            // 
            label9.BackColor = System.Drawing.Color.Transparent;
            label9.Location = new System.Drawing.Point(177, 106);
            label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label9.Name = "label9";
            label9.Size = new System.Drawing.Size(138, 38);
            label9.TabIndex = 30;
            label9.Text = "Seed";
            label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBox4
            // 
            textBox4.Location = new System.Drawing.Point(177, 145);
            textBox4.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            textBox4.Name = "textBox4";
            textBox4.Size = new System.Drawing.Size(138, 26);
            textBox4.TabIndex = 29;
            textBox4.Tag = "level-seed";
            // 
            // label4
            // 
            label4.BackColor = System.Drawing.Color.Transparent;
            label4.Location = new System.Drawing.Point(177, 200);
            label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(138, 38);
            label4.TabIndex = 34;
            label4.Text = "World Size";
            label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label10
            // 
            label10.BackColor = System.Drawing.Color.Transparent;
            label10.Location = new System.Drawing.Point(14, 200);
            label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label10.Name = "label10";
            label10.Size = new System.Drawing.Size(138, 38);
            label10.TabIndex = 32;
            label10.Text = "Level Type";
            label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBox7
            // 
            textBox7.Location = new System.Drawing.Point(14, 238);
            textBox7.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            textBox7.Name = "textBox7";
            textBox7.Size = new System.Drawing.Size(138, 26);
            textBox7.TabIndex = 31;
            textBox7.Tag = "level-type";
            // 
            // numericUpDown3
            // 
            numericUpDown3.Location = new System.Drawing.Point(177, 238);
            numericUpDown3.Maximum = new decimal(new int[] { 29999984, 0, 0, 0 });
            numericUpDown3.Name = "numericUpDown3";
            numericUpDown3.Size = new System.Drawing.Size(138, 26);
            numericUpDown3.TabIndex = 34;
            numericUpDown3.Tag = "max-world-size";
            // 
            // ButtonOpenServerFolder
            // 
            ButtonOpenServerFolder.Location = new System.Drawing.Point(342, 400);
            ButtonOpenServerFolder.Name = "ButtonOpenServerFolder";
            ButtonOpenServerFolder.Size = new System.Drawing.Size(291, 52);
            ButtonOpenServerFolder.TabIndex = 35;
            ButtonOpenServerFolder.Text = "Open Server Folder";
            ButtonOpenServerFolder.UseVisualStyleBackColor = true;
            ButtonOpenServerFolder.Click += new System.EventHandler(ButtonOpenServerFolder_Click);
            // 
            // ButtonSave
            // 
            ButtonSave.Location = new System.Drawing.Point(342, 460);
            ButtonSave.Name = "ButtonSave";
            ButtonSave.Size = new System.Drawing.Size(291, 52);
            ButtonSave.TabIndex = 36;
            ButtonSave.Text = "Save";
            ButtonSave.UseVisualStyleBackColor = true;
            ButtonSave.Click += new System.EventHandler(ButtonSave_Click);
            // 
            // ButtonDeleteServer
            // 
            ButtonDeleteServer.BackColor = System.Drawing.Color.IndianRed;
            ButtonDeleteServer.FlatAppearance.BorderSize = 0;
            ButtonDeleteServer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            ButtonDeleteServer.ForeColor = System.Drawing.Color.White;
            ButtonDeleteServer.Location = new System.Drawing.Point(507, 518);
            ButtonDeleteServer.Name = "ButtonDeleteServer";
            ButtonDeleteServer.Size = new System.Drawing.Size(124, 32);
            ButtonDeleteServer.TabIndex = 38;
            ButtonDeleteServer.Text = "Delete Server";
            ButtonDeleteServer.UseVisualStyleBackColor = false;
            ButtonDeleteServer.Click += new System.EventHandler(ButtonDeleteServer_Click);
            // 
            // ButtonFolderBrowsing3
            // 
            ButtonFolderBrowsing3.Location = new System.Drawing.Point(273, 511);
            ButtonFolderBrowsing3.Name = "ButtonFolderBrowsing3";
            ButtonFolderBrowsing3.Size = new System.Drawing.Size(42, 43);
            ButtonFolderBrowsing3.TabIndex = 41;
            ButtonFolderBrowsing3.Tag = "javaruntimepath";
            ButtonFolderBrowsing3.UseVisualStyleBackColor = true;
            ButtonFolderBrowsing3.Click += new System.EventHandler(ButtonFolderBrowsing_Click);
            // 
            // label11
            // 
            label11.BackColor = System.Drawing.Color.Transparent;
            label11.Location = new System.Drawing.Point(14, 474);
            label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label11.Name = "label11";
            label11.Size = new System.Drawing.Size(254, 38);
            label11.TabIndex = 40;
            label11.Text = "Java Runtime";
            label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBox3
            // 
            textBox3.Location = new System.Drawing.Point(14, 517);
            textBox3.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            textBox3.Name = "textBox3";
            textBox3.Size = new System.Drawing.Size(253, 26);
            textBox3.TabIndex = 39;
            textBox3.Tag = "javaruntimepath";
            // 
            // label6
            // 
            label6.BackColor = System.Drawing.Color.Transparent;
            label6.Location = new System.Drawing.Point(14, 294);
            label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label6.Name = "label6";
            label6.Size = new System.Drawing.Size(254, 38);
            label6.TabIndex = 11;
            label6.Text = "Server Backups Path";
            label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // checkBox3
            // 
            checkBox3.Location = new System.Drawing.Point(450, 125);
            checkBox3.Name = "checkBox3";
            checkBox3.Size = new System.Drawing.Size(219, 45);
            checkBox3.TabIndex = 43;
            checkBox3.Tag = "playerdatabackupson";
            checkBox3.Text = "Playerdata Backups";
            checkBox3.UseVisualStyleBackColor = true;
            // 
            // NumericSpawnProtection
            // 
            NumericSpawnProtection.Location = new System.Drawing.Point(452, 206);
            NumericSpawnProtection.Name = "NumericSpawnProtection";
            NumericSpawnProtection.Size = new System.Drawing.Size(160, 26);
            NumericSpawnProtection.TabIndex = 45;
            NumericSpawnProtection.Tag = "spawn-protection";
            NumericSpawnProtection.Visible = false;
            // 
            // CheckBoxSpawnProtection
            // 
            CheckBoxSpawnProtection.Location = new System.Drawing.Point(450, 165);
            CheckBoxSpawnProtection.Name = "CheckBoxSpawnProtection";
            CheckBoxSpawnProtection.Size = new System.Drawing.Size(186, 45);
            CheckBoxSpawnProtection.TabIndex = 44;
            CheckBoxSpawnProtection.Tag = "";
            CheckBoxSpawnProtection.Text = "Spawn Protection";
            CheckBoxSpawnProtection.UseVisualStyleBackColor = true;
            // 
            // checkBox1
            // 
            checkBox1.Location = new System.Drawing.Point(342, 158);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new System.Drawing.Size(160, 55);
            checkBox1.TabIndex = 42;
            checkBox1.Tag = "serverbackupson";
            checkBox1.Text = "Server \r\nBackups";
            checkBox1.UseVisualStyleBackColor = true;
            // 
            // mkeq
            // 
            mkeq.Location = new System.Drawing.Point(606, 14);
            mkeq.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            mkeq.Name = "mkeq";
            mkeq.Size = new System.Drawing.Size(21, 22);
            mkeq.TabIndex = 46;
            mkeq.Tag = "tooltip";
            ToolTip.SetToolTip(mkeq, resources.GetString("mkeq.ToolTip"));
            // 
            // checkBox8
            // 
            checkBox8.Location = new System.Drawing.Point(344, 513);
            checkBox8.Name = "checkBox8";
            checkBox8.Size = new System.Drawing.Size(114, 45);
            checkBox8.TabIndex = 47;
            checkBox8.Tag = "upnpon";
            checkBox8.Text = "Use UPnP";
            checkBox8.UseVisualStyleBackColor = true;
            // 
            // ServerEditPrompt
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.SystemColors.Control;
            ClientSize = new System.Drawing.Size(645, 568);
            Controls.Add(checkBox8);
            Controls.Add(mkeq);
            Controls.Add(NumericSpawnProtection);
            Controls.Add(CheckBoxSpawnProtection);
            Controls.Add(checkBox3);
            Controls.Add(checkBox1);
            Controls.Add(ButtonFolderBrowsing3);
            Controls.Add(label11);
            Controls.Add(textBox3);
            Controls.Add(ButtonDeleteServer);
            Controls.Add(checkBox4);
            Controls.Add(ButtonSave);
            Controls.Add(checkBox7);
            Controls.Add(checkBox2);
            Controls.Add(ButtonOpenServerFolder);
            Controls.Add(CheckBoxCracked);
            Controls.Add(numericUpDown3);
            Controls.Add(checkBox5);
            Controls.Add(label4);
            Controls.Add(label10);
            Controls.Add(textBox7);
            Controls.Add(label9);
            Controls.Add(textBox4);
            Controls.Add(comboBox1);
            Controls.Add(ComboBoxGamemode);
            Controls.Add(label8);
            Controls.Add(label5);
            Controls.Add(ButtonFolderBrowsing2);
            Controls.Add(ButtonFolderBrowsing);
            Controls.Add(label7);
            Controls.Add(textBox6);
            Controls.Add(label6);
            Controls.Add(textBox5);
            Controls.Add(label3);
            Controls.Add(textBox2);
            Controls.Add(label2);
            Controls.Add(textBox1);
            Controls.Add(label1);
            Controls.Add(TextBoxServerName);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            Location = new System.Drawing.Point(15, 15);
            Name = "ServerEditPrompt";
            FormClosed += new System.Windows.Forms.FormClosedEventHandler(ServerEditPrompt_FormClosed);
            Load += new System.EventHandler(ServerEditPrompt_Load);
            ((System.ComponentModel.ISupportInitialize)(numericUpDown1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(numericUpDown3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(NumericSpawnProtection)).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        private System.Windows.Forms.CheckBox checkBox8;

        private System.Windows.Forms.ToolTip ToolTip;

        private System.Windows.Forms.Label mkeq;

        private System.Windows.Forms.CheckBox CheckBoxSpawnProtection;
        private System.Windows.Forms.NumericUpDown NumericSpawnProtection;

        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox checkBox3;

        private System.Windows.Forms.CheckBox checkBox1;

        private System.Windows.Forms.FolderBrowserDialog FolderBrowser;

        private System.Windows.Forms.Button ButtonFolderBrowsing3;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox textBox3;

        private System.Windows.Forms.Button ButtonDeleteServer;

        private System.Windows.Forms.Button ButtonOpenServerFolder;
        private System.Windows.Forms.Button ButtonSave;

        private System.Windows.Forms.NumericUpDown numericUpDown3;

        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox textBox7;

        private System.Windows.Forms.CheckBox checkBox6;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.CheckBox checkBox7;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox textBox4;

        private System.Windows.Forms.CheckBox checkBox5;

        private System.Windows.Forms.CheckBox checkBox4;

        private System.Windows.Forms.ComboBox ComboBoxGamemode;
        private System.Windows.Forms.ComboBox comboBox1;

        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label8;

        private System.Windows.Forms.CheckBox CheckBoxCracked;

        private System.Windows.Forms.Button ButtonFolderBrowsing;
        private System.Windows.Forms.Button ButtonFolderBrowsing2;

        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBox6;

        private System.Windows.Forms.Label label4;

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox2;

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox TextBoxServerName;

        #endregion
    }
}