using System;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;

namespace JNSoundboard
{
    public class XMLSettings
    {
        readonly static SoundboardSettings DEFAULT_SOUNDBOARD_SETTINGS = new SoundboardSettings(new Keys[] { }, new LoadXMLFile[] { new LoadXMLFile(new Keys[] { }, "") }, true, false, "", "");

        internal static SoundboardSettings soundboardSettings = new SoundboardSettings();

        //saving XML files like this makes the XML messy, but it works
        #region Keys and sounds settings
        public class SoundHotkey
        {
            public Keys[] Keys;
            public string WindowTitle;
            public string[] SoundLocations;

            public SoundHotkey() { }

            public SoundHotkey(Keys[] keys, string windowTitle, string[] soundLocs)
            {
                Keys = keys;
                WindowTitle = windowTitle;
                SoundLocations = soundLocs;
            }
        }

        [Serializable]
        public class Settings
        {
            public SoundHotkey[] SoundHotkeys;

            public Settings() { }

            public Settings(SoundHotkey[] sh)
            {
                SoundHotkeys = sh;
            }
        }
        #endregion

        #region Soundboard settings
        public class LoadXMLFile
        {
            public Keys[] Keys;
            public string XMLLocation;

            public LoadXMLFile() { }

            public LoadXMLFile(Keys[] keys, string xmlLocation)
            {
                Keys = keys;
                XMLLocation = xmlLocation;
            }
        }

        [Serializable]
        public class SoundboardSettings
        {
            public Keys[] StopSoundKeys;
            public LoadXMLFile[] LoadXMLFiles;
            public bool MinimizeToTray, PlaySoundsOverEachOther;
            public string LastPlaybackDevice, LastLoopbackDevice;

            public SoundboardSettings() { }

            public SoundboardSettings(Keys[] stopSoundKeys, LoadXMLFile[] loadXMLFiles, bool minimizeToTray, bool playSoundsOverEachOther, string lastPlaybackDevice, string lastLoopbackDevice)
            {
                StopSoundKeys = stopSoundKeys;
                LoadXMLFiles = loadXMLFiles;
                MinimizeToTray = minimizeToTray;
                PlaySoundsOverEachOther = playSoundsOverEachOther;
                LastPlaybackDevice = lastPlaybackDevice;
                LastLoopbackDevice = lastLoopbackDevice;
            }
        }
        #endregion

        internal static void WriteXML(object kl, string xmlLoc)
        {
            XmlSerializer serializer = new XmlSerializer(kl.GetType());

            using (MemoryStream memStream = new MemoryStream())
            {
                using (StreamWriter stream = new StreamWriter(memStream, Encoding.Unicode))
                {
                    var settings = new XmlWriterSettings();
                    settings.Indent = true;
                    settings.OmitXmlDeclaration = true;

                    using (var writer = XmlWriter.Create(stream, settings))
                    {
                        var emptyNamepsaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });
                        serializer.Serialize(writer, kl, emptyNamepsaces);

                        int count = (int)memStream.Length;

                        byte[] arr = new byte[count];
                        memStream.Seek(0, SeekOrigin.Begin);

                        memStream.Read(arr, 0, count);

                        using (BinaryWriter binWriter = new BinaryWriter(File.Open(xmlLoc, FileMode.Create)))
                        {
                            binWriter.Write(arr);
                        }
                    }
                }
            }
        }

        internal static object ReadXML(Type type, string xmlLoc)
        {
            var serializer = new XmlSerializer(type);

            using (var reader = XmlReader.Create(xmlLoc))
            {
                if (serializer.CanDeserialize(reader))
                {
                    return serializer.Deserialize(reader);
                }
                else return null;
            }
        }

        internal static void SaveSoundboardSettingsXML()
        {
            WriteXML(soundboardSettings, Path.GetDirectoryName(Application.ExecutablePath) + "\\settings.xml");
        }

        internal static void LoadSoundboardSettingsXML()
        {
            string filePath = Path.GetDirectoryName(Application.ExecutablePath) + "\\settings.xml";

            if (File.Exists(filePath))
            {
                SoundboardSettings settings;

                try
                {
                    settings = (SoundboardSettings)ReadXML(typeof(SoundboardSettings), filePath);
                }
                catch
                {
                    soundboardSettings = DEFAULT_SOUNDBOARD_SETTINGS;
                    return;
                }

                if (settings == null)
                {
                    soundboardSettings = DEFAULT_SOUNDBOARD_SETTINGS;
                    return;
                }

                if (settings.StopSoundKeys == null) settings.StopSoundKeys = new Keys[] { };

                if (settings.LoadXMLFiles == null) settings.LoadXMLFiles = new LoadXMLFile[] { };

                if (settings.LastPlaybackDevice == null) settings.LastPlaybackDevice = "";

                if (settings.LastLoopbackDevice == null) settings.LastLoopbackDevice = "";

                soundboardSettings = settings;
            }
            else
            {
                WriteXML(DEFAULT_SOUNDBOARD_SETTINGS, filePath);
            }
        }
    }
}
