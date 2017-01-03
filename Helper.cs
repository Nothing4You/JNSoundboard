using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace JNSoundboard
{
    internal class Helper
    {
        [DllImport("user32.dll", SetLastError = true)]
        private static extern short GetKeyState(ushort virtualKeyCode);

        internal static bool IsKeyDown(Keys keyCode)
        {
            short keyState = GetKeyState((ushort)keyCode);
            return keyState < 0;
        }

        internal static string userGetXMLLoc()
        {
            SaveFileDialog diag = new SaveFileDialog();

            diag.Filter = "XML file containing keys and sounds|*.xml";

            var result = diag.ShowDialog();

            if (result == DialogResult.OK)
            {
                return diag.FileName;
            }
            else return "";
        }

        internal static string[] keysArrayToStringArray(Keys[] keysArr)
        {
            var arr = new List<string>();

            for (int i = 0; i < keysArr.Length; i++)
            {
                arr.Add(keysArr[i].ToString());
            }

            return arr.ToArray();
        }

        internal static Keys[] stringArrayToKeysArray(string[] strArr)
        {
            if (strArr == null) return new Keys[] { Keys.NoName };
            var arr = new List<Keys>();

            for (int i = 0; i < strArr.Length; i++)
            {
                Keys key;

                if (Enum.TryParse(strArr[i], out key))
                {
                    arr.Add(key);
                }
                else
                {
                    return new Keys[] { Keys.NoName };
                }
            }

            return arr.ToArray();
        }

        internal static bool keysArrayFromString(string key, out Keys[] keysArr, out string errorMessage)
        {
            if (key.Contains("+"))
            {
                string[] sKeys = key.Split('+');
                var kKeys = new List<Keys>();

                for (int i = 0; i < sKeys.Length; i++)
                {
                    Keys kKey;

                    if (Enum.TryParse(sKeys[i], out kKey))
                    {
                        kKeys.Add(kKey);
                    }
                    else
                    {
                        errorMessage = "Key string \"" + sKeys[i] + "\" doesn't exist";
                        keysArr = null;
                        return false;
                    }
                }

                keysArr = kKeys.ToArray();
                errorMessage = string.Empty;
                return true;
            }
            else
            {
                Keys kKey;

                if (Enum.TryParse(key, out kKey))
                {
                    keysArr = new Keys[] { kKey };
                    errorMessage = string.Empty;
                    return true;
                }
                else
                {
                    errorMessage = "Key string \"" + key + "\" doesn't exist";
                    keysArr = null;
                    return false;
                }
            }
        }

        internal static string keysArrayToString(Keys[] keysArr)
        {
            if (keysArr == null) return "";
            string temp = "";
            int kLen = keysArr.Length;

            for (int i = 0; i < kLen; i++)
            {
                temp += keysArr[i].ToString() + (i == kLen - 1 ? "" : "+");
            }

            return temp;
        }

        internal static bool soundLocsArrayFromString(string soundLocsStr, out string[] soundLocs, out string errorMessage)
        {
            if (soundLocsStr.Contains(";"))
            {
                string[] sLocs = soundLocsStr.Split(';');
                var lLocs = new List<string>();

                for (int i = 0; i < sLocs.Length; i++)
                {
                    if (File.Exists(sLocs[i]))
                    {
                        lLocs.Add(sLocs[i]);
                    }
                    else
                    {
                        errorMessage = "File \"" + sLocs[i] + "\" does not exist";
                        soundLocs = null;
                        return false;
                    }
                }

                soundLocs = lLocs.ToArray();
                errorMessage = string.Empty;
                return true;
            }
            else
            {
                if (File.Exists(soundLocsStr))
                {
                    soundLocs = new string[] { soundLocsStr };
                    errorMessage = string.Empty;
                    return true;
                }
                else
                {
                    errorMessage = "File \"" + soundLocsStr + "\" does not exist";
                    soundLocs = null;
                    return false;
                }
            }
        }

        internal static string soundLocsArrayToString(string[] soundLocations)
        {
            string temp = "";
            int sLen = soundLocations.Length;

            for (int i = 0; i < sLen; i++)
            {
                temp += soundLocations[i].ToString() + (i == sLen - 1 ? "" : ";");
            }

            return temp;
        }

        internal static string cleanFileName(string fileName)
        {
            return Path.GetInvalidFileNameChars().Aggregate(fileName, (current, c) => current.Replace(c.ToString(), string.Empty));
        }
    }
}
