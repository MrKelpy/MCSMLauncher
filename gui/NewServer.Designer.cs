namespace MCSMLauncher.gui
{
    partial class NewServer
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
            this.NewServerLayout = new System.Windows.Forms.Panel();
            this.LabelServerNameError = new System.Windows.Forms.Label();
            this.TextBoxConsoleOutput = new System.Windows.Forms.TextBox();
            this.ButtonBuild = new System.Windows.Forms.Button();
            this.ComboServerVersion = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.ComboBoxServerType = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.TextBoxServerName = new System.Windows.Forms.TextBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.NewServerLayout.SuspendLayout();
            this.SuspendLayout();
            // 
            // NewServerLayout
            // 
            this.NewServerLayout.Controls.Add(this.LabelServerNameError);
            this.NewServerLayout.Controls.Add(this.TextBoxConsoleOutput);
            this.NewServerLayout.Controls.Add(this.ButtonBuild);
            this.NewServerLayout.Controls.Add(this.ComboServerVersion);
            this.NewServerLayout.Controls.Add(this.label3);
            this.NewServerLayout.Controls.Add(this.ComboBoxServerType);
            this.NewServerLayout.Controls.Add(this.label2);
            this.NewServerLayout.Controls.Add(this.label1);
            this.NewServerLayout.Controls.Add(this.TextBoxServerName);
            this.NewServerLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.NewServerLayout.Location = new System.Drawing.Point(0, 24);
            this.NewServerLayout.Name = "NewServerLayout";
            this.NewServerLayout.Size = new System.Drawing.Size(800, 426);
            this.NewServerLayout.TabIndex = 2;
            // 
            // LabelServerNameError
            // 
            this.LabelServerNameError.BackColor = System.Drawing.Color.Transparent;
            this.LabelServerNameError.ForeColor = System.Drawing.Color.Firebrick;
            this.LabelServerNameError.Location = new System.Drawing.Point(96, 87);
            this.LabelServerNameError.Name = "LabelServerNameError";
            this.LabelServerNameError.Size = new System.Drawing.Size(248, 14);
            this.LabelServerNameError.TabIndex = 10;
            this.LabelServerNameError.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // TextBoxConsoleOutput
            // 
            this.TextBoxConsoleOutput.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBoxConsoleOutput.BackColor = System.Drawing.Color.White;
            this.TextBoxConsoleOutput.Location = new System.Drawing.Point(440, 24);
            this.TextBoxConsoleOutput.Multiline = true;
            this.TextBoxConsoleOutput.Name = "TextBoxConsoleOutput";
            this.TextBoxConsoleOutput.ReadOnly = true;
            this.TextBoxConsoleOutput.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.TextBoxConsoleOutput.Size = new System.Drawing.Size(328, 376);
            this.TextBoxConsoleOutput.TabIndex = 8;
            // 
            // ButtonBuild
            // 
            this.ButtonBuild.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ButtonBuild.Enabled = false;
            this.ButtonBuild.Location = new System.Drawing.Point(96, 352);
            this.ButtonBuild.Name = "ButtonBuild";
            this.ButtonBuild.Size = new System.Drawing.Size(248, 48);
            this.ButtonBuild.TabIndex = 7;
            this.ButtonBuild.Text = "Build Server";
            this.ButtonBuild.UseVisualStyleBackColor = true;
            this.ButtonBuild.Click += new System.EventHandler(this.ButtonBuild_Click);
            // 
            // ComboServerVersion
            // 
            this.ComboServerVersion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboServerVersion.Enabled = false;
            this.ComboServerVersion.FormattingEnabled = true;
            this.ComboServerVersion.Location = new System.Drawing.Point(96, 224);
            this.ComboServerVersion.Name = "ComboServerVersion";
            this.ComboServerVersion.Size = new System.Drawing.Size(248, 21);
            this.ComboServerVersion.TabIndex = 6;
            this.ComboServerVersion.SelectedIndexChanged += new System.EventHandler(this.ComboServerVersion_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Location = new System.Drawing.Point(96, 192);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(248, 32);
            this.label3.TabIndex = 5;
            this.label3.Text = "Server Version";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ComboBoxServerType
            // 
            this.ComboBoxServerType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBoxServerType.FormattingEnabled = true;
            this.ComboBoxServerType.Items.AddRange(new object[] { "Vanilla", "Vanilla Snapshots", "Spigot", "Forge" });
            this.ComboBoxServerType.Location = new System.Drawing.Point(96, 144);
            this.ComboBoxServerType.Name = "ComboBoxServerType";
            this.ComboBoxServerType.Size = new System.Drawing.Size(248, 21);
            this.ComboBoxServerType.TabIndex = 4;
            this.ComboBoxServerType.SelectedIndexChanged += new System.EventHandler(this.ComboBoxServerType_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(96, 112);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(248, 32);
            this.label2.TabIndex = 3;
            this.label2.Text = "Server Type";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(96, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(248, 32);
            this.label1.TabIndex = 1;
            this.label1.Text = "Server Name";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TextBoxServerName
            // 
            this.TextBoxServerName.Location = new System.Drawing.Point(96, 64);
            this.TextBoxServerName.Name = "TextBoxServerName";
            this.TextBoxServerName.Size = new System.Drawing.Size(248, 20);
            this.TextBoxServerName.TabIndex = 0;
            this.TextBoxServerName.TextChanged += new System.EventHandler(this.TextBoxServerName_TextChanged);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 24);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // NewServer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.NewServerLayout);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "NewServer";
            this.Text = "Form1";
            this.NewServerLayout.ResumeLayout(false);
            this.NewServerLayout.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Label LabelServerNameError;

        #endregion

        private System.Windows.Forms.Panel NewServerLayout;
        private System.Windows.Forms.TextBox TextBoxServerName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox ComboBoxServerType;
        private System.Windows.Forms.ComboBox ComboServerVersion;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button ButtonBuild;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.TextBox TextBoxConsoleOutput;
    }
}