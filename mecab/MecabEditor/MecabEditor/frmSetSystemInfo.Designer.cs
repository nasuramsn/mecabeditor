﻿namespace MecabEditor
{
    partial class frmSetSystemInfo
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
            this.lblSeedFolder = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnSet = new System.Windows.Forms.Button();
            this.txtSeedFolder = new System.Windows.Forms.TextBox();
            this.btnSeedFolderSet = new System.Windows.Forms.Button();
            this.btnCsvFolderSet = new System.Windows.Forms.Button();
            this.txtCsvFolder = new System.Windows.Forms.TextBox();
            this.lblCsvFolder = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblSeedFolder
            // 
            this.lblSeedFolder.AutoSize = true;
            this.lblSeedFolder.Font = new System.Drawing.Font("MS UI Gothic", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblSeedFolder.Location = new System.Drawing.Point(12, 9);
            this.lblSeedFolder.Name = "lblSeedFolder";
            this.lblSeedFolder.Size = new System.Drawing.Size(94, 14);
            this.lblSeedFolder.TabIndex = 11;
            this.lblSeedFolder.Text = "Corpusフォルダ：";
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Font = new System.Drawing.Font("MS UI Gothic", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnClose.Location = new System.Drawing.Point(478, 142);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 91;
            this.btnClose.Text = "閉じる";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnSet
            // 
            this.btnSet.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSet.Font = new System.Drawing.Font("MS UI Gothic", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnSet.Location = new System.Drawing.Point(397, 142);
            this.btnSet.Name = "btnSet";
            this.btnSet.Size = new System.Drawing.Size(75, 23);
            this.btnSet.TabIndex = 90;
            this.btnSet.Text = "設定";
            this.btnSet.UseVisualStyleBackColor = true;
            this.btnSet.Click += new System.EventHandler(this.btnSet_Click);
            // 
            // txtSeedFolder
            // 
            this.txtSeedFolder.Font = new System.Drawing.Font("MS UI Gothic", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtSeedFolder.Location = new System.Drawing.Point(108, 7);
            this.txtSeedFolder.Name = "txtSeedFolder";
            this.txtSeedFolder.Size = new System.Drawing.Size(323, 21);
            this.txtSeedFolder.TabIndex = 12;
            // 
            // btnSeedFolderSet
            // 
            this.btnSeedFolderSet.Font = new System.Drawing.Font("MS UI Gothic", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnSeedFolderSet.Location = new System.Drawing.Point(451, 5);
            this.btnSeedFolderSet.Name = "btnSeedFolderSet";
            this.btnSeedFolderSet.Size = new System.Drawing.Size(75, 23);
            this.btnSeedFolderSet.TabIndex = 13;
            this.btnSeedFolderSet.Text = "参照";
            this.btnSeedFolderSet.UseVisualStyleBackColor = true;
            this.btnSeedFolderSet.Click += new System.EventHandler(this.btnSeedFolderSet_Click);
            // 
            // btnCsvFolderSet
            // 
            this.btnCsvFolderSet.Font = new System.Drawing.Font("MS UI Gothic", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnCsvFolderSet.Location = new System.Drawing.Point(451, 32);
            this.btnCsvFolderSet.Name = "btnCsvFolderSet";
            this.btnCsvFolderSet.Size = new System.Drawing.Size(75, 23);
            this.btnCsvFolderSet.TabIndex = 23;
            this.btnCsvFolderSet.Text = "参照";
            this.btnCsvFolderSet.UseVisualStyleBackColor = true;
            this.btnCsvFolderSet.Click += new System.EventHandler(this.btnCsvFolderSet_Click);
            // 
            // txtCsvFolder
            // 
            this.txtCsvFolder.Font = new System.Drawing.Font("MS UI Gothic", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtCsvFolder.Location = new System.Drawing.Point(108, 34);
            this.txtCsvFolder.Name = "txtCsvFolder";
            this.txtCsvFolder.Size = new System.Drawing.Size(323, 21);
            this.txtCsvFolder.TabIndex = 22;
            // 
            // lblCsvFolder
            // 
            this.lblCsvFolder.AutoSize = true;
            this.lblCsvFolder.Font = new System.Drawing.Font("MS UI Gothic", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblCsvFolder.Location = new System.Drawing.Point(12, 36);
            this.lblCsvFolder.Name = "lblCsvFolder";
            this.lblCsvFolder.Size = new System.Drawing.Size(75, 14);
            this.lblCsvFolder.TabIndex = 21;
            this.lblCsvFolder.Text = "Csvフォルダ：";
            // 
            // frmSetSystemInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(556, 167);
            this.ControlBox = false;
            this.Controls.Add(this.btnCsvFolderSet);
            this.Controls.Add(this.txtCsvFolder);
            this.Controls.Add(this.lblCsvFolder);
            this.Controls.Add(this.btnSeedFolderSet);
            this.Controls.Add(this.txtSeedFolder);
            this.Controls.Add(this.btnSet);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.lblSeedFolder);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSetSystemInfo";
            this.ShowIcon = false;
            this.Text = "システム情報設定";
            this.Shown += new System.EventHandler(this.frmSetSystemInfo_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblSeedFolder;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnSet;
        private System.Windows.Forms.TextBox txtSeedFolder;
        private System.Windows.Forms.Button btnSeedFolderSet;
        private System.Windows.Forms.Button btnCsvFolderSet;
        private System.Windows.Forms.TextBox txtCsvFolder;
        private System.Windows.Forms.Label lblCsvFolder;
    }
}