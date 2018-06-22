using System.Windows.Forms;

namespace PorierACTPlugin
{
    public partial class HeaderPanel : Panel
    {
        public HeaderPanel() : base()
        {
            InitializeComponent();
        }

        public virtual void RefreshFeature()
        {
            BackColor = headerBackColor;
            Height = height;
            OnSizeChanged(null);
        }

        protected ColorWrapper headerBackColor;
        protected int height;
        protected Form parentForm;
    }
}