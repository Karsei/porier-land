using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace PorierACTPlugin
{
    public partial class YUDiePanel : Panel
    {
        public Size DesiredSize;

        List<Label> labels = new List<Label>();
        Combatant combatant;
        private FilterType currentFilter = FilterType.All;

        public YUDiePanel() : base()
        {
            InitializeComponent();

            DesiredSize = new Size(0, 50);
        }

        public void RefreshFeature()
        {
            foreach (Label label in labels)
            {
                label.ForeColor = ActPlugin.Setting.YUDieSetting.PanelText.ColorWrapper;
                label.Font = ActPlugin.Setting.YUDieSetting.PanelText.FontWrapper;
            }
        }

        public void SetCombatant(Combatant combatant)
        {
            this.combatant = combatant;
            refreshControls();
        }

        private void refreshControls()
        {
            DesiredSize = new Size(0, 50);

            foreach (Label label in labels)
            {
                label.Dispose();
            }

            if (combatant != null)
            {
                int curHeight = filterButton.Height;

                Label lastInflictedSkill = new Label
                {
                    AutoSize = true,
                    Font = ActPlugin.Setting.YUDieSetting.PanelText.FontWrapper,
                    ForeColor = ActPlugin.Setting.YUDieSetting.PanelText.ColorWrapper,
                    Left = 5,
                    Text = Language.Resource.GetString("YUDIE_PANEL_LAST_SKILLS"),
                    Top = curHeight
                };
                labels.Add(lastInflictedSkill);
                Controls.Add(lastInflictedSkill);

                curHeight += lastInflictedSkill.Height + 5;
                DesiredSize.Width = Math.Max(DesiredSize.Width, lastInflictedSkill.Width + 10);

                string[] lastSkills = combatant.GetSkills(currentFilter);
                foreach (string lastSkill in lastSkills)
                {
                    Label skillLabel = new Label
                    {
                        AutoSize = true,
                        Font = ActPlugin.Setting.YUDieSetting.PanelText.FontWrapper,
                        ForeColor = ActPlugin.Setting.YUDieSetting.PanelText.ColorWrapper,
                        Left = 10,
                        Text = lastSkill,
                        Top = curHeight
                    };
                    labels.Add(skillLabel);
                    Controls.Add(skillLabel);

                    curHeight += skillLabel.Height + 5;
                    DesiredSize.Width = Math.Max(DesiredSize.Width, skillLabel.Width + 15);
                }

                curHeight += 5;

                Label buffTitleLabel = new Label
                {
                    AutoSize = true,
                    Font = ActPlugin.Setting.YUDieSetting.PanelText.FontWrapper,
                    ForeColor = ActPlugin.Setting.YUDieSetting.PanelText.ColorWrapper,
                    Left = 5,
                    Text = Language.Resource.GetString("YUDIE_PANEL_BUFFS"),
                    Top = curHeight
                };
                labels.Add(buffTitleLabel);
                Controls.Add(buffTitleLabel);

                curHeight += buffTitleLabel.Height + 5;
                DesiredSize.Width = Math.Max(DesiredSize.Width, buffTitleLabel.Width + 10);

                string[] statusEffects = combatant.GetStatusEffects();
                foreach (string statusEffect in statusEffects)
                {
                    Label buffLabel = new Label
                    {
                        AutoSize = true,
                        Font = ActPlugin.Setting.YUDieSetting.PanelText.FontWrapper,
                        ForeColor = ActPlugin.Setting.YUDieSetting.PanelText.ColorWrapper,
                        Left = 10,
                        Text = statusEffect,
                        Top = curHeight
                    };
                    labels.Add(buffLabel);
                    Controls.Add(buffLabel);

                    curHeight += buffLabel.Height + 5;
                    DesiredSize.Width = Math.Max(DesiredSize.Width, buffLabel.Width + 15);
                }

                DesiredSize.Height = curHeight;
            }

            if (ActPlugin.Setting.YUDieSetting.AutoSize.EnableAutoSize) ActPlugin.YUDie.AutoResize();
        }
    }
}