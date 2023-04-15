using System.ComponentModel;
using System.Configuration;
using System.Drawing;
using System.IO;
using PgpsUtilsAEFC.common;
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

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoadingScreen));
            this.PictureBoxLoading = new System.Windows.Forms.PictureBox();
            this.LabelStatus = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBoxLoading)).BeginInit();
            this.SuspendLayout();
            // 
            // PictureBoxLoading
            // 
            this.PictureBoxLoading.BackColor = System.Drawing.Color.Transparent;
            this.PictureBoxLoading.Location = new System.Drawing.Point(1083, 578);
            this.PictureBoxLoading.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.PictureBoxLoading.Name = "PictureBoxLoading";
            this.PictureBoxLoading.Size = new System.Drawing.Size(99, 95);
            this.PictureBoxLoading.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.PictureBoxLoading.TabIndex = 0;
            this.PictureBoxLoading.TabStop = false;
            // 
            // LabelStatus
            // 
            this.LabelStatus.BackColor = System.Drawing.Color.Transparent;
            this.LabelStatus.Location = new System.Drawing.Point(18, 638);
            this.LabelStatus.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LabelStatus.Name = "LabelStatus";
            this.LabelStatus.Size = new System.Drawing.Size(1011, 35);
            this.LabelStatus.TabIndex = 1;
            // 
            // LoadingScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.ClientSize = new System.Drawing.Size(1200, 692);
            this.Controls.Add(this.LabelStatus);
            this.Controls.Add(this.PictureBoxLoading);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "LoadingScreen";
            this.Text = "LoadingScreen";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.LoadingScreen_FormClosing);
            this.Load += new System.EventHandler(this.LoadingScreen_Load);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBoxLoading)).EndInit();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Label LabelStatus;

        private System.Windows.Forms.PictureBox PictureBoxLoading;

        #endregion
    }
}