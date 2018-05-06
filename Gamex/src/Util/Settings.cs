using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;
using Gamex.src.Util.Logging;

namespace Gamex.src.Util.Settingsx
{
    public static class Settings
    {
        public static SettingsTree Tree { get; private set; }

        private const string SettingsFileName = "settings";
        private const string SettingsBackupFileName = SettingsFileName + "~";

        static Settings()
        {
            if (Tree == null)
            {
                Load();
            }
        }


        /// <summary>
        /// Attempts to save settings to disk; to the location specified by SettingsFileName
        /// </summary>
        public static void Save()
        {
            using (var filestream = new FileStream(SettingsBackupFileName, FileMode.OpenOrCreate))
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(filestream, Tree);
            }

            File.Delete(SettingsFileName);
            File.Move(SettingsBackupFileName, SettingsFileName);
        }

        /// <summary>
        /// Attepts to load settings from disk; from the location specified by SettingsFileName
        /// Any number of things might go wrong and at the moment we deal with them by reverting to default settings
        /// </summary>
        public static void Load()
        {
            if (!File.Exists(SettingsFileName))
            {
                Logger.Default.Log("Didn't find settings file, using default settings.");
                Tree = new SettingsTree();
                return;
            }

            var formatter = new BinaryFormatter();

            using (var filestream = new FileStream(SettingsFileName, FileMode.Open))
            {
                // i seem to be psychologically unable to copy code even if it's a one liner
                Action neurosis = () => Tree = new SettingsTree();
                try
                {
                    var raw = formatter.Deserialize(filestream);
                    Tree = (SettingsTree)raw;
                }
                catch (TypeInitializationException)
                {
                    // we changed the settings definitions in the softcheese
                    // todo d10 i7 recover
                    neurosis();
                }
                catch (InvalidOperationException)
                {
                    // the settings file is corrupted. overwrite
                    // todo d10i4 attempt to recover the valid settings
                    neurosis();
                }
                catch
                {
                    // we're here either because i can't read or because microshaft fucked something
                    // just default it
                    neurosis();
                }
            }
        }
    }

    [Serializable]
    public class SettingsTree
    {
        [SettingInfo(Comment = "Debug settings")]
        public DebugSettings Debug { get; } = new DebugSettings();
    }

    [Serializable]
    public class DebugSettings
    {
        [SettingInfo(Comment = "The index of the tab shown in the debug window; read on restart")]
        public int TabIndex { get; set; } = 0;

        [SettingInfo(Comment = "Toggles drawing of size boxes")]
        public bool ShowSize { get; set; } = false;
        [SettingInfo(Comment = "Toggles dawing of a marker where the hero is currently moving to")]
        public bool ShowMoveTo { get; set; } = false;
    }

    [System.AttributeUsage(AttributeTargets.Property)]
    public class SettingInfo : Attribute
    {
        public string Comment;

        public string SettingName;
        public Type SettingType;
        public Action<object> SetValueFunctionHandle;
        public Func<object> GetValueFunctionHandle;

        public object Value
        {
            get { return GetValueFunctionHandle?.Invoke(); }
            set { SetValueFunctionHandle?.Invoke(value); }
        }
    }

}

