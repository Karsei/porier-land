using Advanced_Combat_Tracker;
using System;
using System.Windows.Forms;

namespace PorierACTPlugin
{
    partial class YUDieTitleBar
    {
        private void SettingButton_Click(object sender, EventArgs e)
        {
            ActPlugin.PorierActPlugin.DisableAll();

            SettingForm settingForm = new SettingForm(ActPlugin.Setting.YUDieSetting);
            settingForm.SetPropertyValueChanged(SettingForm_PropertyValueChanged);
            settingForm.ShowDialog();

            ActPlugin.PorierActPlugin.RefreshHotKeysAndTimers();
            ActPlugin.YUDie.RefreshFeature();
        }

        private void SettingForm_PropertyValueChanged(object sender, PropertyValueChangedEventArgs e)
        {
            ActPlugin.YUDie.RefreshFeature();
        }

        private void LoadButton_Click(object sender, EventArgs e)
        {
            if (ActGlobals.oFormActMain.InCombat) return;

            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                CheckFileExists = true,
                CheckPathExists = true,
                DefaultExt = ".log",
                Multiselect = false
            };
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                ActPlugin.YUDie.LoadLog(openFileDialog.FileName);
            }
        }

        private void CollapseButton_Click(object sender, EventArgs e)
        {
            ActPlugin.Setting.YUDieSetting.HeaderIsCollapsed = !ActPlugin.Setting.YUDieSetting.HeaderIsCollapsed;
            ActPlugin.YUDie.RefreshFeature();
        }
    }
}