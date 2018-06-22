using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace PorierACTPlugin
{
    public partial class OverlaySetting
    {
        [Browsable(true)]
        [Category("OVERLAY_SETTING_CATEGORY_DPS_TABLE")]
        [DisplayName("OVERLAY_SETTING_DISPLAY_NAME_DPS_TABLE")]
        [Description("OVERLAY_SETTING_DESCRIPTION_DPS_TABLE")]
        public OverlayTableSetting DpsTable
        {
            get
            {
                return dpsTable;
            }

            set
            {
                dpsTable = value;
            }
        }
        private OverlayTableSetting dpsTable = new OverlayTableSetting
        {
            CurrentSortingDataPropertyName = "TotalDPS",
            CurrentSortOrder = SortOrder.Descending,
            CombinePets = true,
            IncludeLimitInSorting = false,
            ShowLimit = true,
            ShowOnlyHealer = false,
            ShowGraph = true,
            CurrentGraphDataPropertyName = "TotalDPS"
        };

        public void InitializeDpsTable()
        {
            dpsTable.Columns = new List<ColumnWrapper>
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
                    Name = "DPS",
                    DataPropertyName = "TotalDPS",
                    DisplayIndex = 2,
                    Visible = true
                },
                new ColumnWrapper
                {
                    Name = "D%",
                    DataPropertyName = "DamagePercent",
                    DisplayIndex = 3,
                    Visible = false
                },
                new ColumnWrapper
                {
                    Name = "Damage",
                    DataPropertyName = "TotalDamage",
                    DisplayIndex = 4,
                    Visible = false
                },
                new ColumnWrapper
                {
                    Name = "D.H%",
                    DataPropertyName = "DirectHitPercent",
                    DisplayIndex = 5,
                    Visible = true
                },
                new ColumnWrapper
                {
                    Name = "C%",
                    DataPropertyName = "CritPercent",
                    DisplayIndex = 6,
                    Visible = true
                },

                new ColumnWrapper
                {
                    Name = "MaxHit",
                    DataPropertyName = "TotalMaxHitString",
                    DisplayIndex = 7,
                    Visible = true
                },
                new ColumnWrapper
                {
                    Name = "S.",
                    DataPropertyName = "TotalSwings",
                    DisplayIndex = 8,
                    Visible = true
                },
                new ColumnWrapper
                {
                    Name = "M.",
                    DataPropertyName = "TotalMisses",
                    DisplayIndex = 9,
                    Visible = true
                },
                new ColumnWrapper
                {
                    Name = "D.",
                    DataPropertyName = "Deaths",
                    DisplayIndex = 10,
                    Visible = true
                }
            };
        }
    }
}