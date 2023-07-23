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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ServerEditPrompt));
            this.label1 = new System.Windows.Forms.Label();
            this.TextBoxServerName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.textBox6 = new System.Windows.Forms.TextBox();
            this.ButtonFolderBrowsing = new System.Windows.Forms.Button();
            this.ButtonFolderBrowsing2 = new System.Windows.Forms.Button();
            this.CheckBoxCracked = new System.Windows.Forms.CheckBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.ComboBoxGamemode = new System.Windows.Forms.ComboBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.checkBox4 = new System.Windows.Forms.CheckBox();
            this.checkBox5 = new System.Windows.Forms.CheckBox();
            this.checkBox7 = new System.Windows.Forms.CheckBox();
            this.checkBox6 = new System.Windows.Forms.CheckBox();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.label9 = new System.Windows.Forms.Label();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.textBox7 = new System.Windows.Forms.TextBox();
            this.numericUpDown3 = new System.Windows.Forms.NumericUpDown();
            this.ButtonOpenServerFolder = new System.Windows.Forms.Button();
            this.ButtonSave = new System.Windows.Forms.Button();
            this.ButtonDeleteServer = new System.Windows.Forms.Button();
            this.ButtonFolderBrowsing3 = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.FolderBrowser = new System.Windows.Forms.FolderBrowserDialog();
            this.label6 = new System.Windows.Forms.Label();
            this.checkBox3 = new System.Windows.Forms.CheckBox();
            this.NumericSpawnProtection = new System.Windows.Forms.NumericUpDown();
            this.CheckBoxSpawnProtection = new System.Windows.Forms.CheckBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.mkeq = new System.Windows.Forms.Label();
            this.ToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.checkBox8 = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumericSpawnProtection)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(14, 20);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(138, 38);
            this.label1.TabIndex = 3;
            this.label1.Text = "Server Name";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TextBoxServerName
            // 
            this.TextBoxServerName.Location = new System.Drawing.Point(14, 58);
            this.TextBoxServerName.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.TextBoxServerName.Name = "TextBoxServerName";
            this.TextBoxServerName.Size = new System.Drawing.Size(138, 26);
            this.TextBoxServerName.TabIndex = 2;
            this.TextBoxServerName.Tag = "";
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(177, 20);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(138, 38);
            this.label2.TabIndex = 5;
            this.label2.Text = "Allocated RAM";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(177, 58);
            this.textBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(138, 26);
            this.textBox1.TabIndex = 4;
            this.textBox1.Tag = "ram";
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Location = new System.Drawing.Point(14, 106);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(138, 38);
            this.label3.TabIndex = 7;
            this.label3.Text = "Server Base Port";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(14, 145);
            this.textBox2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(138, 26);
            this.textBox2.TabIndex = 6;
            this.textBox2.Tag = "baseport";
            // 
            // textBox5
            // 
            this.textBox5.Location = new System.Drawing.Point(14, 337);
            this.textBox5.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new System.Drawing.Size(253, 26);
            this.textBox5.TabIndex = 10;
            this.textBox5.Tag = "serverbackupspath";
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Location = new System.Drawing.Point(14, 385);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(254, 38);
            this.label7.TabIndex = 13;
            this.label7.Text = "Playerdata Backups Path";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBox6
            // 
            this.textBox6.Location = new System.Drawing.Point(14, 428);
            this.textBox6.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new System.Drawing.Size(253, 26);
            this.textBox6.TabIndex = 12;
            this.textBox6.Tag = "playerdatabackupspath";
            // 
            // ButtonFolderBrowsing
            // 
            this.ButtonFolderBrowsing.Location = new System.Drawing.Point(273, 331);
            this.ButtonFolderBrowsing.Name = "ButtonFolderBrowsing";
            this.ButtonFolderBrowsing.Size = new System.Drawing.Size(42, 43);
            this.ButtonFolderBrowsing.TabIndex = 15;
            this.ButtonFolderBrowsing.Tag = "serverbackupspath";
            this.ButtonFolderBrowsing.UseVisualStyleBackColor = true;
            this.ButtonFolderBrowsing.Click += new System.EventHandler(this.ButtonFolderBrowsing_Click);
            // 
            // ButtonFolderBrowsing2
            // 
            this.ButtonFolderBrowsing2.Location = new System.Drawing.Point(273, 422);
            this.ButtonFolderBrowsing2.Name = "ButtonFolderBrowsing2";
            this.ButtonFolderBrowsing2.Size = new System.Drawing.Size(42, 43);
            this.ButtonFolderBrowsing2.TabIndex = 16;
            this.ButtonFolderBrowsing2.Tag = "playerdatabackupspath";
            this.ButtonFolderBrowsing2.UseVisualStyleBackColor = true;
            this.ButtonFolderBrowsing2.Click += new System.EventHandler(this.ButtonFolderBrowsing_Click);
            // 
            // CheckBoxCracked
            // 
            this.CheckBoxCracked.Location = new System.Drawing.Point(342, 52);
            this.CheckBoxCracked.Name = "CheckBoxCracked";
            this.CheckBoxCracked.Size = new System.Drawing.Size(105, 45);
            this.CheckBoxCracked.TabIndex = 17;
            this.CheckBoxCracked.Tag = "online-mode";
            this.CheckBoxCracked.Text = "Cracked";
            this.CheckBoxCracked.UseVisualStyleBackColor = true;
            // 
            // checkBox2
            // 
            this.checkBox2.Location = new System.Drawing.Point(342, 88);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(105, 45);
            this.checkBox2.TabIndex = 18;
            this.checkBox2.Tag = "pvp";
            this.checkBox2.Text = "PVP";
            this.checkBox2.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Location = new System.Drawing.Point(344, 294);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(128, 38);
            this.label5.TabIndex = 21;
            this.label5.Text = "Gamemode";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Location = new System.Drawing.Point(502, 294);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(128, 38);
            this.label8.TabIndex = 23;
            this.label8.Text = "Difficulty";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ComboBoxGamemode
            // 
            this.ComboBoxGamemode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBoxGamemode.FormattingEnabled = true;
            this.ComboBoxGamemode.Items.AddRange(new object[] { "Creative", "Survival" });
            this.ComboBoxGamemode.Location = new System.Drawing.Point(344, 337);
            this.ComboBoxGamemode.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ComboBoxGamemode.Name = "ComboBoxGamemode";
            this.ComboBoxGamemode.Size = new System.Drawing.Size(127, 28);
            this.ComboBoxGamemode.TabIndex = 24;
            this.ComboBoxGamemode.Tag = "gamemode";
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] { "Hard", "Normal", "Easy" });
            this.comboBox1.Location = new System.Drawing.Point(502, 337);
            this.comboBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(127, 28);
            this.comboBox1.TabIndex = 25;
            this.comboBox1.Tag = "difficulty";
            // 
            // checkBox4
            // 
            this.checkBox4.Location = new System.Drawing.Point(450, 52);
            this.checkBox4.Name = "checkBox4";
            this.checkBox4.Size = new System.Drawing.Size(162, 45);
            this.checkBox4.TabIndex = 26;
            this.checkBox4.Tag = "enable-command-block";
            this.checkBox4.Text = "Command Blocks";
            this.checkBox4.UseVisualStyleBackColor = true;
            // 
            // checkBox5
            // 
            this.checkBox5.Location = new System.Drawing.Point(450, 88);
            this.checkBox5.Name = "checkBox5";
            this.checkBox5.Size = new System.Drawing.Size(160, 45);
            this.checkBox5.TabIndex = 27;
            this.checkBox5.Tag = "allow-flight";
            this.checkBox5.Text = "Allow Flight";
            this.checkBox5.UseVisualStyleBackColor = true;
            // 
            // checkBox7
            // 
            this.checkBox7.Location = new System.Drawing.Point(342, 125);
            this.checkBox7.Name = "checkBox7";
            this.checkBox7.Size = new System.Drawing.Size(160, 45);
            this.checkBox7.TabIndex = 30;
            this.checkBox7.Tag = "hardcore";
            this.checkBox7.Text = "Hardcore";
            this.checkBox7.UseVisualStyleBackColor = true;
            // 
            // checkBox6
            // 
            this.checkBox6.Location = new System.Drawing.Point(118, 94);
            this.checkBox6.Name = "checkBox6";
            this.checkBox6.Size = new System.Drawing.Size(161, 44);
            this.checkBox6.TabIndex = 30;
            this.checkBox6.Text = "Spawn Protection";
            this.checkBox6.UseVisualStyleBackColor = true;
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(118, 144);
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(162, 26);
            this.numericUpDown1.TabIndex = 31;
            this.numericUpDown1.Visible = false;
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.Location = new System.Drawing.Point(177, 106);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(138, 38);
            this.label9.TabIndex = 30;
            this.label9.Text = "Seed";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(177, 145);
            this.textBox4.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(138, 26);
            this.textBox4.TabIndex = 29;
            this.textBox4.Tag = "level-seed";
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Location = new System.Drawing.Point(177, 200);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(138, 38);
            this.label4.TabIndex = 34;
            this.label4.Text = "World Size";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label10
            // 
            this.label10.BackColor = System.Drawing.Color.Transparent;
            this.label10.Location = new System.Drawing.Point(14, 200);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(138, 38);
            this.label10.TabIndex = 32;
            this.label10.Text = "Level Type";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBox7
            // 
            this.textBox7.Location = new System.Drawing.Point(14, 238);
            this.textBox7.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textBox7.Name = "textBox7";
            this.textBox7.Size = new System.Drawing.Size(138, 26);
            this.textBox7.TabIndex = 31;
            this.textBox7.Tag = "level-type";
            // 
            // numericUpDown3
            // 
            this.numericUpDown3.Location = new System.Drawing.Point(177, 238);
            this.numericUpDown3.Maximum = new decimal(new int[] { 29999984, 0, 0, 0 });
            this.numericUpDown3.Name = "numericUpDown3";
            this.numericUpDown3.Size = new System.Drawing.Size(138, 26);
            this.numericUpDown3.TabIndex = 34;
            this.numericUpDown3.Tag = "max-world-size";
            // 
            // ButtonOpenServerFolder
            // 
            this.ButtonOpenServerFolder.Location = new System.Drawing.Point(342, 400);
            this.ButtonOpenServerFolder.Name = "ButtonOpenServerFolder";
            this.ButtonOpenServerFolder.Size = new System.Drawing.Size(291, 52);
            this.ButtonOpenServerFolder.TabIndex = 35;
            this.ButtonOpenServerFolder.Text = "Open Server Folder";
            this.ButtonOpenServerFolder.UseVisualStyleBackColor = true;
            this.ButtonOpenServerFolder.Click += new System.EventHandler(this.ButtonOpenServerFolder_Click);
            // 
            // ButtonSave
            // 
            this.ButtonSave.Location = new System.Drawing.Point(342, 460);
            this.ButtonSave.Name = "ButtonSave";
            this.ButtonSave.Size = new System.Drawing.Size(291, 52);
            this.ButtonSave.TabIndex = 36;
            this.ButtonSave.Text = "Save";
            this.ButtonSave.UseVisualStyleBackColor = true;
            this.ButtonSave.Click += new System.EventHandler(this.ButtonSave_Click);
            // 
            // ButtonDeleteServer
            // 
            this.ButtonDeleteServer.BackColor = System.Drawing.Color.IndianRed;
            this.ButtonDeleteServer.FlatAppearance.BorderSize = 0;
            this.ButtonDeleteServer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ButtonDeleteServer.ForeColor = System.Drawing.Color.White;
            this.ButtonDeleteServer.Location = new System.Drawing.Point(507, 518);
            this.ButtonDeleteServer.Name = "ButtonDeleteServer";
            this.ButtonDeleteServer.Size = new System.Drawing.Size(124, 32);
            this.ButtonDeleteServer.TabIndex = 38;
            this.ButtonDeleteServer.Text = "Delete Server";
            this.ButtonDeleteServer.UseVisualStyleBackColor = false;
            this.ButtonDeleteServer.Click += new System.EventHandler(this.ButtonDeleteServer_Click);
            // 
            // ButtonFolderBrowsing3
            // 
            this.ButtonFolderBrowsing3.Location = new System.Drawing.Point(273, 511);
            this.ButtonFolderBrowsing3.Name = "ButtonFolderBrowsing3";
            this.ButtonFolderBrowsing3.Size = new System.Drawing.Size(42, 43);
            this.ButtonFolderBrowsing3.TabIndex = 41;
            this.ButtonFolderBrowsing3.Tag = "javaruntimepath";
            this.ButtonFolderBrowsing3.UseVisualStyleBackColor = true;
            this.ButtonFolderBrowsing3.Click += new System.EventHandler(this.ButtonFolderBrowsing_Click);
            // 
            // label11
            // 
            this.label11.BackColor = System.Drawing.Color.Transparent;
            this.label11.Location = new System.Drawing.Point(14, 474);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(254, 38);
            this.label11.TabIndex = 40;
            this.label11.Text = "Java Runtime";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(14, 517);
            this.textBox3.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(253, 26);
            this.textBox3.TabIndex = 39;
            this.textBox3.Tag = "javaruntimepath";
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Location = new System.Drawing.Point(14, 294);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(254, 38);
            this.label6.TabIndex = 11;
            this.label6.Text = "Server Backups Path";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // checkBox3
            // 
            this.checkBox3.Location = new System.Drawing.Point(450, 125);
            this.checkBox3.Name = "checkBox3";
            this.checkBox3.Size = new System.Drawing.Size(219, 45);
            this.checkBox3.TabIndex = 43;
            this.checkBox3.Tag = "playerdatabackupson";
            this.checkBox3.Text = "Playerdata Backups";
            this.checkBox3.UseVisualStyleBackColor = true;
            // 
            // NumericSpawnProtection
            // 
            this.NumericSpawnProtection.Location = new System.Drawing.Point(452, 206);
            this.NumericSpawnProtection.Name = "NumericSpawnProtection";
            this.NumericSpawnProtection.Size = new System.Drawing.Size(160, 26);
            this.NumericSpawnProtection.TabIndex = 45;
            this.NumericSpawnProtection.Tag = "spawn-protection";
            this.NumericSpawnProtection.Visible = false;
            // 
            // CheckBoxSpawnProtection
            // 
            this.CheckBoxSpawnProtection.Location = new System.Drawing.Point(450, 165);
            this.CheckBoxSpawnProtection.Name = "CheckBoxSpawnProtection";
            this.CheckBoxSpawnProtection.Size = new System.Drawing.Size(186, 45);
            this.CheckBoxSpawnProtection.TabIndex = 44;
            this.CheckBoxSpawnProtection.Tag = "";
            this.CheckBoxSpawnProtection.Text = "Spawn Protection";
            this.CheckBoxSpawnProtection.UseVisualStyleBackColor = true;
            // 
            // checkBox1
            // 
            this.checkBox1.Location = new System.Drawing.Point(342, 158);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(160, 55);
            this.checkBox1.TabIndex = 42;
            this.checkBox1.Tag = "serverbackupson";
            this.checkBox1.Text = "Server \r\nBackups";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // mkeq
            // 
            this.mkeq.Location = new System.Drawing.Point(606, 14);
            this.mkeq.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.mkeq.Name = "mkeq";
            this.mkeq.Size = new System.Drawing.Size(21, 22);
            this.mkeq.TabIndex = 46;
            this.mkeq.Tag = "tooltip";
            this.ToolTip.SetToolTip(this.mkeq, resources.GetString("mkeq.ToolTip"));
            // 
            // checkBox8
            // 
            this.checkBox8.Location = new System.Drawing.Point(344, 513);
            this.checkBox8.Name = "checkBox8";
            this.checkBox8.Size = new System.Drawing.Size(114, 45);
            this.checkBox8.TabIndex = 47;
            this.checkBox8.Tag = "upnpon";
            this.checkBox8.Text = "Use UPnP";
            this.checkBox8.UseVisualStyleBackColor = true;
            // 
            // ServerEditPrompt
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(645, 568);
            this.Controls.Add(this.checkBox8);
            this.Controls.Add(this.mkeq);
            this.Controls.Add(this.NumericSpawnProtection);
            this.Controls.Add(this.CheckBoxSpawnProtection);
            this.Controls.Add(this.checkBox3);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.ButtonFolderBrowsing3);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.ButtonDeleteServer);
            this.Controls.Add(this.checkBox4);
            this.Controls.Add(this.ButtonSave);
            this.Controls.Add(this.checkBox7);
            this.Controls.Add(this.checkBox2);
            this.Controls.Add(this.ButtonOpenServerFolder);
            this.Controls.Add(this.CheckBoxCracked);
            this.Controls.Add(this.numericUpDown3);
            this.Controls.Add(this.checkBox5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.textBox7);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.textBox4);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.ComboBoxGamemode);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.ButtonFolderBrowsing2);
            this.Controls.Add(this.ButtonFolderBrowsing);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.textBox6);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.textBox5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.TextBoxServerName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Location = new System.Drawing.Point(15, 15);
            this.Name = "ServerEditPrompt";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ServerEditPrompt_FormClosed);
            this.Load += new System.EventHandler(this.ServerEditPrompt_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumericSpawnProtection)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
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