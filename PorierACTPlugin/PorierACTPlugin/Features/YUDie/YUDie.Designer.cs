namespace PorierACTPlugin
{
    partial class YUDie
    {
        private void InitializeComponent()
        {
            yUDiePanel = new YUDiePanel();
            containerPanel.Controls.Add(yUDiePanel);

            yUDieHeader = new YUDieHeader(yUDiePanel);
            containerPanel.Controls.Add(yUDieHeader);

            yUDieTitleBar = new YUDieTitleBar();
            containerPanel.Controls.Add(yUDieTitleBar);
        }

        private YUDieTitleBar yUDieTitleBar;
        private YUDieHeader yUDieHeader;
        private YUDiePanel yUDiePanel;
    }
}