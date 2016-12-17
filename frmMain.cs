using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Linq;
using System.Threading;
using System.Media;
using NAudio.Wave;

namespace JNSoundboard
{
    public partial class frmMain : Form
    {
        private WaveIn loopbackSourceStream = null;
        private BufferedWaveProvider loopbackWaveProvider = null;
        private WaveOut loopbackWaveOut = null;
        private WaveOut playbackWaveOut = null;

        private Random rand = new Random();

        internal List<XMLSettings.KeysSounds> keysSounds = new List<XMLSettings.KeysSounds>();

        internal string xmlLoc = "";

        public frmMain()
        {
            InitializeComponent();

            loadSoundDevices();

            XMLSettings.LoadXML();
        }

        private void loadSoundDevices()
        {
            var playbackSources = new List<WaveOutCapabilities>();
            var loopbackSources = new List<WaveInCapabilities>();

            for (int i = 0; i < WaveOut.DeviceCount; i++)
            {
                playbackSources.Add(WaveOut.GetCapabilities(i));
            }

            for (int i = 0; i < WaveIn.DeviceCount; i++)
            {
                loopbackSources.Add(WaveIn.GetCapabilities(i));
            }

            cbPlaybackDevices.Items.Clear();
            cbLoopbackDevices.Items.Clear();

            foreach (var source in playbackSources)
            {
                cbPlaybackDevices.Items.Add(source.ProductName);
            }

            if (cbPlaybackDevices.Items.Count > 0)
                cbPlaybackDevices.SelectedIndex = 0;

            cbLoopbackDevices.Items.Add("");

            foreach (var source in loopbackSources)
            {
                cbLoopbackDevices.Items.Add(source.ProductName);
            }

            cbLoopbackDevices.SelectedIndex = 0;
        }

        private void startLoopback()
        {
            stopLoopback();

            int deviceNumber = cbLoopbackDevices.SelectedIndex - 1;

            if (loopbackSourceStream == null)
                loopbackSourceStream = new WaveIn();
            loopbackSourceStream.DeviceNumber = deviceNumber;
            loopbackSourceStream.WaveFormat = new WaveFormat(44100, WaveIn.GetCapabilities(deviceNumber).Channels);
            loopbackSourceStream.BufferMilliseconds = 25;
            loopbackSourceStream.DataAvailable += loopbackSourceStream_DataAvailable;

            loopbackWaveProvider = new BufferedWaveProvider(loopbackSourceStream.WaveFormat);

            if (loopbackWaveOut == null)
                loopbackWaveOut = new WaveOut();
            loopbackWaveOut.DeviceNumber = cbPlaybackDevices.SelectedIndex;
            loopbackWaveOut.DesiredLatency = 100;
            loopbackWaveOut.Init(loopbackWaveProvider);

            loopbackSourceStream.StartRecording();
            loopbackWaveOut.Play();
        }

        private void stopLoopback()
        {
            try
            {
                if (loopbackWaveOut != null)
                    loopbackWaveOut.Stop();

                if (loopbackWaveProvider != null)
                {
                    loopbackWaveProvider.ClearBuffer();
                    loopbackWaveProvider = null;
                }

                if (loopbackSourceStream != null)
                    loopbackSourceStream.StopRecording();
            }
            catch (Exception) { }
        }

        private void stopPlayback()
        {
            try
            {
                if (playbackWaveOut != null && playbackWaveOut.PlaybackState == PlaybackState.Playing)
                    playbackWaveOut.Stop();
            }
            catch (Exception) { }
        }

        private void playSound(string file)
        {
            int deviceNumber = cbPlaybackDevices.SelectedIndex;

            stopPlayback();

            if (playbackWaveOut == null) playbackWaveOut = new WaveOut();

            playbackWaveOut.DeviceNumber = deviceNumber;

            try
            {
                playbackWaveOut.Init(new AudioFileReader(file));

                playbackWaveOut.Play();
            }
            catch (FormatException ex)
            {
                SystemSounds.Beep.Play();
                MessageBox.Show(ex.ToString());
            }
            catch (NAudio.MmException ex)
            {
                SystemSounds.Beep.Play();
                string msg = ex.ToString();
                MessageBox.Show((msg.Contains("UnspecifiedError calling waveOutOpen") ? "Something is wrong with either the sound you tried to play (" + file.Substring(file.LastIndexOf("\\") + 1) + ") (try converting it to another format) or your sound card driver\n\n" + msg : msg));
            }
        }

        private void loadXMLFile(string path)
        {
            XMLSettings.Settings s = (XMLSettings.Settings)XMLSettings.ReadXML(typeof(XMLSettings.Settings), path);

            if (s != null && s.KeysSounds != null && s.KeysSounds.Length > 0)
            {
                var items = new List<ListViewItem>();
                string errors = "";
                string sameKeys = "";

                for (int i = 0; i < s.KeysSounds.Length; i++)
                {
                    int kLength = s.KeysSounds[i].Keys.Length;
                    bool keysNotNull = s.KeysSounds[i].Keys.Any(x => x != 0);
                    int sLength = s.KeysSounds[i].SoundLocations.Length;
                    bool soundsNotEmpty = s.KeysSounds[i].SoundLocations.All(x => !string.IsNullOrWhiteSpace(x));
                    Environment.CurrentDirectory = Path.GetDirectoryName(Application.ExecutablePath);
                    bool filesExist = s.KeysSounds[i].SoundLocations.All(x => File.Exists(x));

                    if (kLength < 1 || !keysNotNull || sLength < 1 || !soundsNotEmpty || !filesExist) //error in XML file
                    {
                        string tempErr = "";

                        if (kLength < 1) tempErr = "no keys are provided";
                        else if (!keysNotNull) tempErr = "one or more keys are null/set to None";
                        else if (sLength < 1) tempErr = "no sounds provided";
                        else if (!filesExist) tempErr = "one or more sounds do not exist";

                        errors += "Entry #" + i.ToString() + "has an error: " + tempErr;
                    }

                    string keys = (kLength < 1 ? "" : Helper.keysArrayToString(s.KeysSounds[i].Keys));

                    if (keys != "" && items.Count > 0 && items[items.Count - 1].Text == keys && !sameKeys.Contains(keys))
                    {
                        sameKeys += (sameKeys != "" ? ", " : "") + keys;
                    }

                    var temp = new ListViewItem(keys);
                    temp.SubItems.Add((sLength < 1 ? "" : Helper.soundLocsArrayToString(s.KeysSounds[i].SoundLocations)));

                    items.Add(temp); //add even if there was an error, so that the user can fix within the app
                }


                if (items.Count > 0)
                {
                    if (errors != "")
                    {
                        MessageBox.Show((errors == "" ? "" : errors));
                    }

                    if (sameKeys != "")
                    {
                        MessageBox.Show("Multiple entries using the same keys. The key(s) being used multiple times are: " + sameKeys);
                    }

                    keysSounds.Clear();
                    keysSounds.AddRange(s.KeysSounds);

                    lvKeySounds.Items.Clear();
                    lvKeySounds.Items.AddRange(items.ToArray());

                    chKeys.Width = -2;
                    chSoundLoc.Width = -2;

                    xmlLoc = path;
                }
                else
                {
                    SystemSounds.Beep.Play();
                    MessageBox.Show("No entries found, or all entries had errors in them (key being None, sound location behind empty or non-existant)");
                }
            }
            else
            {
                SystemSounds.Beep.Play();
                MessageBox.Show("No entries found, or there was an error reading the settings file");
            }
        }

        private void loopbackSourceStream_DataAvailable(object sender, WaveInEventArgs e)
        {
            if (loopbackWaveProvider != null) loopbackWaveProvider.AddSamples(e.Buffer, 0, e.BytesRecorded);
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new frmSettings();
            form.ShowDialog();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var form = new frmAddEditKSs();
            form.ShowDialog();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (lvKeySounds.SelectedItems.Count > 0)
            {
                var form = new frmAddEditKSs();

                ListViewItem item = lvKeySounds.SelectedItems[0];

                form.editSoundKeys = new string[2];
                form.editSoundKeys[0] = item.Text;
                form.editSoundKeys[1] = item.SubItems[1].Text;

                form.editIndex = lvKeySounds.SelectedIndices[0];

                form.ShowDialog();
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (lvKeySounds.SelectedItems.Count > 0 && MessageBox.Show("Are you sure remove that item?", "Remove", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                keysSounds.RemoveAt(lvKeySounds.SelectedIndices[0]);
                lvKeySounds.Items.Remove(lvKeySounds.SelectedItems[0]);

                if (lvKeySounds.Items.Count == 0) cbEnable.Checked = false;
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to clear all items?", "Clear", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                keysSounds.Clear();
                lvKeySounds.Items.Clear();

                cbEnable.Checked = false;
            }
        }

        private void btnPlaySound_Click(object sender, EventArgs e)
        {
            if (lvKeySounds.SelectedItems.Count > 0)
                playKeySound(keysSounds[lvKeySounds.SelectedIndices[0]]);
        }

        private void btnStopAllSounds_Click(object sender, EventArgs e)
        {
            stopPlayback();
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            var diag = new OpenFileDialog();

            diag.Filter = "XML file containing keys and sounds|*.xml";

            var result = diag.ShowDialog();

            if (result == DialogResult.OK)
            {
                string path = diag.FileName;

                loadXMLFile(path);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (xmlLoc == "" || !File.Exists(xmlLoc))
                xmlLoc = Helper.userGetXMLLoc();

            if (xmlLoc != "")
            {
                XMLSettings.WriteXML(new XMLSettings.Settings() { KeysSounds = keysSounds.ToArray() }, xmlLoc);

                MessageBox.Show("Saved");
            }
        }

        private void btnSaveAs_Click(object sender, EventArgs e)
        {
            string last = xmlLoc;

            xmlLoc = Helper.userGetXMLLoc();

            if (xmlLoc == "")
                xmlLoc = last;
            else if (last != xmlLoc)
            {
                XMLSettings.WriteXML(new XMLSettings.Settings() { KeysSounds = keysSounds.ToArray() }, xmlLoc);

                MessageBox.Show("Saved");
            }
        }

        private void btnReloadDevices_Click(object sender, EventArgs e)
        {
            stopPlayback();
            stopLoopback();

            loadSoundDevices();
        }

        private void btnTTS_Click(object sender, EventArgs e)
        {
            var form = new frmTTS();
            form.ShowDialog();
        }

        private void cbEnable_CheckedChanged(object sender, EventArgs e)
        {
            if (cbEnable.Checked)
            {
                //enable timer if there are any keys to check. start loopback
                if ((keysSounds != null && keysSounds.Count > 0) || (XMLSettings.loadXMLFileKeys != null && XMLSettings.loadXMLFileKeys.Count > 0))
                    timer1.Enabled = true;
                else
                    cbEnable.Checked = false;

                if (cbEnable.Checked && cbPlaybackDevices.Items.Count > 0 && cbLoopbackDevices.SelectedIndex > 0)
                    startLoopback();
            }
            else
            {
                //disable timer, sounds, and loopback
                timer1.Enabled = false;

                stopPlayback();
                stopLoopback();
            }
        }

        private void lvKeySounds_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnEdit_Click(null, null); //open edit form
        }

        private Keys[] keysJustPressed = null;
        private bool showingMsgBox = false;
        private int lastIndex = -1;

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (cbEnable.Checked)
            {
                int keysPressed = 0;

                if (keysSounds.Count > 0) //check that required keys are pressed to play sound
                {
                    for (int i = 0; i < keysSounds.Count; i++)
                    {
                        keysPressed = 0;

                        for (int j = 0; j < keysSounds[i].Keys.Length; j++)
                        {
                            if (Helper.IsKeyDown(keysSounds[i].Keys[j]))
                                keysPressed++;
                        }

                        if (keysPressed == keysSounds[i].Keys.Length)
                        {
                            if (keysJustPressed != keysSounds[i].Keys)
                            {
                                if (keysSounds[i].Keys.Length > 0 && keysSounds[i].Keys.All(x => x != 0) && keysSounds[i].SoundLocations.Length > 0 && keysSounds[i].SoundLocations.Length > 0 && keysSounds[i].SoundLocations.Any(x => File.Exists(x)))
                                {
                                    playKeySound(keysSounds[i]);
                                    return;
                                }
                            }
                        }
                        else if (keysJustPressed == keysSounds[i].Keys)
                            keysJustPressed = null;
                    }

                    keysPressed = 0;
                }

                if (XMLSettings.keysStopSound != null && XMLSettings.keysStopSound.Length > 0) //check that required keys are pressed to stop all sounds
                {
                    for (int i = 0; i < XMLSettings.keysStopSound.Length; i++)
                    {
                        if (Helper.IsKeyDown(XMLSettings.keysStopSound[i])) keysPressed++;
                    }

                    if (keysPressed == XMLSettings.keysStopSound.Length)
                    {
                        if (keysJustPressed == null || !keysJustPressed.Intersect(XMLSettings.keysStopSound).Any())
                        {
                            if (playbackWaveOut != null && playbackWaveOut.PlaybackState == PlaybackState.Playing) playbackWaveOut.Stop();

                            keysJustPressed = XMLSettings.keysStopSound;

                            return;
                        }
                    }
                    else if (keysJustPressed == XMLSettings.keysStopSound)
                        keysJustPressed = null;

                    keysPressed = 0;
                }

                if (XMLSettings.loadXMLFileKeys != null && XMLSettings.loadXMLFileKeys.Count > 0) //check that required keys are pressed to load XML file
                {
                    for (int i = 0; i < XMLSettings.loadXMLFileKeys.Count; i++)
                    {
                        keysPressed = 0;

                        for (int j = 0; j < XMLSettings.loadXMLFileKeys[i].Item1.Length; j++)
                        {
                            if (Helper.IsKeyDown(XMLSettings.loadXMLFileKeys[i].Item1[j])) keysPressed++;
                        }

                        if (keysPressed == XMLSettings.loadXMLFileKeys[i].Item1.Length)
                        {
                            if (keysJustPressed == null || !keysJustPressed.Intersect(XMLSettings.loadXMLFileKeys[i].Item1).Any())
                            {
                                if (!string.IsNullOrWhiteSpace(XMLSettings.loadXMLFileKeys[i].Item2) && File.Exists(XMLSettings.loadXMLFileKeys[i].Item2))
                                {
                                    keysJustPressed = XMLSettings.loadXMLFileKeys[i].Item1;

                                    loadXMLFile(XMLSettings.loadXMLFileKeys[i].Item2);
                                }

                                return;
                            }
                        }
                        else if (keysJustPressed == XMLSettings.loadXMLFileKeys[i].Item1)
                        {
                            keysJustPressed = null;
                        }
                    }

                    keysPressed = 0;
                }
            }
        }

        private void playKeySound(XMLSettings.KeysSounds currentKeysSounds)
        {
            Environment.CurrentDirectory = Path.GetDirectoryName(Application.ExecutablePath);

            string path;

            if (currentKeysSounds.SoundLocations.Length > 1)
            {
                //get random sound
                int temp;

                while (true)
                {
                    temp = rand.Next(0, currentKeysSounds.SoundLocations.Length);

                    if (temp != lastIndex && File.Exists(currentKeysSounds.SoundLocations[temp])) break;
                    Thread.Sleep(1);
                }

                lastIndex = temp;

                path = currentKeysSounds.SoundLocations[lastIndex];
            }
            else
                path = currentKeysSounds.SoundLocations[0]; //get first sound

            if (File.Exists(path))
            {
                playSound(path);
                keysJustPressed = currentKeysSounds.Keys;
            }
            else if (!showingMsgBox) //dont run when already showing messagebox (don't want a bunch of these on your screen, do you?)
            {
                SystemSounds.Beep.Play();
                showingMsgBox = true;
                MessageBox.Show("File " + path + " does not exist");
                showingMsgBox = false;
            }
        }

        private void cbLoopbackDevices_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbLoopbackDevices.SelectedIndex > 0)
            {
                if (cbEnable.Checked) //start loopback on new device, or stop loopback
                    startLoopback();
                else
                    stopLoopback();
            }
        }

        private void cbPlaybackDevices_SelectedIndexChanged(object sender, EventArgs e)
        {
            //start loopback on new device and stop all sounds playing
            if (loopbackWaveOut != null && loopbackSourceStream != null && cbEnable.Checked)
                startLoopback();

            stopPlayback();
        }

        private void frmMain_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                notifyIcon1.Visible = true;

                this.Hide();
            }
        }

        private void notifyIcon1_MouseClick(object sender, MouseEventArgs e)
        {
            notifyIcon1.Visible = false;

            this.WindowState = FormWindowState.Minimized; //show form and give focus
            this.Show();
            this.WindowState = FormWindowState.Normal;
        }
    }
}
