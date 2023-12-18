namespace mcsm.ui.graphical
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
            this.MenuBar = new System.Windows.Forms.MenuStrip();
            this.NewServerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ServersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.RadminVPNToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.WikiToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MainLayout = new System.Windows.Forms.Panel();
            this.MenuBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // MenuBar
            // 
            this.MenuBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(93)))), ((int)(((byte)(140)))));
            this.MenuBar.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.MenuBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { this.NewServerToolStripMenuItem, this.ServersToolStripMenuItem, this.RadminVPNToolStripMenuItem, this.WikiToolStripMenuItem });
            this.MenuBar.Location = new System.Drawing.Point(0, 0);
            this.MenuBar.Name = "MenuBar";
            this.MenuBar.Size = new System.Drawing.Size(1200, 33);
            this.MenuBar.TabIndex = 0;
            this.MenuBar.Text = "menuStrip1";
            // 
            // NewServerToolStripMenuItem
            // 
            this.NewServerToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.NewServerToolStripMenuItem.Name = "NewServerToolStripMenuItem";
            this.NewServerToolStripMenuItem.Size = new System.Drawing.Size(59, 29);
            this.NewServerToolStripMenuItem.Text = "New";
            this.NewServerToolStripMenuItem.Click += new System.EventHandler(this.NewServerToolStripMenuItem_Click);
            // 
            // ServersToolStripMenuItem
            // 
            this.ServersToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ServersToolStripMenuItem.Name = "ServersToolStripMenuItem";
            this.ServersToolStripMenuItem.Size = new System.Drawing.Size(81, 29);
            this.ServersToolStripMenuItem.Text = "Servers";
            this.ServersToolStripMenuItem.Click += new System.EventHandler(this.ServersToolStripMenuItem_Click);
            // 
            // RadminVPNToolStripMenuItem
            // 
            this.RadminVPNToolStripMenuItem.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.RadminVPNToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.RadminVPNToolStripMenuItem.Name = "RadminVPNToolStripMenuItem";
            this.RadminVPNToolStripMenuItem.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.RadminVPNToolStripMenuItem.Size = new System.Drawing.Size(124, 29);
            this.RadminVPNToolStripMenuItem.Text = "Radmin VPN";
            this.RadminVPNToolStripMenuItem.Click += new System.EventHandler(this.RadminVPNToolStripMenuItem_Click);
            // 
            // WikiToolStripMenuItem
            // 
            this.WikiToolStripMenuItem.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.WikiToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.WikiToolStripMenuItem.Name = "WikiToolStripMenuItem";
            this.WikiToolStripMenuItem.Size = new System.Drawing.Size(58, 29);
            this.WikiToolStripMenuItem.Text = "Wiki";
            this.WikiToolStripMenuItem.Click += new System.EventHandler(this.WikiToolStripMenuItem_Click);
            // 
            // MainLayout
            // 
            this.MainLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainLayout.Location = new System.Drawing.Point(0, 33);
            this.MainLayout.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MainLayout.Name = "MainLayout";
            this.MainLayout.Size = new System.Drawing.Size(1200, 659);
            this.MainLayout.TabIndex = 1;
            // 
            // Mainframe
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1200, 692);
            this.Controls.Add(this.MainLayout);
            this.Controls.Add(this.MenuBar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.MenuBar;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Mainframe";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MCSM Launcher";
            this.Load += new System.EventHandler(this.Mainframe_Load);
            this.SizeChanged += new System.EventHandler(this.Mainframe_SizeChanged);
            this.MenuBar.ResumeLayout(false);
            this.MenuBar.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.ToolStripMenuItem RadminVPNToolStripMenuItem;

        #endregion

        private System.Windows.Forms.MenuStrip MenuBar;
        private System.Windows.Forms.ToolStripMenuItem NewServerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ServersToolStripMenuItem;
        private System.Windows.Forms.Panel MainLayout;
        private System.Windows.Forms.ToolStripMenuItem WikiToolStripMenuItem;
    }
}