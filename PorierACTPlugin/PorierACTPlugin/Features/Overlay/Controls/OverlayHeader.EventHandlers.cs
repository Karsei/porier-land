using System;
using System.Windows.Forms;

namespace PorierACTPlugin
{
    partial class OverlayHeader
    {
        private void SettingButton_Click(object sender, EventArgs e)
        {
            ActPlugin.PorierActPlugin.DisableAll();

            SettingForm settingForm = new SettingForm(ActPlugin.Setting.OverlaySetting);
            settingForm.SetPropertyValueChanged(SettingForm_PropertyValueChanged);
            settingForm.ShowDialog();

            ActPlugin.PorierActPlugin.RefreshHotKeysAndTimers();
            ActPlugin.Overlay.RefreshFeature();
        }

        private void SettingForm_PropertyValueChanged(object sender, PropertyValueChangedEventArgs e)
        {
            ActPlugin.Overlay.RefreshFeature();
        }

        private void CollapseButton_Click(object sender, EventArgs e)
        {
            ActPlugin.Setting.OverlaySetting.HeaderIsCollapsed = !ActPlugin.Setting.OverlaySetting.HeaderIsCollapsed;
            RefreshFeature();
        }

        private void HideNamesButton_Click(object sender, EventArgs e)
        {
            ActPlugin.Overlay.ToggleHideNames();
        }
    }
}