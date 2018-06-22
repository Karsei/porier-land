using System.Windows.Forms;

namespace PorierACTPlugin
{
    public partial class OverlayForm : Form
    {
        public OverlayForm() : base()
        {
            InitializeComponent();

            Utility.HideAltTabPreview(Handle);
        }

        protected override void Dispose(bool disposing)
        {
            backgroundForm?.Dispose();

            base.Dispose(disposing);
        }

        public virtual void RefreshFeature()
        {
            Opacity = opacity;
            
            containerPanel.Padding = new Padding(boundary);
            backgroundForm.RefreshFeature(backgroundFormBackColor, backgroundFormOpacity);

            if (autoSize.EnableAutoSize)
            {
                autoResizeOverlay();
            }
            else
            {
                Location = locationWrapper;
                Size = sizeWrapper;
            }

            Location = Location.ScreenClamp(this);
            Size = Size.ScreenClamp(this);

            Utility.SetClickThrough(Handle, ActPlugin.Setting.IsGameMode);
            Utility.SetClickThrough(backgroundForm.Handle, ActPlugin.Setting.IsGameMode);
        }

        public virtual void ToggleHide()
        {
            Visible = !Visible;
            backgroundForm.Visible = Visible;
        }

        protected double opacity;
        
        protected ColorWrapper backgroundFormBackColor;
        protected double backgroundFormOpacity;

        protected PointWrapper locationWrapper;
        protected SizeWrapper sizeWrapper;

        protected int boundary;

        protected SizeWrapper roundCorner;

        protected AutoSizeWrapper autoSize;
        protected virtual void autoResizeOverlay()
        {
            switch (autoSize.AnchorType)
            {
                case AnchorType.TopLeft:
                    Location = autoSize.AnchorLocation;
                    break;
                case AnchorType.TopRight:
                    Location = autoSize.AnchorLocation.AddX(-1 * Width);
                    break;
                case AnchorType.BottomLeft:
                    Location = autoSize.AnchorLocation.AddY(-1 * Height);
                    break;
                case AnchorType.BottomRight:
                    Location = autoSize.AnchorLocation.AddX(-1 * Width).AddY(-1 * Height);
                    break;
            }
        }
    }
}