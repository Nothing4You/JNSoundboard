using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace JNSoundboard
{
    public partial class SettingsForm : Form
    {
        internal List<XMLSettings.LoadXMLFile> loadXMLFilesList = new List<XMLSettings.LoadXMLFile>(XMLSettings.soundboardSettings.LoadXMLFiles); //list so can dynamically add/remove

        internal static bool addingEditingLoadXMLFile = false;

        public SettingsForm()
        {
            InitializeComponent();
                        
            for (int i = 0; i < loadXMLFilesList.Count; i++)
            {
                bool keysLengthCorrect = loadXMLFilesList[i].Keys.Length > 0;
                bool xmlLocationUnempty = !string.IsNullOrWhiteSpace(loadXMLFilesList[i].XMLLocation);

                if (!keysLengthCorrect && !xmlLocationUnempty) //remove if empty
                {
                    loadXMLFilesList.RemoveAt(i);
                    i--;
                    continue;
                }

                var item = new ListViewItem((keysLengthCorrect ? string.Join("+", loadXMLFilesList[i].Keys) : ""));
                item.SubItems.Add((xmlLocationUnempty ? loadXMLFilesList[i].XMLLocation : ""));

                lvKeysLocs.Items.Add(item);
            }

            tbStopSoundKeys.Text = Helper.keysToString(XMLSettings.soundboardSettings.StopSoundKeys);

            cbMinimizeToTray.Checked = XMLSettings.soundboardSettings.MinimizeToTray;

            cbPlaySoundsOverEachOther.Checked = XMLSettings.soundboardSettings.PlaySoundsOverEachOther;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            addingEditingLoadXMLFile = true;

            var form = new AddEditHotkeyForm();
            form.ShowDialog();

            addingEditingLoadXMLFile = false;
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (lvKeysLocs.SelectedIndices.Count > 0)
            {
                addingEditingLoadXMLFile = true;

                var form = new AddEditHotkeyForm();

                form.editIndex = lvKeysLocs.SelectedIndices[0];
                form.editStrings = new string[] { lvKeysLocs.SelectedItems[0].Text, lvKeysLocs.SelectedItems[0].SubItems[1].Text };

                form.ShowDialog();

                addingEditingLoadXMLFile = false;
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (lvKeysLocs.SelectedIndices.Count > 0 && MessageBox.Show("Are you sure?", "Are you sure?", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                int index = lvKeysLocs.SelectedIndices[0];

                lvKeysLocs.Items.RemoveAt(index);
                loadXMLFilesList.RemoveAt(index);
            }
        }
        
        private void btnOK_Click(object sender, EventArgs e)
        {
            Keys[] keysArr = null;
            string error = "";

            if (string.IsNullOrWhiteSpace(tbStopSoundKeys.Text) || Helper.keysArrayFromString(tbStopSoundKeys.Text, out keysArr, out error))
            {
                if (loadXMLFilesList.Count == 0 || loadXMLFilesList.All(x => x.Keys.Length > 0 && !string.IsNullOrWhiteSpace(x.XMLLocation) && File.Exists(x.XMLLocation)))
                {
                    XMLSettings.soundboardSettings.StopSoundKeys = (keysArr == null ? new Keys[] { } : keysArr);

                    XMLSettings.soundboardSettings.LoadXMLFiles = loadXMLFilesList.ToArray();

                    XMLSettings.soundboardSettings.MinimizeToTray = cbMinimizeToTray.Checked;

                    XMLSettings.soundboardSettings.PlaySoundsOverEachOther = cbPlaySoundsOverEachOther.Checked;

                    XMLSettings.SaveSoundboardSettingsXML();

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

            if (Keyboard.IsKeyDown(Keys.Escape))
            {
                lastAmountPressed = 50;

                tbStopSoundKeys.Text = "";
            }
            else
            {
                var pressedKeys = new List<Keys>();

                foreach (Keys key in Enum.GetValues(typeof(Keys)))
                {
                    if (Keyboard.IsKeyDown(key))
                    {
                        amountPressed++;
                        pressedKeys.Add(key);
                    }
                }

                if (amountPressed > lastAmountPressed)
                {
                    tbStopSoundKeys.Text = Helper.keysToString(pressedKeys.ToArray());
                }

                lastAmountPressed = amountPressed;
            }
        }
    }
}
