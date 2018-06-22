using System;

namespace PorierACTPlugin
{
    partial class YUDiePanel
    {
        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);

            filterButton.Left = 0;
            filterButton.Top = 0;
        }
    }
}