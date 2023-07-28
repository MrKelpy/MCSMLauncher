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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ServerList));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            ServerListLayout = new System.Windows.Forms.Panel();
            jkghvcgjv = new System.Windows.Forms.Label();
            ButtonRefresh = new System.Windows.Forms.Button();
            dnjlfe = new System.Windows.Forms.Label();
            GridServerList = new System.Windows.Forms.DataGridView();
            ServerType = new System.Windows.Forms.DataGridViewImageColumn();
            Version = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ServerName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ServerIp = new System.Windows.Forms.DataGridViewTextBoxColumn();
            Edit = new System.Windows.Forms.DataGridViewButtonColumn();
            Play = new System.Windows.Forms.DataGridViewButtonColumn();
            ToolTips = new System.Windows.Forms.ToolTip(components);
            ServerListLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(GridServerList)).BeginInit();
            SuspendLayout();
            // 
            // ServerListLayout
            // 
            ServerListLayout.Controls.Add(jkghvcgjv);
            ServerListLayout.Controls.Add(ButtonRefresh);
            ServerListLayout.Controls.Add(dnjlfe);
            ServerListLayout.Controls.Add(GridServerList);
            ServerListLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            ServerListLayout.Location = new System.Drawing.Point(0, 0);
            ServerListLayout.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            ServerListLayout.Name = "ServerListLayout";
            ServerListLayout.Size = new System.Drawing.Size(1200, 692);
            ServerListLayout.TabIndex = 1;
            // 
            // jkghvcgjv
            // 
            jkghvcgjv.Anchor = System.Windows.Forms.AnchorStyles.Top;
            jkghvcgjv.Location = new System.Drawing.Point(48, 6);
            jkghvcgjv.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            jkghvcgjv.Name = "jkghvcgjv";
            jkghvcgjv.Size = new System.Drawing.Size(1090, 62);
            jkghvcgjv.TabIndex = 19;
            jkghvcgjv.Text = resources.GetString("jkghvcgjv.Text");
            jkghvcgjv.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ButtonRefresh
            // 
            ButtonRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            ButtonRefresh.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            ButtonRefresh.Location = new System.Drawing.Point(1146, 6);
            ButtonRefresh.Name = "ButtonRefresh";
            ButtonRefresh.Size = new System.Drawing.Size(38, 38);
            ButtonRefresh.TabIndex = 18;
            ButtonRefresh.UseVisualStyleBackColor = true;
            ButtonRefresh.Click += new System.EventHandler(ButtonRefresh_Click);
            // 
            // dnjlfe
            // 
            dnjlfe.Location = new System.Drawing.Point(18, 15);
            dnjlfe.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            dnjlfe.Name = "dnjlfe";
            dnjlfe.Size = new System.Drawing.Size(21, 22);
            dnjlfe.TabIndex = 17;
            dnjlfe.Tag = "tooltip";
            ToolTips.SetToolTip(dnjlfe, resources.GetString("dnjlfe.ToolTip"));
            // 
            // GridServerList
            // 
            GridServerList.AllowUserToAddRows = false;
            GridServerList.AllowUserToDeleteRows = false;
            GridServerList.AllowUserToResizeColumns = false;
            GridServerList.AllowUserToResizeRows = false;
            GridServerList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            GridServerList.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            GridServerList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            GridServerList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            GridServerList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { ServerType, Version, ServerName, ServerIp, Edit, Play });
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            GridServerList.DefaultCellStyle = dataGridViewCellStyle2;
            GridServerList.Location = new System.Drawing.Point(18, 73);
            GridServerList.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            GridServerList.MultiSelect = false;
            GridServerList.Name = "GridServerList";
            GridServerList.ReadOnly = true;
            GridServerList.RowHeadersVisible = false;
            GridServerList.ShowCellToolTips = false;
            GridServerList.ShowEditingIcon = false;
            GridServerList.Size = new System.Drawing.Size(1164, 565);
            GridServerList.TabIndex = 6;
            GridServerList.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(GridServerList_CellContentClick);
            GridServerList.SelectionChanged += new System.EventHandler(GridServerList_SelectionChanged);
            // 
            // ServerType
            // 
            ServerType.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            ServerType.HeaderText = "Server Type";
            ServerType.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom;
            ServerType.Name = "ServerType";
            ServerType.ReadOnly = true;
            ServerType.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            ServerType.Width = 99;
            // 
            // Version
            // 
            Version.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            Version.HeaderText = "Version";
            Version.Name = "Version";
            Version.ReadOnly = true;
            Version.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            Version.Width = 69;
            // 
            // ServerName
            // 
            ServerName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            ServerName.HeaderText = "Server Name";
            ServerName.Name = "ServerName";
            ServerName.ReadOnly = true;
            ServerName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // ServerIp
            // 
            ServerIp.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            ServerIp.HeaderText = "Server IP";
            ServerIp.Name = "ServerIp";
            ServerIp.ReadOnly = true;
            ServerIp.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Edit
            // 
            Edit.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            Edit.HeaderText = "                     ";
            Edit.Name = "Edit";
            Edit.ReadOnly = true;
            Edit.Text = "Options";
            Edit.UseColumnTextForButtonValue = true;
            Edit.Width = 99;
            // 
            // Play
            // 
            Play.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            Play.HeaderText = "                     ";
            Play.Name = "Play";
            Play.ReadOnly = true;
            Play.Text = "Start";
            Play.Width = 99;
            // 
            // ServerList
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(1200, 692);
            Controls.Add(ServerListLayout);
            Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            Name = "ServerList";
            Text = "Form1";
            ServerListLayout.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(GridServerList)).EndInit();
            ResumeLayout(false);
        }

        private System.Windows.Forms.Label jkghvcgjv;

        private System.Windows.Forms.Button ButtonRefresh;

        private System.Windows.Forms.DataGridViewTextBoxColumn ServerIp;

        private System.Windows.Forms.ToolTip ToolTips;

        private System.Windows.Forms.Label dnjlfe;

        private System.Windows.Forms.DataGridViewImageColumn ServerType;
        private System.Windows.Forms.DataGridViewTextBoxColumn Version;
        private System.Windows.Forms.DataGridViewTextBoxColumn ServerName;
        private System.Windows.Forms.DataGridViewButtonColumn Edit;
        private System.Windows.Forms.DataGridViewButtonColumn Play;

        public System.Windows.Forms.DataGridView GridServerList;

        #endregion

        private System.Windows.Forms.Panel ServerListLayout;
    }
}