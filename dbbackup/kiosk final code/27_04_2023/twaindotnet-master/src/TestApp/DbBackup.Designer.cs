﻿
namespace Dbbackup
{
    partial class DbBackup
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
            this.btnDbbackup = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnDbbackup
            // 
            this.btnDbbackup.Location = new System.Drawing.Point(276, 187);
            this.btnDbbackup.Name = "btnDbbackup";
            this.btnDbbackup.Size = new System.Drawing.Size(236, 23);
            this.btnDbbackup.TabIndex = 0;
            this.btnDbbackup.Text = "Database BackUp";
            this.btnDbbackup.UseVisualStyleBackColor = true;
            this.btnDbbackup.Click += new System.EventHandler(this.btnDbbackup_Click);
            // 
            // DbBackup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnDbbackup);
            this.Name = "DbBackup";
            this.Text = "DbBackup";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnDbbackup;
    }
}