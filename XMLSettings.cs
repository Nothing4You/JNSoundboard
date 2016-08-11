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

            public SoundboardSettings() { }

            public SoundboardSettings(string stopSoundKeys, LoadXMLFile[] loadXMLFiles)
            {
                StopSoundKeys = stopSoundKeys;
                LoadXMLFiles = loadXMLFiles;
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
                    return (object)serializer.Deserialize(reader);
                }
                else return null;
            }
        }
    }
}
