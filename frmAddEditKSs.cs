using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace JNSoundboard
{
    public partial class frmAddEditKSs : Form
    {
        internal class ListViewItemComparer : IComparer
        {
            private int col;

            public ListViewItemComparer()
            {
                col = 0;
            }

            public ListViewItemComparer(int column)
            {
                col = column;
            }

            public int Compare(object x, object y)
            {
                return string.Compare(((ListViewItem)x).SubItems[col].Text, ((ListViewItem)y).SubItems[col].Text);
            }
        }

        internal string[] editSoundKeys = null;
        internal int editIndex = -1;

        private frmMain mainForm = null;
        private frmSettings settingsForm = null;

        public frmAddEditKSs()
        {
            InitializeComponent();
        }

        string getRelativePath(string filePathTo, string folderPathFrom)
        {
            if (!folderPathFrom.EndsWith(Path.DirectorySeparatorChar.ToString())) folderPathFrom += Path.DirectorySeparatorChar;

            return Uri.UnescapeDataString(new Uri(folderPathFrom).MakeRelativeUri(new Uri(filePathTo)).ToString().Replace('/', Path.DirectorySeparatorChar));
        }

        private void AddEditSoundKeys_Load(object sender, EventArgs e)
        {
            if (frmSettings.addingOrEditing)
            {
                this.Text = "Add/edit keys and XML location";

                settingsForm = Application.OpenForms[1] as frmSettings;

                if (settingsForm.editIndex != -1)
                {
                    tbKeys.Text = settingsForm.editKeysLoc[0];
                    tbLocation.Text = settingsForm.editKeysLoc[1];
                }
            }
            else
            {
                labelLoc.Text += " (use a semi-colon (;) to seperate multiple locations)";

                mainForm = Application.OpenForms[0] as frmMain;

                if (editIndex != -1)
                {
                    tbKeys.Text = editSoundKeys[0];
                    tbLocation.Text = editSoundKeys[1];
                }
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(tbKeys.Text) && !string.IsNullOrWhiteSpace(tbLocation.Text))
            {
                string[] soundLocs = null;
                string errorMessage = "";

                if (!frmSettings.addingOrEditing)
                {
                    if (!frmSettings.addingOrEditing && Helper.soundLocsArrayFromString(tbLocation.Text, out soundLocs, out errorMessage))
                    {
                        if (soundLocs.Any(x => string.IsNullOrWhiteSpace(x) || !File.Exists(x)))
                        {
                            MessageBox.Show("The file/one of the files does not exist");

                            this.Close();

                            return;
                        }
                    }

                    if (soundLocs == null)
                    {
                        MessageBox.Show(errorMessage);
                        return;
                    }
                }

                Keys[] kKeys;

                if (Helper.keysArrayFromString(tbKeys.Text, out kKeys, out errorMessage))
                {
                    if (frmSettings.addingOrEditing)
                    {
                        if (settingsForm.editIndex != -1)
                        {
                            settingsForm.lvKeysLocs.Items[settingsForm.editIndex].Text = tbKeys.Text;
                            settingsForm.lvKeysLocs.Items[settingsForm.editIndex].SubItems[1].Text = tbLocation.Text;

                            settingsForm.loadSettingsFileKeys[settingsForm.editIndex].Keys = Helper.keysArrayToString(kKeys);
                            settingsForm.loadSettingsFileKeys[settingsForm.editIndex].XMLLocation = tbLocation.Text;
                        }
                        else
                        {
                            var item = new ListViewItem(tbKeys.Text);
                            item.SubItems.Add(tbLocation.Text);

                            settingsForm.lvKeysLocs.Items.Add(item);

                            settingsForm.loadSettingsFileKeys.Add(new XMLSettings.LoadXMLFile(Helper.keysArrayToString(kKeys), tbLocation.Text));
                        }
                    }
                    else
                    {
                        if (editIndex > -1)
                        {
                            mainForm.lvKeySounds.Items[editIndex].Text = tbKeys.Text;
                            mainForm.lvKeySounds.Items[editIndex].SubItems[1].Text = tbLocation.Text;

                            mainForm.keysSounds[editIndex] = new XMLSettings.KeysSounds(kKeys, soundLocs);
                        }
                        else
                        {
                            var newItem = new ListViewItem(tbKeys.Text);
                            newItem.SubItems.Add(tbLocation.Text);

                            mainForm.lvKeySounds.Items.Add(newItem);

                            mainForm.keysSounds.Add(new XMLSettings.KeysSounds(kKeys, soundLocs));
                        }

                        mainForm.lvKeySounds.ListViewItemSorter = new ListViewItemComparer(0);
                        mainForm.lvKeySounds.Sort();

                        mainForm.keysSounds.Sort(delegate (XMLSettings.KeysSounds x, XMLSettings.KeysSounds y)
                        {
                            if (x.Keys == null && y.Keys == null) return 0;
                            else if (x.Keys == null) return -1;
                            else if (y.Keys == null) return 1;
                            else return Helper.keysArrayToString(x.Keys).CompareTo(Helper.keysArrayToString(y.Keys));
                        });

                        mainForm.chKeys.Width = -2;
                        mainForm.chSoundLoc.Width = -2;
                    }
                    this.Close();
                }
                else MessageBox.Show(errorMessage);
            }
            else
            {
                MessageBox.Show("Keys and/or sound location empty");
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnBrowseSoundLoc_Click(object sender, EventArgs e)
        {
            var diag = new OpenFileDialog();

            diag.Multiselect = !frmSettings.addingOrEditing;

            diag.Filter = (frmSettings.addingOrEditing ? "XML file containing keys and sounds|*.xml" : "Supported audio formats|*.mp3;*.m4a;*.wav;*.wma;*.ac3;*.aiff;*.mp2|All files|*.*");

            var result = diag.ShowDialog();

            if (result == DialogResult.OK)
            {
                string text = "";

                for (int i = 0; i < diag.FileNames.Length; i++)
                {
                    string fileName = "";

                    if (frmSettings.addingOrEditing)
                    {
                        fileName = diag.FileNames[i];
                    }
                    else
                    {
                        var msgBoxResult = MessageBox.Show("Click Yes to format as an absolute path, or No to format the path as a relative (to program) path.", "Format", MessageBoxButtons.YesNoCancel);

                        if (msgBoxResult == DialogResult.Yes)
                        {
                            fileName = diag.FileNames[i];
                        }
                        else if (msgBoxResult == DialogResult.No)
                        {
                            fileName = getRelativePath(diag.FileNames[i], Path.GetDirectoryName(Application.ExecutablePath));
                        }
                    }

                    if (fileName != "") text += (i == 0 ? "" : ";") + fileName;
                }

                tbLocation.Text = text;
            }
        }

        private void tbKeys_Enter(object sender, EventArgs e)
        {
            timer1.Enabled = true;
        }

        private void tbKeys_Leave(object sender, EventArgs e)
        {
            timer1.Enabled = false;
        }

        int lastAmountPressed = 0;

        private void timer1_Tick(object sender, EventArgs e)
        {
            int amountPressed = 0;
            var pressedKeys = new List<Keys>();

            foreach (Keys key in Enum.GetValues(typeof(Keys)))
            {
                if (Helper.IsKeyDown(key))
                {
                    amountPressed++;
                    pressedKeys.Add(key);
                }
            }

            if (amountPressed > lastAmountPressed)
            {
                tbKeys.Text = Helper.keysArrayToString(pressedKeys.ToArray());
            }

            lastAmountPressed = amountPressed;
        }
    }
}
