namespace MecabEditor
{
    partial class frmMorphemeReplaceInfo
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
            this.lstvwMorpemeReplaceInfoSource = new System.Windows.Forms.ListView();
            this.lstvwMorpemeReplaceInfoDestination = new System.Windows.Forms.ListView();
            this.lblMorpemeReplaceInfoSource = new System.Windows.Forms.Label();
            this.lblMorpemeReplaceInfoDestination = new System.Windows.Forms.Label();
            this.btnSourceDel = new System.Windows.Forms.Button();
            this.btnSourceEdit = new System.Windows.Forms.Button();
            this.btnSourceAdd = new System.Windows.Forms.Button();
            this.btnDestinationDel = new System.Windows.Forms.Button();
            this.btnDestinationEdit = new System.Windows.Forms.Button();
            this.btnDestinationAdd = new System.Windows.Forms.Button();
            this.lblMorpemeReplaceInfoList = new System.Windows.Forms.Label();
            this.lstvwMorphemeReplaceInfo = new System.Windows.Forms.ListView();
            this.btnSet = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnDel = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnTake = new System.Windows.Forms.Button();
            this.lblName = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.btnSourceUp = new System.Windows.Forms.Button();
            this.btnSourceDown = new System.Windows.Forms.Button();
            this.btnDown = new System.Windows.Forms.Button();
            this.btnUp = new System.Windows.Forms.Button();
            this.chkLoop = new System.Windows.Forms.CheckBox();
            this.chkAuto = new System.Windows.Forms.CheckBox();
            this.chkHyoso = new System.Windows.Forms.CheckBox();
            this.lblHyosoIndex = new System.Windows.Forms.Label();
            this.txtHyosoIndex = new System.Windows.Forms.TextBox();
            this.lblReadFile = new System.Windows.Forms.Label();
            this.btnFile = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lstvwMorpemeReplaceInfoSource
            // 
            this.lstvwMorpemeReplaceInfoSource.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstvwMorpemeReplaceInfoSource.FullRowSelect = true;
            this.lstvwMorpemeReplaceInfoSource.Location = new System.Drawing.Point(12, 71);
            this.lstvwMorpemeReplaceInfoSource.Name = "lstvwMorpemeReplaceInfoSource";
            this.lstvwMorpemeReplaceInfoSource.Size = new System.Drawing.Size(605, 162);
            this.lstvwMorpemeReplaceInfoSource.TabIndex = 11;
            this.lstvwMorpemeReplaceInfoSource.UseCompatibleStateImageBehavior = false;
            this.lstvwMorpemeReplaceInfoSource.View = System.Windows.Forms.View.Details;
            // 
            // lstvwMorpemeReplaceInfoDestination
            // 
            this.lstvwMorpemeReplaceInfoDestination.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstvwMorpemeReplaceInfoDestination.FullRowSelect = true;
            this.lstvwMorpemeReplaceInfoDestination.Location = new System.Drawing.Point(12, 345);
            this.lstvwMorpemeReplaceInfoDestination.Name = "lstvwMorpemeReplaceInfoDestination";
            this.lstvwMorpemeReplaceInfoDestination.Size = new System.Drawing.Size(605, 60);
            this.lstvwMorpemeReplaceInfoDestination.TabIndex = 41;
            this.lstvwMorpemeReplaceInfoDestination.UseCompatibleStateImageBehavior = false;
            this.lstvwMorpemeReplaceInfoDestination.View = System.Windows.Forms.View.Details;
            // 
            // lblMorpemeReplaceInfoSource
            // 
            this.lblMorpemeReplaceInfoSource.AutoSize = true;
            this.lblMorpemeReplaceInfoSource.Font = new System.Drawing.Font("MS UI Gothic", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblMorpemeReplaceInfoSource.Location = new System.Drawing.Point(12, 45);
            this.lblMorpemeReplaceInfoSource.Name = "lblMorpemeReplaceInfoSource";
            this.lblMorpemeReplaceInfoSource.Size = new System.Drawing.Size(134, 14);
            this.lblMorpemeReplaceInfoSource.TabIndex = 10;
            this.lblMorpemeReplaceInfoSource.Text = "置換前形態要素リスト";
            // 
            // lblMorpemeReplaceInfoDestination
            // 
            this.lblMorpemeReplaceInfoDestination.AutoSize = true;
            this.lblMorpemeReplaceInfoDestination.Font = new System.Drawing.Font("MS UI Gothic", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblMorpemeReplaceInfoDestination.Location = new System.Drawing.Point(15, 328);
            this.lblMorpemeReplaceInfoDestination.Name = "lblMorpemeReplaceInfoDestination";
            this.lblMorpemeReplaceInfoDestination.Size = new System.Drawing.Size(134, 14);
            this.lblMorpemeReplaceInfoDestination.TabIndex = 40;
            this.lblMorpemeReplaceInfoDestination.Text = "置換後形態要素リスト";
            // 
            // btnSourceDel
            // 
            this.btnSourceDel.Font = new System.Drawing.Font("MS UI Gothic", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnSourceDel.Location = new System.Drawing.Point(199, 248);
            this.btnSourceDel.Name = "btnSourceDel";
            this.btnSourceDel.Size = new System.Drawing.Size(75, 23);
            this.btnSourceDel.TabIndex = 14;
            this.btnSourceDel.Text = "削除";
            this.btnSourceDel.UseVisualStyleBackColor = true;
            this.btnSourceDel.Click += new System.EventHandler(this.btnSourceDel_Click);
            // 
            // btnSourceEdit
            // 
            this.btnSourceEdit.Font = new System.Drawing.Font("MS UI Gothic", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnSourceEdit.Location = new System.Drawing.Point(106, 248);
            this.btnSourceEdit.Name = "btnSourceEdit";
            this.btnSourceEdit.Size = new System.Drawing.Size(75, 23);
            this.btnSourceEdit.TabIndex = 13;
            this.btnSourceEdit.Text = "編集";
            this.btnSourceEdit.UseVisualStyleBackColor = true;
            this.btnSourceEdit.Click += new System.EventHandler(this.btnSourceEdit_Click);
            // 
            // btnSourceAdd
            // 
            this.btnSourceAdd.Font = new System.Drawing.Font("MS UI Gothic", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnSourceAdd.Location = new System.Drawing.Point(15, 248);
            this.btnSourceAdd.Name = "btnSourceAdd";
            this.btnSourceAdd.Size = new System.Drawing.Size(75, 23);
            this.btnSourceAdd.TabIndex = 12;
            this.btnSourceAdd.Text = "追加";
            this.btnSourceAdd.UseVisualStyleBackColor = true;
            this.btnSourceAdd.Click += new System.EventHandler(this.btnSourceAdd_Click);
            // 
            // btnDestinationDel
            // 
            this.btnDestinationDel.Font = new System.Drawing.Font("MS UI Gothic", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnDestinationDel.Location = new System.Drawing.Point(199, 421);
            this.btnDestinationDel.Name = "btnDestinationDel";
            this.btnDestinationDel.Size = new System.Drawing.Size(75, 23);
            this.btnDestinationDel.TabIndex = 53;
            this.btnDestinationDel.Text = "削除";
            this.btnDestinationDel.UseVisualStyleBackColor = true;
            this.btnDestinationDel.Click += new System.EventHandler(this.btnDestinationDel_Click);
            // 
            // btnDestinationEdit
            // 
            this.btnDestinationEdit.Font = new System.Drawing.Font("MS UI Gothic", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnDestinationEdit.Location = new System.Drawing.Point(106, 421);
            this.btnDestinationEdit.Name = "btnDestinationEdit";
            this.btnDestinationEdit.Size = new System.Drawing.Size(75, 23);
            this.btnDestinationEdit.TabIndex = 52;
            this.btnDestinationEdit.Text = "編集";
            this.btnDestinationEdit.UseVisualStyleBackColor = true;
            this.btnDestinationEdit.Click += new System.EventHandler(this.btnDestinationEdit_Click);
            // 
            // btnDestinationAdd
            // 
            this.btnDestinationAdd.Font = new System.Drawing.Font("MS UI Gothic", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnDestinationAdd.Location = new System.Drawing.Point(15, 421);
            this.btnDestinationAdd.Name = "btnDestinationAdd";
            this.btnDestinationAdd.Size = new System.Drawing.Size(75, 23);
            this.btnDestinationAdd.TabIndex = 51;
            this.btnDestinationAdd.Text = "追加";
            this.btnDestinationAdd.UseVisualStyleBackColor = true;
            this.btnDestinationAdd.Click += new System.EventHandler(this.btnDestinationAdd_Click);
            // 
            // lblMorpemeReplaceInfoList
            // 
            this.lblMorpemeReplaceInfoList.AutoSize = true;
            this.lblMorpemeReplaceInfoList.Font = new System.Drawing.Font("MS UI Gothic", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblMorpemeReplaceInfoList.Location = new System.Drawing.Point(663, 9);
            this.lblMorpemeReplaceInfoList.Name = "lblMorpemeReplaceInfoList";
            this.lblMorpemeReplaceInfoList.Size = new System.Drawing.Size(148, 14);
            this.lblMorpemeReplaceInfoList.TabIndex = 60;
            this.lblMorpemeReplaceInfoList.Text = "形態要素置換情報リスト";
            // 
            // lstvwMorphemeReplaceInfo
            // 
            this.lstvwMorphemeReplaceInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstvwMorphemeReplaceInfo.FullRowSelect = true;
            this.lstvwMorphemeReplaceInfo.Location = new System.Drawing.Point(663, 35);
            this.lstvwMorphemeReplaceInfo.Name = "lstvwMorphemeReplaceInfo";
            this.lstvwMorphemeReplaceInfo.Size = new System.Drawing.Size(370, 341);
            this.lstvwMorphemeReplaceInfo.TabIndex = 61;
            this.lstvwMorphemeReplaceInfo.UseCompatibleStateImageBehavior = false;
            this.lstvwMorphemeReplaceInfo.View = System.Windows.Forms.View.Details;
            this.lstvwMorphemeReplaceInfo.SelectedIndexChanged += new System.EventHandler(this.lstvwMorphemeReplaceInfo_SelectedIndexChanged);
            // 
            // btnSet
            // 
            this.btnSet.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSet.Font = new System.Drawing.Font("MS UI Gothic", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnSet.Location = new System.Drawing.Point(877, 431);
            this.btnSet.Name = "btnSet";
            this.btnSet.Size = new System.Drawing.Size(75, 23);
            this.btnSet.TabIndex = 90;
            this.btnSet.Text = "設定";
            this.btnSet.UseVisualStyleBackColor = true;
            this.btnSet.Click += new System.EventHandler(this.btnSet_Click);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Font = new System.Drawing.Font("MS UI Gothic", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnClose.Location = new System.Drawing.Point(958, 431);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 91;
            this.btnClose.Text = "閉じる";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnDel
            // 
            this.btnDel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDel.Font = new System.Drawing.Font("MS UI Gothic", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnDel.Location = new System.Drawing.Point(847, 382);
            this.btnDel.Name = "btnDel";
            this.btnDel.Size = new System.Drawing.Size(75, 23);
            this.btnDel.TabIndex = 65;
            this.btnDel.Text = "削除";
            this.btnDel.UseVisualStyleBackColor = true;
            this.btnDel.Click += new System.EventHandler(this.btnDel_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEdit.Font = new System.Drawing.Font("MS UI Gothic", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnEdit.Location = new System.Drawing.Point(754, 382);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(75, 23);
            this.btnEdit.TabIndex = 64;
            this.btnEdit.Text = "編集";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAdd.Font = new System.Drawing.Font("MS UI Gothic", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnAdd.Location = new System.Drawing.Point(663, 382);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 63;
            this.btnAdd.Text = "追加";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnTake
            // 
            this.btnTake.Font = new System.Drawing.Font("MS UI Gothic", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnTake.Location = new System.Drawing.Point(542, 421);
            this.btnTake.Name = "btnTake";
            this.btnTake.Size = new System.Drawing.Size(75, 23);
            this.btnTake.TabIndex = 55;
            this.btnTake.Text = "決定";
            this.btnTake.UseVisualStyleBackColor = true;
            this.btnTake.Click += new System.EventHandler(this.btnTake_Click);
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Font = new System.Drawing.Font("MS UI Gothic", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblName.Location = new System.Drawing.Point(12, 9);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(133, 14);
            this.lblName.TabIndex = 1;
            this.lblName.Text = "形態要素置換情報名";
            // 
            // txtName
            // 
            this.txtName.Font = new System.Drawing.Font("MS UI Gothic", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtName.Location = new System.Drawing.Point(165, 6);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(234, 21);
            this.txtName.TabIndex = 2;
            // 
            // btnSourceUp
            // 
            this.btnSourceUp.Font = new System.Drawing.Font("MS UI Gothic", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnSourceUp.Location = new System.Drawing.Point(290, 248);
            this.btnSourceUp.Name = "btnSourceUp";
            this.btnSourceUp.Size = new System.Drawing.Size(75, 23);
            this.btnSourceUp.TabIndex = 15;
            this.btnSourceUp.Text = "↑";
            this.btnSourceUp.UseVisualStyleBackColor = true;
            this.btnSourceUp.Click += new System.EventHandler(this.btnSourceUp_Click);
            // 
            // btnSourceDown
            // 
            this.btnSourceDown.Font = new System.Drawing.Font("MS UI Gothic", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnSourceDown.Location = new System.Drawing.Point(381, 248);
            this.btnSourceDown.Name = "btnSourceDown";
            this.btnSourceDown.Size = new System.Drawing.Size(75, 23);
            this.btnSourceDown.TabIndex = 16;
            this.btnSourceDown.Text = "↓";
            this.btnSourceDown.UseVisualStyleBackColor = true;
            this.btnSourceDown.Click += new System.EventHandler(this.btnSourceDown_Click);
            // 
            // btnDown
            // 
            this.btnDown.Font = new System.Drawing.Font("MS UI Gothic", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnDown.Location = new System.Drawing.Point(988, 382);
            this.btnDown.Name = "btnDown";
            this.btnDown.Size = new System.Drawing.Size(45, 23);
            this.btnDown.TabIndex = 67;
            this.btnDown.Text = "↓";
            this.btnDown.UseVisualStyleBackColor = true;
            this.btnDown.Click += new System.EventHandler(this.btnDown_Click);
            // 
            // btnUp
            // 
            this.btnUp.Font = new System.Drawing.Font("MS UI Gothic", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnUp.Location = new System.Drawing.Point(940, 382);
            this.btnUp.Name = "btnUp";
            this.btnUp.Size = new System.Drawing.Size(45, 23);
            this.btnUp.TabIndex = 66;
            this.btnUp.Text = "↑";
            this.btnUp.UseVisualStyleBackColor = true;
            this.btnUp.Click += new System.EventHandler(this.btnUp_Click);
            // 
            // chkLoop
            // 
            this.chkLoop.AutoSize = true;
            this.chkLoop.Font = new System.Drawing.Font("MS UI Gothic", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.chkLoop.Location = new System.Drawing.Point(482, 251);
            this.chkLoop.Name = "chkLoop";
            this.chkLoop.Size = new System.Drawing.Size(59, 18);
            this.chkLoop.TabIndex = 17;
            this.chkLoop.Text = "ループ";
            this.chkLoop.UseVisualStyleBackColor = true;
            // 
            // chkAuto
            // 
            this.chkAuto.AutoSize = true;
            this.chkAuto.Font = new System.Drawing.Font("MS UI Gothic", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.chkAuto.Location = new System.Drawing.Point(558, 251);
            this.chkAuto.Name = "chkAuto";
            this.chkAuto.Size = new System.Drawing.Size(54, 18);
            this.chkAuto.TabIndex = 18;
            this.chkAuto.Text = "自動";
            this.chkAuto.UseVisualStyleBackColor = true;
            // 
            // chkHyoso
            // 
            this.chkHyoso.AutoSize = true;
            this.chkHyoso.Font = new System.Drawing.Font("MS UI Gothic", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.chkHyoso.Location = new System.Drawing.Point(18, 288);
            this.chkHyoso.Name = "chkHyoso";
            this.chkHyoso.Size = new System.Drawing.Size(68, 18);
            this.chkHyoso.TabIndex = 20;
            this.chkHyoso.Text = "表層系";
            this.chkHyoso.UseVisualStyleBackColor = true;
            this.chkHyoso.CheckedChanged += new System.EventHandler(this.chkHyoso_CheckedChanged);
            // 
            // lblHyosoIndex
            // 
            this.lblHyosoIndex.AutoSize = true;
            this.lblHyosoIndex.Font = new System.Drawing.Font("MS UI Gothic", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblHyosoIndex.Location = new System.Drawing.Point(103, 288);
            this.lblHyosoIndex.Name = "lblHyosoIndex";
            this.lblHyosoIndex.Size = new System.Drawing.Size(77, 14);
            this.lblHyosoIndex.TabIndex = 21;
            this.lblHyosoIndex.Text = "対象表層系";
            // 
            // txtHyosoIndex
            // 
            this.txtHyosoIndex.Font = new System.Drawing.Font("MS UI Gothic", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtHyosoIndex.Location = new System.Drawing.Point(199, 285);
            this.txtHyosoIndex.Name = "txtHyosoIndex";
            this.txtHyosoIndex.Size = new System.Drawing.Size(68, 21);
            this.txtHyosoIndex.TabIndex = 22;
            this.txtHyosoIndex.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblReadFile
            // 
            this.lblReadFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblReadFile.Font = new System.Drawing.Font("MS UI Gothic", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblReadFile.Location = new System.Drawing.Point(287, 288);
            this.lblReadFile.Name = "lblReadFile";
            this.lblReadFile.Size = new System.Drawing.Size(330, 14);
            this.lblReadFile.TabIndex = 23;
            this.lblReadFile.Text = "読取り対象";
            // 
            // btnFile
            // 
            this.btnFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFile.Font = new System.Drawing.Font("MS UI Gothic", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnFile.Location = new System.Drawing.Point(418, 306);
            this.btnFile.Name = "btnFile";
            this.btnFile.Size = new System.Drawing.Size(98, 23);
            this.btnFile.TabIndex = 31;
            this.btnFile.Text = "ファイル読取り";
            this.btnFile.UseVisualStyleBackColor = true;
            this.btnFile.Click += new System.EventHandler(this.btnFile_Click);
            // 
            // frmMorphemeReplaceInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1055, 457);
            this.Controls.Add(this.lblReadFile);
            this.Controls.Add(this.btnFile);
            this.Controls.Add(this.txtHyosoIndex);
            this.Controls.Add(this.lblHyosoIndex);
            this.Controls.Add(this.chkHyoso);
            this.Controls.Add(this.chkAuto);
            this.Controls.Add(this.chkLoop);
            this.Controls.Add(this.btnDown);
            this.Controls.Add(this.btnUp);
            this.Controls.Add(this.btnSourceDown);
            this.Controls.Add(this.btnSourceUp);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.btnTake);
            this.Controls.Add(this.btnDel);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.btnSet);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.lstvwMorphemeReplaceInfo);
            this.Controls.Add(this.lblMorpemeReplaceInfoList);
            this.Controls.Add(this.btnDestinationDel);
            this.Controls.Add(this.btnDestinationEdit);
            this.Controls.Add(this.btnDestinationAdd);
            this.Controls.Add(this.btnSourceDel);
            this.Controls.Add(this.btnSourceEdit);
            this.Controls.Add(this.btnSourceAdd);
            this.Controls.Add(this.lblMorpemeReplaceInfoDestination);
            this.Controls.Add(this.lblMorpemeReplaceInfoSource);
            this.Controls.Add(this.lstvwMorpemeReplaceInfoDestination);
            this.Controls.Add(this.lstvwMorpemeReplaceInfoSource);
            this.Name = "frmMorphemeReplaceInfo";
            this.Text = "形態要素置換情報設定";
            this.Shown += new System.EventHandler(this.frmMorphemeReplaceInfo_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView lstvwMorpemeReplaceInfoSource;
        private System.Windows.Forms.ListView lstvwMorpemeReplaceInfoDestination;
        private System.Windows.Forms.Label lblMorpemeReplaceInfoSource;
        private System.Windows.Forms.Label lblMorpemeReplaceInfoDestination;
        private System.Windows.Forms.Button btnSourceDel;
        private System.Windows.Forms.Button btnSourceEdit;
        private System.Windows.Forms.Button btnSourceAdd;
        private System.Windows.Forms.Button btnDestinationDel;
        private System.Windows.Forms.Button btnDestinationEdit;
        private System.Windows.Forms.Button btnDestinationAdd;
        private System.Windows.Forms.Label lblMorpemeReplaceInfoList;
        private System.Windows.Forms.ListView lstvwMorphemeReplaceInfo;
        private System.Windows.Forms.Button btnSet;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnDel;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnTake;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Button btnSourceUp;
        private System.Windows.Forms.Button btnSourceDown;
        private System.Windows.Forms.Button btnDown;
        private System.Windows.Forms.Button btnUp;
        private System.Windows.Forms.CheckBox chkLoop;
        private System.Windows.Forms.CheckBox chkAuto;
        private System.Windows.Forms.CheckBox chkHyoso;
        private System.Windows.Forms.Label lblHyosoIndex;
        private System.Windows.Forms.TextBox txtHyosoIndex;
        private System.Windows.Forms.Label lblReadFile;
        private System.Windows.Forms.Button btnFile;
    }
}