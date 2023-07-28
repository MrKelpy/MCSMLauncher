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
            LabelDownloadingAsset = new System.Windows.Forms.Label();
            ProgressBarDownload = new System.Windows.Forms.ProgressBar();
            SuspendLayout();
            // 
            // LabelDownloadingAsset
            // 
            LabelDownloadingAsset.Location = new System.Drawing.Point(0, 0);
            LabelDownloadingAsset.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            LabelDownloadingAsset.Name = "LabelDownloadingAsset";
            LabelDownloadingAsset.Size = new System.Drawing.Size(726, 96);
            LabelDownloadingAsset.TabIndex = 0;
            LabelDownloadingAsset.Text = "Downloading Assets...";
            LabelDownloadingAsset.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ProgressBarDownload
            // 
            ProgressBarDownload.Location = new System.Drawing.Point(39, 110);
            ProgressBarDownload.MarqueeAnimationSpeed = 400;
            ProgressBarDownload.Name = "ProgressBarDownload";
            ProgressBarDownload.Size = new System.Drawing.Size(654, 47);
            ProgressBarDownload.TabIndex = 1;
            // 
            // PreLoadingScreen
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(726, 169);
            Controls.Add(ProgressBarDownload);
            Controls.Add(LabelDownloadingAsset);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            Name = "PreLoadingScreen";
            FormClosing += new System.Windows.Forms.FormClosingEventHandler(PreLoadingScreen_FormClosing);
            Load += new System.EventHandler(PreLoadingScreen_Load);
            ResumeLayout(false);
        }

        private System.Windows.Forms.ProgressBar ProgressBarDownload;

        private System.Windows.Forms.Label LabelDownloadingAsset;

        #endregion
    }
}