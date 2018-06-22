using System;

namespace PorierACTPlugin
{
    partial class YUDiePanel
    {
        private void FilterButton_Click(object sender, EventArgs e)
        {
            switch (currentFilter)
            {
                case FilterType.All:
                    currentFilter = FilterType.Allies;
                    break;
                case FilterType.Allies:
                    currentFilter = FilterType.Enemies;
                    break;
                case FilterType.Enemies:
                    currentFilter = FilterType.All;
                    break;
            }

            refreshControls();
        }
    }
}