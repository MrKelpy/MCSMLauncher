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
            this.MenuBar = new System.Windows.Forms.MenuStrip();
            this.NewServerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ServersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.radminVPNToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MainLayout = new System.Windows.Forms.Panel();
            this.MenuBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // MenuBar
            // 
            this.MenuBar.BackColor = System.Drawing.Color.Gainsboro;
            this.MenuBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { this.NewServerToolStripMenuItem, this.ServersToolStripMenuItem, this.radminVPNToolStripMenuItem });
            this.MenuBar.Location = new System.Drawing.Point(0, 0);
            this.MenuBar.Name = "MenuBar";
            this.MenuBar.Size = new System.Drawing.Size(800, 24);
            this.MenuBar.TabIndex = 0;
            this.MenuBar.Text = "menuStrip1";
            // 
            // NewServerToolStripMenuItem
            // 
            this.NewServerToolStripMenuItem.Name = "NewServerToolStripMenuItem";
            this.NewServerToolStripMenuItem.Size = new System.Drawing.Size(43, 20);
            this.NewServerToolStripMenuItem.Text = "New";
            this.NewServerToolStripMenuItem.Click += new System.EventHandler(this.NewServerToolStripMenuItem_Click);
            // 
            // ServersToolStripMenuItem
            // 
            this.ServersToolStripMenuItem.Name = "ServersToolStripMenuItem";
            this.ServersToolStripMenuItem.Size = new System.Drawing.Size(56, 20);
            this.ServersToolStripMenuItem.Text = "Servers";
            this.ServersToolStripMenuItem.Click += new System.EventHandler(this.ServersToolStripMenuItem_Click);
            // 
            // radminVPNToolStripMenuItem
            // 
            this.radminVPNToolStripMenuItem.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.radminVPNToolStripMenuItem.Name = "radminVPNToolStripMenuItem";
            this.radminVPNToolStripMenuItem.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.radminVPNToolStripMenuItem.Size = new System.Drawing.Size(86, 20);
            this.radminVPNToolStripMenuItem.Text = "Radmin VPN";
            // 
            // MainLayout
            // 
            this.MainLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainLayout.Location = new System.Drawing.Point(0, 24);
            this.MainLayout.Name = "MainLayout";
            this.MainLayout.Size = new System.Drawing.Size(800, 426);
            this.MainLayout.TabIndex = 1;
            // 
            // Mainframe
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.MainLayout);
            this.Controls.Add(this.MenuBar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.MenuBar;
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

        private System.Windows.Forms.ToolStripMenuItem radminVPNToolStripMenuItem;

        #endregion

        private System.Windows.Forms.MenuStrip MenuBar;
        private System.Windows.Forms.ToolStripMenuItem NewServerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ServersToolStripMenuItem;
        private System.Windows.Forms.Panel MainLayout;
    }
}