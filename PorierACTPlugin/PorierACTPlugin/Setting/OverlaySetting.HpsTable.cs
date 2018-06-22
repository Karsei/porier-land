using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace PorierACTPlugin
{
    public partial class OverlaySetting
    {
        [Browsable(true)]
        [Category("OVERLAY_SETTING_CATEGORY_HPS_TABLE")]
        [DisplayName("OVERLAY_SETTING_DISPLAY_NAME_HPS_TABLE")]
        [Description("OVERLAY_SETTING_DESCRIPTION_HPS_TABLE")]
        public OverlayTableSetting HpsTable
        {
            get
            {
                return hpsTable;
            }

            set
            {
                hpsTable = value;
            }
        }
        private OverlayTableSetting hpsTable = new OverlayTableSetting
        {
            CurrentSortingDataPropertyName = "TotalHPS",
            CurrentSortOrder = SortOrder.Descending,
            CombinePets = true,
            IncludeLimitInSorting = false,
            ShowLimit = false,
            ShowOnlyHealer = true,
            ShowGraph = true,
            CurrentGraphDataPropertyName = "TotalHPS"
        };

        [Browsable(true)]
        [Category("OVERLAY_SETTING_CATEGORY_HPS_TABLE")]
        [DisplayName("OVERLAY_SETTING_DISPLAY_NAME_HPS_OVER_HEAL_COLOR")]
        [Description("OVERLAY_SETTING_DESCRIPTION_HPS_OVER_HEAL_COLOR")]
        public ColorWrapper HpsOverHealColor
        {
            get
            {
                return hpsOverHealColor;
            }

            set
            {
                hpsOverHealColor = value;
            }
        }
        private ColorWrapper hpsOverHealColor = Color.FromArgb(128, 128, 128);

        public void InitializeHpsTable()
        {
            hpsTable.Columns = new List<ColumnWrapper>
            {
                new ColumnWrapper
                {
                    Name = "Name",
                    DataPropertyName = "Name",
                    DisplayIndex = 0,
                    Visible = true
                },
                new ColumnWrapper
                {
                    Name = "Job",
                    DataPropertyName = "JobIcon",
                    DisplayIndex = 1,
                    Visible = true
                },
                new ColumnWrapper
                {
                    Name = "HPS",
                    DataPropertyName = "TotalHPS",
                    DisplayIndex = 2,
                    Visible = true
                },
                new ColumnWrapper
                {
                    Name = "H%",
                    DataPropertyName = "HealedPercent",
                    DisplayIndex = 3,
                    Visible = false
                },
                new ColumnWrapper
                {
                    Name = "Heal",
                    DataPropertyName = "TotalHealed",
                    DisplayIndex = 4,
                    Visible = false
                },
                new ColumnWrapper
                {
                    Name = "O.H%",
                    DataPropertyName = "OverHealPercent",
                    DisplayIndex = 5,
                    Visible = true
                },
                new ColumnWrapper
                {
                    Name = "OverHeal",
                    DataPropertyName = "TotalOverHeal",
                    DisplayIndex = 6,
                    Visible = false
                },
                new ColumnWrapper
                {
                    Name = "C.H%",
                    DataPropertyName = "CritHealPercent",
                    DisplayIndex = 7,
                    Visible = false
                },
                new ColumnWrapper
                {
                    Name = "MaxHeal",
                    DataPropertyName = "TotalMaxHealString",
                    DisplayIndex = 8,
                    Visible = true
                }
            };
        }
    }
}