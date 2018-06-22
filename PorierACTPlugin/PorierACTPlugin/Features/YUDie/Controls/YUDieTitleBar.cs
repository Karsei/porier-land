using System.Drawing;

namespace PorierACTPlugin
{
    public partial class YUDieTitleBar : HeaderPanel
    {
        public Size DesiredSize;

        public YUDieTitleBar() : base()
        {
            InitializeComponent();

            DesiredSize = new Size(0, 0);
        }

        public override void RefreshFeature()
        {
            headerBackColor = ActPlugin.Setting.YUDieSetting.HeaderBackColor;
            height = 25;
            parentForm = ActPlugin.YUDie;

            base.RefreshFeature();

            collapseButton.ForeColor = Color.FromArgb(255 - headerBackColor.R, 255 - headerBackColor.G, 255 - headerBackColor.B);
            collapseButton.Text = ActPlugin.Setting.YUDieSetting.HeaderIsCollapsed ? "▼" : "▲";

            loadButton.ForeColor = Color.FromArgb(255 - headerBackColor.R, 255 - headerBackColor.G, 255 - headerBackColor.B);
        }
    }
}