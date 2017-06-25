﻿namespace MecabEditor
{
    partial class frmMain
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.btnClose = new System.Windows.Forms.Button();
            this.lblReadFile = new System.Windows.Forms.Label();
            this.lstvwMain = new System.Windows.Forms.ListView();
            this.cmbEditMode = new System.Windows.Forms.ComboBox();
            this.lblFilePath = new System.Windows.Forms.Label();
            this.btnDel = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.lblCopusInfo = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.ファイルToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.上書き保存SToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ソース読み込みFToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.コーパス読込みCToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.終了XToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.編集EToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.検索FToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.設定ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.システム設定TToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.置換情報設定WToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnEos = new System.Windows.Forms.Button();
            this.btnAllReplace = new System.Windows.Forms.Button();
            this.btnCheck = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Font = new System.Drawing.Font("MS UI Gothic", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnClose.Location = new System.Drawing.Point(1096, 547);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 601;
            this.btnClose.Text = "閉じる";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // lblReadFile
            // 
            this.lblReadFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblReadFile.AutoSize = true;
            this.lblReadFile.Font = new System.Drawing.Font("MS UI Gothic", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblReadFile.Location = new System.Drawing.Point(20, 39);
            this.lblReadFile.Name = "lblReadFile";
            this.lblReadFile.Size = new System.Drawing.Size(63, 14);
            this.lblReadFile.TabIndex = 10;
            this.lblReadFile.Text = "編集対象";
            // 
            // lstvwMain
            // 
            this.lstvwMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstvwMain.Font = new System.Drawing.Font("MS UI Gothic", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lstvwMain.FullRowSelect = true;
            this.lstvwMain.Location = new System.Drawing.Point(13, 64);
            this.lstvwMain.Name = "lstvwMain";
            this.lstvwMain.Size = new System.Drawing.Size(1159, 475);
            this.lstvwMain.TabIndex = 1;
            this.lstvwMain.UseCompatibleStateImageBehavior = false;
            this.lstvwMain.View = System.Windows.Forms.View.Details;
            this.lstvwMain.SelectedIndexChanged += new System.EventHandler(this.lstvwMain_SelectedIndexChanged);
            // 
            // cmbEditMode
            // 
            this.cmbEditMode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbEditMode.Font = new System.Drawing.Font("MS UI Gothic", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.cmbEditMode.FormattingEnabled = true;
            this.cmbEditMode.Location = new System.Drawing.Point(343, 547);
            this.cmbEditMode.Name = "cmbEditMode";
            this.cmbEditMode.Size = new System.Drawing.Size(121, 21);
            this.cmbEditMode.TabIndex = 100;
            this.cmbEditMode.SelectedIndexChanged += new System.EventHandler(this.cmbEditMode_SelectedIndexChanged);
            // 
            // lblFilePath
            // 
            this.lblFilePath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblFilePath.Font = new System.Drawing.Font("MS UI Gothic", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblFilePath.Location = new System.Drawing.Point(89, 40);
            this.lblFilePath.Name = "lblFilePath";
            this.lblFilePath.Size = new System.Drawing.Size(486, 13);
            this.lblFilePath.TabIndex = 11;
            // 
            // btnDel
            // 
            this.btnDel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnDel.Font = new System.Drawing.Font("MS UI Gothic", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnDel.Location = new System.Drawing.Point(201, 545);
            this.btnDel.Name = "btnDel";
            this.btnDel.Size = new System.Drawing.Size(75, 23);
            this.btnDel.TabIndex = 12;
            this.btnDel.Text = "削除";
            this.btnDel.UseVisualStyleBackColor = true;
            this.btnDel.Click += new System.EventHandler(this.btnDel_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnEdit.Font = new System.Drawing.Font("MS UI Gothic", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnEdit.Location = new System.Drawing.Point(108, 545);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(75, 23);
            this.btnEdit.TabIndex = 11;
            this.btnEdit.Text = "編集";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAdd.Font = new System.Drawing.Font("MS UI Gothic", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnAdd.Location = new System.Drawing.Point(17, 545);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 10;
            this.btnAdd.Text = "追加";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // lblCopusInfo
            // 
            this.lblCopusInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCopusInfo.AutoSize = true;
            this.lblCopusInfo.Font = new System.Drawing.Font("Meiryo UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblCopusInfo.Location = new System.Drawing.Point(844, 40);
            this.lblCopusInfo.Name = "lblCopusInfo";
            this.lblCopusInfo.Size = new System.Drawing.Size(64, 18);
            this.lblCopusInfo.TabIndex = 91;
            this.lblCopusInfo.Text = "要素数：";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ファイルToolStripMenuItem,
            this.編集EToolStripMenuItem,
            this.設定ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1183, 26);
            this.menuStrip1.TabIndex = 92;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // ファイルToolStripMenuItem
            // 
            this.ファイルToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.上書き保存SToolStripMenuItem,
            this.ソース読み込みFToolStripMenuItem,
            this.コーパス読込みCToolStripMenuItem,
            this.toolStripSeparator1,
            this.終了XToolStripMenuItem});
            this.ファイルToolStripMenuItem.Name = "ファイルToolStripMenuItem";
            this.ファイルToolStripMenuItem.Size = new System.Drawing.Size(85, 22);
            this.ファイルToolStripMenuItem.Text = "ファイル(&F)";
            // 
            // 上書き保存SToolStripMenuItem
            // 
            this.上書き保存SToolStripMenuItem.Name = "上書き保存SToolStripMenuItem";
            this.上書き保存SToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.上書き保存SToolStripMenuItem.Size = new System.Drawing.Size(201, 22);
            this.上書き保存SToolStripMenuItem.Text = "上書き保存(&S)";
            this.上書き保存SToolStripMenuItem.Click += new System.EventHandler(this.上書き保存SToolStripMenuItem_Click);
            // 
            // ソース読み込みFToolStripMenuItem
            // 
            this.ソース読み込みFToolStripMenuItem.Enabled = false;
            this.ソース読み込みFToolStripMenuItem.Name = "ソース読み込みFToolStripMenuItem";
            this.ソース読み込みFToolStripMenuItem.Size = new System.Drawing.Size(201, 22);
            this.ソース読み込みFToolStripMenuItem.Text = "ソース読み込み(&F)";
            this.ソース読み込みFToolStripMenuItem.Click += new System.EventHandler(this.ソース読み込みFToolStripMenuItem_Click);
            // 
            // コーパス読込みCToolStripMenuItem
            // 
            this.コーパス読込みCToolStripMenuItem.Name = "コーパス読込みCToolStripMenuItem";
            this.コーパス読込みCToolStripMenuItem.Size = new System.Drawing.Size(201, 22);
            this.コーパス読込みCToolStripMenuItem.Text = "コーパス読込み(&C)";
            this.コーパス読込みCToolStripMenuItem.Click += new System.EventHandler(this.コーパス読込みCToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(198, 6);
            // 
            // 終了XToolStripMenuItem
            // 
            this.終了XToolStripMenuItem.Name = "終了XToolStripMenuItem";
            this.終了XToolStripMenuItem.Size = new System.Drawing.Size(201, 22);
            this.終了XToolStripMenuItem.Text = "終了(&X)";
            this.終了XToolStripMenuItem.Click += new System.EventHandler(this.終了XToolStripMenuItem_Click);
            // 
            // 編集EToolStripMenuItem
            // 
            this.編集EToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.検索FToolStripMenuItem});
            this.編集EToolStripMenuItem.Name = "編集EToolStripMenuItem";
            this.編集EToolStripMenuItem.Size = new System.Drawing.Size(61, 22);
            this.編集EToolStripMenuItem.Text = "編集(&E)";
            // 
            // 検索FToolStripMenuItem
            // 
            this.検索FToolStripMenuItem.Name = "検索FToolStripMenuItem";
            this.検索FToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F)));
            this.検索FToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.検索FToolStripMenuItem.Text = "検索";
            this.検索FToolStripMenuItem.Click += new System.EventHandler(this.検索FToolStripMenuItem_Click);
            // 
            // 設定ToolStripMenuItem
            // 
            this.設定ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.システム設定TToolStripMenuItem,
            this.置換情報設定WToolStripMenuItem});
            this.設定ToolStripMenuItem.Name = "設定ToolStripMenuItem";
            this.設定ToolStripMenuItem.Size = new System.Drawing.Size(62, 22);
            this.設定ToolStripMenuItem.Text = "設定(&S)";
            // 
            // システム設定TToolStripMenuItem
            // 
            this.システム設定TToolStripMenuItem.Name = "システム設定TToolStripMenuItem";
            this.システム設定TToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.システム設定TToolStripMenuItem.Text = "システム設定(&T)";
            this.システム設定TToolStripMenuItem.Click += new System.EventHandler(this.システム設定TToolStripMenuItem_Click);
            // 
            // 置換情報設定WToolStripMenuItem
            // 
            this.置換情報設定WToolStripMenuItem.Name = "置換情報設定WToolStripMenuItem";
            this.置換情報設定WToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.置換情報設定WToolStripMenuItem.Text = "置換情報設定(&W)";
            this.置換情報設定WToolStripMenuItem.Click += new System.EventHandler(this.置換情報設定WToolStripMenuItem_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Font = new System.Drawing.Font("MS UI Gothic", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnSave.Location = new System.Drawing.Point(966, 547);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 501;
            this.btnSave.Text = "保存";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnEos
            // 
            this.btnEos.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEos.Font = new System.Drawing.Font("MS UI Gothic", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnEos.Location = new System.Drawing.Point(652, 547);
            this.btnEos.Name = "btnEos";
            this.btnEos.Size = new System.Drawing.Size(75, 23);
            this.btnEos.TabIndex = 201;
            this.btnEos.Text = "EOS集約";
            this.btnEos.UseVisualStyleBackColor = true;
            this.btnEos.Click += new System.EventHandler(this.btnEos_Click);
            // 
            // btnAllReplace
            // 
            this.btnAllReplace.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAllReplace.Font = new System.Drawing.Font("MS UI Gothic", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnAllReplace.Location = new System.Drawing.Point(571, 547);
            this.btnAllReplace.Name = "btnAllReplace";
            this.btnAllReplace.Size = new System.Drawing.Size(75, 23);
            this.btnAllReplace.TabIndex = 200;
            this.btnAllReplace.Text = "置換実行";
            this.btnAllReplace.UseVisualStyleBackColor = true;
            this.btnAllReplace.Click += new System.EventHandler(this.btnAllReplace_Click);
            // 
            // btnCheck
            // 
            this.btnCheck.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCheck.Font = new System.Drawing.Font("MS UI Gothic", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnCheck.Location = new System.Drawing.Point(733, 547);
            this.btnCheck.Name = "btnCheck";
            this.btnCheck.Size = new System.Drawing.Size(75, 23);
            this.btnCheck.TabIndex = 202;
            this.btnCheck.Text = "チェック";
            this.btnCheck.UseVisualStyleBackColor = true;
            this.btnCheck.Click += new System.EventHandler(this.btnCheck_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1183, 589);
            this.Controls.Add(this.btnCheck);
            this.Controls.Add(this.btnAllReplace);
            this.Controls.Add(this.btnEos);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.lblCopusInfo);
            this.Controls.Add(this.btnDel);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.lblFilePath);
            this.Controls.Add(this.cmbEditMode);
            this.Controls.Add(this.lstvwMain);
            this.Controls.Add(this.lblReadFile);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "frmMain";
            this.Text = "コーパス編集";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.Shown += new System.EventHandler(this.frmMain_Shown);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label lblReadFile;
        private System.Windows.Forms.ListView lstvwMain;
        private System.Windows.Forms.ComboBox cmbEditMode;
        private System.Windows.Forms.Label lblFilePath;
        private System.Windows.Forms.Button btnDel;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Label lblCopusInfo;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem ファイルToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 終了XToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 設定ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem システム設定TToolStripMenuItem;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.ToolStripMenuItem 上書き保存SToolStripMenuItem;
        private System.Windows.Forms.Button btnEos;
        private System.Windows.Forms.ToolStripMenuItem 置換情報設定WToolStripMenuItem;
        private System.Windows.Forms.Button btnAllReplace;
        private System.Windows.Forms.ToolStripMenuItem 編集EToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 検索FToolStripMenuItem;
        private System.Windows.Forms.Button btnCheck;
        private System.Windows.Forms.ToolStripMenuItem ソース読み込みFToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem コーパス読込みCToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    }
}

