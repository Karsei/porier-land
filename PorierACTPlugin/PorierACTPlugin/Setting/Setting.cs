using Advanced_Combat_Tracker;
using System;
using System.IO;
using System.Xml.Serialization;

namespace PorierACTPlugin
{
    public class Setting : IDisposable
    {
        public bool IsGameMode
        {
            get
            {
                return isGameMode;
            }

            set
            {
                isGameMode = value;
            }
        }
        private bool isGameMode = false;

        public bool IsHiding
        {
            get
            {
                return isHiding;
            }

            set
            {
                isHiding = value;
            }
        }
        private bool isHiding = false;

        public MainTabSetting MainTabSetting
        {
            get
            {
                return mainTabSetting;
            }

            set
            {
                mainTabSetting = value;
            }
        }
        private MainTabSetting mainTabSetting = new MainTabSetting();

        public OverlaySetting OverlaySetting
        {
            get
            {
                return overlaySetting;
            }

            set
            {
                overlaySetting = value;
            }
        }
        private OverlaySetting overlaySetting = new OverlaySetting();

        public YUDieSetting YUDieSetting
        {
            get
            {
                return yUDieSetting;
            }

            set
            {
                yUDieSetting = value;
            }
        }
        private YUDieSetting yUDieSetting = new YUDieSetting();

        public void Dispose()
        {
            SaveSetting();
        }

        #region Save/Load
        public static string SettingPath
        {
            get
            {
                return Path.Combine(ActGlobals.oFormActMain.AppDataFolder.FullName, "Config", "PorierACTPlugin.config.xml");
            }
        }

        public void SaveSetting()
        {
            using (FileStream fs = new FileStream(SettingPath, FileMode.Create, FileAccess.Write, FileShare.ReadWrite))
            {
                new XmlSerializer(typeof(Setting)).Serialize(fs, this);
            }
        }

        public static Setting LoadSetting()
        {
            if (File.Exists(SettingPath))
            {
                using (FileStream fs = new FileStream(SettingPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    try
                    {
                        return (Setting)new XmlSerializer(typeof(Setting)).Deserialize(fs);
                    }
                    catch
                    {

                    }
                }
            }

            return new Setting();
        }
        #endregion
    }
}