using System;

namespace PorierACTPlugin
{
    partial class YUDieHeader
    {
        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);

            clearButton.Height = Height - 2;
            clearButton.Width = clearButton.Height;

            filterButton.Height = Height - 2;
            filterButton.Width = filterButton.Height;

            prevButton.Height = Height - 2;
            prevButton.Width = prevButton.Height;

            nextButton.Height = Height - 2;
            nextButton.Width = nextButton.Height;
            
            prevButton.Left = 0;
            prevButton.Top = 1;

            clearButton.Left = Width - clearButton.Width;
            clearButton.Top = 1;

            filterButton.Left = clearButton.Left - filterButton.Width;
            filterButton.Top = 1;

            nextButton.Left = filterButton.Left - nextButton.Width;
            nextButton.Top = 1;
        }
    }
}