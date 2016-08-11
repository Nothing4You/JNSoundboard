namespace JNSoundboard
{
    partial class TTS
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TTS));
            this.tbText = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.tbKeys = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tbWhereSave = new System.Windows.Forms.TextBox();
            this.btnBrowseFolderLoc = new System.Windows.Forms.Button();
            this.btnCreateWAV = new System.Windows.Forms.Button();
            this.btnCreateWAVAdd = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // tbText
            // 
            this.tbText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbText.Location = new System.Drawing.Point(14, 25);
            this.tbText.Name = "tbText";
            this.tbText.Size = new System.Drawing.Size(384, 20);
            this.tbText.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(193, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(28, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Text";
            // 
            // btnClose
            // 
            this.btnClose.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnClose.Location = new System.Drawing.Point(171, 208);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 5;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // tbKeys
            // 
            this.tbKeys.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbKeys.Location = new System.Drawing.Point(14, 163);
            this.tbKeys.Name = "tbKeys";
            this.tbKeys.ReadOnly = true;
            this.tbKeys.Size = new System.Drawing.Size(235, 20);
            this.tbKeys.TabIndex = 3;
            this.tbKeys.Enter += new System.EventHandler(this.tbKeys_Enter);
            this.tbKeys.Leave += new System.EventHandler(this.tbKeys_Leave);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(122, 147);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(30, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Keys";
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(131, 58);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(143, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Where to save perm TTS file";
            // 
            // tbWhereSave
            // 
            this.tbWhereSave.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbWhereSave.Location = new System.Drawing.Point(14, 74);
            this.tbWhereSave.Name = "tbWhereSave";
            this.tbWhereSave.Size = new System.Drawing.Size(353, 20);
            this.tbWhereSave.TabIndex = 1;
            // 
            // btnBrowseFolderLoc
            // 
            this.btnBrowseFolderLoc.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBrowseFolderLoc.Location = new System.Drawing.Point(373, 72);
            this.btnBrowseFolderLoc.Name = "btnBrowseFolderLoc";
            this.btnBrowseFolderLoc.Size = new System.Drawing.Size(25, 23);
            this.btnBrowseFolderLoc.TabIndex = 2;
            this.btnBrowseFolderLoc.Text = "...";
            this.btnBrowseFolderLoc.UseVisualStyleBackColor = true;
            this.btnBrowseFolderLoc.Click += new System.EventHandler(this.btnBrowseFolderLoc_Click);
            // 
            // btnCreateWAV
            // 
            this.btnCreateWAV.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnCreateWAV.Location = new System.Drawing.Point(162, 108);
            this.btnCreateWAV.Name = "btnCreateWAV";
            this.btnCreateWAV.Size = new System.Drawing.Size(98, 23);
            this.btnCreateWAV.TabIndex = 12;
            this.btnCreateWAV.Text = "Only create WAV";
            this.btnCreateWAV.UseVisualStyleBackColor = true;
            this.btnCreateWAV.Click += new System.EventHandler(this.btnCreateWAV_Click);
            // 
            // btnCreateWAVAdd
            // 
            this.btnCreateWAVAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCreateWAVAdd.Location = new System.Drawing.Point(255, 161);
            this.btnCreateWAVAdd.Name = "btnCreateWAVAdd";
            this.btnCreateWAVAdd.Size = new System.Drawing.Size(143, 23);
            this.btnCreateWAVAdd.TabIndex = 4;
            this.btnCreateWAVAdd.Text = "Create WAV and add to list";
            this.btnCreateWAVAdd.UseVisualStyleBackColor = true;
            this.btnCreateWAVAdd.Click += new System.EventHandler(this.btnCreateWAVAdd_Click);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // TTS
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(414, 239);
            this.Controls.Add(this.btnCreateWAVAdd);
            this.Controls.Add(this.btnCreateWAV);
            this.Controls.Add(this.btnBrowseFolderLoc);
            this.Controls.Add(this.tbWhereSave);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbKeys);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbText);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(4000, 270);
            this.MinimumSize = new System.Drawing.Size(422, 270);
            this.Name = "TTS";
            this.Text = "TTS";
            this.Load += new System.EventHandler(this.TTS_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbText;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.TextBox tbKeys;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbWhereSave;
        private System.Windows.Forms.Button btnBrowseFolderLoc;
        private System.Windows.Forms.Button btnCreateWAV;
        private System.Windows.Forms.Button btnCreateWAVAdd;
        private System.Windows.Forms.Timer timer1;
    }
}