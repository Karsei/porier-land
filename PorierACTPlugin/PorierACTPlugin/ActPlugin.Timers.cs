namespace PorierACTPlugin
{
    partial class ActPlugin
    {
        private void refreshTimers()
        {
            disableTimers();

            MainTab?.RefreshTimers();
            Overlay?.RefreshTimers();
            YUDie?.RefreshTimers();
        }

        private void disableTimers()
        {
            MainTab?.DisableTimers();
            Overlay?.DisableTimers();
            YUDie?.DisableTimers();
        }
    }
}
