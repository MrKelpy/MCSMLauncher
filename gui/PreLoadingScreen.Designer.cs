using System.ComponentModel;

namespace MCSMLauncher.gui
{
    partial class PreLoadingScreen 
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PreLoadingScreen));
            this.LabelDownloadingAsset = new System.Windows.Forms.Label();
            this.ProgressBarDownload = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // LabelDownloadingAsset
            // 
            this.LabelDownloadingAsset.Location = new System.Drawing.Point(0, 0);
            this.LabelDownloadingAsset.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LabelDownloadingAsset.Name = "LabelDownloadingAsset";
            this.LabelDownloadingAsset.Size = new System.Drawing.Size(726, 96);
            this.LabelDownloadingAsset.TabIndex = 0;
            this.LabelDownloadingAsset.Text = "Downloading Assets...";
            this.LabelDownloadingAsset.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ProgressBarDownload
            // 
            this.ProgressBarDownload.Location = new System.Drawing.Point(39, 110);
            this.ProgressBarDownload.MarqueeAnimationSpeed = 400;
            this.ProgressBarDownload.Name = "ProgressBarDownload";
            this.ProgressBarDownload.Size = new System.Drawing.Size(654, 47);
            this.ProgressBarDownload.TabIndex = 1;
            // 
            // PreLoadingScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(726, 169);
            this.Controls.Add(this.ProgressBarDownload);
            this.Controls.Add(this.LabelDownloadingAsset);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "PreLoadingScreen";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PreLoadingScreen_FormClosing);
            this.Load += new System.EventHandler(this.PreLoadingScreen_Load);
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.ProgressBar ProgressBarDownload;

        private System.Windows.Forms.Label LabelDownloadingAsset;

        #endregion
    }
}