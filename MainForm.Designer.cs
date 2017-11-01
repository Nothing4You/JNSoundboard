namespace JNSoundboard
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.label1 = new System.Windows.Forms.Label();
            this.cbPlaybackDevices = new System.Windows.Forms.ComboBox();
            this.cbEnable = new System.Windows.Forms.CheckBox();
            this.mainTimer = new System.Windows.Forms.Timer(this.components);
            this.btnSave = new System.Windows.Forms.Button();
            this.btnRemove = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.lvKeySounds = new System.Windows.Forms.ListView();
            this.chKeys = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chWindow = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chSoundLoc = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnLoad = new System.Windows.Forms.Button();
            this.btnReloadDevices = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnSaveAs = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.texttospeechToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.checkForUpdateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label2 = new System.Windows.Forms.Label();
            this.cbLoopbackDevices = new System.Windows.Forms.ComboBox();
            this.btnPlaySelectedSound = new System.Windows.Forms.Button();
            this.btnStopAllSounds = new System.Windows.Forms.Button();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.gbPushToTalk = new System.Windows.Forms.GroupBox();
            this.btnReloadWindows = new System.Windows.Forms.Button();
            this.cbEnablePushToTalk = new System.Windows.Forms.CheckBox();
            this.cbWindows = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tbPushToTalkKey = new System.Windows.Forms.TextBox();
            this.cbAudioDevices = new System.Windows.Forms.GroupBox();
            this.pushToTalkKeyTimer = new System.Windows.Forms.Timer(this.components);
            this.menuStrip1.SuspendLayout();
            this.gbPushToTalk.SuspendLayout();
            this.cbAudioDevices.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Playback";
            // 
            // cbPlaybackDevices
            // 
            this.cbPlaybackDevices.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbPlaybackDevices.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPlaybackDevices.FormattingEnabled = true;
            this.cbPlaybackDevices.Location = new System.Drawing.Point(72, 20);
            this.cbPlaybackDevices.Name = "cbPlaybackDevices";
            this.cbPlaybackDevices.Size = new System.Drawing.Size(240, 21);
            this.cbPlaybackDevices.TabIndex = 10;
            // 
            // cbEnable
            // 
            this.cbEnable.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cbEnable.AutoSize = true;
            this.cbEnable.Location = new System.Drawing.Point(567, 350);
            this.cbEnable.Name = "cbEnable";
            this.cbEnable.Size = new System.Drawing.Size(59, 17);
            this.cbEnable.TabIndex = 17;
            this.cbEnable.Text = "Enable";
            this.cbEnable.UseVisualStyleBackColor = true;
            this.cbEnable.CheckedChanged += new System.EventHandler(this.cbEnable_CheckedChanged);
            // 
            // mainTimer
            // 
            this.mainTimer.Interval = 50;
            this.mainTimer.Tick += new System.EventHandler(this.mainTimer_Tick);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSave.Location = new System.Drawing.Point(163, 344);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(145, 23);
            this.btnSave.TabIndex = 8;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnRemove
            // 
            this.btnRemove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRemove.Location = new System.Drawing.Point(553, 125);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(75, 43);
            this.btnRemove.TabIndex = 3;
            this.btnRemove.Text = "Remove";
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEdit.Location = new System.Drawing.Point(553, 76);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(75, 43);
            this.btnEdit.TabIndex = 2;
            this.btnEdit.Text = "Edit";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAdd.Location = new System.Drawing.Point(553, 27);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 43);
            this.btnAdd.TabIndex = 1;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // lvKeySounds
            // 
            this.lvKeySounds.Alignment = System.Windows.Forms.ListViewAlignment.Default;
            this.lvKeySounds.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvKeySounds.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chKeys,
            this.chWindow,
            this.chSoundLoc});
            this.lvKeySounds.FullRowSelect = true;
            this.lvKeySounds.GridLines = true;
            this.lvKeySounds.Location = new System.Drawing.Point(12, 27);
            this.lvKeySounds.MultiSelect = false;
            this.lvKeySounds.Name = "lvKeySounds";
            this.lvKeySounds.Size = new System.Drawing.Size(535, 311);
            this.lvKeySounds.TabIndex = 0;
            this.lvKeySounds.UseCompatibleStateImageBehavior = false;
            this.lvKeySounds.View = System.Windows.Forms.View.Details;
            this.lvKeySounds.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lvKeySounds_MouseDoubleClick);
            // 
            // chKeys
            // 
            this.chKeys.Text = "Keys";
            this.chKeys.Width = 150;
            // 
            // chWindow
            // 
            this.chWindow.Text = "Window";
            // 
            // chSoundLoc
            // 
            this.chSoundLoc.Text = "Sound location";
            this.chSoundLoc.Width = 300;
            // 
            // btnLoad
            // 
            this.btnLoad.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnLoad.Location = new System.Drawing.Point(12, 344);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(145, 23);
            this.btnLoad.TabIndex = 7;
            this.btnLoad.Text = "Load";
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // btnReloadDevices
            // 
            this.btnReloadDevices.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnReloadDevices.Image = ((System.Drawing.Image)(resources.GetObject("btnReloadDevices.Image")));
            this.btnReloadDevices.Location = new System.Drawing.Point(318, 47);
            this.btnReloadDevices.Name = "btnReloadDevices";
            this.btnReloadDevices.Size = new System.Drawing.Size(22, 22);
            this.btnReloadDevices.TabIndex = 12;
            this.btnReloadDevices.UseVisualStyleBackColor = true;
            this.btnReloadDevices.Click += new System.EventHandler(this.btnReloadDevices_Click);
            // 
            // btnClear
            // 
            this.btnClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClear.Location = new System.Drawing.Point(553, 174);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 43);
            this.btnClear.TabIndex = 4;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnSaveAs
            // 
            this.btnSaveAs.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSaveAs.Location = new System.Drawing.Point(316, 344);
            this.btnSaveAs.Name = "btnSaveAs";
            this.btnSaveAs.Size = new System.Drawing.Size(145, 23);
            this.btnSaveAs.TabIndex = 9;
            this.btnSaveAs.Text = "Save As";
            this.btnSaveAs.UseVisualStyleBackColor = true;
            this.btnSaveAs.Click += new System.EventHandler(this.btnSaveAs_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.settingsToolStripMenuItem,
            this.texttospeechToolStripMenuItem,
            this.checkForUpdateToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(638, 24);
            this.menuStrip1.TabIndex = 17;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.settingsToolStripMenuItem.Text = "Settings";
            this.settingsToolStripMenuItem.Click += new System.EventHandler(this.settingsToolStripMenuItem_Click);
            // 
            // texttospeechToolStripMenuItem
            // 
            this.texttospeechToolStripMenuItem.Name = "texttospeechToolStripMenuItem";
            this.texttospeechToolStripMenuItem.Size = new System.Drawing.Size(99, 20);
            this.texttospeechToolStripMenuItem.Text = "Text-to-speech";
            this.texttospeechToolStripMenuItem.Click += new System.EventHandler(this.texttospeechToolStripMenuItem_Click);
            // 
            // checkForUpdateToolStripMenuItem
            // 
            this.checkForUpdateToolStripMenuItem.Name = "checkForUpdateToolStripMenuItem";
            this.checkForUpdateToolStripMenuItem.Size = new System.Drawing.Size(110, 20);
            this.checkForUpdateToolStripMenuItem.Text = "Check for update";
            this.checkForUpdateToolStripMenuItem.Click += new System.EventHandler(this.checkForUpdateToolStripMenuItem_Click);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 13);
            this.label2.TabIndex = 18;
            this.label2.Text = "Loopback";
            // 
            // cbLoopbackDevices
            // 
            this.cbLoopbackDevices.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbLoopbackDevices.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbLoopbackDevices.FormattingEnabled = true;
            this.cbLoopbackDevices.Location = new System.Drawing.Point(72, 47);
            this.cbLoopbackDevices.Name = "cbLoopbackDevices";
            this.cbLoopbackDevices.Size = new System.Drawing.Size(240, 21);
            this.cbLoopbackDevices.TabIndex = 11;
            // 
            // btnPlaySelectedSound
            // 
            this.btnPlaySelectedSound.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPlaySelectedSound.Location = new System.Drawing.Point(553, 246);
            this.btnPlaySelectedSound.Name = "btnPlaySelectedSound";
            this.btnPlaySelectedSound.Size = new System.Drawing.Size(75, 43);
            this.btnPlaySelectedSound.TabIndex = 5;
            this.btnPlaySelectedSound.Text = "Play sound";
            this.btnPlaySelectedSound.UseVisualStyleBackColor = true;
            this.btnPlaySelectedSound.Click += new System.EventHandler(this.btnPlaySound_Click);
            // 
            // btnStopAllSounds
            // 
            this.btnStopAllSounds.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStopAllSounds.Location = new System.Drawing.Point(553, 295);
            this.btnStopAllSounds.Name = "btnStopAllSounds";
            this.btnStopAllSounds.Size = new System.Drawing.Size(75, 43);
            this.btnStopAllSounds.TabIndex = 6;
            this.btnStopAllSounds.Text = "Stop all sounds";
            this.btnStopAllSounds.UseVisualStyleBackColor = true;
            this.btnStopAllSounds.Click += new System.EventHandler(this.btnStopAllSounds_Click);
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.notifyIcon1.BalloonTipText = "Minimized to the tray.";
            this.notifyIcon1.BalloonTipTitle = "JN Soundboard";
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "JN Soundboard";
            this.notifyIcon1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseClick);
            // 
            // gbPushToTalk
            // 
            this.gbPushToTalk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.gbPushToTalk.Controls.Add(this.btnReloadWindows);
            this.gbPushToTalk.Controls.Add(this.cbEnablePushToTalk);
            this.gbPushToTalk.Controls.Add(this.cbWindows);
            this.gbPushToTalk.Controls.Add(this.label4);
            this.gbPushToTalk.Controls.Add(this.label3);
            this.gbPushToTalk.Controls.Add(this.tbPushToTalkKey);
            this.gbPushToTalk.Location = new System.Drawing.Point(372, 393);
            this.gbPushToTalk.Name = "gbPushToTalk";
            this.gbPushToTalk.Size = new System.Drawing.Size(254, 94);
            this.gbPushToTalk.TabIndex = 13;
            this.gbPushToTalk.TabStop = false;
            this.gbPushToTalk.Text = "Auto press push to talk key";
            // 
            // btnReloadWindows
            // 
            this.btnReloadWindows.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnReloadWindows.Image = ((System.Drawing.Image)(resources.GetObject("btnReloadWindows.Image")));
            this.btnReloadWindows.Location = new System.Drawing.Point(226, 45);
            this.btnReloadWindows.Name = "btnReloadWindows";
            this.btnReloadWindows.Size = new System.Drawing.Size(22, 22);
            this.btnReloadWindows.TabIndex = 15;
            this.btnReloadWindows.UseVisualStyleBackColor = true;
            this.btnReloadWindows.Click += new System.EventHandler(this.btnReloadWindows_Click);
            // 
            // cbEnablePushToTalk
            // 
            this.cbEnablePushToTalk.AutoSize = true;
            this.cbEnablePushToTalk.Location = new System.Drawing.Point(10, 72);
            this.cbEnablePushToTalk.Name = "cbEnablePushToTalk";
            this.cbEnablePushToTalk.Size = new System.Drawing.Size(59, 17);
            this.cbEnablePushToTalk.TabIndex = 16;
            this.cbEnablePushToTalk.Text = "Enable";
            this.cbEnablePushToTalk.UseVisualStyleBackColor = true;
            this.cbEnablePushToTalk.CheckedChanged += new System.EventHandler(this.cbEnablePushToTalk_CheckedChanged);
            // 
            // cbWindows
            // 
            this.cbWindows.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbWindows.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbWindows.FormattingEnabled = true;
            this.cbWindows.Location = new System.Drawing.Point(59, 45);
            this.cbWindows.Name = "cbWindows";
            this.cbWindows.Size = new System.Drawing.Size(161, 21);
            this.cbWindows.TabIndex = 14;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 48);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(46, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "Window";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(25, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Key";
            // 
            // tbPushToTalkKey
            // 
            this.tbPushToTalkKey.Location = new System.Drawing.Point(59, 19);
            this.tbPushToTalkKey.Name = "tbPushToTalkKey";
            this.tbPushToTalkKey.ReadOnly = true;
            this.tbPushToTalkKey.Size = new System.Drawing.Size(161, 20);
            this.tbPushToTalkKey.TabIndex = 13;
            this.tbPushToTalkKey.Enter += new System.EventHandler(this.tbPushToTalkKey_Enter);
            this.tbPushToTalkKey.Leave += new System.EventHandler(this.tbPushToTalkKey_Leave);
            // 
            // cbAudioDevices
            // 
            this.cbAudioDevices.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbAudioDevices.Controls.Add(this.label1);
            this.cbAudioDevices.Controls.Add(this.label2);
            this.cbAudioDevices.Controls.Add(this.cbPlaybackDevices);
            this.cbAudioDevices.Controls.Add(this.cbLoopbackDevices);
            this.cbAudioDevices.Controls.Add(this.btnReloadDevices);
            this.cbAudioDevices.Location = new System.Drawing.Point(12, 413);
            this.cbAudioDevices.Name = "cbAudioDevices";
            this.cbAudioDevices.Size = new System.Drawing.Size(354, 74);
            this.cbAudioDevices.TabIndex = 10;
            this.cbAudioDevices.TabStop = false;
            this.cbAudioDevices.Text = "Audio devices";
            // 
            // pushToTalkKeyTimer
            // 
            this.pushToTalkKeyTimer.Tick += new System.EventHandler(this.pushToTalkKeyTimer_Tick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(638, 499);
            this.Controls.Add(this.cbAudioDevices);
            this.Controls.Add(this.gbPushToTalk);
            this.Controls.Add(this.btnStopAllSounds);
            this.Controls.Add(this.btnPlaySelectedSound);
            this.Controls.Add(this.btnSaveAs);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnLoad);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnRemove);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.lvKeySounds);
            this.Controls.Add(this.cbEnable);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(610, 530);
            this.Name = "MainForm";
            this.Text = "JN Soundboard";
            this.Resize += new System.EventHandler(this.frmMain_Resize);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.gbPushToTalk.ResumeLayout(false);
            this.gbPushToTalk.PerformLayout();
            this.cbAudioDevices.ResumeLayout(false);
            this.cbAudioDevices.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbPlaybackDevices;
        private System.Windows.Forms.CheckBox cbEnable;
        internal System.Windows.Forms.Timer mainTimer;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnAdd;
        internal System.Windows.Forms.ListView lvKeySounds;
        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.Button btnReloadDevices;
        internal System.Windows.Forms.ColumnHeader chKeys;
        internal System.Windows.Forms.ColumnHeader chSoundLoc;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnSaveAs;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbLoopbackDevices;
        private System.Windows.Forms.Button btnPlaySelectedSound;
        private System.Windows.Forms.Button btnStopAllSounds;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.GroupBox gbPushToTalk;
        private System.Windows.Forms.CheckBox cbEnablePushToTalk;
        private System.Windows.Forms.ComboBox cbWindows;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbPushToTalkKey;
        private System.Windows.Forms.GroupBox cbAudioDevices;
        private System.Windows.Forms.ToolStripMenuItem texttospeechToolStripMenuItem;
        private System.Windows.Forms.Button btnReloadWindows;
        private System.Windows.Forms.Timer pushToTalkKeyTimer;
        internal System.Windows.Forms.ColumnHeader chWindow;
        private System.Windows.Forms.ToolStripMenuItem checkForUpdateToolStripMenuItem;
    }
}

