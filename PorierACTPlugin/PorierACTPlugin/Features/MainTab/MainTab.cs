using System.Windows.Forms;

namespace PorierACTPlugin
{
    public partial class MainTab : UserControl
    {
        public MainTab() : base()
        {
            InitializeComponent();
        }

        protected override void Dispose(bool disposing)
        {
            DisableHotKeys();
            DisableTimers();

            ActPlugin.MainTab = null;

            base.Dispose(disposing);
        }

        public void RefreshFeature()
        {
            resetSettingsButton.Text = Language.Resource.GetString("RESET_SETTINGS_TEXT");
            editSettingsButton.Text = Language.Resource.GetString("EDIT_SETTINGS_TEXT");
        }
    }
}