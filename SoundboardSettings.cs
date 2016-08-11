using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace JNSoundboard
{
    public partial class SoundboardSettings : Form
    {
        internal string keysStopSound = "";
        internal List<XMLSettings.LoadXMLFile> loadSettingsFileKeys = new List<XMLSettings.LoadXMLFile>();

        internal static bool addingOrEditing = false;
        internal int editIndex = -1;
        internal string[] editKeysLoc = null;

        private MainForm mainForm = null;

        public SoundboardSettings()
        {
            InitializeComponent();

            mainForm = Application.OpenForms[0] as MainForm;

            keysStopSound = Helper.keysArrayToString(mainForm.keysStopSound);

            for (int i = 0; i < mainForm.loadSettingsKeysLoc.Count; i++)
            {
                loadSettingsFileKeys.Add(new XMLSettings.LoadXMLFile(Helper.keysArrayToString(mainForm.loadSettingsKeysLoc[i].Item1), mainForm.loadSettingsKeysLoc[i].Item2));
            }

            tbStopSoundKeys.Text = keysStopSound;

            for (int i = 0; i < loadSettingsFileKeys.Count; i++)
            {
                var item = new ListViewItem((loadSettingsFileKeys[i].Keys.Length > 0 ? string.Join("+", loadSettingsFileKeys[i].Keys) : ""));
                item.SubItems.Add(((string.IsNullOrWhiteSpace(loadSettingsFileKeys[i].XMLLocation) || !File.Exists(loadSettingsFileKeys[i].XMLLocation)) ? "" : loadSettingsFileKeys[i].XMLLocation));

                lvKeysLocs.Items.Add(item);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            addingOrEditing = true;

            var form = new AddEditKeysLocation();
            form.ShowDialog();

            addingOrEditing = false;
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (lvKeysLocs.SelectedIndices.Count > 0)
            {
                addingOrEditing = true;
                editIndex = lvKeysLocs.SelectedIndices[0];
                editKeysLoc = new string[] { lvKeysLocs.SelectedItems[0].Text, lvKeysLocs.SelectedItems[0].SubItems[1].Text };

                var form = new AddEditKeysLocation();
                form.ShowDialog();

                editIndex = -1;
                editKeysLoc = null;
                addingOrEditing = false;
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (lvKeysLocs.SelectedIndices.Count > 0 && MessageBox.Show("Are you sure?", "Are you sure?", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                int index = lvKeysLocs.SelectedIndices[0];

                lvKeysLocs.Items.RemoveAt(index);
                loadSettingsFileKeys.RemoveAt(index);
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            Keys[] keysArr = null;
            string error = "";

            if (string.IsNullOrWhiteSpace(tbStopSoundKeys.Text) || Helper.keysArrayFromString(tbStopSoundKeys.Text, out keysArr, out error))
            {
                if (loadSettingsFileKeys.Count == 0 || loadSettingsFileKeys.All(x => x.Keys.Length > 0 && !string.IsNullOrWhiteSpace(x.XMLLocation) && File.Exists(x.XMLLocation)))
                {
                    mainForm.keysStopSound = (keysArr == null ? new Keys[] { } : keysArr);

                    mainForm.loadSettingsKeysLoc.Clear();

                    for (int i = 0; i < loadSettingsFileKeys.Count; i++)
                    {
                        if (Helper.keysArrayFromString(loadSettingsFileKeys[i].Keys, out keysArr, out error))
                        {
                            mainForm.loadSettingsKeysLoc.Add(new Tuple<Keys[], string>(keysArr, loadSettingsFileKeys[i].XMLLocation));
                        }
                    }

                    XMLSettings.WriteXML(new XMLSettings.SoundboardSettings(tbStopSoundKeys.Text, loadSettingsFileKeys.ToArray()), Path.GetDirectoryName(Application.ExecutablePath) + "\\settings.xml");

                    this.Close();
                }
                else MessageBox.Show("One or more entries either have no keys added, the location is empty, or the file the location points to does not exist");
            }
            else if (error != "")
            {
                MessageBox.Show(error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void lvKeysLocs_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnEdit_Click(null, null);
        }

        private void tbStopSoundKeys_Enter(object sender, EventArgs e)
        {
            timer1.Enabled = true;
        }

        private void tbStopSoundKeys_Leave(object sender, EventArgs e)
        {
            timer1.Enabled = false;
        }

        int lastAmountPressed = 0;

        private void timer1_Tick(object sender, EventArgs e)
        {
            int amountPressed = 0;

            if (Helper.IsKeyDown(Keys.Escape) && lastAmountPressed < 2)
            {
                lastAmountPressed = 50;

                tbStopSoundKeys.Text = "";
            }
            else
            {
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
                    tbStopSoundKeys.Text = Helper.keysArrayToString(pressedKeys.ToArray());
                }

                lastAmountPressed = amountPressed;
            }
        }
    }
}
