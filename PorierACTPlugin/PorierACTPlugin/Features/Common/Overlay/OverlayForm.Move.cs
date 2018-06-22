using System;

namespace PorierACTPlugin
{
    partial class OverlayForm
    {
        protected override void OnMove(EventArgs e)
        {
            base.OnMove(e);

            backgroundForm.Location = Location;

            if (autoSize.EnableAutoSize)
            {
                switch (autoSize.AnchorType)
                {
                    case AnchorType.TopLeft:
                        autoSize.AnchorLocation.SetPoint(Location);
                        break;
                    case AnchorType.TopRight:
                        autoSize.AnchorLocation.SetPoint(Location.AddX(Width));
                        break;
                    case AnchorType.BottomLeft:
                        autoSize.AnchorLocation.SetPoint(Location.AddY(Height));
                        break;
                    case AnchorType.BottomRight:
                        autoSize.AnchorLocation.SetPoint(Location.AddX(Width).AddY(Height));
                        break;
                }
            }
            else
            {
                locationWrapper.SetPoint(Location);
            }
        }
    }
}