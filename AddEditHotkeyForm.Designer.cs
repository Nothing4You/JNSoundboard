namespace JNSoundboard
{
    partial class AddEditHotkeyForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddEditHotkeyForm));
            this.tbKeys = new System.Windows.Forms.TextBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.tbLocation = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.labelLoc = new System.Windows.Forms.Label();
            this.btnBrowseSoundLoc = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.gbWindowRestriction = new System.Windows.Forms.GroupBox();
            this.btnReloadWindows = new System.Windows.Forms.Button();
            this.cbEnableRestrictWindow = new System.Windows.Forms.CheckBox();
            this.cbWindows = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.gbWindowRestriction.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbKeys
            // 
            this.tbKeys.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbKeys.Location = new System.Drawing.Point(15, 75);
            this.tbKeys.Name = "tbKeys";
            this.tbKeys.ReadOnly = true;
            this.tbKeys.Size = new System.Drawing.Size(346, 20);
            this.tbKeys.TabIndex = 2;
            this.tbKeys.Enter += new System.EventHandler(this.tbKeys_Enter);
            this.tbKeys.Leave += new System.EventHandler(this.tbKeys_Leave);
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Location = new System.Drawing.Point(199, 200);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 6;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(280, 199);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // tbLocation
            // 
            this.tbLocation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbLocation.Location = new System.Drawing.Point(15, 24);
            this.tbLocation.Name = "tbLocation";
            this.tbLocation.Size = new System.Drawing.Size(306, 20);
            this.tbLocation.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 59);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(230, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Keys (click on text box then press desired keys)";
            // 
            // labelLoc
            // 
            this.labelLoc.AutoSize = true;
            this.labelLoc.Location = new System.Drawing.Point(12, 8);
            this.labelLoc.Name = "labelLoc";
            this.labelLoc.Size = new System.Drawing.Size(76, 13);
            this.labelLoc.TabIndex = 5;
            this.labelLoc.Text = "Location of file";
            // 
            // btnBrowseSoundLoc
            // 
            this.btnBrowseSoundLoc.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBrowseSoundLoc.Location = new System.Drawing.Point(327, 22);
            this.btnBrowseSoundLoc.Name = "btnBrowseSoundLoc";
            this.btnBrowseSoundLoc.Size = new System.Drawing.Size(28, 23);
            this.btnBrowseSoundLoc.TabIndex = 1;
            this.btnBrowseSoundLoc.Text = "...";
            this.btnBrowseSoundLoc.UseVisualStyleBackColor = true;
            this.btnBrowseSoundLoc.Click += new System.EventHandler(this.btnBrowseSoundLoc_Click);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // gbWindowRestriction
            // 
            this.gbWindowRestriction.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbWindowRestriction.Controls.Add(this.btnReloadWindows);
            this.gbWindowRestriction.Controls.Add(this.cbEnableRestrictWindow);
            this.gbWindowRestriction.Controls.Add(this.cbWindows);
            this.gbWindowRestriction.Controls.Add(this.label2);
            this.gbWindowRestriction.Location = new System.Drawing.Point(15, 109);
            this.gbWindowRestriction.Name = "gbWindowRestriction";
            this.gbWindowRestriction.Size = new System.Drawing.Size(279, 73);
            this.gbWindowRestriction.TabIndex = 3;
            this.gbWindowRestriction.TabStop = false;
            this.gbWindowRestriction.Text = "Restrict to certain window";
            // 
            // btnReloadWindows
            // 
            this.btnReloadWindows.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnReloadWindows.Enabled = false;
            this.btnReloadWindows.Image = ((System.Drawing.Image)(resources.GetObject("btnReloadWindows.Image")));
            this.btnReloadWindows.Location = new System.Drawing.Point(246, 44);
            this.btnReloadWindows.Name = "btnReloadWindows";
            this.btnReloadWindows.Size = new System.Drawing.Size(22, 22);
            this.btnReloadWindows.TabIndex = 5;
            this.btnReloadWindows.UseVisualStyleBackColor = true;
            this.btnReloadWindows.Click += new System.EventHandler(this.btnReloadWindows_Click);
            // 
            // cbEnableRestrictWindow
            // 
            this.cbEnableRestrictWindow.AutoSize = true;
            this.cbEnableRestrictWindow.Location = new System.Drawing.Point(14, 19);
            this.cbEnableRestrictWindow.Name = "cbEnableRestrictWindow";
            this.cbEnableRestrictWindow.Size = new System.Drawing.Size(59, 17);
            this.cbEnableRestrictWindow.TabIndex = 3;
            this.cbEnableRestrictWindow.Text = "Enable";
            this.cbEnableRestrictWindow.UseVisualStyleBackColor = true;
            this.cbEnableRestrictWindow.CheckedChanged += new System.EventHandler(this.cbEnableRestrictWindow_CheckedChanged);
            // 
            // cbWindows
            // 
            this.cbWindows.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbWindows.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbWindows.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbWindows.Enabled = false;
            this.cbWindows.FormattingEnabled = true;
            this.cbWindows.Location = new System.Drawing.Point(63, 45);
            this.cbWindows.Name = "cbWindows";
            this.cbWindows.Size = new System.Drawing.Size(177, 21);
            this.cbWindows.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 13);
            this.label2.TabIndex = 17;
            this.label2.Text = "Window";
            // 
            // AddEditHotkeyForm
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(367, 231);
            this.Controls.Add(this.gbWindowRestriction);
            this.Controls.Add(this.btnBrowseSoundLoc);
            this.Controls.Add(this.labelLoc);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbLocation);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.tbKeys);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(4000, 270);
            this.MinimumSize = new System.Drawing.Size(375, 270);
            this.Name = "AddEditHotkeyForm";
            this.Text = "Add/edit sound";
            this.Load += new System.EventHandler(this.AddEditSoundKeys_Load);
            this.gbWindowRestriction.ResumeLayout(false);
            this.gbWindowRestriction.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbKeys;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.TextBox tbLocation;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelLoc;
        private System.Windows.Forms.Button btnBrowseSoundLoc;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.GroupBox gbWindowRestriction;
        private System.Windows.Forms.Button btnReloadWindows;
        private System.Windows.Forms.CheckBox cbEnableRestrictWindow;
        private System.Windows.Forms.ComboBox cbWindows;
        private System.Windows.Forms.Label label2;
    }
}