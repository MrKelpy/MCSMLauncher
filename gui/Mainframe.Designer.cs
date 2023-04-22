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
            this.MainLayout = new System.Windows.Forms.Panel();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.creatingAServerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editingAServerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.startingAServerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.radminVPNToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // MenuBar
            // 
            this.MenuBar.BackColor = System.Drawing.Color.Gainsboro;
            this.MenuBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { this.NewServerToolStripMenuItem, this.ServersToolStripMenuItem, this.helpToolStripMenuItem });
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
            // MainLayout
            // 
            this.MainLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainLayout.Location = new System.Drawing.Point(0, 24);
            this.MainLayout.Name = "MainLayout";
            this.MainLayout.Size = new System.Drawing.Size(800, 426);
            this.MainLayout.TabIndex = 1;
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { this.creatingAServerToolStripMenuItem, this.editingAServerToolStripMenuItem, this.startingAServerToolStripMenuItem, this.radminVPNToolStripMenuItem });
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // creatingAServerToolStripMenuItem
            // 
            this.creatingAServerToolStripMenuItem.Name = "creatingAServerToolStripMenuItem";
            this.creatingAServerToolStripMenuItem.Size = new System.Drawing.Size(163, 22);
            this.creatingAServerToolStripMenuItem.Text = "Creating a Server";
            // 
            // editingAServerToolStripMenuItem
            // 
            this.editingAServerToolStripMenuItem.Name = "editingAServerToolStripMenuItem";
            this.editingAServerToolStripMenuItem.Size = new System.Drawing.Size(163, 22);
            this.editingAServerToolStripMenuItem.Text = "Editing a Server";
            // 
            // startingAServerToolStripMenuItem
            // 
            this.startingAServerToolStripMenuItem.Name = "startingAServerToolStripMenuItem";
            this.startingAServerToolStripMenuItem.Size = new System.Drawing.Size(163, 22);
            this.startingAServerToolStripMenuItem.Text = "Starting a Server";
            // 
            // radminVPNToolStripMenuItem
            // 
            this.radminVPNToolStripMenuItem.Name = "radminVPNToolStripMenuItem";
            this.radminVPNToolStripMenuItem.Size = new System.Drawing.Size(163, 22);
            this.radminVPNToolStripMenuItem.Text = "Radmin VPN";
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
            this.SizeChanged += new System.EventHandler(this.Mainframe_SizeChanged);
            this.MenuBar.ResumeLayout(false);
            this.MenuBar.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem creatingAServerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editingAServerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem startingAServerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem radminVPNToolStripMenuItem;

        #endregion

        private System.Windows.Forms.MenuStrip MenuBar;
        private System.Windows.Forms.ToolStripMenuItem NewServerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ServersToolStripMenuItem;
        private System.Windows.Forms.Panel MainLayout;
    }
}