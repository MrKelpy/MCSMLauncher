namespace MCSMLauncher.gui
{
    partial class Mainframe
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Mainframe));
            MenuBar = new System.Windows.Forms.MenuStrip();
            NewServerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ServersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            RadminVPNToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            MainLayout = new System.Windows.Forms.Panel();
            MenuBar.SuspendLayout();
            SuspendLayout();
            // 
            // MenuBar
            // 
            MenuBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(93)))), ((int)(((byte)(140)))));
            MenuBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { NewServerToolStripMenuItem, ServersToolStripMenuItem, RadminVPNToolStripMenuItem });
            MenuBar.Location = new System.Drawing.Point(0, 0);
            MenuBar.Name = "MenuBar";
            MenuBar.Size = new System.Drawing.Size(800, 24);
            MenuBar.TabIndex = 0;
            MenuBar.Text = "menuStrip1";
            // 
            // NewServerToolStripMenuItem
            // 
            NewServerToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            NewServerToolStripMenuItem.Name = "NewServerToolStripMenuItem";
            NewServerToolStripMenuItem.Size = new System.Drawing.Size(43, 20);
            NewServerToolStripMenuItem.Text = "New";
            NewServerToolStripMenuItem.Click += new System.EventHandler(NewServerToolStripMenuItem_Click);
            // 
            // ServersToolStripMenuItem
            // 
            ServersToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            ServersToolStripMenuItem.Name = "ServersToolStripMenuItem";
            ServersToolStripMenuItem.Size = new System.Drawing.Size(56, 20);
            ServersToolStripMenuItem.Text = "Servers";
            ServersToolStripMenuItem.Click += new System.EventHandler(ServersToolStripMenuItem_Click);
            // 
            // RadminVPNToolStripMenuItem
            // 
            RadminVPNToolStripMenuItem.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            RadminVPNToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            RadminVPNToolStripMenuItem.Name = "RadminVPNToolStripMenuItem";
            RadminVPNToolStripMenuItem.RightToLeft = System.Windows.Forms.RightToLeft.No;
            RadminVPNToolStripMenuItem.Size = new System.Drawing.Size(86, 20);
            RadminVPNToolStripMenuItem.Text = "Radmin VPN";
            RadminVPNToolStripMenuItem.Click += new System.EventHandler(RadminVPNToolStripMenuItem_Click);
            // 
            // MainLayout
            // 
            MainLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            MainLayout.Location = new System.Drawing.Point(0, 24);
            MainLayout.Name = "MainLayout";
            MainLayout.Size = new System.Drawing.Size(800, 426);
            MainLayout.TabIndex = 1;
            // 
            // Mainframe
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(800, 450);
            Controls.Add(MainLayout);
            Controls.Add(MenuBar);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            Icon = ((System.Drawing.Icon)(resources.GetObject("$Icon")));
            MainMenuStrip = MenuBar;
            Name = "Mainframe";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "MCSM Launcher";
            Load += new System.EventHandler(Mainframe_Load);
            SizeChanged += new System.EventHandler(Mainframe_SizeChanged);
            MenuBar.ResumeLayout(false);
            MenuBar.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        private System.Windows.Forms.ToolStripMenuItem RadminVPNToolStripMenuItem;

        #endregion

        private System.Windows.Forms.MenuStrip MenuBar;
        private System.Windows.Forms.ToolStripMenuItem NewServerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ServersToolStripMenuItem;
        private System.Windows.Forms.Panel MainLayout;
    }
}