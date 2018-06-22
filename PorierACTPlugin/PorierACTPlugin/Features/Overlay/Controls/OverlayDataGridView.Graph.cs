using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace PorierACTPlugin
{
    partial class OverlayDataGridView
    {
        protected override void OnRowPrePaint(DataGridViewRowPrePaintEventArgs e)
        {
            base.OnRowPrePaint(e);

            e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(1, 1, 1)), e.RowBounds);

            if (!setting.ShowGraph) return;
            if (string.IsNullOrEmpty(setting.CurrentGraphDataPropertyName)) return;

            OverlayDataGridViewItem currentItem = (OverlayDataGridViewItem)Rows[e.RowIndex].DataBoundItem;

            IEnumerable<OverlayDataGridViewItem> filteredItems =
                setting.IncludeLimitInSorting ?
                    backingSource :
                    backingSource.Where(x => x.Job != "LIMIT");

            int width = 0;
            int overHealWidth = 0;
            int petWidth = 0;

            switch (setting.CurrentGraphDataPropertyName)
            {
                case "TotalDPS":
                    double topTotalDPS = filteredItems.Max(x => x.PetDPS + x.EncDPS);
                    width = (int)(e.RowBounds.Width * currentItem.EncDPS / topTotalDPS);
                    petWidth = (int)(e.RowBounds.Width * currentItem.PetDPS / topTotalDPS);
                    break;
                case "TotalHPS":
                    double topTotalHPS = filteredItems.Max(x => x.PetHPS + x.EncHPS);
                    double overHealHPS = currentItem.EncHPS * (currentItem.OverHeal + currentItem.PetOverHeal) / (currentItem.Healed + currentItem.PetHealed);
                    double validHealHPS = currentItem.EncHPS - overHealHPS;
                    width = (int)(e.RowBounds.Width * validHealHPS / topTotalHPS);
                    overHealWidth = (int)(e.RowBounds.Width * overHealHPS / topTotalHPS);
                    petWidth = (int)(e.RowBounds.Width * currentItem.PetHPS / topTotalHPS);
                    break;
                case "DamagePercent":
                case "TotalDamage":
                    long topDamage = filteredItems.Max(x => x.PetDamage + x.Damage);
                    width = (int)((double)e.RowBounds.Width * currentItem.Damage / topDamage);
                    petWidth = (int)((double)e.RowBounds.Width * currentItem.PetDamage / topDamage);
                    break;
                case "HealedPercent":
                case "TotalHealed":
                    long topHealed = filteredItems.Max(x => x.PetHealed + x.Healed);
                    width = (int)((double)e.RowBounds.Width * currentItem.Healed / topHealed);
                    petWidth = (int)((double)e.RowBounds.Width * currentItem.PetHealed / topHealed);
                    break;
                case "OverHealPercent":
                case "TotalOverHeal":
                    long topOverHeal = filteredItems.Max(x => x.OverHeal + x.PetOverHeal);
                    width = (int)((double)e.RowBounds.Width * currentItem.OverHeal / topOverHeal);
                    petWidth = (int)((double)e.RowBounds.Width * currentItem.PetOverHeal / topOverHeal);
                    break;
                case "DirectHitPercent":
                    int topDirectHits = filteredItems.Max(x => x.DirectHits + x.PetDirectHits);
                    width = (int)((float)e.RowBounds.Width * currentItem.DirectHits / topDirectHits);
                    petWidth = (int)((float)e.RowBounds.Width * currentItem.PetDirectHits / topDirectHits);
                    break;
                case "CritPercent":
                    int topCritHits = filteredItems.Max(x => x.CritHits + x.PetCritHits);
                    width = (int)((float)e.RowBounds.Width * currentItem.CritHits / topCritHits);
                    petWidth = (int)((float)e.RowBounds.Width * currentItem.PetCritHits / topCritHits);
                    break;
                case "CritHealPercent":
                    int topCritHeals = filteredItems.Max(x => x.CritHeals + x.PetCritHeals);
                    width = (int)((float)e.RowBounds.Width * currentItem.CritHeals / topCritHeals);
                    petWidth = (int)((float)e.RowBounds.Width * currentItem.PetCritHeals / topCritHeals);
                    break;
                case "TotalMaxHitString":
                    long topMaxHit = filteredItems.Max(x => x.TotalMaxHit);
                    width = (int)((double)e.RowBounds.Width * currentItem.TotalMaxHit / topMaxHit);
                    break;
                case "TotalMaxHealString":
                    long topMaxHeal = filteredItems.Max(x => x.TotalMaxHeal);
                    width = (int)((double)e.RowBounds.Width * currentItem.TotalMaxHeal / topMaxHeal);
                    break;
                case "TotalSwings":
                    int topSwings = filteredItems.Max(x => x.Swings + x.PetSwings);
                    width = (int)((float)e.RowBounds.Width * currentItem.Swings / topSwings);
                    petWidth = (int)((float)e.RowBounds.Width * currentItem.PetSwings / topSwings);
                    break;
                case "TotalMisses":
                    int topMisses = filteredItems.Max(x => x.Misses + x.PetMisses);
                    width = (int)((float)e.RowBounds.Width * currentItem.Misses / topMisses);
                    petWidth = (int)((float)e.RowBounds.Width * currentItem.PetMisses / topMisses);
                    break;
                case "Deaths":
                    int topDeaths = filteredItems.Max(x => x.Deaths);
                    width = (int)((float)e.RowBounds.Width * currentItem.Deaths / topDeaths);
                    break;
            }

            Color mainColor = Color.FromArgb(128, 128, 128);

            if (jobColorDictionary.ContainsKey(currentItem.Job))
            {
                mainColor = jobColorDictionary[currentItem.Job];
            }

            if (width > 0)
            {
                e.Graphics.FillRectangle(new SolidBrush(mainColor), new Rectangle(e.RowBounds.X, e.RowBounds.Y, width, e.RowBounds.Height));
            }

            if (overHealWidth > 0)
            {
                e.Graphics.FillRectangle(
                    new SolidBrush(ActPlugin.Setting.OverlaySetting.HpsOverHealColor),
                    new Rectangle(e.RowBounds.X + width, e.RowBounds.Y, overHealWidth, e.RowBounds.Height));
            }

            if (petWidth > 0)
            {
                e.Graphics.FillRectangle(
                    new SolidBrush(Color.FromArgb(
                        Math.Min(mainColor.R + 50, 255),
                        Math.Min(mainColor.G + 50, 255),
                        Math.Min(mainColor.B + 50, 255))),
                    new Rectangle(e.RowBounds.X + width + overHealWidth, e.RowBounds.Y, petWidth, e.RowBounds.Height));
            }
        }
    }
}