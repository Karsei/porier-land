using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace PorierACTPlugin
{
    public partial class YUDieHeader : Panel
    {
        public Size DesiredSize;

        private List<Combatant> deadCombatants = new List<Combatant>();
        private ICollection<string> allies;
        private List<Combatant> filteredCombatants = new List<Combatant>();

        private FilterType currentFilter = FilterType.All;

        private int currentIndex = -1;

        private YUDiePanel yUDiePanel;

        public YUDieHeader(YUDiePanel yUDiePanel) : base()
        {
            InitializeComponent();

            this.yUDiePanel = yUDiePanel;

            DesiredSize = new Size(0, 0);
        }

        public void RefreshFeature()
        {
            BackColor = ActPlugin.Setting.YUDieSetting.HeaderBackColor;
            Height = ActPlugin.Setting.YUDieSetting.HeaderHeight;

            nameLabel.ForeColor = ActPlugin.Setting.YUDieSetting.HeaderText.ColorWrapper;
            nameLabel.Font = ActPlugin.Setting.YUDieSetting.HeaderText.FontWrapper;
            
            OnSizeChanged(null);

            refreshControls();
        }

        public void SetDeadCombatants(List<Combatant> deadCombatants, ICollection<string> allies)
        {
            this.deadCombatants = deadCombatants;
            this.allies = allies;
            currentIndex = -1;

            refreshControls();
        }

        private void refreshControls()
        {
            filteredCombatants.Clear();

            switch (currentFilter)
            {
                case FilterType.All:
                    deadCombatants.ForEach(x => filteredCombatants.Add(x));
                    break;
                case FilterType.Allies:
                    filteredCombatants = deadCombatants.FindAll(x => allies.Contains(x.Name));
                    break;
                case FilterType.Enemies:
                    filteredCombatants = deadCombatants.FindAll(x => !allies.Contains(x.Name));
                    break;
            }

            if (currentIndex < 0 || currentIndex > filteredCombatants.Count - 1)
            {
                currentIndex = filteredCombatants.Count - 1;
            }

            Combatant targetCombatant = null;

            if (filteredCombatants.Count == 0)
            {
                prevButton.Enabled = false;
                nextButton.Enabled = false;
                nameLabel.Text = "-";
            }
            else
            {
                prevButton.Enabled = currentIndex > 0;
                nextButton.Enabled = currentIndex < filteredCombatants.Count - 1;
                nameLabel.Text = filteredCombatants[currentIndex].ToString();
                targetCombatant = filteredCombatants[currentIndex];
            }

            DesiredSize.Width = prevButton.Width + nameLabel.Width + nextButton.Width + filterButton.Width + clearButton.Width;
            
            yUDiePanel.SetCombatant(targetCombatant);

            nameLabel.Left = ((int)((Width - DesiredSize.Width) / 2f)) + prevButton.Width;
            nameLabel.Top = (int)((Height - nameLabel.Height) / 2f);
        }
    }
}