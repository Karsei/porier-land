using Advanced_Combat_Tracker;
using System;
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Windows.Forms;

namespace PorierACTPlugin
{
    public partial class ActPlugin : IActPluginV1
    {
        const string VERSION_NUMBER = "1.2";

        TabPage pluginScreenSpace;
        Label pluginStatusText;

        public static ActPlugin PorierActPlugin;
        public static Setting Setting;
        public static MainTab MainTab;
        public static Overlay Overlay;
        public static YUDie YUDie;

        private void OFormActMain_UpdateCheckClicked()
        {
            try
            {
                using (HttpClient httpClient = new HttpClient(new HttpClientHandler
                {
                    AllowAutoRedirect = true
                }))
                {
                    string versionNumber = httpClient.GetStringAsync("https://porier-land.tk/api/act_plugin/version").Result;

                    if (versionNumber != VERSION_NUMBER)
                    {
                        if (MessageBox.Show(
                            string.Format(Language.Resource.GetString("UPDATE_QUESTION"), VERSION_NUMBER, versionNumber),
                            Language.Resource.GetString("UPDATE_TITLE"),
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            string latestUrl = httpClient.GetStringAsync("https://porier-land.tk/api/act_plugin/latest").Result;
                            //string latestUrl = httpClient.GetStringAsync("https://porier-land.tk/api/act_plugin/latest_portable").Result;

                            byte[] latestBinary = httpClient.GetByteArrayAsync(latestUrl).Result;

                            ActPluginData pluginData = ActGlobals.oFormActMain.PluginGetSelfData(this);
                            pluginData.pluginFile.Delete();
                            File.WriteAllBytes(pluginData.pluginFile.FullName, latestBinary);
                            ThreadInvokes.CheckboxSetChecked(ActGlobals.oFormActMain, pluginData.cbEnabled, false);
                            Application.DoEvents();
                            ThreadInvokes.CheckboxSetChecked(ActGlobals.oFormActMain, pluginData.cbEnabled, true);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(Language.Resource.GetString("UPDATE_EXCEPTION"));
            }
        }

        public ActPlugin() : base()
        {
            PorierActPlugin = this;
        }

        public void InitPlugin(TabPage pluginScreenSpace, Label pluginStatusText)
        {
            new Thread(new ThreadStart(OFormActMain_UpdateCheckClicked)).Start();

            this.pluginScreenSpace = pluginScreenSpace;
            this.pluginStatusText = pluginStatusText;

            Setting = Setting.LoadSetting();
            MainTab = new MainTab();
            pluginScreenSpace.Controls.Add(MainTab);

            RefreshAll();
        }

        public void DeInitPlugin()
        {
            Overlay?.Dispose();
            YUDie?.Dispose();
            MainTab?.Dispose();
            Setting?.Dispose();

            pluginStatusText.Text = Language.Resource.GetString("PLUGIN_DISABLED");
        }

        public void DisableAll()
        {
            disableHotKeys();
            disableTimers();
        }

        public void RefreshAll()
        {
            refreshFeature();
            refreshHotKeys();
            refreshTimers();
        }

        public void RefreshHotKeysAndTimers()
        {
            refreshHotKeys();
            refreshTimers();
        }

        public void RefreshGameMode()
        {
            Overlay?.RefreshFeature();
            YUDie?.RefreshFeature();
        }

        public void HideAll()
        {
            Overlay?.ToggleHide();
            YUDie?.ToggleHide();
        }

        private void refreshFeature()
        {
            Language.Resource.CultureName = Setting.MainTabSetting.CultureName;

            pluginScreenSpace.Text = Language.Resource.GetString("PLUGIN_NAME");
            pluginStatusText.Text = Language.Resource.GetString("PLUGIN_STARTED");

            MainTab.RefreshFeature();

            Overlay?.Dispose();
            Overlay = null;

            if (Setting.MainTabSetting.ShowOverlay)
            {
                try
                {
                    Overlay = new Overlay();
                    Overlay.Show();
                    Overlay.RefreshFeature();
                }
                catch (Exception e)
                {
                    Setting.MainTabSetting.ShowOverlay = false;
                    Overlay?.Dispose();
                    Overlay = null;
                    
                    MessageBox.Show(Language.Resource.GetString("OVERLAY_EXCEPTION"));
                }
            }

            YUDie?.Dispose();
            YUDie = null;

            if (Setting.MainTabSetting.ShowYUDie)
            {
                try
                {
                    YUDie = new YUDie();
                    YUDie.Show();
                    YUDie.RefreshFeature();
                }
                catch (Exception e)
                {
                    Setting.MainTabSetting.ShowYUDie = false;
                    YUDie?.Dispose();
                    YUDie = null;

                    MessageBox.Show(Language.Resource.GetString("OVERLAY_EXCEPTION"));
                }
            }
        }
    }
}