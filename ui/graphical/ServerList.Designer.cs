namespace MCSMLauncher.ui.graphical
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ServerList));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.ServerListLayout = new System.Windows.Forms.Panel();
            this.jkghvcgjv = new System.Windows.Forms.Label();
            this.ButtonRefresh = new System.Windows.Forms.Button();
            this.dnjlfe = new System.Windows.Forms.Label();
            this.GridServerList = new System.Windows.Forms.DataGridView();
            this.ToolTips = new System.Windows.Forms.ToolTip(this.components);
            this.ServerType = new System.Windows.Forms.DataGridViewImageColumn();
            this.Version = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ServerName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ServerIp = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Edit = new System.Windows.Forms.DataGridViewButtonColumn();
            this.Play = new System.Windows.Forms.DataGridViewButtonColumn();
            this.StopButton = new System.Windows.Forms.DataGridViewButtonColumn();
            this.ServerListLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GridServerList)).BeginInit();
            this.SuspendLayout();
            // 
            // ServerListLayout
            // 
            this.ServerListLayout.Controls.Add(this.jkghvcgjv);
            this.ServerListLayout.Controls.Add(this.ButtonRefresh);
            this.ServerListLayout.Controls.Add(this.dnjlfe);
            this.ServerListLayout.Controls.Add(this.GridServerList);
            this.ServerListLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ServerListLayout.Location = new System.Drawing.Point(0, 0);
            this.ServerListLayout.Name = "ServerListLayout";
            this.ServerListLayout.Size = new System.Drawing.Size(800, 450);
            this.ServerListLayout.TabIndex = 1;
            // 
            // jkghvcgjv
            // 
            this.jkghvcgjv.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.jkghvcgjv.Location = new System.Drawing.Point(32, 4);
            this.jkghvcgjv.Name = "jkghvcgjv";
            this.jkghvcgjv.Size = new System.Drawing.Size(727, 40);
            this.jkghvcgjv.TabIndex = 19;
            this.jkghvcgjv.Text = resources.GetString("jkghvcgjv.Text");
            this.jkghvcgjv.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ButtonRefresh
            // 
            this.ButtonRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ButtonRefresh.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.ButtonRefresh.Location = new System.Drawing.Point(764, 4);
            this.ButtonRefresh.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.ButtonRefresh.Name = "ButtonRefresh";
            this.ButtonRefresh.Size = new System.Drawing.Size(25, 25);
            this.ButtonRefresh.TabIndex = 18;
            this.ButtonRefresh.UseVisualStyleBackColor = true;
            this.ButtonRefresh.Click += new System.EventHandler(this.ButtonRefresh_Click);
            // 
            // dnjlfe
            // 
            this.dnjlfe.Location = new System.Drawing.Point(12, 10);
            this.dnjlfe.Name = "dnjlfe";
            this.dnjlfe.Size = new System.Drawing.Size(14, 14);
            this.dnjlfe.TabIndex = 17;
            this.dnjlfe.Tag = "tooltip";
            this.ToolTips.SetToolTip(this.dnjlfe, resources.GetString("dnjlfe.ToolTip"));
            // 
            // GridServerList
            // 
            this.GridServerList.AllowUserToAddRows = false;
            this.GridServerList.AllowUserToDeleteRows = false;
            this.GridServerList.AllowUserToResizeColumns = false;
            this.GridServerList.AllowUserToResizeRows = false;
            this.GridServerList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.GridServerList.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.GridServerList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.GridServerList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GridServerList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { this.ServerType, this.Version, this.ServerName, this.ServerIp, this.Edit, this.Play, this.StopButton });
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.GridServerList.DefaultCellStyle = dataGridViewCellStyle2;
            this.GridServerList.Location = new System.Drawing.Point(12, 47);
            this.GridServerList.MultiSelect = false;
            this.GridServerList.Name = "GridServerList";
            this.GridServerList.ReadOnly = true;
            this.GridServerList.RowHeadersVisible = false;
            this.GridServerList.ShowCellToolTips = false;
            this.GridServerList.ShowEditingIcon = false;
            this.GridServerList.Size = new System.Drawing.Size(776, 367);
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
            this.ServerType.Width = 71;
            // 
            // Version
            // 
            this.Version.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Version.HeaderText = "Version";
            this.Version.Name = "Version";
            this.Version.ReadOnly = true;
            this.Version.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Version.Width = 48;
            // 
            // ServerName
            // 
            this.ServerName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ServerName.HeaderText = "Server Name";
            this.ServerName.Name = "ServerName";
            this.ServerName.ReadOnly = true;
            this.ServerName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // ServerIp
            // 
            this.ServerIp.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ServerIp.HeaderText = "Server IP";
            this.ServerIp.Name = "ServerIp";
            this.ServerIp.ReadOnly = true;
            this.ServerIp.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Edit
            // 
            this.Edit.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.Edit.HeaderText = "                     ";
            this.Edit.Name = "Edit";
            this.Edit.ReadOnly = true;
            this.Edit.Text = "Options";
            this.Edit.UseColumnTextForButtonValue = true;
            this.Edit.Width = 76;
            // 
            // Play
            // 
            this.Play.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.Play.HeaderText = "                     ";
            this.Play.Name = "Play";
            this.Play.ReadOnly = true;
            this.Play.Text = "Start";
            this.Play.Width = 76;
            // 
            // StopButton
            // 
            this.StopButton.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.StopButton.HeaderText = "                     ";
            this.StopButton.Name = "StopButton";
            this.StopButton.ReadOnly = true;
            this.StopButton.Text = "Stop";
            this.StopButton.UseColumnTextForButtonValue = true;
            this.StopButton.Width = 76;
            // 
            // ServerList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.ServerListLayout);
            this.Name = "ServerList";
            this.Text = "Form1";
            this.ServerListLayout.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.GridServerList)).EndInit();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.DataGridViewButtonColumn StopButton;

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