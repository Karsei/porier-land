using System;

namespace PorierACTPlugin
{
    partial class MainTab
    {
        private HotKey gameModeHotKey;
        private HotKey hideAllHotKey;

        public void RefreshHotKeys()
        {
            DisableHotKeys();

            gameModeHotKey = new HotKey(ActPlugin.Setting.MainTabSetting.GameModeHotKey);
            gameModeHotKey.KeyPressed += GameModeHotKey_KeyPressed;

            hideAllHotKey = new HotKey(ActPlugin.Setting.MainTabSetting.HideAllHotKey);
            hideAllHotKey.KeyPressed += HideAllHotKey_KeyPressed;
        }

        public void DisableHotKeys()
        {
            gameModeHotKey?.Dispose();
            gameModeHotKey = null;

            hideAllHotKey?.Dispose();
            hideAllHotKey = null;
        }

        private void GameModeHotKey_KeyPressed(object sender, EventArgs e)
        {
            ActPlugin.Setting.IsGameMode = !ActPlugin.Setting.IsGameMode;
            ActPlugin.PorierActPlugin.RefreshGameMode();
        }

        private void HideAllHotKey_KeyPressed(object sender, EventArgs e)
        {
            ActPlugin.Setting.IsHiding = !ActPlugin.Setting.IsHiding;
        }
    }
}