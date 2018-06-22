using System;
using System.IO;
using System.Windows.Forms;

namespace PorierACTPlugin
{
    partial class MainTab
    {
        private void ResetSettingsButton_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(Language.Resource.GetString("RESET_SETTINGS_CONFIRM_TEXT"), null, MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                ActPlugin.PorierActPlugin.DisableAll();

                File.Delete(Setting.SettingPath);
                ActPlugin.Setting = Setting.LoadSetting();

                ActPlugin.PorierActPlugin.RefreshAll();
            }
        }

        private void EditSettingsButton_Click(object sender, EventArgs e)
        {
            ActPlugin.PorierActPlugin.DisableAll();

            SettingForm settingForm = new SettingForm(ActPlugin.Setting.MainTabSetting);
            settingForm.ShowDialog();

            ActPlugin.PorierActPlugin.RefreshAll();
        }
    }
}