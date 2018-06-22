using System.Drawing;
using System.Windows.Forms;

namespace PorierACTPlugin
{
    public partial class BackgroundForm : Form
    {
        public BackgroundForm() : base()
        {
            InitializeComponent();

            Utility.HideAltTabPreview(Handle);
        }

        public void RefreshFeature(Color backColor, double opacity)
        {
            BackColor = backColor;
            Opacity = opacity;

            Utility.HideAltTabPreview(Handle);
        }
    }
}