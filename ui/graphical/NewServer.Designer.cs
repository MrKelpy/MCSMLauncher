namespace MCSMLauncher.ui.graphical
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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NewServer));
            NewServerLayout = new System.Windows.Forms.Panel();
            label8 = new System.Windows.Forms.Label();
            label7 = new System.Windows.Forms.Label();
            label6 = new System.Windows.Forms.Label();
            label5 = new System.Windows.Forms.Label();
            RichTextBoxConsoleOutput = new System.Windows.Forms.RichTextBox();
            ButtonFolderBrowser = new System.Windows.Forms.Button();
            ComboBoxJavaVersion = new System.Windows.Forms.ComboBox();
            label4 = new System.Windows.Forms.Label();
            PictureBoxLoading = new System.Windows.Forms.PictureBox();
            LabelServerNameError = new System.Windows.Forms.Label();
            ButtonBuild = new System.Windows.Forms.Button();
            ComboServerVersion = new System.Windows.Forms.ComboBox();
            label3 = new System.Windows.Forms.Label();
            ComboBoxServerType = new System.Windows.Forms.ComboBox();
            label2 = new System.Windows.Forms.Label();
            label1 = new System.Windows.Forms.Label();
            TextBoxServerName = new System.Windows.Forms.TextBox();
            miniToolStrip = new System.Windows.Forms.MenuStrip();
            FolderBrowser = new System.Windows.Forms.FolderBrowserDialog();
            ToolTips = new System.Windows.Forms.ToolTip(components);
            NewServerLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(PictureBoxLoading)).BeginInit();
            SuspendLayout();
            // 
            // NewServerLayout
            // 
            NewServerLayout.BackColor = System.Drawing.SystemColors.Control;
            NewServerLayout.Controls.Add(label8);
            NewServerLayout.Controls.Add(label7);
            NewServerLayout.Controls.Add(label6);
            NewServerLayout.Controls.Add(label5);
            NewServerLayout.Controls.Add(RichTextBoxConsoleOutput);
            NewServerLayout.Controls.Add(ButtonFolderBrowser);
            NewServerLayout.Controls.Add(ComboBoxJavaVersion);
            NewServerLayout.Controls.Add(label4);
            NewServerLayout.Controls.Add(PictureBoxLoading);
            NewServerLayout.Controls.Add(LabelServerNameError);
            NewServerLayout.Controls.Add(ButtonBuild);
            NewServerLayout.Controls.Add(ComboServerVersion);
            NewServerLayout.Controls.Add(label3);
            NewServerLayout.Controls.Add(ComboBoxServerType);
            NewServerLayout.Controls.Add(label2);
            NewServerLayout.Controls.Add(label1);
            NewServerLayout.Controls.Add(TextBoxServerName);
            NewServerLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            NewServerLayout.Location = new System.Drawing.Point(0, 0);
            NewServerLayout.Name = "NewServerLayout";
            NewServerLayout.Size = new System.Drawing.Size(800, 450);
            NewServerLayout.TabIndex = 2;
            // 
            // label8
            // 
            label8.Location = new System.Drawing.Point(350, 67);
            label8.Name = "label8";
            label8.Size = new System.Drawing.Size(14, 14);
            label8.TabIndex = 19;
            label8.Tag = "tooltip";
            ToolTips.SetToolTip(label8, "This is the server\'s name, which is used to identify each server internally. \r\nTh" + "is name will also be the one displayed in the listings when you try to run it.");
            // 
            // label7
            // 
            label7.Location = new System.Drawing.Point(350, 147);
            label7.Name = "label7";
            label7.Size = new System.Drawing.Size(14, 14);
            label7.TabIndex = 18;
            label7.Tag = "tooltip";
            ToolTips.SetToolTip(label7, resources.GetString("label7.ToolTip"));
            // 
            // label6
            // 
            label6.Location = new System.Drawing.Point(350, 227);
            label6.Name = "label6";
            label6.Size = new System.Drawing.Size(14, 14);
            label6.TabIndex = 17;
            label6.Tag = "tooltip";
            ToolTips.SetToolTip(label6, "This is the server\'s version, according to the selected server type.\r\nEach type h" + "as slightly different version ranges to be selected.");
            // 
            // label5
            // 
            label5.Location = new System.Drawing.Point(350, 304);
            label5.Name = "label5";
            label5.Size = new System.Drawing.Size(14, 14);
            label5.TabIndex = 16;
            label5.Tag = "tooltip";
            ToolTips.SetToolTip(label5, resources.GetString("label5.ToolTip"));
            // 
            // RichTextBoxConsoleOutput
            // 
            RichTextBoxConsoleOutput.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            RichTextBoxConsoleOutput.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            RichTextBoxConsoleOutput.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            RichTextBoxConsoleOutput.HideSelection = false;
            RichTextBoxConsoleOutput.Location = new System.Drawing.Point(440, 24);
            RichTextBoxConsoleOutput.Name = "RichTextBoxConsoleOutput";
            RichTextBoxConsoleOutput.ReadOnly = true;
            RichTextBoxConsoleOutput.Size = new System.Drawing.Size(328, 377);
            RichTextBoxConsoleOutput.TabIndex = 15;
            RichTextBoxConsoleOutput.Text = "";
            // 
            // ButtonFolderBrowser
            // 
            ButtonFolderBrowser.Location = new System.Drawing.Point(96, 296);
            ButtonFolderBrowser.Margin = new System.Windows.Forms.Padding(2);
            ButtonFolderBrowser.Name = "ButtonFolderBrowser";
            ButtonFolderBrowser.Size = new System.Drawing.Size(28, 28);
            ButtonFolderBrowser.TabIndex = 14;
            ButtonFolderBrowser.UseVisualStyleBackColor = true;
            ButtonFolderBrowser.Click += new System.EventHandler(FolderBrowserButton_Click);
            // 
            // ComboBoxJavaVersion
            // 
            ComboBoxJavaVersion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            ComboBoxJavaVersion.FormattingEnabled = true;
            ComboBoxJavaVersion.Location = new System.Drawing.Point(129, 301);
            ComboBoxJavaVersion.Name = "ComboBoxJavaVersion";
            ComboBoxJavaVersion.Size = new System.Drawing.Size(215, 21);
            ComboBoxJavaVersion.TabIndex = 13;
            // 
            // label4
            // 
            label4.BackColor = System.Drawing.Color.Transparent;
            label4.Location = new System.Drawing.Point(96, 268);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(248, 32);
            label4.TabIndex = 12;
            label4.Text = "Java Runtime Version";
            label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // PictureBoxLoading
            // 
            PictureBoxLoading.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            PictureBoxLoading.BackColor = System.Drawing.Color.Transparent;
            PictureBoxLoading.Location = new System.Drawing.Point(189, 354);
            PictureBoxLoading.Name = "PictureBoxLoading";
            PictureBoxLoading.Size = new System.Drawing.Size(66, 62);
            PictureBoxLoading.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            PictureBoxLoading.TabIndex = 11;
            PictureBoxLoading.TabStop = false;
            PictureBoxLoading.Visible = false;
            // 
            // LabelServerNameError
            // 
            LabelServerNameError.BackColor = System.Drawing.Color.Transparent;
            LabelServerNameError.ForeColor = System.Drawing.Color.Firebrick;
            LabelServerNameError.Location = new System.Drawing.Point(96, 87);
            LabelServerNameError.Name = "LabelServerNameError";
            LabelServerNameError.Size = new System.Drawing.Size(248, 14);
            LabelServerNameError.TabIndex = 10;
            LabelServerNameError.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ButtonBuild
            // 
            ButtonBuild.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            ButtonBuild.Enabled = false;
            ButtonBuild.Location = new System.Drawing.Point(96, 353);
            ButtonBuild.Name = "ButtonBuild";
            ButtonBuild.Size = new System.Drawing.Size(248, 48);
            ButtonBuild.TabIndex = 7;
            ButtonBuild.Text = "Build Server";
            ButtonBuild.UseVisualStyleBackColor = true;
            ButtonBuild.Click += new System.EventHandler(ButtonBuild_Click);
            // 
            // ComboServerVersion
            // 
            ComboServerVersion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            ComboServerVersion.Enabled = false;
            ComboServerVersion.FormattingEnabled = true;
            ComboServerVersion.Location = new System.Drawing.Point(96, 224);
            ComboServerVersion.Name = "ComboServerVersion";
            ComboServerVersion.Size = new System.Drawing.Size(248, 21);
            ComboServerVersion.TabIndex = 6;
            ComboServerVersion.SelectedIndexChanged += new System.EventHandler(ComboServerVersion_SelectedIndexChanged);
            // 
            // label3
            // 
            label3.BackColor = System.Drawing.Color.Transparent;
            label3.Location = new System.Drawing.Point(96, 192);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(248, 32);
            label3.TabIndex = 5;
            label3.Text = "Server Version";
            label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ComboBoxServerType
            // 
            ComboBoxServerType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            ComboBoxServerType.FormattingEnabled = true;
            ComboBoxServerType.Location = new System.Drawing.Point(96, 144);
            ComboBoxServerType.Name = "ComboBoxServerType";
            ComboBoxServerType.Size = new System.Drawing.Size(248, 21);
            ComboBoxServerType.TabIndex = 4;
            ComboBoxServerType.SelectedIndexChanged += new System.EventHandler(ComboBoxServerType_SelectedIndexChanged);
            // 
            // label2
            // 
            label2.BackColor = System.Drawing.Color.Transparent;
            label2.Location = new System.Drawing.Point(96, 112);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(248, 32);
            label2.TabIndex = 3;
            label2.Text = "Server Type";
            label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            label1.BackColor = System.Drawing.Color.Transparent;
            label1.Location = new System.Drawing.Point(96, 32);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(248, 32);
            label1.TabIndex = 1;
            label1.Text = "Server Name";
            label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TextBoxServerName
            // 
            TextBoxServerName.Location = new System.Drawing.Point(96, 64);
            TextBoxServerName.Name = "TextBoxServerName";
            TextBoxServerName.Size = new System.Drawing.Size(248, 20);
            TextBoxServerName.TabIndex = 0;
            TextBoxServerName.TextChanged += new System.EventHandler(TextBoxServerName_TextChanged);
            // 
            // miniToolStrip
            // 
            miniToolStrip.AutoSize = false;
            miniToolStrip.Dock = System.Windows.Forms.DockStyle.None;
            miniToolStrip.Location = new System.Drawing.Point(0, 0);
            miniToolStrip.Name = "miniToolStrip";
            miniToolStrip.Size = new System.Drawing.Size(800, 24);
            miniToolStrip.TabIndex = 3;
            miniToolStrip.Visible = false;
            // 
            // NewServer
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.SystemColors.Control;
            ClientSize = new System.Drawing.Size(800, 450);
            Controls.Add(NewServerLayout);
            Controls.Add(miniToolStrip);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            Location = new System.Drawing.Point(15, 15);
            Name = "NewServer";
            NewServerLayout.ResumeLayout(false);
            NewServerLayout.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(PictureBoxLoading)).EndInit();
            ResumeLayout(false);
        }

        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;

        private System.Windows.Forms.ToolTip ToolTips;

        private System.Windows.Forms.Label label5;

        public System.Windows.Forms.RichTextBox RichTextBoxConsoleOutput;

        private System.Windows.Forms.FolderBrowserDialog FolderBrowser;
        private System.Windows.Forms.Button ButtonFolderBrowser;

        private System.Windows.Forms.Label label4;
        public System.Windows.Forms.ComboBox ComboBoxJavaVersion;

        private System.Windows.Forms.PictureBox PictureBoxLoading;

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
        private System.Windows.Forms.MenuStrip miniToolStrip;
    }
}