using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Linq;
using System.Threading;
using System.Media;
using System.Net;

namespace JNSoundboard
{
    public partial class MainForm : Form
    {
        NAudio.Wave.WaveIn loopbackSourceStream = null;
        NAudio.Wave.WaveOut loopbackWaveOut = null;
        NAudio.Wave.WaveOut playbackWaveOut = null;

        private Random rand = new Random();

        internal List<XMLSettings.KeysSounds> keysSounds = new List<XMLSettings.KeysSounds>();
        internal Keys[] keysStopSound = null;
        internal List<Tuple<Keys[], string>> loadSettingsKeysLoc = new List<Tuple<Keys[], string>>();

        internal string xmlLoc = "";

        internal string[] editSoundKeys = null;
        internal int editIndex = -1;

        public MainForm()
        {
            InitializeComponent();

            loadSoundDevices();

            string filePath = Path.GetDirectoryName(Application.ExecutablePath) + "\\settings.xml";

            if (File.Exists(filePath))
            {
                var settings = (XMLSettings.SoundboardSettings)XMLSettings.ReadXML(typeof(XMLSettings.SoundboardSettings), filePath);

                if (settings.StopSoundKeys != null)
                {
                    Keys[] keysArr = null;
                    string error = "";

                    if (Helper.keysArrayFromString(settings.StopSoundKeys, out keysArr, out error))
                    {
                        keysStopSound = keysArr;
                    }
                    else if (error != "Key string \"\" doesn't exist") MessageBox.Show(error);
                }
                else keysStopSound = new Keys[] { };

                if (settings.LoadXMLFiles != null && settings.LoadXMLFiles.Any(x => x.Keys != null && x.Keys.Length > 0 && !string.IsNullOrWhiteSpace(string.Join("", x.Keys)) && !string.IsNullOrWhiteSpace(x.XMLLocation) && File.Exists(x.XMLLocation)))
                {
                    for (int i = 0; i < settings.LoadXMLFiles.Length; i++)
                    {
                        Keys[] keysArr;
                        string error;

                        if (Helper.keysArrayFromString(settings.LoadXMLFiles[i].Keys, out keysArr, out error))
                        {
                            loadSettingsKeysLoc.Add(new Tuple<Keys[], string>(keysArr, settings.LoadXMLFiles[i].XMLLocation));
                        }
                    }
                }
            }
            else
            {
                XMLSettings.WriteXML(new XMLSettings.SoundboardSettings("", new XMLSettings.LoadXMLFile[] { new XMLSettings.LoadXMLFile("", "") }), filePath);
                keysStopSound = new Keys[] { };
            }
        }

        private void loadSoundDevices()
        {
            var playbackSources = new List<NAudio.Wave.WaveOutCapabilities>();
            var loopbackSources = new List<NAudio.Wave.WaveInCapabilities>();

            for (int i = 0; i < NAudio.Wave.WaveOut.DeviceCount; i++)
            {
                playbackSources.Add(NAudio.Wave.WaveOut.GetCapabilities(i));
            }

            for (int i = 0; i < NAudio.Wave.WaveIn.DeviceCount; i++)
            {
                loopbackSources.Add(NAudio.Wave.WaveIn.GetCapabilities(i));
            }

            cbPlaybackDevices.Items.Clear();
            cbLoopbackDevices.Items.Clear();

            foreach (var source in playbackSources)
            {
                cbPlaybackDevices.Items.Add(source.ProductName);
            }

            if (cbPlaybackDevices.Items.Count > 0) cbPlaybackDevices.SelectedIndex = 0;
            
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

            loopbackSourceStream = new NAudio.Wave.WaveIn();
            loopbackSourceStream.DeviceNumber = deviceNumber;
            loopbackSourceStream.WaveFormat = new NAudio.Wave.WaveFormat(44100, NAudio.Wave.WaveIn.GetCapabilities(deviceNumber).Channels);
            loopbackSourceStream.BufferMilliseconds = 100;

            var waveIn = new NAudio.Wave.WaveInProvider(loopbackSourceStream);

            loopbackWaveOut = new NAudio.Wave.WaveOut();
            loopbackWaveOut.DeviceNumber = cbPlaybackDevices.SelectedIndex;
            loopbackWaveOut.Init(waveIn);
            
            loopbackWaveOut.Play();
            loopbackSourceStream.StartRecording();
        }

        private void stopLoopback()
        {
            if (loopbackSourceStream != null || loopbackWaveOut != null)
            {
                if (loopbackWaveOut != null)
                {
                    try
                    {
                        loopbackWaveOut.Stop();
                    }
                    catch (Exception) { }

                    loopbackWaveOut.Dispose();
                    loopbackWaveOut = null;
                }

                if (loopbackSourceStream != null)
                {
                    try
                    {
                        loopbackSourceStream.StopRecording();
                    }
                    catch (Exception) { }

                    loopbackSourceStream.Dispose();
                    loopbackSourceStream = null;
                }
            }
        }

        private void stopPlayback()
        {
            if (playbackWaveOut != null)
            {
                try
                {
                    playbackWaveOut.Stop();
                }
                catch (Exception) { }

                playbackWaveOut.Dispose();
                playbackWaveOut = null;
            }
        }

        private void playSound(string file)
        {
            int deviceNumber = cbPlaybackDevices.SelectedIndex;

            stopPlayback();

            if (playbackWaveOut == null) playbackWaveOut = new NAudio.Wave.WaveOut();
            

            playbackWaveOut.DeviceNumber = deviceNumber;

            try
            {
                playbackWaveOut.Init(new NAudio.Wave.AudioFileReader(file));

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

                    if (kLength < 1 || !keysNotNull || sLength < 1 || !soundsNotEmpty || !filesExist)
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

                    items.Add(temp);
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
                MessageBox.Show("Returned null or KeysSounds.Length == 0");
            }
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new SoundboardSettings();
            form.ShowDialog();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            editSoundKeys = null;
            editIndex = -1;

            var form = new AddEditKeysLocation();
            form.ShowDialog();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (lvKeySounds.SelectedItems.Count > 0)
            {
                ListViewItem item = lvKeySounds.SelectedItems[0];

                editSoundKeys = new string[2];
                editSoundKeys[0] = item.Text;
                editSoundKeys[1] = item.SubItems[1].Text;

                editIndex = lvKeySounds.SelectedIndices[0];

                var form = new AddEditKeysLocation();
                form.ShowDialog();

                editSoundKeys = null;
                editIndex = -1;
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (lvKeySounds.SelectedItems.Count > 0 && MessageBox.Show("Are you sure about that?", "Well...", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                keysSounds.RemoveAt(lvKeySounds.SelectedIndices[0]);
                lvKeySounds.Items.Remove(lvKeySounds.SelectedItems[0]);

                if (lvKeySounds.Items.Count == 0) cbEnable.Checked = false;
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure about that?", "Well...", MessageBoxButtons.YesNo) == DialogResult.Yes && MessageBox.Show("ABSOLUTELY SURE?", "Sure?", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                keysSounds.Clear();
                lvKeySounds.Items.Clear();

                cbEnable.Checked = false;
            }
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
            {
                xmlLoc = Helper.userGetXMLLoc();
            }

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

            if (xmlLoc == "") xmlLoc = last;
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
            var form = new TTS();
            form.ShowDialog();
        }

        private void cbEnable_CheckedChanged(object sender, EventArgs e)
        {
            if (cbEnable.Checked)
            {
                if ((keysSounds != null && keysSounds.Count > 0) || (loadSettingsKeysLoc != null && loadSettingsKeysLoc.Count > 0))
                {
                    timer1.Enabled = true;
                }
                else
                {
                    cbEnable.Checked = false;
                }

                if (cbEnable.Checked && cbPlaybackDevices.Items.Count > 0 && cbLoopbackDevices.SelectedIndex > 0) startLoopback();
            }
            else
            {
                timer1.Enabled = false;

                stopPlayback();
                stopLoopback();
            }
        }

        private void lvKeySounds_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnEdit_Click(null, null);
        }

        Keys[] keysJustPressed = null;
        bool showingMsgBox = false;
        int lastIndex = -1;

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (cbEnable.Checked)
            {
                int keysPressed = 0;

                if (keysSounds.Count > 0)
                {
                    for (int i = 0; i < keysSounds.Count; i++)
                    {
                        keysPressed = 0;

                        for (int j = 0; j < keysSounds[i].Keys.Length; j++)
                        {
                            if (Helper.IsKeyDown(keysSounds[i].Keys[j]))
                            {
                                keysPressed++;
                            }
                        }

                        if (keysPressed == keysSounds[i].Keys.Length)
                        {
                            if (keysJustPressed != keysSounds[i].Keys)
                            {
                                string path;

                                if (keysSounds[i].Keys.Length > 0 && keysSounds[i].Keys.All(x => x != 0) && keysSounds[i].SoundLocations.Length > 0 && keysSounds[i].SoundLocations.Length > 0 && keysSounds[i].SoundLocations.Any(x => File.Exists(x)))
                                {
                                    Environment.CurrentDirectory = Path.GetDirectoryName(Application.ExecutablePath);

                                    if (keysSounds[i].SoundLocations.Length > 1)
                                    {
                                        int temp;

                                        while (true)
                                        {
                                            temp = rand.Next(0, keysSounds[i].SoundLocations.Length);

                                            if (temp != lastIndex && File.Exists(keysSounds[i].SoundLocations[temp])) break;
                                            Thread.Sleep(1);
                                        }

                                        lastIndex = temp;

                                        path = keysSounds[i].SoundLocations[lastIndex];
                                    }
                                    else
                                    {
                                        path = keysSounds[i].SoundLocations[0];
                                    }

                                    if (File.Exists(path))
                                    {
                                        playSound(path);
                                        keysJustPressed = keysSounds[i].Keys;
                                    }
                                    else if (!showingMsgBox)
                                    {
                                        SystemSounds.Beep.Play();
                                        showingMsgBox = true;
                                        MessageBox.Show("File " + path + " does not exist");
                                        showingMsgBox = false;
                                    }
                                    return;
                                }
                            }
                        }
                        else if (keysJustPressed == keysSounds[i].Keys)
                        {
                            keysJustPressed = null;
                        }
                    }

                    keysPressed = 0;
                }

                if (keysStopSound != null && keysStopSound.Length > 0)
                {
                    for (int i = 0; i < keysStopSound.Length; i++)
                    {
                        if (Helper.IsKeyDown(keysStopSound[i])) keysPressed++;
                    }

                    if (keysPressed == keysStopSound.Length)
                    {
                        if (keysJustPressed == null || !keysJustPressed.Intersect(keysStopSound).Any())
                        {
                            if (playbackWaveOut != null && playbackWaveOut.PlaybackState == NAudio.Wave.PlaybackState.Playing) playbackWaveOut.Stop();

                            keysJustPressed = keysStopSound;

                            return;
                        }
                    }
                    else if (keysJustPressed == keysStopSound)
                    {
                        keysJustPressed = null;
                    }

                    keysPressed = 0;
                }

                if (loadSettingsKeysLoc != null && loadSettingsKeysLoc.Count > 0)
                {
                    for (int i = 0; i < loadSettingsKeysLoc.Count; i++)
                    {
                        keysPressed = 0;

                        for (int j = 0; j < loadSettingsKeysLoc[i].Item1.Length; j++)
                        {
                            if (Helper.IsKeyDown(loadSettingsKeysLoc[i].Item1[j])) keysPressed++;
                        }

                        if (keysPressed == loadSettingsKeysLoc[i].Item1.Length)
                        {
                            if (keysJustPressed == null || !keysJustPressed.Intersect(loadSettingsKeysLoc[i].Item1).Any())
                            {
                                if (!string.IsNullOrWhiteSpace(loadSettingsKeysLoc[i].Item2) && File.Exists(loadSettingsKeysLoc[i].Item2))
                                {
                                    keysJustPressed = loadSettingsKeysLoc[i].Item1;

                                    loadXMLFile(loadSettingsKeysLoc[i].Item2);
                                }

                                return;
                            }
                        }
                        else if (keysJustPressed == loadSettingsKeysLoc[i].Item1)
                        {
                            keysJustPressed = null;
                        }
                    }

                    keysPressed = 0;
                }
            }
        }

        private void cbLoopbackDevices_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbLoopbackDevices.SelectedIndex > 0)
            {
                if (cbEnable.Checked)
                {
                    startLoopback();
                }
            }
            else
            {
                stopLoopback();
            }
        }

        private void cbPlaybackDevices_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (loopbackWaveOut != null && loopbackSourceStream != null && cbEnable.Checked)
            {
                stopLoopback();
                startLoopback();
            }

            if (playbackWaveOut != null && playbackWaveOut.PlaybackState == NAudio.Wave.PlaybackState.Playing)
            {
                stopPlayback();
            }
        }

        private void llBitcoinAddress_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Clipboard.SetText("8e0029c2-9bed-41ee-9ec5-3950fa463fa");

            string bcMarkets = new WebClient().DownloadString(@"http://blockchain.info/ticker");

            string fiveBucksInBitcoin = "";

            if (bcMarkets.Contains("\"15m\" : "))
            {
                int startIndex = bcMarkets.IndexOf("\"15m\" : ") + 8;
                float usdPrice = float.Parse(bcMarkets.Substring(startIndex, bcMarkets.IndexOf(',') - startIndex));
                float bitcoinOfFiveBucks = 1f / (usdPrice / 5f);

                fiveBucksInBitcoin = "\n\n$5 in bitcoin is " + bitcoinOfFiveBucks.ToString();
            }

            MessageBox.Show("My bitcoin address (8e0029c2-9bed-41ee-9ec5-3950fa463fa) has been copied to the clipboard." + fiveBucksInBitcoin);
        }
    }
}
