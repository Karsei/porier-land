using System;

namespace PorierACTPlugin
{
    partial class YUDieHeader
    {
        private void NextButton_Click(object sender, EventArgs e)
        {
            currentIndex = Math.Min(currentIndex + 1, deadCombatants.Count - 1);
            refreshControls();
        }

        private void PrevButton_Click(object sender, EventArgs e)
        {
            currentIndex = Math.Max(currentIndex - 1, 0);
            refreshControls();
        }

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

            currentIndex = -1;
            refreshControls();
        }

        private void ClearButton_Click(object sender, EventArgs e)
        {
            filteredCombatants.Clear();
            deadCombatants.Clear();
            refreshControls();
        }
    }
}