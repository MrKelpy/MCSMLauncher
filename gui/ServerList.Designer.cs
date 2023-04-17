namespace MCSMLauncher.gui
{
    partial class ServerList
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.ServerListLayout = new System.Windows.Forms.Panel();
            this.GridServerList = new System.Windows.Forms.DataGridView();
            this.ServerType = new System.Windows.Forms.DataGridViewImageColumn();
            this.Version = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ServerName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Edit = new System.Windows.Forms.DataGridViewButtonColumn();
            this.Play = new System.Windows.Forms.DataGridViewButtonColumn();
            this.ServerListLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GridServerList)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(9, 3, 0, 3);
            this.menuStrip1.Size = new System.Drawing.Size(1200, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // ServerListLayout
            // 
            this.ServerListLayout.Controls.Add(this.GridServerList);
            this.ServerListLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ServerListLayout.Location = new System.Drawing.Point(0, 24);
            this.ServerListLayout.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ServerListLayout.Name = "ServerListLayout";
            this.ServerListLayout.Size = new System.Drawing.Size(1200, 668);
            this.ServerListLayout.TabIndex = 1;
            // 
            // GridServerList
            // 
            this.GridServerList.AllowUserToAddRows = false;
            this.GridServerList.AllowUserToDeleteRows = false;
            this.GridServerList.AllowUserToOrderColumns = true;
            this.GridServerList.AllowUserToResizeColumns = false;
            this.GridServerList.AllowUserToResizeRows = false;
            this.GridServerList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.GridServerList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.GridServerList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GridServerList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { this.ServerType, this.Version, this.ServerName, this.Status, this.Edit, this.Play });
            this.GridServerList.Location = new System.Drawing.Point(18, 23);
            this.GridServerList.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.GridServerList.MultiSelect = false;
            this.GridServerList.Name = "GridServerList";
            this.GridServerList.ReadOnly = true;
            this.GridServerList.RowHeadersVisible = false;
            this.GridServerList.ShowCellToolTips = false;
            this.GridServerList.ShowEditingIcon = false;
            this.GridServerList.Size = new System.Drawing.Size(1164, 627);
            this.GridServerList.TabIndex = 6;
            this.GridServerList.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.GridServerList_CellContentClick);
            this.GridServerList.SelectionChanged += new System.EventHandler(this.GridServerList_SelectionChanged);
            // 
            // ServerType
            // 
            this.ServerType.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.ServerType.HeaderText = "Server Type";
            this.ServerType.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom;
            this.ServerType.Name = "ServerType";
            this.ServerType.ReadOnly = true;
            this.ServerType.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.ServerType.Width = 99;
            // 
            // Version
            // 
            this.Version.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Version.HeaderText = "Version";
            this.Version.Name = "Version";
            this.Version.ReadOnly = true;
            this.Version.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Version.Width = 69;
            // 
            // ServerName
            // 
            this.ServerName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ServerName.HeaderText = "Server Name";
            this.ServerName.Name = "ServerName";
            this.ServerName.ReadOnly = true;
            this.ServerName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Status
            // 
            this.Status.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Status.HeaderText = "Status";
            this.Status.Name = "Status";
            this.Status.ReadOnly = true;
            this.Status.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Status.ToolTipText = "Offline";
            this.Status.Width = 62;
            // 
            // Edit
            // 
            this.Edit.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.Edit.HeaderText = "                     ";
            this.Edit.Name = "Edit";
            this.Edit.ReadOnly = true;
            this.Edit.Text = "Options";
            this.Edit.UseColumnTextForButtonValue = true;
            this.Edit.Width = 99;
            // 
            // Play
            // 
            this.Play.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.Play.HeaderText = "                     ";
            this.Play.Name = "Play";
            this.Play.ReadOnly = true;
            this.Play.Text = "Start";
            this.Play.UseColumnTextForButtonValue = true;
            this.Play.Width = 99;
            // 
            // ServerList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1200, 692);
            this.Controls.Add(this.ServerListLayout);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "ServerList";
            this.Text = "Form1";
            this.ServerListLayout.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.GridServerList)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.DataGridViewTextBoxColumn Status;

        private System.Windows.Forms.DataGridViewImageColumn ServerType;
        private System.Windows.Forms.DataGridViewTextBoxColumn Version;
        private System.Windows.Forms.DataGridViewTextBoxColumn ServerName;
        private System.Windows.Forms.DataGridViewButtonColumn Edit;
        private System.Windows.Forms.DataGridViewButtonColumn Play;

        public System.Windows.Forms.DataGridView GridServerList;

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.Panel ServerListLayout;
    }
}