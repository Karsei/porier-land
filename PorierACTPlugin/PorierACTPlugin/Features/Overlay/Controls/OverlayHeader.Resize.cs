using System;

namespace PorierACTPlugin
{
    partial class OverlayHeader
    {
        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);

            if (ActPlugin.Setting.OverlaySetting.HeaderIsCollapsed)
            {
                settingButton.Height = Height - 2;
                collapseButton.Height = Height - 2;
                hideNamesButton.Height = Height - 2;
            }
            else
            {
                settingButton.Height = (int)(Height / 2.0f);
                collapseButton.Height = (int)(Height / 2.0f);
                hideNamesButton.Height = (int)(Height / 2.0f);
            }

            settingButton.Width = settingButton.Height;
            settingButton.Left = Width - settingButton.Width;
            settingButton.Top = 1;

            collapseButton.Width = collapseButton.Height;
            collapseButton.Left = settingButton.Left - collapseButton.Width;
            collapseButton.Top = 1;

            hideNamesButton.Width = hideNamesButton.Height;
            hideNamesButton.Left = collapseButton.Left - hideNamesButton.Width;
            hideNamesButton.Top = 1;
        }
    }
}