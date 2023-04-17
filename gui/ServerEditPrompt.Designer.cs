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
            this.FolderBrowserButton = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
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
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumericSpawnProtection)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(13, 20);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(138, 38);
            this.label1.TabIndex = 3;
            this.label1.Text = "Server Name";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TextBoxServerName
            // 
            this.TextBoxServerName.Location = new System.Drawing.Point(13, 58);
            this.TextBoxServerName.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.TextBoxServerName.Name = "TextBoxServerName";
            this.TextBoxServerName.Size = new System.Drawing.Size(138, 26);
            this.TextBoxServerName.TabIndex = 2;
            this.TextBoxServerName.Tag = "level-name";
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
            this.label3.Location = new System.Drawing.Point(13, 106);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(138, 38);
            this.label3.TabIndex = 7;
            this.label3.Text = "Server Base Port";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(13, 144);
            this.textBox2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(138, 26);
            this.textBox2.TabIndex = 6;
            this.textBox2.Tag = "server-port";
            // 
            // textBox5
            // 
            this.textBox5.Location = new System.Drawing.Point(13, 337);
            this.textBox5.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new System.Drawing.Size(253, 26);
            this.textBox5.TabIndex = 10;
            this.textBox5.Tag = "server_backups_path";
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Location = new System.Drawing.Point(13, 294);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(253, 38);
            this.label6.TabIndex = 11;
            this.label6.Text = "Server Backups Path";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Location = new System.Drawing.Point(13, 385);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(253, 38);
            this.label7.TabIndex = 13;
            this.label7.Text = "Playerdata Backups Path";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBox6
            // 
            this.textBox6.Location = new System.Drawing.Point(13, 428);
            this.textBox6.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new System.Drawing.Size(253, 26);
            this.textBox6.TabIndex = 12;
            this.textBox6.Tag = "playerdata_backups_path";
            // 
            // FolderBrowserButton
            // 
            this.FolderBrowserButton.Location = new System.Drawing.Point(273, 329);
            this.FolderBrowserButton.Name = "FolderBrowserButton";
            this.FolderBrowserButton.Size = new System.Drawing.Size(42, 42);
            this.FolderBrowserButton.TabIndex = 15;
            this.FolderBrowserButton.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(273, 420);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(42, 42);
            this.button1.TabIndex = 16;
            this.button1.UseVisualStyleBackColor = true;
            // 
            // checkBox1
            // 
            this.checkBox1.Location = new System.Drawing.Point(343, 50);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(105, 44);
            this.checkBox1.TabIndex = 17;
            this.checkBox1.Tag = "online-mode";
            this.checkBox1.Text = "Cracked";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // checkBox2
            // 
            this.checkBox2.Location = new System.Drawing.Point(343, 85);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(105, 44);
            this.checkBox2.TabIndex = 18;
            this.checkBox2.Tag = "pvp";
            this.checkBox2.Text = "PVP";
            this.checkBox2.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Location = new System.Drawing.Point(343, 200);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(127, 38);
            this.label5.TabIndex = 21;
            this.label5.Text = "Gamemode";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Location = new System.Drawing.Point(507, 200);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(127, 38);
            this.label8.TabIndex = 23;
            this.label8.Text = "Difficulty";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ComboBoxGamemode
            // 
            this.ComboBoxGamemode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBoxGamemode.FormattingEnabled = true;
            this.ComboBoxGamemode.Items.AddRange(new object[] { "Creative", "Survival" });
            this.ComboBoxGamemode.Location = new System.Drawing.Point(343, 243);
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
            this.comboBox1.Location = new System.Drawing.Point(507, 243);
            this.comboBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(127, 28);
            this.comboBox1.TabIndex = 25;
            this.comboBox1.Tag = "difficulty";
            // 
            // checkBox4
            // 
            this.checkBox4.Location = new System.Drawing.Point(452, 50);
            this.checkBox4.Name = "checkBox4";
            this.checkBox4.Size = new System.Drawing.Size(162, 44);
            this.checkBox4.TabIndex = 26;
            this.checkBox4.Tag = "enable-command-block";
            this.checkBox4.Text = "Command Blocks";
            this.checkBox4.UseVisualStyleBackColor = true;
            // 
            // checkBox5
            // 
            this.checkBox5.Location = new System.Drawing.Point(452, 85);
            this.checkBox5.Name = "checkBox5";
            this.checkBox5.Size = new System.Drawing.Size(161, 44);
            this.checkBox5.TabIndex = 27;
            this.checkBox5.Tag = "allow-flight";
            this.checkBox5.Text = "Allow Flight";
            this.checkBox5.UseVisualStyleBackColor = true;
            // 
            // CheckBoxSpawnProtection
            // 
            this.CheckBoxSpawnProtection.Location = new System.Drawing.Point(452, 121);
            this.CheckBoxSpawnProtection.Name = "CheckBoxSpawnProtection";
            this.CheckBoxSpawnProtection.Size = new System.Drawing.Size(161, 44);
            this.CheckBoxSpawnProtection.TabIndex = 32;
            this.CheckBoxSpawnProtection.Text = "Spawn Protection";
            this.CheckBoxSpawnProtection.UseVisualStyleBackColor = true;
            this.CheckBoxSpawnProtection.CheckedChanged += new System.EventHandler(this.CheckBoxSpawnProtection_CheckedChanged);
            // 
            // checkBox7
            // 
            this.checkBox7.Location = new System.Drawing.Point(343, 121);
            this.checkBox7.Name = "checkBox7";
            this.checkBox7.Size = new System.Drawing.Size(161, 44);
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
            this.textBox4.Location = new System.Drawing.Point(177, 144);
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
            this.label10.Location = new System.Drawing.Point(13, 200);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(138, 38);
            this.label10.TabIndex = 32;
            this.label10.Text = "Level Type";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBox7
            // 
            this.textBox7.Location = new System.Drawing.Point(13, 238);
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
            this.ButtonOpenServerFolder.Location = new System.Drawing.Point(343, 329);
            this.ButtonOpenServerFolder.Name = "ButtonOpenServerFolder";
            this.ButtonOpenServerFolder.Size = new System.Drawing.Size(291, 52);
            this.ButtonOpenServerFolder.TabIndex = 35;
            this.ButtonOpenServerFolder.Text = "Open Server Folder";
            this.ButtonOpenServerFolder.UseVisualStyleBackColor = true;
            this.ButtonOpenServerFolder.Click += new System.EventHandler(this.ButtonOpenServerFolder_Click);
            // 
            // ButtonSave
            // 
            this.ButtonSave.Location = new System.Drawing.Point(343, 389);
            this.ButtonSave.Name = "ButtonSave";
            this.ButtonSave.Size = new System.Drawing.Size(291, 52);
            this.ButtonSave.TabIndex = 36;
            this.ButtonSave.Text = "Save";
            this.ButtonSave.UseVisualStyleBackColor = true;
            this.ButtonSave.Click += new System.EventHandler(this.ButtonSave_Click);
            // 
            // NumericSpawnProtection
            // 
            this.NumericSpawnProtection.Location = new System.Drawing.Point(452, 161);
            this.NumericSpawnProtection.Name = "NumericSpawnProtection";
            this.NumericSpawnProtection.Size = new System.Drawing.Size(161, 26);
            this.NumericSpawnProtection.TabIndex = 37;
            this.NumericSpawnProtection.Tag = "spawn-protection";
            // 
            // ButtonDeleteServer
            // 
            this.ButtonDeleteServer.BackColor = System.Drawing.Color.IndianRed;
            this.ButtonDeleteServer.FlatAppearance.BorderSize = 0;
            this.ButtonDeleteServer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ButtonDeleteServer.ForeColor = System.Drawing.Color.White;
            this.ButtonDeleteServer.Location = new System.Drawing.Point(509, 447);
            this.ButtonDeleteServer.Name = "ButtonDeleteServer";
            this.ButtonDeleteServer.Size = new System.Drawing.Size(125, 33);
            this.ButtonDeleteServer.TabIndex = 38;
            this.ButtonDeleteServer.Text = "Delete Server";
            this.ButtonDeleteServer.UseVisualStyleBackColor = false;
            this.ButtonDeleteServer.Click += new System.EventHandler(this.ButtonDeleteServer_Click);
            // 
            // ServerEditPrompt
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(654, 488);
            this.Controls.Add(this.ButtonDeleteServer);
            this.Controls.Add(this.NumericSpawnProtection);
            this.Controls.Add(this.CheckBoxSpawnProtection);
            this.Controls.Add(this.checkBox4);
            this.Controls.Add(this.ButtonSave);
            this.Controls.Add(this.checkBox7);
            this.Controls.Add(this.checkBox2);
            this.Controls.Add(this.ButtonOpenServerFolder);
            this.Controls.Add(this.checkBox1);
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
            this.Controls.Add(this.button1);
            this.Controls.Add(this.FolderBrowserButton);
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
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumericSpawnProtection)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

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

        private System.Windows.Forms.CheckBox checkBox1;

        private System.Windows.Forms.Button FolderBrowserButton;
        private System.Windows.Forms.Button button1;

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