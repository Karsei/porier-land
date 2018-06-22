namespace PorierACTPlugin
{
    partial class ActPlugin
    {
        private void refreshHotKeys()
        {
            disableHotKeys();

            MainTab?.RefreshHotKeys();
            Overlay?.RefreshHotKeys();
            YUDie?.RefreshHotKeys();
        }

        private void disableHotKeys()
        {
            MainTab?.DisableHotKeys();
            Overlay?.DisableHotKeys();
            YUDie?.DisableHotKeys();
        }
    }
}
