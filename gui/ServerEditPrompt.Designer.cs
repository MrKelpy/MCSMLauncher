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
            this.label1 = new System.Windows.Forms.Label();
            this.TextBoxServerName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
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
            this.CheckBoxSpawnProtection = new System.Windows.Forms.CheckBox();
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
            this.NumericSpawnProtection = new System.Windows.Forms.NumericUpDown();
            this.ButtonDeleteServer = new System.Windows.Forms.Button();
            this.ButtonFolderBrowsing3 = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.FolderBrowser = new System.Windows.Forms.FolderBrowserDialog();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumericSpawnProtection)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(9, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 25);
            this.label1.TabIndex = 3;
            this.label1.Text = "Server Name";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TextBoxServerName
            // 
            this.TextBoxServerName.Location = new System.Drawing.Point(9, 38);
            this.TextBoxServerName.Name = "TextBoxServerName";
            this.TextBoxServerName.Size = new System.Drawing.Size(93, 20);
            this.TextBoxServerName.TabIndex = 2;
            this.TextBoxServerName.Tag = "";
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(118, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(92, 25);
            this.label2.TabIndex = 5;
            this.label2.Text = "Allocated RAM";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(118, 38);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(93, 20);
            this.textBox1.TabIndex = 4;
            this.textBox1.Tag = "ram";
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Location = new System.Drawing.Point(9, 69);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(92, 25);
            this.label3.TabIndex = 7;
            this.label3.Text = "Server Base Port";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(9, 94);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(93, 20);
            this.textBox2.TabIndex = 6;
            this.textBox2.Tag = "server-port";
            // 
            // textBox5
            // 
            this.textBox5.Location = new System.Drawing.Point(9, 219);
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new System.Drawing.Size(170, 20);
            this.textBox5.TabIndex = 10;
            this.textBox5.Tag = "serverbackupspath";
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Location = new System.Drawing.Point(9, 191);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(169, 25);
            this.label6.TabIndex = 11;
            this.label6.Text = "Server Backups Path";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Location = new System.Drawing.Point(9, 250);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(169, 25);
            this.label7.TabIndex = 13;
            this.label7.Text = "Playerdata Backups Path";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBox6
            // 
            this.textBox6.Location = new System.Drawing.Point(9, 278);
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new System.Drawing.Size(170, 20);
            this.textBox6.TabIndex = 12;
            this.textBox6.Tag = "playerdatabackupspath";
            // 
            // ButtonFolderBrowsing
            // 
            this.ButtonFolderBrowsing.Location = new System.Drawing.Point(182, 215);
            this.ButtonFolderBrowsing.Margin = new System.Windows.Forms.Padding(2);
            this.ButtonFolderBrowsing.Name = "ButtonFolderBrowsing";
            this.ButtonFolderBrowsing.Size = new System.Drawing.Size(28, 28);
            this.ButtonFolderBrowsing.TabIndex = 15;
            this.ButtonFolderBrowsing.Tag = "serverbackupspath";
            this.ButtonFolderBrowsing.UseVisualStyleBackColor = true;
            this.ButtonFolderBrowsing.Click += new System.EventHandler(this.ButtonFolderBrowsing_Click);
            // 
            // ButtonFolderBrowsing2
            // 
            this.ButtonFolderBrowsing2.Location = new System.Drawing.Point(182, 274);
            this.ButtonFolderBrowsing2.Margin = new System.Windows.Forms.Padding(2);
            this.ButtonFolderBrowsing2.Name = "ButtonFolderBrowsing2";
            this.ButtonFolderBrowsing2.Size = new System.Drawing.Size(28, 28);
            this.ButtonFolderBrowsing2.TabIndex = 16;
            this.ButtonFolderBrowsing2.Tag = "playerdatabackupspath";
            this.ButtonFolderBrowsing2.UseVisualStyleBackColor = true;
            this.ButtonFolderBrowsing2.Click += new System.EventHandler(this.ButtonFolderBrowsing_Click);
            // 
            // CheckBoxCracked
            // 
            this.CheckBoxCracked.Location = new System.Drawing.Point(228, 34);
            this.CheckBoxCracked.Margin = new System.Windows.Forms.Padding(2);
            this.CheckBoxCracked.Name = "CheckBoxCracked";
            this.CheckBoxCracked.Size = new System.Drawing.Size(70, 29);
            this.CheckBoxCracked.TabIndex = 17;
            this.CheckBoxCracked.Tag = "online-mode";
            this.CheckBoxCracked.Text = "Cracked";
            this.CheckBoxCracked.UseVisualStyleBackColor = true;
            // 
            // checkBox2
            // 
            this.checkBox2.Location = new System.Drawing.Point(228, 57);
            this.checkBox2.Margin = new System.Windows.Forms.Padding(2);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(70, 29);
            this.checkBox2.TabIndex = 18;
            this.checkBox2.Tag = "pvp";
            this.checkBox2.Text = "PVP";
            this.checkBox2.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Location = new System.Drawing.Point(229, 191);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(85, 25);
            this.label5.TabIndex = 21;
            this.label5.Text = "Gamemode";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Location = new System.Drawing.Point(335, 191);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(85, 25);
            this.label8.TabIndex = 23;
            this.label8.Text = "Difficulty";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ComboBoxGamemode
            // 
            this.ComboBoxGamemode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBoxGamemode.FormattingEnabled = true;
            this.ComboBoxGamemode.Items.AddRange(new object[] { "Creative", "Survival" });
            this.ComboBoxGamemode.Location = new System.Drawing.Point(229, 219);
            this.ComboBoxGamemode.Name = "ComboBoxGamemode";
            this.ComboBoxGamemode.Size = new System.Drawing.Size(86, 21);
            this.ComboBoxGamemode.TabIndex = 24;
            this.ComboBoxGamemode.Tag = "gamemode";
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] { "Hard", "Normal", "Easy" });
            this.comboBox1.Location = new System.Drawing.Point(335, 219);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(86, 21);
            this.comboBox1.TabIndex = 25;
            this.comboBox1.Tag = "difficulty";
            // 
            // checkBox4
            // 
            this.checkBox4.Location = new System.Drawing.Point(300, 34);
            this.checkBox4.Margin = new System.Windows.Forms.Padding(2);
            this.checkBox4.Name = "checkBox4";
            this.checkBox4.Size = new System.Drawing.Size(108, 29);
            this.checkBox4.TabIndex = 26;
            this.checkBox4.Tag = "enable-command-block";
            this.checkBox4.Text = "Command Blocks";
            this.checkBox4.UseVisualStyleBackColor = true;
            // 
            // checkBox5
            // 
            this.checkBox5.Location = new System.Drawing.Point(300, 57);
            this.checkBox5.Margin = new System.Windows.Forms.Padding(2);
            this.checkBox5.Name = "checkBox5";
            this.checkBox5.Size = new System.Drawing.Size(107, 29);
            this.checkBox5.TabIndex = 27;
            this.checkBox5.Tag = "allow-flight";
            this.checkBox5.Text = "Allow Flight";
            this.checkBox5.UseVisualStyleBackColor = true;
            // 
            // CheckBoxSpawnProtection
            // 
            this.CheckBoxSpawnProtection.Location = new System.Drawing.Point(300, 81);
            this.CheckBoxSpawnProtection.Margin = new System.Windows.Forms.Padding(2);
            this.CheckBoxSpawnProtection.Name = "CheckBoxSpawnProtection";
            this.CheckBoxSpawnProtection.Size = new System.Drawing.Size(124, 29);
            this.CheckBoxSpawnProtection.TabIndex = 32;
            this.CheckBoxSpawnProtection.Text = "Spawn Protection";
            this.CheckBoxSpawnProtection.UseVisualStyleBackColor = true;
            this.CheckBoxSpawnProtection.CheckedChanged += new System.EventHandler(this.CheckBoxSpawnProtection_CheckedChanged);
            // 
            // checkBox7
            // 
            this.checkBox7.Location = new System.Drawing.Point(228, 81);
            this.checkBox7.Margin = new System.Windows.Forms.Padding(2);
            this.checkBox7.Name = "checkBox7";
            this.checkBox7.Size = new System.Drawing.Size(107, 29);
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
            this.numericUpDown1.Size = new System.Drawing.Size(162, 20);
            this.numericUpDown1.TabIndex = 31;
            this.numericUpDown1.Visible = false;
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.Location = new System.Drawing.Point(118, 69);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(92, 25);
            this.label9.TabIndex = 30;
            this.label9.Text = "Seed";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(118, 94);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(93, 20);
            this.textBox4.TabIndex = 29;
            this.textBox4.Tag = "level-seed";
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Location = new System.Drawing.Point(118, 130);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(92, 25);
            this.label4.TabIndex = 34;
            this.label4.Text = "World Size";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label10
            // 
            this.label10.BackColor = System.Drawing.Color.Transparent;
            this.label10.Location = new System.Drawing.Point(9, 130);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(92, 25);
            this.label10.TabIndex = 32;
            this.label10.Text = "Level Type";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBox7
            // 
            this.textBox7.Location = new System.Drawing.Point(9, 155);
            this.textBox7.Name = "textBox7";
            this.textBox7.Size = new System.Drawing.Size(93, 20);
            this.textBox7.TabIndex = 31;
            this.textBox7.Tag = "level-type";
            // 
            // numericUpDown3
            // 
            this.numericUpDown3.Location = new System.Drawing.Point(118, 155);
            this.numericUpDown3.Margin = new System.Windows.Forms.Padding(2);
            this.numericUpDown3.Maximum = new decimal(new int[] { 29999984, 0, 0, 0 });
            this.numericUpDown3.Name = "numericUpDown3";
            this.numericUpDown3.Size = new System.Drawing.Size(92, 20);
            this.numericUpDown3.TabIndex = 34;
            this.numericUpDown3.Tag = "max-world-size";
            // 
            // ButtonOpenServerFolder
            // 
            this.ButtonOpenServerFolder.Location = new System.Drawing.Point(228, 260);
            this.ButtonOpenServerFolder.Margin = new System.Windows.Forms.Padding(2);
            this.ButtonOpenServerFolder.Name = "ButtonOpenServerFolder";
            this.ButtonOpenServerFolder.Size = new System.Drawing.Size(194, 34);
            this.ButtonOpenServerFolder.TabIndex = 35;
            this.ButtonOpenServerFolder.Text = "Open Server Folder";
            this.ButtonOpenServerFolder.UseVisualStyleBackColor = true;
            this.ButtonOpenServerFolder.Click += new System.EventHandler(this.ButtonOpenServerFolder_Click);
            // 
            // ButtonSave
            // 
            this.ButtonSave.Location = new System.Drawing.Point(228, 299);
            this.ButtonSave.Margin = new System.Windows.Forms.Padding(2);
            this.ButtonSave.Name = "ButtonSave";
            this.ButtonSave.Size = new System.Drawing.Size(194, 34);
            this.ButtonSave.TabIndex = 36;
            this.ButtonSave.Text = "Save";
            this.ButtonSave.UseVisualStyleBackColor = true;
            this.ButtonSave.Click += new System.EventHandler(this.ButtonSave_Click);
            // 
            // NumericSpawnProtection
            // 
            this.NumericSpawnProtection.Location = new System.Drawing.Point(300, 107);
            this.NumericSpawnProtection.Margin = new System.Windows.Forms.Padding(2);
            this.NumericSpawnProtection.Name = "NumericSpawnProtection";
            this.NumericSpawnProtection.Size = new System.Drawing.Size(107, 20);
            this.NumericSpawnProtection.TabIndex = 37;
            this.NumericSpawnProtection.Tag = "spawn-protection";
            this.NumericSpawnProtection.Visible = false;
            // 
            // ButtonDeleteServer
            // 
            this.ButtonDeleteServer.BackColor = System.Drawing.Color.IndianRed;
            this.ButtonDeleteServer.FlatAppearance.BorderSize = 0;
            this.ButtonDeleteServer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ButtonDeleteServer.ForeColor = System.Drawing.Color.White;
            this.ButtonDeleteServer.Location = new System.Drawing.Point(338, 337);
            this.ButtonDeleteServer.Margin = new System.Windows.Forms.Padding(2);
            this.ButtonDeleteServer.Name = "ButtonDeleteServer";
            this.ButtonDeleteServer.Size = new System.Drawing.Size(83, 21);
            this.ButtonDeleteServer.TabIndex = 38;
            this.ButtonDeleteServer.Text = "Delete Server";
            this.ButtonDeleteServer.UseVisualStyleBackColor = false;
            this.ButtonDeleteServer.Click += new System.EventHandler(this.ButtonDeleteServer_Click);
            // 
            // ButtonFolderBrowsing3
            // 
            this.ButtonFolderBrowsing3.Location = new System.Drawing.Point(182, 332);
            this.ButtonFolderBrowsing3.Margin = new System.Windows.Forms.Padding(2);
            this.ButtonFolderBrowsing3.Name = "ButtonFolderBrowsing3";
            this.ButtonFolderBrowsing3.Size = new System.Drawing.Size(28, 28);
            this.ButtonFolderBrowsing3.TabIndex = 41;
            this.ButtonFolderBrowsing3.Tag = "javaruntimepath";
            this.ButtonFolderBrowsing3.UseVisualStyleBackColor = true;
            this.ButtonFolderBrowsing3.Click += new System.EventHandler(this.ButtonFolderBrowsing_Click);
            // 
            // label11
            // 
            this.label11.BackColor = System.Drawing.Color.Transparent;
            this.label11.Location = new System.Drawing.Point(9, 308);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(169, 25);
            this.label11.TabIndex = 40;
            this.label11.Text = "Java Runtime";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(9, 336);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(170, 20);
            this.textBox3.TabIndex = 39;
            this.textBox3.Tag = "javaruntimepath";
            // 
            // ServerEditPrompt
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(436, 369);
            this.Controls.Add(this.ButtonFolderBrowsing3);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.ButtonDeleteServer);
            this.Controls.Add(this.NumericSpawnProtection);
            this.Controls.Add(this.CheckBoxSpawnProtection);
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
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "ServerEditPrompt";
            this.Load += new System.EventHandler(this.ServerEditPrompt_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumericSpawnProtection)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

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
        private System.Windows.Forms.NumericUpDown NumericSpawnProtection;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox textBox4;

        private System.Windows.Forms.CheckBox CheckBoxSpawnProtection;

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
        private System.Windows.Forms.Label label6;
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