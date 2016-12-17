using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;

namespace JNSoundboard
{
    public class XMLSettings
    {
        internal static Keys[] keysStopSound = null;
        internal static List<Tuple<Keys[], string>> loadXMLFileKeys = new List<Tuple<Keys[], string>>();
        internal static bool minimizeToTray = true;

        //saving XML files like this makes the XML messy, but it works. if you can un-messy it, please do it and make a pull request :)
        #region Keys and sounds settings
        public class KeysSounds
        {
            public Keys[] Keys;
            public string[] SoundLocations;

            public KeysSounds() { }

            public KeysSounds(Keys[] keys, string[] soundLocs)
            {
                Keys = keys;
                SoundLocations = soundLocs;
            }
        }

        [Serializable]
        public class Settings
        {
            public KeysSounds[] KeysSounds;

            public Settings() { }

            public Settings(KeysSounds[] ks)
            {
                KeysSounds = ks;
            }
        }
        #endregion

        #region Soundboard settings
        public class LoadXMLFile
        {
            public string Keys;
            public string XMLLocation;

            public LoadXMLFile() { }

            public LoadXMLFile(string keys, string xmlLocation)
            {
                Keys = keys;
                XMLLocation = xmlLocation;
            }
        }

        [Serializable]
        public class SoundboardSettings
        {
            public string StopSoundKeys;
            public LoadXMLFile[] LoadXMLFiles;
            public bool MinimizeToTray;

            public SoundboardSettings() { }

            public SoundboardSettings(string stopSoundKeys, LoadXMLFile[] loadXMLFiles, bool minimizeToTray)
            {
                StopSoundKeys = stopSoundKeys;
                LoadXMLFiles = loadXMLFiles;
                MinimizeToTray = minimizeToTray;
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

        internal static void LoadXML()
        {
            string filePath = Path.GetDirectoryName(Application.ExecutablePath) + "\\settings.xml";

            if (File.Exists(filePath))
            {
                var settings = (SoundboardSettings)ReadXML(typeof(SoundboardSettings), filePath);

                if (settings.StopSoundKeys != null)
                {
                    Keys[] keysArr = null;
                    string error = "";

                    if (Helper.keysArrayFromString(settings.StopSoundKeys, out keysArr, out error))
                        keysStopSound = keysArr;
                    else if (error != "Key string \"\" doesn't exist")
                        MessageBox.Show(error);
                }
                else keysStopSound = new Keys[] { };

                if (settings.LoadXMLFiles != null && settings.LoadXMLFiles.Any(x => x.Keys != null && x.Keys.Length > 0 && !string.IsNullOrWhiteSpace(string.Join("", x.Keys)) && !string.IsNullOrWhiteSpace(x.XMLLocation) && File.Exists(x.XMLLocation)))
                {
                    for (int i = 0; i < settings.LoadXMLFiles.Length; i++)
                    {
                        Keys[] keysArr;
                        string error;

                        if (Helper.keysArrayFromString(settings.LoadXMLFiles[i].Keys, out keysArr, out error))
                            loadXMLFileKeys.Add(new Tuple<Keys[], string>(keysArr, settings.LoadXMLFiles[i].XMLLocation));
                    }
                }

                minimizeToTray = settings.MinimizeToTray;
            }
            else
            {
                WriteXML(new SoundboardSettings("", new LoadXMLFile[] { new LoadXMLFile("", "") }, true), filePath);
                keysStopSound = new Keys[] { };
            }
        }
    }
}
