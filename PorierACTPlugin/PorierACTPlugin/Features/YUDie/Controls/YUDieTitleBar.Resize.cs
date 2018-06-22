using System;

namespace PorierACTPlugin
{
    partial class YUDieTitleBar
    {
        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);

            settingButton.Height = Height - 2;
            settingButton.Width = settingButton.Height;
            settingButton.Left = Width - settingButton.Width;
            settingButton.Top = 1;

            collapseButton.Height = Height - 2;
            collapseButton.Width = collapseButton.Height;
            collapseButton.Left = settingButton.Left - collapseButton.Width;
            collapseButton.Top = 1;

            loadButton.Height = Height - 2;
            loadButton.Width = loadButton.Height;
            loadButton.Left = collapseButton.Left - loadButton.Width;
            loadButton.Top = 1;

            DesiredSize.Width = settingButton.Width + collapseButton.Width + loadButton.Width + 10;
        }
    }
}