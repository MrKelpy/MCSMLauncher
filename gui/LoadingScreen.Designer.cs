using System.ComponentModel;
using System.Configuration;
using System.Drawing;
using System.IO;
using static MCSMLauncher.common.Constants;

namespace MCSMLauncher.gui
{
    sealed partial class LoadingScreen
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

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoadingScreen));
            PictureBoxLoading = new System.Windows.Forms.PictureBox();
            LabelStatus = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(PictureBoxLoading)).BeginInit();
            SuspendLayout();
            // 
            // PictureBoxLoading
            // 
            PictureBoxLoading.BackColor = System.Drawing.Color.Transparent;
            PictureBoxLoading.Location = new System.Drawing.Point(722, 12);
            PictureBoxLoading.Name = "PictureBoxLoading";
            PictureBoxLoading.Size = new System.Drawing.Size(66, 62);
            PictureBoxLoading.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            PictureBoxLoading.TabIndex = 0;
            PictureBoxLoading.TabStop = false;
            // 
            // LabelStatus
            // 
            LabelStatus.BackColor = System.Drawing.Color.Transparent;
            LabelStatus.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            LabelStatus.Location = new System.Drawing.Point(4, 393);
            LabelStatus.Name = "LabelStatus";
            LabelStatus.Size = new System.Drawing.Size(673, 18);
            LabelStatus.TabIndex = 1;
            LabelStatus.Text = "TEXT GOES HERE";
            // 
            // LoadingScreen
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            ClientSize = new System.Drawing.Size(800, 413);
            Controls.Add(LabelStatus);
            Controls.Add(PictureBoxLoading);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            Icon = ((System.Drawing.Icon)(resources.GetObject("$Icon")));
            Name = "LoadingScreen";
            Text = "LoadingScreen";
            FormClosing += new System.Windows.Forms.FormClosingEventHandler(LoadingScreen_FormClosing);
            Load += new System.EventHandler(LoadingScreen_Load);
            ((System.ComponentModel.ISupportInitialize)(PictureBoxLoading)).EndInit();
            ResumeLayout(false);
        }

        private System.Windows.Forms.Label LabelStatus;
        private System.Windows.Forms.PictureBox PictureBoxLoading;
        
        #endregion
        
    }
}