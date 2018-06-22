using System.Windows.Forms;

namespace PorierACTPlugin
{
    public class AnimatedSplitContainer : SplitContainer
    {
        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (ActPlugin.Setting.OverlaySetting.AutoSize.EnableAutoSize) return;

            IsSplitterFixed = true;
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            if (ActPlugin.Setting.OverlaySetting.AutoSize.EnableAutoSize) return;

            IsSplitterFixed = false;
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (ActPlugin.Setting.OverlaySetting.AutoSize.EnableAutoSize) return;
            if (!IsSplitterFixed) return;
            if (e.Button != MouseButtons.Left) return;

            if (e.Y > 0 && e.Y < Height)
            {
                SplitterDistance = e.Y;
                ActPlugin.Setting.OverlaySetting.TableSplitterDistance = e.Y;
                Invalidate();
            }
        }
    }
}