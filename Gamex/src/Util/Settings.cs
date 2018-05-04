using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;
using Gamex.src.Util.Logging;

namespace Gamex.src.Util
{
    public static class Settings
    {
        public static SettingsTree SettingsTree { get; private set; }

        private const string SettingsFileName = "settings";
        private const string SettingsBackupFileName = SettingsFileName + "~";

        static Settings()
        {
            if (SettingsTree == null)
            {
                Load();
            }
        }

        public static void Save()
        {
            using (var filestream = new FileStream(SettingsBackupFileName, FileMode.OpenOrCreate))
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(filestream, SettingsTree);
            }

            File.Delete(SettingsFileName);
            File.Move(SettingsBackupFileName, SettingsFileName);
        }

        public static void Load()
        {
            if (!File.Exists(SettingsFileName))
            {
                Logger.Default.Log("Didn't find settings file, using default settings.");
                SettingsTree = new SettingsTree();
                return;
            }

            var formatter = new BinaryFormatter();

            using (var filestream = new FileStream(SettingsFileName, FileMode.Open))
            {
                try
                {
                    var raw = formatter.Deserialize(filestream);
                    SettingsTree = (SettingsTree)raw;
                }
                catch (InvalidOperationException e)
                {
                    // the settings file is corrupted. overwrite
                    // todo d10i4 attempt to recover the valid settings
                    SettingsTree = new SettingsTree();
                }
            }
        }
    }

    [Serializable]
    public class SettingsTree
    {
        public DebugSettings Debug { get; } = new DebugSettings();
    }

    [Serializable]
    public class DebugSettings
    {
        public int TabIndex { get; set; }

        public bool ShowSize { get; set; }
        public bool ShowMoveTo { get; set; }
    }
}

